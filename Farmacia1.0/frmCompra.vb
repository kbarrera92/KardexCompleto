Imports System.Data.SqlClient
Imports System.Globalization

Public Class frmCompra

    Dim correlativo As String = "SELECT IDENT_CURRENT ('COMPRA') AS Current_Identity"
    Dim ind1 As Integer
    Dim total As Double = 0
    Dim sqlProveedor As String = "SELECT idProveedor, rzProveedor FROM PROVEEDOR"
    Dim sqlFormaPago As String = "SELECT idFormaPago, foraPago FROM FORMAPAGO"

    Sub guardarCXP()
        Dim sql As String = "INSERT INTO CUENTAXPAGAR VALUES(@concep, @fi, @fl, @tot, @saldo, @estado, @prov, @compra)"
        Dim cmd As SqlCommand
        'Try
        cmd = New SqlCommand(sql, conn)
        With cmd.Parameters
            .AddWithValue("concep", "Por concepto de compra No. " & getCorrelativoTrasiego(correlativo) & " al cliente: " & cmbProveedor.Text)
            .AddWithValue("fi", DateTime.ParseExact(mskfecha.Text, "dd/MM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None))
            .AddWithValue("fl", DateTime.ParseExact(txtFechaPago.Text, "dd/MM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None))
            .AddWithValue("tot", 0.0)
            .AddWithValue("saldo", 0.0)
            .AddWithValue("estado", 200)
            .AddWithValue("prov", CInt(cmbProveedor.SelectedValue.ToString))
            .AddWithValue("compra", getCorrelativoTrasiego(correlativo))
        End With
        openConnection()
        cmd.ExecuteNonQuery()
        MessageBox.Show("Se guardo correctamente la cuenta por pagar", "Cuenta creada", MessageBoxButtons.OK, MessageBoxIcon.Information)
        'Catch ex As Exception
        'MsgBox("Hubo un error al guardar la cuenta" & vbCrLf & "Error: " & ex.Message, MsgBoxStyle.Critical, "Error al guardar CXP")
        'Finally
        closeConnection()
        'End Try
    End Sub

    Sub actualizarCompra()
        Dim sqlupdate As String = "UPDATE COMPRA SET total = @tot WHERE nCompra = @nc"
        Dim cmd As SqlCommand
        Try
            cmd = New SqlCommand(sqlupdate, conn)

            cmd.Parameters.AddWithValue("tot", total)
            cmd.Parameters.AddWithValue("nc", getCorrelativoTrasiego(correlativo))

            openConnection()
            cmd.ExecuteNonQuery()
            closeConnection()
            'MsgBox("Compra registrada correctamente", MsgBoxStyle.Information, "Guardado")
            actualizarCXP()
        Catch ex As Exception
            MsgBox("Error al guardar los datos", MsgBoxStyle.Critical, "Error")

        End Try



    End Sub
    Sub actualizarCXP()
        Dim sqlupdate As String = "UPDATE CUENTAXPAGAR SET totalCuenta = @tot, saldoCuenta = @tot WHERE idCompra = @nc"
        Dim cmd As SqlCommand
        Try
            cmd = New SqlCommand(sqlupdate, conn)

            cmd.Parameters.AddWithValue("tot", total)
            cmd.Parameters.AddWithValue("nc", getCorrelativoTrasiego(correlativo))

            openConnection()
            cmd.ExecuteNonQuery()
            closeConnection()
            MessageBox.Show("Se guardo correctamente la compra y la cuenta por pagar", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MsgBox("Error al guardar los datos", MsgBoxStyle.Critical, "Error")

        End Try
    End Sub

    Private Sub TextBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles txtcodpro.KeyDown
        If e.KeyCode = Keys.Enter Then
            If Trim(txtcodpro.Text) = "" Then
                MessageBox.Show("Debe ingresar un código de producto", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                Dim reader As SqlDataReader
                Try
                    openConnection()
                    Dim query As String = "SELECT CONCAT(dProducto, ' ', presentacion, ' ', medida), PRO.rzProveedor, costo, presentacion, medida " _
                                          & "FROM PRODUCTO " _
                                          & "INNER JOIN PROVEEDOR PRO " _
                                          & "ON proveedor = PRO.idProveedor " _
                                          & "WHERE idProducto = @id"

                    Dim cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("id", CInt(txtcodpro.Text))

                    reader = cmd.ExecuteReader
                    reader.Read()

                    If reader.HasRows Then
                        txtdescpro.Text = reader(0)
                        ind1 = cmbProveedor.FindStringExact(reader(1))
                        cmbProveedor.SelectedIndex = ind1
                        txtprecio.Text = reader(2)
                        txtpres.Text = reader(3)
                        txtmedida.Text = reader(4)
                        reader.Close()
                        txtexistencia.Text = getStock(sucActual, CInt(txtcodpro.Text), "sp_getStoc")
                    Else
                        MsgBox("No se encontró el producto", MsgBoxStyle.Critical, "FarmaciAhorro")
                        txtcodpro.Select()
                        reader.Close()
                    End If


                Catch ex As Exception
                    MsgBox("Error en la conexión a la Base de datos" & vbCrLf & ex.ToString)
                Finally
                    closeConnection()
                End Try
            End If
        End If
    End Sub

    Private Sub btnnuevaventa_Click(sender As Object, e As EventArgs) Handles btnnuevaventa.Click
        If cmbProveedor.SelectedIndex = -1 Then
            MessageBox.Show("Faltan datos requeridos", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        ElseIf txtFactura.Text.Trim = "" Then
            MessageBox.Show("Faltan datos requeridos", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        ElseIf cmbFP.SelectedIndex = -1 Then
            MessageBox.Show("Faltan datos requeridos", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        ElseIf mskfecha.Text.Trim = "" Then
            MessageBox.Show("Faltan datos requeridos", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If


        lblNoCompra.Text = "Compra No. " & getCorrelativoTrasiego(correlativo) + 1
        txtbuscapro.Clear()
        txtcodpro.Clear()
        txtdescpro.Clear()
        txtpres.Clear()
        txtmedida.Clear()

        txtcantidad.Text = "0"
        txtprecio.Text = "0.00"
        txttotal.Text = "0.00"
        total = 0
        DataGridView2.DataSource = Nothing
        DataGridView2.Rows.Clear()


        'Try
        If CInt(cmbFP.SelectedValue.ToString) = 1000 Then
            guardarCompra()
            txtbuscapro.Select()
            fillDGVSP("sp_infoProdCompras", DataGridView1, Me, sucActual)
        ElseIf CInt(cmbFP.SelectedValue.ToString) = 2000 Then
            guardarCompra()
            guardarCXP()
            txtbuscapro.Select()
            fillDGVSP("sp_infoProdCompras", DataGridView1, Me, sucActual)

        End If
        'Catch ex As Exception
        '    MsgBox("No se pudo registrar la compra" & vbCrLf & "Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
        'End Try

    End Sub

    Private Sub btnagregard_Click(sender As Object, e As EventArgs) Handles btnagregard.Click
        If Val(txtcantidad.Text) <= 0 Then
            MsgBox("Cantidad no válida", MsgBoxStyle.Critical, "Error")
        Else
            DataGridView2.Rows.Add(txtcodpro.Text, txtdescpro.Text, txtcantidad.Text, txtprecio.Text, Val(txtcantidad.Text) * Val(txtprecio.Text))
            total = total + (Val(txtcantidad.Text) * Val(txtprecio.Text))
            txttotal.Text = FormatNumber(total, 2)
            txtbuscapro.Clear()
            txtcodpro.Clear()
            txtdescpro.Clear()
            txtpres.Clear()
            txtmedida.Clear()
            txtprecio.Text = "0.00"
            txtcantidad.Text = "0"
            txtexistencia.Text = "0"
            txtbuscapro.Select()
        End If

    End Sub

    Private Sub frmCompra_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DataGridView1.AutoGenerateColumns = False
        DataGridView2.AutoGenerateColumns = False

        cmbFP.DataSource = updateCm(sqlFormaPago)
        cmbFP.DisplayMember = updateCm(sqlFormaPago).Columns(1).ToString
        cmbFP.ValueMember = updateCm(sqlFormaPago).Columns(0).ToString

        cmbProveedor.DataSource = updateCm(sqlProveedor)
        cmbProveedor.DisplayMember = updateCm(sqlProveedor).Columns(1).ToString
        cmbProveedor.ValueMember = updateCm(sqlProveedor).Columns(0).ToString

        cmbProveedor.SelectedIndex = -1
        cmbFP.SelectedIndex = 0
        mskfecha.Text = Format(DateTime.Now, "dd/MM/yyyy")
        fillDGVSP("sp_infoProdCompras", DataGridView1, Me, sucActual)
    End Sub



    Private Sub btnregistrarc_Click(sender As Object, e As EventArgs) Handles btnregistrarc.Click
        If DataGridView2.Rows.Count = 0 Then
            If MsgBox("No se ha agregado nada a la compra. ¿Desea descartarla?", MsgBoxStyle.YesNo, "Faltan datos") = MsgBoxResult.Yes Then
                btnDescartar.PerformClick()
            End If
        Else
            guardarDetalleCompra()
            lblNoCompra.Text = "Compra No."
            cmbFP.SelectedIndex = 0
            cmbProveedor.SelectedIndex = -1
            txtbuscapro.Clear()
            txtcodpro.Clear()
            txtdescpro.Clear()
            txtpres.Clear()
            txtmedida.Clear()
            txtprecio.Text = "0.00"
            txtcantidad.Text = "0"
            txtexistencia.Text = "0"
            mskfecha.Text = Format(DateTime.Now, "dd/MM/yyyy")
            txtFactura.Clear()
            txttotal.Text = "0.00"
            txtbuscapro.Select()
            DataGridView2.Rows.Clear()
            actualizarCompra()

        End If
    End Sub

    Private Sub txtbuscapro_TextChanged(sender As Object, e As EventArgs) Handles txtbuscapro.TextChanged
        Dim filt As String

        filt = String.Format("dProducto like '%{0}%' Or presentacion like '%{0}%' Or Convert(idProducto,'System.String') like '{0}%'", txtbuscapro.Text)
        dv.RowFilter = filt
    End Sub

    Private Sub txtbuscapro_KeyDown(sender As Object, e As KeyEventArgs) Handles txtbuscapro.KeyDown
        If e.KeyData = Keys.Enter Then
            DataGridView1.Select()
        End If
    End Sub

    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyData = Keys.Enter Then
            txtcodpro.Text = DataGridView1.CurrentRow.Cells(0).Value
            txtdescpro.Text = DataGridView1.CurrentRow.Cells(1).Value & " " & DataGridView1.CurrentRow.Cells(4).Value & " " & DataGridView1.CurrentRow.Cells(4).Value
            txtexistencia.Text = DataGridView1.CurrentRow.Cells(2).Value
            'ind1 = cmbProveedor.FindStringExact(DataGridView1.CurrentRow.Cells(6).Value)
            'cmbProveedor.SelectedIndex = ind1
            txtcantidad.Select()
            txtprecio.Text = DataGridView1.CurrentRow.Cells(5).Value
            txtpres.Text = DataGridView1.CurrentRow.Cells(4).Value
            txtmedida.Text = DataGridView1.CurrentRow.Cells(7).Value
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub txtcantidad_KeyDown(sender As Object, e As KeyEventArgs) Handles txtcantidad.KeyDown
        If e.KeyCode = Keys.Enter Then
            btnagregard.PerformClick()
        End If
    End Sub

    Private Sub btneliminard_Click(sender As Object, e As EventArgs) Handles btneliminard.Click
        If DataGridView2.Rows.Count = 0 Or DataGridView2.SelectedRows.Count = 0 Then
            MsgBox("Debe seleccionar un registro para eliminarlo", MsgBoxStyle.Critical, "¡No hay nada para eliminar!")
        Else
            total = total - DataGridView2.CurrentRow.Cells(4).Value
            txttotal.Text = FormatNumber(total, 2)
            DataGridView2.Rows.RemoveAt(DataGridView2.CurrentRow.Index)
        End If
    End Sub

    Private Sub btnsalir_Click(sender As Object, e As EventArgs) Handles btnsalir.Click
        Me.Close()
    End Sub


    Private Sub btnDescartar_Click(sender As Object, e As EventArgs) Handles btnDescartar.Click
        Dim sqlDescartar As String = "DELETE FROM COMPRA WHERE inCompra = @nc"
        Dim cmd As SqlCommand

        Try
            cmd = New SqlCommand(sqlDescartar, conn)

            cmd.Parameters.AddWithValue("nc", getCorrelativoTrasiego(correlativo))

            openConnection()
            cmd.ExecuteNonQuery()
            closeConnection()
            MsgBox("Compra descartada", MsgBoxStyle.Exclamation, "Descartada")
        Catch ex As Exception
            MsgBox("Ha ocurrido un error", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub



    Private Sub cmbFP_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbFP.SelectedIndexChanged
        If cmbFP.Text = "CREDITO" Then
            formaPago = CInt(cmbFP.SelectedValue.ToString)
            Dim hoy As DateTime = Now.AddDays(30)
            txtFechaPago.Text = Format(hoy, "dd/MM/yyyy")
        Else
            Try
                formaPago = CInt(cmbFP.SelectedValue.ToString)
                txtFechaPago.Clear()
            Catch ex As Exception

            End Try

        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        frmVerCompras.Show()
    End Sub

    Private Sub txtbuscapro_Click(sender As Object, e As EventArgs) Handles txtbuscapro.Click
        fillDGVSP("sp_infoProdCompras", DataGridView1, Me, sucActual)
    End Sub
End Class