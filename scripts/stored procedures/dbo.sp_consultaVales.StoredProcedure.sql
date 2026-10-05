-- =============================================
-- Description:	Lista vales con su estado calculado (PENDIENTE / VENCIDO / PAGADO).
--				@sucursal y @vendedor: NULL o 0 = todos.
--				@estado: NULL o '' = todos, o PENDIENTE / VENCIDO / PAGADO.
-- =============================================
IF OBJECT_ID('dbo.sp_consultaVales') IS NOT NULL
	DROP PROCEDURE dbo.sp_consultaVales
GO
CREATE PROCEDURE [dbo].[sp_consultaVales]
	@sucursal	INT = NULL,
	@vendedor	INT = NULL,
	@estado		VARCHAR(10) = NULL
AS
BEGIN
	SET NOCOUNT ON;

	SELECT * FROM (
		SELECT
			V.idVale,
			V.fecha,
			V.fechaVencimiento,
			S.nombreSuc,
			VE.nombre AS nombreVendedor,
			V.total,
			V.saldo,
			CASE
				WHEN V.estado = 'C' THEN 'PAGADO'
				WHEN GETDATE() > V.fechaVencimiento THEN 'VENCIDO'
				ELSE 'PENDIENTE'
			END AS estadoVale
		FROM VALE V
		INNER JOIN VENDEDOR VE ON V.vendedor = VE.idVendedor
		INNER JOIN SUCURSAL S ON V.sucursal = S.idSucursal
		WHERE (ISNULL(@sucursal, 0) = 0 OR V.sucursal = @sucursal)
		  AND (ISNULL(@vendedor, 0) = 0 OR V.vendedor = @vendedor)
	) X
	WHERE ISNULL(@estado, '') = '' OR X.estadoVale = @estado
	ORDER BY X.idVale DESC
END
GO
