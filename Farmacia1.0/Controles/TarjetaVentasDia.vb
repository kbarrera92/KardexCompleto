Imports System.Data.SqlClient
Imports Serilog

Public Class TarjetaVentasDia

    Public Property CantidadTotal As Decimal
    Public Property AccionAlHacerClick As Action

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        AddHandler LinkLabel1.LinkClicked, AddressOf LinkLabelDetalles_LinkClicked
    End Sub

    Public Sub CargarVentas(ByVal query As String, ByVal titulo As String)
        Try
            openConnection()
            Using cmd As New SqlCommand(query, conn)
                Dim result As Object = cmd.ExecuteScalar()
                CantidadTotal = If(IsDBNull(result), 0, Convert.ToDecimal(result)) ' Evitar NULL
                LabelCantidad.Text = String.Format("Q {0:N2}", CantidadTotal)
                LabelTitulo.Text = titulo
                LabelFecha.Text = Date.Now.ToShortDateString()

            End Using
            closeConnection()
        Catch ex As Exception
            Log.Error($"Ocurrio un error. Error: {ex.Message}")
            MessageBox.Show("Error al obtener ventas. Revise el log del sistema", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LinkLabelDetalles_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)
        ' Disparar el evento personalizado
        AccionAlHacerClick?.Invoke()
    End Sub


End Class
