Imports System.Data.SqlClient
Public Class frmStockMinimo
    Dim sql As String = "SELECT idSucursal, nombreSuc FROM SUCURSAL"
    Dim ds As DataSet
    Sub llenarDTSMP()
        Dim cmd As SqlCommand
        Dim da As SqlDataAdapter
        Dim dt As DataTable
        ds = New dsReportes

        Try
            openConnection()
            cmd = New SqlCommand()
            With cmd
                .CommandText = "verprodsstockmin"
                .CommandType = CommandType.StoredProcedure
                .Connection = conn
                .Parameters.AddWithValue("suc", CInt(ComboBox1.SelectedValue.ToString))

            End With
            dt = ds.Tables("dtstockmin")

            da = New SqlDataAdapter(cmd)
            da.FillSchema(ds.Tables("dtstockmin"), SchemaType.Source)
            da.Fill(ds.Tables("dtstockmin"))
            mostrarRPT()
        Catch ex As NullReferenceException
            MessageBox.Show("No se ha elegido ninguna sucursal", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Catch ex As Exception
            MessageBox.Show("Error al cargar los datos" & vbCrLf & "Error: " & ex.ToString)
        Finally
            closeConnection()

        End Try
    End Sub

    Sub mostrarRPT()
        Try
            Dim informe As New rptStockMin

            informe.SetDataSource(ds.Tables("dtstockmin"))

            frmVerReportes.CrystalReportViewer1.ReportSource = informe
            frmVerReportes.Show()
        Catch ex As Exception
            MessageBox.Show("No se eligió ninguna sucursal", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try

    End Sub
    Private Sub frmStockMinimo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBox1.DataSource = updateCm(sql)
        ComboBox1.DisplayMember = updateCm(sql).Columns(1).ToString
        ComboBox1.ValueMember = updateCm(sql).Columns(0).ToString
        ComboBox1.SelectedIndex = -1
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        llenarDTSMP()
    End Sub
End Class