Imports System.Data.SqlClient
Public Class frmMedidas

    Dim RegOAct As Integer = 0
    Dim correlativo As String = "SELECT IDENT_CURRENT ('MEDIDA') AS Current_Identity"
    Dim sqlMedida As String = "SELECT idMedida, CONCAT(cantidad, ' ', unidad) FROM MEDIDA"

    Sub getDatos()
        Dim sql As String = "SELECT * FROM MEDIDA WHERE idMedida = @id"
        Dim cmd As SqlCommand

        cmd = New SqlCommand(sql, conn)
        cmd.Parameters.AddWithValue("id", ListBox1.SelectedValue.ToString)

        Try
            openConnection()
            Dim reader As SqlDataReader = cmd.ExecuteReader
            reader.Read()

            If reader.HasRows Then
                TextBox1.Text = reader(0)
                TextBox2.Text = reader(1)
                TextBox3.Text = reader(2)
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
            Return Nothing
            MsgBox(ex.Message)
        End Try
    End Function

    Private Sub frmMedidas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ListBox1.DataSource = updateList(sqlMedida)
        ListBox1.ValueMember = updateList(sqlMedida).Columns(0).ToString
        ListBox1.DisplayMember = updateList(sqlMedida).Columns(1).ToString
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TextBox1.Text = getCorrelativoTrasiego(correlativo) + 1
        TextBox2.Clear()
        TextBox3.Clear()

        TextBox2.Select()
        RegOAct = 1
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If RegOAct = 1 Then

            If Trim(TextBox1.Text) = "" Or Trim(TextBox2.Text) = "" Or Trim(TextBox3.Text) = "" Then
                MsgBox("Todos los campos son obligatorios", MsgBoxStyle.Information, "Faltan datos")
            Else
                If MessageBox.Show("¿Desea guardar este registro?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then


                    Dim sql As String = "INSERT INTO MEDIDA VALUES(@cant, @un)"
                    Dim cmd As SqlCommand
                    cmd = New SqlCommand(sql, conn)

                    cmd.Parameters.AddWithValue("cant", Trim(TextBox2.Text))
                    cmd.Parameters.AddWithValue("un", Trim(TextBox3.Text))
                    
                    Try
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
                            .DataSource = updateList(sqlMedida)
                            .ValueMember = updateList(sqlMedida).Columns(0).ToString
                        End With

                    End Try
                End If
            End If

            RegOAct = 0
            ListBox1.Select()
        Else
            If MessageBox.Show("¿Desea guardar los cambios de este registro?", "Guardar cambios", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim sqlupdate As String = "UPDATE MEDIDA SET cantidad = @c, unidad = @un WHERE idMedida = @id"
                Dim cmd As SqlCommand
                cmd = New SqlCommand(sqlupdate, conn)

                cmd.Parameters.AddWithValue("c", Trim(TextBox2.Text))
                cmd.Parameters.AddWithValue("un", Trim(TextBox3.Text))
                cmd.Parameters.AddWithValue("id", CInt(TextBox1.Text))

                Try
                    openConnection()
                    cmd.ExecuteNonQuery()
                    TextBox1.Clear()
                    TextBox2.Clear()
                    TextBox3.Clear()
                    
                    MessageBox.Show("La información de la medida se actualizó de forma correcta", "Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Catch ex As Exception
                    MessageBox.Show("Algo salió mal" & vbCrLf & "Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Finally
                    closeConnection()
                    With ListBox1
                        .DataSource = updateList(sqlMedida)
                        .ValueMember = updateList(sqlMedida).Columns(0).ToString
                    End With
                    ListBox1.Select()
                End Try
            End If
        End If
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        getDatos()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If MessageBox.Show("¿Desea eliminar este registro?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim sqldelete As String = "DELETE FROM MEDIDA WHERE idMedida = @id"
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

            Catch ex As Exception
                MessageBox.Show("Algo salió mal" & vbCrLf & "Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Finally
                closeConnection()
                With ListBox1
                    .DataSource = updateList(sqlMedida)
                    .ValueMember = updateList(sqlMedida).Columns(0).ToString
                End With
                ListBox1.Select()
            End Try
        End If
    End Sub
End Class