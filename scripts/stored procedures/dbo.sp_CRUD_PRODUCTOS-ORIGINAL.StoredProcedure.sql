USE [farmaciahorro_ispro2]
GO

/****** Object:  StoredProcedure [dbo].[sp_CRUD_PRODUCTOS]    Script Date: 28/08/2026 04:00:46 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Kevin Barrera
-- Create date: 18-02-2025
-- Description:	Mantenimiento del catálogo de productos
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[sp_CRUD_PRODUCTOS]
    @Accion NVARCHAR(10),  -- 'LEER', 'INSERTAR', 'ACTUALIZAR', 'ELIMINAR'
    @idProducto INT = NULL,
    @dProducto VARCHAR(150) = NULL,
    @composicion VARCHAR(150) = NULL,
    @presentacion VARCHAR(100) = NULL,
    @aterapeutica VARCHAR(150) = NULL,
    @indicaciones VARCHAR(150) = NULL,
    @contraindicaciones VARCHAR(150) = NULL,
    @observaciones VARCHAR(250) = NULL,
    @proveedor INT = NULL,
    @medida VARCHAR(75) = NULL,
    @categoria INT = NULL,
    @laboratorio VARCHAR(100) = NULL,
    @precio DECIMAL(10,2) = NULL,
    @costo DECIMAL(10,2) = NULL,
    @fechaRegistro DATE = NULL,
    @estanteria INT = NULL,
    @barcode VARCHAR(25) = NULL,
    @stockmin INT = NULL,
    @estado BIT = NULL,
	@flag char(2) = NULL,

	@msg VARCHAR(200) OUTPUT,
	@precios PRECIOSXSUCURSAL READONLY
AS
BEGIN
    SET NOCOUNT ON;

	DECLARE @resultadoGrabaPrecios smallint

	BEGIN TRANSACTION MANTPRODUCTOS
    IF @Accion = 'LEER'
    BEGIN
		DECLARE @QUERYSELECT VARCHAR(5000), @CONDITION VARCHAR(100)
        -- Leer todos los productos activos o uno en específico
        SET @QUERYSELECT = 'SELECT 
				P.idProducto,
				P.dProducto,
				ISNULL(P.composicion, ''''),
				ISNULL(P.presentacion, ''''),
				ISNULL(P.aterapeutica, ''''),
				ISNULL(P.indicaciones, ''''),
				ISNULL(P.contraindicaciones, ''''),
				ISNULL(P.observaciones, ''''),
				PR.rzProveedor,
				ISNULL(P.medida, ''''),
				C.categoria,
				ISNULL(P.laboratorio, ''''),
				P.precio,
				P.costo,
				P.fechaRegistro,
				ISNULL(P.estanteria, 0),
				ISNULL(P.barcode, ''''),
				ISNULL(P.stockmin, 0),
				ISNULL(P.bandera, ''N'')
			FROM PRODUCTOS P
			INNER JOIN CATEGORIA C ON P.categoria = C.idCategoria
			INNER JOIN PROVEEDOR PR ON P.proveedor = PR.idProveedor
			WHERE estado = 1'

		SET @CONDITION = 'AND idProducto = ' + CAST(@idProducto as VARCHAR) + ';'

		IF @idProducto IS NOT NULL
			SET @QUERYSELECT = TRIM(CONCAT_WS(' ', @QUERYSELECT, @CONDITION))	
    
		EXEC sp_sqlEXEC @QUERYSELECT
		IF @@ROWCOUNT > 0
			SELECT @msg = 'REGISTROS: ' + CAST(@@ROWCOUNT AS VARCHAR)
	END
    ELSE IF @Accion = 'INSERTAR'
    BEGIN
		DECLARE @SEC INT				
		EXEC @SEC = sp_get_sequence 'PRODUCTOS'
		
		IF @SEC = -1
		BEGIN
			SELECT @msg = 'La tabla no está configurada en la tabla secuenciales'
			ROLLBACK TRAN MANTPRODUCTOS
			RETURN -1
		END

		SET @idProducto = @SEC

        INSERT INTO PRODUCTOS (
            idProducto, dProducto, composicion, presentacion, aterapeutica, indicaciones, 
            contraindicaciones, observaciones, proveedor, medida, categoria, laboratorio, 
            precio, costo, fechaRegistro, estanteria, barcode, stockmin, estado, bandera
        )
        VALUES (
            @idProducto, @dProducto, @composicion, @presentacion, @aterapeutica, @indicaciones, 
            @contraindicaciones, @observaciones, @proveedor, @medida, @categoria, @laboratorio, 
            @precio, @costo, @fechaRegistro, @estanteria, @barcode, @stockmin, 1, @flag
        );
		IF @@ERROR != 0
		BEGIN
			SELECT @msg = 'ERROR AL INSERTAR EL REGISTRO'
			ROLLBACK TRANSACTION MANTPRODUCTOS
			RETURN -1
		END

		INSERT INTO EXISTENCIAS (idProducto, estado) VALUES (@idProducto, 1) 
		IF @@ERROR != 0
		BEGIN
			SELECT @msg = 'ERROR AL INSERTAR EL REGISTRO EN LA TABLA EXISTENCIAS'
			ROLLBACK TRANSACTION MANTPRODUCTOS
			RETURN -1
		END

		--Graba precios
		EXEC @resultadoGrabaPrecios = sp_mantPreciosXSucursal 
			@opcion = 2, 
			@msg = @msg output, 
			@codigo_producto = @idProducto, @precios = @precios

		IF @resultadoGrabaPrecios <> 0
		BEGIN
			SELECT @msg = 'ERROR AL INSERTAR EL REGISTRO EN LA TABLA PRECIOS_SUCURSAL'
			ROLLBACK TRANSACTION MANTPRODUCTOS
			RETURN -1
		END

		SELECT @msg = 'REGISTRO INSERTADO CORRECTAMENTE'
    END
    ELSE IF @Accion = 'ACTUALIZAR'
    BEGIN
        UPDATE PRODUCTOS
        SET 
            dProducto = @dProducto,
            composicion = @composicion,
            presentacion = @presentacion,
            aterapeutica = @aterapeutica,
            indicaciones = @indicaciones,
            contraindicaciones = @contraindicaciones,
            observaciones = @observaciones,
            proveedor = @proveedor,
            medida = @medida,
            categoria = @categoria,
            laboratorio = @laboratorio,
            precio = @precio,
            costo = @costo,
            fechaRegistro = @fechaRegistro,
            estanteria = @estanteria,
            barcode = @barcode,
            stockmin = @stockmin,
			bandera = @flag
        WHERE idProducto = @idProducto;
		IF @@ERROR != 0
		BEGIN
			SELECT @msg = 'ERROR AL INSERTAR EL REGISTRO'
			ROLLBACK TRANSACTION MANTPRODUCTOS
			RETURN -1
		END

		IF EXISTS (SELECT TOP 1 1 FROM @precios)
		BEGIN
			--Graba precios
			EXEC @resultadoGrabaPrecios = sp_mantPreciosXSucursal 
				@opcion = 2, 
				@msg = @msg output, 
				@codigo_producto = @idProducto, @precios = @precios

			IF @resultadoGrabaPrecios <> 0
			BEGIN
				SELECT @msg = 'ERROR AL INSERTAR EL REGISTRO EN LA TABLA PRECIOS_SUCURSAL'
				ROLLBACK TRANSACTION MANTPRODUCTOS
				RETURN -1
			END
		END

		SELECT @msg = 'REGISTRO ACTUALIZADO CORRECTAMENTE'
    END
    ELSE IF @Accion = 'ELIMINAR'
    BEGIN
        -- Cambiar estado a 0 (Inactivo)
        UPDATE PRODUCTOS
        SET estado = 0
        WHERE idProducto = @idProducto;
		IF @@ERROR != 0
		BEGIN
			SELECT @msg = 'ERROR AL INSERTAR EL REGISTRO'
			ROLLBACK TRANSACTION MANTPRODUCTOS
			RETURN -1
		END

		SELECT @msg = 'REGISTRO DESHABILITADO CORRECTAMENTE'
    END

	COMMIT TRANSACTION MANTPRODUCTOS
	RETURN 0
END;
GO


