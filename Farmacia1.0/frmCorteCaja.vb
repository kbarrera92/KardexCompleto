Imports System.Data.SqlClient
Public Class frmCorteCaja

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
                .CommandText = "sp_cortecaja"
                .CommandType = CommandType.StoredProcedure
                .Connection = conn
                .Parameters.AddWithValue("suc", CInt(ComboBox1.SelectedValue.ToString))
                .Parameters.AddWithValue("fecha", DateTimePicker1.Value)
                .Parameters.AddWithValue("user", CInt(ComboBox2.SelectedValue.ToString))
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

    Private Sub frmCorteCaja_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBox1.DataSource = updateCm(sql)
        ComboBox1.DisplayMember = updateCm(sql).Columns(1).ToString
        ComboBox1.ValueMember = updateCm(sql).Columns(0).ToString
        ComboBox1.SelectedIndex = -1
    End Sub

    Private Sub ComboBox2_Click(sender As Object, e As EventArgs) Handles ComboBox2.Click
        Dim sql2 As String = "SELECT idUsuario, nombreUsuario FROM USUARIO WHERE sucursal = " & CInt(ComboBox1.SelectedValue.ToString)
        ComboBox2.DataSource = updateCm(sql2)
        ComboBox2.DisplayMember = updateCm(sql2).Columns(1).ToString
        ComboBox2.ValueMember = updateCm(sql2).Columns(0).ToString
        ComboBox2.SelectedIndex = -1
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

                frmVerReportes.CrystalReportViewer1.ReportSource = informe
                frmVerReportes.Show()
            Catch ex As Exception

            End Try

        Else
            MessageBox.Show("No se eligió ninguna sucursal", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Warning)

        End If
    End Sub
End Class