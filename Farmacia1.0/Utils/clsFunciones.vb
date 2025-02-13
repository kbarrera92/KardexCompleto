Imports System.Drawing.Printing
Imports System.IO
Imports System.Text

Public Class clsFunciones
    Public Shared printFont As Font
    Public Shared streamtoprint As StreamReader

    Public Class crearTicket
        Public line As StringBuilder = New StringBuilder()
        Dim ticket As String = ""
        Dim parte1, parte2 As String
        Public max As Integer = 40
        Dim cort As Integer


        'Public Serverbyte As Byte()

        Public Function lineasGuion() As String
            Dim lineaGuion As String = "----------------------------------------"
            Return line.AppendLine(lineaGuion).ToString()
        End Function

        Public Sub EncabezadoVenta()
            Dim lineaEncabezado As String = "Artículo------------Cant--P.Unit--Valor"
            line.AppendLine(lineaEncabezado).ToString()
        End Sub

        Public Sub textoIzquierda(ByVal par1 As String)
            max = par1.Length
            If max > 40 Then
                cort = max - 40
                parte1 = par1.Remove(40, cort)
            Else
                parte1 = par1
            End If
            ticket = parte1
            line.AppendLine(ticket)
        End Sub

        Public Sub textoDerecha(ByVal par1 As String)
            ticket = ""
            max = par1.Length
            If max > 40 Then
                cort = max - 40
                parte1 = par1.Remove(40, cort)
            Else
                parte1 = par1
            End If
            max = 40 - par1.Length
            For i = 0 To max - 1
                ticket += " "
            Next
            line.AppendLine(ticket = ticket & parte1)
        End Sub

        Public Sub textoCentro(ByVal par1 As String)
            ticket = ""
            max = par1.Length
            If max > 40 Then
                cort = max - 40
                parte1 = par1.Remove(40, cort)
            Else
                parte1 = par1
            End If
            max = Math.Truncate((40 - par1.Length) / 2)
            For i = 0 To max - 1
                ticket += " "
            Next
            ticket += parte1
            line.AppendLine(ticket)
        End Sub

        Public Sub textoExtremos(ByVal par1 As String, ByVal par2 As String)

            max = par1.Length
            If max > 18 Then
                cort = max - 18
                parte1 = par1.Remove(18, cort)
            Else
                parte1 = par1
            End If
            ticket = parte1
            max = par2.Length
            If max > 18 Then
                cort = max - 18
                parte2 = par2.Remove(18, cort)
            Else
                parte2 = par2
            End If
            max = 40 - (parte1.Length + parte2.Length)
            For i = 0 To max - 1
                ticket += " "
            Next
            line.AppendLine(ticket = ticket & parte2)
        End Sub

        Public Sub agregaTotales(ByVal par1 As String, ByVal total As Double)
            max = par1.Length
            If max > 25 Then
                cort = max + 25
                parte1 = par1.Remove(25, cort)
            Else
                parte1 = par1
            End If
            ticket = parte1
            parte2 = "Q" & String.Format("{0:N2}", total)
            max = 25 - (parte1.Length - parte2.Length)
            For i = 0 To max - 1
                ticket &= " "
            Next
            ticket = ticket & parte2
            line.AppendLine(ticket)

        End Sub

        Public Sub agregarArticulo(ByVal articulo As String, ByVal precio As Double, ByVal cant As Integer, ByVal subt As Double)
            If cant.ToString().Length <= 3 And precio.ToString("C").Length <= 10 And subt.ToString("C").Length <= 11 Then
                Dim elementos As String = ""
                Dim espacios As String = ""
                Dim bandera As Boolean = False
                Dim nroespacios As Integer = 0

                If articulo.Length > 40 Then
                    'cort = max - 16
                    'parte1 = articulo.Remove(16, cort)
                    nroespacios = (3 - cant.ToString().Length)
                    espacios = ""
                    For i = 0 To nroespacios - 1
                        espacios = espacios & " "

                    Next
                    elementos &= espacios & cant.ToString()

                    nroespacios = (20 - precio.ToString().Length)
                    espacios = ""

                    For i = 0 To nroespacios - 1
                        espacios &= " "
                    Next
                    elementos &= espacios & precio.ToString

                    nroespacios = (11 - subt.ToString().Length)
                    espacios = ""

                    For i = 0 To nroespacios - 1 Step 1
                        espacios &= " "
                    Next
                    elementos &= espacios & subt.ToString()

                    Dim caracterActual As Integer = 0
                    For longtext = articulo.Length To 16 + 1
                        If bandera = False Then
                            line.AppendLine(articulo.Substring(caracterActual, 16) + elementos)
                            bandera = True
                        Else
                            line.AppendLine(articulo.Substring(caracterActual, 16))
                        End If
                        caracterActual += 16
                    Next
                    line.AppendLine(articulo.Substring(caracterActual, articulo.Length - caracterActual))

                Else
                    For i = 0 To (19 - articulo.Length)
                        espacios &= " "

                    Next
                    elementos = articulo & espacios
                    nroespacios = (3 - cant.ToString().Length)
                    espacios = ""

                    For i = 0 To nroespacios - 1
                        espacios &= " "
                    Next

                    elementos &= espacios & cant.ToString()

                    nroespacios = (8 - precio.ToString().Length)
                    espacios = ""

                    For i = 0 To nroespacios - 1
                        espacios &= " "
                    Next
                    elementos &= espacios & precio.ToString()

                    nroespacios = (7 - subt.ToString().Length)
                    espacios = ""

                    For i = 0 To nroespacios - 1
                        espacios &= " "
                    Next
                    elementos &= espacios & subt.ToString()
                    line.AppendLine(elementos)
                End If
            Else

            End If
        End Sub

        Public Sub imprimirTicket(ByVal impresora As String)
            File.WriteAllText("Factura.txt", line.ToString())

            line = New StringBuilder()

            Try

                streamtoprint = New StreamReader("Factura.txt")

                Try
                    printFont = New Font("Lucida Sans Typewriter", 8)
                    Dim pd As PrintDocument = New PrintDocument()

                    AddHandler pd.PrintPage, AddressOf Me.pd_PrintPage

                    pd.PrinterSettings.PrinterName = impresora
                    pd.DocumentName = "Ticket" & DateTime.Now.ToShortDateString()

                    pd.Print()
                Catch ex As Exception

                Finally
                    streamtoprint.Close()
                End Try
            Catch ex As Exception
                Throw
            End Try
        End Sub

        Public Sub pd_PrintPage(ByVal sender As Object, ByVal ev As PrintPageEventArgs)
            Dim linesperpage As Single = 0
            Dim yPos As Single = 0
            Dim count As Integer = 0

            Dim leftMargin As Single = 10
            Dim topMargin As Single = 10
            Dim line As String = Nothing

            linesperpage = ev.MarginBounds.Height / printFont.GetHeight(ev.Graphics)

            line = streamtoprint.ReadLine
            While count < linesperpage And line <> Nothing
                yPos = topMargin + (count * printFont.GetHeight(ev.Graphics))
                ev.Graphics.DrawString(line, printFont, Brushes.Black, leftMargin, yPos, New StringFormat())
                count += 1
                line = streamtoprint.ReadLine
            End While

            If line <> Nothing Then
                ev.HasMorePages = True
            Else
                ev.HasMorePages = False
            End If
        End Sub
    End Class



End Class
