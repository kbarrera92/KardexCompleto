Imports System.Data.SqlClient
Imports Serilog

Public Class FormReporteUtilidad
    Dim ds As DataSet
    Dim sqlSucursal As String = "SELECT idSucursal, nombreSuc FROM SUCURSAL"
    Function updateList(ByVal sql As String) As DataTable
        Dim da As SqlDataAdapter
        Dim dt As New DataTable

        Try
            openConnection()
            da = New SqlDataAdapter(sql, conn)
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            MsgBox(ex.Message)
            Return Nothing
        End Try
    End Function
    Private Sub FormReporteUtilidad_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Log.Information("Cargando sucursales en formulario de Reporte de utilidades")
        Try
            ComboBox1.DataSource = updateList(sqlSucursal)
            ComboBox1.ValueMember = updateList(sqlSucursal).Columns(0).ToString
            ComboBox1.DisplayMember = updateList(sqlSucursal).Columns(1).ToString

        Catch ex As Exception
            Log.Error($"Ocurrio un error. Error: {ex.Message}")
        End Try
    End Sub

    Sub llenarDTSMP2()
        Dim cmd As SqlCommand
        Dim da As SqlDataAdapter
        Dim dt As DataTable
        ds = New dsReportes

        Try
            openConnection()
            cmd = New SqlCommand()
            With cmd
                .CommandText = "sp_muestraUtilidad"
                .CommandType = CommandType.StoredProcedure
                .Connection = conn
                .Parameters.AddWithValue("fechainicial", DateTimePicker1.Value)
                .Parameters.AddWithValue("fechafinal", DateTimePicker2.Value)
                .Parameters.AddWithValue("sucursal", Integer.Parse(ComboBox1.SelectedValue.ToString()))
            End With
            dt = ds.Tables("dtutilidad")

            da = New SqlDataAdapter(cmd)
            da.FillSchema(ds.Tables("dtutilidad"), SchemaType.Source)
            da.Fill(ds.Tables("dtutilidad"))
        Catch ex As Exception
            MessageBox.Show("Error al cargar los datos" & vbCrLf & "Error: " & ex.ToString)
        Finally
            closeConnection()

        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            llenarDTSMP2()
            Dim informe As New rptUtilidad

            informe.SetDataSource(ds.Tables("dtutilidad"))
            informe.SetParameterValue("fechainicio", DateTimePicker1.Value)
            informe.SetParameterValue("fechafin", DateTimePicker2.Value)

            frmVerReportes.CrystalReportViewer1.ReportSource = informe
            frmVerReportes.Show()
        Catch ex As Exception
            MessageBox.Show($"Error al cargar el reporte. Error: {ex.Message}")
        End Try
    End Sub
End Class