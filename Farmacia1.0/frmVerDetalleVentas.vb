Imports System.Data.SqlClient

Public Class frmVerDetalleVentas

    Sub updateDet()
        Dim sqlupdate As String = "UPDATE DETALLEVENTA SET cantidad = @c, precio = @p, subtotal = @subt WHERE (nDetalleV = @nd AND nVenta = @nv)"
        Dim cmd As SqlCommand
        Try
            cmd = New SqlCommand(sqlupdate, conn)
            cmd.Parameters.AddWithValue("c", CInt(DataGridView1.CurrentRow.Cells(2).Value))
            cmd.Parameters.AddWithValue("p", CDbl(DataGridView1.CurrentRow.Cells(3).Value))
            cmd.Parameters.AddWithValue("subt", CDbl(DataGridView1.CurrentRow.Cells(4).Value))
            cmd.Parameters.AddWithValue("nd", CDbl(DataGridView1.CurrentRow.Cells(0).Value))
            cmd.Parameters.AddWithValue("nv", CInt(TextBox1.Text))

            openConnection()
            cmd.ExecuteNonQuery()
            closeConnection()
            MsgBox("Cambio realizado", MsgBoxStyle.Information, "Éxito")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub frmVerDetalleVentas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If nombreRol = "VENDEDOR" Then
            DataGridView1.Columns(2).ReadOnly = True
            DataGridView1.Columns(3).ReadOnly = True
            DataGridView1.Columns(4).ReadOnly = True
            fillDGVSP("getDetallesVentas", DataGridView1, Me, CInt(TextBox1.Text))
        Else
            fillDGVSP("getDetallesVentas", DataGridView1, Me, CInt(TextBox1.Text))
        End If

    End Sub

    Private Sub DataGridView1_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit

        DataGridView1.CurrentRow.Cells(4).Value = FormatNumber(CDbl(DataGridView1.CurrentRow.Cells(3).Value) * CDbl(DataGridView1.CurrentRow.Cells(2).Value), 2)
        updateDet()
    End Sub

    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.Delete Then
            If MessageBox.Show("¿Desea eliminar este registro?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim sqldeldet As String = "DELETE FROM DETALLEVENTA WHERE (nDetalleV = @nd AND nVenta = @nv)"
                Dim cmd As SqlCommand
                Try
                    cmd = New SqlCommand(sqldeldet, conn)
                    cmd.Parameters.AddWithValue("nd", CInt(DataGridView1.CurrentRow.Cells(0).Value))
                    cmd.Parameters.AddWithValue("nv", CInt(TextBox1.Text))
                    openConnection()
                    cmd.ExecuteNonQuery()
                    closeConnection()
                    MsgBox("Eliminado correctamente", MsgBoxStyle.Information, "Éxito")
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End If
        End If
    End Sub
End Class