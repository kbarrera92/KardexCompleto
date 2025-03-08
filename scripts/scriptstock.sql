use farmaciahorro_ispro2
go

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

declare @count int = (select count(1) from #tempsucursales)
select @count

drop table #tempsucursales