Imports System.Data.SqlClient

Public Class frmCobrar

    'El punto de venta está cobrando un vale: se registra el pago del vale (que genera la venta) en vez de una venta normal
    Private Function CobrandoVale() As Boolean
        Return pv = 1 AndAlso frmPuntoDeVentaMejorado.idValeEnCobro > 0
    End Function

    'sp_pagaVale registra en una sola transacción el pago del vale y la venta, sin descontar existencias
    '(ya se descontaron al grabar el vale) y sin bonificación.
    Private Function GrabaPagoVale() As Boolean
        Dim idVale As Integer = frmPuntoDeVentaMejorado.idValeEnCobro
        Dim rc As Integer = -3
        Dim nventa As Integer

        Try
            Dim dtLineas As New DataTable()
            dtLineas.Columns.Add("nDetalle", GetType(Short))
            For Each n As Short In frmPuntoDeVentaMejorado.lineasVale
                dtLineas.Rows.Add(n)
            Next

            Dim pMensaje As New SqlParameter("@message", SqlDbType.VarChar, 200) With {.Direction = ParameterDirection.Output}
            Dim pVenta As New SqlParameter("@nVenta", SqlDbType.Int) With {.Direction = ParameterDirection.Output}
            Dim pRetorno As New SqlParameter("@returnValue", SqlDbType.Int) With {.Direction = ParameterDirection.ReturnValue}

            Dim parametros As New List(Of SqlParameter) From {
                New SqlParameter("@idVale", SqlDbType.Int) With {.Value = idVale},
                New SqlParameter("@usuario", SqlDbType.Int) With {.Value = usuarioActual},
                New SqlParameter("@lineas", SqlDbType.Structured) With {.TypeName = "dbo.LINEASVALE", .Value = dtLineas},
                New SqlParameter("@horasDiferencia", SqlDbType.Int) With {.Value = CInt(ConsultaParametro("horasDiferencia"))},
                New SqlParameter("@cliente", SqlDbType.VarChar, 10) With {.Value = Trim(txtnit.Text)},
                New SqlParameter("@documento", SqlDbType.VarChar, 20) With {.Value = If(Trim(txtFactura.Text) = "", CObj(DBNull.Value), CObj(Trim(txtFactura.Text)))},
                New SqlParameter("@tarjeta", SqlDbType.Decimal) With {.Precision = 10, .Scale = 2, .Value = LeerMonto(txttarjeta)},
                New SqlParameter("@autoriza", SqlDbType.VarChar, 16) With {.Value = Trim(txtautori.Text)},
                New SqlParameter("@codempleado", SqlDbType.Int) With {.Value = CInt(TextBoxCodigoEmpleado.Text)},
                pMensaje,
                pVenta,
                pRetorno
            }

            Serilog.Log.Information($"Cobrando vale {idVale} desde el punto de venta. Usuario: {usuarioActual}, líneas: {frmPuntoDeVentaMejorado.lineasVale.Count}, total: {LeerMonto(txttotal)}")

            Dim resultado = EjecutarStoredProcedureMultiple("sp_pagaVale", parametros)

            If resultado.Item2.StartsWith("Error") Then
                Serilog.Log.Error($"Error al ejecutar sp_pagaVale. {resultado.Item2}")
                MessageBox.Show("Hubo un error en el procesamiento", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If

            rc = If(IsDBNull(pRetorno.Value) OrElse pRetorno.Value Is Nothing, -1, Convert.ToInt32(pRetorno.Value))
            Dim mensaje As String = Convert.ToString(pMensaje.Value)
            MessageBox.Show(mensaje, If(rc = 0, "Éxito", "Error"), MessageBoxButtons.OK, If(rc = 0, MessageBoxIcon.Information, MessageBoxIcon.Error))
            If rc <> 0 Then
                Serilog.Log.Warning($"El pago del vale {idVale} no se registró. {mensaje}")
                Return False
            End If

            Serilog.Log.Information($"Pago del vale {idVale} registrado. {mensaje}")
            nventa = If(IsDBNull(pVenta.Value), 0, Convert.ToInt32(pVenta.Value))
            ImprimeTicket(nventa)
            If nombreRol = "ADMINISTRADOR" Then
                DibujaTarjetasResumen()
            End If

            'Si la consulta de vales está abierta se refresca con el nuevo saldo
            For Each f As Form In Application.OpenForms
                If TypeOf f Is FormVerVales Then
                    DirectCast(f, FormVerVales).RecargarVales(idVale)
                    Exit For
                End If
            Next
        Catch ex As Exception
            Serilog.Log.Error($"Error al cobrar el vale {idVale}. Error: {ex.Message}, Trace: {ex.StackTrace}")
            MessageBox.Show($"Hubo un error al registrar el pago del vale. {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            rc = -3
        End Try

        Return rc = 0
    End Function

    Function GrabaVenta(ByVal table As DataTable) As Boolean
        If CobrandoVale() Then Return GrabaPagoVale()

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

        'Pago de vale: el empleado de la venta es el vendedor del vale
        If CobrandoVale() Then
            TextBoxCodigoEmpleado.Text = frmPuntoDeVentaMejorado.idVendedorVale.ToString()
            TextBoxCodigoEmpleado.ReadOnly = True
        End If
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