Imports System.Data.SqlClient
Public Class frmTraslados

    Dim sqlSuc As String = "SELECT idSucursal, nombreSuc FROM SUCURSAL where idSucursal <> @idSuc"
    Dim sqlSuc2 As String = "SELECT idSucursal, nombreSuc FROM SUCURSAL where idSucursal = @idSuc"
    Dim sqlSuc3 As String = "SELECT idSucursal, nombreSuc FROM SUCURSAL"

    Function updateList(ByVal sql As String, ByVal sucursal As String) As DataTable
        Dim da As SqlDataAdapter
        Dim dt As New DataTable
        Dim comando As New SqlCommand()
        With comando
            .CommandText = sql
            .CommandType = CommandType.Text
            .Connection = conn
            .Parameters.AddWithValue("idSuc", sucursal)
        End With

        Try
            openConnection()
            da = New SqlDataAdapter(comando)
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            MsgBox(ex.Message)
            Return Nothing
        End Try
    End Function

    Private Sub frmTraslados_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DataGridView1.AutoGenerateColumns = False
        If nombreRol = "BODEGUERO" Or nombreRol = "VENDEDOR" Then
            DataGridView1.Columns("costo").Visible = False
            cmbSucSalida.DataSource = updateList(sqlSuc2, ConsultaParametro("codigoSucursal"))
            cmbSucSalida.DisplayMember = updateList(sqlSuc2, ConsultaParametro("codigoSucursal")).Columns(1).ToString
            cmbSucSalida.ValueMember = updateList(sqlSuc2, ConsultaParametro("codigoSucursal")).Columns(0).ToString
        Else
            cmbSucSalida.DataSource = updateList(sqlSuc3, ConsultaParametro("codigoSucursal"))
            cmbSucSalida.DisplayMember = updateList(sqlSuc3, ConsultaParametro("codigoSucursal")).Columns(1).ToString
            cmbSucSalida.ValueMember = updateList(sqlSuc3, ConsultaParametro("codigoSucursal")).Columns(0).ToString
        End If

        cmbSucEntranda.DataSource = updateList(sqlSuc, Convert.ToInt32(cmbSucSalida.SelectedValue))
        cmbSucEntranda.DisplayMember = updateList(sqlSuc, Convert.ToInt32(cmbSucSalida.SelectedValue)).Columns(1).ToString
        cmbSucEntranda.ValueMember = updateList(sqlSuc, Convert.ToInt32(cmbSucSalida.SelectedValue)).Columns(0).ToString

        mskfecha.Text = Format(DateTime.Now, "dd/MM/yyyy")
        txtUsuarioEnvia.Text = nameUsuarioActual
        Button2.PerformClick()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        
        cmbSucEntranda.SelectedIndex = -1
        cmbSucSalida.SelectedIndex = -1
        
        DataGridView2.Rows.Clear()
        DataGridView1.DataSource = Nothing
        DataGridView1.Rows.Clear()

        txtcodpro.Clear()
        txtdescpro.Clear()
        txtlabpro.Clear()
        txtcantidad.Clear()
        txtexistencia.Clear()
        txtexissucent.Clear()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If Val(txtcantidad.Text) = 0 Or Val(txtcantidad.Text) > Val(txtexistencia.Text) Then
            MsgBox("Cantidad no válida", MsgBoxStyle.Critical, "Error")
        Else
            If Trim(txtcodpro.Text) = "" Then
                MsgBox("Faltan datos", MsgBoxStyle.Critical, "Error")
            Else
                DataGridView2.Rows.Add(txtcodpro.Text, txtdescpro.Text, txtcantidad.Text)
                txtcodpro.Clear()
                txtdescpro.Clear()
                txtlabpro.Clear()
                txtcantidad.Clear()
                txtexistencia.Clear()
                txtexissucent.Clear()
            End If
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If cmbSucEntranda.SelectedIndex = -1 Or cmbSucSalida.SelectedIndex = -1 Then
            MsgBox("No se ha seleccionado la sucursal de destino o de salida", MsgBoxStyle.Exclamation, "Faltan datos")
        Else
            If DataGridView2.Rows.Count = 0 Then
                MsgBox("No ha agregado productos para trasladar", MsgBoxStyle.Exclamation, "Faltan datos")
            Else
                If MessageBox.Show("¿Desea realizar el traslado de estos productos?", "Realizar traslado", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    guardarSalidaxTraslado()
                    
                    Button2.PerformClick()
                End If
            End If
            
        End If
        
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If cmbSucSalida.SelectedIndex = -1 Then
            MsgBox("Debe elegir una sucursal de salida", MsgBoxStyle.Exclamation, "Faltan datos")
        Else
            datosreq = 2
            frmProductos.Show()
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
                    Dim query As String = "SELECT dProducto, laboratorio " _
                                          & "FROM PRODUCTO " _
                                          & "WHERE idProducto = @id"

                    Dim cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("id", CInt(txtcodpro.Text))

                    reader = cmd.ExecuteReader
                    reader.Read()

                    If reader.HasRows Then
                        txtdescpro.Text = reader(0)
                        txtlabpro.Text = reader(1)
                        reader.Close()
                        txtexistencia.Text = getStock(CInt(cmbSucSalida.SelectedValue.ToString), CInt(txtcodpro.Text), "sp_getStoc")
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

    Private Sub txtcantidad_KeyDown(sender As Object, e As KeyEventArgs) Handles txtcantidad.KeyDown
        If e.KeyCode = Keys.Enter Then
            Button3.PerformClick()

        End If

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Me.Close()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If DataGridView2.Rows.Count = 0 Or DataGridView2.SelectedRows.Count = 0 Then
            MsgBox("Debe seleccionar un registro para eliminarlo", MsgBoxStyle.Critical, "¡No hay nada para eliminar!")
        Else
            DataGridView2.Rows.RemoveAt(DataGridView2.CurrentRow.Index)
        End If
    End Sub

    Private Sub DataGridView2_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView2.KeyDown
        If e.KeyCode = Keys.Delete Then
            Button4.PerformClick()
        End If
    End Sub

   
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        frmVerTraslados.Show()
    End Sub

    Private Sub cmbSucSalida_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSucSalida.SelectedIndexChanged
        Try
            fillDGVSP("sp_infoProdCompras", DataGridView1, Me, CInt(cmbSucSalida.SelectedValue.ToString))
        Catch ex As Exception

        End Try

    End Sub

    
    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        Try
            txtcodpro.Text = DataGridView1.CurrentRow.Cells(0).Value
            txtdescpro.Text = DataGridView1.CurrentRow.Cells(1).Value
            txtlabpro.Text = DataGridView1.CurrentRow.Cells(6).Value
            txtexistencia.Text = DataGridView1.CurrentRow.Cells(2).Value

            txtexissucent.Text = getStock(CInt(cmbSucEntranda.SelectedValue.ToString), CInt(txtcodpro.Text), "sp_getStoc")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtcantidad.Select()
        End If
        e.SuppressKeyPress = True
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles txtbuscarpro.TextChanged
        Dim filt As String
        Try
            filt = String.Format("dProducto like '%{0}%' Or Convert(idProducto,'System.String') like '{0}%'", txtbuscarpro.Text)
            dv.RowFilter = filt
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub txtbuscarpro_Click(sender As Object, e As EventArgs) Handles txtbuscarpro.Click
        Try
            fillDGVSP("sp_infoProdCompras", DataGridView1, Me, CInt(cmbSucSalida.SelectedValue.ToString))
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtbuscarpro_KeyDown(sender As Object, e As KeyEventArgs) Handles txtbuscarpro.KeyDown
        If e.KeyData = Keys.Enter Then
            DataGridView1.Select()
        End If
    End Sub
End Class