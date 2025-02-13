Imports System.Data.SqlClient
Public Class frmElegirSucursal


    Function updateList(ByVal sql As String) As DataTable
        Dim da As SqlDataAdapter
        Dim dt As New DataTable
        Dim comando As New SqlCommand()
        With comando
            .CommandText = sql
            .CommandType = CommandType.Text
            .Connection = conn
            .Parameters.AddWithValue("idSuc", ConsultaParametro("codigoSucursal"))
        End With

        Try
            openConnection()
            da = New SqlDataAdapter(comando)
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            MsgBox(ex.Message)
            Return Nothing
        End Try
    End Function

    Private Sub frmElegirSucursal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim sqlSucursal As String = "SELECT idSucursal, nombreSuc FROM SUCURSAL where idSucursal = @idSuc"

        Try
            ListBox1.DataSource = updateList(sqlSucursal)
            ListBox1.ValueMember = updateList(sqlSucursal).Columns(0).ToString
            ListBox1.DisplayMember = updateList(sqlSucursal).Columns(1).ToString
        Catch ex As Exception

        End Try
        
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ListBox1.SelectedItems.Count = 0 Then
            MessageBox.Show("Debe seleccionar una sucursal", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        sucActual = CInt(ListBox1.SelectedValue.ToString)
        nameSucActual = ListBox1.SelectedItem(1).ToString()
        Form1.Show()
        Me.Close()
    End Sub
End Class