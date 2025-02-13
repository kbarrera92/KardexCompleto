Imports System.Data.SqlClient
Public Class frmPuntoDeVenta

    Dim correlativo As String = "SELECT IDENT_CURRENT ('VENTA') AS Current_Identity"
    Dim sql As String = "SELECT idSerie, letra FROM SERIEFACTURA WHERE sucursal = " & sucActual
    Dim totalVenta As Double = 0

    

    Sub cleanAll()
        txtfecha.Text = DateTime.Now.ToShortDateString
        
        DataGridView1.Rows.Clear()
        txtNVenta.Clear()

        txtFactura.Clear()
        txtcodigo.Clear()
        txtdescripcion.Clear()
        txtpresentacion.Clear()
        txtlaboratorio.Clear()
        txtmedida.Clear()
        txtcategoria.Clear()
        txtprecio.Text = "0.00"
        txtcantidad.Text = "0"
        txtexistencia.Text = "0"
        txtstanteria.Text = "0"
        'txtnit.Clear()
        'txtnombrecliente.Clear()
        'txtdircliente.Clear()
        txtTotal.Text = "0.00"
        btnnuevaventa.Select()
    End Sub

    Sub cleaninfopro()
        txtcodigo.Clear()
        txtdescripcion.Clear()
        txtpresentacion.Clear()
        txtlaboratorio.Clear()
        txtmedida.Clear()
        txtcategoria.Clear()
        txtprecio.Text = "0.00"
        txtcantidad.Text = "0"
        txtstanteria.Text = "0"
        txtexistencia.Text = "0"
        TextBox1.Clear()
        txtcodigo.Select()
    End Sub

    Private Sub btnnuevaventa_Click(sender As Object, e As EventArgs) Handles btnnuevaventa.Click
        totalVenta = 0.0
        txtNVenta.Text = getCorrelativoTrasiego(correlativo) + 1

        txtcodigo.Clear()
        txtdescripcion.Clear()
        txtpresentacion.Clear()
        txtlaboratorio.Clear()
        txtmedida.Clear()
        txtcategoria.Clear()
        txtprecio.Text = "0.00"
        txtcantidad.Text = "0"
        txtexistencia.Text = "0"
        txtstanteria.Text = "0"
        
        txtTotal.Text = "0.00"


        guardarVenta()
        txtcodigo.Select()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        datosreq = 1
        frmProductos.Show()
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

   

    Private Sub btnadddetalle_Click(sender As Object, e As EventArgs) Handles btnadddetalle.Click
        'If Val(txtcantidad.Text) > Val(txtexistencia.Text) Then
        '    MsgBox("No hay existencia suficiente", MsgBoxStyle.Exclamation, "Sin existencia")
        '    txtcantidad.Select()
        'Else
        If Trim(txtcantidad.Text) = "" Or Val(txtcantidad.Text) <= 0 Then
            MsgBox("La cantidad ingresada no es válida", MsgBoxStyle.Exclamation, "Faltan datos")
            txtcantidad.Select()
        Else
            DataGridView1.Rows.Add(txtcodigo.Text, txtdescripcion.Text, txtcantidad.Text, FormatNumber(txtprecio.Text, 2), FormatNumber(Val(txtcantidad.Text) * Val(txtprecio.Text), 2))
            totalVenta = totalVenta + (Val(txtcantidad.Text) * Val(txtprecio.Text))
            txtTotal.Text = FormatNumber(totalVenta, 2)
            cleaninfopro()
        End If

        'End If

    End Sub

    Private Sub btncobrar_Click(sender As Object, e As EventArgs) Handles btncobrar.Click
        If DataGridView1.Rows.Count = 0 Then
            MsgBox("No se ha agregado ningún producto", MsgBoxStyle.Exclamation, "Faltan datos")
        Else
            If Trim(txtnit.Text) = "" Or Trim(txtnombrecliente.Text) = "" Or Trim(txtdircliente.Text) = "" Then
                MsgBox("Faltan datos obligatorios", MsgBoxStyle.Exclamation, "Faltan datos")
            Else
                frmCobrar.Show()
                frmCobrar.txttotal.Text = Replace(txtTotal.Text, ",", "")
                frmCobrar.txtpago.Select()
            End If
        End If
    End Sub

    Private Sub txtcantidad_KeyDown(sender As Object, e As KeyEventArgs) Handles txtcantidad.KeyDown
        If e.KeyCode = Keys.Enter Then
            btnadddetalle.PerformClick()
        End If

    End Sub

    Private Sub btneliminardetalle_Click(sender As Object, e As EventArgs) Handles btneliminardetalle.Click
        If DataGridView1.Rows.Count = 0 Or DataGridView1.SelectedRows.Count = 0 Then
            MsgBox("Agregue o seleccione un registro para eliminar", MsgBoxStyle.Exclamation, "¡No hay nada seleccionado!")
        Else
            Dim fila As Integer = DataGridView1.CurrentRow.Index
            totalVenta = totalVenta - DataGridView1.Rows(fila).Cells(4).Value
            DataGridView1.Rows.RemoveAt(fila)
            txtTotal.Text = FormatNumber(totalVenta, 2)
        End If
    End Sub

    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.Delete Then
            btneliminardetalle.PerformClick()
        End If

    End Sub

    Private Sub txtcodigo_KeyDown(sender As Object, e As KeyEventArgs) Handles txtcodigo.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim reader As SqlDataReader

            Try
                openConnection()
                Dim query As String = "SELECT P.dProducto, ISNULL(P.presentacion, '') AS presentacion, ISNULL(P.laboratorio, '') AS laboratorio, ISNULL(P.medida, '') AS medida, C.categoria, P.precio, P.estanteria, ISNULL(P.barcode, '') AS barcode FROM PRODUCTO P " _
                                      & "INNER JOIN CATEGORIA C " _
                                      & "ON P.categoria = C.idCategoria " _
                                      & "WHERE P.idProducto = @pro"

                Dim cmd As New SqlCommand(query, conn)
                cmd.Parameters.AddWithValue("pro", CInt(txtcodigo.Text))

                reader = cmd.ExecuteReader
                reader.Read()

                If reader.HasRows Then
                    txtdescripcion.Text = reader(0)
                    txtpresentacion.Text = reader(1)
                    txtlaboratorio.Text = reader(2)
                    txtmedida.Text = reader(3)
                    txtcategoria.Text = reader(4)
                    txtprecio.Text = reader(5)
                    txtstanteria.Text = reader(6)
                    TextBox1.Text = reader(7)
                    reader.Close()
                    txtexistencia.Text = getStock(sucActual, CInt(txtcodigo.Text), "sp_getStoc")
                Else
                    MsgBox("No se encontraron coincidencias", MsgBoxStyle.Critical, "Error en los datos")
                    reader.Close()
                End If


            Catch ex As Exception
                MsgBox("No se encontraron coincidencias" & vbCrLf & ex.Message, MsgBoxStyle.Critical, "Error en los datos")
                'reader.Close()
            Finally
                closeConnection()
                txtcantidad.Select()
            End Try
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        cleaninfopro()
    End Sub

    Private Sub frmPuntoDeVenta_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        datosreq = 0
        ToolTip1.SetToolTip(btnnuevaventa, "Realizar venta nueva")
        ToolTip1.SetToolTip(btnadddetalle, "Agregue el detalle de la venta")
        ToolTip1.SetToolTip(btneliminardetalle, "Eliminar detalle de la venta")
        ToolTip1.SetToolTip(btncobrar, "Recibir efectivo y cobrar")
        ToolTip1.SetToolTip(btnsalir, "Salir de esta ventana")
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

    Private Sub btnsalir_Click(sender As Object, e As EventArgs) Handles btnsalir.Click
        Me.Close()
    End Sub

    Private Sub txtnombrecliente_KeyDown(sender As Object, e As KeyEventArgs) Handles txtnombrecliente.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtdircliente.Select()
        End If

    End Sub

   
    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim reader As SqlDataReader

            Try
                openConnection()
                Dim query As String = "SELECT P.dProducto, ISNULL(P.presentacion, '') as presentacion, ISNULL(P.laboratorio, '') as laboratorio, ISNULL(P.medida, '') as medida, C.categoria, P.precio, P.estanteria, P.idProducto FROM PRODUCTO P " _
                                      & "INNER JOIN CATEGORIA C " _
                                      & "ON P.categoria = C.idCategoria " _
                                      & "WHERE P.barcode = @pro"

                Dim cmd As New SqlCommand(query, conn)
                cmd.Parameters.AddWithValue("pro", TextBox1.Text)
                reader = cmd.ExecuteReader
                reader.Read()

                If reader.HasRows Then
                    txtdescripcion.Text = reader(0)
                    txtpresentacion.Text = reader(1)
                    txtlaboratorio.Text = reader(2)
                    txtmedida.Text = reader(3)
                    txtcategoria.Text = reader(4)
                    txtprecio.Text = reader(5)
                    txtstanteria.Text = reader(6)
                    txtcodigo.Text = reader(7)
                    reader.Close()
                    txtexistencia.Text = getStock(sucActual, CInt(txtcodigo.Text), "sp_getStoc")
                Else
                    MsgBox("No se encontraron coincidencias", MsgBoxStyle.Critical, "Error en los datos")
                    reader.Close()
                End If


            Catch ex As Exception
                MsgBox("No se encontraron coincidencias", MsgBoxStyle.Critical, "Error en los datos")
                'reader.Close()
            Finally
                closeConnection()
                txtcantidad.Select()
            End Try
        End If
    End Sub

    Private Sub txtFactura_KeyDown(sender As Object, e As KeyEventArgs) Handles txtFactura.KeyDown
        If Trim(txtFactura.Text) = "" Then
            txtFactura.Text = txtNVenta.Text
        Else
            If Not IsNumeric(txtFactura.Text) Then
                txtFactura.Clear()
                txtFactura.Select()
            End If
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If MessageBox.Show("¿Desea descartar esta venta?", "Descartar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim sqldes As String = "DELETE FROM VENTA WHERE nVenta = @nv"
            Dim cmd As SqlCommand
            Try
                cmd = New SqlCommand(sqldes, conn)
                cmd.Parameters.AddWithValue("nv", CInt(txtNVenta.Text))

                openConnection()
                cmd.ExecuteNonQuery()
                closeConnection()
                MsgBox("Venta descartada", MsgBoxStyle.Information, "Éxito")
                txtNVenta.Clear()
            Catch ex As Exception
                MsgBox("Hubo un error", MsgBoxStyle.Critical, "Error")
            End Try
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        frmVentasDiarias.Show()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        frmVerVentas.Show()
    End Sub
End Class