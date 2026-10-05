-- =============================================
-- Description:	Productos con existencia y precio de la sucursal para armar un vale.
--				Mismas columnas que sp_infoProductos; precio = precio de la sucursal
--				(PRECIOS_SUCURSAL) o el precio general del producto si no tiene.
-- =============================================
IF OBJECT_ID('dbo.sp_productosVale') IS NOT NULL
	DROP PROCEDURE dbo.sp_productosVale
GO
CREATE PROCEDURE [dbo].[sp_productosVale]
	@suc INT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @QUERY NVARCHAR(2000)

	SET @QUERY =
	N'SELECT P.idProducto, P.dProducto,
	ISNULL(E.suc_' + CAST(@suc AS NVARCHAR(10)) + N', 0) AS Existencia,
	ISNULL(P.laboratorio, '''') AS laboratorio, ISNULL(P.presentacion, '''') AS presentacion,
	ISNULL(PS.precio, P.precio) AS precio, C.categoria, ISNULL(P.medida, '''') AS medida,
	PROV.rzProveedor, ISNULL(P.indicaciones, '''') AS indicaciones, ISNULL(P.estanteria, '''') AS estanteria,
	ISNULL(P.barcode, '''') AS barcode
	FROM PRODUCTOS P
	INNER JOIN CATEGORIA C ON P.categoria = C.idCategoria
	INNER JOIN PROVEEDOR PROV ON P.proveedor = PROV.idProveedor
	INNER JOIN EXISTENCIAS E ON P.idProducto = E.idProducto
	LEFT JOIN PRECIOS_SUCURSAL PS ON PS.idProducto = P.idProducto AND PS.idSucursal = @s'

	EXEC sp_executesql @QUERY, N'@s INT', @s = @suc
END
GO
