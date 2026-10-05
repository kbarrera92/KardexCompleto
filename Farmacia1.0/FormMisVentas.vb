Imports System.Data.SqlClient
Imports Serilog

Public Class FormMisVentas

    Private Sub FormMisVentas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
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
        AgregarColumna("colProducto", "Producto", "dProducto", 44)
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
        'El vendedor es el que abrió el turno (código capturado en FormAbrirCaja)
        If vendedorRegistrado = 0 Then
            lblVendedor.Text = "No hay un turno abierto"
            DataGridView1.DataSource = Nothing
            Return
        End If

        lblVendedor.Text = $"Vendedor: {nombreVendedorRegistrado}    Fecha: {Format(DateTime.Now, "dd/MM/yyyy")}"

        Try
            Dim dt As New DataTable

            openConnection()

            Using cmd As New SqlCommand("sp_misVentas", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@sucursal", SqlDbType.Int).Value = Convert.ToInt32(ConsultaParametro("codigoSucursal"))
                cmd.Parameters.Add("@vendedor", SqlDbType.Int).Value = vendedorRegistrado
                cmd.Parameters.Add("@horasDiferencia", SqlDbType.Int).Value = Convert.ToInt32(ConsultaParametro("horasDiferencia"))

                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using

            DataGridView1.DataSource = dt
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

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Close()
    End Sub
End Class
