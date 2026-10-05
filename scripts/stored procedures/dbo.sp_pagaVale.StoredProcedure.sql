-- =============================================
-- Description:	Registra el pago (abono o total) de lineas completas de un vale.
--				No genera venta ni toca existencias.
-- Return:		0 = ok, -1 = error (detalle en @message)
-- =============================================
IF OBJECT_ID('dbo.sp_pagaVale') IS NOT NULL
	DROP PROCEDURE dbo.sp_pagaVale
GO
CREATE PROCEDURE [dbo].[sp_pagaVale]
	@idVale		INT,
	@usuario	INT,
	@lineas		LINEASVALE READONLY,
	@message	VARCHAR(200) = NULL OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	IF NOT EXISTS (SELECT 1 FROM @lineas)
	BEGIN
		SELECT @message = 'NO SE SELECCIONO NINGUN PRODUCTO'
		RETURN -1
	END

	DECLARE @estado CHAR(1), @sucursal INT, @monto DECIMAL(10,2), @idPago INT, @idTurno INT

	BEGIN TRY
		BEGIN TRANSACTION PAGAVALE

		SELECT @estado = estado, @sucursal = sucursal FROM VALE WITH (UPDLOCK) WHERE idVale = @idVale

		IF @estado IS NULL
		BEGIN
			SELECT @message = 'EL VALE NO EXISTE'
			ROLLBACK TRANSACTION PAGAVALE
			RETURN -1
		END

		IF @estado <> 'P'
		BEGIN
			SELECT @message = 'EL VALE YA ESTA PAGADO'
			ROLLBACK TRANSACTION PAGAVALE
			RETURN -1
		END

		-- Todas las lineas enviadas deben existir en el vale y estar sin pagar
		IF EXISTS (
			SELECT 1 FROM @lineas L
			LEFT JOIN DETALLEVALE D ON D.idVale = @idVale AND D.nDetalle = L.nDetalle
			WHERE D.nDetalle IS NULL OR D.pagado = 1
		)
		BEGIN
			SELECT @message = 'HAY PRODUCTOS QUE NO PERTENECEN AL VALE O YA ESTAN PAGADOS'
			ROLLBACK TRANSACTION PAGAVALE
			RETURN -1
		END

		SELECT @monto = SUM(D.subtotal)
		FROM DETALLEVALE D INNER JOIN @lineas L ON D.nDetalle = L.nDetalle
		WHERE D.idVale = @idVale

		SELECT @idTurno = idTurno FROM APERTURACAJAS WHERE estado = 'A' AND sucursal = @sucursal

		INSERT INTO PAGOVALE (idVale, fecha, monto, usuario, idTurno)
		VALUES (@idVale, GETDATE(), @monto, @usuario, @idTurno)

		SET @idPago = SCOPE_IDENTITY()

		UPDATE D SET pagado = 1, idPago = @idPago
		FROM DETALLEVALE D INNER JOIN @lineas L ON D.nDetalle = L.nDetalle
		WHERE D.idVale = @idVale

		UPDATE VALE
		SET saldo = saldo - @monto,
			estado = CASE WHEN saldo - @monto <= 0 THEN 'C' ELSE 'P' END
		WHERE idVale = @idVale

		COMMIT TRANSACTION PAGAVALE
		SELECT @message = 'PAGO REGISTRADO POR Q ' + CONVERT(VARCHAR(20), @monto)
		RETURN 0
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
		SELECT @message = 'HUBO UN ERROR AL REGISTRAR EL PAGO'
		RETURN -1
	END CATCH
END
GO
