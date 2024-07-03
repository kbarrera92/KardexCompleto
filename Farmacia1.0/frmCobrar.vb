Imports System.Data.SqlClient
Imports System.IO
Imports System.Text

Public Class frmCobrar

    Dim sql As String = "SELECT idSerie, letra FROM SERIEFACTURA WHERE sucursal = " & sucActual

    Sub actualizarVenta()
        Dim sqlUpdate As String = "UPDATE VENTA SET cliente = @cli, documento = @doc, fechaVenta = @fech, total = @t, efectivo = @ef, tarjeta = @tar, autorizacion = @aut WHERE nVenta = @nv"
        Dim cmd As SqlCommand

        Try
            cmd = New SqlCommand(sqlUpdate, conn)
            With cmd.Parameters
                .AddWithValue("cli", Trim(txtnit.Text))
                .AddWithValue("doc", If(Trim(txtFactura.Text) = "", DBNull.Value, CInt(txtFactura.Text)))
                .AddWithValue("fech", Convert.ToDateTime(txtfecha.Text))
                .AddWithValue("t", CDbl(txttotal.Text))
                .AddWithValue("ef", (CDbl(txtpago.Text) + CDbl(txttarjeta.Text)) - CDbl(txtcambio.Text))
                .AddWithValue("tar", CDbl(txttarjeta.Text))
                .AddWithValue("aut", txtautori.Text)
                If pv = 1 Then
                    .AddWithValue("nv", CInt(frmPuntoDeVentaMejorado.txtcorrelativo.Text))

                End If

            End With

            openConnection()
            cmd.ExecuteNonQuery()
            closeConnection()
            MsgBox("Se grabo correctamente el registro", MsgBoxStyle.Information, "Éxito")
            ImprimeTicket()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Sub GrabaVenta(ByVal table As DataTable)
        Dim cmd As SqlCommand

        Try
            cmd = New SqlCommand()
            With cmd
                .CommandText = "grabaVenta"
                .CommandType = CommandType.StoredProcedure
                .Connection = conn
            End With

            With cmd.Parameters
                .AddWithValue("usuario", usuarioActual)
                .AddWithValue("sucursal", sucActual)
                .AddWithValue("cliente", Trim(txtnit.Text))
                .AddWithValue("documento", If(Trim(txtFactura.Text) = "", DBNull.Value, CInt(txtFactura.Text)))
                .AddWithValue("total", CDbl(txttotal.Text))
                .AddWithValue("efectivo", (CDbl(txtpago.Text) + CDbl(txttarjeta.Text)) - CDbl(txtcambio.Text))
                .AddWithValue("tarjeta", CDbl(txttarjeta.Text))
                .AddWithValue("autoriza", txtautori.Text)
                .AddWithValue("detalles", table)

            End With

            openConnection()
            cmd.ExecuteNonQuery()
            closeConnection()
            MsgBox("Se grabo correctamente el registro", MsgBoxStyle.Information, "Éxito")
            ImprimeTicket()
        Catch ex As Exception
            MessageBox.Show("Hubo un error al grabar la venta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If MessageBox.Show("¿Desea cancelar esta acción?", "Cancelar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Me.Close()
            frmPuntoDeVentaMejorado.Select()

        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Val(txtcambio.Text) >= 0 Then
            If MessageBox.Show("¿Desea guardar esta venta?", "Guardando", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                'guardarVenta2()
                If saveClient = True Then
                    saveinfoclient()
                    saveClient = False
                End If
                If Me.CheckBox1.Checked = True Then
                    'Guardar detalles
                    'guardarDetalleVenta2()
                    'Actualizar la venta
                    'actualizarVenta()
                    GrabaVenta(table)
                    guardarFactura()
                    'aca deberia de mostrar la factura
                Else
                    'Guardar detalles
                    'guardarDetalleVenta2()
                    'Actualizar la venta
                    'actualizarVenta()
                    GrabaVenta(table)


                End If

                If pv = 1 Then
                    frmPuntoDeVentaMejorado.cleanAll()
                    Me.Close()

                End If

            End If
        Else
            MessageBox.Show("Datos invalidos", "Datos no válidos", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub txtpago_TextChanged(sender As Object, e As EventArgs) Handles txtpago.TextChanged
        txtcambio.Text = String.Format("{0:N2}", (Val(txtpago.Text) + Val(txttarjeta.Text)) - Val(txttotal.Text))
        If (Val(txtpago.Text) + Val(txttarjeta.Text)) >= Val(txttotal.Text) Then
            txtcambio.BackColor = Color.Blue
        Else
            txtcambio.BackColor = Color.Red
        End If
    End Sub

    Private Sub frmCobrar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtpago.Text = 0.0
        txttarjeta.Text = 0.0

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

    Private Sub txttotal_TextChanged(sender As Object, e As EventArgs) Handles txttotal.TextChanged
        txtcambio.Text = String.Format("{0:N2}", (Val(txtpago.Text) - Val(txttotal.Text)))
    End Sub

    Private Sub txtcambio_TextChanged(sender As Object, e As EventArgs) Handles txtcambio.TextChanged
        If Val(txtpago.Text) >= Val(txttotal.Text) Then
            txtcambio.BackColor = Color.Blue
        Else
            txtcambio.BackColor = Color.Red
        End If
    End Sub

    Private Sub txttarjeta_TextChanged(sender As Object, e As EventArgs) Handles txttarjeta.TextChanged
        txtcambio.Text = (Val(txtpago.Text) + Val(txttarjeta.Text)) - Val(txttotal.Text)
        If (Val(txtpago.Text) + Val(txttarjeta.Text)) >= Val(txttotal.Text) Then
            txtcambio.BackColor = Color.Blue
        Else
            txtcambio.BackColor = Color.Red
        End If
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

    'Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
    '    Dim linesperpage As Single = 0
    '    Dim yPos As Single = 0
    '    Dim count As Integer = 0
    '    Dim fuente1 As New Font("Arial", 10)
    '    Dim fuente2 As New Font("Arial", 11, FontStyle.Bold)

    '    Dim leftMargin As Single = 1
    '    Dim topMargin As Single = 10
    '    Dim line As String = Nothing

    '    linesperpage = e.MarginBounds.Height / fuente1.GetHeight(e.Graphics)

    '    e.Graphics.DrawString("FarmaciAhorro", fuente2, Brushes.Black, leftMargin, 10)
    '    e.Graphics.DrawString("******************************************", fuente2, Brushes.Black, leftMargin, 28)
    '    e.Graphics.DrawString("TIKET DE VENTA", fuente2, Brushes.Black, leftMargin, 45)
    '    e.Graphics.DrawString("Fecha: " & txtfecha.Text, fuente2, Brushes.Black, leftMargin, 65)
    '    e.Graphics.DrawString("Ticket No. " & frmPuntoDeVentaMejorado.txtcorrelativo.Text, fuente2, Brushes.Black, leftMargin, 85)
    '    e.Graphics.DrawString("Le atendió: " & nameUsuarioActual, fuente2, Brushes.Black, leftMargin, 105)


    '    count = frmPuntoDeVentaMejorado.DataGridView1.Rows.Count - 1
    '    Dim ini As Integer = 180
    '    Dim arti As String
    '    Dim cant As Integer
    '    Dim precio As Double
    '    Dim subt As Double

    '    e.Graphics.DrawString("---------------------------------------------------------", fuente2, Brushes.Black, leftMargin, 120)
    '    e.Graphics.DrawString("Artículo", fuente2, Brushes.Black, leftMargin, 155)
    '    e.Graphics.DrawString("Cant", fuente2, Brushes.Black, 120, 155)
    '    e.Graphics.DrawString("Precio", fuente2, Brushes.Black, 170, 155)
    '    e.Graphics.DrawString("Subt.", fuente2, Brushes.Black, 235, 155)
    '    For i = 0 To count Step 1

    '        arti = frmPuntoDeVentaMejorado.DataGridView1.Rows(i).Cells(1).Value.ToString
    '        cant = frmPuntoDeVentaMejorado.DataGridView1.Rows(i).Cells(2).Value.ToString
    '        precio = frmPuntoDeVentaMejorado.DataGridView1.Rows(i).Cells(3).Value.ToString
    '        subt = frmPuntoDeVentaMejorado.DataGridView1.Rows(i).Cells(4).Value.ToString

    '        If arti.Length >= 15 Then
    '            e.Graphics.DrawString(arti.Substring(0, 15), fuente1, Brushes.Black, leftMargin, ini)
    '        Else
    '            e.Graphics.DrawString(arti, fuente1, Brushes.Black, leftMargin, ini)
    '        End If
    '        e.Graphics.DrawString(cant, fuente1, Brushes.Black, 125, ini)
    '        e.Graphics.DrawString(precio, fuente1, Brushes.Black, 175, ini)
    '        e.Graphics.DrawString(subt, fuente1, Brushes.Black, 240, ini)
    '        ini += 25

    '        e.HasMorePages = True
    '    Next

    '    Dim linea As StringBuilder = New StringBuilder()
    '    Dim streamtoprint As StreamReader

    '    linea.AppendLine("---------------------------------------------------------")
    '    linea.AppendLine(" ")

    '    linea.AppendLine("Total: Q" & String.Format("{0:2}", txttotal.Text))
    '    linea.AppendLine("Efectivo: Q" & txtpago.Text)
    '    linea.AppendLine("Cambio: Q" & txtcambio.Text)

    '    linea.AppendLine(" ")

    '    linea.AppendLine("***************************************************")
    '    linea.AppendLine("*             ¡Gracias por preferirnos            *")
    '    linea.AppendLine("***************************************************")

    '    File.WriteAllText("Factura.txt", linea.ToString())

    '    linea = New StringBuilder()

    '    streamtoprint = New StreamReader("Factura.txt")

    '    Dim pie As String = streamtoprint.ReadLine

    '    Dim sigpie As Integer = ini + 20
    '    While pie <> Nothing
    '        e.Graphics.DrawString(pie, fuente1, Brushes.Black, leftMargin, sigpie, New StringFormat())

    '        pie = streamtoprint.ReadLine
    '        sigpie += 25
    '    End While


    '    If pie <> Nothing Then
    '        e.HasMorePages = True
    '    Else
    '        e.HasMorePages = False
    '    End If
    'End Sub

    Private Sub txtpago_KeyDown(sender As Object, e As KeyEventArgs) Handles txtpago.KeyDown
        If e.KeyCode = Keys.Enter Then
            Button1.PerformClick()
        End If
    End Sub
End Class