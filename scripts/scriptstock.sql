use db_ab10ba_kbarreradev
go

begin try

select idSucursal 
into #tempsucursales
from SUCURSAL

select '#tempsucursales'
select idSucursal from #tempsucursales

begin tran createstocktable

if OBJECT_ID('EXISTENCIAS') is null
	create table EXISTENCIAS (
		idProducto int primary key,
		estado bit
	) on [primary]

declare @sucursales int = (select count(1) from #tempsucursales), @cont int = 1, @colName varchar(20), @codSuc varchar(10)
select @sucursales 'Cantidad Sucursales'

while @cont <= @sucursales
begin
	set @codSuc = cast((select top(1) idSucursal from #tempsucursales) as varchar)
	set @colName = CONCAT('suc_', @codSuc)
	select @colName

	select column_name from information_schema.columns where table_name='EXISTENCIAS' and column_name= @colName
	if @@ROWCOUNT > 0
	begin
		print 'columna ' + @colName + ' ya existe...'
	end
	else
	begin
		exec ('alter table EXISTENCIAS add ' + @colName + ' decimal(10,2) null')
		print 'columna ' + @colName + ' agregada correctamente...'
	end

	delete top(1) from #tempsucursales
	set @cont = @cont + 1
end

insert into EXISTENCIAS (idProducto, estado)
select idProducto, estado from PRODUCTOS
if @@ERROR <> 0
begin
	select ERROR_MESSAGE()
	rollback tran createstocktable
	drop table #tempsucursales
end

DECLARE @sql NVARCHAR(MAX) = ''
DECLARE @suc NVARCHAR(10)

select idSucursal 
into #tempsucursales2
from SUCURSAL

-- Cursor para recorrer las sucursales almacenadas en la tabla temporal
DECLARE cur CURSOR FOR 
SELECT idSucursal FROM #tempsucursales2 -- Reemplaza con el nombre real de tu tabla temporal

OPEN cur
FETCH NEXT FROM cur INTO @suc

WHILE @@FETCH_STATUS = 0
BEGIN
    -- Construir la sentencia UPDATE para cada sucursal
    SET @sql = @sql + '
    UPDATE e
    SET e.suc_' + cast(@suc as varchar) + ' = ex.Existencia
    FROM Existencias e
    INNER JOIN (
        SELECT idProducto,
            (ISNULL((SELECT SUM(DC.cantidad) FROM DETALLECOMPRA DC INNER JOIN COMPRA C ON DC.nCompra = C.nCompra WHERE P.idProducto = DC.producto AND C.sucursal = ' + cast(@suc as varchar) + '), 0) + 
            ISNULL((SELECT SUM(DET.cant) FROM DETENTRADAXTRASLADO DET INNER JOIN ENTXTRASLADO EXT ON DET.nentrxtrasld = EXT.nEXTraslado WHERE P.idProducto = DET.producto AND EXT.sucursal = ' + cast(@suc as varchar) + '), 0) +
            ISNULL((SELECT SUM(DEXT.cantidad) FROM DETENTRADAXT DEXT INNER JOIN TRASIEGOE TE ON DEXT.nentraxtrasiego = TE.nTrasiego WHERE P.idProducto = DEXT.producto AND TE.sucursal = ' + cast(@suc as varchar) + '), 0) +
            ISNULL((SELECT SUM(DA.cantidad) FROM DETAJUSTE DA INNER JOIN AJUSTE A ON DA.najuste = A.nAjuste INNER JOIN TIPOAJUSTE TA ON A.tipoAjuste = TA.idTipoAjuste WHERE P.idProducto = DA.producto AND A.sucursal = ' + cast(@suc as varchar) + ' AND TA.idTipoAjuste = 100),0)) - 
            (ISNULL((SELECT SUM(DV.cantidad) FROM DETALLEVENTAS DV INNER JOIN VENTAS V ON DV.nVenta = V.nVenta WHERE P.idProducto = DV.producto AND V.idSucursal = ' + cast(@suc as varchar) + '), 0) + 
            ISNULL((SELECT SUM(DST.cantidad) FROM DETSALXTRASLADO DST INNER JOIN SALXTRASLADO SXT ON DST.nsalxtraslado = SXT.nSXTraslado WHERE P.idProducto = DST.producto AND SXT.sucursalSalida = ' + cast(@suc as varchar) + '), 0) +
            ISNULL((SELECT SUM(DSXT.cantidad) FROM DETSALIDAXT DSXT INNER JOIN TRASIEGOSSAL SXTRA ON DSXT.nsalxtrasiego = SXTRA.ntrasiegosalida WHERE P.idProducto = DSXT.producto AND SXTRA.sucursal = ' + cast(@suc as varchar) + '), 0) +
            ISNULL((SELECT SUM(DA.cantidad) FROM DETAJUSTE DA INNER JOIN AJUSTE A ON DA.najuste = A.nAjuste INNER JOIN TIPOAJUSTE TA ON A.tipoAjuste = TA.idTipoAjuste WHERE P.idProducto = DA.producto AND A.sucursal = ' + cast(@suc as varchar) + ' AND TA.idTipoAjuste = 101),0)) AS Existencia
        FROM PRODUCTOS P
    ) ex ON e.idProducto = ex.idProducto; 
    '

    FETCH NEXT FROM cur INTO @suc
END

CLOSE cur
DEALLOCATE cur

-- Ejecutar el SQL dinámico
EXEC sp_executesql @sql


commit tran createstocktable
drop table #tempsucursales
drop table #tempsucursales2
select * from EXISTENCIAS

end try
begin catch
	select ERROR_MESSAGE()
	rollback tran createstocktable
	drop table #tempsucursales
	drop table #tempsucursales2
end catch