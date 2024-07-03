Imports System.Data.SqlClient
Public Class frmAjuste
    Dim correlativo As String = "SELECT MAX(nAjuste) FROM AJUSTE"
    Dim ind1 As Integer
    Dim total As Double = 0

    Sub limpiar()
        lblNoCompra.Text = "Ajuste No."
        txtbuscapro.Clear()

        mskfecha.Text = Format(DateTime.Now, "dd/MM/yyyy")
        DataGridView2.Rows.Clear()
        txttotal.Clear()
        txtcodpro.Clear()
        txtdescpro.Clear()
        
        cmbProveedor.SelectedIndex = -1
        txtprecio.Clear()
        txtcantidad.Clear()
        txtexistencia.Clear()
        txtbuscapro.Select()
    End Sub

    Private Sub frmAjuste_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: esta línea de código carga datos en la tabla 'IS_PRO2DataSet.SUCURSAL' Puede moverla o quitarla según sea necesario.
        Me.SUCURSALTableAdapter.Fill(Me.IS_PRO2DataSet.SUCURSAL)
        'TODO: esta línea de código carga datos en la tabla 'IS_PRO2DataSet.PROVEEDOR' Puede moverla o quitarla según sea necesario.
        Me.PROVEEDORTableAdapter.Fill(Me.IS_PRO2DataSet.PROVEEDOR)
        'TODO: esta línea de código carga datos en la tabla 'IS_PRO2DataSet.TIPOAJUSTE' Puede moverla o quitarla según sea necesario.
        Me.TIPOAJUSTETableAdapter.Fill(Me.IS_PRO2DataSet.TIPOAJUSTE)
        mskfecha.Text = Format(DateTime.Now, "dd/MM/yyyy")
        ComboBox1.SelectedIndex = -1
        ComboBox2.SelectedIndex = -1
        cmbProveedor.SelectedIndex = -1
    End Sub

    Private Sub txtbuscapro_TextChanged(sender As Object, e As EventArgs) Handles txtbuscapro.TextChanged
        Dim filt As String

        filt = String.Format("dProducto like '%{0}%' Or presentacion like '%{0}%' Or Convert(idProducto,'System.String') like '{0}%'", txtbuscapro.Text)
        dv.RowFilter = filt
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        Try
            fillDGVSP("sp_infoProdCompras", DataGridView1, Me, CInt(ComboBox2.SelectedValue.ToString))
        Catch ex As Exception

        End Try

    End Sub

    Private Sub btnnuevaventa_Click(sender As Object, e As EventArgs) Handles btnnuevaventa.Click
        lblNoCompra.Text = "Ajuste No. " & getCorrelativoTrasiego(correlativo) + 1
        txtbuscapro.Clear()
        txtcodpro.Clear()
        txtdescpro.Clear()
        cmbProveedor.SelectedIndex = -1
        txtcantidad.Text = "0"
        txtprecio.Text = "0.00"
        txttotal.Text = "0.00"
        DataGridView2.Rows.Clear()
        txtbuscapro.Select()
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
            cmbProveedor.SelectedIndex = -1
            txtprecio.Clear()
            txtcantidad.Clear()
            txtexistencia.Clear()
            txtbuscapro.Select()
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

    Private Sub txtcantidad_KeyDown(sender As Object, e As KeyEventArgs) Handles txtcantidad.KeyDown
        If e.KeyCode = Keys.Enter Then
            btnagregard.PerformClick()
        End If
    End Sub

    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyData = Keys.Enter Then
            txtcodpro.Text = DataGridView1.CurrentRow.Cells(0).Value
            txtdescpro.Text = DataGridView1.CurrentRow.Cells(1).Value & " " & DataGridView1.CurrentRow.Cells(4).Value & " " & DataGridView1.CurrentRow.Cells(4).Value
            txtexistencia.Text = DataGridView1.CurrentRow.Cells(2).Value
            ind1 = cmbProveedor.FindStringExact(DataGridView1.CurrentRow.Cells(6).Value)
            cmbProveedor.SelectedIndex = ind1
            txtcantidad.Select()
            txtprecio.Text = DataGridView1.CurrentRow.Cells(5).Value
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub txtbuscapro_KeyDown(sender As Object, e As KeyEventArgs) Handles txtbuscapro.KeyDown
        If e.KeyData = Keys.Enter Then
            DataGridView1.Select()
        End If
    End Sub

    Private Sub txtcodpro_KeyDown(sender As Object, e As KeyEventArgs) Handles txtcodpro.KeyDown
        If e.KeyCode = Keys.Enter Then
            If Trim(txtcodpro.Text) = "" Then
                MessageBox.Show("Debe ingresar un código de producto", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                Dim reader As SqlDataReader
                Try
                    openConnection()
                    Dim query As String = "SELECT CONCAT(dProducto, ' ', presentacion, ' ', medida), PRO.rzProveedor, costo " _
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
                        reader.Close()
                        txtexistencia.Text = getStock(CInt(ComboBox2.SelectedValue.ToString), CInt(txtcodpro.Text), "sp_getStoc")
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

    Private Sub btnregistrarc_Click(sender As Object, e As EventArgs) Handles btnregistrarc.Click
        If DataGridView2.Rows.Count = 0 Then
            MsgBox("No se ha agregado nada a la compra", MsgBoxStyle.Critical, "Faltan datos")
        Else
            guardarAjuste()
            limpiar()
        End If
    End Sub
End Class