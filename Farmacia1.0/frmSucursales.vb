Imports System.Data.SqlClient
Public Class frmSucursales

    Dim sql As String = "SELECT idSucursal, nombreSuc FROM SUCURSAL"

    Dim RegOAct As Integer = 0
    Dim correlativo As String = "SELECT IDENT_CURRENT ('SUCURSAL') AS Current_Identity"

    Sub crearInventario()
        Dim sql As String = "INSERT INTO INVENTARIO VALUES(@idin)"
        Dim cmd As SqlCommand
        Try
            cmd = New SqlCommand(sql, conn)

            cmd.Parameters.AddWithValue("idin", Trim(TextBox1.Text))

            openConnection()
            cmd.ExecuteNonQuery()
            

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Algo salió mal", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            closeConnection()

        End Try
    End Sub

    Sub getDatos()
        Dim sql As String = "SELECT * FROM SUCURSAL WHERE idSucursal = @id"
        Dim cmd As SqlCommand

        cmd = New SqlCommand(sql, conn)
        Try
            cmd.Parameters.AddWithValue("id", ListBox1.SelectedValue.ToString)


            openConnection()
            Dim reader As SqlDataReader = cmd.ExecuteReader
            reader.Read()

            If reader.HasRows Then
                TextBox1.Text = reader(0)
                TextBox2.Text = reader(1)
                TextBox3.Text = reader(2)
                TextBox4.Text = reader(3)
            End If
            reader.Close()
        Catch ex As Exception
        Finally
            closeConnection()
        End Try
    End Sub


    Private Sub frmSucursales_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ListBox1.DataSource = updateCm(sql)
        ListBox1.DisplayMember = updateCm(sql).Columns(1).ToString
        ListBox1.ValueMember = updateCm(sql).Columns(0).ToString

        Estilos.AplicarEstilos(Me)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Close()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TextBox1.Text = getCorrelativoTrasiego(correlativo) + 1
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()

        TextBox2.Select()
        RegOAct = 1
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If RegOAct = 1 Then

            If Trim(TextBox1.Text) = "" Or Trim(TextBox2.Text) = "" Or Trim(TextBox3.Text) = "" Then
                MsgBox("Todos los campos son obligatorios", MsgBoxStyle.Information, "Faltan datos")
            Else
                If MessageBox.Show("¿Desea guardar este registro?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                    crearInventario()
                    Dim sql As String = "INSERT INTO SUCURSAL VALUES(@nsuc, @dirsuc, @telsuc)"
                    Dim cmd As SqlCommand
                    Try
                        cmd = New SqlCommand(sql, conn)

                        cmd.Parameters.AddWithValue("nsuc", Trim(TextBox2.Text))
                        cmd.Parameters.AddWithValue("dirsuc", Trim(TextBox3.Text))
                        cmd.Parameters.AddWithValue("telsuc", Trim(TextBox4.Text))


                        openConnection()
                        cmd.ExecuteNonQuery()
                        MessageBox.Show("El registro guardó correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        TextBox1.Clear()
                        TextBox2.Clear()
                        TextBox3.Clear()

                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "Algo salió mal", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Finally
                        closeConnection()
                        With ListBox1
                            .DataSource = updateCm(sql)
                            .ValueMember = updateCm(sql).Columns(0).ToString
                        End With

                    End Try
                End If
            End If

            RegOAct = 0
            ListBox1.Select()
        Else
            If MessageBox.Show("¿Desea guardar los cambios de este registro?", "Guardar cambios", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim sqlupdate As String = "UPDATE SUCURSAL SET nombreSuc = @c, direccionSuc = @un, telefono = @telsuc WHERE idSucursal = @id"
                Dim cmd As SqlCommand
                cmd = New SqlCommand(sqlupdate, conn)

                cmd.Parameters.AddWithValue("c", Trim(TextBox2.Text))
                cmd.Parameters.AddWithValue("un", Trim(TextBox3.Text))
                cmd.Parameters.AddWithValue("telsuc", Trim(TextBox4.Text))
                cmd.Parameters.AddWithValue("id", CInt(TextBox1.Text))

                Try
                    openConnection()
                    cmd.ExecuteNonQuery()
                    TextBox1.Clear()
                    TextBox2.Clear()
                    TextBox3.Clear()
                    TextBox4.Clear()

                    MessageBox.Show("La información de la medida se actualizó de forma correcta", "Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Catch ex As Exception
                    MessageBox.Show("Algo salió mal" & vbCrLf & "Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Finally
                    closeConnection()
                    With ListBox1
                        .DataSource = updateCm(sql)
                        .ValueMember = updateCm(sql).Columns(0).ToString
                    End With
                    ListBox1.Select()
                End Try
            End If
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If MessageBox.Show("¿Desea eliminar este registro?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim sqldelete As String = "DELETE FROM SUCURSAL WHERE idSucursal = @id"
            Dim comand As SqlCommand

            comand = New SqlCommand(sqldelete, conn)
            comand.Parameters.AddWithValue("id", CInt(TextBox1.Text))
            Try
                openConnection()
                comand.ExecuteNonQuery()
                MessageBox.Show("El registro se eliminó de forma correcta", "Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information)
                TextBox1.Clear()
                TextBox2.Clear()
                TextBox3.Clear()
                TextBox4.Clear()
            Catch ex As Exception
                MessageBox.Show("Algo salió mal" & vbCrLf & "Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Finally
                closeConnection()
                With ListBox1
                    .DataSource = updateCm(sql)
                    .ValueMember = updateCm(sql).Columns(0).ToString
                End With
                ListBox1.Select()
            End Try
        End If
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        getDatos()
    End Sub
End Class