Imports System.Data.SqlClient
Public Class frmProveedores

    Dim RegOAct As Integer = 0
    Dim sqlProveedores As String = "SELECT idProveedor, rzProveedor FROM PROVEEDOR"

    Sub getDatos()
        Dim sql As String = "SELECT * FROM PROVEEDOR WHERE idProveedor = @id"
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
                TextBox4.Text = reader(4)
                TextBox5.Text = reader(5)
                TextBox6.Text = reader(6)
                TextBox7.Text = reader(7)
            End If
            reader.Close()
        Catch ex As Exception
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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
        TextBox6.Clear()
        TextBox7.Clear()
        TextBox8.Text = "-AUTOMATICO-"
        TextBox1.Select()
        RegOAct = 1
    End Sub

    Private Sub frmProveedores_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ListBox1.DataSource = updateList(sqlProveedores)
        ListBox1.ValueMember = updateList(sqlProveedores).Columns(0).ToString
        ListBox1.DisplayMember = updateList(sqlProveedores).Columns(1).ToString
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        getDatos()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If RegOAct = 1 Then

            If Trim(TextBox1.Text) = "" Or Trim(TextBox2.Text) = "" Or Trim(TextBox3.Text) = "" Then
                MsgBox("Todos los campos son obligatorios", MsgBoxStyle.Information, "Faltan datos")
            Else
                If MessageBox.Show("¿Desea guardar este registro?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then


                    Dim sql As String = "INSERT INTO PROVEEDOR VALUES(@n, @rz, @dir, @tel, @mail, @c, @tc)"
                    Dim cmd As SqlCommand
                    cmd = New SqlCommand(sql, conn)

                    cmd.Parameters.AddWithValue("n", Trim(TextBox1.Text))
                    cmd.Parameters.AddWithValue("rz", Trim(TextBox2.Text))
                    cmd.Parameters.AddWithValue("dir", Trim(TextBox3.Text))
                    cmd.Parameters.AddWithValue("tel", Trim(TextBox4.Text))
                    cmd.Parameters.AddWithValue("mail", Trim(TextBox5.Text))
                    cmd.Parameters.AddWithValue("c", Trim(TextBox6.Text))
                    cmd.Parameters.AddWithValue("tc", Trim(TextBox7.Text))
                    Try
                        openConnection()
                        cmd.ExecuteNonQuery()
                        MessageBox.Show("El registro guardó correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        TextBox1.Clear()
                        TextBox2.Clear()
                        TextBox3.Clear()
                        TextBox4.Clear()
                        TextBox5.Clear()
                        TextBox6.Clear()
                        TextBox7.Clear()
                        TextBox8.Clear()
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "Algo salió mal", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Finally
                        closeConnection()
                        With ListBox1
                            .DataSource = updateList(sqlProveedores)
                            .ValueMember = updateList(sqlProveedores).Columns(0).ToString
                        End With

                    End Try
                End If
            End If

            RegOAct = 0
            ListBox1.Select()
        Else
            If MessageBox.Show("¿Desea guardar los cambios de este registro?", "Guardar cambios", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim sqlupdate As String = "UPDATE PROVEEDOR SET nitProveedor = @n, rzProveedor = @rz, dirProveedor = @dir, telProveedor = @tel, mailProveedor = @mail, contactoProveedor = @c, telContacto = @tc  WHERE idProveedor = @id"
                Dim cmd As SqlCommand
                cmd = New SqlCommand(sqlupdate, conn)
                cmd.Parameters.AddWithValue("n", Trim(TextBox1.Text))
                cmd.Parameters.AddWithValue("rz", Trim(TextBox2.Text))
                cmd.Parameters.AddWithValue("dir", Trim(TextBox3.Text))
                cmd.Parameters.AddWithValue("id", Trim(TextBox8.Text))
                cmd.Parameters.AddWithValue("tel", Trim(TextBox4.Text))
                cmd.Parameters.AddWithValue("mail", Trim(TextBox5.Text))
                cmd.Parameters.AddWithValue("c", Trim(TextBox6.Text))
                cmd.Parameters.AddWithValue("tc", Trim(TextBox7.Text))

                Try
                    openConnection()
                    cmd.ExecuteNonQuery()
                    TextBox1.Clear()
                    TextBox2.Clear()
                    TextBox3.Clear()
                    TextBox4.Clear()
                    TextBox5.Clear()
                    TextBox6.Clear()
                    TextBox7.Clear()
                    TextBox8.Clear()
                    MessageBox.Show("La información del proveedor se actualizó de forma correcta", "Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Catch ex As Exception
                    MessageBox.Show("Algo salió mal" & vbCrLf & "Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Finally
                    closeConnection()
                    With ListBox1
                        .DataSource = updateList(sqlProveedores)
                        .ValueMember = updateList(sqlProveedores).Columns(0).ToString
                    End With
                    ListBox1.Select()
                End Try
            End If
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If MessageBox.Show("¿Desea eliminar este registro?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim sqldelete As String = "DELETE FROM PROVEEDOR WHERE idProveedor = @id"
            Dim comand As SqlCommand

            comand = New SqlCommand(sqldelete, conn)
            comand.Parameters.AddWithValue("id", CInt(TextBox8.Text))
            Try
                openConnection()
                comand.ExecuteNonQuery()
                MessageBox.Show("El registro se eliminó de forma correcta", "Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information)
                TextBox1.Clear()
                TextBox2.Clear()
                TextBox3.Clear()
                TextBox4.Clear()
                TextBox5.Clear()
                TextBox6.Clear()
                TextBox7.Clear()
                TextBox8.Clear()
            Catch ex As Exception
                MessageBox.Show("Algo salió mal" & vbCrLf & "Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Finally
                closeConnection()
                With ListBox1
                    .DataSource = updateList(sqlProveedores)
                    .ValueMember = updateList(sqlProveedores).Columns(0).ToString
                End With
                ListBox1.Select()
            End Try
        End If
    End Sub
End Class