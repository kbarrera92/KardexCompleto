Imports System.Configuration
Imports System.IO
Imports System.IO.Compression
Imports System.Net
Imports System.Security.Cryptography
Imports Serilog

''' <summary>
''' Actualización automática al iniciar: lee version.xml del hosting por HTTPS, descarga el zip,
''' verifica SHA256 y reemplaza los archivos del programa. Un exe en uso se puede renombrar en Windows,
''' así que los archivos reemplazados pasan a *.old (se borran en el siguiente arranque).
''' Nunca toca la cadena de conexión cifrada del .exe.config ni logs\ ni Factura.txt.
''' </summary>
Module Actualizador

    ' Carpeta pública del hosting. Se puede sobrescribir con appSettings "urlActualizaciones".
    Private Const UrlPorDefecto As String = "https://isprodev.xyz/farmacia/"
    Private Const ArchivoManifiesto As String = "version.xml"
    Private Const TimeoutMs As Integer = 5000

    Private ReadOnly Protegidos As String() = {"Factura.txt"}

    Private ReadOnly Property CarpetaApp As String
        Get
            Return AppDomain.CurrentDomain.BaseDirectory
        End Get
    End Property

    Private Function UrlBase() As String
        Dim url = ConfigurationManager.AppSettings("urlActualizaciones")
        If String.IsNullOrWhiteSpace(url) Then url = UrlPorDefecto
        If Not url.EndsWith("/") Then url &= "/"
        Return url
    End Function

    Sub LimpiarArchivosViejos()
        Try
            For Each f In Directory.EnumerateFiles(CarpetaApp, "*.old", SearchOption.AllDirectories)
                Try
                    File.Delete(f)
                Catch ex As IOException
                    ' Sigue en uso; se intentará en el próximo arranque.
                End Try
            Next
        Catch ex As Exception When TypeOf ex Is IOException OrElse TypeOf ex Is UnauthorizedAccessException
            Log.Warning(ex, "No se pudieron limpiar archivos .old")
        End Try
    End Sub

    ''' <summary>True si se aplicó una actualización y hay que reiniciar. Cualquier fallo deja la versión actual.</summary>
    Function BuscarYAplicar() As Boolean
        Dim url = UrlBase()
        If url.Contains("CAMBIAR-DOMINIO") Then
            Log.Information("Actualizaciones sin configurar (urlActualizaciones)")
            Return False
        End If

        Dim ventana As FormActualizando = Nothing
        Dim tempDir = Path.Combine(Path.GetTempPath(), "FarmaciaUpdate")
        Try
            ServicePointManager.SecurityProtocol = ServicePointManager.SecurityProtocol Or SecurityProtocolType.Tls12

            Dim manifiesto As XElement
            Try
                manifiesto = XElement.Parse(DescargarTexto(url & ArchivoManifiesto))
            Catch ex As Exception When TypeOf ex Is WebException OrElse TypeOf ex Is Xml.XmlException
                Log.Warning(ex, "No se pudo leer el manifiesto de actualización")
                Return False
            End Try

            Dim versionNueva As New Version(CStr(manifiesto.Attribute("version")))
            If versionNueva <= My.Application.Info.Version Then Return False

            Dim archivo = Path.GetFileName(CStr(manifiesto.Attribute("archivo")))
            Dim sha = CStr(manifiesto.Attribute("sha256"))
            Log.Information("Actualizando de {Actual} a {Nueva}", My.Application.Info.Version, versionNueva)

            ventana = New FormActualizando()
            ventana.Show()
            ventana.Mostrar("Descargando versión " & versionNueva.ToString() & "...", 0)

            If Directory.Exists(tempDir) Then Directory.Delete(tempDir, True)
            Directory.CreateDirectory(tempDir)
            Dim zipLocal = Path.Combine(tempDir, archivo)
            Descargar(url & archivo, zipLocal, ventana)

            ventana.Mostrar("Verificando archivos...")
            If Not String.Equals(CalcularSha256(zipLocal), sha, StringComparison.OrdinalIgnoreCase) Then
                Log.Error("SHA256 de la actualización no coincide; se cancela")
                Return False
            End If

            Dim staging = Path.Combine(tempDir, "contenido")
            ZipFile.ExtractToDirectory(zipLocal, staging)

            ventana.Mostrar("Instalando...")
            Return Aplicar(staging)
        Catch ex As Exception
            Log.Error(ex, "Falló la actualización automática")
            Return False
        Finally
            If ventana IsNot Nothing Then ventana.Close()
            Try
                If Directory.Exists(tempDir) Then Directory.Delete(tempDir, True)
            Catch ex As Exception When TypeOf ex Is IOException OrElse TypeOf ex Is UnauthorizedAccessException
            End Try
        End Try
    End Function

    Private Function EsProtegido(relativa As String) As Boolean
        If relativa.EndsWith(".exe.config", StringComparison.OrdinalIgnoreCase) Then Return True
        If relativa.StartsWith("logs" & Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase) Then Return True
        Return Protegidos.Contains(relativa, StringComparer.OrdinalIgnoreCase)
    End Function

    ''' <summary>Reemplaza archivos con rollback completo si algo falla a mitad.</summary>
    Private Function Aplicar(staging As String) As Boolean
        Dim configLocal = Application.ExecutablePath & ".config"
        Dim configOriginal = If(File.Exists(configLocal), File.ReadAllBytes(configLocal), Nothing)
        Dim renombrados As New List(Of Tuple(Of String, String)) ' (destino, .old)
        Dim creados As New List(Of String)

        Try
            For Each origen In Directory.EnumerateFiles(staging, "*", SearchOption.AllDirectories)
                Dim relativa = origen.Substring(staging.Length).TrimStart(Path.DirectorySeparatorChar)
                If EsProtegido(relativa) Then Continue For

                Dim destino = Path.Combine(CarpetaApp, relativa)
                Directory.CreateDirectory(Path.GetDirectoryName(destino))

                If File.Exists(destino) Then
                    Dim viejo = destino & ".old"
                    If File.Exists(viejo) Then File.Delete(viejo)
                    File.Move(destino, viejo)
                    renombrados.Add(Tuple.Create(destino, viejo))
                Else
                    creados.Add(destino)
                End If
                File.Copy(origen, destino, True)
            Next

            Dim configNuevo = Directory.EnumerateFiles(staging, "*.exe.config", SearchOption.TopDirectoryOnly).FirstOrDefault()
            If configNuevo IsNot Nothing Then MezclarConfig(configLocal, configNuevo)

            Log.Information("Actualización aplicada")
            Return True
        Catch ex As Exception
            Log.Error(ex, "Error al aplicar la actualización; se revierte")
            For Each c In creados
                Try
                    File.Delete(c)
                Catch
                End Try
            Next
            For Each r In renombrados
                Try
                    If File.Exists(r.Item1) Then File.Delete(r.Item1)
                    File.Move(r.Item2, r.Item1)
                Catch
                End Try
            Next
            Try
                If configOriginal IsNot Nothing Then File.WriteAllBytes(configLocal, configOriginal)
            Catch
            End Try
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Reemplaza solo la sección runtime (bindingRedirect) y agrega appSettings nuevos que falten.
    ''' connectionStrings (cifrada) y los valores existentes no se tocan.
    ''' </summary>
    Private Sub MezclarConfig(local As String, nuevo As String)
        Dim docLocal = XDocument.Load(local)
        Dim docNuevo = XDocument.Load(nuevo)
        Dim raizLocal = docLocal.Root
        Dim raizNueva = docNuevo.Root

        Dim runtimeNuevo = raizNueva.Element("runtime")
        If runtimeNuevo IsNot Nothing Then
            Dim runtimeLocal = raizLocal.Element("runtime")
            If runtimeLocal IsNot Nothing Then
                runtimeLocal.ReplaceWith(runtimeNuevo)
            Else
                raizLocal.Add(runtimeNuevo)
            End If
        End If

        Dim settingsNuevo = raizNueva.Element("appSettings")
        If settingsNuevo IsNot Nothing Then
            Dim settingsLocal = raizLocal.Element("appSettings")
            If settingsLocal Is Nothing Then
                settingsLocal = New XElement("appSettings")
                raizLocal.Add(settingsLocal)
            End If
            For Each add In settingsNuevo.Elements("add")
                Dim clave = CStr(add.Attribute("key"))
                If Not settingsLocal.Elements("add").Any(Function(a) CStr(a.Attribute("key")) = clave) Then
                    settingsLocal.Add(add)
                End If
            Next
        End If

        docLocal.Save(local)
    End Sub

    Private Function DescargarTexto(url As String) As String
        Using wc As New TimeoutWebClient(TimeoutMs)
            wc.Encoding = Text.Encoding.UTF8
            ' DownloadString no quita el BOM UTF-8 y XElement.Parse lo rechaza.
            Return wc.DownloadString(url).TrimStart(ChrW(&HFEFF))
        End Using
    End Function

    Private Sub Descargar(url As String, destino As String, ventana As FormActualizando)
        Using wc As New TimeoutWebClient(30000)
            Dim terminado = False
            Dim fallo As Exception = Nothing
            AddHandler wc.DownloadProgressChanged, Sub(s, e) ventana.Mostrar("Descargando... " & e.ProgressPercentage & "%", e.ProgressPercentage)
            AddHandler wc.DownloadFileCompleted, Sub(s, e)
                                                     fallo = e.Error
                                                     terminado = True
                                                 End Sub
            wc.DownloadFileAsync(New Uri(url), destino)
            While Not terminado
                Application.DoEvents()
                Threading.Thread.Sleep(30)
            End While
            If fallo IsNot Nothing Then Throw fallo
        End Using
    End Sub

    Private Function CalcularSha256(ruta As String) As String
        Using sha = SHA256.Create(), fs = File.OpenRead(ruta)
            Return BitConverter.ToString(sha.ComputeHash(fs)).Replace("-", "")
        End Using
    End Function

    Private Class TimeoutWebClient
        Inherits WebClient
        Private ReadOnly timeout As Integer

        Public Sub New(timeoutMs As Integer)
            timeout = timeoutMs
        End Sub

        Protected Overrides Function GetWebRequest(address As Uri) As WebRequest
            Dim req = MyBase.GetWebRequest(address)
            req.Timeout = timeout
            Return req
        End Function
    End Class

End Module
