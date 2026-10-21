Imports System.Data.SqlClient
Imports Serilog

Public Class FormMisVentas

    Private lblResumen As Label

    Private Sub FormMisVentas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            lblResumen = CrearLabelResumenEgresos(Me, 160, 463, 480)
            ConfigurarColumnas()
            Estilos.AplicarEstilos(Me)
            CargarVentas()
        Catch ex As Exception
            Log.Error($"Ocurrió un error. Error: {ex.Message}")
        End Try
    End Sub

    Private Sub FormMisVentas_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        CargarVentas()
    End Sub

    'Las columnas se crean aquí y no en el .Designer.vb: el diseñador de Visual Studio
    'las borraba al regenerar el archivo.
    Private Sub ConfigurarColumnas()
        DataGridView1.AutoGenerateColumns = False
        DataGridView1.Columns.Clear()
        AgregarColumna("colVenta", "No. Venta", "nVenta", 10)
        AgregarColumna("colHora", "Hora", "fechaVenta", 10, "HH:mm")
        AgregarColumna("colVendedor", "Vendedor", "vendedor", 16)
        AgregarColumna("colProducto", "Producto", "dProducto", 30)
        AgregarColumna("colCantidad", "Cant.", "cantidad", 10)
        AgregarColumna("colPrecio", "Precio", "precio", 12, "N2")
        AgregarColumna("colImporte", "Importe", "subtotal", 14, "N2")
    End Sub

    Private Sub AgregarColumna(ByVal nombre As String, ByVal titulo As String, ByVal propiedad As String,
                               ByVal peso As Single, Optional ByVal formato As String = "")
        Dim columna As New DataGridViewTextBoxColumn With {
            .Name = nombre,
            .HeaderText = titulo,
            .DataPropertyName = propiedad,
            .ReadOnly = True,
            .FillWeight = peso
        }
        If formato <> "" Then columna.DefaultCellStyle.Format = formato
        DataGridView1.Columns.Add(columna)
    End Sub

    Private Sub CargarVentas()
        'Por defecto solo el vendedor que abrió el turno (se relee de BD por si otra PC de la sucursal
        'abrió o cerró el turno); con "Ver todas" se muestran las ventas del día de toda la sucursal.
        CargaVendedorTurnoActivo()
        Dim verTodas As Boolean = chkVerTodas.Checked
        DataGridView1.Columns("colVendedor").Visible = verTodas

        If Not verTodas AndAlso vendedorRegistrado = 0 Then
            lblVendedor.Text = "No hay un turno abierto"
            DataGridView1.DataSource = Nothing
            lblResumen.Text = String.Empty
            Return
        End If

        Dim fecha As String = Format(DateTime.Now, "dd/MM/yyyy")
        lblVendedor.Text = If(verTodas,
            $"Sucursal: {nameSucActual}    Todas las ventas    Fecha: {fecha}",
            $"Vendedor: {nombreVendedorRegistrado}    Fecha: {fecha}")

        Try
            Dim dt As New DataTable

            openConnection()

            Using cmd As New SqlCommand("sp_misVentas", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@sucursal", SqlDbType.Int).Value = Convert.ToInt32(ConsultaParametro("codigoSucursal"))
                cmd.Parameters.Add("@vendedor", SqlDbType.Int).Value = If(verTodas, CObj(DBNull.Value), vendedorRegistrado)
                cmd.Parameters.Add("@horasDiferencia", SqlDbType.Int).Value = Convert.ToInt32(ConsultaParametro("horasDiferencia"))

                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using

            DataGridView1.DataSource = dt

            Dim ventas As Decimal = 0D
            For Each fila As DataRow In dt.Rows
                If Not IsDBNull(fila("subtotal")) Then ventas += Convert.ToDecimal(fila("subtotal"))
            Next
            Dim sucursal As Integer = Convert.ToInt32(ConsultaParametro("codigoSucursal"))
            lblResumen.Text = TextoResumenEgresos(ventas, ObtenerEgresosReporte(sucursal, DateTime.Now, Not verTodas))
        Catch ex As Exception
            Log.Error($"Error al consultar mis ventas. Error: {ex.Message}")
            MessageBox.Show("No fue posible consultar las ventas.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            closeConnection()
        End Try
    End Sub

    Private Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.Click
        CargarVentas()
    End Sub

    Private Sub chkVerTodas_CheckedChanged(sender As Object, e As EventArgs) Handles chkVerTodas.CheckedChanged
        CargarVentas()
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Close()
    End Sub
End Class
