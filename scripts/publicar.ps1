<#
.SYNOPSIS
  Compila Release, empaqueta y sube la actualización al hosting por SFTP.
.DESCRIPTION
  Sube primero el zip y al final version.xml, para que ningún cliente vea un manifiesto
  que apunte a un zip aún incompleto. Usa WinSCP.com y pide la contraseña al ejecutarse;
  nada se guarda en el repo.
.EXAMPLE
  .\scripts\publicar.ps1
  .\scripts\publicar.ps1 -SoloEmpaquetar
#>
param(
    [string] $Conexion = 'ftp://farmaciahorro@52.249.30.165/',
    [string] $RutaRemota = '/farmacia/farmacia',
    [string] $UrlPublica = 'https://isprodev.xyz/farmacia/',
    [switch] $SoloEmpaquetar
)

$ErrorActionPreference = 'Stop'
$raiz = Split-Path $PSScriptRoot -Parent
$release = Join-Path $raiz 'Farmacia1.0\bin\Release'
$salida = Join-Path $raiz 'publicacion'

# 1. Compilar
$msbuild = & "${env:ProgramFiles(x86)}\Microsoft Visual Studio\Installer\vswhere.exe" -latest -requires Microsoft.Component.MSBuild -find 'MSBuild\**\Bin\MSBuild.exe' | Select-Object -First 1
if (-not $msbuild) { throw 'No se encontró MSBuild (vswhere).' }
& $msbuild (Join-Path $raiz 'Farmacia1.0.sln') /p:Configuration=Release /t:Rebuild /v:minimal
if ($LASTEXITCODE -ne 0) { throw 'La compilación falló.' }

# 2. Versión
$version = (Get-Item (Join-Path $release 'Farmacia1.0.exe')).VersionInfo.FileVersion
Write-Host "Versión compilada: $version"

# 3. Zip sin archivos de desarrollo/runtime
if (Test-Path $salida) { Remove-Item $salida -Recurse -Force }
$staging = Join-Path $salida 'contenido'
New-Item -ItemType Directory $staging | Out-Null
$excluir = '*.pdb', '*.xml', '*.application', '*.exe.manifest', 'Factura.txt'
Get-ChildItem $release -Recurse -File |
    Where-Object {
        $archivo = $_
        $rel = $archivo.FullName.Substring($release.Length + 1)
        -not ($rel -like 'logs\*' -or $rel -like 'app.publish\*') -and
        -not ($excluir | Where-Object { $archivo.Name -like $_ })
    } | ForEach-Object {
        $rel = $_.FullName.Substring($release.Length + 1)
        $dest = Join-Path $staging $rel
        New-Item -ItemType Directory (Split-Path $dest) -Force | Out-Null
        Copy-Item $_.FullName $dest
    }

$zipNombre = "Farmacia1.0-$version.zip"
$zip = Join-Path $salida $zipNombre
Compress-Archive -Path (Join-Path $staging '*') -DestinationPath $zip
$sha = (Get-FileHash $zip -Algorithm SHA256).Hash

$manifiesto = Join-Path $salida 'version.xml'
[IO.File]::WriteAllText($manifiesto,
    "<actualizacion version=`"$version`" archivo=`"$zipNombre`" sha256=`"$sha`" />",
    (New-Object Text.UTF8Encoding($false)))
Write-Host "Paquete: $zip ($([math]::Round((Get-Item $zip).Length/1MB,2)) MB)  SHA256: $sha"

if ($SoloEmpaquetar) { return }

# 4. Subir: zip primero, version.xml al final
$winscp = @(
    (Get-Command WinSCP.com -ErrorAction SilentlyContinue).Source,
    "$env:ProgramFiles\WinSCP\WinSCP.com",
    "${env:ProgramFiles(x86)}\WinSCP\WinSCP.com",
    "$env:LOCALAPPDATA\Programs\WinSCP\WinSCP.com"
) | Where-Object { $_ -and (Test-Path $_) } | Select-Object -First 1
if (-not $winscp) { throw 'WinSCP no está instalado (https://winscp.net).' }

# WinSCP en modo script no usa las contraseñas guardadas del almacén de sitios: se pide aquí,
# solo vive en memoria durante la subida y no se escribe en el log.
$clave = Read-Host "Contraseña de $Conexion" -AsSecureString
$claveTexto = [Runtime.InteropServices.Marshal]::PtrToStringAuto([Runtime.InteropServices.Marshal]::SecureStringToBSTR($clave))
$claveOpcion = $claveTexto.Replace('"', '""')

& $winscp /log="$salida\winscp.log" `
    /command `
    "open $Conexion -password=`"$claveOpcion`" -rawsettings ProxyPort=0" `
    "cd `"$RutaRemota`"" `
    "put `"$zip`"" `
    "put `"$manifiesto`"" `
    "exit"
if ($LASTEXITCODE -ne 0) { throw "La subida falló (ver $salida\winscp.log)." }

# 5. Verificar lo publicado por HTTPS: el manifiesto y el zip deben coincidir con lo local
$remoto = Invoke-WebRequest "$($UrlPublica)version.xml" -UseBasicParsing
if (([xml]$remoto.Content).actualizacion.version -ne $version) { throw 'version.xml publicado no coincide.' }
$zipRemoto = Join-Path $salida 'verificacion.zip'
Invoke-WebRequest "$UrlPublica$zipNombre" -OutFile $zipRemoto -UseBasicParsing
if ((Get-FileHash $zipRemoto -Algorithm SHA256).Hash -ne $sha) { throw 'El zip publicado no coincide con el local.' }
Remove-Item $zipRemoto
Write-Host "Publicada y verificada la versión $version."
