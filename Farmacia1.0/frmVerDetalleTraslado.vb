Imports System.Data.SqlClient
Imports System.Globalization

Public Class frmVerDetalleTraslado

    Dim cult As CultureInfo = CultureInfo.InvariantCulture

    Sub updateDet()
        Dim sqlup As String = "UPDATE DETSALXTRASLADO SET cantidad = @cant WHERE nsalxtraslado = @nv AND ndetxtraslado = @nd"
        Dim cmd As SqlCommand
        Try
            cmd = New SqlCommand(sqlup, conn)

            cmd.Parameters.AddWithValue("cant", CInt(DataGridView1.CurrentRow.Cells(2).Value))
            cmd.Parameters.AddWithValue("nv", CInt(TextBox1.Text))
            cmd.Parameters.AddWithValue("nd", CInt(DataGridView1.CurrentRow.Cells(0).Value))

            openConnection()
            cmd.ExecuteNonQuery()
            closeConnection()
            MsgBox("Datos actualizados", MsgBoxStyle.Information, "Éxito")
        Catch ex As Exception
            MsgBox(ex.Message)
            fillDGVSP("getDetalleTraslado", DataGridView1, Me, CInt(TextBox1.Text))
        End Try
    End Sub

    Private Sub frmVerDetalleTraslado_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        fillDGVSP("getDetalleTraslado", DataGridView1, Me, CInt(TextBox1.Text))
    End Sub

    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.Delete Then
            If nombreRol = "VENDEDOR" Then

            Else
                If MessageBox.Show("¿Desea eliminar este detalle?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    Dim sqldel As String = "DELETE DETSALXTRASLADO WHERE ndetxtraslado = @nd AND nsalxtraslado = @nv"
                    Dim cmd As SqlCommand

                    Try
                        cmd = New SqlCommand(sqldel, conn)
                        cmd.Parameters.AddWithValue("nv", CInt(TextBox1.Text))
                        cmd.Parameters.AddWithValue("nd", CInt(DataGridView1.CurrentRow.Cells(0).Value))

                        openConnection()
                        cmd.ExecuteNonQuery()
                        closeConnection()
                    Catch ex As Exception
                        MsgBox("No se pudo eliminar el detalle" & vbCrLf & "Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
                    End Try

                    sqldel = "DELETE DETENTRADAXTRASLADO WHERE ndetentrxtralad = @nd AND nentrxtrasld = @nv"
                    Try
                        cmd = New SqlCommand(sqldel, conn)
                        cmd.Parameters.AddWithValue("nv", CInt(TextBox1.Text))
                        cmd.Parameters.AddWithValue("nd", CInt(DataGridView1.CurrentRow.Cells(0).Value))

                        openConnection()
                        cmd.ExecuteNonQuery()
                        closeConnection()
                        MessageBox.Show("Detalle eliminado", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Catch ex As Exception
                        MsgBox("No se pudo eliminar el detalle" & vbCrLf & "Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
                    Finally
                        fillDGVSP("getDetalleTraslado", DataGridView1, Me, CInt(TextBox1.Text))
                    End Try
                End If
            End If

        End If
    End Sub

    Private Sub DataGridView1_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit
        Try

            DataGridView1.CurrentRow.Cells(4).Value = Convert.ToDecimal(FormatNumber(DataGridView1.CurrentRow.Cells(2).Value * DataGridView1.CurrentRow.Cells(3).Value, 2), cult)
            updateDet()
        Catch ex As Exception

        End Try
    End Sub
End Class