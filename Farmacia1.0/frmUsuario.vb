Imports System.Data.SqlClient
Imports System.Text

Public Class frmUsuario

    Dim RegOAct As Integer = 0
    Dim correlativo As String = "SELECT IDENT_CURRENT ('USUARIO') AS Current_Identity"
    Dim sqlUsuarios As String = "SELECT idUsuario, nombreUsuario FROM USUARIO U INNER JOIN TIPOUSUARIO TU ON U.tipoUsuario = TU.idTipoUsuario " _
                                & "WHERE TU.nombreTipo <> 'GERENTE' AND U.estado = 1"
    Dim sqlSuc As String = "SELECT idSucursal, nombreSuc FROM SUCURSAL"
    Dim sqlEstado As String = "SELECT idEstado, estadoUsuario FROM ESTADOUSUARIO"
    Private saltStored As Byte()
    Private hashStored As Byte()

    Sub getDatos()
        Dim ind, ind2, ind3 As Integer
        Dim sql As String = "SELECT U.idUsuario, U.nombreUsuario, U.nick, U.contraUsuario, TU.nombreTipo, S.nombreSuc, EU.estadoUsuario, U.PasswordSalt, U.PasswordHash " _
                            & "FROM USUARIO U " _
                            & "INNER JOIN TIPOUSUARIO TU " _
                            & "ON U.tipoUsuario = TU.idTipoUsuario " _
                            & "INNER JOIN SUCURSAL S " _
                            & "ON U.sucursal = S.idSucursal " _
                            & "INNER JOIN ESTADOUSUARIO EU " _
                            & "ON U.estado = EU.idEstado " _
                            & "WHERE U.idUsuario = @id"
        Dim cmd As SqlCommand

        cmd = New SqlCommand(sql, conn)
        cmd.Parameters.AddWithValue("id", ListBox1.SelectedValue.ToString)

        Try
            openConnection()
            Dim reader As SqlDataReader = cmd.ExecuteReader
            reader.Read()

            If reader.HasRows Then
                TextBox8.Text = reader(0)
                TextBox1.Text = reader(1)
                TextBox2.Text = reader(2)
                TextBox3.Text = reader(3)
                saltStored = reader(7)
                hashStored = reader(8)
                ind = ComboBox1.FindStringExact(reader(4).ToString)
                ComboBox1.SelectedIndex = ind
                ind2 = ComboBox2.FindStringExact(reader(5).ToString)
                ComboBox2.SelectedIndex = ind2
                ind3 = ComboBox3.FindStringExact(reader(6).ToString)
                ComboBox3.SelectedIndex = ind3
            End If
            reader.Close()
        Catch ex As Exception
            'MsgBox(ex.Message)
        Finally
            closeConnection()
        End Try
    End Sub

    Function updateList(ByVal sql As String) As DataTable
        Dim da As SqlDataAdapter
        Dim dt As New DataTable

        Try
            openConnection()
            da = New SqlDataAdapter(sql, conn)
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            MsgBox(ex.Message)
            Return Nothing
        End Try
    End Function

    Private Sub frmUsuario_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBox2.DataSource = updateCm(sqlSuc)
        ComboBox2.ValueMember = updateCm(sqlSuc).Columns(0).ToString
        ComboBox2.DisplayMember = updateCm(sqlSuc).Columns(1).ToString

        ComboBox3.DataSource = updateCm(sqlEstado)
        ComboBox3.ValueMember = updateCm(sqlEstado).Columns(0).ToString
        ComboBox3.DisplayMember = updateCm(sqlEstado).Columns(1).ToString

        ComboBox1.DataSource = updateCm("SELECT idTipoUsuario, nombreTipo FROM TIPOUSUARIO WHERE nombreTipo <> 'GERENTE'")
        ComboBox1.ValueMember = updateCm("SELECT idTipoUsuario, nombreTipo FROM TIPOUSUARIO WHERE nombreTipo <> 'GERENTE'").Columns(0).ToString
        ComboBox1.DisplayMember = updateCm("SELECT idTipoUsuario, nombreTipo FROM TIPOUSUARIO WHERE nombreTipo <> 'GERENTE'").Columns(1).ToString

        ListBox1.DataSource = updateList(sqlUsuarios)
        ListBox1.ValueMember = updateList(sqlUsuarios).Columns(0).ToString
        ListBox1.DisplayMember = updateList(sqlUsuarios).Columns(1).ToString

        Estilos.AplicarEstilos(Me)
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        getDatos()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TextBox8.Text = getCorrelativoTrasiego(correlativo) + 1
        TextBox1.Clear()
        TextBox1.Select()
        TextBox2.Clear()
        TextBox3.Clear()
        ComboBox1.SelectedIndex = -1
        ComboBox2.SelectedIndex = -1
        ComboBox3.SelectedIndex = -1
        RegOAct = 1
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        If Trim(TextBox1.Text) = "" OrElse Trim(TextBox2.Text) = "" OrElse ComboBox1.SelectedIndex = -1 OrElse ComboBox2.SelectedIndex = -1 OrElse ComboBox3.SelectedIndex = -1 Then
            MsgBox("Faltan datos que son obligatorios.", MsgBoxStyle.Information, "Faltan datos")
            Return
        End If
        ' Al registrar (RegOAct = 1), la contraseña también es obligatoria
        If RegOAct = 1 AndAlso Trim(TextBoxPass2.Text) = "" AndAlso Trim(TextBoxPass3.Text) = "" Then
            MsgBox("La contraseña es obligatoria.", MsgBoxStyle.Information, "Faltan datos")
            Return
        End If

        If TextBoxPass2.Text.Trim() <> TextBoxPass3.Text.Trim() Then
            MsgBox("Las contraseñas no coinciden.", MsgBoxStyle.Information, "Faltan datos")
            TextBoxPass2.Select()
            Return
        End If

        openConnection()
        If RegOAct = 1 Then
            ' --- INSERT ---
            If MessageBox.Show("¿Desea guardar este registro?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) _
                    = DialogResult.Yes Then

                ' Generamos salt y hash
                Dim salt() = GenerateSalt()
                Dim hash() = HashPassword(Trim(TextBoxPass2.Text), salt)

                Using cmd As New SqlCommand(
                    "INSERT INTO USUARIO
                       (nombreUsuario,nick,contraUsuario,tipoUsuario,sucursal,estado,PasswordSalt,PasswordHash)
                     VALUES
                       (@n,@nick,@contra,@tu,@suc,@estado,@salt,@hash)", conn)

                    cmd.Parameters.Add("@n", SqlDbType.VarChar, 100).Value = Trim(TextBox1.Text)
                    cmd.Parameters.Add("@nick", SqlDbType.VarChar, 50).Value = Trim(TextBox2.Text)
                    cmd.Parameters.Add("@tu", SqlDbType.Int).Value = CInt(ComboBox1.SelectedValue)
                    cmd.Parameters.Add("@suc", SqlDbType.Int).Value = CInt(ComboBox2.SelectedValue)
                    cmd.Parameters.Add("@estado", SqlDbType.Int).Value = CInt(ComboBox3.SelectedValue)
                    cmd.Parameters.Add("@salt", SqlDbType.VarBinary, 128).Value = salt
                    cmd.Parameters.Add("@hash", SqlDbType.VarBinary, 256).Value = hash
                    cmd.Parameters.Add("@contra", SqlDbType.VarChar, 50).Value = String.Empty

                    cmd.ExecuteNonQuery()
                End Using

                MessageBox.Show("Usuario registrado correctamente.", "Éxito",
                                MessageBoxButtons.OK, MessageBoxIcon.Information)

            End If

        Else
            ' --- UPDATE ---
            If MessageBox.Show("¿Desea guardar los cambios de este registro?", "Guardar cambios",
                               MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Dim nuevaPass = Trim(TextBoxPass2.Text)
                Dim salt() As Byte = Nothing
                Dim hash() As Byte = Nothing
                ' Armamos la instrucción UPDATE
                Dim sqlUpdate = New StringBuilder()
                sqlUpdate.Append("UPDATE USUARIO SET ")
                sqlUpdate.Append("nombreUsuario = @n, ")
                sqlUpdate.Append("nick          = @nick, ")
                sqlUpdate.Append("tipoUsuario   = @tu, ")
                sqlUpdate.Append("sucursal      = @sucu, ")
                sqlUpdate.Append("estado        = @es ")
                If Not String.IsNullOrWhiteSpace(TextBoxPass1.Text.Trim()) Then
                    If TextBoxPass2.Text = TextBoxPass3.Text Then
                        Dim hashActual = HashPassword(TextBoxPass1.Text.Trim(), saltStored)
                        'If Not hashActual.SequenceEqual(hashStored) Then
                        '    MsgBox("La contraseña actual ingresada es incorrecta.", MsgBoxStyle.Critical, "Acceso denegado")
                        '    TextBoxPass1.SelectAll()
                        '    TextBoxPass1.Focus()
                        '    Return
                        'End If
                        sqlUpdate.Append(", contraUsuario = @contra, ")


                        If nuevaPass <> "" Then
                            salt = GenerateSalt()
                            hash = HashPassword(nuevaPass, salt)
                            sqlUpdate.Append("PasswordSalt = @salt, PasswordHash = @hash")
                        End If
                    End If
                End If

                sqlUpdate.Append(" WHERE idUsuario = @id")

                Using cmd As New SqlCommand(sqlUpdate.ToString(), conn)
                    cmd.Parameters.Add("@n", SqlDbType.VarChar, 100).Value = Trim(TextBox1.Text)
                    cmd.Parameters.Add("@nick", SqlDbType.VarChar, 50).Value = Trim(TextBox2.Text)
                    cmd.Parameters.Add("@tu", SqlDbType.Int).Value = CInt(ComboBox1.SelectedValue)
                    cmd.Parameters.Add("@sucu", SqlDbType.Int).Value = CInt(ComboBox2.SelectedValue)
                    cmd.Parameters.Add("@es", SqlDbType.Int).Value = CInt(ComboBox3.SelectedValue)
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = CInt(TextBox8.Text)
                    cmd.Parameters.Add("@contra", SqlDbType.VarChar, 50).Value = String.Empty

                    If nuevaPass <> "" Then
                        cmd.Parameters.Add("@salt", SqlDbType.VarBinary, 128).Value = salt
                        cmd.Parameters.Add("@hash", SqlDbType.VarBinary, 256).Value = hash
                    End If

                    cmd.ExecuteNonQuery()
                End Using

                MessageBox.Show("Usuario actualizado correctamente.", "Éxito",
                                MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If

        closeConnection()
        With ListBox1
            .DataSource = updateList(sqlUsuarios)
            .ValueMember = updateList(sqlUsuarios).Columns(0).ToString()
        End With
        TextBox1.Clear() : TextBox2.Clear() : TextBox3.Clear() : TextBox8.Clear() : TextBoxPass1.Clear() : TextBoxPass2.Clear() : TextBoxPass3.Clear()
        ComboBox1.SelectedIndex = -1
        ComboBox2.SelectedIndex = -1
        ComboBox3.SelectedIndex = -1
        RegOAct = 0
        ListBox1.Select()

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If MessageBox.Show("¿Desea eliminar este registro?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim sqlupdate As String = "UPDATE USUARIO SET estado = @es WHERE idUsuario = @id"
            Dim cmd As SqlCommand
            cmd = New SqlCommand(sqlupdate, conn)

            cmd.Parameters.AddWithValue("es", 0)

            cmd.Parameters.AddWithValue("id", CInt(TextBox8.Text))

            Try
                openConnection()
                cmd.ExecuteNonQuery()
                TextBox1.Clear()
                TextBox2.Clear()
                TextBox3.Clear()
                TextBox8.Clear()
                ComboBox1.SelectedIndex = -1
                ComboBox2.SelectedIndex = -1
                ComboBox3.SelectedIndex = -1

                MessageBox.Show("El usuario se desactivó de forma correcta", "Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show("Algo salió mal" & vbCrLf & "Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Finally
                closeConnection()
                With ListBox1
                    .DataSource = updateList(sqlUsuarios)
                    .ValueMember = updateList(sqlUsuarios).Columns(0).ToString
                End With
                ListBox1.Select()
            End Try
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Close()
    End Sub

    Private Sub btnPass1_Click(sender As Object, e As EventArgs) Handles btnPass1.Click
        If btnPass1.Text = "M" Then
            TextBoxPass1.UseSystemPasswordChar = False
            btnPass1.Text = "O"
        Else
            TextBoxPass1.UseSystemPasswordChar = True
            btnPass1.Text = "M"
        End If

    End Sub

    Private Sub btnPass2_Click(sender As Object, e As EventArgs) Handles btnPass2.Click
        If btnPass2.Text = "M" Then
            TextBoxPass2.UseSystemPasswordChar = False
            btnPass2.Text = "O"
        Else
            TextBoxPass2.UseSystemPasswordChar = True
            btnPass2.Text = "M"
        End If

    End Sub

    Private Sub btnPass3_Click(sender As Object, e As EventArgs) Handles btnPass3.Click
        If btnPass3.Text = "M" Then
            TextBoxPass3.UseSystemPasswordChar = False
            btnPass3.Text = "O"
        Else
            TextBoxPass3.UseSystemPasswordChar = True
            btnPass3.Text = "M"
        End If

    End Sub
End Class