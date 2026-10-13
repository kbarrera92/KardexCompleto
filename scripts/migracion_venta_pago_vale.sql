USE [farmaciahorro_ispro2]
GO
-- Cada pago de vale (PAGOVALE) ahora se graba tambien como venta (VENTAS / DETALLEVENTAS),
-- SIN descontar EXISTENCIAS (el inventario ya se desconto al grabar el vale).
-- Ejecutar ANTES de desplegar la version nueva del sistema (FormVerVales envia @horasDiferencia).
-- Requiere haber ejecutado antes migracion_anulacion_pagos_vale.sql.

IF COL_LENGTH('dbo.PAGOVALE', 'nVenta') IS NULL
	ALTER TABLE dbo.PAGOVALE ADD nVenta int NULL
GO
IF OBJECT_ID('dbo.FK_PAGOVALE_VENTAS', 'F') IS NULL
	ALTER TABLE dbo.PAGOVALE WITH CHECK ADD CONSTRAINT FK_PAGOVALE_VENTAS FOREIGN KEY (nVenta) REFERENCES dbo.VENTAS (nVenta)
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

	DECLARE @estado CHAR(1), @sucursal INT, @vendedor INT, @monto DECIMAL(10,2), @idPago INT, @idTurno INT, @nVenta INT
	DECLARE @fechaVenta DATETIME = DATEADD(HOUR, @horasDiferencia, GETDATE())

	BEGIN TRY
		BEGIN TRANSACTION PAGAVALE

		SELECT @estado = estado, @sucursal = sucursal, @vendedor = vendedor FROM VALE WITH (UPDLOCK) WHERE idVale = @idVale

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

		-- El pago genera una venta, por lo que requiere caja aperturada
		SELECT @idTurno = idTurno FROM APERTURACAJAS WHERE estado = 'A' AND sucursal = @sucursal
		IF @idTurno IS NULL
		BEGIN
			SELECT @message = 'NO SE HA APERTURADO LA CAJA PARA ESTA SUCURSAL'
			ROLLBACK TRANSACTION PAGAVALE
			RETURN -1
		END

		SELECT @monto = SUM(D.subtotal)
		FROM DETALLEVALE D INNER JOIN @lineas L ON D.nDetalle = L.nDetalle
		WHERE D.idVale = @idVale

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

CREATE OR ALTER PROCEDURE dbo.sp_anulaPagoVale
	@idPago		INT,
	@usuario	INT,
	@motivo		VARCHAR(200),
	@message	VARCHAR(200) = NULL OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	IF ISNULL(LTRIM(RTRIM(@motivo)), '') = ''
	BEGIN
		SELECT @message = 'DEBE INDICAR EL MOTIVO DE LA ANULACION'
		RETURN -1
	END

	DECLARE @estado CHAR(1), @idVale INT, @monto DECIMAL(10,2), @nVenta INT

	BEGIN TRY
		BEGIN TRANSACTION ANULAPAGOVALE

		SELECT @estado = estado, @idVale = idVale, @monto = monto, @nVenta = nVenta
		FROM PAGOVALE WITH (UPDLOCK) WHERE idPago = @idPago

		IF @estado IS NULL
		BEGIN
			SELECT @message = 'EL PAGO NO EXISTE'
			ROLLBACK TRANSACTION ANULAPAGOVALE
			RETURN -1
		END

		IF @estado <> 'A'
		BEGIN
			SELECT @message = 'EL PAGO YA ESTA ANULADO'
			ROLLBACK TRANSACTION ANULAPAGOVALE
			RETURN -1
		END

		-- Se conserva idPago en las lineas como rastro hasta que se vuelvan a pagar
		UPDATE DETALLEVALE SET pagado = 0 WHERE idPago = @idPago

		UPDATE VALE SET saldo = saldo + @monto, estado = 'P' WHERE idVale = @idVale

		-- Elimina la venta generada por el pago, SIN devolver existencias
		-- (el producto sigue en poder del vendedor; el inventario lo controla el vale)
		IF @nVenta IS NOT NULL
		BEGIN
			INSERT INTO VENTASELIMINADAS (nVenta, fechaVenta, usuario, total, documento, idSucursal, cliente, efectivo, tarjeta, autorizacion, codempleado, turno, fechElim)
			SELECT nVenta, fechaVenta, usuario, total, documento, idSucursal, cliente, efectivo, tarjeta, autorizacion, codempleado, turno, GETDATE()
			FROM VENTAS WHERE nVenta = @nVenta

			INSERT INTO DETALLEVENTASELIMINADOS (ndetalle, nventa, producto, cantidad, precio, subtotal, bonificacion, costo, felim)
			SELECT nDetalleV, nVenta, producto, cantidad, precio, subtotal, bonificacion, costo, GETDATE()
			FROM DETALLEVENTAS WHERE nVenta = @nVenta

			-- Primero se suelta la referencia (FK) hacia la venta
			UPDATE PAGOVALE SET nVenta = NULL WHERE idPago = @idPago

			DELETE DETALLEVENTAS WHERE nVenta = @nVenta
			DELETE VENTAS WHERE nVenta = @nVenta
		END

		UPDATE PAGOVALE
		SET estado = 'N', fechaAnulacion = GETDATE(), usuarioAnula = @usuario, motivoAnulacion = @motivo
		WHERE idPago = @idPago

		COMMIT TRANSACTION ANULAPAGOVALE
		SELECT @message = 'PAGO ANULADO. SE RESTAURO Q ' + CONVERT(VARCHAR(20), @monto) + ' AL SALDO DEL VALE'
		RETURN 0
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
		SELECT @message = 'HUBO UN ERROR AL ANULAR EL PAGO'
		RETURN -1
	END CATCH
END
GO

-- Las ventas generadas por pagos de vales no se eliminan desde "Ver ventas"
-- porque esa eliminacion devuelve existencias. Se anulan desde "Pagos de vales".
CREATE OR ALTER PROCEDURE dbo.sp_eliminaVenta
	@message		VARCHAR(200) = NULL OUTPUT,
	@idVenta		INT,
	@nameSucursal	VARCHAR(40)
AS
BEGIN
	SET NOCOUNT ON;

	IF EXISTS (SELECT 1 FROM PAGOVALE WHERE nVenta = @idVenta)
	BEGIN
		SELECT @message = 'LA VENTA CORRESPONDE AL PAGO DE UN VALE. ANULE EL PAGO DESDE PAGOS DE VALES'
		RETURN -1
	END

	DECLARE @sucursal INT
	SELECT @sucursal = idSucursal FROM SUCURSAL WHERE nombreSuc = @nameSucursal
	IF ISNULL(@sucursal, '') = ''
	BEGIN
		SELECT @message = 'SUCURSAL NO ENCONTRADA'
		RETURN -1
	END

	BEGIN TRAN ELIMINAVENTA

	--COPIA REGISTRO A TABLA DE LOG
	INSERT INTO VENTASELIMINADAS (nVenta, fechaVenta, usuario, total,documento, idSucursal, cliente, efectivo,tarjeta, autorizacion, codempleado, turno, fechElim)
	SELECT nVenta, fechaVenta, usuario, total,documento, idSucursal, cliente, efectivo,tarjeta, autorizacion, codempleado, turno, GETDATE()
	FROM VENTAS
	WHERE nVenta = @idVenta
	IF @@ROWCOUNT = 0 OR @@ERROR <> 0
	BEGIN
		SELECT @message = 'HUBO UN ERROR AL TRASLADAR LA VENTA A LA TABLA DE LOG'
		ROLLBACK TRAN ELIMINAVENTA
		RETURN -1
	END

	INSERT INTO DETALLEVENTASELIMINADOS (ndetalle, nventa, producto, cantidad, precio, subtotal, bonificacion, costo, felim)
	SELECT nDetalleV, nVenta, producto, cantidad, precio, subtotal, bonificacion, costo, GETDATE()
	FROM DETALLEVENTAS
	WHERE nVenta = @idVenta
	IF @@ROWCOUNT = 0 OR @@ERROR <> 0
	BEGIN
		SELECT @message = 'HUBO UN ERROR AL TRASLADAR LA VENTA A LA TABLA DE LOG'
		ROLLBACK TRAN ELIMINAVENTA
		RETURN -1
	END

	--ELIMINA REGISTROS DE TABLAS CABECERA DETALLE
	DECLARE @QUERY NVARCHAR(500),
			@nDetalle SMALLINT,
			@nVenta INT,
			@producto INT,
			@cantidad INT

	DECLARE CURSORDETALLES CURSOR FOR
	SELECT nDetalleV, nVenta, producto, cantidad
	FROM DETALLEVENTAS
	WHERE nVenta = @idVenta

	OPEN CURSORDETALLES
	FETCH NEXT FROM CURSORDETALLES INTO @nDetalle, @nVenta, @producto, @cantidad

	WHILE @@FETCH_STATUS = 0
	BEGIN
		--ELIMINA DETALLES
		DELETE DETALLEVENTAS WHERE nDetalleV = @nDetalle AND nVenta = @nVenta
		IF @@ERROR <> 0
		BEGIN
			ROLLBACK TRANSACTION ELIMINAVENTA
			SET @message = 'HUBO UN ERROR AL ELIMINAR EL DETALLE'
			RETURN -1
		END

		SET @QUERY =
		'UPDATE EXISTENCIAS SET suc_' + CAST(@sucursal AS VARCHAR) + ' = suc_' + CAST(@sucursal AS VARCHAR) + ' + ' + CAST(@cantidad AS VARCHAR) + ' WHERE idProducto = ' + CAST(@producto AS VARCHAR)

		EXEC sp_executesql @QUERY
		IF @@ERROR <> 0
		BEGIN
			ROLLBACK TRANSACTION ELIMINAVENTA
			SET @message = 'HUBO UN ERROR AL ACTUALIZAR LAS EXISTENCIAS'
			RETURN -1
		END

		FETCH NEXT FROM CURSORDETALLES INTO @nDetalle, @nVenta, @producto, @cantidad
	END

	CLOSE CURSORDETALLES
	DEALLOCATE CURSORDETALLES

	DELETE VENTAS WHERE nVenta = @idVenta
	IF @@ROWCOUNT = 0 OR @@ERROR <> 0
	BEGIN
		SELECT @message = 'HUBO UN ERROR AL ELIMINAR EL ENCABEZADO'
		ROLLBACK TRAN ELIMINAVENTA
		RETURN -1
	END

	SELECT @message = 'SE ELIMINO LA VENTA CORRECTAMENTE'
	COMMIT TRAN ELIMINAVENTA
	RETURN 0
END
GO
