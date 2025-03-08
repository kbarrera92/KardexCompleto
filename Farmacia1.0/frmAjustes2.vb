Imports System.Data.SqlClient

Public Class frmAjustes2

    'Consulta para obtener el número de ajuste siguiente
    Dim correlativo As String = "SELECT IDENT_CURRENT ('AJUSTE') AS Current_Identity"
    Dim ind1 As Integer

    'Sucursal
    Dim sqlSuc As String = "SELECT idSucursal, nombreSuc FROM SUCURSAL"

    'Tipo ajuste
    Dim tipoAjuste As String = "SELECT idTipoAjuste, tipoAjuste FROM TIPOAJUSTE"
    'Proveedor
    Dim sqlprov As String = "SELECT idProveedor, rzProveedor FROM PROVEEDOR"
    Dim najuste As Integer

    Function calcularTotal() As Double
        Dim tot As Double = 0
        For i = 0 To DataGridView2.Rows.Count - 1
            tot = tot + DataGridView2.Rows(i).Cells(5).Value
        Next
        Return tot
    End Function

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

    Private Sub btnnuevaventa_Click(sender As Object, e As EventArgs) Handles btnnuevaventa.Click
        Try
            najuste = getCorrelativoTrasiego(correlativo) + 1
            lblNoCompra.Text = "Ajuste No. " & najuste
            txtbuscapro.Clear()
            txtcodpro.Clear()
            txtdescpro.Clear()
            cmbProveedor.SelectedIndex = -1
            txtcantidad.Text = "0"
            txtprecio.Text = "0.00"
            txttotal.Text = "0.00"
            DataGridView2.DataSource = Nothing
            DataGridView2.Rows.Clear()
            txtbuscapro.Select()


            'Guardar ajuste
            'guardarAjuste()
        Catch ex As NullReferenceException
            lblNoCompra.Text = "Ajuste No."
            MsgBox("Faltan datos obligatorios", MsgBoxStyle.Critical, "Faltan datos")
        Catch ex As Exception
            MsgBox("No se pudo realizar esta acción" & vbCrLf & "Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub frmAjustes2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DataGridView1.AutoGenerateColumns = False
        DataGridView2.AutoGenerateColumns = False
        If nombreRol = "BODEGUERO" Then
            DataGridView1.Columns("preciopro").Visible = False
            sqlSuc = "SELECT idSucursal, nombreSuc FROM SUCURSAL" 'WHERE nombreSuc = 'BODEGA'"'
            tipoAjuste = "SELECT idTipoAjuste, tipoAjuste FROM TIPOAJUSTE" 'WHERE tipoAjuste = 'ENTRADA'"'
        End If

        mskfecha.Text = Format(DateTime.Now, "dd/MM/yyyy")
        ComboBox2.DataSource = updateCm(sqlSuc)
        ComboBox2.DisplayMember = updateCm(sqlSuc).Columns(1).ToString
        ComboBox2.ValueMember = updateCm(sqlSuc).Columns(0).ToString

        ComboBox1.DataSource = updateCm(tipoAjuste)
        ComboBox1.DisplayMember = updateCm(tipoAjuste).Columns(1).ToString
        ComboBox1.ValueMember = updateCm(tipoAjuste).Columns(0).ToString

        cmbProveedor.DataSource = updateCm(sqlprov)
        cmbProveedor.DisplayMember = updateCm(sqlprov).Columns(1).ToString
        cmbProveedor.ValueMember = updateCm(sqlprov).Columns(0).ToString

        ComboBox1.SelectedIndex = -1
        ComboBox2.SelectedIndex = -1

        Estilos.AplicarEstilos(Me)
    End Sub



    

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        Try
            fillDGVSP("sp_infoProdCompras", DataGridView1, Me, CInt(ComboBox2.SelectedValue.ToString))
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnagregard_Click(sender As Object, e As EventArgs) Handles btnagregard.Click
        If Val(txtcantidad.Text) <= 0 Then
            MsgBox("Cantidad no válida", MsgBoxStyle.Critical, "Error")
        Else
            Try
                'Dim queryID As String = "INSERT INTO DETAJUSTE VALUES(@no, @trans, @prod, @cant, @precio, @subt);"
                'Dim comand As SqlCommand

                'comand = New SqlCommand(queryID, conn)

                Dim det As Integer
                If DataGridView2.Rows.Count = 0 Then
                    det = 1
                Else
                    det = Me.DataGridView2.Rows(Me.DataGridView2.Rows.Count - 1).Cells(0).Value + 1
                End If

                'comand.Parameters.AddWithValue("no", det)
                'comand.Parameters.AddWithValue("trans", najuste)
                'comand.Parameters.AddWithValue("prod", CInt(txtcodpro.Text))
                'comand.Parameters.AddWithValue("cant", CInt(txtcantidad.Text))
                'comand.Parameters.AddWithValue("subt", (CDbl(txtcantidad.Text) * CDbl(txtprecio.Text)))
                'comand.Parameters.AddWithValue("precio", CDbl(txtprecio.Text))


                'openConnection()
                'comand.ExecuteNonQuery()
                'closeConnection()

                DataGridView2.Rows.Add(det, CInt(txtcodpro.Text), txtdescpro.Text.Trim(), CInt(txtcantidad.Text), CDbl(txtprecio.Text), (CDbl(txtcantidad.Text) * CDbl(txtprecio.Text)))

            Catch ex As Exception
                MsgBox("Algo salió mal" & vbCrLf & "Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
            Finally

                'fillDGVSP("detAjustes", DataGridView2, Me, najuste)
                fillDGVSP("sp_infoProdCompras", DataGridView1, Me, CInt(ComboBox2.SelectedValue.ToString))
                txttotal.Text = FormatNumber(calcularTotal(), 2)
                txtbuscapro.Clear()
                txtcodpro.Clear()
                txtdescpro.Clear()
                cmbProveedor.SelectedIndex = -1
                txtprecio.Text = "0.00"
                txtcantidad.Text = "0"
                txtexistencia.Text = "0"
                txtbuscapro.Select()
            End Try

            
        End If
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
                    Dim query As String = "SELECT dProducto, PRO.rzProveedor, costo " _
                                          & "FROM PRODUCTOS " _
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
                        MsgBox("No se encontró el producto", MsgBoxStyle.Critical, ConsultaParametro("nombreEmpresa"))
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

    Private Sub btneliminard_Click(sender As Object, e As EventArgs) Handles btneliminard.Click
        If DataGridView2.Rows.Count = 0 Or DataGridView2.SelectedRows.Count = 0 Then
            MsgBox("Debe seleccionar un registro para eliminarlo", MsgBoxStyle.Critical, "¡No hay nada para eliminar!")
        Else
            Try
                Dim ndet As Integer = DataGridView2.CurrentRow.Cells(0).Value
                Dim ntrans As Integer = getCorrelativoTrasiego("SELECT IDENT_CURRENT ('AJUSTE') AS Current_Identity")

                Dim cmd As SqlCommand
                Dim sqlDeleteDet As String = "DELETE FROM DETAJUSTE WHERE ndetajuste = @ndet AND najuste = @najuste"

                cmd = New SqlCommand(sqlDeleteDet, conn)

                With cmd.Parameters
                    .AddWithValue("ndet", ndet)
                    .AddWithValue("najuste", ntrans)
                End With

                openConnection()
                cmd.ExecuteNonQuery()
                closeConnection()
                MsgBox("Eliminado correctamente", MsgBoxStyle.Information, "Borrado")
            Catch ex As Exception
                MsgBox("No se puede borrar este detalle" & vbCrLf & "Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
            Finally
                fillDGVSP("detAjustes", DataGridView2, Me, getCorrelativoTrasiego("SELECT IDENT_CURRENT ('AJUSTE') AS Current_Identity"))
                txttotal.Text = FormatNumber(calcularTotal(), 2)
            End Try

            
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If lblNoCompra.Text = "Ajuste No." Then

        Else
            If MessageBox.Show("¿Desea descartar este ajuste", "Descartar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) Then
                Try

                    Dim ntrans As Integer = najuste

                    Dim cmd As SqlCommand
                    Dim sqlDeleteDet As String = "DELETE FROM AJUSTE WHERE nAjuste = @najuste"

                    cmd = New SqlCommand(sqlDeleteDet, conn)

                    With cmd.Parameters

                        .AddWithValue("najuste", ntrans)
                    End With

                    openConnection()
                    cmd.ExecuteNonQuery()
                    closeConnection()
                    MsgBox("Eliminado correctamente", MsgBoxStyle.Information, "Borrado")
                Catch ex As Exception
                    MsgBox("No se puede borrar este ajuste?" & vbCrLf & "Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
                Finally
                    DataGridView2.DataSource = Nothing
                    DataGridView2.Rows.Clear()
                    txttotal.Text = FormatNumber(calcularTotal(), 2)

                    lblNoCompra.Text = "Ajuste No."
                    txtbuscapro.Clear()
                    txtcodpro.Clear()
                    txtdescpro.Clear()
                    cmbProveedor.SelectedIndex = -1
                    txtcantidad.Text = "0"
                    txtprecio.Text = "0.00"
                    txttotal.Text = "0.00"
                    ComboBox2.SelectedIndex = -1
                    ComboBox1.SelectedIndex = -1
                    txtconcep.Clear()
                    mskfecha.Text = Format(DateTime.Now, "dd/MM/yyyy")
                    DataGridView2.DataSource = Nothing
                    DataGridView2.Rows.Clear()
                    txtbuscapro.Select()
                    najuste = Nothing
                End Try
            End If
        End If
        
    End Sub

    Private Sub txtbuscapro_Click(sender As Object, e As EventArgs) Handles txtbuscapro.Click
        Try
            fillDGVSP("sp_infoProdCompras", DataGridView1, Me, CInt(ComboBox2.SelectedValue.ToString))
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnregistrarc_Click(sender As Object, e As EventArgs) Handles btnregistrarc.Click

        If lblNoCompra.Text = "Ajuste No." Then

        Else
            Try
                Dim na As Integer = najuste
                Dim sqlRegis As String = "UPDATE AJUSTE SET total = @total WHERE nAjuste = @na"
                Dim cmd As SqlCommand

                cmd = New SqlCommand(sqlRegis, conn)

                cmd.Parameters.AddWithValue("na", na)
                cmd.Parameters.AddWithValue("total", CDbl(txttotal.Text))

                openConnection()
                cmd.ExecuteNonQuery()
                closeConnection()
                MsgBox("Ajuste registrado correctamente", MsgBoxStyle.Information, "Guardado")

            Catch ex As Exception
                MsgBox("No se actualizó el total del ajuste", MsgBoxStyle.Exclamation, "Error")
            Finally
                fillDGVSP("sp_infoProdCompras", DataGridView1, Me, CInt(ComboBox2.SelectedValue.ToString))
                lblNoCompra.Text = "Ajuste No."
                txtbuscapro.Clear()
                txtcodpro.Clear()
                txtdescpro.Clear()
                cmbProveedor.SelectedIndex = -1
                txtcantidad.Text = "0"
                txtprecio.Text = "0.00"
                txttotal.Text = "0.00"
                ComboBox2.SelectedIndex = -1
                ComboBox1.SelectedIndex = -1
                txtconcep.Clear()
                mskfecha.Text = Format(DateTime.Now, "dd/MM/yyyy")
                DataGridView2.DataSource = Nothing
                DataGridView2.Rows.Clear()
                txtbuscapro.Select()
                najuste = Nothing
            End Try
        End If
    End Sub

    Private Sub btnsalir_Click(sender As Object, e As EventArgs) Handles btnsalir.Click
        Me.Close()
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        frmVerAjustes.Show()
    End Sub

    
    Private Sub txtbuscapro_TextChanged(sender As Object, e As EventArgs) Handles txtbuscapro.TextChanged
        Dim filt As String
        Try
            filt = String.Format("dProducto like '%{0}%' Or Convert(idProducto,'System.String') like '{0}%'", txtbuscapro.Text)
            dv.RowFilter = filt
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            Dim sqlRegis As String = "UPDATE PRODUCTOS SET barcode = @bar WHERE idProducto = @id"
            Dim cmd As SqlCommand

            cmd = New SqlCommand(sqlRegis, conn)

            cmd.Parameters.AddWithValue("bar", Integer.Parse(txtbarcode.Text.Trim))
            cmd.Parameters.AddWithValue("id", CDbl(txtcodpro.Text))

            openConnection()
            cmd.ExecuteNonQuery()
            closeConnection()
            MsgBox("Código de barra registrado correctamente", MsgBoxStyle.Information, "Guardado")
            txtbarcode.Clear()
        Catch ex As Exception
            MsgBox("Hubo un error al registrar el código de barra", MsgBoxStyle.Information, "Error")
        End Try
    End Sub
End Class