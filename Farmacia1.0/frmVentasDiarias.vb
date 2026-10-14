Imports System.Data.SqlClient
Public Class frmVentasDiarias

    Dim ds As DataSet
    Private lblResumen As Label

    Sub calculartotal()
        Dim tot As Double = 0.0
        For i = 0 To DataGridView1.Rows.Count - 1
            tot = tot + DataGridView1.Rows(i).Cells(6).Value
        Next
        TextBox1.Text = FormatNumber(tot, 2)

        'Egresos del turno (vista del vendedor) o de toda la sucursal (Reporte General)
        If lblResumen IsNot Nothing Then
            Dim sucursal As Integer = Convert.ToInt32(ConsultaParametro("codigoSucursal"))
            Dim egresos = ObtenerEgresosReporte(sucursal, DateTimePicker1.Value, Not CheckBox1.Checked)
            lblResumen.Text = TextoResumenEgresos(CDec(tot), egresos)
        End If
    End Sub


    Sub llenarDTSMP()
        Dim cmd As SqlCommand
        Dim da As SqlDataAdapter
        Dim dt As DataTable
        ds = New dsReportes

        'Por defecto solo el vendedor que abrió el turno (se relee de BD por si cambió en otra PC)
        CargaVendedorTurnoActivo()
        If Not CheckBox1.Checked AndAlso vendedorRegistrado = 0 Then
            Me.Text = "Ventas diarias"
            MessageBox.Show("No hay un turno abierto. Marque 'Reporte General' para ver todas las ventas de la sucursal.", "Sin turno", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        Me.Text = If(CheckBox1.Checked, "Ventas diarias - Todas", $"Ventas diarias - {nombreVendedorRegistrado}")

        Try
            openConnection()
            cmd = New SqlCommand()

            With cmd
                .CommandText = "sp_cortecaja"
                .CommandType = CommandType.StoredProcedure
                .Connection = conn
                .Parameters.AddWithValue("suc", Convert.ToInt32(ConsultaParametro("codigoSucursal")))
                .Parameters.AddWithValue("fecha", DateTimePicker1.Value)
                .Parameters.AddWithValue("user", If(CheckBox1.Checked, DBNull.Value, vendedorRegistrado))
            End With

            dt = ds.Tables("dtcortecaja")

            da = New SqlDataAdapter(cmd)
            da.Fill(ds.Tables("dtcortecaja"))

            For i = 0 To dt.Columns.Count - 1
                DataGridView1.Columns(i).DataPropertyName = dt.Columns(i).ToString
            Next

            dv = dt.DefaultView
            DataGridView1.DataSource = dv
        Catch ex As NullReferenceException
            MessageBox.Show("No se ha elegido un valor obligatorio", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Catch ex As Exception
            MessageBox.Show("Error al cargar los datos" & vbCrLf & "Error: " & ex.ToString)
        Finally
            closeConnection()

        End Try
    End Sub

    Private Sub frmVentasDiarias_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DateTimePicker1.Value = Now
        lblResumen = CrearLabelResumenEgresos(Me, 13, 425, 580)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            llenarDTSMP()
            calculartotal()
        Catch ex As Exception

        End Try
    End Sub
End Class