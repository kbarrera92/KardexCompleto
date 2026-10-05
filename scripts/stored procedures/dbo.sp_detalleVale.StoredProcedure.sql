-- =============================================
-- Description:	Lineas de un vale, con el nombre del producto y si ya fueron pagadas.
-- =============================================
IF OBJECT_ID('dbo.sp_detalleVale') IS NOT NULL
	DROP PROCEDURE dbo.sp_detalleVale
GO
CREATE PROCEDURE [dbo].[sp_detalleVale]
	@idVale INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		D.nDetalle,
		D.producto,
		P.dProducto,
		D.cantidad,
		D.precio,
		D.subtotal,
		D.pagado
	FROM DETALLEVALE D
	INNER JOIN PRODUCTOS P ON D.producto = P.idProducto
	WHERE D.idVale = @idVale
	ORDER BY D.nDetalle
END
GO
