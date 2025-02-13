Imports System.Data.SqlClient
Public Class frmVentasDiarias

    Dim ds As DataSet
    Sub calculartotal()
        Dim tot As Double = 0.0
        For i = 0 To DataGridView1.Rows.Count - 1
            tot = tot + DataGridView1.Rows(i).Cells(6).Value
        Next
        TextBox1.Text = FormatNumber(tot, 2)
    End Sub


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
                .Parameters.AddWithValue("suc", sucActual)
                .Parameters.AddWithValue("fecha", DateTimePicker1.Value)
                .Parameters.AddWithValue("user", If(CheckBox1.Checked, DBNull.Value, usuarioActual))
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