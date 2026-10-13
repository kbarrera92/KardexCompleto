USE [farmaciahorro_ispro2]
GO
-- Valida en el servidor los limites de vales (los mismos que valida la aplicacion):
--   valeMaximo     : sp_grabaVale rechaza el vale si saldo pendiente + total nuevo supera este monto.
--   valePagoMinimo : sp_pagaVale exige pagar al menos este monto si el saldo del vale lo supera;
--                    si el saldo es igual o menor, exige pagar el saldo completo.
-- Los valores se leen de PARAMETRO (global, idSucursal NULL); si no existen, 300.00 y 150.00.
-- Este script REEMPLAZA sp_grabaVale (migracion_vale_abierto.sql) y sp_pagaVale (migracion_venta_pago_vale.sql):
-- ejecutarlo despues de ambos. Se recomienda correr tambien migracion_parametros_vales.sql.

CREATE OR ALTER FUNCTION dbo.fn_parametroMonto
(
	@clave		VARCHAR(50),
	@porDefecto	DECIMAL(10,2)
)
RETURNS DECIMAL(10,2)
AS
BEGIN
	DECLARE @v DECIMAL(10,2)

	SELECT @v = TRY_CAST(valor AS DECIMAL(10,2))
	FROM dbo.PARAMETRO
	WHERE clave = @clave AND idSucursal IS NULL

	RETURN CASE WHEN ISNULL(@v, 0) > 0 THEN @v ELSE @porDefecto END
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_grabaVale
	@sucursal	INT,
	@vendedor	INT,
	@usuario	INT,
	@detalles	DETALLESVENTA READONLY,
	@message	VARCHAR(200) = NULL OUTPUT,
	@idVale		INT = NULL OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	IF NOT EXISTS (SELECT 1 FROM VENDEDOR WHERE idVendedor = @vendedor AND estado = 1)
	BEGIN
		SELECT @message = 'EL VENDEDOR NO EXISTE O ESTA INACTIVO'
		RETURN -1
	END

	IF NOT EXISTS (SELECT 1 FROM @detalles)
	BEGIN
		SELECT @message = 'EL VALE NO TIENE PRODUCTOS'
		RETURN -1
	END

	DECLARE @lineas TABLE (
		fila		INT IDENTITY(1,1),
		nDetalle	SMALLINT,
		producto	INT,
		cantidad	INT,
		precio		DECIMAL(10,2),
		subtotal	DECIMAL(10,2)
	)

	-- El precio se toma del servidor (precio de la sucursal, o el general si no tiene), no del cliente
	INSERT INTO @lineas (nDetalle, producto, cantidad, precio, subtotal)
	SELECT D.nDetalleVenta, D.producto, D.cantidad,
		ISNULL(PS.precio, P.precio),
		D.cantidad * ISNULL(PS.precio, P.precio)
	FROM @detalles D
	INNER JOIN PRODUCTOS P ON D.producto = P.idProducto
	LEFT JOIN PRECIOS_SUCURSAL PS ON PS.idProducto = P.idProducto AND PS.idSucursal = @sucursal
	ORDER BY D.nDetalleVenta

	IF (SELECT COUNT(1) FROM @lineas) <> (SELECT COUNT(1) FROM @detalles)
	BEGIN
		SELECT @message = 'HAY PRODUCTOS QUE NO EXISTEN EN EL CATALOGO'
		RETURN -1
	END

	IF EXISTS (SELECT 1 FROM @lineas WHERE cantidad <= 0)
	BEGIN
		SELECT @message = 'HAY PRODUCTOS CON CANTIDAD NO VALIDA'
		RETURN -1
	END

	DECLARE @fila INT = 1, @max INT, @producto INT, @cantidad INT, @existencia DECIMAL(10,2),
			@query NVARCHAR(500), @total DECIMAL(10,2), @fecha DATETIME = GETDATE(),
			@idExistente INT, @base SMALLINT = 0,
			@valeMaximo DECIMAL(10,2) = dbo.fn_parametroMonto('valeMaximo', 300), @saldoActual DECIMAL(10,2)

	SELECT @max = COUNT(1), @total = SUM(subtotal) FROM @lineas

	BEGIN TRY
		BEGIN TRANSACTION GRABAVALE

		-- Vale abierto del vendedor en la sucursal (si hay mas de uno, el mas reciente)
		SELECT @idExistente = MAX(idVale)
		FROM VALE WITH (UPDLOCK, HOLDLOCK)
		WHERE vendedor = @vendedor AND sucursal = @sucursal AND estado = 'P'

		-- El limite aplica al saldo pendiente: lo que ya debe el vendedor mas lo nuevo
		SELECT @saldoActual = ISNULL(saldo, 0) FROM VALE WHERE idVale = @idExistente
		SET @saldoActual = ISNULL(@saldoActual, 0)

		IF @saldoActual + @total > @valeMaximo
		BEGIN
			SELECT @message = 'EL MONTO MAXIMO DE UN VALE ES Q ' + CONVERT(VARCHAR(20), @valeMaximo)
				+ '. SALDO PENDIENTE Q ' + CONVERT(VARCHAR(20), @saldoActual)
				+ ' + NUEVO Q ' + CONVERT(VARCHAR(20), @total)
			ROLLBACK TRANSACTION GRABAVALE
			SET @idVale = NULL
			RETURN -1
		END

		IF @idExistente IS NULL
		BEGIN
			INSERT INTO VALE (fecha, fechaVencimiento, sucursal, vendedor, usuario, total, saldo, estado)
			VALUES (@fecha, EOMONTH(@fecha), @sucursal, @vendedor, @usuario, @total, @total, 'P')

			SET @idVale = SCOPE_IDENTITY()
		END
		ELSE
		BEGIN
			SET @idVale = @idExistente

			SELECT @base = ISNULL(MAX(nDetalle), 0) FROM DETALLEVALE WHERE idVale = @idVale

			UPDATE VALE SET total = total + @total, saldo = saldo + @total WHERE idVale = @idVale
		END

		-- Las lineas nuevas continuan la numeracion del vale
		INSERT INTO DETALLEVALE (idVale, nDetalle, producto, cantidad, precio, subtotal)
		SELECT @idVale, @base + fila, producto, cantidad, precio, subtotal FROM @lineas

		WHILE @fila <= @max
		BEGIN
			SELECT @producto = producto, @cantidad = cantidad FROM @lineas WHERE fila = @fila

			-- Validar existencia (las lineas repetidas del mismo producto se validan una tras otra)
			SET @query = N'SELECT @stk = ISNULL(suc_' + CAST(@sucursal AS NVARCHAR(10)) + N', 0) FROM EXISTENCIAS WHERE idProducto = @p'
			SET @existencia = NULL
			EXEC sp_executesql @query, N'@p INT, @stk DECIMAL(10,2) OUTPUT', @p = @producto, @stk = @existencia OUTPUT

			IF @existencia IS NULL OR @existencia < @cantidad
			BEGIN
				SELECT @message = 'EXISTENCIA INSUFICIENTE PARA EL PRODUCTO ' + CAST(@producto AS VARCHAR(10))
				ROLLBACK TRANSACTION GRABAVALE
				SET @idVale = NULL
				RETURN -1
			END

			SET @query = N'UPDATE EXISTENCIAS SET suc_' + CAST(@sucursal AS NVARCHAR(10)) + N' = suc_' + CAST(@sucursal AS NVARCHAR(10)) + N' - @c WHERE idProducto = @p'
			EXEC sp_executesql @query, N'@c INT, @p INT', @c = @cantidad, @p = @producto

			SET @fila = @fila + 1
		END

		COMMIT TRANSACTION GRABAVALE

		IF @idExistente IS NULL
			SELECT @message = 'EL VALE No. ' + CAST(@idVale AS VARCHAR(10)) + ' SE REGISTRO CORRECTAMENTE'
		ELSE
			SELECT @message = 'SE AGREGARON LOS PRODUCTOS AL VALE No. ' + CAST(@idVale AS VARCHAR(10))
		RETURN 0
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
		SET @idVale = NULL
		SELECT @message = 'HUBO UN ERROR AL REGISTRAR EL VALE'
		RETURN -1
	END CATCH
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_pagaVale
	@idVale			INT,
	@usuario		INT,
	@lineas			LINEASVALE READONLY,
	@horasDiferencia	INT = 0,
	@message		VARCHAR(200) = NULL OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	IF NOT EXISTS (SELECT 1 FROM @lineas)
	BEGIN
		SELECT @message = 'NO SE SELECCIONO NINGUN PRODUCTO'
		RETURN -1
	END

	DECLARE @estado CHAR(1), @sucursal INT, @vendedor INT, @monto DECIMAL(10,2), @idPago INT, @idTurno INT, @nVenta INT,
			@saldo DECIMAL(10,2), @pagoMinimo DECIMAL(10,2) = dbo.fn_parametroMonto('valePagoMinimo', 150)
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
		EXEC @nVenta = sp_get_sequence 'VENTAS'
		IF @nVenta = -1
		BEGIN
			SELECT @message = 'LA TABLA VENTAS NO ESTA CONFIGURADA EN LA TABLA SECUENCIALES'
			ROLLBACK TRANSACTION PAGAVALE
			RETURN -1
		END

		INSERT INTO VENTAS (nVenta, fechaVenta, usuario, total, documento, idSucursal, cliente, efectivo, tarjeta, autorizacion, codempleado, turno)
		VALUES (@nVenta, @fechaVenta, @usuario, @monto, NULL, @sucursal, 'C/F', @monto, 0, '', @vendedor, @idTurno)

		-- Sin bonificacion: el vendedor compra para si mismo
		INSERT INTO DETALLEVENTAS (nDetalleV, nVenta, producto, cantidad, precio, subtotal, bonificacion, costo)
		SELECT ROW_NUMBER() OVER (ORDER BY D.nDetalle), @nVenta, D.producto, D.cantidad, D.precio, D.subtotal, 0, ISNULL(P.costo, 0)
		FROM DETALLEVALE D
		INNER JOIN @lineas L ON D.nDetalle = L.nDetalle
		INNER JOIN PRODUCTOS P ON D.producto = P.idProducto
		WHERE D.idVale = @idVale

		UPDATE PAGOVALE SET nVenta = @nVenta WHERE idPago = @idPago

		COMMIT TRANSACTION PAGAVALE
		SELECT @message = 'PAGO REGISTRADO POR Q ' + CONVERT(VARCHAR(20), @monto) + '. VENTA No. ' + CONVERT(VARCHAR(20), @nVenta)
		RETURN 0
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
		SELECT @message = 'HUBO UN ERROR AL REGISTRAR EL PAGO'
		RETURN -1
	END CATCH
END
GO
