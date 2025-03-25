Imports System.Data.SqlClient
Imports Serilog

Public Class frmProductos
    Dim criterio As String
    Dim sqlSucursal As String = "SELECT idSucursal, nombreSuc FROM SUCURSAL"

    Function updateList(ByVal sql As String) As DataTable
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

    Private Sub frmProductos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ComboBox2.DataSource = updateList(sqlSucursal)
            ComboBox2.ValueMember = updateList(sqlSucursal).Columns(0).ToString
            ComboBox2.DisplayMember = updateList(sqlSucursal).Columns(1).ToString

            If datosreq = 1 Or datosreq = 3 Or datosreq = 4 Then
                fillDGVSP("sp_infoProductos", DataGridView1, Me, sucActual)
            Else
                If datosreq = 2 Then
                    fillDGVSP("sp_infoProductos", DataGridView1, Me, CInt(frmTraslados.cmbSucSalida.SelectedValue.ToString))
                End If
            End If
            ComboBox1.SelectedIndex = 3
            ComboBox2.SelectedIndex = -1
        Catch ex As Exception

        End Try


    End Sub

    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtprecio.Focus()
            e.SuppressKeyPress = True

        End If
    End Sub



    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        Try
            getinfo()
        Catch ex As Exception

        End Try
    End Sub

    Sub getinfo()

        txtcodigo.Text = DataGridView1.CurrentRow.Cells(0).Value
        txtdesc.Text = DataGridView1.CurrentRow.Cells(1).Value & " " & DataGridView1.CurrentRow.Cells(4).Value & " " & DataGridView1.CurrentRow.Cells(7).Value
        txtlab.Text = DataGridView1.CurrentRow.Cells(3).Value
        txtpres.Text = DataGridView1.CurrentRow.Cells(4).Value
        txtcat.Text = DataGridView1.CurrentRow.Cells(6).Value
        txtproveedor.Text = DataGridView1.CurrentRow.Cells(8).Value
        txtindicaciones.Text = DataGridView1.CurrentRow.Cells(9).Value
        txtestanteria.Text = CInt(DataGridView1.CurrentRow.Cells(10).Value)
        txtprecio.Text = FormatNumber(DataGridView1.CurrentRow.Cells(5).Value, 2)
        txtmed.Text = DataGridView1.CurrentRow.Cells(7).Value
        txtexistencia.Text = CInt(DataGridView1.CurrentRow.Cells(2).Value)
    End Sub

    Private Sub txtbuscar_TextChanged(sender As Object, e As EventArgs) Handles txtbuscar.TextChanged
        If ComboBox1.SelectedIndex = 0 Then
            criterio = "categoria"
        Else
            If ComboBox1.SelectedIndex = 2 Then
                criterio = "idProducto"
            Else
                If ComboBox1.SelectedIndex = 3 Then
                    criterio = "dProducto"
                Else
                    If ComboBox1.SelectedIndex = 4 Then
                        criterio = "laboratorio"
                    Else
                        If ComboBox1.SelectedIndex = 5 Then
                            criterio = "presentacion"
                        Else
                            If ComboBox1.SelectedIndex = 1 Then
                                criterio = "barcode"
                            End If
                        End If
                    End If
                End If
            End If
        End If
        Try
            dv.RowFilter = String.Format("Convert(" & criterio & ", 'System.String') LIKE '%{0}%'", Trim(txtbuscar.Text))
        Catch ex As Exception
            Log.Error($"Ocurrió un error al buscar. Error {ex.Message}")
        End Try

    End Sub

    Private Sub txtbuscar_KeyDown(sender As Object, e As KeyEventArgs) Handles txtbuscar.KeyDown
        If e.KeyCode = Keys.Enter Then
            DataGridView1.Select()
        End If
    End Sub

    Private Sub ComboBox2_Click(sender As Object, e As EventArgs) Handles ComboBox2.Click
        ComboBox2.DataSource = updateList(sqlSucursal)
        ComboBox2.ValueMember = updateList(sqlSucursal).Columns(0).ToString
        ComboBox2.DisplayMember = updateList(sqlSucursal).Columns(1).ToString
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ComboBox2.SelectedIndex = -1 Then
            MsgBox("No se ha seleccionado una sucursal", MsgBoxStyle.Exclamation, "Faltan datos")
        Else
            fillDGVSP("sp_infoProductos", DataGridView1, Me, CInt(ComboBox2.SelectedValue.ToString))
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        fillDGVSP("sp_infoProductos", DataGridView1, Me, sucActual)
        ComboBox2.SelectedIndex = -1
    End Sub

    Private Sub frmProductos_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F7 Then
            ComboBox1.SelectedIndex = 1
            txtbuscar.Clear()
            txtbuscar.Select()
        End If
    End Sub

    Private Sub txtprecio_KeyDown(sender As Object, e As KeyEventArgs) Handles txtprecio.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim intVal As Integer
                Dim decValue As Decimal

                If Decimal.TryParse(txtprecio.Text, decValue) = False Then
                    MessageBox.Show("Precio inválido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If

                If datosreq = 1 Then
                    If MessageBox.Show("¿Guardar los datos de este producto?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                        Dim myValue As String = InputBox("Ingresa la cantidad", "Datos", "0")

                        If Integer.TryParse(myValue, intVal) = False Then
                            Return
                        ElseIf myValue <= 0 Then
                            'MessageBox.Show("Se cancelo el Inputbox")
                            Return
                        End If

                        frmPuntoDeVentaMejorado.DataGridView1.Rows.Add(DataGridView1.CurrentRow.Cells(0).Value, DataGridView1.CurrentRow.Cells(1).Value, myValue, Decimal.Parse(txtprecio.Text), String.Format("{0:N2}", Decimal.Parse(txtprecio.Text) * myValue))
                        frmPuntoDeVentaMejorado.txttotal.Text = String.Format("{0:N2}", frmPuntoDeVentaMejorado.calculartotal())
                        frmPuntoDeVentaMejorado.txttotalarti.Text = String.Format("{0:N2}", frmPuntoDeVentaMejorado.calculartotalarti())

                        If Not Decimal.Parse(txtprecio.Text).Equals(Decimal.Parse(DataGridView1.CurrentRow.Cells(5).Value)) Then
                            Dim params(3) As String
                            params(0) = Trim(nameUsuarioActual)
                            params(1) = Environment.MachineName & " - " & Environment.UserName
                            params(2) = String.Format("{0} cambió el precio del producto: {1}", nameUsuarioActual, DataGridView1.CurrentRow.Cells(0).Value)
                            GrabaBitacora(params, grabaBitacoraSp)
                            Log.Warning(String.Format("{0} cambió el precio del producto: {1}. Fecha: {2}", nameUsuarioActual, DataGridView1.CurrentRow.Cells(0).Value, Date.Now))
                        End If

                        Me.Close()
                    End If
                Else
                    If datosreq = 2 Then
                        If MessageBox.Show("¿Guardar los datos de este producto?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                            frmTraslados.txtcodpro.Text = DataGridView1.CurrentRow.Cells(0).Value
                            frmTraslados.txtdescpro.Text = DataGridView1.CurrentRow.Cells(1).Value & " " & DataGridView1.CurrentRow.Cells(4).Value & " " & DataGridView1.CurrentRow.Cells(7).Value

                            frmTraslados.txtlabpro.Text = DataGridView1.CurrentRow.Cells(3).Value

                            frmTraslados.txtexistencia.Text = DataGridView1.CurrentRow.Cells(2).Value
                            frmTraslados.txtcantidad.Select()
                            frmTraslados.Select()
                            Me.Close()

                        End If
                    Else
                        If datosreq = 3 Then
                            If enQueDGV = 1 Then
                                If MessageBox.Show("¿Guardar los datos de este producto?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                                    codDTS = DataGridView1.CurrentRow.Cells(0).Value
                                    descDTS = DataGridView1.CurrentRow.Cells(1).Value
                                    presDTS = DataGridView1.CurrentRow.Cells(4).Value
                                    medDTS = DataGridView1.CurrentRow.Cells(7).Value
                                    exisDTS = DataGridView1.CurrentRow.Cells(2).Value
                                    frmTrasiegos2.Show()
                                    frmTrasiegos2.txtCantProST.Select()
                                    Me.Close()
                                    'frmTrasiegos2.DataGridView1.Rows.Add(codDTS, cantDTS, descDTS, presDTS, medDTS)
                                End If
                            Else
                                If MessageBox.Show("¿Guardar los datos de este producto?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                                    codDTS = DataGridView1.CurrentRow.Cells(0).Value
                                    descDTS = DataGridView1.CurrentRow.Cells(1).Value
                                    presDTS = DataGridView1.CurrentRow.Cells(4).Value
                                    medDTS = DataGridView1.CurrentRow.Cells(7).Value
                                    frmTrasiegos2.Show()
                                    frmTrasiegos2.txtCantProdET.Select()
                                    Me.Close()

                                End If
                            End If
                        Else
                            If datosreq = 4 Then
                                frmKardexMov.TextBox1.Text = DataGridView1.CurrentRow.Cells(0).Value
                                frmKardexMov.TextBox2.Text = DataGridView1.CurrentRow.Cells(1).Value
                                'frmKardexMov.TextBox3.Text = getStock(CInt(frmKardexMov.ComboBox1.SelectedValue), CInt(frmKardexMov.TextBox1.Text), "sp_getStoc")
                                Me.Close()
                            End If
                        End If

                    End If
                End If
                e.SuppressKeyPress = True
            End If
        Catch ex As Exception
            Log.Error($"Ocurrió un error. Error. {ex.Message}")
            MessageBox.Show("Ocurrió un error. Revise el log del programa.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
End Class