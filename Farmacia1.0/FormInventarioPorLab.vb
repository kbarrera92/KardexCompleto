Imports System.Data.SqlClient

Public Class FormInventarioPorLab

    Private sqlSucursal As String = String.Format("EXEC spExistencias @OPCION = {0}", 1)
    Private Sub FormInventarioPorLab_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ListBox1.DataSource = updateList(sqlSucursal)
        ListBox1.ValueMember = updateList(sqlSucursal).Columns(0).ToString
        ListBox1.DisplayMember = updateList(sqlSucursal).Columns(0).ToString
    End Sub

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
End Class