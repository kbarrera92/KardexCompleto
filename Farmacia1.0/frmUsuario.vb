Imports System.Data.SqlClient
Imports System.Text
Imports Serilog

Public Class frmUsuario

    Dim RegOAct As Integer = 0
    Dim correlativo As String = "SELECT IDENT_CURRENT ('USUARIO') AS Current_Identity"
    Dim sqlUsuarios As String = "SELECT idUsuario, nombreUsuario FROM USUARIO U INNER JOIN TIPOUSUARIO TU ON U.tipoUsuario = TU.idTipoUsuario " _
                                & "WHERE TU.nombreTipo <> 'GERENTE'"
    Dim sqlEstado As String = "SELECT idEstado, estadoUsuario FROM ESTADOUSUARIO"
    Dim sqlSucursalesPorUsuario As String = "SELECT S.idSucursal, S.nombreSuc 
                                 FROM USUARIO_SUCURSAL US
                                 INNER JOIN SUCURSAL S ON US.idSucursal = S.idSucursal
                                 WHERE US.idUsuario = @usuario"
    Private saltStored As Byte()
    Private hashStored As Byte()

    Private Sub CargarSucursalesUsuario(ByVal idUsuario As Integer)
        Try

            For i As Integer = 0 To CheckedListBoxSucursales.Items.Count - 1
                CheckedListBoxSucursales.SetItemChecked(i, False)
            Next

            openConnection()

            Dim query As String = "
            SELECT 
                    S.idSucursal,
                    S.nombreSuc
                FROM USUARIO_SUCURSAL US
                INNER JOIN SUCURSAL S 
                    ON US.idSucursal = S.idSucursal
                WHERE US.idUsuario = @usuario
            "

            Using cmd As New SqlCommand(query, conn)

                cmd.Parameters.Add("@usuario", SqlDbType.Int).Value = idUsuario

                Using reader As SqlDataReader = cmd.ExecuteReader()

                    While reader.Read()

                        Dim idSucursal As Integer =
                        Convert.ToInt32(reader("idSucursal"))

                        For i As Integer = 0 To CheckedListBoxSucursales.Items.Count - 1

                            Dim sucursal As SucursalItem =
                            DirectCast(CheckedListBoxSucursales.Items(i), SucursalItem)

                            If sucursal.IdSucursal = idSucursal Then
                                CheckedListBoxSucursales.SetItemChecked(i, True)
                                Exit For
                            End If

                        Next

                    End While

                End Using

            End Using

        Catch ex As Exception

            Log.Error($"Ocurrió un error al consultar las sucursales del usuario. Error: {ex.Message}")

        Finally

            closeConnection()

        End Try

    End Sub
    Sub LlenaListaSucursales(ByVal query As String, Optional ByVal usuario As Integer = 0)
        Try
            openConnection()
            Using cmd As New SqlCommand(query, conn)

                If usuario <> 0 Then
                    cmd.Parameters.Add("usuario", SqlDbType.Int).Value = usuario
                End If

                Using reader As SqlDataReader = cmd.ExecuteReader()
                    While reader.Read()

                        Dim sucursal As New SucursalItem With {
                                .IdSucursal = Convert.ToInt32(reader("idSucursal")),
                                .NombreSucursal = reader("nombreSuc").ToString()
                            }

                        CheckedListBoxSucursales.Items.Add(sucursal)

                    End While
                End Using
            End Using
        Catch ex As Exception
            Log.Error($"Ocurrió un error al consultar las sucursales. Error: {ex.Message}")
        Finally
            closeConnection()
        End Try
    End Sub

    Sub getDatos()
        Dim ind, ind3 As Integer
        Dim sql As String = "SELECT U.idUsuario, U.nombreUsuario, U.nick, U.contraUsuario, TU.nombreTipo, EU.estadoUsuario, U.PasswordSalt, U.PasswordHash " _
                            & "FROM USUARIO U " _
                            & "INNER JOIN TIPOUSUARIO TU " _
                            & "ON U.tipoUsuario = TU.idTipoUsuario " _
                            & "INNER JOIN ESTADOUSUARIO EU " _
                            & "ON U.estado = EU.idEstado " _
                            & "WHERE U.idUsuario = @id"
        Dim cmd As SqlCommand

        cmd = New SqlCommand(sql, conn)
        Dim idUsuario As Integer = Convert.ToInt32(ListBox1.SelectedValue)
        cmd.Parameters.AddWithValue("id", idUsuario)

        Try
            openConnection()
            Dim reader As SqlDataReader = cmd.ExecuteReader
            reader.Read()

            If reader.HasRows Then
                TextBoxIdUsuario.Text = reader(0)
                TextBoxNombre.Text = reader(1)
                TextBoxNickname.Text = reader(2)
                TextBox3.Text = reader(3)
                saltStored = reader(6)
                hashStored = reader(7)
                ind = ComboBoxTipoUsuario.FindStringExact(reader(4).ToString)
                ComboBoxTipoUsuario.SelectedIndex = ind
                ind3 = ComboBoxEstadoUsuario.FindStringExact(reader(5).ToString)
                ComboBoxEstadoUsuario.SelectedIndex = ind3
            End If
            reader.Close()
        Catch ex As Exception
            'MsgBox(ex.Message)
        Finally
            closeConnection()
            CargarSucursalesUsuario(idUsuario)
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
        ComboBoxEstadoUsuario.DataSource = updateCm(sqlEstado)
        ComboBoxEstadoUsuario.ValueMember = updateCm(sqlEstado).Columns(0).ToString
        ComboBoxEstadoUsuario.DisplayMember = updateCm(sqlEstado).Columns(1).ToString

        ComboBoxTipoUsuario.DataSource = updateCm("SELECT idTipoUsuario, nombreTipo FROM TIPOUSUARIO WHERE nombreTipo <> 'GERENTE'")
        ComboBoxTipoUsuario.ValueMember = updateCm("SELECT idTipoUsuario, nombreTipo FROM TIPOUSUARIO WHERE nombreTipo <> 'GERENTE'").Columns(0).ToString
        ComboBoxTipoUsuario.DisplayMember = updateCm("SELECT idTipoUsuario, nombreTipo FROM TIPOUSUARIO WHERE nombreTipo <> 'GERENTE'").Columns(1).ToString

        Dim dtUsuarios As DataTable = updateList(sqlUsuarios)

        ListBox1.DataSource = dtUsuarios
        ListBox1.ValueMember = "idUsuario"
        ListBox1.DisplayMember = "nombreUsuario"

        LlenaListaSucursales("SELECT idSucursal, nombreSuc FROM SUCURSAL")

        Estilos.AplicarEstilos(Me)
    End Sub



    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TextBoxIdUsuario.Text = getCorrelativoTrasiego(correlativo) + 1
        TextBoxNombre.Clear()
        TextBoxNombre.Select()
        TextBoxNickname.Clear()
        TextBox3.Clear()
        ComboBoxTipoUsuario.SelectedIndex = -1
        ComboBoxEstadoUsuario.SelectedIndex = -1
        RegOAct = 1
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        If Trim(TextBoxNombre.Text) = "" OrElse Trim(TextBoxNickname.Text) = "" OrElse ComboBoxTipoUsuario.SelectedIndex = -1 OrElse ComboBoxEstadoUsuario.SelectedIndex = -1 Then
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
            If MessageBox.Show("¿Desea guardar este registro?", "Guardar",
                   MessageBoxButtons.YesNo,
                   MessageBoxIcon.Question) = DialogResult.Yes Then

                Try
                    ' Generamos salt y hash
                    Dim salt() = GenerateSalt()
                    Dim hash() = HashPassword(Trim(TextBoxPass2.Text), salt)

                    Using trans As SqlTransaction = conn.BeginTransaction()

                        Try

                            ' ============================
                            ' 1. GRABAR USUARIO
                            ' ============================

                            Dim idUsuario As Integer

                            Using cmd As New SqlCommand(
                                "INSERT INTO USUARIO
                                    (nombreUsuario, nick, contraUsuario, tipoUsuario, estado, PasswordSalt, PasswordHash)
                                 VALUES
                                    (@n, @nick, @contra, @tu, @estado, @salt, @hash);

                                 SELECT CAST(SCOPE_IDENTITY() AS INT);",
                                conn,
                                trans)

                                cmd.Parameters.Add("@n", SqlDbType.VarChar, 100).Value =
                                    Trim(TextBoxNombre.Text)

                                cmd.Parameters.Add("@nick", SqlDbType.VarChar, 50).Value =
                                    Trim(TextBoxNickname.Text)

                                cmd.Parameters.Add("@tu", SqlDbType.Int).Value =
                                    CInt(ComboBoxTipoUsuario.SelectedValue)

                                cmd.Parameters.Add("@estado", SqlDbType.Int).Value =
                                    CInt(ComboBoxEstadoUsuario.SelectedValue)

                                cmd.Parameters.Add("@salt", SqlDbType.VarBinary, 128).Value =
                                    salt

                                cmd.Parameters.Add("@hash", SqlDbType.VarBinary, 256).Value =
                                    hash

                                cmd.Parameters.Add("@contra", SqlDbType.VarChar, 50).Value =
                                    String.Empty

                                idUsuario = Convert.ToInt32(cmd.ExecuteScalar())

                            End Using


                            ' ============================
                            ' 2. GRABAR SUCURSALES
                            ' ============================

                            For Each item As SucursalItem In CheckedListBoxSucursales.CheckedItems

                                Using cmdSucursal As New SqlCommand(
                                    "INSERT INTO USUARIO_SUCURSAL
                                        (idUsuario, idSucursal)
                                     VALUES
                                        (@idUsuario, @idSucursal)",
                                    conn,
                                    trans)

                                    cmdSucursal.Parameters.Add("@idUsuario", SqlDbType.Int).Value =
                                        idUsuario

                                    cmdSucursal.Parameters.Add("@idSucursal", SqlDbType.Int).Value =
                                        item.IdSucursal

                                    cmdSucursal.ExecuteNonQuery()

                                End Using

                            Next


                            ' ============================
                            ' 3. CONFIRMAR TODO
                            ' ============================

                            trans.Commit()

                            MessageBox.Show(
                                "Usuario registrado correctamente.",
                                "Éxito",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information)

                        Catch ex As Exception

                            trans.Rollback()

                            Throw

                        End Try

                    End Using

                Catch ex As Exception

                    Log.Error($"Error al registrar el usuario. Error: {ex.Message}")

                    MessageBox.Show(
                        "No fue posible registrar el usuario.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)

                End Try

            End If

        Else
            ' --- UPDATE ---
            If MessageBox.Show("¿Desea guardar los cambios de este registro?",
                   "Guardar cambios",
                   MessageBoxButtons.YesNo,
                   MessageBoxIcon.Question) = DialogResult.Yes Then

                Try

                    Dim idUsuario As Integer = CInt(TextBoxIdUsuario.Text)

                    Dim nuevaPass As String = TextBoxPass2.Text.Trim()

                    Dim salt() As Byte = Nothing
                    Dim hash() As Byte = Nothing

                    ' ==========================================
                    ' ARMAMOS EL UPDATE DEL USUARIO
                    ' ==========================================

                    Dim sqlUpdate As New StringBuilder()

                    sqlUpdate.Append("UPDATE USUARIO SET ")
                    sqlUpdate.Append("nombreUsuario = @n, ")
                    sqlUpdate.Append("nick = @nick, ")
                    sqlUpdate.Append("tipoUsuario = @tu, ")
                    sqlUpdate.Append("estado = @es ")

                    If Not String.IsNullOrWhiteSpace(TextBoxPass1.Text.Trim()) Then

                        If TextBoxPass2.Text = TextBoxPass3.Text Then

                            sqlUpdate.Append(", contraUsuario = @contra ")

                            If nuevaPass <> "" Then

                                salt = GenerateSalt()
                                hash = HashPassword(nuevaPass, salt)

                                sqlUpdate.Append(", PasswordSalt = @salt ")
                                sqlUpdate.Append(", PasswordHash = @hash ")

                            End If

                        End If

                    End If

                    sqlUpdate.Append(" WHERE idUsuario = @id")


                    ' ==========================================
                    ' TRANSACCIÓN
                    ' ==========================================

                    Using trans As SqlTransaction = conn.BeginTransaction()

                        Try

                            ' ==========================================
                            ' 1. ACTUALIZAR USUARIO
                            ' ==========================================

                            Using cmd As New SqlCommand(
                                sqlUpdate.ToString(),
                                conn,
                                trans)

                                cmd.Parameters.Add("@n", SqlDbType.VarChar, 100).Value = TextBoxNombre.Text.Trim()

                                cmd.Parameters.Add("@nick", SqlDbType.VarChar, 50).Value = TextBoxNickname.Text.Trim()

                                cmd.Parameters.Add("@tu", SqlDbType.Int).Value = CInt(ComboBoxTipoUsuario.SelectedValue)

                                cmd.Parameters.Add("@es", SqlDbType.Int).Value = CInt(ComboBoxEstadoUsuario.SelectedValue)

                                cmd.Parameters.Add("@id", SqlDbType.Int).Value = idUsuario

                                cmd.Parameters.Add("@contra", SqlDbType.VarChar, 50).Value = String.Empty

                                If nuevaPass <> "" Then

                                    cmd.Parameters.Add("@salt", SqlDbType.VarBinary, 128).Value = salt

                                    cmd.Parameters.Add("@hash", SqlDbType.VarBinary, 256).Value = hash

                                End If

                                cmd.ExecuteNonQuery()

                            End Using


                            ' ==========================================
                            ' 2. ELIMINAR SUCURSALES ACTUALES
                            ' ==========================================

                            Using cmdDelete As New SqlCommand(
                                "DELETE FROM USUARIO_SUCURSAL
                                 WHERE idUsuario = @idUsuario",
                                conn,
                                trans)

                                cmdDelete.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario

                                cmdDelete.ExecuteNonQuery()

                            End Using


                            ' ==========================================
                            ' 3. INSERTAR LAS NUEVAS SUCURSALES
                            ' ==========================================

                            For Each item As SucursalItem In CheckedListBoxSucursales.CheckedItems

                                Using cmdSucursal As New SqlCommand(
                                    "INSERT INTO USUARIO_SUCURSAL
                                        (idUsuario, idSucursal)
                                     VALUES
                                        (@idUsuario, @idSucursal)",
                                    conn,
                                    trans)

                                    cmdSucursal.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario

                                    cmdSucursal.Parameters.Add("@idSucursal", SqlDbType.Int).Value = item.IdSucursal

                                    cmdSucursal.ExecuteNonQuery()

                                End Using

                            Next


                            ' ==========================================
                            ' 4. CONFIRMAR TODO
                            ' ==========================================

                            trans.Commit()

                            MessageBox.Show(
                                "Usuario actualizado correctamente.",
                                "Éxito",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information)


                        Catch ex As Exception

                            ' Si algo falla, deshacemos TODO
                            trans.Rollback()

                            Throw

                        End Try

                    End Using


                Catch ex As Exception

                    Log.Error(
                        $"Error al actualizar el usuario. Error: {ex.Message}")

                    MessageBox.Show(
                        "No fue posible actualizar el usuario.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)

                End Try

            End If
        End If

        closeConnection()
        With ListBox1
            .DataSource = updateList(sqlUsuarios)
            .ValueMember = updateList(sqlUsuarios).Columns(0).ToString()
        End With
        TextBoxNombre.Clear() : TextBoxNickname.Clear() : TextBox3.Clear() : TextBoxIdUsuario.Clear() : TextBoxPass1.Clear() : TextBoxPass2.Clear() : TextBoxPass3.Clear()
        ComboBoxTipoUsuario.SelectedIndex = -1
        ComboBoxEstadoUsuario.SelectedIndex = -1
        RegOAct = 0
        ListBox1.Select()

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If MessageBox.Show("¿Desea eliminar este registro?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim sqlupdate As String = "UPDATE USUARIO SET estado = @es WHERE idUsuario = @id"
            Dim cmd As SqlCommand
            cmd = New SqlCommand(sqlupdate, conn)

            cmd.Parameters.AddWithValue("es", 0)

            cmd.Parameters.AddWithValue("id", CInt(TextBoxIdUsuario.Text))

            Try
                openConnection()
                cmd.ExecuteNonQuery()
                TextBoxNombre.Clear()
                TextBoxNickname.Clear()
                TextBox3.Clear()
                TextBoxIdUsuario.Clear()
                ComboBoxTipoUsuario.SelectedIndex = -1
                ComboBoxEstadoUsuario.SelectedIndex = -1

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

    Private Sub ListBox1_DoubleClick(sender As Object, e As EventArgs) Handles ListBox1.DoubleClick
        If ListBox1.SelectedIndex = -1 Then Exit Sub

        If ListBox1.SelectedValue Is Nothing Then Exit Sub

        Dim idUsuario As Integer = Convert.ToInt32(ListBox1.SelectedValue)

        getDatos()
        CargarSucursalesUsuario(idUsuario)
    End Sub
End Class