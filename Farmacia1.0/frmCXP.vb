Public Class frmCXP

    Private Sub frmCXP_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        filldgvestandar("sp_vercxp", DataGridView1, Me)
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        Try
            txtncuenta.Text = DataGridView1.CurrentRow.Cells(0).Value
            txtconcep.Text = DataGridView1.CurrentRow.Cells(1).Value
            txtfi.Text = Format(Convert.ToDateTime(DataGridView1.CurrentRow.Cells(2).Value), "dd/MM/yyyy")
            txtfl.Text = Format(Convert.ToDateTime(DataGridView1.CurrentRow.Cells(3).Value), "dd/MM/yyyy")
            txtcodpro.Text = DataGridView1.CurrentRow.Cells(8).Value
            txtrz.Text = DataGridView1.CurrentRow.Cells(9).Value
            txtcompra.Text = DataGridView1.CurrentRow.Cells(11).Value
            txttot.Text = DataGridView1.CurrentRow.Cells(4).Value
            txtsaldo.Text = DataGridView1.CurrentRow.Cells(5).Value
            txtestado.Text = DataGridView1.CurrentRow.Cells(7).Value
            txtsttus.Text = DataGridView1.CurrentRow.Cells(6).Value
            txtnit.Text = DataGridView1.CurrentRow.Cells(10).Value
            txtdias.Text = DateDiff(DateInterval.Day, Convert.ToDateTime(DateTime.Now.ToShortDateString), Convert.ToDateTime(txtfl.Text))
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If MessageBox.Show("¿Desea salir de esta ventana?", "Saliendo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Me.Close()

        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        fillDGVSPDetCxP("sp_verDetallesCxP", frmCuentasxP.DataGridView1, frmCuentasxP, CInt(txtncuenta.Text))
        frmCuentasxP.txtNoCuenta.Text = txtncuenta.Text
        frmCuentasxP.txtEstado.Text = txtestado.Text
        frmCuentasxP.txtSaldo.Text = txtsaldo.Text
        frmCuentasxP.txtTotal.Text = txttot.Text
        frmCuentasxP.Show()

    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        frmListadoGeneralCxP.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        frmVerCuentasXMes.Show()
    End Sub
End Class