Imports System.Data.SqlClient

Public Class frmCobrar

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
                .AddWithValue("total", LeerMonto(txttotal))
                .AddWithValue("efectivo", LeerMonto(txttotal) - LeerMonto(txttarjeta))
                .AddWithValue("tarjeta", LeerMonto(txttarjeta))
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
            If rc <> 0 Then
                closeConnection()
                Return False
            End If
            closeConnection()
            ImprimeTicket(nventa)
            If nombreRol = "ADMINISTRADOR" Then
                DibujaTarjetasResumen()
            End If
        Catch ex As Exception
            MessageBox.Show($"Hubo un error al grabar la venta. {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            rc = -3
        End Try

        Return If(rc = 0, True, False)
    End Function



    Private Function LeerMonto(tb As TextBox) As Decimal
        Dim valor As Decimal
        If Decimal.TryParse(tb.Text, Globalization.NumberStyles.Number, Globalization.CultureInfo.CurrentCulture, valor) Then
            Return valor
        End If
        Return 0D
    End Function

    Private Sub RecalcularCambio()
        Dim total As Decimal = LeerMonto(txttotal)
        Dim pago As Decimal = LeerMonto(txtpago)
        Dim tarjeta As Decimal = LeerMonto(txttarjeta)
        txtcambio.Text = (pago + tarjeta - total).ToString("N2")
        If pago + tarjeta >= total AndAlso tarjeta <= total Then
            txtcambio.BackColor = Color.Blue
        Else
            txtcambio.BackColor = Color.Red
        End If
    End Sub

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

        Dim total As Decimal = LeerMonto(txttotal)
        Dim pago As Decimal = LeerMonto(txtpago)
        Dim tarjeta As Decimal = LeerMonto(txttarjeta)

        If pago < 0 OrElse tarjeta < 0 Then
            MessageBox.Show("Los montos no pueden ser negativos", "Datos no válidos", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        If tarjeta > total Then
            MessageBox.Show("El monto con tarjeta no puede ser mayor al total de la venta", "Datos no válidos", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txttarjeta.Select()
            Return
        End If
        If pago + tarjeta < total Then
            MessageBox.Show("El monto recibido es insuficiente", "Datos no válidos", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        If tarjeta > 0 AndAlso Trim(txtautori.Text) = "" Then
            MessageBox.Show("Ingrese el número de autorización del pago con tarjeta", "Datos no válidos", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtautori.Select()
            Return
        End If

        Dim res As Boolean
        If MessageBox.Show("¿Desea guardar esta venta?", "Guardando", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            If saveClient = True Then
                saveinfoclient()
                saveClient = False
            End If
            res = GrabaVenta(table)
            If res AndAlso Me.CheckBox1.Checked = True Then
                guardarFactura()
            End If

            If pv = 1 And res = True Then
                frmPuntoDeVentaMejorado.cleanAll()
                Me.Close()
            End If
        End If
    End Sub

    Private Sub txtpago_TextChanged(sender As Object, e As EventArgs) Handles txtpago.TextChanged
        RecalcularCambio()
    End Sub

    Private Sub frmCobrar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtpago.Text = "0.00"
        txttarjeta.Text = "0.00"

        txtfecha.Text = DateTime.Now.ToShortDateString
        txtusuario.Text = nameUsuarioActual
        txtsucursal.Text = sucActual

        txtnit.Text = "C/F"
        txtnombrecliente.Text = "CONSUMIDOR FINAL"
        txtdircliente.Text = "CIUDAD"
    End Sub

    Private Sub txttotal_TextChanged(sender As Object, e As EventArgs) Handles txttotal.TextChanged
        RecalcularCambio()
    End Sub

    Private Sub txttarjeta_TextChanged(sender As Object, e As EventArgs) Handles txttarjeta.TextChanged
        RecalcularCambio()
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
                    Try
                        openConnection()
                        Dim query As String = "SELECT rzCliente, direccionCliente FROM CLIENTE WHERE nitCliente = @nit;"
                        Dim cmd As New SqlCommand(query, conn)
                        cmd.Parameters.AddWithValue("nit", Trim(txtnit.Text))

                        Using reader As SqlDataReader = cmd.ExecuteReader
                            If reader.Read() Then
                                txtnombrecliente.Text = If(reader.IsDBNull(0), "", reader.GetString(0))
                                txtdircliente.Text = If(reader.IsDBNull(1), "", reader.GetString(1))
                            Else
                                saveClient = True
                                txtnombrecliente.Select()
                            End If
                        End Using


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