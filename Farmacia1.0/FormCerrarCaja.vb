Imports System.Data.SqlClient

Public Class FormCerrarCaja
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim res As Short

        If Not Integer.TryParse(txtcodempleado.Text, Nothing) Then
            MessageBox.Show("El código de empleado debe ser numérico", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If Not Decimal.TryParse(txttotalfisico.Text, Nothing) Then
            MessageBox.Show("El efectivo no es correcto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If MessageBox.Show("¿Desea cerrar la caja?", "Abriendo caja", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
            Return
        End If

        res = CierraTurno()
        If res = 0 Then
            Close()
        End If

    End Sub

    Function CierraTurno() As Short
        Dim cmd As SqlCommand
        Dim msg As String
        Dim rc As Integer, inicial As Decimal, totalfisico As Decimal, totalsistema As Decimal, dif As Decimal

        Try
            cmd = New SqlCommand()
            With cmd
                .CommandText = "sp_mantAperturasCajas"
                .CommandType = CommandType.StoredProcedure
                .Connection = conn
            End With

            Dim paramsaldoinicial As New SqlParameter With {
                .ParameterName = "@saldoinicial",
                .SqlDbType = SqlDbType.Decimal,
                .Precision = 10,
                .Scale = 2,
                .Direction = ParameterDirection.Output
            }

            Dim paramtotalfisico As New SqlParameter With {
                .ParameterName = "@totalfisico",
                .SqlDbType = SqlDbType.Decimal,
                .Precision = 10,
                .Scale = 2,
                .Direction = ParameterDirection.InputOutput,
                .Value = CDec(txttotalfisico.Text)
            }

            Dim paramtotalsistema As New SqlParameter With {
                .ParameterName = "@totalsistema",
                .SqlDbType = SqlDbType.Decimal,
                .Precision = 10,
                .Scale = 2,
                .Direction = ParameterDirection.Output
            }

            Dim diferencia As New SqlParameter With {
                .ParameterName = "@diferencia",
                .SqlDbType = SqlDbType.Decimal,
                .Precision = 10,
                .Scale = 2,
                .Direction = ParameterDirection.Output
            }

            With cmd.Parameters
                .AddWithValue("opt", "C")
                .AddWithValue("sucursal", If(ConsultaParametro("codigoSucursal") = sucActual, sucActual, Convert.ToInt32(ConsultaParametro("codigoSucursal"))))
                .Add(diferencia)
                .Add(paramsaldoinicial)
                .Add(paramtotalfisico)
                .Add(paramtotalsistema)
                .AddWithValue("codempleado", CInt(txtcodempleado.Text))
                .Add("@MSG", SqlDbType.VarChar, 200).Direction = ParameterDirection.Output
                .Add("@rc", SqlDbType.Int).Direction = ParameterDirection.Output
            End With

            openConnection()
            cmd.ExecuteNonQuery()
            rc = CInt(cmd.Parameters("@rc").Value)
            msg = cmd.Parameters("@MSG").Value.ToString()
            inicial = CDec(cmd.Parameters("@saldoinicial").Value.ToString())
            dif = CDec(If(cmd.Parameters("@diferencia").Value = Nothing, 0, cmd.Parameters("@diferencia").Value.ToString()))
            totalfisico = CDec(If(cmd.Parameters("@totalfisico").Value = Nothing, 0, cmd.Parameters("@totalfisico").Value.ToString()))
            totalsistema = CDec(If(cmd.Parameters("@totalsistema").Value = Nothing, 0, cmd.Parameters("@totalsistema").Value.ToString()))

            MessageBox.Show(msg, If(rc = 0, "Éxito", "Error"), MessageBoxButtons.OK, If(rc = 0, MessageBoxIcon.Information, MessageBoxIcon.Error))
            closeConnection()
        Catch ex As Exception
            MessageBox.Show($"Hubo un error al grabar el registro. {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            rc = -3
        End Try

        Return rc
    End Function

End Class