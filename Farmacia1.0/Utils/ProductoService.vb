Module ProductoService
    Public Function ValidarProducto(prod As Producto, esNuevo As Boolean) As String
        Dim errores As New List(Of String)

        ' Si es actualización, validar idProducto
        If esNuevo AndAlso prod.IdProducto <= 0 Then
            errores.Add("El idProducto debe ser válido para actualizar.")
        End If

        ' dProducto (Obligatorio, max 150)
        If String.IsNullOrWhiteSpace(prod.DProducto) Then
            errores.Add("El campo dProducto es obligatorio.")
        ElseIf prod.DProducto.Length > 150 Then
            errores.Add("El campo dProducto no puede exceder 150 caracteres.")
        End If

        ' presentacion (Obligatorio, max 100)
        If String.IsNullOrWhiteSpace(prod.Presentacion) Then
            errores.Add("El campo presentacion es obligatorio.")
        ElseIf prod.Presentacion.Length > 100 Then
            errores.Add("El campo presentacion no puede exceder 100 caracteres.")
        End If

        ' proveedor (Obligatorio)
        If Not prod.Proveedor.HasValue OrElse prod.Proveedor <= 0 Then
            errores.Add("El campo proveedor es obligatorio y debe ser un número entero positivo.")
        End If

        ' categoria (Obligatorio)
        If Not prod.Categoria.HasValue OrElse prod.Categoria <= 0 Then
            errores.Add("El campo categoria es obligatorio y debe ser un número entero positivo.")
        End If

        ' precio (Obligatorio)
        If Not prod.Precio.HasValue Then
            errores.Add("El campo precio es obligatorio.")
        ElseIf prod.Precio <= 0D Then
            errores.Add("El precio debe ser mayor que 0.")
        End If

        ' costo (Obligatorio)
        If Not prod.Costo.HasValue Then
            errores.Add("El campo costo es obligatorio.")
        ElseIf prod.Costo < 0D Then
            errores.Add("El costo no puede ser negativo.")
        ElseIf prod.Costo >= prod.Precio Then
            errores.Add("El costo no puede ser mayor que el precio.")
        End If

        ' fechaRegistro (Obligatorio)
        If Not prod.FechaRegistro.HasValue Then
            errores.Add("El campo fechaRegistro es obligatorio.")
        End If

        ' composicion (opcional, max 150)
        If Not String.IsNullOrEmpty(prod.Composicion) AndAlso prod.Composicion.Length > 150 Then
            errores.Add("El campo composicion no puede exceder 150 caracteres.")
        End If

        ' aterapeutica (opcional, max 150)
        If Not String.IsNullOrEmpty(prod.Aterapeutica) AndAlso prod.Aterapeutica.Length > 150 Then
            errores.Add("El campo aterapeutica no puede exceder 150 caracteres.")
        End If

        ' indicaciones (opcional, max 150)
        If Not String.IsNullOrEmpty(prod.Indicaciones) AndAlso prod.Indicaciones.Length > 150 Then
            errores.Add("El campo indicaciones no puede exceder 150 caracteres.")
        End If

        ' contraindicaciones (opcional, max 150)
        If Not String.IsNullOrEmpty(prod.Contraindicaciones) AndAlso prod.Contraindicaciones.Length > 150 Then
            errores.Add("El campo contraindicaciones no puede exceder 150 caracteres.")
        End If

        ' observaciones (opcional, max 250)
        If Not String.IsNullOrEmpty(prod.Observaciones) AndAlso prod.Observaciones.Length > 250 Then
            errores.Add("El campo observaciones no puede exceder 250 caracteres.")
        End If

        ' medida (opcional, max 75)
        If Not String.IsNullOrEmpty(prod.Medida) AndAlso prod.Medida.Length > 75 Then
            errores.Add("El campo medida no puede exceder 75 caracteres.")
        End If

        ' laboratorio (opcional, max 100)
        If Not String.IsNullOrEmpty(prod.Laboratorio) AndAlso prod.Laboratorio.Length > 100 Then
            errores.Add("El campo laboratorio no puede exceder 100 caracteres.")
        End If

        ' barcode (opcional, max 25)
        If Not String.IsNullOrEmpty(prod.Barcode) AndAlso prod.Barcode.Length > 25 Then
            errores.Add("El campo barcode no puede exceder 25 caracteres.")
        End If

        ' estanteria (opcional, debe ser positivo si se da)
        If prod.Estanteria.HasValue AndAlso prod.Estanteria < 0 Then
            errores.Add("El campo estanteria debe ser un número entero positivo.")
        End If

        ' stockmin (opcional, debe ser >=0 si se da)
        If prod.Stockmin.HasValue AndAlso prod.Stockmin < 0 Then
            errores.Add("El campo stockmin no puede ser negativo.")
        End If

        ' Resultado
        If errores.Count > 0 Then
            Return String.Join(Environment.NewLine, errores)
        Else
            Return String.Empty
        End If
    End Function

End Module
