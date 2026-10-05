USE [farmaciahorro_ispro2]
GO
-- Parametros de sucursal/empresa configurables desde el sistema (antes vivian en App.config).
-- idSucursal NULL = valor global (empresa); con idSucursal = valor propio de la sucursal (sobrescribe el global).
-- Ejecutar ANTES de desplegar la version nueva del sistema (frmConfiguracion).

IF OBJECT_ID('dbo.PARAMETRO', 'U') IS NULL
BEGIN
	CREATE TABLE dbo.PARAMETRO(
		idParametro		int IDENTITY(1,1) NOT NULL CONSTRAINT PK_PARAMETRO PRIMARY KEY,
		clave			varchar(50) NOT NULL,
		idSucursal		int NULL CONSTRAINT FK_PARAMETRO_SUCURSAL REFERENCES dbo.SUCURSAL(idSucursal),
		valor			nvarchar(500) NOT NULL,
		fechaModifica	datetime NOT NULL CONSTRAINT DF_PARAMETRO_fecha DEFAULT (GETDATE()),
		usuarioModifica	int NULL
	)
END
GO
-- NULL no cuenta como duplicado en un indice unico normal; se separan en dos indices filtrados.
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'UX_PARAMETRO_clave_global' AND object_id = OBJECT_ID('dbo.PARAMETRO'))
	CREATE UNIQUE INDEX UX_PARAMETRO_clave_global ON dbo.PARAMETRO(clave) WHERE idSucursal IS NULL
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'UX_PARAMETRO_clave_sucursal' AND object_id = OBJECT_ID('dbo.PARAMETRO'))
	CREATE UNIQUE INDEX UX_PARAMETRO_clave_sucursal ON dbo.PARAMETRO(clave, idSucursal) WHERE idSucursal IS NOT NULL
GO

-- Devuelve un valor por clave para la sucursal: el de la sucursal si existe, si no el global.
CREATE OR ALTER PROCEDURE dbo.sp_consultaParametros
	@idSucursal	INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT clave, valor
	FROM (
		SELECT clave, valor,
			ROW_NUMBER() OVER (PARTITION BY clave ORDER BY CASE WHEN idSucursal IS NULL THEN 1 ELSE 0 END) AS rn
		FROM dbo.PARAMETRO
		WHERE idSucursal IS NULL OR idSucursal = @idSucursal
	) P
	WHERE rn = 1
END
GO

-- Inserta o actualiza un parametro. @idSucursal NULL = global.
CREATE OR ALTER PROCEDURE dbo.sp_grabaParametro
	@clave		VARCHAR(50),
	@idSucursal	INT = NULL,
	@valor		NVARCHAR(500),
	@usuario	INT = NULL
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE dbo.PARAMETRO
	SET valor = @valor, fechaModifica = GETDATE(), usuarioModifica = @usuario
	WHERE clave = @clave
	  AND ((idSucursal IS NULL AND @idSucursal IS NULL) OR idSucursal = @idSucursal)

	IF @@ROWCOUNT = 0
		INSERT INTO dbo.PARAMETRO (clave, idSucursal, valor, usuarioModifica)
		VALUES (@clave, @idSucursal, @valor, @usuario)
END
GO

-- Seed: valores globales y por sucursal actuales (solo si la clave aun no existe).
-- Ajustar los valores por sucursal desde Administracion > Configuracion o con sp_grabaParametro.
INSERT INTO dbo.PARAMETRO (clave, idSucursal, valor)
SELECT v.clave, NULL, v.valor
FROM (VALUES
	('nombreEmpresa', N'Comercial La Bendición'),
	('eslogan', N'Comercial La Bendición'),
	('cliente', N'FARMAVELA')
) v(clave, valor)
WHERE NOT EXISTS (SELECT 1 FROM dbo.PARAMETRO p WHERE p.clave = v.clave AND p.idSucursal IS NULL)
GO

INSERT INTO dbo.PARAMETRO (clave, idSucursal, valor)
SELECT v.clave, S.idSucursal, v.valor
FROM dbo.SUCURSAL S
CROSS JOIN (VALUES
	('imprimeTicket', N'N'),
	('imprimeTicketCuadre', N'N'),
	('cantTickets', N'2'),
	('horasDiferencia', N'0'),
	('logoPath', N'C:\Images\logo.jpeg')
) v(clave, valor)
WHERE NOT EXISTS (SELECT 1 FROM dbo.PARAMETRO p WHERE p.clave = v.clave AND p.idSucursal = S.idSucursal)
GO

-- sucursalFisica: por defecto el nombre registrado de la sucursal.
INSERT INTO dbo.PARAMETRO (clave, idSucursal, valor)
SELECT 'sucursalFisica', S.idSucursal, S.nombreSuc
FROM dbo.SUCURSAL S
WHERE NOT EXISTS (SELECT 1 FROM dbo.PARAMETRO p WHERE p.clave = 'sucursalFisica' AND p.idSucursal = S.idSucursal)
GO
