-- =============================================
-- Vales de mercadería para vendedores
-- Tablas y tipo de tabla. Es idempotente: se puede ejecutar más de una vez.
-- Estado de VALE: 'P' = pendiente, 'C' = pagado.
-- El estado VENCIDO no se guarda: se calcula (estado = 'P' y fechaVencimiento < hoy).
-- =============================================
IF OBJECT_ID('dbo.VALE') IS NULL
	CREATE TABLE dbo.VALE (
		idVale				INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_VALE PRIMARY KEY,
		fecha				DATETIME NOT NULL CONSTRAINT DF_VALE_fecha DEFAULT (GETDATE()),
		fechaVencimiento	DATETIME NOT NULL,
		sucursal			INT NOT NULL CONSTRAINT FK_VALE_SUCURSAL REFERENCES dbo.SUCURSAL (idSucursal),
		vendedor			INT NOT NULL CONSTRAINT FK_VALE_VENDEDORES REFERENCES dbo.VENDEDOR (idVendedor),
		usuario				INT NOT NULL,
		total				DECIMAL(10,2) NOT NULL,
		saldo				DECIMAL(10,2) NOT NULL,
		estado				CHAR(1) NOT NULL CONSTRAINT DF_VALE_estado DEFAULT ('P')
							CONSTRAINT CK_VALE_estado CHECK (estado IN ('P', 'C'))
	)
GO

IF OBJECT_ID('dbo.PAGOVALE') IS NULL
	CREATE TABLE dbo.PAGOVALE (
		idPago		INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_PAGOVALE PRIMARY KEY,
		idVale		INT NOT NULL CONSTRAINT FK_PAGOVALE_VALE REFERENCES dbo.VALE (idVale),
		fecha		DATETIME NOT NULL CONSTRAINT DF_PAGOVALE_fecha DEFAULT (GETDATE()),
		monto		DECIMAL(10,2) NOT NULL,
		usuario		INT NOT NULL,
		idTurno		INT NULL		-- turno de caja abierto al momento del pago (si lo hay)
	)
GO

IF OBJECT_ID('dbo.DETALLEVALE') IS NULL
	CREATE TABLE dbo.DETALLEVALE (
		idVale		INT NOT NULL CONSTRAINT FK_DETALLEVALE_VALE REFERENCES dbo.VALE (idVale),
		nDetalle	SMALLINT NOT NULL,
		producto	INT NOT NULL,
		cantidad	INT NOT NULL,
		precio		DECIMAL(10,2) NOT NULL,
		subtotal	DECIMAL(10,2) NOT NULL,
		pagado		BIT NOT NULL CONSTRAINT DF_DETALLEVALE_pagado DEFAULT (0),
		idPago		INT NULL CONSTRAINT FK_DETALLEVALE_PAGOVALE REFERENCES dbo.PAGOVALE (idPago),
		CONSTRAINT PK_DETALLEVALE PRIMARY KEY (idVale, nDetalle)
	)
GO

-- Líneas del vale que se pagan en un abono
IF TYPE_ID('dbo.LINEASVALE') IS NULL
	CREATE TYPE dbo.LINEASVALE AS TABLE (
		nDetalle SMALLINT NOT NULL PRIMARY KEY
	)
GO
