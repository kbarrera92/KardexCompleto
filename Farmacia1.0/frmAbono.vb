Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Drawing.Printing

Public Class frmAbono

    Sub imprimirRecibo()
        'Dim pd As New PrintDocument()
        'AddHandler pd.PrintPage, New PrintPageEventHandler(AddressOf Me.PrintDocument_PrintPage)
        'Imprimir en tamaño de papel A5
        Dim name As String = "A5"
        Dim width As Integer = 830  'Ancho en Centesimas de pulgada
        Dim height As Integer = 580 'Alto en Centesimas de pulgada
        Dim TipoPapel As New PaperSize(name, width, height)
        PrintDocument1.DefaultPageSettings.PaperSize = TipoPapel
        '
        Dim ppd As New PrintPreviewDialog()
        ppd.Document = PrintDocument1
        ppd.ShowDialog()
    End Sub

    Private Sub frmAbono_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MaskedTextBox1.Text = Format(DateTime.Now, "dd/MM/yyyy")
        If frmCuentasxP.DataGridView1.Rows.Count = 0 Then
            txtNoAbono.Text = 1
        Else
            txtNoAbono.Text = frmCuentasxP.DataGridView1.Rows(frmCuentasxP.DataGridView1.Rows.Count - 1).Cells(0).Value + 1
        End If
        txtNoCuenta.Text = frmCuentasxP.txtNoCuenta.Text
        txtSaldo.Text = frmCuentasxP.txtSaldo.Text
        txtImporte.Select()
    End Sub

    Sub updateSaldo()
        Dim sqlUpSaldo As String = "UPDATE CUENTAXPAGAR SET saldoCuenta = saldoCuenta - @imp WHERE idCuenta = @id"
        Dim comand As SqlCommand
        comand = New SqlCommand(sqlUpSaldo, conn)
        comand.Parameters.AddWithValue("imp", Val(txtImporte.Text))
        comand.Parameters.AddWithValue("id", Val(txtNoCuenta.Text))

        Try
            openConnection()
            comand.ExecuteNonQuery()
            'MsgBox("Cuenta actualizada correctamente", MsgBoxStyle.Information, "Éxito")
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            closeConnection()
        End Try
    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
       
        If Val(txtImporte.Text) > Val(txtSaldo.Text) Then
            MsgBox("El abono sobrepasa el saldo de la cuenta")
            txtImporte.Select()
        ElseIf frmCuentasxP.txtEstado.Text = "SOLVENTE" Then
            MsgBox("La cuenta ya ha sido cancelada")
        Else
            If MessageBox.Show("¿Imprimir recibo?", "Recibo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                If MessageBox.Show("¿Desea realizar el abono?", "Abono", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    Dim sql As String = "INSERT INTO DETALLECXP VALUES (@idD, @idC, @fecha, @imp, @recibo);"
                    Dim comand As SqlCommand
                    comand = New SqlCommand(sql, conn)

                    comand.Parameters.AddWithValue("idD", CInt(txtNoAbono.Text))
                    comand.Parameters.AddWithValue("idC", CInt(txtNoCuenta.Text))
                    comand.Parameters.AddWithValue("fecha", Convert.ToDateTime(MaskedTextBox1.Text))
                    comand.Parameters.AddWithValue("imp", CDbl(txtImporte.Text))
                    comand.Parameters.AddWithValue("recibo", If(Trim(txtrecibo.Text) = "", DBNull.Value, CInt(txtrecibo.Text)))

                    Try
                        openConnection()
                        comand.ExecuteNonQuery()
                        MessageBox.Show("Abono realizado con éxito", "Realizado", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    Catch ex As Exception
                        MsgBox(ex.Message)
                    Finally
                        closeConnection()
                        updateSaldo()
                        fillDGVSPDetCxP("sp_verDetallesCxP", frmCuentasxP.DataGridView1, frmCuentasxP, CInt(txtNoCuenta.Text))
                        'llenarDGV(frmCuentasxP.DataGridView1, frmCuentasxP, "SELECT * FROM ABONOCXP WHERE idCuenta = " & noCuentaXP)
                        calcularSaldo()
                        imprimirRecibo()
                        Me.Close()
                    End Try
                End If
            Else
                If MessageBox.Show("¿Desea realizar el abono?", "Abono", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    Dim sql As String = "INSERT INTO DETALLECXP VALUES (@idD, @idC, @fecha, @imp);"
                    Dim comand As SqlCommand
                    comand = New SqlCommand(sql, conn)

                    comand.Parameters.AddWithValue("idD", CInt(txtNoAbono.Text))
                    comand.Parameters.AddWithValue("idC", CInt(txtNoCuenta.Text))
                    comand.Parameters.AddWithValue("fecha", Convert.ToDateTime(MaskedTextBox1.Text))
                    comand.Parameters.AddWithValue("imp", CDbl(txtImporte.Text))

                    Try
                        openConnection()
                        comand.ExecuteNonQuery()
                        MessageBox.Show("Abono realizado con éxito", "Realizado", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    Catch ex As Exception
                        MsgBox(ex.Message)
                    Finally
                        closeConnection()
                        updateSaldo()
                        fillDGVSPDetCxP("sp_verDetallesCxP", frmCuentasxP.DataGridView1, frmCuentasxP, CInt(txtNoCuenta.Text))
                        'llenarDGV(frmCuentasxP.DataGridView1, frmCuentasxP, "SELECT * FROM ABONOCXP WHERE idCuenta = " & noCuentaXP)
                        calcularSaldo()
                        Me.Close()
                    End Try
                End If
            End If
            
        End If
        
    End Sub

  
    
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Dim f1 As New Font("Arial Black", 20, FontStyle.Bold)
        Dim f2 As New Font("Arial", 16, FontStyle.Regular)
        Dim pen As New Pen(Brushes.Black, 2)
        Dim rect As New Rectangle(555, 480, 240, 80)

        e.Graphics.DrawString("Recibo No _____" & txtNoAbono.Text & "______", f1, Brushes.Black, 40, 30)
        e.Graphics.DrawString("Fecha: " & DateTime.Now.ToShortDateString, f1, Brushes.Black, 520, 30)

        e.Graphics.DrawString("Lugar: San Francisco Zapotitlán, Suchitepéquez", f2, Brushes.Black, 40, 100)
        e.Graphics.DrawString("Por concepto de: Abono de Cuenta por Pagar No. " & txtNoCuenta.Text, f2, Brushes.Black, 40, 150)
        e.Graphics.DrawString("_________________________________________________________", f2, Brushes.Black, 40, 180)

        e.Graphics.DrawString("Recibí de: Edwin Maldonado", f2, Brushes.Black, 40, 240)
        e.Graphics.DrawString("La cantidad de: Q" & FormatNumber(txtImporte.Text, 2), f2, Brushes.Black, 40, 290)

        e.Graphics.DrawString("_____________________________", f2, Brushes.Black, 40, 380)
        e.Graphics.DrawString("Recibí conforme", f2, Brushes.Black, 40, 410)
        e.Graphics.DrawString("DPI: _________________________", f2, Brushes.Black, 40, 440)

        e.Graphics.DrawString("Saldo:", f2, Brushes.Blue, 550, 450)
        e.Graphics.DrawRectangle(pen, rect)
    End Sub
End Class