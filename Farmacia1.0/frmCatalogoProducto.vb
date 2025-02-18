Imports System.Data.SqlClient
Imports Serilog

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
            MessageBox.Show("Ocurrio un error, revise el log.")
            Log.Error($"Ocurrió un error. Error: {ex.Message}")
            Return Nothing
        End Try
    End Function

    Sub getDatos()

        Dim ind1 As Integer
        Dim ind2 As Integer

        Try

            txtcod.Text = DataGridView1.Rows(fila).Cells(0).Value
            txtdesc.Text = DataGridView1.Rows(fila).Cells(1).Value _
                & " " & DataGridView1.Rows(fila).Cells(3).Value & " " & DataGridView1.Rows(fila).Cells(9).Value
            txtcomp.Text = DataGridView1.Rows(fila).Cells(2).Value

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
            DateTimePicker1.Value = Convert.ToDateTime(DataGridView1.Rows(fila).Cells(14).Value)
            txtutilidad.Text = FormatNumber(Val(txtprecio.Text) - Val(txtcosto.Text), 2)
            txtEstanteria.Text = DataGridView1.Rows(fila).Cells(15).Value
            txtbarcode.Clear()
            txtbarcode.Text = DataGridView1.Rows(fila).Cells(16).Value
            txtstockmin.Clear()
            txtstockmin.Text = DataGridView1.Rows(fila).Cells(17).Value
        Catch ex As Exception
            Log.Error($"Ocurrió un error. Error: {ex.Message}")
        End Try
    End Sub

    Sub cargarDGVProd()
        Dim sql As String
        Dim cmd As SqlCommand
        Dim da As SqlDataAdapter

        Dim dt As New DataTable

        sql = "SELECT PRODUCTO.idProducto, PRODUCTO.dProducto, ISNULL(PRODUCTO.composicion, ''), PRODUCTO.presentacion, ISNULL(PRODUCTO.aterapeutica, ''), ISNULL(PRODUCTO.indicaciones, ''), " _
            & "ISNULL(PRODUCTO.contraindicaciones, ''), ISNULL(PRODUCTO.observaciones, ''), PROVEEDOR.rzProveedor, ISNULL(PRODUCTO.medida, ''), CATEGORIA.categoria, ISNULL(PRODUCTO.laboratorio, ''), PRODUCTO.precio, PRODUCTO.costo, PRODUCTO.fechaRegistro, ISNULL(PRODUCTO.estanteria, ''), ISNULL(PRODUCTO.barcode, ''), ISNULL(PRODUCTO.stockmin, '') " _
            & "FROM CATEGORIA INNER JOIN " _
            & "PRODUCTO ON CATEGORIA.idCategoria = PRODUCTO.categoria INNER JOIN " _
            & "PROVEEDOR ON PRODUCTO.proveedor = dbo.PROVEEDOR.idProveedor"

        Try
            openConnection()
            cmd = New SqlCommand()

            With cmd
                cmd.CommandText = sql
                cmd.CommandType = CommandType.Text
                cmd.Connection = conn
            End With

            da = New SqlDataAdapter(cmd)
            da.Fill(dt)

            For i = 0 To dt.Columns.Count - 1
                DataGridView1.Columns(i).DataPropertyName = dt.Columns(i).ToString
            Next

            dv = dt.DefaultView
            DataGridView1.DataSource = dv
        Catch ex As Exception
            MsgBox("Error al cargar los datos")
            Log.Error($"Ocurrió un error. Error: {ex.Message}")
        Finally
            closeConnection()
        End Try
    End Sub

    Private Sub frmCatalogoProducto_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
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
            DateTimePicker1.CustomFormat = "dd/MM/yyyy"
            DateTimePicker1.Value = Now
            getDatos()
        Catch ex As Exception
            MessageBox.Show("Error al cargar la información", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Log.Error($"Ocurrió un error. Error: {ex.Message}")
        End Try

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            txtcod.Text = getCorrelativoTrasiego(correlativo) + 1
            txtdesc.Clear()
            txtcomp.Clear()
            DateTimePicker1.Value = Today
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
            txtprecio.Text = "0.0"
            txtcosto.Text = "0.0"
            txtutilidad.Text = "0.0"
            txtEstanteria.Clear()
            txtbarcode.Clear()
            txtstockmin.Clear()
            RegOAct = 1
        Catch ex As Exception
            MessageBox.Show("Error al cargar la información", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Log.Error($"Ocurrió un error. Error: {ex.Message}")
        End Try

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            If RegOAct = 1 Then

                If Trim(txtdesc.Text) = "" Then
                    MsgBox("Todos los campos son obligatorios", MsgBoxStyle.Information, "Faltan datos")
                Else
                    If MessageBox.Show("¿Desea guardar este registro?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then


                        Dim sql As String = "INSERT INTO PRODUCTO VALUES(@desc, @comp, @pres, @at, @indi, @con, @obs, @pro, @med, @cat, @lab, @prec, @cost, @fi, @est, @bar, @stock)"
                        Dim cmd As SqlCommand
                        cmd = New SqlCommand(sql, conn)

                        cmd.Parameters.AddWithValue("desc", Trim(txtdesc.Text))
                        cmd.Parameters.AddWithValue("comp", Trim(txtcomp.Text))

                        cmd.Parameters.AddWithValue("pres", Trim(txtpres.Text))
                        cmd.Parameters.AddWithValue("at", Trim(txtat.Text))
                        cmd.Parameters.AddWithValue("indi", Trim(txtindi.Text))

                        cmd.Parameters.AddWithValue("con", Trim(txtcontra.Text))
                        cmd.Parameters.AddWithValue("obs", Trim(txtobs.Text))
                        cmd.Parameters.AddWithValue("pro", Trim(cmbpro.SelectedValue))
                        cmd.Parameters.AddWithValue("med", Trim(txtmed.Text))
                        cmd.Parameters.AddWithValue("cat", Trim(cmbcat.SelectedValue))
                        cmd.Parameters.AddWithValue("lab", Trim(txtlab.Text))
                        cmd.Parameters.AddWithValue("prec", CDbl(txtprecio.Text))
                        cmd.Parameters.AddWithValue("cost", CDbl(txtcosto.Text))
                        cmd.Parameters.AddWithValue("fi", DateTimePicker1.Value)
                        cmd.Parameters.AddWithValue("est", If(txtEstanteria.Text.Trim() = "", DBNull.Value, CInt(txtEstanteria.Text)))
                        cmd.Parameters.AddWithValue("bar", Trim(txtbarcode.Text))
                        cmd.Parameters.AddWithValue("stock", If(String.IsNullOrEmpty(txtstockmin.Text), 0, CInt(txtstockmin.Text)))
                        Try
                            openConnection()
                            cmd.ExecuteNonQuery()
                            MessageBox.Show("El registro se guardó correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            TextBox1.Clear()
                            txtdesc.Clear()
                            txtcomp.Clear()

                            txtpres.Clear()
                            txtat.Clear()
                            DateTimePicker1.Value = Today
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
                End If

                RegOAct = 0

            Else
                If MessageBox.Show("¿Desea guardar los cambios de este registro?", "Guardar cambios", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    Dim sqlupdate As String = "UPDATE PRODUCTO SET dProducto = @desc, " _
                                              & "composicion = @comp, presentacion = @pres, aterapeutica = @at, " _
                                              & "indicaciones = @indi, contraindicaciones = @con, observaciones = @obs, " _
                                              & "proveedor = @pro, medida = @med, categoria = @cat, laboratorio = @lab, precio = @prec, costo = @cost, fechaRegistro = @fi, estanteria = @est, barcode = @bar, stockmin = @stock WHERE idProducto = @id"
                    Dim cmd As SqlCommand
                    cmd = New SqlCommand(sqlupdate, conn)

                    cmd.Parameters.AddWithValue("desc", Trim(txtdesc.Text))
                    cmd.Parameters.AddWithValue("comp", Trim(txtcomp.Text))

                    cmd.Parameters.AddWithValue("pres", Trim(txtpres.Text))
                    cmd.Parameters.AddWithValue("at", Trim(txtat.Text))
                    cmd.Parameters.AddWithValue("indi", Trim(txtindi.Text))

                    cmd.Parameters.AddWithValue("con", Trim(txtcontra.Text))
                    cmd.Parameters.AddWithValue("obs", Trim(txtobs.Text))
                    cmd.Parameters.AddWithValue("pro", Trim(cmbpro.SelectedValue))
                    cmd.Parameters.AddWithValue("med", Trim(txtmed.Text))
                    cmd.Parameters.AddWithValue("cat", Trim(cmbcat.SelectedValue))
                    cmd.Parameters.AddWithValue("lab", Trim(txtlab.Text))
                    cmd.Parameters.AddWithValue("prec", CDbl(txtprecio.Text))
                    cmd.Parameters.AddWithValue("cost", CDbl(txtcosto.Text))
                    cmd.Parameters.AddWithValue("fi", DateTimePicker1.Value)
                    cmd.Parameters.AddWithValue("id", CInt(txtcod.Text))
                    cmd.Parameters.AddWithValue("est", CInt(txtEstanteria.Text))
                    cmd.Parameters.AddWithValue("bar", Trim(txtbarcode.Text))
                    cmd.Parameters.AddWithValue("stock", CInt(Val(txtstockmin.Text)))
                    Try
                        openConnection()
                        cmd.ExecuteNonQuery()
                        TextBox1.Clear()


                        MessageBox.Show("La información del producto se actualizó de forma correcta", "Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Catch ex As Exception
                        MessageBox.Show("Algo salió mal" & vbCrLf & "Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Finally
                        closeConnection()
                        cargarDGVProd()
                    End Try
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error en el ingreso de los datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Log.Error($"Ocurrió un error. Error: {ex.Message}")
        End Try

    End Sub



    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            If Trim(txtcod.Text) = "" Or Trim(txtdesc.Text) = "" Then
                MessageBox.Show("No se ha elegido ningún registro para eliminar", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else


                If MessageBox.Show("¿Desea eliminar este registro?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    Dim sqldelete As String = "DELETE FROM PRODUCTO WHERE idProducto = @id"
                    Dim comand As SqlCommand

                    comand = New SqlCommand(sqldelete, conn)
                    comand.Parameters.AddWithValue("id", CInt(txtcod.Text))
                    Try
                        openConnection()
                        comand.ExecuteNonQuery()
                        MessageBox.Show("El registro se eliminó de forma correcta", "Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        TextBox1.Clear()
                        txtcod.Clear()
                        txtdesc.Clear()
                        txtcomp.Clear()

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
        Catch ex As Exception
            MessageBox.Show("Error al borrar el registro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Log.Error($"Ocurrió un error. Error: {ex.Message}")
        End Try

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If MessageBox.Show("¿Desea salir de esta ventana?", "Saliendo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub txtcosto_TextChanged(sender As Object, e As EventArgs) Handles txtcosto.TextChanged
        Try
            txtutilidad.Text = Val(txtprecio.Text) - Val(txtcosto.Text)
        Catch ex As Exception

        End Try
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

        Try
            dv.RowFilter = String.Format("Convert(" & criterio & ", 'System.String') LIKE '%{0}%'", Trim(TextBox1.Text))
        Catch ex As Exception

        End Try
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