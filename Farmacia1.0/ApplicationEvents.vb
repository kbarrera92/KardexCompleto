Imports Serilog

Namespace My
    ' The following events are available for MyApplication:
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active. 
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.
    Partial Friend Class MyApplication
        Private Sub MyApplication_Startup(sender As Object, e As Microsoft.VisualBasic.ApplicationServices.StartupEventArgs) Handles Me.Startup
            Serilog.Log.Logger = New LoggerConfiguration() _
                .WriteTo.File("logs/app.log", rollingInterval:=Serilog.RollingInterval.Day) _
                .CreateLogger()

            Serilog.Log.Information("Iniciando aplicación")

            ' La configuración de usuario (impresora) va por versión: se trae la de la versión anterior.
            If My.Settings.actualizarConfiguracion Then
                My.Settings.Upgrade()
                My.Settings.actualizarConfiguracion = False
                My.Settings.Save()
            End If

            ' Actualización automática: si hay versión nueva se aplica y se reinicia (/actualizado evita bucles).
            Actualizador.LimpiarArchivosViejos()
            If Not e.CommandLine.Contains("/actualizado") AndAlso Actualizador.BuscarYAplicar() Then
                ConfigSegura.Reiniciar(e.CommandLine.Concat({"/actualizado"}))
                e.Cancel = True
                Return
            End If

            ' Equipos con la cadena en el .exe.config: se pasa a la configuración de la terminal (%ProgramData%).
            ConfigSegura.MigrarDesdeConfig()

            If Not ConfigSegura.HayCadena() OrElse e.CommandLine.Contains("/conexion") Then
                Using f As New frmConexion()
                    If f.ShowDialog() = DialogResult.OK Then
                        ConfigSegura.Reiniciar(e.CommandLine)
                    End If
                End Using
                e.Cancel = True
            End If
        End Sub

        Private Sub MyApplication_Shutdown(sender As Object, e As EventArgs) Handles Me.Shutdown
            Serilog.Log.Information("Aplicación cerrándose...")
            Serilog.Log.CloseAndFlush()
        End Sub
    End Class
End Namespace
