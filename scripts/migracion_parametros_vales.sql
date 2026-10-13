USE [farmaciahorro_ispro2]
GO
-- Parametros globales de vales (editables en Administracion > Configuracion):
--   valeMaximo     : monto maximo (saldo pendiente) que puede tener un vale.
--   valePagoMinimo : si el saldo del vale supera este monto, cada pago debe ser al menos este monto;
--                    si el saldo es igual o menor, el vale se debe pagar completo.
-- Si no se ejecuta, el sistema usa 300.00 y 150.00 por defecto.
INSERT INTO dbo.PARAMETRO (clave, idSucursal, valor)
SELECT v.clave, NULL, v.valor
FROM (VALUES
	('valeMaximo', N'300'),
	('valePagoMinimo', N'150')
) v(clave, valor)
WHERE NOT EXISTS (SELECT 1 FROM dbo.PARAMETRO p WHERE p.clave = v.clave AND p.idSucursal IS NULL)
GO
