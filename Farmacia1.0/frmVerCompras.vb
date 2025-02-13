Imports System.Data.SqlClient

Public Class frmVerCompras
    Dim criterio As String
    Private Sub frmVerCompras_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DataGridView1.AutoGenerateColumns = False
        filldgvestandar("getCompras", DataGridView1, Me)
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If RadioButton1.Checked = True Then
            criterio = "rzProveedor"
        ElseIf RadioButton2.Checked = True Then
            criterio = "documento"
        ElseIf RadioButton3.Checked = True Then
            criterio = "nombreUsuario"
        ElseIf RadioButton4.Checked = True Then
            criterio = "nCompra"
        End If
        Try
            dv.RowFilter = String.Format("Convert(" & criterio & ", 'System.String') LIKE '%{0}%'", Trim(TextBox1.Text))
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If DataGridView1.SelectedRows.Count = 0 Then
            MsgBox("No se ha seleccionado ninguna compra", MsgBoxStyle.Exclamation, "Error")
        Else
            If MessageBox.Show("¿Desea anular esta compra? Se eliminarán todos los datos asociados a ella", "Anular", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim sqldel As String = "DELETE FROM COMPRA WHERE nCompra = @nc"
                Dim cmd As SqlCommand
                Try
                    cmd = New SqlCommand(sqldel, conn)
                    cmd.Parameters.AddWithValue("nc", CInt(DataGridView1.CurrentRow.Cells(0).Value))
                    openConnection()
                    cmd.ExecuteNonQuery()
                    closeConnection()
                    MessageBox.Show("Compra anulada. Se borraron todos los datos de ella.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    filldgvestandar("getCompras", DataGridView1, Me)
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End If
        End If
        
    End Sub

    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.Enter Then
            If DataGridView1.SelectedRows.Count = 0 Then
                MsgBox("No se ha seleccionado ninguna compra", MsgBoxStyle.Exclamation, "Error")
            Else
                frmVerDetalleCompra.TextBox1.Text = DataGridView1.CurrentRow.Cells(0).Value
                frmVerDetalleCompra.Show()
            End If

            e.SuppressKeyPress = True
        End If
    End Sub
End Class