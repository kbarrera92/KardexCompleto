Imports System.Data.SqlClient

Public Class frmCatalogoProducto
    Dim RegOAct As Integer = 0
    Dim ds As New DataSet
    Dim correlativo As String = "SELECT IDENT_CURRENT ('PRODUCTO') AS Current_Identity"
    Dim sqlCat As String = "SELECT idCategoria, categoria FROM CATEGORIA"
    Dim sqlProv As String = "SELECT idProveedor, rzProveedor FROM PROVEEDOR"
    Dim fila As Integer
    Dim criterio As String

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

    Sub getDatos()

        Dim ind1 As Integer
        Dim ind2 As Integer

        Try

            txtcod.Text = DataGridView1.Rows(fila).Cells(0).Value
            txtdesc.Text = DataGridView1.Rows(fila).Cells(1).Value

            txtpres.Text = DataGridView1.Rows(fila).Cells(3).Value
            txtat.Text = DataGridView1.Rows(fila).Cells(4).Value

            txtindi.Text = DataGridView1.Rows(fila).Cells(5).Value
            txtcontra.Text = DataGridView1.Rows(fila).Cells(6).Value

            ind1 = cmbpro.FindStringExact(DataGridView1.Rows(fila).Cells(8).Value)
            cmbpro.SelectedIndex = ind1

            txtobs.Text = DataGridView1.Rows(fila).Cells(7).Value
            txtmed.Text = DataGridView1.Rows(fila).Cells(9).Value

            ind2 = cmbcat.FindStringExact(DataGridView1.Rows(fila).Cells(10).Value)
            cmbcat.SelectedIndex = ind2
            txtlab.Text = DataGridView1.Rows(fila).Cells(11).Value
            txtprecio.Text = DataGridView1.Rows(fila).Cells(12).Value
            txtcosto.Text = DataGridView1.Rows(fila).Cells(13).Value
            txtutilidad.Text = FormatNumber(Val(txtprecio.Text) - Val(txtcosto.Text), 2)
            txtEstanteria.Text = DataGridView1.Rows(fila).Cells(15).Value
            txtbarcode.Clear()
            txtbarcode.Text = DataGridView1.Rows(fila).Cells(16).Value
            txtstockmin.Clear()
            txtstockmin.Text = DataGridView1.Rows(fila).Cells(17).Value

            ComboBoxEstado.SelectedIndex = If(Convert.ToBoolean(DataGridView1.Rows(fila).Cells(18).Value), 0, 1)
        Catch ex As Exception

        End Try
    End Sub

    Sub cargarDGVProd()
        Dim sqlParameters As New List(Of SqlParameter) From {
            New SqlParameter("@Operacion", "LISTAR")
        }

        Try
            openConnection()
            Dim spResult As SpResult = SqlHelper.ExecuteStoredProcedure("sp_mantProducto", sqlParameters)
            Dim dt As DataTable = spResult.Data.Tables(0)

            For i = 0 To dt.Columns.Count - 1
                DataGridView1.Columns(i).DataPropertyName = dt.Columns(i).ToString
            Next

            dv = dt.DefaultView
            DataGridView1.DataSource = dv
        Catch ex As Exception
            MsgBox("Error al cargar los datos.")
            'Guardar en log
        Finally
            closeConnection()
        End Try
    End Sub

    Private Sub frmCatalogoProducto_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cmbcat.DataSource = updateList(sqlCat)
        cmbcat.ValueMember = updateList(sqlCat).Columns(0).ToString
        cmbcat.DisplayMember = updateList(sqlCat).Columns(1).ToString

        cmbpro.DataSource = updateList(sqlProv)
        cmbpro.ValueMember = updateList(sqlProv).Columns(0).ToString
        cmbpro.DisplayMember = updateList(sqlProv).Columns(1).ToString

        cargarDGVProd()
        cmbcat.SelectedIndex = -1
        cmbpro.SelectedIndex = -1
        ComboBox1.SelectedIndex = 0
        getDatos()
        'Timer1.Start()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        txtcod.Clear()
        txtdesc.Clear()
        txtcomposicion.Clear()
        txtpres.Clear()
        txtat.Clear()

        txtindi.Clear()
        txtcontra.Clear()
        cmbpro.SelectedIndex = -1
        txtobs.Clear()
        txtmed.Clear()
        txtlab.Clear()
        cmbcat.SelectedIndex = -1
        txtdesc.Select()
        txtprecio.Clear()
        txtcosto.Clear()
        txtEstanteria.Text = "0"
        txtbarcode.Clear()
        txtstockmin.Clear()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim result As Integer
        Dim resultDecimal As Decimal
        Dim producto As New Producto With {
            .IdProducto = If(Not String.IsNullOrEmpty(txtcod.Text) AndAlso Integer.TryParse(txtcod.Text.Trim(), result), result, 0),
            .DProducto = txtdesc.Text.Trim(),
            .Composicion = txtcomposicion.Text.Trim(),
            .Presentacion = txtpres.Text.Trim(),
            .Aterapeutica = txtat.Text.Trim(),
            .Indicaciones = txtindi.Text.Trim(),
            .Contraindicaciones = txtcontra.Text.Trim(),
            .Observaciones = txtobs.Text.Trim(),
            .Proveedor = Convert.ToInt32(cmbpro.SelectedValue),
            .Medida = txtmed.Text.Trim(),
            .Categoria = Convert.ToInt32(cmbcat.SelectedValue),
            .Laboratorio = txtlab.Text.Trim(),
            .Precio = If(Not String.IsNullOrEmpty(txtprecio.Text) AndAlso Decimal.TryParse(txtprecio.Text.Trim(), resultDecimal), resultDecimal, 0),
            .Costo = If(Not String.IsNullOrEmpty(txtcosto.Text) AndAlso Decimal.TryParse(txtcosto.Text.Trim(), resultDecimal), resultDecimal, 0),
            .FechaRegistro = Date.Now,
            .Estanteria = If(Not String.IsNullOrEmpty(txtEstanteria.Text) AndAlso Integer.TryParse(txtEstanteria.Text.Trim(), result), result, 0),
            .Barcode = txtbarcode.Text.Trim(),
            .Stockmin = If(Not String.IsNullOrEmpty(txtstockmin.Text) AndAlso Integer.TryParse(txtstockmin.Text.Trim(), result), result, 0)
        }

        Dim idProducto = If(Not String.IsNullOrEmpty(txtcod.Text) AndAlso Integer.TryParse(txtcod.Text.Trim(), result), result, 0)
        Dim errores As String = ValidarProducto(producto, idProducto)

        If errores <> "" Then
            MessageBox.Show("Errores encontrados:" & Environment.NewLine & errores)
        Else
            If idProducto = 0 Then
                If MessageBox.Show("¿Desea guardar este registro?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    Dim sqlParameters As New List(Of SqlParameter) From {
                        New SqlParameter("@Operacion", "INSERTAR"),
                        New SqlParameter("@idProducto", If(String.IsNullOrWhiteSpace(txtcod.Text), DBNull.Value, CInt(txtcod.Text))),
                        New SqlParameter("@dProducto", If(String.IsNullOrWhiteSpace(txtdesc.Text), DBNull.Value, txtdesc.Text.Trim())),
                        New SqlParameter("@composicion", If(String.IsNullOrWhiteSpace(txtcomposicion.Text), DBNull.Value, txtcomposicion.Text.Trim())),
                        New SqlParameter("@presentacion", If(String.IsNullOrWhiteSpace(txtpres.Text), DBNull.Value, txtpres.Text.Trim())),
                        New SqlParameter("@aterapeutica", If(String.IsNullOrWhiteSpace(txtat.Text), DBNull.Value, txtat.Text.Trim())),
                        New SqlParameter("@indicaciones", If(String.IsNullOrWhiteSpace(txtindi.Text), DBNull.Value, txtindi.Text.Trim())),
                        New SqlParameter("@contraindicaciones", If(String.IsNullOrWhiteSpace(txtcontra.Text), DBNull.Value, txtcontra.Text.Trim())),
                        New SqlParameter("@observaciones", If(String.IsNullOrWhiteSpace(txtobs.Text), DBNull.Value, txtobs.Text.Trim())),
                        New SqlParameter("@proveedor", If(cmbpro.SelectedValue Is Nothing, DBNull.Value, cmbpro.SelectedValue)),
                        New SqlParameter("@medida", If(String.IsNullOrWhiteSpace(txtmed.Text), DBNull.Value, txtmed.Text.Trim())),
                        New SqlParameter("@categoria", If(cmbcat.SelectedValue Is Nothing, DBNull.Value, cmbcat.SelectedValue)),
                        New SqlParameter("@laboratorio", If(String.IsNullOrWhiteSpace(txtlab.Text), DBNull.Value, txtlab.Text.Trim())),
                        New SqlParameter("@precio", If(String.IsNullOrWhiteSpace(txtprecio.Text), DBNull.Value, CDbl(txtprecio.Text))),
                        New SqlParameter("@costo", If(String.IsNullOrWhiteSpace(txtcosto.Text), DBNull.Value, CDbl(txtcosto.Text))),
                        New SqlParameter("@fechaRegistro", Date.Now),
                        New SqlParameter("@estanteria", If(String.IsNullOrWhiteSpace(txtEstanteria.Text), DBNull.Value, CInt(txtEstanteria.Text))),
                        New SqlParameter("@barcode", If(String.IsNullOrWhiteSpace(txtbarcode.Text), DBNull.Value, txtbarcode.Text.Trim())),
                        New SqlParameter("@stockmin", If(String.IsNullOrWhiteSpace(txtstockmin.Text), DBNull.Value, CInt(txtstockmin.Text))),
                        New SqlParameter("@MSG", SqlDbType.VarChar, 200) With {.Direction = ParameterDirection.Output},
                        New SqlParameter("@nuevoId", SqlDbType.Int) With {.Direction = ParameterDirection.Output}
                    }

                    Try
                        openConnection()
                        Dim spResult As SpResult = SqlHelper.ExecuteStoredProcedure("sp_mantProducto", sqlParameters)
                        If spResult.OutputParams("@nuevoId") > 0 Then
                            MessageBox.Show(spResult.OutputParams("@MSG"), "Registro correcto", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            MessageBox.Show(spResult.OutputParams("@MSG"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Return
                        End If

                        TextBox1.Clear()
                        txtdesc.Clear()
                        txtcomposicion.Clear()

                        txtpres.Clear()
                        txtat.Clear()
                        txtindi.Clear()
                        txtcontra.Clear()
                        cmbpro.SelectedIndex = -1
                        txtobs.Clear()
                        txtmed.Clear()
                        txtlab.Clear()
                        txtprecio.Clear()
                        txtcosto.Clear()
                        txtEstanteria.Clear()
                        cmbcat.SelectedIndex = -1
                        txtbarcode.Clear()
                        txtstockmin.Clear()
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "Algo salió mal", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Finally
                        closeConnection()
                        cargarDGVProd()
                    End Try
                End If
            Else
                If MessageBox.Show("¿Desea guardar los cambios de este registro?", "Guardar cambios", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                    Dim sqlParameters As New List(Of SqlParameter) From {
                        New SqlParameter("@Operacion", "ACTUALIZAR"),
                        New SqlParameter("@idProducto", If(String.IsNullOrWhiteSpace(txtcod.Text), DBNull.Value, CInt(txtcod.Text))),
                        New SqlParameter("@dProducto", If(String.IsNullOrWhiteSpace(txtdesc.Text), DBNull.Value, txtdesc.Text.Trim())),
                        New SqlParameter("@composicion", If(String.IsNullOrWhiteSpace(txtcomposicion.Text), DBNull.Value, txtcomposicion.Text.Trim())),
                        New SqlParameter("@presentacion", If(String.IsNullOrWhiteSpace(txtpres.Text), DBNull.Value, txtpres.Text.Trim())),
                        New SqlParameter("@aterapeutica", If(String.IsNullOrWhiteSpace(txtat.Text), DBNull.Value, txtat.Text.Trim())),
                        New SqlParameter("@indicaciones", If(String.IsNullOrWhiteSpace(txtindi.Text), DBNull.Value, txtindi.Text.Trim())),
                        New SqlParameter("@contraindicaciones", If(String.IsNullOrWhiteSpace(txtcontra.Text), DBNull.Value, txtcontra.Text.Trim())),
                        New SqlParameter("@observaciones", If(String.IsNullOrWhiteSpace(txtobs.Text), DBNull.Value, txtobs.Text.Trim())),
                        New SqlParameter("@proveedor", If(cmbpro.SelectedValue Is Nothing, DBNull.Value, cmbpro.SelectedValue)),
                        New SqlParameter("@medida", If(String.IsNullOrWhiteSpace(txtmed.Text), DBNull.Value, txtmed.Text.Trim())),
                        New SqlParameter("@categoria", If(cmbcat.SelectedValue Is Nothing, DBNull.Value, cmbcat.SelectedValue)),
                        New SqlParameter("@laboratorio", If(String.IsNullOrWhiteSpace(txtlab.Text), DBNull.Value, txtlab.Text.Trim())),
                        New SqlParameter("@precio", If(String.IsNullOrWhiteSpace(txtprecio.Text), DBNull.Value, CDbl(txtprecio.Text))),
                        New SqlParameter("@costo", If(String.IsNullOrWhiteSpace(txtcosto.Text), DBNull.Value, CDbl(txtcosto.Text))),
                        New SqlParameter("@estanteria", If(String.IsNullOrWhiteSpace(txtEstanteria.Text), DBNull.Value, CInt(txtEstanteria.Text))),
                        New SqlParameter("@barcode", If(String.IsNullOrWhiteSpace(txtbarcode.Text), DBNull.Value, txtbarcode.Text.Trim())),
                        New SqlParameter("@stockmin", If(String.IsNullOrWhiteSpace(txtstockmin.Text), DBNull.Value, CInt(txtstockmin.Text))),
                        New SqlParameter("@MSG", SqlDbType.VarChar, 200) With {.Direction = ParameterDirection.Output},
                        New SqlParameter("@nuevoId", SqlDbType.Int) With {.Direction = ParameterDirection.Output}
                    }

                    Try
                        openConnection()
                        Dim spResult As SpResult = SqlHelper.ExecuteStoredProcedure("sp_mantProducto", sqlParameters)
                        If spResult.OutputParams("@nuevoId") > 0 Then
                            MessageBox.Show(spResult.OutputParams("@MSG"), "Actualización correcta", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            MessageBox.Show(spResult.OutputParams("@MSG"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Return
                        End If

                        TextBox1.Clear()
                    Catch ex As Exception
                        MessageBox.Show("Algo salió mal. Comuniquese con el Administrador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        'Dejar Log
                    Finally
                        closeConnection()
                        cargarDGVProd()
                    End Try
                End If
            End If
        End If
    End Sub



    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If Trim(txtcod.Text) = "" Or Trim(txtdesc.Text) = "" Then
            MessageBox.Show("No se ha elegido ningún registro para eliminar", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            Dim sqlParameters As New List(Of SqlParameter) From {
                New SqlParameter("@Operacion", "DESACTIVAR"),
                New SqlParameter("@idProducto", If(String.IsNullOrWhiteSpace(txtcod.Text), DBNull.Value, CInt(txtcod.Text))),
                New SqlParameter("@MSG", SqlDbType.VarChar, 200) With {.Direction = ParameterDirection.Output},
                New SqlParameter("@nuevoId", SqlDbType.Int) With {.Direction = ParameterDirection.Output}
            }

            If MessageBox.Show("¿Desea desactivar este producto?", "Desactivar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then


                Try
                    openConnection()
                    Dim spResult As SpResult = SqlHelper.ExecuteStoredProcedure("sp_mantProducto", sqlParameters)
                    If spResult.OutputParams("@nuevoId") > 0 Then
                        MessageBox.Show(spResult.OutputParams("@MSG"), "Desactivación correcta", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        MessageBox.Show(spResult.OutputParams("@MSG"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return
                    End If

                    TextBox1.Clear()
                    txtcod.Clear()
                    txtdesc.Clear()
                    txtcomposicion.Clear()

                    txtpres.Clear()
                    txtat.Clear()

                    txtindi.Clear()
                    txtcontra.Clear()
                    cmbpro.SelectedIndex = -1
                    txtobs.Clear()
                    txtmed.Clear()
                    txtlab.Clear()
                    txtprecio.Clear()
                    txtcosto.Clear()
                    txtbarcode.Clear()
                    txtstockmin.Clear()
                    txtEstanteria.Clear()
                    cmbcat.SelectedIndex = -1
                    DataGridView1.Select()
                Catch ex As Exception
                    MessageBox.Show("Algo salió mal" & vbCrLf & "Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Finally
                    closeConnection()
                    cargarDGVProd()
                End Try
            End If
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If MessageBox.Show("¿Desea salir de esta ventana?", "Saliendo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub txtcosto_TextChanged(sender As Object, e As EventArgs) Handles txtcosto.TextChanged
        txtutilidad.Text = Val(txtprecio.Text) - Val(txtcosto.Text)
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If ComboBox1.SelectedIndex = 0 Then
            criterio = "idProducto"
        Else
            If ComboBox1.SelectedIndex = 1 Then
                criterio = "dProducto"
            Else
                If ComboBox1.SelectedIndex = 2 Then
                    criterio = "composicion"
                Else
                    If ComboBox1.SelectedIndex = 3 Then
                        criterio = "marca"
                    Else
                        If ComboBox1.SelectedIndex = 4 Then
                            criterio = "rzProveedor"
                        Else

                            If ComboBox1.SelectedIndex = 5 Then
                                criterio = "laboratorio"
                            Else
                                If ComboBox1.SelectedIndex = 6 Then
                                    criterio = "presentacion"
                                Else
                                    If ComboBox1.SelectedIndex = 7 Then
                                        criterio = "categoria"
                                    Else
                                        If ComboBox1.SelectedIndex = 8 Then
                                            criterio = "barcode"
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        End If

        dv.RowFilter = String.Format("Convert(" & criterio & ", 'System.String') LIKE '%{0}%'", Trim(TextBox1.Text))
    End Sub


    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        Try
            fila = DataGridView1.CurrentRow.Index
            getDatos()
        Catch ex As Exception

        End Try
    End Sub


    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        frmEstanterias.Show()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        txtbarcode.Text = "A" & txtcod.Text & "A"
    End Sub


End Class