Imports System.Configuration
Imports Serilog

''' <summary>
''' Mantiene la cadena de conexión cifrada en el .exe.config con DPAPI (ámbito equipo).
''' La clave la administra Windows, por lo que el archivo copiado a otro equipo no se puede leer.
''' </summary>
Module ConfigSegura

    Private Const NombreCadena As String = "IS_PRO2CS"
    Private Const Proveedor As String = "DataProtectionConfigurationProvider"

    Function HayCadena() As Boolean
        Dim cs = ConfigurationManager.ConnectionStrings(NombreCadena)
        Return cs IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(cs.ConnectionString)
    End Function

    ''' <summary>Cadena actual sin la contraseña, para precargar la pantalla de conexión.</summary>
    Function CadenaActual() As SqlClient.SqlConnectionStringBuilder
        Dim sb As New SqlClient.SqlConnectionStringBuilder()
        Try
            If HayCadena() Then sb.ConnectionString = ConfigurationManager.ConnectionStrings(NombreCadena).ConnectionString
        Catch ex As ArgumentException
            Log.Warning("La cadena de conexión actual no tiene un formato válido")
        End Try
        Return sb
    End Function

    ''' <summary>
    ''' Migra una cadena en texto plano a formato cifrado. No bloquea el arranque si falla.
    ''' Devuelve True si modificó el archivo: el proceso debe reiniciarse, porque tras guardar la sección
    ''' cifrada ConfigurationManager no puede releerla dentro del mismo proceso.
    ''' </summary>
    Function ProtegerSiEstaEnClaro() As Boolean
        Try
            Dim config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
            Dim seccion = config.ConnectionStrings
            If seccion.SectionInformation.IsProtected OrElse seccion.ConnectionStrings(NombreCadena) Is Nothing Then Return False

            seccion.SectionInformation.ProtectSection(Proveedor)
            seccion.SectionInformation.ForceSave = True
            config.Save(ConfigurationSaveMode.Modified)
            Log.Information("Cadena de conexión protegida en el archivo de configuración")
            Return True
        Catch ex As Exception When TypeOf ex Is UnauthorizedAccessException OrElse TypeOf ex Is ConfigurationErrorsException
            Log.Warning(ex, "No se pudo proteger la cadena de conexión")
            MessageBox.Show("No se pudo cifrar la cadena de conexión del archivo de configuración." & vbCrLf &
                            "Ejecute la aplicación una vez como administrador.",
                            "Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Guarda la cadena ya cifrada: nunca se escribe en texto plano. Propaga errores de permisos.
    ''' Después de guardar hay que llamar a Reiniciar.
    ''' </summary>
    Sub GuardarCadena(cadena As String)
        Dim config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
        Dim seccion = config.ConnectionStrings

        seccion.ConnectionStrings.Remove(NombreCadena)
        seccion.ConnectionStrings.Add(New ConnectionStringSettings(NombreCadena, cadena, "System.Data.SqlClient"))
        If Not seccion.SectionInformation.IsProtected Then seccion.SectionInformation.ProtectSection(Proveedor)
        seccion.SectionInformation.ForceSave = True
        config.Save(ConfigurationSaveMode.Modified)
        Log.Information("Cadena de conexión actualizada")
    End Sub

    ''' <summary>Inicia una nueva instancia sin /conexion para que lea el config cifrado desde cero.</summary>
    Sub Reiniciar(argumentos As IEnumerable(Of String))
        Dim sinConexion = argumentos.Where(Function(a) Not a.Equals("/conexion", StringComparison.OrdinalIgnoreCase))
        Process.Start(Application.ExecutablePath, String.Join(" ", sinConexion.Select(Function(a) """" & a & """")))
    End Sub

End Module
