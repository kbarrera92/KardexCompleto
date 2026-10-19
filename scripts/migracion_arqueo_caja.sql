USE [farmaciahorro_ispro2]
GO
-- Arqueo de caja: conteo de monedas/billetes por denominacion de un turno cerrado, junto al resumen del cierre.
--     Total efectivo   = sobrante + efectivo inicial + total venta - venta POS - helados - bebidas - egresos
--     Efectivo en caja = total efectivo + helados + bebidas       (lo que se cuenta: incluye el fondo y el efectivo de helados/bebidas)
--     Diferencia       = total contado - efectivo en caja
--     (sobrante = APERTURACAJAS.diferencia del cierre; efectivo inicial = APERTURACAJAS.saldoInicial, el fondo para cambio;
--      helados/bebidas = ventas del turno de las categorias configuradas en PARAMETRO:
--      categoriasHelados / categoriasBebidas, IDs de CATEGORIA separados por coma)
-- Ejecutar ANTES de desplegar la version nueva del sistema (FormArqueoCaja).

IF OBJECT_ID('dbo.ARQUEOCAJA', 'U') IS NULL
BEGIN
	CREATE TABLE dbo.ARQUEOCAJA(
		idTurno			int NOT NULL CONSTRAINT PK_ARQUEOCAJA PRIMARY KEY
						CONSTRAINT FK_ARQUEOCAJA_APERTURACAJAS REFERENCES dbo.APERTURACAJAS(idTurno),
		fechaArqueo		datetime NOT NULL CONSTRAINT DF_ARQUEOCAJA_fecha DEFAULT (GETDATE()),
		usuario			int NULL,
		sobrante		decimal(10,2) NOT NULL,
		saldoInicial	decimal(10,2) NOT NULL,
		totalVenta		decimal(10,2) NOT NULL,
		totalTarjeta	decimal(10,2) NOT NULL,
		totalHelados	decimal(10,2) NOT NULL,
		totalBebidas	decimal(10,2) NOT NULL,
		totalEgresos	decimal(10,2) NOT NULL,
		totalEfectivo	decimal(10,2) NOT NULL,
		totalArqueo		decimal(10,2) NOT NULL,
		diferencia		decimal(10,2) NOT NULL
	)
END
GO
-- BD donde ARQUEOCAJA ya se creo sin el efectivo inicial (los arqueos previos quedan con 0 y se recalculan al guardarlos de nuevo)
IF COL_LENGTH('dbo.ARQUEOCAJA', 'saldoInicial') IS NULL
	ALTER TABLE dbo.ARQUEOCAJA ADD saldoInicial decimal(10,2) NOT NULL CONSTRAINT DF_ARQUEOCAJA_saldoInicial DEFAULT (0)
GO
IF OBJECT_ID('dbo.ARQUEOCAJADETALLE', 'U') IS NULL
BEGIN
	CREATE TABLE dbo.ARQUEOCAJADETALLE(
		idTurno			int NOT NULL CONSTRAINT FK_ARQUEOCAJADETALLE_ARQUEOCAJA REFERENCES dbo.ARQUEOCAJA(idTurno),
		denominacion	decimal(10,2) NOT NULL,
		cantidad		int NOT NULL,
		subtotal		decimal(10,2) NOT NULL,
		CONSTRAINT PK_ARQUEOCAJADETALLE PRIMARY KEY (idTurno, denominacion)
	)
END
GO

-- Claves de categorias de helados y bebidas (globales; vacias hasta configurarlas en Administracion > Configuracion).
INSERT INTO dbo.PARAMETRO (clave, idSucursal, valor)
SELECT v.clave, NULL, v.valor
FROM (VALUES
	('categoriasHelados', N''),
	('categoriasBebidas', N'')
) v(clave, valor)
WHERE NOT EXISTS (SELECT 1 FROM dbo.PARAMETRO p WHERE p.clave = v.clave AND p.idSucursal IS NULL)
GO

-- Resumen del cierre de un turno para el arqueo. Una fila; las listas de categorias no dependen de STRING_SPLIT.
CREATE OR ALTER FUNCTION dbo.fn_arqueoResumen (@idTurno INT, @sucursal INT)
RETURNS TABLE
AS
RETURN
(
	SELECT
		A.idTurno,
		ISNULL(A.diferencia, 0)		AS sobrante,
		ISNULL(A.saldoInicial, 0)	AS saldoInicial,
		ISNULL(A.totalSistema, 0)	AS totalVenta,
		ISNULL(A.totalTarjeta, 0)	AS totalTarjeta,
		ISNULL(A.totalEgresos, 0)	AS totalEgresos,
		ISNULL((SELECT SUM(DV.subtotal)
				FROM DETALLEVENTAS DV
				INNER JOIN VENTAS V ON V.nVenta = DV.nVenta
				INNER JOIN PRODUCTOS P ON P.idProducto = DV.producto
				WHERE V.turno = A.idTurno AND V.idSucursal = A.sucursal
				  AND NULLIF(PH.valor, '') IS NOT NULL
				  AND ',' + REPLACE(PH.valor, ' ', '') + ',' LIKE '%,' + CAST(P.categoria AS varchar(10)) + ',%'), 0) AS totalHelados,
		ISNULL((SELECT SUM(DV.subtotal)
				FROM DETALLEVENTAS DV
				INNER JOIN VENTAS V ON V.nVenta = DV.nVenta
				INNER JOIN PRODUCTOS P ON P.idProducto = DV.producto
				WHERE V.turno = A.idTurno AND V.idSucursal = A.sucursal
				  AND NULLIF(PB.valor, '') IS NOT NULL
				  AND ',' + REPLACE(PB.valor, ' ', '') + ',' LIKE '%,' + CAST(P.categoria AS varchar(10)) + ',%'), 0) AS totalBebidas
	FROM APERTURACAJAS A
	OUTER APPLY (SELECT TOP 1 valor FROM dbo.PARAMETRO
				 WHERE clave = 'categoriasHelados' AND (idSucursal IS NULL OR idSucursal = @sucursal)
				 ORDER BY CASE WHEN idSucursal IS NULL THEN 1 ELSE 0 END) PH
	OUTER APPLY (SELECT TOP 1 valor FROM dbo.PARAMETRO
				 WHERE clave = 'categoriasBebidas' AND (idSucursal IS NULL OR idSucursal = @sucursal)
				 ORDER BY CASE WHEN idSucursal IS NULL THEN 1 ELSE 0 END) PB
	WHERE A.idTurno = @idTurno AND A.estado = 'C'
)
GO

-- =============================================
-- Description:	Arqueo de caja de un turno cerrado.
--	'U' : resumen del turno (@idTurno, o el ultimo cerrado de @sucursal si viene en 0). Devuelve dos result sets:
--		  1) cabecera y resumen, 2) detalle del arqueo guardado (vacio si aun no se ha hecho).
--	'G' : graba el arqueo del turno. @detalle = "denominacion:cantidad;..." (ej. "0.25:10;1:5;200:3").
--		  Los montos del resumen se recalculan en el servidor. Un arqueo previo del mismo turno se reemplaza.
-- =============================================
CREATE OR ALTER PROCEDURE dbo.sp_mantArqueoCaja
	@opt		CHAR(1),
	@msg		VARCHAR(200) OUTPUT,
	@rc			INT OUTPUT,
	@sucursal	INT = 0,
	@idTurno	INT = 0,
	@usuario	INT = 0,
	@detalle	VARCHAR(2000) = NULL
AS
BEGIN
	SET NOCOUNT ON;

	SELECT @rc = 0, @msg = ''

	IF @opt IN ('U', 'G') AND ISNULL(@idTurno, 0) = 0
		SELECT TOP 1 @idTurno = idTurno
		FROM APERTURACAJAS
		WHERE estado = 'C' AND sucursal = @sucursal
		ORDER BY fechaCierre DESC

	IF @opt IN ('U', 'G') AND NOT EXISTS (SELECT 1 FROM APERTURACAJAS WHERE idTurno = @idTurno AND estado = 'C' AND sucursal = @sucursal)
	BEGIN
		SELECT @rc = -1, @msg = 'NO SE ENCONTRO UN TURNO CERRADO PARA EL ARQUEO'
		RETURN @rc
	END

	IF @opt = 'U'
	BEGIN
		SELECT
			A.idTurno, A.fechaApertura, A.fechaCierre,
			ISNULL(V.nombre, '') AS vendedor,
			R.sobrante, R.saldoInicial, R.totalVenta, R.totalTarjeta, R.totalHelados, R.totalBebidas, R.totalEgresos
		FROM APERTURACAJAS A
		CROSS APPLY dbo.fn_arqueoResumen(A.idTurno, A.sucursal) R
		LEFT JOIN VENDEDOR V ON V.idVendedor = A.codempleado
		WHERE A.idTurno = @idTurno

		SELECT denominacion, cantidad, subtotal
		FROM ARQUEOCAJADETALLE
		WHERE idTurno = @idTurno
		ORDER BY denominacion

		RETURN 0
	END

	IF @opt = 'G'
	BEGIN
		DECLARE @items TABLE (denominacion decimal(10,2) PRIMARY KEY, cantidad int NOT NULL)
		DECLARE @resto VARCHAR(2000) = ISNULL(@detalle, ''), @item VARCHAR(100), @pos INT, @sep INT
		DECLARE @den decimal(10,2), @cant int

		WHILE LEN(@resto) > 0
		BEGIN
			SET @pos = CHARINDEX(';', @resto)
			IF @pos = 0
				SELECT @item = @resto, @resto = ''
			ELSE
				SELECT @item = LEFT(@resto, @pos - 1), @resto = SUBSTRING(@resto, @pos + 1, 2000)

			SET @sep = CHARINDEX(':', @item)
			SET @den = TRY_CAST(LEFT(@item, NULLIF(@sep, 0) - 1) AS decimal(10,2))
			SET @cant = TRY_CAST(SUBSTRING(@item, @sep + 1, 20) AS int)

			IF @sep = 0 OR @den IS NULL OR @den <= 0 OR @cant IS NULL OR @cant < 0
			BEGIN
				SELECT @rc = -1, @msg = 'EL DETALLE DEL ARQUEO NO ES VALIDO'
				RETURN @rc
			END

			IF EXISTS (SELECT 1 FROM @items WHERE denominacion = @den)
			BEGIN
				SELECT @rc = -1, @msg = 'EL DETALLE DEL ARQUEO TIENE DENOMINACIONES REPETIDAS'
				RETURN @rc
			END

			INSERT INTO @items (denominacion, cantidad) VALUES (@den, @cant)
		END

		BEGIN TRANSACTION ARQUEO
		BEGIN TRY
			DELETE FROM ARQUEOCAJADETALLE WHERE idTurno = @idTurno
			DELETE FROM ARQUEOCAJA WHERE idTurno = @idTurno

			-- El conteo incluye el fondo inicial y el efectivo de helados/bebidas: se compara contra el efectivo en caja
			INSERT INTO ARQUEOCAJA (idTurno, fechaArqueo, usuario, sobrante, saldoInicial, totalVenta, totalTarjeta, totalHelados, totalBebidas,
									totalEgresos, totalEfectivo, totalArqueo, diferencia)
			SELECT @idTurno, GETDATE(), @usuario, R.sobrante, R.saldoInicial, R.totalVenta, R.totalTarjeta, R.totalHelados, R.totalBebidas,
				   R.totalEgresos,
				   E.totalEfectivo,
				   C.totalArqueo,
				   C.totalArqueo - (E.totalEfectivo + R.totalHelados + R.totalBebidas)
			FROM dbo.fn_arqueoResumen(@idTurno, @sucursal) R
			CROSS APPLY (SELECT R.sobrante + R.saldoInicial + R.totalVenta - R.totalTarjeta - R.totalHelados - R.totalBebidas - R.totalEgresos AS totalEfectivo) E
			CROSS APPLY (SELECT ISNULL((SELECT SUM(denominacion * cantidad) FROM @items), 0) AS totalArqueo) C

			INSERT INTO ARQUEOCAJADETALLE (idTurno, denominacion, cantidad, subtotal)
			SELECT @idTurno, denominacion, cantidad, denominacion * cantidad FROM @items WHERE cantidad > 0

			COMMIT TRANSACTION ARQUEO
		END TRY
		BEGIN CATCH
			IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION ARQUEO
			SELECT @rc = -2, @msg = 'HUBO UN ERROR AL GRABAR EL ARQUEO: ' + LEFT(ERROR_MESSAGE(), 140)
			RETURN @rc
		END CATCH

		SELECT @msg = 'EL ARQUEO SE GUARDO CORRECTAMENTE'
		RETURN 0
	END

	SELECT @rc = -1, @msg = 'OPCION NO VALIDA'
	RETURN @rc
END
GO
