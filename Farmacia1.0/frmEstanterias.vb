Imports System.Data.SqlClient
Public Class frmEstanterias

    Dim RegOAct As Integer = 1
    Dim correlativo As String = "SELECT IDENT_CURRENT ('ESTANTERIA') AS Current_Identity FROM ESTANTERIA"
    Dim sqlEstanterias As String = "SELECT idEstanteria FROM ESTANTERIA" 

    Dim fila As Integer

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

    Private Sub frmEstanterias_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ListBox1.DataSource = updateList(sqlEstanterias)
        ListBox1.ValueMember = updateList(sqlEstanterias).Columns(0).ToString
        ListBox1.DisplayMember = updateList(sqlEstanterias).Columns(0).ToString
        ListBox1.Select()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        TextBox1.Text = getCorrelativoTrasiego(correlativo) + 1

        RegOAct = 1
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Close()
    End Sub


    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim cmd As SqlCommand
        Dim sql As String = "DELETE FROM ESTANTERIA WHERE idEstanteria = @id"

        cmd = New SqlCommand(sql, conn)
        cmd.Parameters.AddWithValue("id", CInt(TextBox1.Text))

        Try
            openConnection()
            cmd.ExecuteNonQuery()
            MessageBox.Show("El registro se borró correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MsgBox("Hubo un error al eliminar los datos." & vbCrLf & "Error: " & ex.Message, MsgBoxStyle.Critical, "Algo salió mal")
        Finally
            closeConnection()
            With ListBox1
                .DataSource = updateList(sqlEstanterias)
                .ValueMember = updateList(sqlEstanterias).Columns(0).ToString
            End With
            ListBox1.Select()
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If RegOAct = 1 Then
            If MessageBox.Show("¿Desea guardar este registro?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then


                Dim sql As String = "INSERT INTO ESTANTERIA DEFAULT VALUES"
                Dim cmd As SqlCommand
                cmd = New SqlCommand(sql, conn)

                Try
                    openConnection()
                    cmd.ExecuteNonQuery()
                    MessageBox.Show("El registro guardó correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    TextBox1.Clear()


                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Algo salió mal", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    closeConnection()

                    TextBox1.Clear()
                    With ListBox1
                        .DataSource = updateList(sqlEstanterias)
                        .ValueMember = updateList(sqlEstanterias).Columns(0).ToString
                    End With
                    ListBox1.Select()
                End Try
            End If


            RegOAct = 1
            ListBox1.Select()
        
        End If
    End Sub

   
    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        Try
            TextBox1.Text = CInt(ListBox1.SelectedValue.ToString)
        Catch ex As Exception

        End Try
    End Sub
End Class