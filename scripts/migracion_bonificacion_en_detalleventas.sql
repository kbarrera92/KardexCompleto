USE [farmaciahorro_ispro2]
GO
-- Congela la bonificacion y el costo al momento de la venta (DETALLEVENTAS.bonificacion, DETALLEVENTAS.costo).
-- Ejecutar ANTES de desplegar las versiones nuevas de grabaVenta1, sp_eliminaVenta y sp_bonificacionesXvendedor, sp_muestraUtilidad y sp_cortecaja.

IF COL_LENGTH('dbo.DETALLEVENTAS', 'bonificacion') IS NULL
	ALTER TABLE dbo.DETALLEVENTAS ADD bonificacion decimal(10,2) NULL
GO
IF COL_LENGTH('dbo.DETALLEVENTAS', 'costo') IS NULL
	ALTER TABLE dbo.DETALLEVENTAS ADD costo decimal(10,2) NULL
GO
IF COL_LENGTH('dbo.DETALLEVENTASELIMINADOS', 'bonificacion') IS NULL
	ALTER TABLE dbo.DETALLEVENTASELIMINADOS ADD bonificacion decimal(10,2) NULL
GO
IF COL_LENGTH('dbo.DETALLEVENTASELIMINADOS', 'costo') IS NULL
	ALTER TABLE dbo.DETALLEVENTASELIMINADOS ADD costo decimal(10,2) NULL
GO
-- Relleno de ventas existentes: no se conoce el valor historico real, se usa el valor ACTUAL del catalogo.
-- Ejecutar UNA sola vez, antes de modificar bonificaciones en PRODUCTOS.
UPDATE dv
SET dv.bonificacion = p.bonificacion
FROM dbo.DETALLEVENTAS dv
INNER JOIN dbo.PRODUCTOS p ON dv.producto = p.idProducto
WHERE p.bandera = 'I' AND dv.bonificacion IS NULL
GO
UPDATE dv
SET dv.bonificacion = p.bonificacion
FROM dbo.DETALLEVENTASELIMINADOS dv
INNER JOIN dbo.PRODUCTOS p ON dv.producto = p.idProducto
WHERE p.bandera = 'I' AND dv.bonificacion IS NULL
GO
-- Costo: mismo criterio (valor ACTUAL del catalogo para ventas existentes).
UPDATE dv
SET dv.costo = p.costo
FROM dbo.DETALLEVENTAS dv
INNER JOIN dbo.PRODUCTOS p ON dv.producto = p.idProducto
WHERE dv.costo IS NULL
GO
UPDATE dv
SET dv.costo = p.costo
FROM dbo.DETALLEVENTASELIMINADOS dv
INNER JOIN dbo.PRODUCTOS p ON dv.producto = p.idProducto
WHERE dv.costo IS NULL
GO
