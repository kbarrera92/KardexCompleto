USE [farmaciahorro_ispro2]
GO
-- El vencimiento de un vale nuevo es el ultimo dia del mes en que se crea (antes: un mes despues).
-- Los vales existentes conservan su fechaVencimiento. sp_consultaVales compara por fecha (sin hora)
-- para que el vale no figure como VENCIDO durante el propio ultimo dia.
-- Requiere haber ejecutado antes migracion_vale_abierto.sql.

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
			@idExistente INT, @base SMALLINT = 0

	SELECT @max = COUNT(1), @total = SUM(subtotal) FROM @lineas

	BEGIN TRY
		BEGIN TRANSACTION GRABAVALE

		-- Vale abierto del vendedor en la sucursal (si hay mas de uno, el mas reciente)
		SELECT @idExistente = MAX(idVale)
		FROM VALE WITH (UPDLOCK, HOLDLOCK)
		WHERE vendedor = @vendedor AND sucursal = @sucursal AND estado = 'P'

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
			S.nombreSuc,
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
