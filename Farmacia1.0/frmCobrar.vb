Imports System.Data.SqlClient

Public Class frmCobrar

    Dim sql As String = "SELECT idSerie, letra FROM SERIEFACTURA WHERE sucursal = " & sucActual

    Function GrabaVenta(ByVal table As DataTable) As Boolean
        Dim cmd As SqlCommand
        Dim msg As String
        Dim rc As Integer, nventa As Integer
        Try
            cmd = New SqlCommand()
            With cmd
                .CommandText = "grabaVenta1"
                .CommandType = CommandType.StoredProcedure
                .Connection = conn
            End With

            With cmd.Parameters
                .AddWithValue("usuario", usuarioActual)
                .AddWithValue("sucursal", If(ConsultaParametro("codigoSucursal") = sucActual, sucActual, Convert.ToInt32(ConsultaParametro("codigoSucursal"))))
                .AddWithValue("cliente", Trim(txtnit.Text))
                .AddWithValue("documento", If(Trim(txtFactura.Text) = "", DBNull.Value, CInt(txtFactura.Text)))
                .AddWithValue("total", CDbl(txttotal.Text))
                .AddWithValue("efectivo", (CDbl(txtpago.Text) + CDbl(txttarjeta.Text)) - CDbl(txtcambio.Text))
                .AddWithValue("tarjeta", CDbl(txttarjeta.Text))
                .AddWithValue("autoriza", txtautori.Text)
                .AddWithValue("codempleado", CInt(TextBoxCodigoEmpleado.Text))
                .Add("@MSG", SqlDbType.VarChar, 200).Direction = ParameterDirection.Output
                .Add("@rc", SqlDbType.Int).Direction = ParameterDirection.Output
                .Add("@nVenta", SqlDbType.Int).Direction = ParameterDirection.Output
                .AddWithValue("detalles", table)
                .AddWithValue("horasDiferencia", CInt(ConsultaParametro("horasDiferencia")))
            End With

            openConnection()
            cmd.ExecuteNonQuery()
            rc = CInt(cmd.Parameters("@rc").Value)
            msg = cmd.Parameters("@MSG").Value.ToString()
            nventa = CInt(If(cmd.Parameters("@nVenta").Value, 0))
            MessageBox.Show(msg, If(rc = 0, "Éxito", "Error"), MessageBoxButtons.OK, If(rc = 0, MessageBoxIcon.Information, MessageBoxIcon.Error))
            closeConnection()
            ImprimeTicket(nventa)
        Catch ex As Exception
            MessageBox.Show($"Hubo un error al grabar la venta. {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            rc = -3
        End Try

        Return If(rc = 0, True, False)
    End Function



    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If MessageBox.Show("¿Desea cancelar esta acción?", "Cancelar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Me.Close()
            frmPuntoDeVentaMejorado.Select()

        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Not Integer.TryParse(TextBoxCodigoEmpleado.Text, Nothing) Then
            MessageBox.Show("El código de empleado debe ser numérico", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim res As Boolean
        If Val(txtcambio.Text) >= 0 Then
            If MessageBox.Show("¿Desea guardar esta venta?", "Guardando", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                If saveClient = True Then
                    saveinfoclient()
                    saveClient = False
                End If
                If Me.CheckBox1.Checked = True Then
                    res = GrabaVenta(table)
                    guardarFactura()
                Else
                    res = GrabaVenta(table)

                End If

                If pv = 1 And res = True Then
                    frmPuntoDeVentaMejorado.cleanAll()
                    Me.Close()

                End If

            End If
        Else
            MessageBox.Show("Datos invalidos", "Datos no válidos", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub txtpago_TextChanged(sender As Object, e As EventArgs) Handles txtpago.TextChanged
        txtcambio.Text = String.Format("{0:N2}", (Val(txtpago.Text) + Val(txttarjeta.Text)) - Val(txttotal.Text))
        If (Val(txtpago.Text) + Val(txttarjeta.Text)) >= Val(txttotal.Text) Then
            txtcambio.BackColor = Color.Blue
        Else
            txtcambio.BackColor = Color.Red
        End If
    End Sub

    Private Sub frmCobrar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtpago.Text = 0.0
        txttarjeta.Text = 0.0

        txtfecha.Text = DateTime.Now.ToShortDateString
        txtusuario.Text = nameUsuarioActual
        txtsucursal.Text = sucActual

        ComboBox1.DataSource = updateCm(sql)
        ComboBox1.DisplayMember = updateCm(sql).Columns(1).ToString
        ComboBox1.ValueMember = updateCm(sql).Columns(0).ToString

        txtnit.Text = "C/F"
        txtnombrecliente.Text = "CONSUMIDOR FINAL"
        txtdircliente.Text = "CIUDAD"
    End Sub

    Private Sub txttotal_TextChanged(sender As Object, e As EventArgs) Handles txttotal.TextChanged
        txtcambio.Text = String.Format("{0:N2}", (Val(txtpago.Text) - Val(txttotal.Text)))
    End Sub

    Private Sub txtcambio_TextChanged(sender As Object, e As EventArgs) Handles txtcambio.TextChanged
        If Val(txtpago.Text) >= Val(txttotal.Text) Then
            txtcambio.BackColor = Color.Blue
        Else
            txtcambio.BackColor = Color.Red
        End If
    End Sub

    Private Sub txttarjeta_TextChanged(sender As Object, e As EventArgs) Handles txttarjeta.TextChanged
        txtcambio.Text = (Val(txtpago.Text) + Val(txttarjeta.Text)) - Val(txttotal.Text)
        If (Val(txtpago.Text) + Val(txttarjeta.Text)) >= Val(txttotal.Text) Then
            txtcambio.BackColor = Color.Blue
        Else
            txtcambio.BackColor = Color.Red
        End If
    End Sub

    Private Sub txtnit_KeyDown(sender As Object, e As KeyEventArgs) Handles txtnit.KeyDown
        If e.KeyCode = Keys.Enter Then
            If txtnit.Text = "C/F" Then
                txtnombrecliente.Select()
            Else
                If Trim(txtnit.Text) = "" Then
                    txtnit.Text = "C/F"
                    txtnombrecliente.Select()
                Else
                    Dim reader As SqlDataReader
                    Try
                        openConnection()
                        Dim query As String = "SELECT rzCliente, direccionCliente FROM CLIENTE WHERE nitCliente = @nit;"
                        Dim cmd As New SqlCommand(query, conn)
                        cmd.Parameters.AddWithValue("nit", Trim(txtnit.Text))

                        reader = cmd.ExecuteReader
                        reader.Read()

                        If reader.HasRows Then
                            txtnombrecliente.Text = reader(0)
                            txtdircliente.Text = reader(1)
                        Else
                            saveClient = True
                            txtnombrecliente.Select()
                            reader.Close()
                        End If


                    Catch ex As Exception
                        MsgBox("Error en la conexión a la Base de datos" & vbCrLf & ex.ToString)
                    Finally
                        closeConnection()
                    End Try
                End If

            End If
        End If
    End Sub


    Private Sub txtpago_KeyDown(sender As Object, e As KeyEventArgs) Handles txtpago.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBoxCodigoEmpleado.Select()
        End If
    End Sub

    Private Sub TextBoxCodigoEmpleado_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBoxCodigoEmpleado.KeyDown
        If e.KeyCode = Keys.Enter Then
            Button1.PerformClick()
        End If
    End Sub
End Class