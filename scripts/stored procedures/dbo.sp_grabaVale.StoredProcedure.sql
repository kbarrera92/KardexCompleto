-- =============================================
-- Description:	Graba un vale de mercadería para un vendedor.
--				Descuenta los productos de EXISTENCIAS pero NO genera venta.
--				El vale vence al mes de su creación.
-- Return:		0 = ok, -1 = error (detalle en @message)
-- =============================================
IF OBJECT_ID('dbo.sp_grabaVale') IS NOT NULL
	DROP PROCEDURE dbo.sp_grabaVale
GO
CREATE PROCEDURE [dbo].[sp_grabaVale]
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
	SELECT D.nDetalleV, D.producto, D.cantidad,
		ISNULL(PS.precio, P.precio),
		D.cantidad * ISNULL(PS.precio, P.precio)
	FROM @detalles D
	INNER JOIN PRODUCTOS P ON D.producto = P.idProducto
	LEFT JOIN PRECIOS_SUCURSAL PS ON PS.idProducto = P.idProducto AND PS.idSucursal = @sucursal
	ORDER BY D.nDetalleV

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
			@query NVARCHAR(500), @total DECIMAL(10,2), @fecha DATETIME = GETDATE()

	SELECT @max = COUNT(1), @total = SUM(subtotal) FROM @lineas

	BEGIN TRY
		BEGIN TRANSACTION GRABAVALE

		INSERT INTO VALE (fecha, fechaVencimiento, sucursal, vendedor, usuario, total, saldo, estado)
		VALUES (@fecha, DATEADD(MONTH, 1, @fecha), @sucursal, @vendedor, @usuario, @total, @total, 'P')

		SET @idVale = SCOPE_IDENTITY()

		INSERT INTO DETALLEVALE (idVale, nDetalle, producto, cantidad, precio, subtotal)
		SELECT @idVale, nDetalle, producto, cantidad, precio, subtotal FROM @lineas

		WHILE @fila <= @max
		BEGIN
			SELECT @producto = producto, @cantidad = cantidad FROM @lineas WHERE fila = @fila

			-- Validar existencia (las líneas repetidas del mismo producto se validan una tras otra)
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
		SELECT @message = 'EL VALE No. ' + CAST(@idVale AS VARCHAR(10)) + ' SE REGISTRO CORRECTAMENTE'
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
