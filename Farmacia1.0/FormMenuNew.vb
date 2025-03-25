Imports Serilog

Public Class FormMenuNew
    Private Sub ToolStripButtonLogin_Click(sender As Object, e As EventArgs) Handles ToolStripButtonLogin.Click
        If ToolStripButtonLogin.Text.ToUpper() = "INICIAR SESIÓN" Then
            frmElegirSucursal.Show()
        Else
            For Each frm As Form In Application.OpenForms.Cast(Of Form).ToList()
                If frm IsNot Me Then
                    frm.Close()
                End If
            Next

            Me.FlowLayoutPanel1.Controls.Clear()

            ToolStripButtonLogin.Text = "Iniciar sesión"
            rolUsuarioActual = Nothing
            nameUsuarioActual = ""
            nombreRol = ""
            usuarioActual = 0
            sucActual = 0
            StatusStripPrincipal.BackColor = Color.Salmon
            ToolStripStatusLabelConnectionStatus.Text = "Estado de la conexión: "
        End If
    End Sub

    Private Sub CatálogoDeProductosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CatálogoDeProductosToolStripMenuItem.Click
        If nombreRol <> "ADMINISTRADOR" Then
            MessageBox.Show("No tiene permisos para este módulo", "No tiene permisos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If
        Log.Information("Ingresando al catálogo de productos")
        frmCatalogoProducto.Show()
    End Sub

    Private Sub ProveedoresToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProveedoresToolStripMenuItem.Click
        If nombreRol <> "ADMINISTRADOR" Then
            MessageBox.Show("No tiene permisos para este módulo", "No tiene permisos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        frmProveedores.Show()
    End Sub

    Private Sub CategoríasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CategoríasToolStripMenuItem.Click
        If nombreRol <> "ADMINISTRADOR" Then
            MessageBox.Show("No tiene permisos para este módulo", "No tiene permisos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        frmCategoria.Show()
    End Sub

    Private Sub UsuariosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UsuariosToolStripMenuItem.Click
        If nombreRol <> "ADMINISTRADOR" Then
            MessageBox.Show("No tiene permisos para este módulo", "No tiene permisos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        frmUsuario.Show()
    End Sub

    Private Sub SucursalesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SucursalesToolStripMenuItem.Click
        If nombreRol = "VENDEDOR" Or nombreRol = "ADMINISTRADOR" Or nombreRol = "BODEGUERO" Then
            MessageBox.Show("Contactar al titular del software para habilitar una sucursal", "Permiso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            frmSucursales.Show()
        End If
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        If nombreRol <> "ADMINISTRADOR" Then
            MessageBox.Show("No tiene permisos para este módulo", "No tiene permisos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        frmCompra.Show()
    End Sub

    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click
        If nombreRol <> "ADMINISTRADOR" Then
            MessageBox.Show("No tiene permisos para este módulo", "No tiene permisos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If
        frmAjustes2.Show()
    End Sub

    Private Sub NuevoTrasladoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NuevoTrasladoToolStripMenuItem.Click
        If rolUsuarioActual = Nothing Then
            MessageBox.Show("No tiene permisos para este módulo", "No tiene permisos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If
        frmTraslados.Show()
    End Sub

    Private Sub VerTrasladosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VerTrasladosToolStripMenuItem.Click
        If nombreRol <> "ADMINISTRADOR" Then
            MessageBox.Show("No tiene permisos para este módulo", "No tiene permisos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If
        frmVerTraslados.Show()
    End Sub

    Private Sub RecibirTrasladoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RecibirTrasladoToolStripMenuItem.Click
        If rolUsuarioActual = Nothing Then
            MessageBox.Show("No tiene permisos para este módulo", "No tiene permisos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        FormRecibirTraslado.Show()
    End Sub

    Private Sub ToolStripMenuItem4_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem4.Click
        If nombreRol <> "ADMINISTRADOR" Then
            MessageBox.Show("No tiene permisos para este módulo", "No tiene permisos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If
        frmVerVentas.Show()
    End Sub

    Private Sub ToolStripMenuItem5_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem5.Click
        If nombreRol <> "ADMINISTRADOR" Then
            MessageBox.Show("No tiene permisos para este módulo", "No tiene permisos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        frmVerVentasElim.Show()
    End Sub

    Private Sub ToolStripMenuItem6_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem6.Click
        If nombreRol <> "ADMINISTRADOR" Then
            MessageBox.Show("No tiene permisos para este módulo", "No tiene permisos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If
        frmInventarioRPT.Show()
    End Sub

    Private Sub ToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem3.Click
        If nombreRol <> "ADMINISTRADOR" Then
            MessageBox.Show("No tiene permisos para este módulo", "No tiene permisos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If
        frmVerVentas.Show()
    End Sub

    Private Sub CorteDeCajaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CorteDeCajaToolStripMenuItem.Click
        If nombreRol <> "ADMINISTRADOR" Then
            MessageBox.Show("No tiene permisos para este módulo", "No tiene permisos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If
        frmCorteCaja.Show()
    End Sub

    Private Sub ToolStripMenuItem7_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem7.Click
        If nombreRol <> "ADMINISTRADOR" Then
            MessageBox.Show("No tiene permisos para este módulo", "No tiene permisos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If
        frmStockMinimo.Show()
    End Sub

    Private Sub ToolStripMenuItem8_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem8.Click
        If nombreRol <> "ADMINISTRADOR" Then
            MessageBox.Show("No tiene permisos para este módulo", "No tiene permisos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If
        frmVerVentasElim.Show()
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        If rolUsuarioActual = Nothing Then
            MessageBox.Show("No tiene permisos para este módulo", "No tiene permisos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        Else
            frmPuntoDeVentaMejorado.Show()
        End If
    End Sub

    Private Sub ToolStripMenuItem9_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem9.Click
        If rolUsuarioActual = Nothing Then
            MessageBox.Show("No tiene permisos para este módulo", "No tiene permisos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        Else
            FormAbrirCaja.Show()
        End If
    End Sub

    Private Sub ToolStripMenuItem10_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem10.Click
        If rolUsuarioActual = Nothing Then
            MessageBox.Show("No tiene permisos para este módulo", "No tiene permisos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        Else
            FormCerrarCaja.Show()
        End If
    End Sub

    Private Sub VerTurnosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VerTurnosToolStripMenuItem.Click
        If rolUsuarioActual = Nothing Then
            MessageBox.Show("No tiene permisos para este módulo", "No tiene permisos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        Else
            FormVerTurnos.Show()
        End If
    End Sub

    Private Sub FormMenuNew_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Estilos.AplicarEstilos(Me)
        Estilos.AplicarEstilosToolStrip(ToolStrip1)
        ToolStripStatusLabelConnectionStatus.Text &= "desconectado"

    End Sub



    Private Sub ReporteDeUtilidadToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReporteDeUtilidadToolStripMenuItem.Click
        If nombreRol <> "ADMINISTRADOR" Then
            MessageBox.Show("No tiene permisos para este módulo", "No tiene permisos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If
        FormReporteUtilidad.Show()
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        If rolUsuarioActual = Nothing Then
            MessageBox.Show("No tiene permisos para este módulo", "No tiene permisos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        Else
            FormEgresos.Show()
        End If
    End Sub

    Private Sub CategoríasEgresosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CategoríasEgresosToolStripMenuItem.Click
        If rolUsuarioActual = Nothing Then
            MessageBox.Show("No tiene permisos para este módulo", "No tiene permisos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        Else
            FormCategoriaEgreso.Show()
        End If
    End Sub
End Class