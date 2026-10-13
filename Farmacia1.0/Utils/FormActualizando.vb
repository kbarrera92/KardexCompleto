''' <summary>Ventana mínima de progreso mostrada mientras se descarga una actualización.</summary>
Friend Class FormActualizando
    Inherits Form

    Private ReadOnly lblEstado As New Label()
    Private ReadOnly barra As New ProgressBar()

    Public Sub New()
        Text = "Actualizando"
        FormBorderStyle = FormBorderStyle.FixedDialog
        StartPosition = FormStartPosition.CenterScreen
        ControlBox = False
        ShowInTaskbar = True
        ClientSize = New Size(380, 80)

        lblEstado.AutoSize = False
        lblEstado.SetBounds(12, 10, 356, 20)
        lblEstado.Text = "Buscando actualización..."
        barra.SetBounds(12, 40, 356, 22)

        Controls.Add(lblEstado)
        Controls.Add(barra)
    End Sub

    Public Sub Mostrar(texto As String, Optional porcentaje As Integer = -1)
        lblEstado.Text = texto
        If porcentaje >= 0 Then
            barra.Style = ProgressBarStyle.Continuous
            barra.Value = Math.Min(100, porcentaje)
        Else
            barra.Style = ProgressBarStyle.Marquee
        End If
        Application.DoEvents()
    End Sub

End Class
