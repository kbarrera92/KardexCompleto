Imports System.Data.SqlClient
Imports Serilog

Public Class frmCatalogoProductoLaBendicion
    Dim RegOAct As Integer = 0
    Dim ds As New DataSet
    Dim correlativo As String = "SELECT IDENT_CURRENT ('PRODUCTOS') AS Current_Identity"
    Dim sqlCat As String = "SELECT idCategoria, categoria FROM CATEGORIA"
    Dim sqlProv As String = "SELECT idProveedor, rzProveedor FROM PROVEEDOR"
    Dim fila As Integer
    Dim criterio As String
    Private dtPreciosSucursal As DataTable

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
            txtdesc.Text = DataGridView1.Rows(fila).Cells(1).Value

            txtpres.Text = DataGridView1.Rows(fila).Cells(3).Value

            txtExistencia.Text = DataGridView1.Rows(fila).Cells(5).Value

            ind1 = cmbpro.FindStringExact(DataGridView1.Rows(fila).Cells(8).Value)
            cmbpro.SelectedIndex = ind1

            txtobs.Text = DataGridView1.Rows(fila).Cells(7).Value
            txtmed.Text = DataGridView1.Rows(fila).Cells(9).Value

            ind2 = cmbcat.FindStringExact(DataGridView1.Rows(fila).Cells(10).Value)
            cmbcat.SelectedIndex = ind2
            txtprecio.Text = DataGridView1.Rows(fila).Cells(12).Value
            txtcosto.Text = DataGridView1.Rows(fila).Cells(13).Value
            DateTimePicker1.Value = Convert.ToDateTime(DataGridView1.Rows(fila).Cells(14).Value)
            txtutilidad.Text = FormatNumber(Val(txtprecio.Text) - Val(txtcosto.Text), 2)
            txtbarcode.Clear()
            txtbarcode.Text = DataGridView1.Rows(fila).Cells(16).Value
            txtstockmin.Clear()
            txtstockmin.Text = DataGridView1.Rows(fila).Cells(17).Value
            txtExistencia.Text = getStock(sucActual, CInt(txtcod.Text), "sp_getStoc").ToString()
            Dim flag As Char = If(DataGridView1.Rows(fila).Cells(18).Value, "N")
            Select Case flag
                Case "N"
                    ComboBoxFlag.SelectedIndex = 0
                Case "C"
                    ComboBoxFlag.SelectedIndex = 1
                Case Else
                    ComboBoxFlag.SelectedIndex = 2
            End Select
        Catch ex As Exception
            Log.Error($"Ocurrió un error. Error: {ex.Message}")
        End Try
    End Sub

    Public Sub DeshabilitaProducto()
        Dim parametros As New List(Of SqlParameter) From {
            New SqlParameter("@accion", SqlDbType.VarChar, 10) With {.Value = "ELIMINAR"},
            New SqlParameter("@idProducto", SqlDbType.Int) With {.Value = Convert.ToInt32(txtcod.Text)},
            New SqlParameter("@msg", SqlDbType.VarChar, 200) With {.Direction = ParameterDirection.Output},
            New SqlParameter("@returnValue", SqlDbType.Int) With {.Direction = ParameterDirection.ReturnValue}
        }

        Dim resultado = EjecutarStoredProcedureMultiple("sp_CRUD_PRODUCTOS", parametros)

        Dim codigoRetorno As Integer = Convert.ToInt32(parametros.Find(Function(p) p.ParameterName = "@returnValue").Value)
        Dim mensajeSalida As String = parametros.Find(Function(p) p.ParameterName = "@msg").Value.ToString()
        MessageBox.Show(mensajeSalida, "Resultado", MessageBoxButtons.OK, IIf(codigoRetorno = 0, MessageBoxIcon.Information, MessageBoxIcon.Error))
    End Sub

    Public Sub IAProducto(ByVal accion As String)
        Try
            Dim estanteria As Object
            estanteria = DBNull.Value

            Dim parametros As New List(Of SqlParameter) From {
                New SqlParameter("@accion", SqlDbType.VarChar, 10) With {.Value = accion},
                New SqlParameter("@dProducto", SqlDbType.VarChar, 150) With {.Value = txtdesc.Text},
                New SqlParameter("@composicion", SqlDbType.VarChar, 150) With {.Value = DBNull.Value},
                New SqlParameter("@presentacion", SqlDbType.VarChar, 100) With {.Value = txtpres.Text},
                New SqlParameter("@aterapeutica", SqlDbType.VarChar, 150) With {.Value = DBNull.Value},
                New SqlParameter("@indicaciones", SqlDbType.VarChar, 150) With {.Value = DBNull.Value},
                New SqlParameter("@contraindicaciones", SqlDbType.VarChar, 150) With {.Value = DBNull.Value},
                New SqlParameter("@observaciones", SqlDbType.VarChar, 250) With {.Value = txtobs.Text},
                New SqlParameter("@proveedor", SqlDbType.Int) With {.Value = Convert.ToInt32(cmbpro.SelectedValue)},
                New SqlParameter("@medida", SqlDbType.VarChar, 75) With {.Value = txtmed.Text},
                New SqlParameter("@categoria", SqlDbType.Int) With {.Value = Convert.ToInt32(cmbcat.SelectedValue)},
                New SqlParameter("@laboratorio", SqlDbType.VarChar, 100) With {.Value = DBNull.Value},
                New SqlParameter("@precio", SqlDbType.Decimal) With {.Value = Convert.ToDecimal(txtprecio.Text)},
                New SqlParameter("@costo", SqlDbType.Decimal) With {.Value = Convert.ToDecimal(txtcosto.Text)},
                New SqlParameter("@fechaRegistro", SqlDbType.Date) With {.Value = Date.Now},
                New SqlParameter("@estanteria", SqlDbType.Int) With {.Value = estanteria},
                New SqlParameter("@barcode", SqlDbType.VarChar, 25) With {.Value = txtbarcode.Text},
                New SqlParameter("@stockmin", SqlDbType.Int) With {.Value = Convert.ToInt32(If(String.IsNullOrWhiteSpace(txtstockmin.Text), 0, txtstockmin.Text))},
                New SqlParameter("@estado", SqlDbType.Bit) With {.Value = 1},
                New SqlParameter("@flag", SqlDbType.Char, 2) With {.Value = ComboBoxFlag.Text.Substring(0, 1)},
                New SqlParameter("@existencia", SqlDbType.Decimal) With {.Value = Convert.ToDecimal(txtExistencia.Text)},
                New SqlParameter("@sucursal", SqlDbType.Int) With {.Value = sucActual},
                New SqlParameter("@msg", SqlDbType.VarChar, 200) With {.Direction = ParameterDirection.Output},
                New SqlParameter("@returnValue", SqlDbType.Int) With {.Direction = ParameterDirection.ReturnValue}
            }

            Dim parametroExtra As New SqlParameter("@idProducto", SqlDbType.Int) With {.Value = If(Integer.TryParse(txtcod.Text, New Integer()), Convert.ToInt32(txtcod.Text), DBNull.Value)}
            If accion = "ACTUALIZAR" Then
                parametros.Add(parametroExtra)
            End If

            Dim parametro As New SqlParameter("@precios", SqlDbType.Structured)
            parametro.TypeName = "dbo.PRECIOSXSUCURSAL" 'tipo definido en SQL Server
            parametro.Value = dtPreciosSucursal
            parametros.Add(parametro)

            Dim paramtodb As String = String.Empty
            For Each param As SqlParameter In parametros
                If TypeOf param.Value Is DataTable Then
                    ' Es un DataTable
                    paramtodb &= param.ParameterName & "=" & If(DataTableToString(param.Value), """") & vbCrLf
                Else
                    paramtodb &= param.ParameterName & "=" & If(param.Value, """") & vbCrLf
                End If

            Next
            Log.Information($"Parametros enviados al sp: {vbCrLf}{paramtodb}")

            Dim resultado = EjecutarStoredProcedureMultiple("sp_CRUD_PRODUCTOS", parametros)

            Dim codigoRetorno As Integer = Convert.ToInt32(parametros.Find(Function(p) p.ParameterName = "@returnValue").Value)
            Dim mensajeSalida As String = parametros.Find(Function(p) p.ParameterName = "@msg").Value.ToString()
            MessageBox.Show(mensajeSalida, "Resultado", MessageBoxButtons.OK, IIf(codigoRetorno = 0, MessageBoxIcon.Information, MessageBoxIcon.Error))
        Catch ex As Exception
            Log.Error($"Ocurrio un error. Error: {ex.Message}")
        End Try

    End Sub


    Sub cargarDGVProdMejorado()
        Dim parametros As New List(Of SqlParameter) From {
            New SqlParameter("@Accion", SqlDbType.VarChar, 10) With {.Value = "LEER"},
            New SqlParameter("@msg", SqlDbType.VarChar, 200) With {.Value = DBNull.Value, .Direction = ParameterDirection.Output}
        }

        Dim resultado = EjecutarStoredProcedureMultiple("sp_CRUD_PRODUCTOS", parametros)

        Dim dataTables As List(Of DataTable) = resultado.Item1
        Dim mensajeSalida As String = resultado.Item2
        Dim parametrosSalida As List(Of SqlParameter) = resultado.Item3

        If dataTables.Count > 0 AndAlso dataTables(0).Rows.Count > 0 Then
            Dim dt As DataTable = dataTables(0)
            For i = 0 To dt.Columns.Count - 1
                DataGridView1.Columns(i).DataPropertyName = dt.Columns(i).ToString
            Next

            dv = dt.DefaultView
            DataGridView1.DataSource = dv
        End If

    End Sub

    Private Sub frmCatalogoProducto_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        With DataGridView1
            .EnableHeadersVisualStyles = False
            .ColumnHeadersDefaultCellStyle.BackColor = Color.DarkBlue
            .ColumnHeadersDefaultCellStyle.ForeColor = Color.White
            .DefaultCellStyle.SelectionBackColor = Color.LightBlue
            .BorderStyle = BorderStyle.None
            .CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical
        End With

        Estilos.AplicarEstilos(Me)

        Try
            cmbcat.DataSource = updateList(sqlCat)
            cmbcat.ValueMember = updateList(sqlCat).Columns(0).ToString
            cmbcat.DisplayMember = updateList(sqlCat).Columns(1).ToString

            cmbpro.DataSource = updateList(sqlProv)
            cmbpro.ValueMember = updateList(sqlProv).Columns(0).ToString
            cmbpro.DisplayMember = updateList(sqlProv).Columns(1).ToString

            cargarDGVProdMejorado()
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
            dtPreciosSucursal = Nothing
            txtcod.Clear()
            txtdesc.Clear()
            DateTimePicker1.Value = Today
            txtpres.Clear()

            txtExistencia.Clear()
            cmbpro.SelectedIndex = -1
            txtobs.Clear()
            txtmed.Clear()
            cmbcat.SelectedIndex = -1
            txtdesc.Select()
            txtprecio.Text = "0.0"
            txtcosto.Text = "0.0"
            txtutilidad.Text = "0.0"
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
                Log.Information("Iniciando el registro de un nuevo producto")
                If Trim(txtdesc.Text) = "" Then
                    MsgBox("Todos los campos son obligatorios", MsgBoxStyle.Information, "Faltan datos")
                Else
                    If MessageBox.Show("¿Desea guardar este registro?", "Guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        Try
                            IAProducto("INSERTAR")
                            TextBox1.Clear()
                            txtdesc.Clear()

                            txtpres.Clear()
                            DateTimePicker1.Value = Today
                            txtExistencia.Clear()
                            cmbpro.SelectedIndex = -1
                            txtobs.Clear()
                            txtmed.Clear()
                            txtprecio.Clear()
                            txtcosto.Clear()
                            cmbcat.SelectedIndex = -1
                            txtbarcode.Clear()
                            txtstockmin.Clear()
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, "Algo salió mal", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Finally
                            closeConnection()
                            cargarDGVProdMejorado()
                        End Try
                    End If
                End If
                RegOAct = 0
            Else
                If MessageBox.Show("¿Desea guardar los cambios de este registro?", "Guardar cambios", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    Log.Information("Iniciando la actualización de un producto")
                    Try
                        IAProducto("ACTUALIZAR")
                    Catch ex As Exception
                        MessageBox.Show("Error en el ingreso de los datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Log.Error($"Ocurrió un error. Error: {ex.Message}")
                    Finally
                        closeConnection()
                        cargarDGVProdMejorado()
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
                    Try
                        DeshabilitaProducto()
                        TextBox1.Clear()
                        txtcod.Clear()
                        txtdesc.Clear()

                        txtpres.Clear()

                        txtExistencia.Clear()
                        cmbpro.SelectedIndex = -1
                        txtobs.Clear()
                        txtmed.Clear()
                        txtprecio.Clear()
                        txtcosto.Clear()
                        txtbarcode.Clear()
                        txtstockmin.Clear()
                        cmbcat.SelectedIndex = -1
                        DataGridView1.Select()
                    Catch ex As Exception
                        MessageBox.Show("Algo salió mal" & vbCrLf & "Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Finally
                        closeConnection()
                        cargarDGVProdMejorado()
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
            Log.Error($"Ocurrió un error. Error: {ex.Message}")
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
            Log.Error($"Ocurrió un error. Error: {ex.Message}")
        End Try
    End Sub


    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        Try
            dtPreciosSucursal = Nothing
            fila = DataGridView1.CurrentRow.Index
            getDatos()
            RegOAct = 0
        Catch ex As Exception
            Log.Error($"Ocurrió un error. Error: {ex.Message}")
        End Try
    End Sub


    Private Sub Button5_Click(sender As Object, e As EventArgs)
        frmEstanterias.Show()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        txtbarcode.Text = "A" & txtcod.Text & "A"
    End Sub

    Private Sub btnEditarPrecios_Click(sender As Object, e As EventArgs) Handles btnEditarPrecios.Click
        Try
            If Decimal.TryParse(txtprecio.Text, New Decimal()) And Decimal.Parse(txtprecio.Text) > 0 Then
                Dim formPrecios As New FormPreciosXSucursal()

                ' Si quieres pasar el ID del producto, puedes hacerlo también (opcional)
                ' formPrecios.IdProducto = Me.txtIdProducto.Text

                If formPrecios.ShowDialog() = DialogResult.OK Then
                    dtPreciosSucursal = formPrecios.dtPrecios
                End If
            Else
                MessageBox.Show("Se debe ingresar el precio por defecto", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch ex As Exception
            Log.Error($"Ocurrio un error. Error: {ex.Message}")
            MessageBox.Show("Se debe ingresar el precio por defecto", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try

    End Sub
End Class