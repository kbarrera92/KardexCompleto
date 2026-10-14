USE [farmaciahorro_ispro2]
GO
-- verMisVentas (por sucursal): S = muestra el botón "Mis ventas" en el punto de venta; N = lo oculta.
-- Si no existe, el sistema lo muestra. Editable en Administracion > Configuracion.
INSERT INTO dbo.PARAMETRO (clave, idSucursal, valor)
SELECT 'verMisVentas', S.idSucursal, N'S'
FROM dbo.SUCURSAL S
WHERE NOT EXISTS (SELECT 1 FROM dbo.PARAMETRO p WHERE p.clave = 'verMisVentas' AND p.idSucursal = S.idSucursal)
GO
