Imports System.Data.SqlClient

Public Class frmKardexMov
    Dim sqlSuc As String = "SELECT idSucursal, nombreSuc FROM SUCURSAL"
    

    Private Sub frmKardexMov_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBox1.DataSource = updateCm(sqlSuc)
        ComboBox1.ValueMember = updateCm(sqlSuc).Columns(0).ToString
        ComboBox1.DisplayMember = updateCm(sqlSuc).Columns(1).ToString
        DataGridView1.AutoGenerateColumns = False
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TextBox3.Clear()
        datosreq = 4
        frmProductos.Show()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim cmd As SqlCommand
        Dim da As SqlDataAdapter
        Dim dt As New DataTable

        Try
            openConnection()
            cmd = New SqlCommand()

            With cmd
                .CommandText = "sp_ventasxproducto"
                .CommandType = CommandType.StoredProcedure
                .Connection = conn
                .Parameters.AddWithValue("suc", CInt(ComboBox1.SelectedValue.ToString))
                .Parameters.AddWithValue("pro", CInt(TextBox1.Text))
                .Parameters.AddWithValue("fini", DateTimePicker1.Value)
                .Parameters.AddWithValue("ff", DateTimePicker2.Value)
            End With

            da = New SqlDataAdapter(cmd)
            da.Fill(dt)

            For i = 0 To dt.Columns.Count - 1
                DataGridView1.Columns(i).DataPropertyName = dt.Columns(i).ToString
            Next

            dv = dt.DefaultView
            DataGridView1.DataSource = dv
            TextBox3.Text = getStock(CInt(ComboBox1.SelectedValue), CInt(TextBox1.Text), "sp_getStoc")
            Label6.Text = "Total de productos vendidos: " & calcCantidad()
            Label7.Text = "Valor total: " & FormatNumber(calcSubt(), 2)
        Catch ex As Exception
            MsgBox("Error al cargar los datos" & vbCrLf & "Error: " & ex.ToString)
        Finally
            closeConnection()
        End Try

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        
        DataGridView1.DataSource = Nothing
        DataGridView1.Rows.Clear()
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        DateTimePicker1.Value = "20/10/2019"
        DateTimePicker2.Value = DateTime.Today
    End Sub

    Function calcCantidad() As Integer
        Dim cant As Integer = 0
        For i = 0 To DataGridView1.Rows.Count - 1
            cant = cant + DataGridView1.Rows(i).Cells(2).Value
        Next
        Return cant
    End Function
    Function calcSubt()
        Dim subtotal As Double = 0.0
        For i = 0 To DataGridView1.Rows.Count - 1
            subtotal = subtotal + DataGridView1.Rows(i).Cells(3).Value
        Next
        Return subtotal
    End Function
End Class