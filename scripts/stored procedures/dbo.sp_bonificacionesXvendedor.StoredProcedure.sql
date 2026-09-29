USE db_ab10ba_kbarreradev
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Kevin Barrera
-- Create date: 29-09-2026
-- Description:	Muestra las ventas de productos con bonificación (incentivo)
--				agrupadas por vendedor, en un rango de fechas. Un mismo
--				vendedor puede vender en varias sucursales, por lo que la
--				sucursal no limita el reporte: solo se muestra como dato
--				informativo por fila (se puede filtrar opcionalmente).
-- =============================================
CREATE PROCEDURE [dbo].[sp_bonificacionesXvendedor]
	@fechainicial date,
	@fechafinal date,
	@vendedor int = NULL,		-- NULL = todos los vendedores
	@sucursal int = NULL,		-- NULL = todas las sucursales
	@bandera char(2) = 'I'		-- 'I' = Incentivo
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		v.codempleado									AS codVendedor,
		ISNULL(ve.nombre, 'SIN VENDEDOR')				AS nombreVendedor,
		v.idSucursal									AS idSucursal,
		suc.nombreSuc									AS nombreSucursal,
		p.idProducto,
		p.dProducto,
		SUM(dv.cantidad)								AS cantidad,
		dv.precio										AS precio,
		SUM(dv.subtotal)								AS totalVendido,
		p.bonificacion									AS bonificacionUnitaria,
		(SUM(dv.cantidad) * p.bonificacion)			AS bonificacionTotal
	FROM VENTAS v
	INNER JOIN DETALLEVENTAS dv ON v.nVenta = dv.nVenta
	INNER JOIN PRODUCTOS p ON dv.producto = p.idProducto
	INNER JOIN SUCURSAL suc ON v.idSucursal = suc.idSucursal
	LEFT JOIN VENDEDORES ve ON v.codempleado = ve.idVendedor
	WHERE (CAST(v.fechaVenta AS date) BETWEEN @fechainicial AND @fechafinal)
		AND p.bandera = @bandera
		AND ISNULL(p.bonificacion, 0) > 0
		AND (@vendedor IS NULL OR v.codempleado = @vendedor)
		AND (@sucursal IS NULL OR v.idSucursal = @sucursal)
	GROUP BY v.codempleado, ve.nombre, v.idSucursal, suc.nombreSuc, p.idProducto, p.dProducto, dv.precio, p.bonificacion
	ORDER BY nombreVendedor ASC, nombreSucursal ASC, p.dProducto ASC
END
GO
