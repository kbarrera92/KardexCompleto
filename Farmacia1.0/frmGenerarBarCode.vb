Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Drawing.Drawing2D

Imports iTextSharp.text.pdf
Imports iTextSharp.text.pdf.BarcodeCodabar
Imports System.IO
Imports CrystalDecisions.Shared


Public Class frmGenerarBarCode

    Dim sql As String = "SELECT P.idProducto, p.dProducto," _
                        & "ISNULL(PR.rzProveedor, ''), ISNULL(p.presentacion, ''), ISNULL(P.medida, ''), ISNULL(p.barcode, '') " _
                        & "FROM PRODUCTO P " _
                        & "INNER JOIN PROVEEDOR PR " _
                        & "ON P.proveedor = PR.idProveedor"

    Dim fila As Integer

    Public Shared Function codigo128(ByVal _code As String, Optional ByVal vertexto As Boolean = False, Optional ByVal Height As Single = 0)
        Dim barcode As New Barcode128
        barcode.StartStopText = True
        If Height <> 0 Then
            barcode.BarHeight = Height
        End If
        barcode.Code = _code
        Try
            Dim bm As New System.Drawing.Bitmap(barcode.CreateDrawingImage(Color.Black, Color.White))
            If vertexto = False Then
                Return bm
            Else
                'generando el texto
                Dim bmT As Image
                bmT = New Bitmap(bm.Width, bm.Height + 14)
                Dim g As Graphics = Graphics.FromImage(bmT)
                g.FillRectangle(New SolidBrush(Color.White), 0, 0, bm.Width, bm.Height + 14)

                Dim pintarTexto As New Font("Arial", 8)
                Dim brocha As New SolidBrush(Color.Black)

                Dim stringSize As New SizeF
                stringSize = g.MeasureString(_code, pintarTexto)
                Dim centrox As Single = (bm.Width - stringSize.Width) / 2
                Dim x As Single = centrox
                Dim y As Single = bm.Height

                Dim drawformat As New StringFormat
                drawformat.FormatFlags = StringFormatFlags.NoWrap
                g.DrawImage(bm, 0, 0)

                Dim ncode As String = _code.Substring(1, _code.Length - 2)
                g.DrawString(ncode, pintarTexto, brocha, x, y, drawformat)
                Return bmT

            End If
        Catch ex As Exception
            Throw New Exception("Error al generar el codigo" & ex.ToString)
        End Try
    End Function

    Sub getdatos()
        txtdesc.Text = DataGridView1.Rows(fila).Cells(1).Value
        txtcodpro.Text = DataGridView1.Rows(fila).Cells(0).Value
        txtlab.Text = DataGridView1.Rows(fila).Cells(2).Value
        txtpres.Text = DataGridView1.Rows(fila).Cells(3).Value
        txtmed.Text = DataGridView1.Rows(fila).Cells(4).Value
        txtbarcode.Text = DataGridView1.Rows(fila).Cells(5).Value

        'convertir codigo de barra a imagen
        If Trim(txtbarcode.Text) = "" Then
            PictureBox1.Image = Nothing
        Else
            Try
                Dim alto As Single = 0
                alto = Convert.ToSingle(PictureBox1.Height)

                Dim bm As Bitmap = Nothing
                bm = codigo128(txtbarcode.Text, False, alto)
                If Not IsNothing(bm) Then
                    PictureBox1.Image = bm
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub

    Private Sub frmGenerarBarCode_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        fillDGV(sql, DataGridView1, Me)
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        Try
            fila = DataGridView1.CurrentRow.Index
            getdatos()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.Enter Then
            DataGridView2.Rows.Add(txtcodpro.Text, txtdesc.Text, txtlab.Text, txtpres.Text, txtmed.Text, PictureBox1.Image)
        End If
    End Sub

    
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim dt As New DataTable
        With dt
            .Columns.Add("idProducto")
            .Columns.Add("dProducto")
            .Columns.Add("laboratorio")
            .Columns.Add("presentacion")
            .Columns.Add("medida")
            .Columns.Add("barcode", GetType(Byte()))
        End With

        For Each dr As DataGridViewRow In DataGridView2.Rows

            dt.Rows.Add(dr.Cells(0).Value, dr.Cells(1).Value, dr.Cells(2).Value, dr.Cells(3).Value, dr.Cells(4).Value, ConvertToByteArray(dr.Cells(5).Value))

        Next

        Dim rpt As CrystalDecisions.CrystalReports.Engine.ReportDocument
        rpt = New rptBarCode
        rpt.SetDataSource(dt)
        frmVerReportes.CrystalReportViewer1.ReportSource = rpt
        frmVerReportes.ShowDialog()
    End Sub


    Function ConvertToByteArray(ByVal value As Bitmap) As Byte()
        'Dim bitmapBytes As Byte()
        Try
            Using streamm As New MemoryStream

                value.Save(streamm, Imaging.ImageFormat.Jpeg)
                'bitmapBytes = stream.ToArray
                Return streamm.GetBuffer
                streamm.Close()
            End Using

        Catch ex As NullReferenceException
            MsgBox(ex.Message)
            Return Nothing
        End Try
       
    End Function


    
    Private Sub DataGridView2_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView2.KeyDown
        If e.KeyCode = Keys.Delete Then
            DataGridView2.Rows.RemoveAt(DataGridView2.CurrentRow.Index)
        End If
    End Sub

    Private Sub txtbuscar_TextChanged(sender As Object, e As EventArgs) Handles txtbuscar.TextChanged
        Dim filt As String
        Try
            filt = String.Format("Convert(idProducto, 'System.String') like '%{0}%' Or dProducto like '{0}%'", txtbuscar.Text)
            dv.RowFilter = filt
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        
    End Sub
End Class