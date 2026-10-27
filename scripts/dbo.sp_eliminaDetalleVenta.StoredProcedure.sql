USE [farmaciahorro_ispro2]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.sp_eliminaDetalleVenta
	@idVenta	INT,
	@nDetalle	SMALLINT,
	@message	VARCHAR(200) = NULL OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	IF EXISTS (SELECT 1 FROM PAGOVALE WHERE nVenta = @idVenta)
	BEGIN
		SELECT @message = 'LA VENTA CORRESPONDE AL PAGO DE UN VALE. ANULE EL PAGO DESDE PAGOS DE VALES'
		RETURN -1
	END

	DECLARE @sucursal INT, @efectivo DECIMAL(18,2), @tarjeta DECIMAL(18,2),
			@producto INT, @cantidad INT, @subtotal DECIMAL(18,2),
			@deEfectivo DECIMAL(18,2), @query NVARCHAR(500)

	BEGIN TRY
		BEGIN TRANSACTION ELIMINADETALLE

		SELECT @sucursal = idSucursal, @efectivo = ISNULL(efectivo, 0), @tarjeta = ISNULL(tarjeta, 0)
		FROM VENTAS WITH (UPDLOCK) WHERE nVenta = @idVenta

		IF @sucursal IS NULL
		BEGIN
			SELECT @message = 'LA VENTA NO EXISTE'
			ROLLBACK TRANSACTION ELIMINADETALLE
			RETURN -1
		END

		SELECT @producto = producto, @cantidad = ISNULL(cantidad, 0), @subtotal = ISNULL(subtotal, 0)
		FROM DETALLEVENTAS WHERE nVenta = @idVenta AND nDetalleV = @nDetalle

		IF @producto IS NULL
		BEGIN
			SELECT @message = 'EL DETALLE NO EXISTE'
			ROLLBACK TRANSACTION ELIMINADETALLE
			RETURN -1
		END

		IF (SELECT COUNT(*) FROM DETALLEVENTAS WHERE nVenta = @idVenta) = 1
		BEGIN
			SELECT @message = 'ES EL UNICO DETALLE DE LA VENTA. ELIMINE LA VENTA DESDE VER VENTAS'
			ROLLBACK TRANSACTION ELIMINADETALLE
			RETURN -1
		END

		-- LOG
		INSERT INTO DETALLEVENTASELIMINADOS (ndetalle, nventa, producto, cantidad, precio, subtotal, bonificacion, costo, felim)
		SELECT nDetalleV, nVenta, producto, cantidad, precio, subtotal, bonificacion, costo, GETDATE()
		FROM DETALLEVENTAS WHERE nVenta = @idVenta AND nDetalleV = @nDetalle

		DELETE DETALLEVENTAS WHERE nVenta = @idVenta AND nDetalleV = @nDetalle

		-- DEVUELVE EXISTENCIAS
		SET @query = 'UPDATE EXISTENCIAS SET suc_' + CAST(@sucursal AS VARCHAR) + ' = ISNULL(suc_' + CAST(@sucursal AS VARCHAR) + ', 0) + @cant WHERE idProducto = @prod'
		EXEC sp_executesql @query, N'@cant INT, @prod INT', @cant = @cantidad, @prod = @producto

		-- AJUSTA LA CABECERA: EFECTIVO PRIMERO, EL RESTO TARJETA
		SET @deEfectivo = CASE WHEN @efectivo < @subtotal THEN @efectivo ELSE @subtotal END

		UPDATE VENTAS
		SET total    = ISNULL(total, 0) - @subtotal,
			efectivo = @efectivo - @deEfectivo,
			tarjeta  = CASE WHEN @tarjeta - (@subtotal - @deEfectivo) < 0 THEN 0 ELSE @tarjeta - (@subtotal - @deEfectivo) END
		WHERE nVenta = @idVenta

		COMMIT TRANSACTION ELIMINADETALLE
		SELECT @message = 'SE ELIMINO EL DETALLE CORRECTAMENTE'
		RETURN 0
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
		SELECT @message = 'HUBO UN ERROR AL ELIMINAR EL DETALLE'
		RETURN -1
	END CATCH
END
GO
