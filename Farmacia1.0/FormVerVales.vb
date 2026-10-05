Imports System.Data.SqlClient
Imports Serilog

Public Class FormVerVales
    Private cargando As Boolean = False

    Private Sub FormVerVales_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ConfigurarColumnas()
            CargarSucursales()
            CargarVendedores()
            ComboBoxEstado.SelectedIndex = 0
            Estilos.AplicarEstilos(Me)
            CargarVales()
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
        AgregarColumna(DataGridView1, "colIdVale", "No. Vale", "idVale", 7)
        AgregarColumna(DataGridView1, "colFecha", "Fecha", "fecha", 11, "dd/MM/yyyy")
        AgregarColumna(DataGridView1, "colVence", "Vence", "fechaVencimiento", 11, "dd/MM/yyyy")
        AgregarColumna(DataGridView1, "colSucursal", "Sucursal", "nombreSuc", 16)
        AgregarColumna(DataGridView1, "colVendedor", "Vendedor", "nombreVendedor", 22)
        AgregarColumna(DataGridView1, "colTotal", "Total", "total", 9, "N2")
        AgregarColumna(DataGridView1, "colSaldo", "Saldo", "saldo", 9, "N2")
        AgregarColumna(DataGridView1, "colEstadoVale", "Estado", "estadoVale", 12)

        DataGridView2.AutoGenerateColumns = False
        DataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView2.Columns.Clear()
        DataGridView2.Columns.Add(New DataGridViewCheckBoxColumn With {.Name = "colPagar", .HeaderText = "Pagar", .FillWeight = 6})
        AgregarColumna(DataGridView2, "colNDetalle", "No", "nDetalle", 5)
        AgregarColumna(DataGridView2, "colProducto", "Código", "producto", 8)
        AgregarColumna(DataGridView2, "colDProducto", "Descripción", "dProducto", 45)
        AgregarColumna(DataGridView2, "colCantidad", "Cant.", "cantidad", 7)
        AgregarColumna(DataGridView2, "colPrecio", "Precio", "precio", 10, "N2")
        AgregarColumna(DataGridView2, "colSubtotal", "Importe", "subtotal", 11, "N2")
        DataGridView2.Columns.Add(New DataGridViewCheckBoxColumn With {.Name = "colPagado", .HeaderText = "Pagado", .DataPropertyName = "pagado", .ReadOnly = True, .FillWeight = 7})
    End Sub

    Private Sub AgregarColumna(ByVal dgv As DataGridView, ByVal nombre As String, ByVal titulo As String,
                               ByVal propiedad As String, ByVal peso As Single, Optional ByVal formato As String = "")
        Dim columna As New DataGridViewTextBoxColumn With {
            .Name = nombre,
            .HeaderText = titulo,
            .DataPropertyName = propiedad,
            .ReadOnly = True,
            .FillWeight = peso
        }
        If formato <> "" Then columna.DefaultCellStyle.Format = formato
        dgv.Columns.Add(columna)
    End Sub

    Private Sub CargarSucursales()
        Dim dtSuc As DataTable = updateCm("SELECT idSucursal, nombreSuc FROM SUCURSAL")
        If dtSuc Is Nothing Then Return

        Dim filaTodas As DataRow = dtSuc.NewRow()
        filaTodas("idSucursal") = 0
        filaTodas("nombreSuc") = "-- Todas las sucursales --"
        dtSuc.Rows.InsertAt(filaTodas, 0)

        ComboBoxSucursal.DataSource = dtSuc
        ComboBoxSucursal.ValueMember = "idSucursal"
        ComboBoxSucursal.DisplayMember = "nombreSuc"
        ComboBoxSucursal.SelectedValue = sucActual
        If ComboBoxSucursal.SelectedIndex = -1 Then ComboBoxSucursal.SelectedIndex = 0
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

        Dim filaTodos As DataRow = dtVend.NewRow()
        filaTodos(0) = 0
        filaTodos(1) = "-- Todos los vendedores --"
        dtVend.Rows.InsertAt(filaTodos, 0)

        ComboBoxVendedor.DataSource = dtVend
        ComboBoxVendedor.ValueMember = dtVend.Columns(0).ColumnName
        ComboBoxVendedor.DisplayMember = dtVend.Columns(1).ColumnName
        ComboBoxVendedor.SelectedIndex = 0
    End Sub

    Private Sub CargarVales(Optional ByVal idValeASeleccionar As Integer = 0)
        Try
            Dim dt As New DataTable

            openConnection()

            Using cmd As New SqlCommand("sp_consultaVales", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@sucursal", SqlDbType.Int).Value = Convert.ToInt32(ComboBoxSucursal.SelectedValue)
                cmd.Parameters.Add("@vendedor", SqlDbType.Int).Value = Convert.ToInt32(ComboBoxVendedor.SelectedValue)
                cmd.Parameters.Add("@estado", SqlDbType.VarChar, 10).Value =
                    If(ComboBoxEstado.SelectedIndex <= 0, CObj(DBNull.Value), ComboBoxEstado.Text)

                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using

            cargando = True
            DataGridView1.DataSource = dt
            cargando = False

            If DataGridView1.Rows.Count = 0 Then
                DataGridView2.DataSource = Nothing
                ActualizarResumen()
                Return
            End If

            Dim indice As Integer = 0
            If idValeASeleccionar > 0 Then
                For i As Integer = 0 To DataGridView1.Rows.Count - 1
                    If Convert.ToInt32(DataGridView1.Rows(i).Cells("colIdVale").Value) = idValeASeleccionar Then
                        indice = i
                        Exit For
                    End If
                Next
            End If

            DataGridView1.CurrentCell = DataGridView1.Rows(indice).Cells("colIdVale")
            DataGridView1.Rows(indice).Selected = True
            CargarDetalle()
        Catch ex As Exception
            cargando = False
            Log.Error($"Error al consultar vales. Error: {ex.Message}")
            MessageBox.Show("No fue posible consultar los vales.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            closeConnection()
        End Try
    End Sub

    Private Function ValeActual() As DataRowView
        If DataGridView1.CurrentRow Is Nothing Then Return Nothing
        Return TryCast(DataGridView1.CurrentRow.DataBoundItem, DataRowView)
    End Function

    Private Sub CargarDetalle()
        Dim vale As DataRowView = ValeActual()
        If vale Is Nothing Then
            DataGridView2.DataSource = Nothing
            ActualizarResumen()
            Return
        End If

        Try
            Dim dt As New DataTable

            openConnection()

            Using cmd As New SqlCommand("sp_detalleVale", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@idVale", SqlDbType.Int).Value = Convert.ToInt32(vale("idVale"))

                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using

            DataGridView2.DataSource = dt
        Catch ex As Exception
            Log.Error($"Error al consultar el detalle del vale. Error: {ex.Message}")
            MessageBox.Show("No fue posible consultar el detalle del vale.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            closeConnection()
        End Try

        ActualizarResumen()
    End Sub

    Private Sub ActualizarResumen()
        Dim vale As DataRowView = ValeActual()
        Dim pendiente As Boolean = vale IsNot Nothing AndAlso Convert.ToString(vale("estadoVale")) <> "PAGADO"

        lblSaldoVale.Text = "Saldo del vale: Q " & If(vale Is Nothing, 0D, Convert.ToDecimal(vale("saldo"))).ToString("N2")
        lblSeleccionado.Text = "Monto seleccionado: Q " & MontoSeleccionado().ToString("N2")
        btnPagarSeleccionados.Enabled = pendiente
        btnPagarTodo.Enabled = pendiente
    End Sub

    Private Function LineaPagada(ByVal fila As DataGridViewRow) As Boolean
        Return Convert.ToBoolean(fila.Cells("colPagado").Value)
    End Function

    Private Function MontoSeleccionado() As Decimal
        Dim monto As Decimal = 0
        For Each fila As DataGridViewRow In DataGridView2.Rows
            If Not LineaPagada(fila) AndAlso Convert.ToBoolean(fila.Cells("colPagar").Value) Then
                monto += Convert.ToDecimal(fila.Cells("colSubtotal").Value)
            End If
        Next
        Return monto
    End Function

    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        CargarVales()
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Close()
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If cargando Then Return
        CargarDetalle()
    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        If e.RowIndex < 0 Then Return

        Select Case Convert.ToString(DataGridView1.Rows(e.RowIndex).Cells("colEstadoVale").Value)
            Case "VENCIDO"
                e.CellStyle.BackColor = Color.LightCoral
            Case "PAGADO"
                e.CellStyle.BackColor = Color.LightGreen
        End Select
    End Sub

    Private Sub DataGridView2_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles DataGridView2.DataBindingComplete
        For Each fila As DataGridViewRow In DataGridView2.Rows
            If LineaPagada(fila) Then
                fila.Cells("colPagar").ReadOnly = True
                fila.DefaultCellStyle.BackColor = Color.LightGray
            End If
        Next
    End Sub

    Private Sub DataGridView2_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles DataGridView2.CurrentCellDirtyStateChanged
        If DataGridView2.IsCurrentCellDirty Then
            DataGridView2.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub

    Private Sub DataGridView2_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellValueChanged
        If e.RowIndex >= 0 AndAlso DataGridView2.Columns(e.ColumnIndex).Name = "colPagar" Then
            lblSeleccionado.Text = "Monto seleccionado: Q " & MontoSeleccionado().ToString("N2")
        End If
    End Sub

    Private Sub btnPagarSeleccionados_Click(sender As Object, e As EventArgs) Handles btnPagarSeleccionados.Click
        Dim lineas As New List(Of Short)
        For Each fila As DataGridViewRow In DataGridView2.Rows
            If Not LineaPagada(fila) AndAlso Convert.ToBoolean(fila.Cells("colPagar").Value) Then
                lineas.Add(Convert.ToInt16(fila.Cells("colNDetalle").Value))
            End If
        Next

        If lineas.Count = 0 Then
            MessageBox.Show("Marque los productos que desea pagar", "Pagar vale", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        PagarLineas(lineas, MontoSeleccionado())
    End Sub

    Private Sub btnPagarTodo_Click(sender As Object, e As EventArgs) Handles btnPagarTodo.Click
        Dim lineas As New List(Of Short)
        Dim monto As Decimal = 0
        For Each fila As DataGridViewRow In DataGridView2.Rows
            If Not LineaPagada(fila) Then
                lineas.Add(Convert.ToInt16(fila.Cells("colNDetalle").Value))
                monto += Convert.ToDecimal(fila.Cells("colSubtotal").Value)
            End If
        Next

        If lineas.Count = 0 Then
            MessageBox.Show("El vale no tiene productos pendientes de pago", "Pagar vale", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        PagarLineas(lineas, monto)
    End Sub

    Private Sub PagarLineas(ByVal lineas As List(Of Short), ByVal monto As Decimal)
        Dim vale As DataRowView = ValeActual()
        If vale Is Nothing Then Return

        Dim idVale As Integer = Convert.ToInt32(vale("idVale"))

        If MessageBox.Show($"¿Registrar el pago de Q {monto:N2} del vale No. {idVale} de {vale("nombreVendedor")}?" & vbCrLf &
                           $"Productos incluidos: {lineas.Count}", "Confirmar pago",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
            Return
        End If

        Try
            Dim dtLineas As New DataTable()
            dtLineas.Columns.Add("nDetalle", GetType(Short))
            For Each n As Short In lineas
                dtLineas.Rows.Add(n)
            Next

            Dim pMensaje As New SqlParameter("@message", SqlDbType.VarChar, 200) With {.Direction = ParameterDirection.Output}
            Dim pRetorno As New SqlParameter("@returnValue", SqlDbType.Int) With {.Direction = ParameterDirection.ReturnValue}

            Dim parametros As New List(Of SqlParameter) From {
                New SqlParameter("@idVale", SqlDbType.Int) With {.Value = idVale},
                New SqlParameter("@usuario", SqlDbType.Int) With {.Value = usuarioActual},
                New SqlParameter("@lineas", SqlDbType.Structured) With {.TypeName = "dbo.LINEASVALE", .Value = dtLineas},
                pMensaje,
                pRetorno
            }

            Log.Information($"Pagando vale {idVale}. Usuario: {usuarioActual}, líneas: {lineas.Count}, monto: {monto}")

            Dim resultado = EjecutarStoredProcedureMultiple("sp_pagaVale", parametros)

            If resultado.Item2.StartsWith("Error") Then
                Log.Error($"Error al ejecutar sp_pagaVale. {resultado.Item2}")
                MessageBox.Show("Hubo un error en el procesamiento", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            Dim codigoRetorno As Integer = If(IsDBNull(pRetorno.Value) OrElse pRetorno.Value Is Nothing, -1, Convert.ToInt32(pRetorno.Value))
            Dim mensaje As String = Convert.ToString(pMensaje.Value)

            If codigoRetorno = 0 Then
                Log.Information($"Pago del vale {idVale} registrado. {mensaje}")
                MessageBox.Show(mensaje, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information)
                CargarVales(idVale)
                If nombreRol = "ADMINISTRADOR" Then
                    DibujaTarjetasResumen()
                End If
                BringToFront()
            Else
                Log.Warning($"El pago del vale {idVale} no se registró. {mensaje}")
                MessageBox.Show(mensaje, "No se pudo registrar el pago", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            Log.Error($"Ocurrió un error. Error: {ex.Message}, Trace: {ex.StackTrace}")
            MessageBox.Show("Hubo un error en el procesamiento", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
