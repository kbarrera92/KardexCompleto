Public Class frmCuentasxP

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        filldgvestandar("sp_vercxp", frmCXP.DataGridView1, frmCXP)
        frmCXP.Show()

        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If txtEstado.Text = "SOLVENTE" Then
            MsgBox("La cuenta ha sido pagada en su totalidad")
        Else
            frmAbono.Show()
        End If

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Button2.PerformClick()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    
End Class