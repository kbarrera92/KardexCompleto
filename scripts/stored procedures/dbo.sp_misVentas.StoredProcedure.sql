-- =============================================
-- Description:	Productos vendidos hoy por un vendedor en una sucursal.
--				Solo devuelve datos no sensibles (sin costo, cliente ni forma de pago).
--				VENTAS.codempleado = VENDEDOR.idVendedor.
--				"Hoy" respeta @horasDiferencia, igual que grabaVenta1.
-- =============================================
IF OBJECT_ID('dbo.sp_misVentas') IS NOT NULL
	DROP PROCEDURE dbo.sp_misVentas
GO
CREATE PROCEDURE [dbo].[sp_misVentas]
	@sucursal			INT,
	@vendedor			INT,
	@horasDiferencia	INT = 0
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @hoy DATE = CAST(DATEADD(HOUR, @horasDiferencia, GETDATE()) AS DATE)

	SELECT
		V.nVenta,
		V.fechaVenta,
		P.dProducto,
		DV.cantidad,
		DV.precio,
		DV.subtotal
	FROM VENTAS V
	INNER JOIN DETALLEVENTAS DV ON DV.nVenta = V.nVenta
	INNER JOIN PRODUCTOS P ON DV.producto = P.idProducto
	WHERE V.idSucursal = @sucursal
	  AND V.codempleado = @vendedor
	  AND CAST(V.fechaVenta AS DATE) = @hoy
	ORDER BY V.nVenta DESC, DV.nDetalleV
END
GO
