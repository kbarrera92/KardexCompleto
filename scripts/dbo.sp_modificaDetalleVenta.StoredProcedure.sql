USE [farmaciahorro_ispro2]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE dbo.sp_modificaDetalleVenta
	@idVenta	INT,
	@nDetalle	SMALLINT,
	@cantidad	INT,
	@precio		DECIMAL(18,2),
	@message	VARCHAR(200) = NULL OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	IF EXISTS (SELECT 1 FROM PAGOVALE WHERE nVenta = @idVenta)
	BEGIN
		SELECT @message = 'LA VENTA CORRESPONDE AL PAGO DE UN VALE. NO SE PUEDE MODIFICAR'
		RETURN -1
	END

	IF ISNULL(@cantidad, 0) <= 0
	BEGIN
		SELECT @message = 'LA CANTIDAD DEBE SER MAYOR A CERO. PARA QUITAR EL PRODUCTO ELIMINE EL DETALLE'
		RETURN -1
	END

	IF ISNULL(@precio, -1) < 0
	BEGIN
		SELECT @message = 'EL PRECIO NO PUEDE SER NEGATIVO'
		RETURN -1
	END

	DECLARE @sucursal INT, @efectivo DECIMAL(18,2), @tarjeta DECIMAL(18,2),
			@producto INT, @cantAnterior INT, @subtotalAnterior DECIMAL(18,2),
			@subtotal DECIMAL(18,2) = @cantidad * @precio,
			@diff DECIMAL(18,2), @reduccion DECIMAL(18,2), @deEfectivo DECIMAL(18,2),
			@difCantidad INT, @query NVARCHAR(500)

	BEGIN TRY
		BEGIN TRANSACTION MODIFICADETALLE

		SELECT @sucursal = idSucursal, @efectivo = ISNULL(efectivo, 0), @tarjeta = ISNULL(tarjeta, 0)
		FROM VENTAS WITH (UPDLOCK) WHERE nVenta = @idVenta

		IF @sucursal IS NULL
		BEGIN
			SELECT @message = 'LA VENTA NO EXISTE'
			ROLLBACK TRANSACTION MODIFICADETALLE
			RETURN -1
		END

		SELECT @producto = producto, @cantAnterior = ISNULL(cantidad, 0), @subtotalAnterior = ISNULL(subtotal, 0)
		FROM DETALLEVENTAS WHERE nVenta = @idVenta AND nDetalleV = @nDetalle

		IF @producto IS NULL
		BEGIN
			SELECT @message = 'EL DETALLE NO EXISTE'
			ROLLBACK TRANSACTION MODIFICADETALLE
			RETURN -1
		END

		UPDATE DETALLEVENTAS SET cantidad = @cantidad, precio = @precio, subtotal = @subtotal
		WHERE nVenta = @idVenta AND nDetalleV = @nDetalle

		-- AJUSTA EXISTENCIAS POR LA DIFERENCIA DE CANTIDAD
		SET @difCantidad = @cantidad - @cantAnterior
		IF @difCantidad <> 0
		BEGIN
			SET @query = 'UPDATE EXISTENCIAS SET suc_' + CAST(@sucursal AS VARCHAR) + ' = ISNULL(suc_' + CAST(@sucursal AS VARCHAR) + ', 0) - @dif WHERE idProducto = @prod'
			EXEC sp_executesql @query, N'@dif INT, @prod INT', @dif = @difCantidad, @prod = @producto
		END

		-- AJUSTA LA CABECERA: AUMENTO A EFECTIVO; REDUCCION EFECTIVO PRIMERO Y EL RESTO TARJETA
		SET @diff = @subtotal - @subtotalAnterior
		IF @diff >= 0
		BEGIN
			SET @efectivo = @efectivo + @diff
		END
		ELSE
		BEGIN
			SET @reduccion = -@diff
			SET @deEfectivo = CASE WHEN @efectivo < @reduccion THEN @efectivo ELSE @reduccion END
			SET @efectivo = @efectivo - @deEfectivo
			SET @tarjeta = CASE WHEN @tarjeta - (@reduccion - @deEfectivo) < 0 THEN 0 ELSE @tarjeta - (@reduccion - @deEfectivo) END
		END

		UPDATE VENTAS
		SET total = ISNULL(total, 0) + @diff, efectivo = @efectivo, tarjeta = @tarjeta
		WHERE nVenta = @idVenta

		COMMIT TRANSACTION MODIFICADETALLE
		SELECT @message = 'SE MODIFICO EL DETALLE CORRECTAMENTE'
		RETURN 0
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
		SELECT @message = 'HUBO UN ERROR AL MODIFICAR EL DETALLE'
		RETURN -1
	END CATCH
END
GO
