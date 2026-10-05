USE [farmaciahorro_ispro2]
GO
-- Permite anular pagos de vales (PAGOVALE) y agrega los sp para revisarlos.
-- Ejecutar ANTES de desplegar la version nueva del sistema (FormPagosVales y tarjeta "Pagos de vales").

IF COL_LENGTH('dbo.PAGOVALE', 'estado') IS NULL
	ALTER TABLE dbo.PAGOVALE ADD estado char(1) NOT NULL CONSTRAINT DF_PAGOVALE_estado DEFAULT ('A')
GO
IF COL_LENGTH('dbo.PAGOVALE', 'fechaAnulacion') IS NULL
	ALTER TABLE dbo.PAGOVALE ADD fechaAnulacion datetime NULL
GO
IF COL_LENGTH('dbo.PAGOVALE', 'usuarioAnula') IS NULL
	ALTER TABLE dbo.PAGOVALE ADD usuarioAnula int NULL
GO
IF COL_LENGTH('dbo.PAGOVALE', 'motivoAnulacion') IS NULL
	ALTER TABLE dbo.PAGOVALE ADD motivoAnulacion varchar(200) NULL
GO
IF OBJECT_ID('dbo.CK_PAGOVALE_estado', 'C') IS NULL
	ALTER TABLE dbo.PAGOVALE WITH CHECK ADD CONSTRAINT CK_PAGOVALE_estado CHECK (estado = 'A' OR estado = 'N')
GO

CREATE OR ALTER PROCEDURE dbo.sp_consultaPagosVale
	@fecha		DATE,
	@sucursal	INT = NULL
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		P.idPago,
		P.fecha,
		P.idVale,
		S.nombreSuc,
		VE.nombre AS nombreVendedor,
		P.monto,
		U.nombreUsuario,
		CASE WHEN P.estado = 'N' THEN 'ANULADO' ELSE 'ACTIVO' END AS estadoPago,
		P.motivoAnulacion
	FROM PAGOVALE P
	INNER JOIN VALE V ON P.idVale = V.idVale
	INNER JOIN VENDEDOR VE ON V.vendedor = VE.idVendedor
	INNER JOIN SUCURSAL S ON V.sucursal = S.idSucursal
	LEFT JOIN USUARIO U ON P.usuario = U.idUsuario
	WHERE CONVERT(DATE, P.fecha) = @fecha
	  AND (ISNULL(@sucursal, 0) = 0 OR V.sucursal = @sucursal)
	ORDER BY P.idPago DESC
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_detallePagoVale
	@idPago INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		D.nDetalle,
		D.producto,
		P.dProducto,
		D.cantidad,
		D.precio,
		D.subtotal,
		D.pagado
	FROM DETALLEVALE D
	INNER JOIN PRODUCTOS P ON D.producto = P.idProducto
	WHERE D.idPago = @idPago
	ORDER BY D.nDetalle
END
GO

CREATE OR ALTER PROCEDURE dbo.sp_anulaPagoVale
	@idPago		INT,
	@usuario	INT,
	@motivo		VARCHAR(200),
	@message	VARCHAR(200) = NULL OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	IF ISNULL(LTRIM(RTRIM(@motivo)), '') = ''
	BEGIN
		SELECT @message = 'DEBE INDICAR EL MOTIVO DE LA ANULACION'
		RETURN -1
	END

	DECLARE @estado CHAR(1), @idVale INT, @monto DECIMAL(10,2)

	BEGIN TRY
		BEGIN TRANSACTION ANULAPAGOVALE

		SELECT @estado = estado, @idVale = idVale, @monto = monto
		FROM PAGOVALE WITH (UPDLOCK) WHERE idPago = @idPago

		IF @estado IS NULL
		BEGIN
			SELECT @message = 'EL PAGO NO EXISTE'
			ROLLBACK TRANSACTION ANULAPAGOVALE
			RETURN -1
		END

		IF @estado <> 'A'
		BEGIN
			SELECT @message = 'EL PAGO YA ESTA ANULADO'
			ROLLBACK TRANSACTION ANULAPAGOVALE
			RETURN -1
		END

		-- Se conserva idPago en las lineas como rastro hasta que se vuelvan a pagar
		UPDATE DETALLEVALE SET pagado = 0 WHERE idPago = @idPago

		UPDATE VALE SET saldo = saldo + @monto, estado = 'P' WHERE idVale = @idVale

		UPDATE PAGOVALE
		SET estado = 'N', fechaAnulacion = GETDATE(), usuarioAnula = @usuario, motivoAnulacion = @motivo
		WHERE idPago = @idPago

		COMMIT TRANSACTION ANULAPAGOVALE
		SELECT @message = 'PAGO ANULADO. SE RESTAURO Q ' + CONVERT(VARCHAR(20), @monto) + ' AL SALDO DEL VALE'
		RETURN 0
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
		SELECT @message = 'HUBO UN ERROR AL ANULAR EL PAGO'
		RETURN -1
	END CATCH
END
GO
