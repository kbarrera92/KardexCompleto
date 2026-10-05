Imports System.Data.SqlClient
Imports Serilog

Public Class FormVales
    Private dvProductos As DataView
    Private precioSeleccionado As Decimal
    Private existenciaSeleccionada As Decimal

    Private Sub FormVales_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ConfigurarColumnas()
            lblSucursal.Text = "Sucursal: " & nameSucActual
            lblFecha.Text = "Fecha: " & Format(DateTime.Now, "dd/MM/yyyy")
            CargarVendedores()
            CargarProductos()
            Estilos.AplicarEstilos(Me)
        Catch ex As Exception
            Log.Error($"Ocurrió un error. Error: {ex.Message}")
        End Try
    End Sub

    'Las columnas se crean aquí y no en el .Designer.vb: el diseñador de Visual Studio
    'las borraba al regenerar el archivo.
    Private Sub ConfigurarColumnas()
        DataGridView1.AutoGenerateColumns = False
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView1.Columns.Clear()
        AgregarColumna(DataGridView1, "codpro", "Código", "idProducto", 10)
        AgregarColumna(DataGridView1, "dpro", "Descripción", "dProducto", 36)
        AgregarColumna(DataGridView1, "exist", "Existencia", "Existencia", 12, "N0")
        AgregarColumna(DataGridView1, "marca", "Marca", "laboratorio", 16)
        AgregarColumna(DataGridView1, "pres", "Presentación", "presentacion", 16)
        AgregarColumna(DataGridView1, "preciopro", "Precio", "precio", 10, "N2")

        DataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView2.Columns.Clear()
        AgregarColumna(DataGridView2, "clNo", "No", Nothing, 6)
        AgregarColumna(DataGridView2, "clCodigo", "Código", Nothing, 11)
        AgregarColumna(DataGridView2, "clDescripcion", "Descripción", Nothing, 41)
        AgregarColumna(DataGridView2, "clCant", "Cant.", Nothing, 9)
        AgregarColumna(DataGridView2, "clPrecio", "Precio", Nothing, 14)
        AgregarColumna(DataGridView2, "clSubt", "Importe", Nothing, 16)
    End Sub

    Private Sub AgregarColumna(ByVal dgv As DataGridView, ByVal nombre As String, ByVal titulo As String,
                               ByVal propiedad As String, ByVal peso As Single, Optional ByVal formato As String = "")
        Dim columna As New DataGridViewTextBoxColumn With {
            .Name = nombre,
            .HeaderText = titulo,
            .ReadOnly = True,
            .FillWeight = peso
        }
        If propiedad IsNot Nothing Then columna.DataPropertyName = propiedad
        If formato <> "" Then columna.DefaultCellStyle.Format = formato
        dgv.Columns.Add(columna)
    End Sub

    Private Sub CargarVendedores()
        Dim dtVend As New DataTable

        Try
            openConnection()

            Using cmd As New SqlCommand("sp_MantenimientoVendedor", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@Accion", SqlDbType.VarChar, 20).Value = "LISTAR"
                cmd.Parameters.Add("@mensaje", SqlDbType.VarChar, 200).Value = DBNull.Value

                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dtVend)
                End Using
            End Using
        Catch ex As Exception
            Log.Error($"Error al cargar vendedores. Error: {ex.Message}")
            MessageBox.Show("No fue posible cargar los vendedores.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        Finally
            closeConnection()
        End Try

        'Solo vendedores activos (el sp también lo valida al grabar el vale)
        If dtVend.Columns.Count > 3 Then
            For i As Integer = dtVend.Rows.Count - 1 To 0 Step -1
                Dim valor As String = Convert.ToString(dtVend.Rows(i)(3))
                If valor.Equals("False", StringComparison.OrdinalIgnoreCase) OrElse valor.Equals("Inactivo", StringComparison.OrdinalIgnoreCase) Then
                    dtVend.Rows.RemoveAt(i)
                End If
            Next
        End If

        ComboBoxVendedor.DataSource = dtVend
        ComboBoxVendedor.ValueMember = dtVend.Columns(0).ColumnName
        ComboBoxVendedor.DisplayMember = dtVend.Columns(1).ColumnName
        ComboBoxVendedor.SelectedIndex = -1
    End Sub

    Private Sub CargarProductos()
        Try
            openConnection()

            Using cmd As New SqlCommand("sp_productosVale", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@suc", SqlDbType.Int).Value = sucActual

                Using da As New SqlDataAdapter(cmd)
                    Dim dt As New DataTable
                    da.Fill(dt)
                    dvProductos = dt.DefaultView
                    DataGridView1.DataSource = dvProductos
                End Using
            End Using
        Catch ex As Exception
            Log.Error($"Error al cargar productos del vale. Error: {ex.Message}")
            MessageBox.Show("No fue posible cargar los productos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            closeConnection()
        End Try
    End Sub

    Private Sub txtbuscapro_TextChanged(sender As Object, e As EventArgs) Handles txtbuscapro.TextChanged
        If dvProductos Is Nothing Then Return

        Try
            Dim texto As String = txtbuscapro.Text.Replace("'", "''").Replace("[", "[[]")
            dvProductos.RowFilter = String.Format("dProducto like '%{0}%' Or Convert(idProducto,'System.String') like '{0}%'", texto)
        Catch ex As Exception
            Log.Error($"Ocurrió un error. Error: {ex.Message}")
        End Try
    End Sub

    Private Sub txtbuscapro_KeyDown(sender As Object, e As KeyEventArgs) Handles txtbuscapro.KeyDown
        If e.KeyCode = Keys.Enter Then
            DataGridView1.Select()
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.Enter Then
            SeleccionarProducto()
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        If e.RowIndex >= 0 Then SeleccionarProducto()
    End Sub

    Private Sub SeleccionarProducto()
        If DataGridView1.CurrentRow Is Nothing Then Return

        Try
            With DataGridView1.CurrentRow
                txtcodpro.Text = Convert.ToString(.Cells("codpro").Value)
                txtdescpro.Text = Convert.ToString(.Cells("dpro").Value) & " " & Convert.ToString(.Cells("pres").Value)
                precioSeleccionado = Convert.ToDecimal(.Cells("preciopro").Value)
                existenciaSeleccionada = Convert.ToDecimal(.Cells("exist").Value)
            End With

            txtprecio.Text = precioSeleccionado.ToString("N2")
            txtexistencia.Text = existenciaSeleccionada.ToString("N0")
            txtcantidad.Text = "1"
            txtcantidad.Select()
            txtcantidad.SelectAll()
        Catch ex As Exception
            Log.Error($"Ocurrió un error. Error: {ex.Message}")
        End Try
    End Sub

    Private Sub txtcantidad_KeyDown(sender As Object, e As KeyEventArgs) Handles txtcantidad.KeyDown
        If e.KeyCode = Keys.Enter Then
            btnAgregar.PerformClick()
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        If String.IsNullOrWhiteSpace(txtcodpro.Text) Then
            MessageBox.Show("Seleccione un producto", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim cantidad As Integer
        If Not Integer.TryParse(txtcantidad.Text.Trim(), cantidad) OrElse cantidad <= 0 Then
            MessageBox.Show("Cantidad no válida", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtcantidad.Select()
            Return
        End If

        'Si el producto ya está en el vale se suma a la misma línea
        Dim filaExistente As DataGridViewRow = Nothing
        Dim cantidadEnVale As Integer = 0
        For Each fila As DataGridViewRow In DataGridView2.Rows
            If Convert.ToString(fila.Cells("clCodigo").Value) = txtcodpro.Text Then
                filaExistente = fila
                cantidadEnVale = Convert.ToInt32(fila.Cells("clCant").Value)
                Exit For
            End If
        Next

        If cantidad + cantidadEnVale > existenciaSeleccionada Then
            MessageBox.Show($"Existencia insuficiente. Disponible: {existenciaSeleccionada:N0}, en el vale: {cantidadEnVale}",
                            "Existencia insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtcantidad.Select()
            Return
        End If

        If filaExistente Is Nothing Then
            DataGridView2.Rows.Add(DataGridView2.Rows.Count + 1, txtcodpro.Text, txtdescpro.Text.Trim(), cantidad,
                                   precioSeleccionado.ToString("N2"), (cantidad * precioSeleccionado).ToString("N2"))
        Else
            Dim nuevaCantidad As Integer = cantidadEnVale + cantidad
            filaExistente.Cells("clCant").Value = nuevaCantidad
            filaExistente.Cells("clSubt").Value = (nuevaCantidad * precioSeleccionado).ToString("N2")
        End If

        LimpiarProducto()
        txttotal.Text = CalcularTotal().ToString("N2")
        txtbuscapro.Clear()
        txtbuscapro.Select()
    End Sub

    Private Sub btnQuitar_Click(sender As Object, e As EventArgs) Handles btnQuitar.Click
        If DataGridView2.CurrentRow Is Nothing Then
            MessageBox.Show("Seleccione un producto del vale para quitarlo", "Quitar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        DataGridView2.Rows.RemoveAt(DataGridView2.CurrentRow.Index)
        For i As Integer = 0 To DataGridView2.Rows.Count - 1
            DataGridView2.Rows(i).Cells("clNo").Value = i + 1
        Next
        txttotal.Text = CalcularTotal().ToString("N2")
    End Sub

    Private Sub btnDescartar_Click(sender As Object, e As EventArgs) Handles btnDescartar.Click
        If DataGridView2.Rows.Count = 0 Then Return

        If MessageBox.Show("¿Desea descartar este vale?", "Descartar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            LimpiarFormulario()
        End If
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Close()
    End Sub

    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
        If ComboBoxVendedor.SelectedValue Is Nothing Then
            MessageBox.Show("Seleccione el vendedor", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If DataGridView2.Rows.Count = 0 Then
            MessageBox.Show("No se ha agregado ningún producto", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        Dim total As Decimal = CalcularTotal()
        If MessageBox.Show($"¿Registrar el vale de {ComboBoxVendedor.Text} por Q {total:N2}?" & vbCrLf &
                           "Los productos se descontarán del inventario.", "Confirmar vale",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
            Return
        End If

        Try
            Dim detalles As New DataTable()
            detalles.Columns.Add("nDetalleV", GetType(Short))
            detalles.Columns.Add("producto", GetType(Integer))
            detalles.Columns.Add("cantidad", GetType(Integer))
            detalles.Columns.Add("precio", GetType(Decimal))
            detalles.Columns.Add("subtotal", GetType(Decimal))

            'El sp vuelve a calcular precio e importe en el servidor
            For i As Integer = 0 To DataGridView2.Rows.Count - 1
                Dim cantidad As Integer = Convert.ToInt32(DataGridView2.Rows(i).Cells("clCant").Value)
                Dim precio As Decimal = Convert.ToDecimal(DataGridView2.Rows(i).Cells("clPrecio").Value)
                detalles.Rows.Add(CShort(i + 1), Convert.ToInt32(DataGridView2.Rows(i).Cells("clCodigo").Value),
                                  cantidad, precio, cantidad * precio)
            Next

            Dim pMensaje As New SqlParameter("@message", SqlDbType.VarChar, 200) With {.Direction = ParameterDirection.Output}
            Dim pIdVale As New SqlParameter("@idVale", SqlDbType.Int) With {.Direction = ParameterDirection.Output}
            Dim pRetorno As New SqlParameter("@returnValue", SqlDbType.Int) With {.Direction = ParameterDirection.ReturnValue}

            Dim parametros As New List(Of SqlParameter) From {
                New SqlParameter("@sucursal", SqlDbType.Int) With {.Value = sucActual},
                New SqlParameter("@vendedor", SqlDbType.Int) With {.Value = Convert.ToInt32(ComboBoxVendedor.SelectedValue)},
                New SqlParameter("@usuario", SqlDbType.Int) With {.Value = usuarioActual},
                New SqlParameter("@detalles", SqlDbType.Structured) With {.TypeName = "dbo.DETALLESVENTA", .Value = detalles},
                pMensaje,
                pIdVale,
                pRetorno
            }

            Log.Information($"Registrando vale. Sucursal: {sucActual}, vendedor: {ComboBoxVendedor.SelectedValue}, usuario: {usuarioActual}, líneas: {detalles.Rows.Count}, total: {total}")

            Dim resultado = EjecutarStoredProcedureMultiple("sp_grabaVale", parametros)

            If resultado.Item2.StartsWith("Error") Then
                Log.Error($"Error al ejecutar sp_grabaVale. {resultado.Item2}")
                MessageBox.Show("Hubo un error en el procesamiento", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            Dim codigoRetorno As Integer = If(IsDBNull(pRetorno.Value) OrElse pRetorno.Value Is Nothing, -1, Convert.ToInt32(pRetorno.Value))
            Dim mensaje As String = Convert.ToString(pMensaje.Value)

            If codigoRetorno = 0 Then
                Log.Information($"Vale registrado. idVale: {pIdVale.Value}")
                MessageBox.Show(mensaje, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LimpiarFormulario()
                CargarProductos()
            Else
                Log.Warning($"El vale no se registró. {mensaje}")
                MessageBox.Show(mensaje, "No se pudo registrar el vale", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            Log.Error($"Ocurrió un error. Error: {ex.Message}, Trace: {ex.StackTrace}")
            MessageBox.Show("Hubo un error en el procesamiento", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function CalcularTotal() As Decimal
        Dim total As Decimal = 0
        For Each fila As DataGridViewRow In DataGridView2.Rows
            total += Convert.ToDecimal(fila.Cells("clSubt").Value)
        Next
        Return total
    End Function

    Private Sub LimpiarProducto()
        txtcodpro.Clear()
        txtdescpro.Clear()
        txtprecio.Clear()
        txtexistencia.Clear()
        txtcantidad.Text = "1"
        precioSeleccionado = 0
        existenciaSeleccionada = 0
    End Sub

    Private Sub LimpiarFormulario()
        DataGridView2.Rows.Clear()
        LimpiarProducto()
        txtbuscapro.Clear()
        txttotal.Text = "0.00"
        ComboBoxVendedor.SelectedIndex = -1
        ComboBoxVendedor.Select()
    End Sub
End Class
