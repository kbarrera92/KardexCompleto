Imports System.Data.SqlClient

Public Class frmPuntoDeVentaMejorado

    'Modo "pago de vale": la venta se arma con las líneas de un vale y no admite cambios.
    'Al cobrar, frmCobrar registra el pago del vale (sp_pagaVale) en lugar de una venta normal.
    Public idValeEnCobro As Integer = 0
    Public idVendedorVale As Integer = 0
    Public lineasVale As New List(Of Short)

    Private Function TextoNota() As String
        Return "Nota" & vbCrLf _
            & "Puede identificar el artículo " & vbCrLf _
            & "con el código de barra o" & vbCrLf _
            & " manualmente."
    End Function

    ''' <summary>
    ''' Carga las líneas de un vale en la venta y bloquea su edición. Devuelve False si el usuario
    ''' decide no descartar los productos que ya había en el punto de venta.
    ''' </summary>
    Public Function CargarVale(ByVal idVale As Integer, ByVal idVendedor As Integer, ByVal nombreVendedor As String, ByVal lineas As DataTable) As Boolean
        If DataGridView1.Rows.Count > 0 Then
            If MessageBox.Show("El punto de venta tiene productos agregados. ¿Desea descartarlos para cobrar el vale?", "Pagar vale",
                               MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
                Return False
            End If
        End If

        DataGridView1.Rows.Clear()
        lineasVale = New List(Of Short)

        For Each fila As DataRow In lineas.Rows
            Dim precio As Decimal = Convert.ToDecimal(fila("precio"))
            Dim cantidad As Integer = Convert.ToInt32(fila("cantidad"))
            DataGridView1.Rows.Add(fila("producto"), fila("descripcion"), cantidad, precio, String.Format("{0:N2}", precio * cantidad))
            lineasVale.Add(Convert.ToInt16(fila("nDetalle")))
        Next

        idValeEnCobro = idVale
        idVendedorVale = idVendedor
        txttotal.Text = String.Format("{0:N2}", calculartotal())
        txttotalarti.Text = String.Format("{0:N2}", calculartotalarti())
        lblNota.Text = $"PAGO DEL VALE No. {idVale}" & vbCrLf & $"Vendedor: {nombreVendedor}"
        BloquearEdicion(True)
        Return True
    End Function

    Private Sub SalirModoVale()
        If idValeEnCobro = 0 Then Return
        idValeEnCobro = 0
        idVendedorVale = 0
        lineasVale = New List(Of Short)
        lblNota.Text = TextoNota()
        BloquearEdicion(False)
    End Sub

    Private Sub BloquearEdicion(ByVal bloquear As Boolean)
        txtbarcode.Enabled = Not bloquear
        btbuscar.Enabled = Not bloquear
        Button1.Enabled = Not bloquear
    End Sub

    Private Sub frmPuntoDeVentaMejorado_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        With DataGridView1
            .EnableHeadersVisualStyles = False
            .ColumnHeadersDefaultCellStyle.BackColor = Color.DarkBlue
            .ColumnHeadersDefaultCellStyle.ForeColor = Color.White
            .DefaultCellStyle.ForeColor = Color.DarkBlue
            .DefaultCellStyle.SelectionBackColor = Color.LightBlue
            .BorderStyle = BorderStyle.None
            .CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical
        End With

        Estilos.AplicarEstilos(Me)
        Button6.Visible = ConsultaParametro("verMisVentas") <> "N"
        If idValeEnCobro = 0 Then lblNota.Text = TextoNota()
    End Sub

    Sub cleanAll()
        SalirModoVale()
        DateTimePicker1.Value = DateTime.Now.Date

        DataGridView1.Rows.Clear()
        txtcorrelativo.Clear()
        txtbarcode.Clear()
        txttotal.Clear()
        txttotalarti.Clear()

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub txtbarcode_KeyDown(sender As Object, e As KeyEventArgs) Handles txtbarcode.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim reader As SqlDataReader

            Try
                openConnection()
                Dim query As String = "SELECT P.dProducto, ISNULL(P.presentacion, '') as presentacion, ISNULL(P.laboratorio, '') as laboratorio, ISNULL(P.medida, '') " _
                                      & "as medida, C.categoria, P.precio, P.estanteria, P.idProducto FROM PRODUCTOS P " _
                                      & "INNER JOIN CATEGORIA C " _
                                      & "ON P.categoria = C.idCategoria " _
                                      & "WHERE P.barcode = @pro"

                Dim cmd As New SqlCommand(query, conn)
                cmd.Parameters.AddWithValue("pro", txtbarcode.Text)
                reader = cmd.ExecuteReader
                reader.Read()

                If reader.HasRows Then
                    Dim prod As Integer = reader(7)
                    Dim desc As String = reader(0)
                    Dim precio As Double = reader(5)
                    Dim subtotal As Double = reader(5) * 1
                    reader.Close()

                    DataGridView1.Rows.Add(prod, desc, 1, String.Format("{0:N2}", precio), String.Format("{0:N2}", subtotal))
                    txttotal.Text = String.Format("{0:N2}", calculartotal())
                    txttotalarti.Text = String.Format("{0:N2}", calculartotalarti())


                Else
                    MsgBox("No se encontraron coincidencias", MsgBoxStyle.Critical, "Error en los datos")
                    reader.Close()
                End If


            Catch ex As Exception
                MsgBox("No se encontraron coincidencias", MsgBoxStyle.Critical, "Error en los datos")
            Finally
                closeConnection()
                txtbarcode.Clear()
            End Try
        End If


    End Sub

    Function calculartotal() As Double
        Dim total As Double = 0

        For Each row As DataGridViewRow In DataGridView1.Rows
            total += row.Cells(4).Value
        Next

        Return total
    End Function

    Function calculartotalarti() As Integer
        Dim total As Integer = 0

        For Each row As DataGridViewRow In DataGridView1.Rows
            total += row.Cells(2).Value
        Next

        Return total
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If idValeEnCobro > 0 Then Return
        If DataGridView1.SelectedRows.Count = 0 Then
            MessageBox.Show("Debe seleccionar un detalle", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            DataGridView1.Rows.RemoveAt(DataGridView1.CurrentRow.Index)
            txttotal.Text = String.Format("{0:N2}", calculartotal())
            txttotalarti.Text = String.Format("{0:N2}", calculartotalarti())
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If DataGridView1.Rows.Count = 0 Then
            MsgBox("No se ha agregado ningún producto", MsgBoxStyle.Exclamation, "Faltan datos")
        Else
            'crear la datatable
            table = New DataTable()
            table.Columns.Add("nDetalleV", GetType(Short))
            table.Columns.Add("producto", GetType(Integer))
            table.Columns.Add("cantidad", GetType(Integer))
            table.Columns.Add("precio", GetType(Decimal))
            table.Columns.Add("subtotal", GetType(Decimal))

            For index = 0 To DataGridView1.Rows.Count - 1
                table.Rows.Add(index + 1, DataGridView1.Rows(index).Cells(0).Value,
                               DataGridView1.Rows(index).Cells(2).Value, DataGridView1.Rows(index).Cells(3).Value, DataGridView1.Rows(index).Cells(4).Value)
            Next

            pv = 1
            frmCobrar.txttotal.Text = Replace(txttotal.Text, ",", "")
            frmCobrar.txtpago.Select()
            frmCobrar.Show()
        End If

    End Sub

    Private Sub btbuscar_Click(sender As Object, e As EventArgs) Handles btbuscar.Click
        If idValeEnCobro > 0 Then Return
        datosreq = 1
        frmProductos.Show()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        SalirModoVale()
        DataGridView1.Rows.Clear()
        txtbarcode.Clear()
        txttotal.Text = "0.00"
        txttotalarti.Text = 0
        txtbarcode.Focus()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        frmVentasDiarias.Show()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If ConsultaParametro("verMisVentas") = "N" Then Return
        FormMisVentas.Show()
        FormMisVentas.Activate()
    End Sub
End Class