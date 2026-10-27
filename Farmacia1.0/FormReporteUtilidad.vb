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
            CargarSucursales()
        Catch ex As Exception
            Log.Error($"Ocurrio un error. Error: {ex.Message}")
        End Try
    End Sub

    Private Sub CargarSucursales()
        Dim dtSuc As DataTable = updateList(sqlSucursal)

        Dim filaTodas As DataRow = dtSuc.NewRow()
        filaTodas("idSucursal") = 0
        filaTodas("nombreSuc") = "-- Todas las sucursales --"
        dtSuc.Rows.InsertAt(filaTodas, 0)

        ComboBox1.DataSource = dtSuc
        ComboBox1.ValueMember = "idSucursal"
        ComboBox1.DisplayMember = "nombreSuc"
        ComboBox1.SelectedIndex = 0
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

                If Convert.ToInt32(ComboBox1.SelectedValue) = 0 Then
                    .Parameters.AddWithValue("sucursal", DBNull.Value)
                Else
                    .Parameters.AddWithValue("sucursal", Convert.ToInt32(ComboBox1.SelectedValue))
                End If
            End With
            dt = ds.Tables("dtutilidad")

            da = New SqlDataAdapter(cmd)
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