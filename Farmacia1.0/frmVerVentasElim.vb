Public Class frmVerVentasElim

    Private Sub frmVerVentasElim_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        filldgvestandar("verVentasEliminadas", DataGridView1, Me)
    End Sub

    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.Enter Then
            frmVerDetElim.TextBox1.Text = DataGridView1.CurrentRow.Cells(0).Value
            frmVerDetElim.Show()

            e.SuppressKeyPress = True
        End If
    End Sub
End Class