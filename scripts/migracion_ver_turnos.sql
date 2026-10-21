USE [farmaciahorro_ispro2]
GO
-- Listado de turnos de caja para FormVerTurnos (solo lectura).
-- Antes el listado usaba sp_mantAperturasCajas opcion 'B', que solo devolvia turnos cerrados (estado = 'C'),
-- por lo que los turnos abiertos nunca aparecian. Este SP devuelve abiertos y cerrados.
--   Cerrados: montos guardados en el cierre.
--   Abiertos: montos parciales calculados al momento con la misma formula del cierre (sp_mantAperturasCajas opcion 'C');
--             totalFisico y diferencia quedan en NULL porque aun no hay conteo.
--   Los turnos abiertos se devuelven siempre, sin importar el rango de fechas.

-- =============================================
-- Author:		Kevin Barrera
-- Description:	Lista turnos de caja (abiertos y cerrados) con filtros por sucursal, estado y rango de fechas.
-- =============================================
CREATE OR ALTER PROCEDURE dbo.sp_listaTurnosCaja
	@sucursal	INT = 0,		-- 0 = todas
	@estado		CHAR(1) = NULL,	-- 'A' abiertos, 'C' cerrados, NULL = todos
	@desde		DATE = NULL,
	@hasta		DATE = NULL
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		T.idTurno,
		T.estado,
		T.estadoTexto,
		T.nombreSuc,
		T.vendedor,
		T.usuario,
		T.fechaApertura,
		T.fechaCierre,
		T.saldoInicial,
		T.totalVentas,
		T.totalTarjeta,
		T.totalEgresos,
		T.saldoInicial + T.totalVentas - T.totalTarjeta - T.totalEgresos AS efectivoEsperado,
		T.totalFisico,
		T.diferencia
	FROM (
		SELECT
			A.idTurno,
			A.estado,
			CASE A.estado WHEN 'A' THEN 'ABIERTO' ELSE 'CERRADO' END AS estadoTexto,
			S.nombreSuc,
			ISNULL(V.nombre, '') AS vendedor,
			ISNULL(U.nick, '') AS usuario,
			A.fechaApertura,
			A.fechaCierre,
			ISNULL(A.saldoInicial, 0) AS saldoInicial,
			CASE WHEN A.estado = 'A' THEN ISNULL(VT.totalVentas, 0)  ELSE ISNULL(A.totalSistema, 0) END AS totalVentas,
			CASE WHEN A.estado = 'A' THEN ISNULL(VT.totalTarjeta, 0) ELSE ISNULL(A.totalTarjeta, 0) END AS totalTarjeta,
			CASE WHEN A.estado = 'A' THEN ISNULL(EG.totalEgresos, 0) ELSE ISNULL(A.totalEgresos, 0) END AS totalEgresos,
			A.totalFisico,
			A.diferencia
		FROM APERTURACAJAS A
		LEFT JOIN USUARIO  U ON A.idUsuario = U.idUsuario
		LEFT JOIN SUCURSAL S ON A.sucursal = S.idSucursal
		LEFT JOIN VENDEDOR V ON A.codempleado = V.idVendedor
		OUTER APPLY (
			SELECT SUM(VE.total) AS totalVentas, SUM(ISNULL(VE.tarjeta, 0)) AS totalTarjeta
			FROM VENTAS VE
			WHERE A.estado = 'A' AND VE.turno = A.idTurno AND VE.idSucursal = A.sucursal
		) VT
		OUTER APPLY (
			SELECT SUM(E.total) AS totalEgresos
			FROM EGRESOS E
			WHERE A.estado = 'A' AND E.idTurno = A.idTurno AND E.estado = 1
		) EG
		WHERE (@sucursal = 0 OR A.sucursal = @sucursal)
		  AND (@estado IS NULL OR A.estado = @estado)
		  AND (A.estado = 'A'
			   OR ((@desde IS NULL OR CAST(A.fechaApertura AS DATE) >= @desde)
			   AND (@hasta IS NULL OR CAST(A.fechaApertura AS DATE) <= @hasta)))
	) T
	ORDER BY CASE WHEN T.estado = 'A' THEN 0 ELSE 1 END, ISNULL(T.fechaCierre, T.fechaApertura) DESC, T.idTurno DESC
END
GO
