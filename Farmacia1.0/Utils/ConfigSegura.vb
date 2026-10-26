Imports System.Configuration
Imports System.IO
Imports System.Security.Cryptography
Imports System.Text
Imports Serilog

''' <summary>
''' Guarda la configuración propia de la terminal (cadena de conexión y código de sucursal) en
''' %ProgramData%\FarmaciAhorro\terminal.xml, fuera de la carpeta del programa, para que una
''' actualización o un reemplazo de archivos no la borre. La cadena va cifrada con DPAPI (ámbito equipo):
''' la clave la administra Windows, por lo que el archivo copiado a otro equipo no se puede leer.
''' </summary>
Module ConfigSegura

    Private Const NombreCadena As String = "IS_PRO2CS"
    Private ReadOnly Entropia As Byte() = Encoding.UTF8.GetBytes("FarmaciAhorro.terminal")

    Private cargado As Boolean
    Private cadenaGuardada As String = ""
    Private sucursalGuardada As String = ""

    Private ReadOnly Property RutaTerminal As String
        Get
            Return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "FarmaciAhorro", "terminal.xml")
        End Get
    End Property

    ''' <summary>Lee y descifra terminal.xml una sola vez. Si no existe o no se puede leer, queda vacío.</summary>
    Private Sub Cargar()
        If cargado Then Return
        cadenaGuardada = ""
        sucursalGuardada = ""
        Try
            If File.Exists(RutaTerminal) Then
                Dim raiz = XDocument.Load(RutaTerminal).Root
                sucursalGuardada = If(CStr(raiz.Attribute("codigoSucursal")), "")
                Dim cifrada = CStr(raiz.Element("conexion"))
                If Not String.IsNullOrWhiteSpace(cifrada) Then
                    Dim bytes = ProtectedData.Unprotect(Convert.FromBase64String(cifrada), Entropia, DataProtectionScope.LocalMachine)
                    cadenaGuardada = Encoding.UTF8.GetString(bytes)
                End If
            End If
        Catch ex As Exception When TypeOf ex Is IOException OrElse TypeOf ex Is UnauthorizedAccessException OrElse
                                   TypeOf ex Is System.Xml.XmlException OrElse TypeOf ex Is CryptographicException OrElse
                                   TypeOf ex Is FormatException
            Log.Warning(ex, "No se pudo leer la configuración de la terminal")
        End Try
        cargado = True
    End Sub

    ''' <summary>Cadena de conexión de la terminal; vacía si no hay.</summary>
    Function Cadena() As String
        Cargar()
        Return cadenaGuardada
    End Function

    ''' <summary>Código de sucursal de la terminal; vacío si no hay (se usa entonces appSettings).</summary>
    Function CodigoSucursal() As String
        Cargar()
        Return sucursalGuardada
    End Function

    Function HayCadena() As Boolean
        Return Not String.IsNullOrWhiteSpace(Cadena())
    End Function

    ''' <summary>Cadena actual sin la contraseña, para precargar la pantalla de conexión.</summary>
    Function CadenaActual() As SqlClient.SqlConnectionStringBuilder
        Dim sb As New SqlClient.SqlConnectionStringBuilder()
        Try
            If HayCadena() Then sb.ConnectionString = Cadena()
        Catch ex As ArgumentException
            Log.Warning("La cadena de conexión actual no tiene un formato válido")
        End Try
        Return sb
    End Function

    ''' <summary>
    ''' Guarda la cadena ya cifrada: nunca se escribe en texto plano. Propaga errores de permisos.
    ''' Después de guardar hay que llamar a Reiniciar.
    ''' </summary>
    Sub Guardar(cadena As String, codigoSucursal As String)
        Dim ruta = RutaTerminal
        Directory.CreateDirectory(Path.GetDirectoryName(ruta))

        Dim cifrada = Convert.ToBase64String(ProtectedData.Protect(Encoding.UTF8.GetBytes(cadena), Entropia, DataProtectionScope.LocalMachine))
        Dim doc As New XDocument(New XElement("terminal",
                                              New XAttribute("codigoSucursal", If(codigoSucursal, "")),
                                              New XElement("conexion", cifrada)))

        ' Se escribe a un temporal para no dejar un archivo a medias si falla.
        Dim temporal = ruta & ".tmp"
        doc.Save(temporal)
        If File.Exists(ruta) Then
            File.Replace(temporal, ruta, Nothing)
        Else
            File.Move(temporal, ruta)
        End If

        cargado = False
        Log.Information("Configuración de la terminal guardada")
    End Sub

    ''' <summary>
    ''' Pasa la cadena y el código de sucursal del .exe.config (cifrada o en claro) al almacén de la terminal,
    ''' solo si el almacén aún no tiene cadena. No bloquea el arranque si falla ni requiere reiniciar.
    ''' Devuelve True si migró.
    ''' </summary>
    Function MigrarDesdeConfig() As Boolean
        Try
            If HayCadena() Then Return False

            Dim cs = ConfigurationManager.ConnectionStrings(NombreCadena)
            If cs Is Nothing OrElse String.IsNullOrWhiteSpace(cs.ConnectionString) Then Return False

            Guardar(cs.ConnectionString, ConfigurationManager.AppSettings("codigoSucursal"))
            If Cadena() <> cs.ConnectionString Then
                Log.Warning("La configuración migrada no coincide con la original")
                Return False
            End If
            Log.Information("Configuración de la terminal migrada desde el archivo de configuración")
        Catch ex As Exception When TypeOf ex Is UnauthorizedAccessException OrElse TypeOf ex Is IOException OrElse
                                   TypeOf ex Is ConfigurationErrorsException OrElse TypeOf ex Is CryptographicException
            Log.Warning(ex, "No se pudo migrar la configuración de la terminal")
            MessageBox.Show("No se pudo guardar la configuración de la terminal." & vbCrLf &
                            "Ejecute la aplicación una vez como administrador.",
                            "Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End Try

        QuitarCadenaDelConfig()
        Return True
    End Function

    ''' <summary>Quita la cadena del .exe.config una vez migrada. Si no se puede, solo se registra.</summary>
    Private Sub QuitarCadenaDelConfig()
        Try
            Dim config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
            Dim seccion = config.ConnectionStrings
            If seccion.ConnectionStrings(NombreCadena) Is Nothing Then Return

            seccion.ConnectionStrings.Remove(NombreCadena)
            If seccion.SectionInformation.IsProtected Then seccion.SectionInformation.UnprotectSection()
            seccion.SectionInformation.ForceSave = True
            config.Save(ConfigurationSaveMode.Modified)
        Catch ex As Exception When TypeOf ex Is UnauthorizedAccessException OrElse TypeOf ex Is ConfigurationErrorsException
            Log.Warning(ex, "No se pudo quitar la cadena de conexión del archivo de configuración")
        End Try
    End Sub

    ''' <summary>Inicia una nueva instancia sin /conexion para que lea la configuración desde cero.</summary>
    Sub Reiniciar(argumentos As IEnumerable(Of String))
        Dim sinConexion = argumentos.Where(Function(a) Not a.Equals("/conexion", StringComparison.OrdinalIgnoreCase))
        Process.Start(Application.ExecutablePath, String.Join(" ", sinConexion.Select(Function(a) """" & a & """")))
    End Sub

End Module
