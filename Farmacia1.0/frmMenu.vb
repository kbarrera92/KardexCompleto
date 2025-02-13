Public Class frmMenu
    Private Sub frmMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Button1.Text = vbCrLf & "INICIAR SESIÓN"
        Button2.Text = vbCrLf & "NUEVA VENTA"
        Button3.Text = vbCrLf & "ADMINISTRACION"
        Button4.Text = vbCrLf & "NUEVA COMPRA"
        btnTraslados.Text = vbCrLf & "TRASLADOS"
        Button6.Text = vbCrLf & "TRASIEGOS"
        Button7.Text = vbCrLf & "VER PRODUCTOS"
        Button8.Text = vbCrLf & "AJUSTES"
        Button9.Text = vbCrLf & "CUENTAS X PAGAR"
        Button11.Text = vbCrLf & "REPORTES"
        Button12.Text = vbCrLf & "SALIR"
        btnRecibirTraslado.Text = vbCrLf & "RECIBE TRASLADO"
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        frmAdmin.Show()
        Me.Hide()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Button1.Text = vbCrLf & "INICIAR SESIÓN" Then
            frmElegirSucursal.Show()
        Else
            Button1.Text = vbCrLf & "INICIAR SESIÓN"
            rolUsuarioActual = Nothing
            nameUsuarioActual = ""
            nombreRol = ""
            sucActual = 0
            Button2.Enabled = False
            Button3.Enabled = False
            Button4.Enabled = False
            btnTraslados.Enabled = False
            Button6.Enabled = False
            Button7.Enabled = False
            Button8.Enabled = False
            Button9.Enabled = False
            btnRecibirTraslado.Enabled = False
            Button11.Enabled = False
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        frmPuntoDeVentaMejorado.Show()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        frmCompra.Show()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles btnTraslados.Click
        frmTraslados.Show()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        frmTrasiegos2.Show()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        datosreq = 4
        frmProductos.Show()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        frmAjustes2.Show()

    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        If MessageBox.Show("¿Desea cerrar el programa?", "Cerrando", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Application.Exit()
        End If
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        frmCXP.Show()
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles btnRecibirTraslado.Click
        FormRecibirTraslado.Show()
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        frmReportes.Show()
    End Sub
End Class
