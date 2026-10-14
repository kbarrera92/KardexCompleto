USE [farmaciahorro_ispro2]
GO
-- Los egresos (EGRESOS) ahora se vinculan al turno de caja (APERTURACAJAS) y se restan en el cierre.
-- El efectivo esperado del cierre pasa a ser:
--     saldoInicial + SUM(VENTAS.total - VENTAS.tarjeta) - SUM(EGRESOS.total)     (del turno, egresos con estado = 1)
-- Antes se sumaba el total de las ventas (tarjeta incluida) y no se descontaban los egresos.
-- Ejecutar ANTES de desplegar la version nueva del sistema (FormCerrarCaja envia @totalegresos y @totaltarjeta).

IF COL_LENGTH('dbo.EGRESOS', 'idTurno') IS NULL
	ALTER TABLE dbo.EGRESOS ADD idTurno int NULL
GO
IF COL_LENGTH('dbo.APERTURACAJAS', 'totalEgresos') IS NULL
	ALTER TABLE dbo.APERTURACAJAS ADD totalEgresos decimal(10,2) NULL
GO
IF COL_LENGTH('dbo.APERTURACAJAS', 'totalTarjeta') IS NULL
	ALTER TABLE dbo.APERTURACAJAS ADD totalTarjeta decimal(10,2) NULL
GO

-- Total de egresos activos. Con @idTurno suma los de ese turno; sin el, los de la sucursal en la fecha.
CREATE OR ALTER PROCEDURE dbo.sp_totalEgresos
	@sucursal	INT,
	@fecha		DATETIME = NULL,
	@idTurno	INT = NULL
AS
BEGIN
	SET NOCOUNT ON;

	IF @idTurno IS NOT NULL
		SELECT ISNULL(SUM(total), 0) AS totalEgresos
		FROM EGRESOS
		WHERE idTurno = @idTurno AND estado = 1
	ELSE
		SELECT ISNULL(SUM(total), 0) AS totalEgresos
		FROM EGRESOS
		WHERE sucursal = @sucursal AND estado = 1
		  AND CAST(fechaEgreso AS DATE) = CAST(ISNULL(@fecha, GETDATE()) AS DATE)
END
GO

-- =============================================
-- Author:		Kevin Barrera
-- Create date: 13/03/2025
-- Description:	Mantenimiento de egresos por sucursal. Los egresos se ligan al turno de caja abierto.
-- =============================================
CREATE OR ALTER PROCEDURE dbo.sp_mantEgresos
	@opcion			char(1),
	@msg			varchar(200) = '' output,
	@idEgreso		int = null,
	@fechaEgreso	datetime = null,
	@concepto		varchar(100) = null,
	@usuarioRegistra varchar(20) = null,
	@total			decimal(10,2) = null,
	@sucursal		int = null,
	@categoria		int = null
AS
BEGIN
	SET NOCOUNT ON;

	declare @SEC int, @idTurno int, @estadoTurno char(1)

	if @opcion = 'L'
	begin
		select e.idEgreso, e.fechaEgreso, c.nombreCategoriaEgreso, e.concepto, e.total, e.usuarioRegistra, s.nombreSuc
		from EGRESOS e
		inner join CATEGORIAEGRESO c on categoria = c.idCategoriaEgreso
		inner join USUARIO u on usuarioRegistra = u.nombreUsuario
		inner join SUCURSAL s on e.sucursal = s.idSucursal
		where e.sucursal = @sucursal and cast(fechaEgreso as date) = cast(@fechaEgreso as date) and e.estado = 1
		order by e.fechaEgreso desc
		if @@ROWCOUNT = 0
			select @msg = 'No se encontraron registros'

		return 0
	end

	begin tran grabaegreso
	if @opcion = 'I'
	begin
		-- El egreso sale de la caja: debe existir un turno abierto en la sucursal
		select @idTurno = idTurno from APERTURACAJAS where estado = 'A' and sucursal = @sucursal
		if @idTurno is null
		begin
			rollback tran grabaegreso
			select @msg = 'NO EXISTE UNA CAJA ABIERTA PARA ESTA SUCURSAL'
			return -1
		end

		exec @SEC = sp_get_sequence 'EGRESOS'

		if @SEC = -1
		begin
			select @msg = 'La tabla no est' + char(225) + ' configurada en la tabla secuenciales'
			rollback tran grabaegreso
			return -1
		end

		insert into EGRESOS (idEgreso, fechaEgreso, concepto, usuarioRegistra, total, sucursal, categoria, estado, idTurno)
		values (@SEC, @fechaEgreso, @concepto, @usuarioRegistra, @total, @sucursal, @categoria, 1, @idTurno)
		if @@ROWCOUNT = 0 or @@ERROR <> 0
		begin
			rollback tran grabaegreso
			select @msg = 'Hubo un error al grabar el registro'
			return -1
		end

		set @msg = 'El registro se guard' + char(243) + ' correctamente'
		commit tran grabaegreso
		return 0
	end

	if @opcion = 'D'
	begin
		if isnull(@idEgreso, 0) = 0
		begin
			rollback tran grabaegreso
			select @msg = 'Hubo un error al descartar el registro'
			return -1
		end

		-- No se alteran egresos de una caja que ya cerro (el cierre quedo calculado con ellos)
		select @estadoTurno = A.estado
		from EGRESOS E
		inner join APERTURACAJAS A on A.idTurno = E.idTurno
		where E.idEgreso = @idEgreso
		if @estadoTurno = 'C'
		begin
			rollback tran grabaegreso
			select @msg = 'No se puede descartar un egreso de una caja cerrada'
			return -1
		end

		update EGRESOS set estado = 0 where idEgreso = @idEgreso
		if @@ROWCOUNT = 0 or @@ERROR <> 0
		begin
			rollback tran grabaegreso
			select @msg = 'Hubo un error al cambiar el estado del registro'
			return -1
		end

		set @msg = 'El registro se descart' + char(243) + ' correctamente'
		commit tran grabaegreso
		return 0
	end

	-- Opcion no valida: no dejar la transaccion abierta
	rollback tran grabaegreso
	return -1
END
GO

-- =============================================
-- Author:		Kevin Fernando Barrera Reyes
-- Description:	Mantenimiento de aperturas y cierres de cajas para cuadres.
--				El cierre descuenta las ventas con tarjeta y los egresos del turno.
-- =============================================
CREATE OR ALTER PROCEDURE dbo.sp_mantAperturasCajas
	@opt			CHAR(1),
	@msg			VARCHAR(200) OUTPUT,
	@rc				INT OUTPUT,
	@usuario		INT = 0,
	@saldoinicial	DECIMAL(10,2) = 0 OUTPUT,
	@sucursal		INT = 0,
	@codempleado	INT = 0,

	@totalfisico	DECIMAL(10,2) = 0 OUTPUT,
	@diferencia		DECIMAL(10,2) = 0 OUTPUT,
	@totalsistema	DECIMAL(10,2) = 0 OUTPUT,
	@nombreEmpleado	VARCHAR(150) = NULL OUTPUT,
	@totalegresos	DECIMAL(10,2) = 0 OUTPUT,
	@totaltarjeta	DECIMAL(10,2) = 0 OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @idCaja INT
	DECLARE @sucursalEmpleado INT
	DECLARE @FECHAGT DATETIME = DATEADD(HOUR, 2, GETDATE()),
			@inicial DECIMAL(10,2), @efectivoEsperado DECIMAL(10,2), @totsistema DECIMAL(10,2),
			@tottarjeta DECIMAL(10,2), @totegresos DECIMAL(10,2), @dif DECIMAL(10,2)

	BEGIN TRANSACTION MANTCAJAS
	IF @opt = 'B'
	BEGIN
		SELECT
			idTurno 'Código',
			U.nick 'Usuario',
			fechaApertura 'Fecha inicio',
			fechaCierre 'Fecha cierre',
			saldoInicial 'Cant. Inicial',
			totalSistema 'Cant. Sistema',
			totalFisico 'Cant. Física',
			diferencia 'Diferencia',
			S.nombreSuc 'Sucursal',
			ISNULL(totalTarjeta, 0) 'Tarjeta',
			ISNULL(totalEgresos, 0) 'Egresos'
		FROM APERTURACAJAS A
		LEFT JOIN USUARIO U ON A.idUsuario = U.idUsuario
		LEFT JOIN SUCURSAL S ON A.sucursal = S.idSucursal
		WHERE A.estado = 'C'
		ORDER BY A.fechaCierre DESC
		IF @@ROWCOUNT = 0
		BEGIN
			SELECT @msg = 'NO SE ENCONTRARON REGISTROS'
			SELECT @rc = -1
		END

		GOTO FINALIZA
	END

	IF @opt = 'A'
	BEGIN
		IF EXISTS (SELECT TOP 1 1 FROM APERTURACAJAS WHERE estado = 'A' AND sucursal = @sucursal)
		BEGIN
			SELECT @rc = -1
			SELECT @msg = 'YA EXISTE UNA CAJA ABIERTA PARA ESTA SUCURSAL'
			ROLLBACK TRAN MANTCAJAS
			RETURN @rc
		END

		DECLARE @SEC INT
		EXEC @SEC = sp_get_sequence 'APERTURACAJAS'

		IF @SEC = -1
		BEGIN
			SELECT @MSG = 'La tabla no est' + CHAR(225) + ' configurada en la tabla secuenciales'
			ROLLBACK TRAN MANTCAJAS
			SET @rc = -1
			RETURN @rc
		END

		SET @codempleado = CAST(@codempleado AS INT)

		SELECT @nombreEmpleado = nombre FROM VENDEDOR WHERE idVendedor = @codempleado

		IF ISNULL(@nombreEmpleado, '') = ''
		BEGIN
			SELECT @MSG = 'EL CODIGO DEL EMPLEADO ES INCORRECTO'
			SELECT @rc = -2
			ROLLBACK TRANSACTION MANTCAJAS
			RETURN @rc
		END

		INSERT INTO APERTURACAJAS (idTurno, idUsuario, fechaApertura, saldoInicial, estado, fechaCierre, totalSistema, totalFisico, diferencia, sucursal, codempleado)
		VALUES (@SEC, @usuario, @FECHAGT, @saldoinicial, 'A', NULL, NULL, NULL, NULL, @sucursal, @codempleado)
		IF @@ERROR <> 0
		BEGIN
			ROLLBACK TRANSACTION MANTCAJAS
			SET @rc = -1
			SELECT @MSG = 'HUBO UN ERROR AL GRABAR EL REGISTRO'
			RETURN @rc
		END

		GOTO FINALIZA
	END

	IF @opt = 'C'
	BEGIN
		SELECT @idCaja = idTurno
			 , @inicial = saldoInicial
		FROM APERTURACAJAS WHERE estado = 'A' AND sucursal = @sucursal AND codempleado = @codempleado
		IF ISNULL(@idCaja, '') = ''
		BEGIN
			SELECT @rc = -1
			SELECT @msg = 'NO EXISTE UNA CAJA ABIERTA PARA ESTA SUCURSAL'
			ROLLBACK TRAN MANTCAJAS
			RETURN @rc
		END

		SELECT @saldoinicial = ISNULL(@inicial, 0)

		-- Ventas del turno; lo cobrado con tarjeta no entra a la caja
		SELECT @totsistema = SUM(total), @tottarjeta = SUM(ISNULL(tarjeta, 0))
		FROM VENTAS WHERE turno = @idCaja AND idSucursal = @sucursal
		SELECT @totalsistema = ISNULL(@totsistema, 0)
		SELECT @totaltarjeta = ISNULL(@tottarjeta, 0)

		-- Egresos de efectivo del turno (los descartados tienen estado = 0)
		SELECT @totegresos = SUM(total) FROM EGRESOS WHERE idTurno = @idCaja AND estado = 1
		SELECT @totalegresos = ISNULL(@totegresos, 0)

		SELECT @efectivoEsperado = @saldoinicial + @totalsistema - @totaltarjeta - @totalegresos
		SELECT @dif = @totalfisico - @efectivoEsperado

		UPDATE APERTURACAJAS
		SET estado = 'C',
			fechaCierre = @FECHAGT,
			totalSistema = @totalsistema,
			totalFisico = @totalfisico,
			diferencia = @dif,
			totalEgresos = @totalegresos,
			totalTarjeta = @totaltarjeta
		WHERE idTurno = @idCaja AND sucursal = @sucursal AND codempleado = @codempleado
		IF @@ERROR <> 0
		BEGIN
			ROLLBACK TRANSACTION MANTCAJAS
			SET @rc = -2
			SELECT @MSG = 'HUBO UN ERROR AL CERRAR LA CAJA'
			RETURN @rc
		END

		GOTO FINALIZA
	END

	FINALIZA:
		COMMIT TRANSACTION MANTCAJAS
		SELECT @rc = 0
		SELECT @MSG = 'LA TRANSACCION SE REALIZO CORRECTAMENTE'
		SELECT @diferencia = @dif
		RETURN @rc
END
GO
