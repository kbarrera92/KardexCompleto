Imports System.Data.SqlClient

Public Class FormRecibirTraslado
    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            GetInfoTraslado(Integer.Parse(TextBox1.Text), "B")
        End If
    End Sub

    Private Sub GetInfoTraslado(ByVal idTraslado As Integer, ByVal opt As Char)
        Dim cmd As SqlCommand
        Dim da As SqlDataAdapter
        Dim dt As New DataTable

        Try
            openConnection()
            cmd = New SqlCommand()

            With cmd
                cmd.CommandText = "sp_RecibeTraslado"
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Connection = conn
                cmd.Parameters.AddWithValue("OPT", "B")
                cmd.Parameters.AddWithValue("IDTRASLADO", idTraslado)
                cmd.Parameters.Add("USUARIOENVIA", SqlDbType.VarChar, 50)
                cmd.Parameters("USUARIOENVIA").Direction = ParameterDirection.Output
                cmd.Parameters.Add("FECHAENVIA", SqlDbType.Date)
                cmd.Parameters("FECHAENVIA").Direction = ParameterDirection.Output
                cmd.Parameters.Add("SUCENVIA", SqlDbType.VarChar, 50)
                cmd.Parameters("SUCENVIA").Direction = ParameterDirection.Output
                cmd.Parameters.Add("MSG", SqlDbType.VarChar, 200)
                cmd.Parameters("MSG").Direction = ParameterDirection.Output
            End With

            da = New SqlDataAdapter(cmd)
            da.Fill(dt)

            For i = 0 To dt.Columns.Count - 1
                DataGridView1.Columns(i).DataPropertyName = dt.Columns(i).ToString
            Next

            If dt.Rows.Count > 0 Then
                If cmd.Parameters("SUCENVIA").Value.ToString() = nameSucActual Then
                    MessageBox.Show("No se puede recibir el traslado en la misma sucursal", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If

                TextBoxUsuarioEnvia.Text = cmd.Parameters("USUARIOENVIA").Value.ToString()
                TextBoxSucursalEnvia.Text = cmd.Parameters("SUCENVIA").Value.ToString()

                TextBoxUsuarioRecibe.Text = nameUsuarioActual
                TextBoxSucursalRecibe.Text = nameSucActual

                TextBoxFechaEnvia.Text = cmd.Parameters("FECHAENVIA").Value.ToString()
                TextBoxFechaRecibe.Text = DateTime.Now.ToShortDateString()

                dv = dt.DefaultView
                DataGridView1.DataSource = dv
            Else
                MessageBox.Show("No se encontraron resultados", "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            MsgBox("Error al cargar los datos" & vbCrLf & "Error: " & ex.ToString)
        Finally
            closeConnection()
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If MessageBox.Show("¿Confirmar Traslado?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
            Return

        End If

        Dim cmd As SqlCommand
        Try
            cmd = New SqlCommand()

            With cmd
                cmd.CommandText = "sp_RecibeTraslado"
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Connection = conn
                cmd.Parameters.AddWithValue("OPT", "R")
                cmd.Parameters.AddWithValue("IDTRASLADO", CInt(TextBox1.Text))
                cmd.Parameters.Add("USUARIOENVIA", SqlDbType.VarChar, 50)
                cmd.Parameters("USUARIOENVIA").Direction = ParameterDirection.Output
                cmd.Parameters.Add("FECHAENVIA", SqlDbType.Date)
                cmd.Parameters("FECHAENVIA").Direction = ParameterDirection.Output
                cmd.Parameters.Add("SUCENVIA", SqlDbType.VarChar, 50)
                cmd.Parameters("SUCENVIA").Direction = ParameterDirection.Output
                cmd.Parameters.Add("MSG", SqlDbType.VarChar, 200)
                cmd.Parameters("MSG").Direction = ParameterDirection.Output
            End With


            openConnection()
            cmd.ExecuteNonQuery()
            MessageBox.Show("Se recibió correctamente el traslado", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Limpiar()

            Dim params(3) As String
            params(0) = nameUsuarioActual
            params(1) = Environment.MachineName & " - " & Environment.UserName
            params(2) = String.Format("{0} recibió traslado No. {1}, en la sucursal: {2}", nameUsuarioActual, TextBox1.Text, ConsultaParametro("sucursalFisica"))

            GrabaBitacora(params, grabaBitacoraSp)
        Catch ex As Exception
            MessageBox.Show("Hubo un error al recibir el traslado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Limpiar()
        For Each control As Control In Me.Controls
            If TypeOf control Is TextBox Then
                control.Text = "" ' eliminar el texto  
            End If
        Next

        DataGridView1.DataSource = Nothing
        TextBox1.Focus()
    End Sub
End Class