Public Class frmVerDetElim

    Private Sub frmVerDetElim_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        fillDGVSP("getDetalleVentaElim", DataGridView1, Me, CInt(TextBox1.Text))
    End Sub
End Class