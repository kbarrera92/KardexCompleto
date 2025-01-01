Imports System.Data.SqlClient
Public Class Form1

    Sub login()
        Dim reader As SqlDataReader

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

                Dim params(3) As String
                params(0) = Trim(TextBox1.Text)
                params(1) = Environment.MachineName & " - " & Environment.UserName
                params(2) = "Inicio de sesión: " & nameUsuarioActual & ", desde: " & ConsultaParametro("sucursalFisica")


                If ((reader(3).ToString() = "ADMINISTRADOR" Or reader(3).ToString() = "GERENTE")) Then
                    MsgBox("Bienvenido al sistema: " & nameUsuarioActual.ToString, MsgBoxStyle.Information, ConsultaParametro("nombreEmpresa"))
                    reader.Close()
                    Me.Close()
                    'frmMenu.Select()
                    FormMenuNew.ToolStripButtonLogin.Text = "Cerrar sesión"
                    'frmMenu.Button3.Enabled = True
                    'frmMenu.Button2.Enabled = True
                    'frmMenu.Button4.Enabled = True
                    'frmMenu.btnTraslados.Enabled = True
                    'frmMenu.Button6.Enabled = True
                    'frmMenu.Button7.Enabled = True
                    'frmMenu.Button8.Enabled = True
                    'frmMenu.Button9.Enabled = True
                    'frmMenu.btnRecibirTraslado.Enabled = True
                    'frmMenu.Button11.Enabled = True
                    GrabaBitacora(params, grabaBitacoraSp)
                Else
                    If (sucActual = reader(4) And (reader(3).ToString() = "VENDEDOR")) Then
                        MsgBox("Bienvenido al sistema: " & nameUsuarioActual.ToString, MsgBoxStyle.Information, ConsultaParametro("nombreEmpresa"))
                        reader.Close()
                        Me.Close()
                        'frmMenu.Select()
                        FormMenuNew.ToolStripButtonLogin.Text = "Cerrar sesión"
                        'frmMenu.Button2.Enabled = True
                        'frmMenu.btnTraslados.Enabled = True
                        'frmMenu.btnRecibirTraslado.Enabled = True
                        GrabaBitacora(params, grabaBitacoraSp)
                    Else
                        If (sucActual = reader(4) And (reader(3).ToString = "BODEGUERO")) Then
                            MsgBox("Bienvenido al sistema: " & nameUsuarioActual.ToString, MsgBoxStyle.Information, ConsultaParametro("nombreEmpresa"))
                            reader.Close()
                            Me.Close()
                            'frmMenu.Select()
                            FormMenuNew.ToolStripButtonLogin.Text = "Cerrar sesión"
                            'frmMenu.Button8.Enabled = True
                            'frmMenu.btnTraslados.Enabled = True
                            'frmMenu.btnRecibirTraslado.Enabled = True
                            GrabaBitacora(params, grabaBitacoraSp)
                        Else
                            params(2) = "Inicio de sesión no autorizado: " & nameUsuarioActual & ", desde: " & ConsultaParametro("sucursalFisica")
                            MsgBox("No se encontraron coincidencias", MsgBoxStyle.Critical, "Error en los datos")
                            reader.Close()
                            GrabaBitacora(params, grabaBitacoraSp)
                        End If

                    End If
                End If
            Else
                MsgBox("No se encontraron coincidencias", MsgBoxStyle.Critical, "Error en los datos")
            End If


        Catch ex As Exception
            MsgBox("No se encontraron coincidencias", MsgBoxStyle.Critical, "Error en los datos")
        Finally
            closeConnection()
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
