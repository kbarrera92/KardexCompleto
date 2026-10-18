USE [farmaciahorro_ispro2]
GO
-- Los egresos (EGRESOS) ahora se ligan al VENDEDOR del turno de caja abierto (APERTURACAJAS.codempleado).
-- usuarioRegistra se conserva como auditoria (usuario que digito el egreso).
-- El vendedor lo toma el servidor del turno abierto de la sucursal (igual que idTurno); no se envia desde la aplicacion.
-- Ejecutar DESPUES de migracion_egresos_turno.sql. La firma de sp_mantEgresos no cambia.

IF COL_LENGTH('dbo.EGRESOS', 'vendedor') IS NULL
	ALTER TABLE dbo.EGRESOS ADD vendedor int NULL
GO

-- Egresos existentes: el vendedor del turno al que pertenecen
UPDATE E SET vendedor = A.codempleado
FROM dbo.EGRESOS E
INNER JOIN dbo.APERTURACAJAS A ON A.idTurno = E.idTurno
WHERE E.vendedor IS NULL
GO

-- =============================================
-- Author:		Kevin Barrera
-- Create date: 13/03/2025
-- Description:	Mantenimiento de egresos por sucursal. Los egresos se ligan al turno de caja abierto y a su vendedor.
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

	declare @SEC int, @idTurno int, @estadoTurno char(1), @vendedor int

	if @opcion = 'L'
	begin
		-- La columna del vendedor ocupa el lugar de usuarioRegistra (la grilla mapea por posicion)
		select e.idEgreso, e.fechaEgreso, c.nombreCategoriaEgreso, e.concepto, e.total, isnull(v.nombre, '') as nombreVendedor, s.nombreSuc
		from EGRESOS e
		inner join CATEGORIAEGRESO c on e.categoria = c.idCategoriaEgreso
		left join VENDEDOR v on e.vendedor = v.idVendedor
		inner join SUCURSAL s on e.sucursal = s.idSucursal
		where e.sucursal = @sucursal and cast(e.fechaEgreso as date) = cast(@fechaEgreso as date) and e.estado = 1
		order by e.fechaEgreso desc
		if @@ROWCOUNT = 0
			select @msg = 'No se encontraron registros'

		return 0
	end

	begin tran grabaegreso
	if @opcion = 'I'
	begin
		-- El egreso sale de la caja: debe existir un turno abierto en la sucursal; el vendedor es el del turno
		select @idTurno = idTurno, @vendedor = codempleado from APERTURACAJAS where estado = 'A' and sucursal = @sucursal
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

		insert into EGRESOS (idEgreso, fechaEgreso, concepto, usuarioRegistra, total, sucursal, categoria, estado, idTurno, vendedor)
		values (@SEC, @fechaEgreso, @concepto, @usuarioRegistra, @total, @sucursal, @categoria, 1, @idTurno, @vendedor)
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
