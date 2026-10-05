Imports System.Data.SqlClient

''' <summary>
''' Captura la conexión a la base de datos y la guarda cifrada en el archivo de configuración.
''' Se muestra cuando el equipo no tiene cadena o al ejecutar con /conexion.
''' </summary>
Public Class frmConexion

    Private Sub frmConexion_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Estilos.AplicarEstilos(Me)

        Dim actual = ConfigSegura.CadenaActual()
        txtServidor.Text = actual.DataSource
        txtBase.Text = actual.InitialCatalog
        txtUsuario.Text = actual.UserID
    End Sub

    Private Function ConstruyeCadena() As String
        Dim sb As New SqlConnectionStringBuilder() With {
            .DataSource = txtServidor.Text.Trim(),
            .InitialCatalog = txtBase.Text.Trim(),
            .UserID = txtUsuario.Text.Trim(),
            .Password = txtPassword.Text,
            .ConnectTimeout = 5
        }
        Return sb.ConnectionString
    End Function

    Private Function DatosCompletos() As Boolean
        If txtServidor.Text.Trim() = "" OrElse txtBase.Text.Trim() = "" OrElse txtUsuario.Text.Trim() = "" OrElse txtPassword.Text = "" Then
            MessageBox.Show("Complete servidor, base de datos, usuario y contraseña", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If
        Return True
    End Function

    Private Function ProbarConexion(cadena As String) As Boolean
        Try
            Using c As New SqlConnection(cadena)
                c.Open()
            End Using
            Return True
        Catch ex As Exception
            MessageBox.Show("No se pudo conectar: " & ex.Message, "Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    Private Sub btnProbar_Click(sender As Object, e As EventArgs) Handles btnProbar.Click
        If Not DatosCompletos() Then Return
        Cursor = Cursors.WaitCursor
        Try
            If ProbarConexion(ConstruyeCadena()) Then
                MessageBox.Show("Conexión exitosa", "Conexión", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If Not DatosCompletos() Then Return
        Dim cadena = ConstruyeCadena()

        Cursor = Cursors.WaitCursor
        Try
            If Not ProbarConexion(cadena) Then Return
            ConfigSegura.GuardarCadena(cadena)
            DialogResult = DialogResult.OK
        Catch ex As Exception When TypeOf ex Is UnauthorizedAccessException OrElse TypeOf ex Is Configuration.ConfigurationErrorsException
            MessageBox.Show("No se pudo guardar la configuración." & vbCrLf &
                            "Ejecute la aplicación como administrador e inténtelo de nuevo.",
                            "Permisos", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

End Class
