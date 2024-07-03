Imports System.Data.SqlClient

Public Class frmVerCuentasXMes
    Dim ds As DataSet
    Sub llebarcmbanio()
        For i = 2019 To DateTime.Now.Year
            ComboBox1.Items.Add(i)
        Next
    End Sub

    Sub calculartotal()
        Dim tot As Double = 0.0
        For i = 0 To DataGridView1.Rows.Count - 1
            tot = tot + DataGridView1.Rows(i).Cells(4).Value
        Next
        TextBox1.Text = FormatNumber(tot, 2)
    End Sub

    Sub calculartotalS()
        Dim tot As Double = 0.0
        For i = 0 To DataGridView1.Rows.Count - 1
            tot = tot + DataGridView1.Rows(i).Cells(5).Value
        Next
        TextBox2.Text = FormatNumber(tot, 2)
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
                .CommandText = "sp_vercxpxmes"
                .CommandType = CommandType.StoredProcedure
                .Connection = conn

                .Parameters.AddWithValue("mes", (ComboBox2.SelectedIndex + 1))
                .Parameters.AddWithValue("anio", CInt(ComboBox1.Text))

            End With
            dt = New DataTable

            da = New SqlDataAdapter(cmd)
            da.FillSchema(dt, SchemaType.Source)
            da.Fill(dt)

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

    Private Sub frmVerCuentasXMes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        llebarcmbanio()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            llenarDTSMP()
            calculartotal()
            calculartotalS()
        Catch ex As Exception
            MessageBox.Show("Faltan datos obligatorioa", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
End Class