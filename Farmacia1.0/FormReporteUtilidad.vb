Imports System.Data.SqlClient

Public Class FormReporteUtilidad
    Dim ds As DataSet
    Private Sub FormReporteUtilidad_Load(sender As Object, e As EventArgs) Handles MyBase.Load

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