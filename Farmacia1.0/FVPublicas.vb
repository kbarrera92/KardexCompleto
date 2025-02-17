Imports System.Data.SqlClient
Imports System.Configuration
Imports System.ComponentModel
Imports System.IO
Imports System.Globalization

Module FVPublicas

    'Variables correspondientes al trasiego
    Public enQueDGV As Integer
    Public codDTS As Integer
    Public cantDTS As Integer
    Public descDTS As String
    Public presDTS As String
    Public medDTS As String
    Public exisDTS As Integer

    Public pv As Integer

    Public saveClient As Boolean = False

    Public table As DataTable

    'Variable que definirá que información se necesita como respuesta a la búsqueda del producto
    '(1 --> Venta, 2 --> Compra, 3 --> Traslado)
    Public datosreq As Integer = 0

    'Esta variable guardará la sucursal actual o a la que se quiere acceder
    Public sucActual As Integer
    Public nameSucActual As String

    Public conn As New SqlConnection
    Public dv As DataView
    Public rolUsuarioActual As Integer
    Public nameUsuarioActual As String
    Public usuarioActual As Integer
    Public nombreRol As String

    Public formaPago As Integer

    Public nSalidaXTraslado As Integer



    Sub saveinfoclient()
        Dim query As String = "INSERT INTO CLIENTE VALUES(@nit, @rz, @dir, null, null)"
        Dim cmd As SqlCommand
        Try
            cmd = New SqlCommand(query, conn)

            cmd.Parameters.AddWithValue("nit", Trim(frmCobrar.txtnit.Text))
            cmd.Parameters.AddWithValue("rz", Trim(frmCobrar.txtnombrecliente.Text))
            cmd.Parameters.AddWithValue("dir", Trim(frmCobrar.txtdircliente.Text))

            openConnection()
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show("No se guardo el cliente" & vbCrLf & "Error: " & ex.Message, "Algo salio mal", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            closeConnection()
        End Try

    End Sub

    Sub fillDGV(ByVal str As String, ByVal dgv As DataGridView, ByVal frm As Form)
        Dim cmd As SqlCommand
        Dim da As SqlDataAdapter
        Dim dt As New DataTable

        Try
            openConnection()
            cmd = New SqlCommand()

            With cmd
                cmd.CommandText = str
                cmd.CommandType = CommandType.Text
                cmd.Connection = conn
            End With

            da = New SqlDataAdapter(cmd)
            da.Fill(dt)

            For i = 0 To dt.Columns.Count - 1
                dgv.Columns(i).DataPropertyName = dt.Columns(i).ToString
            Next

            dv = dt.DefaultView
            dgv.DataSource = dv

        Catch ex As Exception
            MsgBox("Error al cargar los datos" & vbCrLf & "Error: " & ex.ToString)
        Finally
            closeConnection()
        End Try

    End Sub

    Function getCorrelativoTrasiego(ByVal sql As String) As Integer
        Dim correlativo As Integer

        Dim cmd As SqlCommand

        cmd = New SqlCommand(sql, conn)

        Try
            openConnection()
            correlativo = CInt(cmd.ExecuteScalar)
            Return correlativo
        Catch ex As Exception
            MsgBox(ex.Message)
            Return 0
        Finally
            closeConnection()
        End Try
    End Function

    Sub openConnection()
        If conn.State = ConnectionState.Closed Then

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("IS_PRO2CS").ToString
            conn.Open()
        End If
    End Sub

    Sub closeConnection()
        If conn.State = ConnectionState.Open Then
            conn.Close()

        End If
    End Sub

    Sub guardarAjuste()
        Dim query1 As String = "INSERT INTO AJUSTE VALUES(@fech, @suc, @usuario, @concep, @total, @tipo)"

        Dim cmd As SqlCommand


        cmd = New SqlCommand(query1, conn)

        cmd.Parameters.AddWithValue("fech", Convert.ToDateTime(frmAjustes2.mskfecha.Text))
        cmd.Parameters.AddWithValue("usuario", usuarioActual)
        'cmd.Parameters.AddWithValue("total", CDbl(frmAjuste.txttotal.Text))
        cmd.Parameters.AddWithValue("total", 0.0)
        cmd.Parameters.AddWithValue("concep", Trim(frmAjustes2.txtconcep.Text))
        cmd.Parameters.AddWithValue("suc", CInt(frmAjustes2.ComboBox2.SelectedValue.ToString))

        cmd.Parameters.AddWithValue("tipo", CInt(frmAjustes2.ComboBox1.SelectedValue.ToString))


        Try
            openConnection()
            cmd.ExecuteNonQuery()

        Catch ex As Exception
            MessageBox.Show("Algo salio mal" & vbCrLf & "Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            closeConnection()
            'guardarDetalleAjuste()
            frmAjustes2.txtbuscapro.Select()
        End Try
    End Sub

    Sub guardarDetalleAjuste()
        Dim queryID As String = "INSERT INTO DETAJUSTE VALUES(@no, @trans, @prod, @cant, @precio, @subt);"
        Dim comand As SqlCommand

        comand = New SqlCommand(queryID, conn)

        Try
            For i = 0 To frmAjuste.DataGridView2.Rows.Count - 1
                comand.Parameters.Clear()

                comand.Parameters.AddWithValue("no", i + 1)
                'comand.Parameters.AddWithValue("trans", najuste)
                comand.Parameters.AddWithValue("prod", frmAjuste.DataGridView2.Rows(i).Cells(0).Value)
                comand.Parameters.AddWithValue("cant", frmAjuste.DataGridView2.Rows(i).Cells(2).Value)
                comand.Parameters.AddWithValue("subt", frmAjuste.DataGridView2.Rows(i).Cells(4).Value)
                comand.Parameters.AddWithValue("precio", frmAjuste.DataGridView2.Rows(i).Cells(3).Value)


                openConnection()
                comand.ExecuteNonQuery()
                closeConnection()


            Next
            MessageBox.Show("Transacción realizada correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MsgBox("Error: " & ex.ToString)
        Finally

        End Try
    End Sub

    Sub guardarCompra()
        Dim query1 As String = "INSERT INTO COMPRA VALUES(@fech, @usuario, @total, @doc, @suc, @prov, @fp, @fechpago)"

        Dim cmd As SqlCommand


        cmd = New SqlCommand(query1, conn)

        cmd.Parameters.AddWithValue("fech", DateTime.ParseExact(frmCompra.mskfecha.Text, "dd/MM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None))
        cmd.Parameters.AddWithValue("usuario", usuarioActual)
        cmd.Parameters.AddWithValue("total", 0.0)
        cmd.Parameters.AddWithValue("doc", CInt(frmCompra.txtFactura.Text))
        cmd.Parameters.AddWithValue("suc", sucActual)
        cmd.Parameters.AddWithValue("prov", CInt(frmCompra.cmbProveedor.SelectedValue.ToString))
        cmd.Parameters.AddWithValue("fp", CInt(frmCompra.cmbFP.SelectedValue.ToString))
        If formaPago = 1000 Then
            cmd.Parameters.AddWithValue("fechpago", DBNull.Value)
        Else
            cmd.Parameters.AddWithValue("fechpago", DateTime.ParseExact(frmCompra.txtFechaPago.Text, "dd/MM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None))
        End If

        Try
            openConnection()
            cmd.ExecuteNonQuery()

        Catch ex As Exception
            MessageBox.Show("Algo salio mal" & vbCrLf & "Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            closeConnection()
            'guardarDetalleCompra()
        End Try
    End Sub

    Function updateCm(ByVal sql As String) As DataTable
        Dim da As SqlDataAdapter
        Dim dt As New DataTable

        Try
            openConnection()
            da = New SqlDataAdapter(sql, conn)
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            MsgBox(ex.Message)
            Return Nothing
        End Try
    End Function



    Sub guardarVenta2()
        Dim query As String = "INSERT INTO VENTA VALUES(@fech, @usuario, @total, @doc, @suc, @cliente, @efec, @tarj, @aut)"
        Dim cmd As SqlCommand
        Try
            cmd = New SqlCommand(query, conn)

            cmd.Parameters.AddWithValue("fech", Convert.ToDateTime(frmPuntoDeVentaMejorado.DateTimePicker1.Value))
            cmd.Parameters.AddWithValue("usuario", usuarioActual)
            cmd.Parameters.AddWithValue("total", CDbl(frmPuntoDeVentaMejorado.txttotal.Text))
            cmd.Parameters.AddWithValue("doc", "")
            cmd.Parameters.AddWithValue("suc", sucActual)
            cmd.Parameters.AddWithValue("cliente", "C/F")
            cmd.Parameters.AddWithValue("efec", 0.0)
            cmd.Parameters.AddWithValue("tarj", 0.0)
            cmd.Parameters.AddWithValue("aut", "")

            openConnection()
            cmd.ExecuteNonQuery()

        Catch ex As Exception
            MessageBox.Show("Algo salio mal" & vbCrLf & "Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            closeConnection()
            'guardarDetalleVenta()
        End Try
    End Sub

    Sub guardarDetalleVenta2()
        Dim queryID As String = "INSERT INTO DETALLEVENTA VALUES(@no, @trans, @prod, @cant, @precio, @subt);"
        Dim comand As SqlCommand

        comand = New SqlCommand(queryID, conn)

        Try
            If pv = 1 Then
                For i = 0 To frmPuntoDeVentaMejorado.DataGridView1.Rows.Count - 1
                    comand.Parameters.Clear()

                    comand.Parameters.AddWithValue("no", i + 1)
                    comand.Parameters.AddWithValue("trans", CInt(frmPuntoDeVentaMejorado.txtcorrelativo.Text))
                    comand.Parameters.AddWithValue("prod", frmPuntoDeVentaMejorado.DataGridView1.Rows(i).Cells(0).Value)
                    comand.Parameters.AddWithValue("cant", frmPuntoDeVentaMejorado.DataGridView1.Rows(i).Cells(2).Value)
                    comand.Parameters.AddWithValue("subt", frmPuntoDeVentaMejorado.DataGridView1.Rows(i).Cells(4).Value)
                    comand.Parameters.AddWithValue("precio", frmPuntoDeVentaMejorado.DataGridView1.Rows(i).Cells(3).Value)


                    openConnection()
                    comand.ExecuteNonQuery()
                    closeConnection()


                Next

            End If

        Catch ex As Exception
            MsgBox("Error: " & ex.ToString)
        Finally

        End Try

    End Sub

    Sub guardarSalidaxTraslado()
        Dim query As String = "INSERT INTO SALXTRASLADO VALUES(@fech, @suc, @usuario, @recibido)"
        Dim cmd As SqlCommand

        cmd = New SqlCommand(query, conn)

        cmd.Parameters.AddWithValue("fech", Convert.ToDateTime(frmTraslados.mskfecha.Text))
        cmd.Parameters.AddWithValue("usuario", usuarioActual)

        cmd.Parameters.AddWithValue("suc", CInt(frmTraslados.cmbSucSalida.SelectedValue.ToString))
        cmd.Parameters.AddWithValue("recibido", CChar("N"))

        Try
            openConnection()
            cmd.ExecuteNonQuery()
            guardarDetalleSalidaxTraslado()
        Catch ex As Exception
            MessageBox.Show("Algo salio mal" & vbCrLf & "Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            closeConnection()

        End Try
    End Sub

    Sub guardarEntradaxTraslado()
        Dim query As String = "INSERT INTO ENTXTRASLADO VALUES(@fech, @suc, @usuario, @nSalidaRelacionada)"
        Dim cmd As SqlCommand

        cmd = New SqlCommand(query, conn)

        cmd.Parameters.AddWithValue("fech", Convert.ToDateTime(frmTraslados.mskfecha.Text))
        cmd.Parameters.AddWithValue("usuario", usuarioActual)

        cmd.Parameters.AddWithValue("suc", CInt(frmTraslados.cmbSucEntranda.SelectedValue.ToString))
        cmd.Parameters.AddWithValue("nSalidaRelacionada", nSalidaXTraslado)

        Try
            openConnection()
            cmd.ExecuteNonQuery()
            guardarDetalleEntradaxTraslado()
        Catch ex As Exception
            MessageBox.Show("Algo salio mal" & vbCrLf & "Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            closeConnection()

        End Try
    End Sub

    Sub guardarDetalleCompra()
        Dim queryID As String = "INSERT INTO DETALLECOMPRA VALUES(@no, @trans, @prod, @cant, @precio, @subt);"
        Dim comand As SqlCommand

        comand = New SqlCommand(queryID, conn)

        Try
            For i = 0 To frmCompra.DataGridView2.Rows.Count - 1
                comand.Parameters.Clear()

                comand.Parameters.AddWithValue("no", i + 1)
                comand.Parameters.AddWithValue("trans", getCorrelativoTrasiego("SELECT IDENT_CURRENT ('COMPRA') AS Current_Identity"))
                comand.Parameters.AddWithValue("prod", frmCompra.DataGridView2.Rows(i).Cells(0).Value)
                comand.Parameters.AddWithValue("cant", frmCompra.DataGridView2.Rows(i).Cells(2).Value)
                comand.Parameters.AddWithValue("subt", frmCompra.DataGridView2.Rows(i).Cells(4).Value)
                comand.Parameters.AddWithValue("precio", frmCompra.DataGridView2.Rows(i).Cells(3).Value)


                openConnection()
                comand.ExecuteNonQuery()
                closeConnection()


            Next
            MessageBox.Show("Transacción realizada correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MsgBox("Error: " & ex.ToString)
        Finally

        End Try
    End Sub





    Sub guardarFactura()
        Dim query As String = "INSERT INTO FACTURA VALUES(@fac, @fech, @nitc, @nombrec, @dirc, @serie, @venta, @estado)"
        Dim cmd As SqlCommand
        Try
            cmd = New SqlCommand(query, conn)

            cmd.Parameters.AddWithValue("fech", Convert.ToDateTime(frmCobrar.txtfecha.Text))
            cmd.Parameters.AddWithValue("fac", CInt(frmCobrar.txtFactura.Text))
            cmd.Parameters.AddWithValue("nitc", frmCobrar.txtnit.Text)
            cmd.Parameters.AddWithValue("nombrec", frmCobrar.txtnombrecliente.Text)
            cmd.Parameters.AddWithValue("dirc", frmCobrar.txtdircliente.Text)
            cmd.Parameters.AddWithValue("serie", CInt(frmCobrar.ComboBox1.SelectedValue))
            'cmd.Parameters.AddWithValue("venta", CInt(frmCobrar.txtNVenta.Text))
            cmd.Parameters.AddWithValue("estado", 1)

            openConnection()
            cmd.ExecuteNonQuery()

        Catch ex As Exception
            MessageBox.Show("Algo salio mal" & vbCrLf & "Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            closeConnection()

        End Try
    End Sub

    Sub guardarDetalleSalidaxTraslado()
        Dim queryID As String = "INSERT INTO DETSALXTRASLADO VALUES(@no, @trans, @prod, @cant);"
        Dim comand As SqlCommand

        comand = New SqlCommand(queryID, conn)
        nSalidaXTraslado = getCorrelativoTrasiego("SELECT IDENT_CURRENT ('SALXTRASLADO') AS Current_Identity")
        Try
            For i = 0 To frmTraslados.DataGridView2.Rows.Count - 1
                comand.Parameters.Clear()

                comand.Parameters.AddWithValue("no", i + 1)
                comand.Parameters.AddWithValue("trans", nSalidaXTraslado)
                comand.Parameters.AddWithValue("prod", frmTraslados.DataGridView2.Rows(i).Cells(0).Value)
                comand.Parameters.AddWithValue("cant", frmTraslados.DataGridView2.Rows(i).Cells(2).Value)

                openConnection()
                comand.ExecuteNonQuery()
                closeConnection()
            Next
            'EN ESTE PUNTO HABRÍA QUE AGREGAR EL REGISTRO DE TRASLADO
            guardarEntradaxTraslado()
        Catch ex As Exception
            MsgBox("Error: " & ex.ToString)
        Finally

        End Try
    End Sub

    Sub guardarDetalleEntradaxTraslado()
        Dim queryID As String = "INSERT INTO DETENTRADAXTRASLADOTEMP VALUES(@no, @trans, @prod, @cant);"
        Dim comand As SqlCommand
        Dim trans As Integer = getCorrelativoTrasiego("SELECT IDENT_CURRENT ('ENTXTRASLADO') AS Current_Identity")

        comand = New SqlCommand(queryID, conn)

        Try
            For i = 0 To frmTraslados.DataGridView2.Rows.Count - 1
                comand.Parameters.Clear()

                comand.Parameters.AddWithValue("no", i + 1)
                comand.Parameters.AddWithValue("trans", trans)
                comand.Parameters.AddWithValue("prod", frmTraslados.DataGridView2.Rows(i).Cells(0).Value)
                comand.Parameters.AddWithValue("cant", frmTraslados.DataGridView2.Rows(i).Cells(2).Value)

                openConnection()
                comand.ExecuteNonQuery()
                closeConnection()

            Next
            MessageBox.Show("Transacción realizada correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Dim params(3) As String
            params(0) = nameUsuarioActual
            params(1) = Environment.MachineName & " - " & Environment.UserName
            params(2) = String.Format("{0} envió traslado No. {1}, en la sucursal: {2}", nameUsuarioActual, trans, ConsultaParametro("sucursalFisica"))

            GrabaBitacora(params, grabaBitacoraSp)
        Catch ex As Exception
            MsgBox("Error: " & ex.ToString)
        Finally

        End Try
    End Sub

    Sub fillDGVSP(ByVal str As String, ByVal dgv As DataGridView, ByVal frm As Form, ByVal suc As Integer)
        Dim cmd As SqlCommand
        Dim da As SqlDataAdapter
        Dim dt As New DataTable

        Try
            openConnection()
            cmd = New SqlCommand()

            With cmd
                cmd.CommandText = str
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Connection = conn
                cmd.Parameters.AddWithValue("suc", suc)
            End With

            da = New SqlDataAdapter(cmd)
            da.Fill(dt)

            For i = 0 To dt.Columns.Count - 1
                dgv.Columns(i).DataPropertyName = dt.Columns(i).ToString
            Next

            dv = dt.DefaultView
            dgv.DataSource = dv

        Catch ex As Exception
            MsgBox("Error al cargar los datos" & vbCrLf & "Error: " & ex.ToString)
        Finally
            closeConnection()
        End Try

    End Sub

    Sub calcularSaldo()
        Dim abonos As Double = 0

        For i = 0 To frmCuentasxP.DataGridView1.Rows.Count - 1
            abonos = abonos + frmCuentasxP.DataGridView1.Rows(i).Cells(3).Value
        Next

        frmCuentasxP.txtSaldo.Text = Val(frmCuentasxP.txtTotal.Text) - abonos
        If frmCuentasxP.txtSaldo.Text <= 0 Then
            frmCuentasxP.txtEstado.Text = "SOLVENTE"

        End If
        actualizarCXP()
    End Sub

    Sub actualizarCXP()
        Dim sqlUpdateCXP As String = "UPDATE CUENTAXPAGAR SET saldoCuenta = @saldo, estado = @estado WHERE idCuenta = @idC"
        Dim cmd As SqlCommand

        cmd = New SqlCommand(sqlUpdateCXP, conn)

        cmd.Parameters.AddWithValue("saldo", Val(frmCuentasxP.txtSaldo.Text))
        If frmCuentasxP.txtEstado.Text = "INSOLVENTE" Then
            cmd.Parameters.AddWithValue("estado", 200)
        Else
            cmd.Parameters.AddWithValue("estado", 100)
        End If
        cmd.Parameters.AddWithValue("idC", Val(frmAbono.txtNoCuenta.Text))

        Try
            openConnection()
            cmd.ExecuteNonQuery()
            MessageBox.Show("Los datos de la cuenta se han actualizado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            closeConnection()
        End Try
    End Sub

    Sub fillDGVSPDetCxP(ByVal str As String, ByVal dgv As DataGridView, ByVal frm As Form, ByVal suc As Integer)
        Dim cmd As SqlCommand
        Dim da As SqlDataAdapter
        Dim dt As New DataTable

        Try
            openConnection()
            cmd = New SqlCommand()

            With cmd
                cmd.CommandText = str
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Connection = conn
                cmd.Parameters.AddWithValue("cuenta", suc)
            End With

            da = New SqlDataAdapter(cmd)
            da.Fill(dt)

            For i = 0 To dt.Columns.Count - 1
                dgv.Columns(i).DataPropertyName = dt.Columns(i).ToString
            Next

            dv = dt.DefaultView
            dgv.DataSource = dv

        Catch ex As Exception
            MsgBox("Error al cargar los datos" & vbCrLf & "Error: " & ex.ToString)
        Finally
            closeConnection()
        End Try

    End Sub

    Sub filldgvestandar(ByVal str As String, ByVal dgv As DataGridView, ByVal frm As Form)
        Dim cmd As SqlCommand
        Dim da As SqlDataAdapter
        Dim dt As New DataTable

        Try
            openConnection()
            cmd = New SqlCommand()

            With cmd
                cmd.CommandText = str
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Connection = conn
            End With

            da = New SqlDataAdapter(cmd)
            da.Fill(dt)

            For i = 0 To dt.Columns.Count - 1
                dgv.Columns(i).DataPropertyName = dt.Columns(i).ToString
            Next

            dv = dt.DefaultView
            dgv.DataSource = dv

        Catch ex As Exception
            MsgBox("Error al cargar los datos" & vbCrLf & "Error: " & ex.ToString)
        Finally
            closeConnection()
        End Try

    End Sub


    Function getStock(ByVal suc As Integer, ByVal pro As Integer, namesp As String) As Integer
        Dim stock As Integer
        Dim cmd As New SqlCommand
        With cmd
            'cmd.CommandText = "sp_getStoc"
            cmd.CommandText = namesp
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Connection = conn
        End With

        cmd.Parameters.AddWithValue("suc", suc)
        cmd.Parameters.AddWithValue("pro", pro)

        Try
            openConnection()
            stock = cmd.ExecuteScalar
            Return stock
        Catch ex As Exception
            Return 0
        Finally
            closeConnection()
        End Try

    End Function

End Module
