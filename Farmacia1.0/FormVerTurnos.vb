Imports System.Data.SqlClient

Public Class FormVerTurnos
    Private Sub FormVerTurnos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ListaTurnos()

    End Sub

    Private Sub ListaTurnos()
        Dim cmd As SqlCommand
        Dim adapter As SqlDataAdapter
        Dim datatable As DataTable
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
                .AddWithValue("opt", "B")
                .Add("@MSG", SqlDbType.VarChar, 200).Direction = ParameterDirection.Output
                .Add("@rc", SqlDbType.Int).Direction = ParameterDirection.Output
            End With

            adapter = New SqlDataAdapter(cmd)
            datatable = New DataTable

            openConnection()
            adapter.Fill(datatable)
            rc = CInt(cmd.Parameters("@rc").Value)
            msg = cmd.Parameters("@MSG").Value.ToString()
            DataGridView1.DataSource = datatable
            closeConnection()
        Catch ex As SqlException
            MessageBox.Show($"Hubo un error en BD al grabar la venta. {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            rc = -2
        Catch ex As Exception
            MessageBox.Show($"Hubo un error al grabar la venta. {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            rc = -3
        End Try

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If DataGridView1.CurrentRow Is Nothing Then
            MessageBox.Show($"No se ha seleccionado ningún registro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim diferencia As Decimal, totalfisico As Decimal, totalsistema As Decimal, inicial As Decimal
        diferencia = If(IsDBNull(DataGridView1.CurrentRow.Cells(7).Value), 0D, Convert.ToDecimal(DataGridView1.CurrentRow.Cells(7).Value))
        totalfisico = If(IsDBNull(DataGridView1.CurrentRow.Cells(6).Value), 0D, Convert.ToDecimal(DataGridView1.CurrentRow.Cells(6).Value))
        totalsistema = If(IsDBNull(DataGridView1.CurrentRow.Cells(5).Value), 0D, Convert.ToDecimal(DataGridView1.CurrentRow.Cells(5).Value))
        inicial = If(IsDBNull(DataGridView1.CurrentRow.Cells(4).Value), 0D, Convert.ToDecimal(DataGridView1.CurrentRow.Cells(4).Value))
        ImprimeTicketCuadre(inicial, totalsistema, totalfisico, diferencia)
    End Sub
End Class