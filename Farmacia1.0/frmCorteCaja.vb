Imports System.Data.SqlClient
Imports Serilog

Public Class frmCorteCaja

    Dim sql As String = "SELECT idSucursal, nombreSuc FROM SUCURSAL"
    Dim ds As DataSet
    Private lblResumen As Label

    Sub llenarDTSMP()
        Dim cmd As SqlCommand
        Dim da As SqlDataAdapter
        Dim dt As DataTable
        ds = New dsReportes

        Try
            openConnection()
            cmd = New SqlCommand()
            With cmd
                .CommandText = "sp_cortecaja"
                .CommandType = CommandType.StoredProcedure
                .Connection = conn
                .Parameters.AddWithValue("suc", CInt(ComboBox1.SelectedValue.ToString))
                .Parameters.AddWithValue("fecha", DateTimePicker1.Value)
                Dim idVendedor As Integer = CInt(ComboBox2.SelectedValue.ToString)
                .Parameters.AddWithValue("user", If(idVendedor = 0, CObj(DBNull.Value), idVendedor))
            End With
            dt = ds.Tables("dtcortecaja")

            da = New SqlDataAdapter(cmd)
            da.Fill(ds.Tables("dtcortecaja"))

            For i = 0 To dt.Columns.Count - 1
                DataGridView1.Columns(i).DataPropertyName = dt.Columns(i).ToString
            Next

            dv = dt.DefaultView
            DataGridView1.DataSource = dv

            ActualizaResumenEgresos(dt, CInt(ComboBox1.SelectedValue.ToString), CInt(ComboBox2.SelectedValue.ToString))
        Catch ex As NullReferenceException
            MessageBox.Show("No se ha elegido un valor obligatorio", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Catch ex As Exception
            MessageBox.Show("Error al cargar los datos" & vbCrLf & "Error: " & ex.ToString)
        Finally
            closeConnection()

        End Try
    End Sub

    ''' <summary>Ventas, egresos y neto bajo el detalle. Con un vendedor distinto del turno abierto se muestran los egresos de la sucursal.</summary>
    Private Sub ActualizaResumenEgresos(ByVal dt As DataTable, ByVal sucursal As Integer, ByVal idVendedor As Integer)
        Dim ventas As Decimal = 0D
        For Each fila As DataRow In dt.Rows
            If Not IsDBNull(fila("subtotal")) Then ventas += Convert.ToDecimal(fila("subtotal"))
        Next

        CargaVendedorTurnoActivo()
        Dim soloTurno As Boolean = idVendedor <> 0 AndAlso idVendedor = vendedorRegistrado
        Dim egresos = ObtenerEgresosReporte(sucursal, DateTimePicker1.Value, soloTurno)
        lblResumen.Text = TextoResumenEgresos(ventas, egresos)
    End Sub

    Private Sub frmCorteCaja_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ClientSize = New Size(ClientSize.Width, ClientSize.Height + 36)
        lblResumen = CrearLabelResumenEgresos(Me, 160, DataGridView1.Bottom + 6, 800)
        ComboBox1.DataSource = updateCm(sql)
        ComboBox1.DisplayMember = updateCm(sql).Columns(1).ToString
        ComboBox1.ValueMember = updateCm(sql).Columns(0).ToString
        ComboBox1.SelectedIndex = -1

        Dim esAdmin As Boolean = (nombreRol = "ADMINISTRADOR")
        Dim sql2 As String = "SELECT idVendedor, nombre FROM VENDEDOR WHERE estado = 1"
        If esAdmin Then
            sql2 = "SELECT 0 AS idVendedor, '(TODOS)' AS nombre UNION ALL " & sql2 & " ORDER BY idVendedor"
        End If
        Dim dtVendedores As DataTable = updateCm(sql2)
        ComboBox2.DataSource = dtVendedores
        ComboBox2.DisplayMember = dtVendedores.Columns(1).ToString
        ComboBox2.ValueMember = dtVendedores.Columns(0).ToString
        ComboBox2.SelectedIndex = If(esAdmin, 0, -1)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        llenarDTSMP()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        Try
            Dim dt As DataTable
            dt = ds.Tables("dtcortecaja")
            dt.Clear()
            DataGridView1.DataSource = dt
        Catch ex As NullReferenceException
            MessageBox.Show("No se ha elegido ninguna sucursal", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs)
        If DataGridView1.Rows.Count > 0 Then
            Dim informe As New rptCorteCaja

            informe.SetDataSource(ds.Tables("dtcortecaja"))

            frmVerReportes.CrystalReportViewer1.ReportSource = informe
            frmVerReportes.Show()
        Else
            MessageBox.Show("No se eligió ninguna sucursal", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Warning)

        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        llenarDTSMP()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Try
            Dim dt As DataTable
            dt = ds.Tables("dtcortecaja")
            dt.Clear()
            DataGridView1.DataSource = dt
        Catch ex As NullReferenceException
            MessageBox.Show("No se ha elegido ninguna sucursal", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If DataGridView1.Rows.Count > 0 Then
            Try
                Dim informe As New rptCorteCaja

                informe.SetDataSource(ds.Tables("dtcortecaja"))
                informe.SetParameterValue(0, ConsultaParametro("nombreEmpresa"))
                informe.SetParameterValue(1, ConsultaParametro("eslogan"))
                informe.SetParameterValue(2, ObtenerTotalEgresos(sucActual, DateTimePicker1.Value.Date, turnoActivo))

                frmVerReportes.CrystalReportViewer1.ReportSource = informe
                frmVerReportes.Show()
            Catch ex As Exception
                Log.Error($"Ocurrio un error. Error: {ex.Message}")
            End Try

        Else
            MessageBox.Show("No se eligió ninguna sucursal", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Warning)

        End If
    End Sub


End Class