USE [farmaciahorro_ispro2]
GO

/****** Object:  Table [dbo].[PRODUCTO]    Script Date: 18/02/2025 11:09:44 a. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PRODUCTOS](
	[idProducto] [int] NOT NULL,
	[dProducto] [varchar](150) NOT NULL,
	[composicion] [varchar](150) NULL,
	[presentacion] [varchar](100) NOT NULL,
	[aterapeutica] [varchar](150) NULL,
	[indicaciones] [varchar](150) NULL,
	[contraindicaciones] [varchar](150) NULL,
	[observaciones] [varchar](250) NULL,
	[proveedor] [int] NOT NULL,
	[medida] [varchar](75) NULL,
	[categoria] [int] NOT NULL,
	[laboratorio] [varchar](100) NULL,
	[precio] [decimal](10, 2) NOT NULL,
	[costo] [decimal](10, 2) NOT NULL,
	[fechaRegistro] [date] NOT NULL,
	[estanteria] [int] NULL,
	[barcode] [varchar](25) NULL,
	[stockmin] [int] NULL,
	[estado] [bit] NULL,
 CONSTRAINT [PK_PRODUCTOS] PRIMARY KEY CLUSTERED 
(
	[idProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[PRODUCTOS]  WITH CHECK ADD  CONSTRAINT [FK_PRODUCTOS_CATEGORIA] FOREIGN KEY([categoria])
REFERENCES [dbo].[CATEGORIA] ([idCategoria])
GO

ALTER TABLE [dbo].[PRODUCTOS] CHECK CONSTRAINT [FK_PRODUCTOS_CATEGORIA]
GO

ALTER TABLE [dbo].[PRODUCTOS]  WITH CHECK ADD  CONSTRAINT [FK_PRODUCTOS_ESTANTERIA] FOREIGN KEY([estanteria])
REFERENCES [dbo].[ESTANTERIA] ([idEstanteria])
GO

ALTER TABLE [dbo].[PRODUCTOS] CHECK CONSTRAINT [FK_PRODUCTOS_ESTANTERIA]
GO

ALTER TABLE [dbo].[PRODUCTOS]  WITH CHECK ADD  CONSTRAINT [FK_PRODUCTOS_PROVEEDOR] FOREIGN KEY([proveedor])
REFERENCES [dbo].[PROVEEDOR] ([idProveedor])
GO

ALTER TABLE [dbo].[PRODUCTOS] CHECK CONSTRAINT [FK_PRODUCTOS_PROVEEDOR]
GO


