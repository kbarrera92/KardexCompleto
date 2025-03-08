Imports System.Data.SqlClient
Imports Serilog

Public Class Form1

    Sub login()
        Dim reader As SqlDataReader

        If sucActual = 0 Then
            sucActual = Integer.Parse(ConsultaParametro("codigoSucursal"))
        End If

        Try
            openConnection()
            Dim cmd As New SqlCommand()
            With cmd
                .CommandText = "sp_validaUsuario"
                .CommandType = CommandType.StoredProcedure
                .Connection = conn
            End With
            cmd.Parameters.AddWithValue("NICK", Trim(TextBox1.Text))
            cmd.Parameters.AddWithValue("PASSWORD", Trim(TextBox2.Text))

            reader = cmd.ExecuteReader
            reader.Read()

            If reader.HasRows Then
                rolUsuarioActual = Val(reader(2).ToString)
                nameUsuarioActual = reader(1).ToString
                usuarioActual = Val(reader(0).ToString)
                nombreRol = reader(3).ToString

                If ((reader(3).ToString() = "ADMINISTRADOR" Or reader(3).ToString() = "GERENTE")) Then
                    MsgBox("Bienvenido al sistema: " & nameUsuarioActual.ToString, MsgBoxStyle.Information, ConsultaParametro("nombreEmpresa"))
                    reader.Close()
                    Me.Close()
                    FormMenuNew.ToolStripButtonLogin.Text = "Cerrar sesión"

                    Log.Information(Environment.MachineName & " - " & Environment.UserName)
                    Log.Information("Inicio de sesión: " & nameUsuarioActual & ", desde: " & ConsultaParametro("sucursalFisica"))
                Else
                    If (sucActual = reader(4) And (reader(3).ToString() = "VENDEDOR")) Then
                        MsgBox("Bienvenido al sistema: " & nameUsuarioActual.ToString, MsgBoxStyle.Information, ConsultaParametro("nombreEmpresa"))
                        reader.Close()
                        Me.Close()
                        FormMenuNew.ToolStripButtonLogin.Text = "Cerrar sesión"

                        Log.Information("Inicio de sesión: " & nameUsuarioActual & ", desde: " & ConsultaParametro("sucursalFisica"))
                    Else
                        If (sucActual = reader(4) And (reader(3).ToString = "BODEGUERO")) Then
                            MsgBox("Bienvenido al sistema: " & nameUsuarioActual.ToString, MsgBoxStyle.Information, ConsultaParametro("nombreEmpresa"))
                            reader.Close()
                            Me.Close()
                            FormMenuNew.ToolStripButtonLogin.Text = "Cerrar sesión"

                            Log.Information("Inicio de sesión: " & nameUsuarioActual & ", desde: " & ConsultaParametro("sucursalFisica"))
                        Else
                            MsgBox("No se encontraron coincidencias", MsgBoxStyle.Critical, "Error en los datos")
                            reader.Close()

                            Log.Information("Inicio de sesión no autorizado: " & nameUsuarioActual & ", desde: " & ConsultaParametro("sucursalFisica"))
                        End If

                    End If
                End If
            Else
                MsgBox("No se encontraron coincidencias", MsgBoxStyle.Critical, "Error en los datos")
            End If


        Catch ex As Exception
            Log.Information($"Ocurrio un error. Error: {ex.Message}")
        Finally
            closeConnection()
            Log.Information("Finaliza Login")
        End Try
    End Sub

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
        login()
    End Sub
End Class
