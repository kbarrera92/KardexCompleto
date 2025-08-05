Imports System.Data.SqlClient
Imports Serilog

Public Class Form1

    Sub Login()
        Dim nickInput = Trim(TextBox1.Text)
        Dim pwdInput = Trim(TextBox2.Text)

        ' Validaciones
        If nickInput = "" OrElse pwdInput = "" Then
            MsgBox("Nick y contraseña son obligatorios.", MsgBoxStyle.Exclamation, "Faltan datos")
            Return
        End If

        If sucActual = 0 Then
            sucActual = Integer.Parse(ConsultaParametro("codigoSucursal"))
        End If


        Try
            openConnection()

            Using cmd As New SqlCommand("sp_validaUsuario", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@NICK", nickInput)

                Using reader = cmd.ExecuteReader()
                    If Not reader.Read() Then
                        MsgBox("No se encontraron coincidencias", MsgBoxStyle.Critical, "Error en los datos")
                        Return
                    End If

                    ' Leemos salt y hash
                    Dim saltStored = DirectCast(reader("PasswordSalt"), Byte())
                    Dim hashStored = DirectCast(reader("PasswordHash"), Byte())
                    Dim hashComputed = PasswordHelper.HashPassword(pwdInput, saltStored)

                    If Not hashComputed.SequenceEqual(hashStored) Then
                        MsgBox("Usuario o contraseña incorrectos", MsgBoxStyle.Critical, "Error en los datos")
                        Return
                    End If

                    ' Si coincide, cargamos resto de datos
                    Dim idUsuario = CInt(reader("idUsuario"))
                    Dim nombreUser = reader("nombreUsuario").ToString()
                    Dim rolId = CInt(reader("tipoUsuario"))
                    Dim rolNombre = reader("rolNombre").ToString()
                    Dim sucursalId = CInt(reader("sucursal"))
                    Dim sucursalName = reader("sucNombre").ToString()

                    Dim bienvenido = $"Bienvenido al sistema: {nombreUser}"
                    If rolNombre = "ADMINISTRADOR" OrElse rolNombre = "GERENTE" Then
                        MsgBox(bienvenido, MsgBoxStyle.Information, ConsultaParametro("nombreEmpresa"))
                    ElseIf sucActual = sucursalId AndAlso (rolNombre = "VENDEDOR" OrElse rolNombre = "BODEGUERO") Then
                        MsgBox(bienvenido, MsgBoxStyle.Information, ConsultaParametro("nombreEmpresa"))
                    Else
                        MsgBox("No tiene permisos para esta sucursal", MsgBoxStyle.Critical, "Acceso denegado")
                        Return
                    End If

                    ' Guardamos globals y cerramos
                    usuarioActual = idUsuario
                    nameUsuarioActual = nombreUser
                    rolUsuarioActual = rolId
                    nombreRol = rolNombre
                    nameSucActual = sucursalName

                    reader.Close()
                    Me.Close()

                    With FormMenuNew
                        .ToolStripButtonLogin.Text = "Cerrar sesión"
                        .StatusStripPrincipal.BackColor = Color.LimeGreen
                        .ToolStripStatusLabelConnectionStatus.Text =
                        $"Estado de la conexión: conectado, Usuario: {nombreUser}, Sucursal: {sucursalName}"
                        .FlowLayoutPanelDashboard.Visible = True
                    End With

                    DibujaTarjetasResumen()
                    Log.Information($"{Environment.MachineName} - {Environment.UserName}")
                    Log.Information($"Inicio de sesión: {nombreUser}, desde: {ConsultaParametro("sucursalFisica")}")
                End Using
            End Using

        Catch ex As Exception
            Log.Information($"Ocurrió un error en Login: {ex.Message}")
            MsgBox("Error al conectar con la base de datos.", MsgBoxStyle.Critical, "Error")
        Finally
            closeConnection()
            Log.Information("Finaliza Login")
        End Try
    End Sub

    'Sub login()
    '    Dim reader As SqlDataReader

    '    If sucActual = 0 Then
    '        sucActual = Integer.Parse(ConsultaParametro("codigoSucursal"))
    '    End If

    '    Try
    '        openConnection()
    '        Dim cmd As New SqlCommand()
    '        With cmd
    '            .CommandText = "sp_validaUsuario"
    '            .CommandType = CommandType.StoredProcedure
    '            .Connection = conn
    '        End With
    '        cmd.Parameters.AddWithValue("NICK", Trim(TextBox1.Text))
    '        cmd.Parameters.AddWithValue("PASSWORD", Trim(TextBox2.Text))

    '        reader = cmd.ExecuteReader
    '        reader.Read()

    '        If reader.HasRows Then
    '            rolUsuarioActual = Val(reader(2).ToString)
    '            nameUsuarioActual = reader(1).ToString
    '            usuarioActual = Val(reader(0).ToString)
    '            nombreRol = reader(3).ToString

    '            If ((reader(3).ToString() = "ADMINISTRADOR" Or reader(3).ToString() = "GERENTE")) Then
    '                MsgBox("Bienvenido al sistema: " & nameUsuarioActual.ToString, MsgBoxStyle.Information, ConsultaParametro("nombreEmpresa"))
    '                reader.Close()
    '                Me.Close()
    '                FormMenuNew.ToolStripButtonLogin.Text = "Cerrar sesión"

    '                DibujaTarjetasResumen()
    '                FormMenuNew.FlowLayoutPanelDashboard.Visible = True

    '                Log.Information(Environment.MachineName & " - " & Environment.UserName)
    '                Log.Information("Inicio de sesión: " & nameUsuarioActual & ", desde: " & ConsultaParametro("sucursalFisica"))
    '            Else
    '                If (sucActual = reader(4) And (reader(3).ToString() = "VENDEDOR")) Then
    '                    MsgBox("Bienvenido al sistema: " & nameUsuarioActual.ToString, MsgBoxStyle.Information, ConsultaParametro("nombreEmpresa"))
    '                    reader.Close()
    '                    Me.Close()
    '                    FormMenuNew.ToolStripButtonLogin.Text = "Cerrar sesión"

    '                    Log.Information("Inicio de sesión: " & nameUsuarioActual & ", desde: " & ConsultaParametro("sucursalFisica"))
    '                Else
    '                    If (sucActual = reader(4) And (reader(3).ToString = "BODEGUERO")) Then
    '                        MsgBox("Bienvenido al sistema: " & nameUsuarioActual.ToString, MsgBoxStyle.Information, ConsultaParametro("nombreEmpresa"))
    '                        reader.Close()
    '                        Me.Close()
    '                        FormMenuNew.ToolStripButtonLogin.Text = "Cerrar sesión"

    '                        Log.Information("Inicio de sesión: " & nameUsuarioActual & ", desde: " & ConsultaParametro("sucursalFisica"))
    '                    Else
    '                        MsgBox("No se encontraron coincidencias", MsgBoxStyle.Critical, "Error en los datos")
    '                        reader.Close()

    '                        Log.Information("Inicio de sesión no autorizado: " & nameUsuarioActual & ", desde: " & ConsultaParametro("sucursalFisica"))
    '                    End If

    '                End If
    '            End If
    '            FormMenuNew.StatusStripPrincipal.BackColor = Color.LimeGreen
    '            FormMenuNew.ToolStripStatusLabelConnectionStatus.Text = $"Estado de la conexión: conectado, Usuario: {nameUsuarioActual}, Sucursal: {nameSucActual}"
    '        Else
    '            MsgBox("No se encontraron coincidencias", MsgBoxStyle.Critical, "Error en los datos")
    '        End If


    '    Catch ex As Exception
    '        Log.Information($"Ocurrio un error. Error: {ex.Message}")
    '    Finally
    '        closeConnection()
    '        Log.Information("Finaliza Login")
    '    End Try
    'End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        If TextBox2.PasswordChar = "*" Then
            TextBox2.PasswordChar = ""
        Else
            TextBox2.PasswordChar = "*"
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Estilos.AplicarEstilos(Me)
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            If Trim(TextBox1.Text) = "" Then
                MsgBox("Debe escribirse un usuario", MsgBoxStyle.Exclamation, "Faltan datos")
                TextBox1.Select()
            Else
                TextBox2.Select()
            End If
        End If
    End Sub

    Private Sub TextBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox2.KeyDown
        If e.KeyCode = Keys.Enter Then
            Button1.Select()
        End If
    End Sub

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            If MessageBox.Show("¿Desea salir de esta ventana?", "Saliendo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Me.Close()
            End If
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Login()
    End Sub
End Class
