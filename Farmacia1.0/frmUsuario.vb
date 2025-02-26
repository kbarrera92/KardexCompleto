Imports System.Data.SqlClient

Public Class frmUsuario

    Dim RegOAct As Integer = 0
    Dim correlativo As String = "SELECT IDENT_CURRENT ('USUARIO') AS Current_Identity"
    Dim sqlUsuarios As String = "SELECT idUsuario, nombreUsuario FROM USUARIO U INNER JOIN TIPOUSUARIO TU ON U.tipoUsuario = TU.idTipoUsuario " _
                                & "WHERE TU.nombreTipo <> 'GERENTE' AND U.estado = 1"
    Dim sqlSuc As String = "SELECT idSucursal, nombreSuc FROM SUCURSAL"
    Dim sqlEstado As String = "SELECT idEstado, estadoUsuario FROM ESTADOUSUARIO"

    Sub getDatos()
        Dim ind, ind2, ind3 As Integer
        Dim sql As String = "SELECT U.idUsuario, U.nombreUsuario, U.nick, U.contraUsuario, TU.nombreTipo, S.nombreSuc, EU.estadoUsuario FROM USUARIO U " _
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
        If RegOAct = 1 Then

            If Trim(TextBox1.Text) = "" Or Trim(TextBox2.Text) = "" Then
                MsgBox("Todos los campos son obligatorios", MsgBoxStyle.Information, "Faltan datos")
            Else
                If MessageBox.Show("¿Desea guardar este registro?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then


                    Dim sql As String = "INSERT INTO USUARIO VALUES(@n, @nick, @c, @tu, @suc, @estado)"
                    Dim cmd As SqlCommand
                    cmd = New SqlCommand(sql, conn)

                    cmd.Parameters.AddWithValue("n", Trim(TextBox1.Text))
                    cmd.Parameters.AddWithValue("nick", Trim(TextBox2.Text))
                    cmd.Parameters.AddWithValue("c", Trim(TextBox3.Text))
                    cmd.Parameters.AddWithValue("tu", CInt(ComboBox1.SelectedValue.ToString))
                    cmd.Parameters.AddWithValue("suc", CInt(ComboBox2.SelectedValue.ToString))
                    cmd.Parameters.AddWithValue("estado", CInt(ComboBox3.SelectedValue.ToString))

                    Try
                        openConnection()
                        cmd.ExecuteNonQuery()
                        MessageBox.Show("El registro guardó correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        TextBox1.Clear()
                        TextBox2.Clear()
                        TextBox3.Clear()
                        TextBox8.Clear()
                        ComboBox1.SelectedIndex = -1
                        ComboBox2.SelectedIndex = -1
                        ComboBox3.SelectedIndex = -1
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "Algo salió mal", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Finally
                        closeConnection()
                        With ListBox1
                            .DataSource = updateList(sqlUsuarios)
                            .ValueMember = updateList(sqlUsuarios).Columns(0).ToString
                        End With

                    End Try
                End If
            End If

            RegOAct = 0
            ListBox1.Select()
        Else
            If MessageBox.Show("¿Desea guardar los cambios de este registro?", "Guardar cambios", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim sqlupdate As String = "UPDATE USUARIO SET nombreUsuario = @n, nick = @nick, contraUsuario = @c, tipoUsuario = @tu, sucursal = @sucu, estado = @es WHERE idUsuario = @id"
                Dim cmd As SqlCommand
                cmd = New SqlCommand(sqlupdate, conn)

                cmd.Parameters.AddWithValue("n", Trim(TextBox1.Text))
                cmd.Parameters.AddWithValue("nick", Trim(TextBox2.Text))
                cmd.Parameters.AddWithValue("c", Trim(TextBox3.Text))
                cmd.Parameters.AddWithValue("tu", CInt(ComboBox1.SelectedValue.ToString))
                cmd.Parameters.AddWithValue("sucu", CInt(ComboBox2.SelectedValue.ToString))
                cmd.Parameters.AddWithValue("es", CInt(ComboBox3.SelectedValue.ToString))

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

                    MessageBox.Show("La información del usuario se actualizó de forma correcta", "Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
        End If
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
End Class