USE [farmaciahorro_ispro2]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Devuelve el turno (caja) abierto de una sucursal y el vendedor que lo abrió.
--				Permite recuperar el "vendedor actual" tras cerrar sesión o reiniciar el programa.
--				Si no hay turno abierto, @idTurno = 0 y @codempleado = 0.
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[sp_consultaTurnoAbierto]
	@sucursal INT,
	@idTurno INT = 0 OUTPUT,
	@codempleado INT = 0 OUTPUT,
	@nombreEmpleado VARCHAR(150) = NULL OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT @idTurno = 0, @codempleado = 0, @nombreEmpleado = NULL

	SELECT TOP 1
		@idTurno = A.idTurno,
		@codempleado = ISNULL(A.codempleado, 0),
		@nombreEmpleado = V.nombre
	FROM APERTURACAJAS A
	LEFT JOIN VENDEDOR V ON V.idVendedor = A.codempleado
	WHERE A.estado = 'A' AND A.sucursal = @sucursal
	ORDER BY A.fechaApertura DESC
END
GO
