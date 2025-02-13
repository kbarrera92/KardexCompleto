Imports System.Data.SqlClient

Public Class FormAbrirCaja
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim res As Short

        If Not Integer.TryParse(txtcodempleado.Text, Nothing) Then
            MessageBox.Show("El código de empleado debe ser numérico", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If Not Decimal.TryParse(txtsaldoinicial.Text, Nothing) Then
            MessageBox.Show("El saldo inicial no es correcto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If MessageBox.Show("¿Desea aperturar la caja?", "Abriendo caja", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
            Return
        End If

        res = AbreTurno()
        If res = 0 Then
            Close()
        End If

    End Sub

    Function AbreTurno() As Short
        Dim cmd As SqlCommand
        Dim msg As String
        Dim rc As Integer
        Try
            cmd = New SqlCommand()
            With cmd
                .CommandText = "sp_mantAperturasCajas"
                .CommandType = CommandType.StoredProcedure
                .Connection = conn
            End With

            With cmd.Parameters
                .AddWithValue("opt", "A")
                .AddWithValue("usuario", usuarioActual)
                .AddWithValue("sucursal", If(ConsultaParametro("codigoSucursal") = sucActual, sucActual, Convert.ToInt32(ConsultaParametro("codigoSucursal"))))
                .AddWithValue("saldoinicial", CDbl(txtsaldoinicial.Text))
                .AddWithValue("codempleado", CInt(txtcodempleado.Text))
                .Add("@MSG", SqlDbType.VarChar, 200).Direction = ParameterDirection.Output
                .Add("@rc", SqlDbType.Int).Direction = ParameterDirection.Output
            End With

            openConnection()
            cmd.ExecuteNonQuery()
            rc = CInt(cmd.Parameters("@rc").Value)
            msg = CStr(cmd.Parameters("@MSG").Value.ToString())
            MessageBox.Show(msg, If(rc = 0, "Éxito", "Error"), MessageBoxButtons.OK, If(rc = 0, MessageBoxIcon.Information, MessageBoxIcon.Error))
            closeConnection()
        Catch ex As Exception
            MessageBox.Show($"Hubo un error al grabar el registro. {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            rc = -3
        End Try

        Return rc
    End Function

End Class