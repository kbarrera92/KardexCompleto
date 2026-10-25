USE [farmaciahorro_ispro2]
GO
-- El pago de un vale se cobra desde el Punto de Venta (doble clic en "Ver vales"), con los datos de una venta normal:
-- cliente, documento, efectivo/tarjeta, autorizacion y codigo de empleado.
--   sp_pagaVale     : recibe los datos del cobro y devuelve el numero de venta generado (@nVenta).
--                     Todos los parametros nuevos son opcionales; sin ellos se comporta igual que antes
--                     (cliente C/F, todo efectivo, empleado = vendedor del vale).
--                     Sigue sin tocar EXISTENCIAS (ya se descontaron al grabar el vale) y sin bonificacion.
--   sp_consultaVales: ademas devuelve idSucursal e idVendedor del vale (la aplicacion los necesita para cobrarlo).
-- Requiere haber ejecutado antes migracion_limites_vales.sql (fn_parametroMonto) y migracion_venta_pago_vale.sql.
-- Ejecutar ANTES de desplegar la version nueva del sistema.

CREATE OR ALTER PROCEDURE dbo.sp_consultaVales
	@sucursal	INT = NULL,
	@vendedor	INT = NULL,
	@estado		VARCHAR(10) = NULL
AS
BEGIN
	SET NOCOUNT ON;

	SELECT * FROM (
		SELECT
			V.idVale,
			V.fecha,
			V.fechaVencimiento,
			V.sucursal AS idSucursal,
			S.nombreSuc,
			V.vendedor AS idVendedor,
			VE.nombre AS nombreVendedor,
			V.total,
			V.saldo,
			CASE
				WHEN V.estado = 'C' THEN 'PAGADO'
				WHEN CAST(GETDATE() AS DATE) > CAST(V.fechaVencimiento AS DATE) THEN 'VENCIDO'
				ELSE 'PENDIENTE'
			END AS estadoVale
		FROM VALE V
		INNER JOIN VENDEDOR VE ON V.vendedor = VE.idVendedor
		INNER JOIN SUCURSAL S ON V.sucursal = S.idSucursal
		WHERE (ISNULL(@sucursal, 0) = 0 OR V.sucursal = @sucursal)
		    AND (ISNULL(@vendedor, 0) = 0 OR V.vendedor = @vendedor)
	) X
	WHERE ISNULL(@estado, '') = '' OR X.estadoVale = @estado
	ORDER BY X.idVale DESC
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_pagaVale
	@idVale			INT,
	@usuario		INT,
	@lineas			LINEASVALE READONLY,
	@horasDiferencia	INT = 0,
	@message		VARCHAR(200) = NULL OUTPUT,
	@cliente		VARCHAR(10) = 'C/F',
	@documento		VARCHAR(20) = NULL,
	@tarjeta		DECIMAL(10,2) = 0,
	@autoriza		VARCHAR(16) = '',
	@codempleado		INT = NULL,
	@nVenta			INT = NULL OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	SET @nVenta = NULL

	IF NOT EXISTS (SELECT 1 FROM @lineas)
	BEGIN
		SELECT @message = 'NO SE SELECCIONO NINGUN PRODUCTO'
		RETURN -1
	END

	DECLARE @estado CHAR(1), @sucursal INT, @vendedor INT, @monto DECIMAL(10,2), @idPago INT, @idTurno INT, @sec INT,
			@saldo DECIMAL(10,2), @pagoMinimo DECIMAL(10,2) = dbo.fn_parametroMonto('valePagoMinimo', 150),
			@efectivo DECIMAL(10,2)
	DECLARE @fechaVenta DATETIME = DATEADD(HOUR, @horasDiferencia, GETDATE())

	BEGIN TRY
		BEGIN TRANSACTION PAGAVALE

		SELECT @estado = estado, @sucursal = sucursal, @vendedor = vendedor, @saldo = saldo FROM VALE WITH (UPDLOCK) WHERE idVale = @idVale

		IF @estado IS NULL
		BEGIN
			SELECT @message = 'EL VALE NO EXISTE'
			ROLLBACK TRANSACTION PAGAVALE
			RETURN -1
		END

		IF @estado <> 'P'
		BEGIN
			SELECT @message = 'EL VALE YA ESTA PAGADO'
			ROLLBACK TRANSACTION PAGAVALE
			RETURN -1
		END

		-- Todas las lineas enviadas deben existir en el vale y estar sin pagar
		IF EXISTS (
			SELECT 1 FROM @lineas L
			LEFT JOIN DETALLEVALE D ON D.idVale = @idVale AND D.nDetalle = L.nDetalle
			WHERE D.nDetalle IS NULL OR D.pagado = 1
		)
		BEGIN
			SELECT @message = 'HAY PRODUCTOS QUE NO PERTENECEN AL VALE O YA ESTAN PAGADOS'
			ROLLBACK TRANSACTION PAGAVALE
			RETURN -1
		END

		SELECT @monto = SUM(D.subtotal)
		FROM DETALLEVALE D INNER JOIN @lineas L ON D.nDetalle = L.nDetalle
		WHERE D.idVale = @idVale

		-- Saldo mayor al minimo: el pago debe ser al menos el minimo. Si no, se paga el saldo completo
		IF @saldo > @pagoMinimo AND @monto < @pagoMinimo
		BEGIN
			SELECT @message = 'EL PAGO MINIMO ES DE Q ' + CONVERT(VARCHAR(20), @pagoMinimo)
				+ ' (SALDO Q ' + CONVERT(VARCHAR(20), @saldo) + ')'
			ROLLBACK TRANSACTION PAGAVALE
			RETURN -1
		END

		IF @saldo <= @pagoMinimo AND @monto < @saldo
		BEGIN
			SELECT @message = 'EL SALDO DEL VALE (Q ' + CONVERT(VARCHAR(20), @saldo) + ') DEBE PAGARSE COMPLETO'
			ROLLBACK TRANSACTION PAGAVALE
			RETURN -1
		END

		-- Datos del cobro. El efectivo es lo que resta del monto despues de la tarjeta
		SET @tarjeta = ISNULL(@tarjeta, 0)
		IF @tarjeta < 0 OR @tarjeta > @monto
		BEGIN
			SELECT @message = 'EL MONTO CON TARJETA NO ES VALIDO'
			ROLLBACK TRANSACTION PAGAVALE
			RETURN -1
		END

		IF @tarjeta > 0 AND ISNULL(LTRIM(RTRIM(@autoriza)), '') = ''
		BEGIN
			SELECT @message = 'INGRESE EL NUMERO DE AUTORIZACION DEL PAGO CON TARJETA'
			ROLLBACK TRANSACTION PAGAVALE
			RETURN -1
		END

		SET @efectivo = @monto - @tarjeta

		-- Sin codigo de empleado se usa el vendedor del vale
		SET @codempleado = ISNULL(@codempleado, @vendedor)
		IF NOT EXISTS (SELECT 1 FROM VENDEDOR WHERE idVendedor = @codempleado AND estado = 1)
		BEGIN
			SELECT @message = 'EL CODIGO DEL EMPLEADO ES INCORRECTO'
			ROLLBACK TRANSACTION PAGAVALE
			RETURN -1
		END

		-- El pago genera una venta, por lo que requiere caja aperturada
		SELECT @idTurno = idTurno FROM APERTURACAJAS WHERE estado = 'A' AND sucursal = @sucursal
		IF @idTurno IS NULL
		BEGIN
			SELECT @message = 'NO SE HA APERTURADO LA CAJA PARA ESTA SUCURSAL'
			ROLLBACK TRANSACTION PAGAVALE
			RETURN -1
		END

		INSERT INTO PAGOVALE (idVale, fecha, monto, usuario, idTurno)
		VALUES (@idVale, GETDATE(), @monto, @usuario, @idTurno)

		SET @idPago = SCOPE_IDENTITY()

		UPDATE D SET pagado = 1, idPago = @idPago
		FROM DETALLEVALE D INNER JOIN @lineas L ON D.nDetalle = L.nDetalle
		WHERE D.idVale = @idVale

		UPDATE VALE
		SET saldo = saldo - @monto,
			estado = CASE WHEN saldo - @monto <= 0 THEN 'C' ELSE 'P' END
		WHERE idVale = @idVale

		-- Venta generada por el pago. NO se toca EXISTENCIAS (ya se descontaron al grabar el vale)
		EXEC @sec = sp_get_sequence 'VENTAS'
		IF @sec = -1
		BEGIN
			SELECT @message = 'LA TABLA VENTAS NO ESTA CONFIGURADA EN LA TABLA SECUENCIALES'
			ROLLBACK TRANSACTION PAGAVALE
			RETURN -1
		END

		INSERT INTO VENTAS (nVenta, fechaVenta, usuario, total, documento, idSucursal, cliente, efectivo, tarjeta, autorizacion, codempleado, turno)
		VALUES (@sec, @fechaVenta, @usuario, @monto, @documento, @sucursal, ISNULL(NULLIF(LTRIM(RTRIM(@cliente)), ''), 'C/F'), @efectivo, @tarjeta, ISNULL(@autoriza, ''), @codempleado, @idTurno)

		-- Sin bonificacion: el vendedor compra para si mismo
		INSERT INTO DETALLEVENTAS (nDetalleV, nVenta, producto, cantidad, precio, subtotal, bonificacion, costo)
		SELECT ROW_NUMBER() OVER (ORDER BY D.nDetalle), @sec, D.producto, D.cantidad, D.precio, D.subtotal, 0, ISNULL(P.costo, 0)
		FROM DETALLEVALE D
		INNER JOIN @lineas L ON D.nDetalle = L.nDetalle
		INNER JOIN PRODUCTOS P ON D.producto = P.idProducto
		WHERE D.idVale = @idVale

		UPDATE PAGOVALE SET nVenta = @sec WHERE idPago = @idPago

		COMMIT TRANSACTION PAGAVALE
		SET @nVenta = @sec
		SELECT @message = 'PAGO REGISTRADO POR Q ' + CONVERT(VARCHAR(20), @monto) + '. VENTA No. ' + CONVERT(VARCHAR(20), @sec)
		RETURN 0
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
		SET @nVenta = NULL
		SELECT @message = 'HUBO UN ERROR AL REGISTRAR EL PAGO'
		RETURN -1
	END CATCH
END
GO
