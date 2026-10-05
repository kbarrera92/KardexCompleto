Imports System.Data.SqlClient
Imports Serilog

Public Class FormPagosVales
    Private cargando As Boolean = False

    Private Sub FormPagosVales_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ConfigurarColumnas()
            CargarSucursales()
            DateTimePickerFecha.Value = Date.Now
            Estilos.AplicarEstilos(Me)
            CargarPagos()
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
        AgregarColumna(DataGridView1, "colIdPago", "No. Pago", "idPago", 8)
        AgregarColumna(DataGridView1, "colHora", "Hora", "fecha", 8, "HH:mm:ss")
        AgregarColumna(DataGridView1, "colIdVale", "No. Vale", "idVale", 8)
        AgregarColumna(DataGridView1, "colSucursal", "Sucursal", "nombreSuc", 15)
        AgregarColumna(DataGridView1, "colVendedor", "Vendedor", "nombreVendedor", 20)
        AgregarColumna(DataGridView1, "colMonto", "Monto", "monto", 9, "N2")
        AgregarColumna(DataGridView1, "colUsuario", "Registró", "nombreUsuario", 14)
        AgregarColumna(DataGridView1, "colEstadoPago", "Estado", "estadoPago", 9)
        AgregarColumna(DataGridView1, "colMotivo", "Motivo de anulación", "motivoAnulacion", 20)

        DataGridView2.AutoGenerateColumns = False
        DataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView2.Columns.Clear()
        AgregarColumna(DataGridView2, "colNDetalle", "No", "nDetalle", 5)
        AgregarColumna(DataGridView2, "colProducto", "Código", "producto", 8)
        AgregarColumna(DataGridView2, "colDProducto", "Descripción", "dProducto", 50)
        AgregarColumna(DataGridView2, "colCantidad", "Cant.", "cantidad", 8)
        AgregarColumna(DataGridView2, "colPrecio", "Precio", "precio", 12, "N2")
        AgregarColumna(DataGridView2, "colSubtotal", "Importe", "subtotal", 12, "N2")
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
        ComboBoxSucursal.Enabled = (nombreRol = "ADMINISTRADOR")
    End Sub

    Private Sub CargarPagos(Optional ByVal idPagoASeleccionar As Integer = 0)
        Try
            Dim dt As New DataTable

            openConnection()

            Using cmd As New SqlCommand("sp_consultaPagosVale", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@fecha", SqlDbType.Date).Value = DateTimePickerFecha.Value.Date
                cmd.Parameters.Add("@sucursal", SqlDbType.Int).Value = Convert.ToInt32(ComboBoxSucursal.SelectedValue)

                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using

            cargando = True
            DataGridView1.DataSource = dt
            cargando = False

            ActualizarTotal(dt)

            If DataGridView1.Rows.Count = 0 Then
                DataGridView2.DataSource = Nothing
                ActualizarBotones()
                Return
            End If

            Dim indice As Integer = 0
            If idPagoASeleccionar > 0 Then
                For i As Integer = 0 To DataGridView1.Rows.Count - 1
                    If Convert.ToInt32(DataGridView1.Rows(i).Cells("colIdPago").Value) = idPagoASeleccionar Then
                        indice = i
                        Exit For
                    End If
                Next
            End If

            DataGridView1.CurrentCell = DataGridView1.Rows(indice).Cells("colIdPago")
            DataGridView1.Rows(indice).Selected = True
            CargarDetalle()
        Catch ex As Exception
            cargando = False
            Log.Error($"Error al consultar pagos de vales. Error: {ex.Message}")
            MessageBox.Show("No fue posible consultar los pagos de vales.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            closeConnection()
        End Try
    End Sub

    Private Sub ActualizarTotal(ByVal dt As DataTable)
        Dim total As Object = dt.Compute("SUM(monto)", "estadoPago = 'ACTIVO'")
        lblTotal.Text = "Total pagos activos: Q " & If(IsDBNull(total), 0D, Convert.ToDecimal(total)).ToString("N2")
    End Sub

    Private Function PagoActual() As DataRowView
        If DataGridView1.CurrentRow Is Nothing Then Return Nothing
        Return TryCast(DataGridView1.CurrentRow.DataBoundItem, DataRowView)
    End Function

    Private Sub CargarDetalle()
        Dim pago As DataRowView = PagoActual()
        If pago Is Nothing Then
            DataGridView2.DataSource = Nothing
            ActualizarBotones()
            Return
        End If

        Try
            Dim dt As New DataTable

            openConnection()

            Using cmd As New SqlCommand("sp_detallePagoVale", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@idPago", SqlDbType.Int).Value = Convert.ToInt32(pago("idPago"))

                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using

            DataGridView2.DataSource = dt
        Catch ex As Exception
            Log.Error($"Error al consultar el detalle del pago. Error: {ex.Message}")
            MessageBox.Show("No fue posible consultar el detalle del pago.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            closeConnection()
        End Try

        ActualizarBotones()
    End Sub

    Private Sub ActualizarBotones()
        Dim pago As DataRowView = PagoActual()
        btnAnular.Enabled = nombreRol = "ADMINISTRADOR" AndAlso pago IsNot Nothing AndAlso Convert.ToString(pago("estadoPago")) = "ACTIVO"
    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        CargarPagos()
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

        If Convert.ToString(DataGridView1.Rows(e.RowIndex).Cells("colEstadoPago").Value) = "ANULADO" Then
            e.CellStyle.BackColor = Color.LightCoral
        End If
    End Sub

    Private Sub btnAnular_Click(sender As Object, e As EventArgs) Handles btnAnular.Click
        If nombreRol <> "ADMINISTRADOR" Then Return

        Dim pago As DataRowView = PagoActual()
        If pago Is Nothing Then Return

        Dim idPago As Integer = Convert.ToInt32(pago("idPago"))
        Dim monto As Decimal = Convert.ToDecimal(pago("monto"))

        Dim motivo As String = Microsoft.VisualBasic.InputBox($"Motivo de la anulación del pago No. {idPago} (Q {monto:N2}):", "Anular pago").Trim()
        If motivo = "" Then
            MessageBox.Show("Debe indicar el motivo de la anulación", "Anular pago", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        If MessageBox.Show($"¿Anular el pago de Q {monto:N2} del vale No. {pago("idVale")} de {pago("nombreVendedor")}?" & vbCrLf &
                           "El saldo del vale se restaurará y los productos volverán a quedar pendientes.", "Confirmar anulación",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
            Return
        End If

        Try
            Dim pMensaje As New SqlParameter("@message", SqlDbType.VarChar, 200) With {.Direction = ParameterDirection.Output}
            Dim pRetorno As New SqlParameter("@returnValue", SqlDbType.Int) With {.Direction = ParameterDirection.ReturnValue}

            Dim parametros As New List(Of SqlParameter) From {
                New SqlParameter("@idPago", SqlDbType.Int) With {.Value = idPago},
                New SqlParameter("@usuario", SqlDbType.Int) With {.Value = usuarioActual},
                New SqlParameter("@motivo", SqlDbType.VarChar, 200) With {.Value = If(motivo.Length > 200, motivo.Substring(0, 200), motivo)},
                pMensaje,
                pRetorno
            }

            Log.Information($"Anulando pago de vale {idPago}. Usuario: {usuarioActual}, monto: {monto}, motivo: {motivo}")

            Dim resultado = EjecutarStoredProcedureMultiple("sp_anulaPagoVale", parametros)

            If resultado.Item2.StartsWith("Error") Then
                Log.Error($"Error al ejecutar sp_anulaPagoVale. {resultado.Item2}")
                MessageBox.Show("Hubo un error en el procesamiento", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            Dim codigoRetorno As Integer = If(IsDBNull(pRetorno.Value) OrElse pRetorno.Value Is Nothing, -1, Convert.ToInt32(pRetorno.Value))
            Dim mensaje As String = Convert.ToString(pMensaje.Value)

            If codigoRetorno = 0 Then
                Log.Information($"Pago {idPago} anulado. {mensaje}")
                MessageBox.Show(mensaje, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information)
                CargarPagos(idPago)
                DibujaTarjetasResumen()
                BringToFront()
            Else
                Log.Warning($"El pago {idPago} no se anuló. {mensaje}")
                MessageBox.Show(mensaje, "No se pudo anular el pago", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            Log.Error($"Ocurrió un error. Error: {ex.Message}, Trace: {ex.StackTrace}")
            MessageBox.Show("Hubo un error en el procesamiento", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
