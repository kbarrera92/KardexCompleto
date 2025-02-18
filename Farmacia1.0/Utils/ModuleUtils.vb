Imports System.Collections.Specialized
Imports System.Data.SqlClient

Module ModuleUtils

    Public grabaBitacoraSp As String = "sp_grabaBitacora"
    Function ConsultaParametro(ByVal param As String) As String
        Dim retValue As String = String.Empty

        Try
            Dim appSetting As NameValueCollection = Configuration.ConfigurationManager.AppSettings
            retValue = If(appSetting(param), String.Empty)
        Catch ex As Exception
            retValue = String.Empty
        End Try

        Return retValue
    End Function

    Public Sub GrabaBitacora(ByVal params As String(), ByVal sp As String)
        Dim cmd As SqlCommand
        Try
            cmd = New SqlCommand()
            With cmd
                .CommandText = "sp_grabaBitacora"
                .CommandType = CommandType.StoredProcedure
                .Connection = conn
            End With

            cmd.Parameters.AddWithValue("NICK", params(0))
            cmd.Parameters.AddWithValue("ESTACION", params(1))
            cmd.Parameters.AddWithValue("ACCION", params(2))

            openConnection()
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show("No se guardo el cliente" & vbCrLf & "Error: " & ex.Message, "Algo salio mal", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            closeConnection()
        End Try
    End Sub

    Function RegresaArray(ByVal cadena As String) As String()
        Return Split(cadena, ";")
    End Function

    Public Function EjecutarStoredProcedureMultiple(ByVal conexionString As String,
                                               ByVal nombreSP As String,
                                               ByVal parametros As List(Of SqlParameter)) As (List(Of DataTable), String, List(Of SqlParameter))
        Dim dataTables As New List(Of DataTable)()  ' Lista para almacenar los DataTables resultantes
        Dim salida As String = String.Empty  ' Variable para el mensaje de salida
        Dim parametrosSalida As New List(Of SqlParameter)()  ' Lista para los parámetros de salida

        ' Usamos una conexión a la base de datos SQL Server
        Using conexion As New SqlConnection(conexionString)
            ' Definimos el comando para ejecutar el SP
            Using cmd As New SqlCommand(nombreSP, conexion)
                cmd.CommandType = CommandType.StoredProcedure

                ' Agregar los parámetros a la colección del comando
                If parametros IsNot Nothing Then
                    For Each parametro As SqlParameter In parametros
                        cmd.Parameters.Add(parametro)
                    Next
                End If

                ' Intentamos abrir la conexión y ejecutar el SP
                Try
                    conexion.Open()

                    ' Ejecutar el stored procedure y leer los resultados
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        ' Mientras haya más conjuntos de resultados
                        Do
                            ' Crear un DataTable para almacenar el conjunto actual de resultados
                            Dim dt As New DataTable()
                            dt.Load(reader)  ' Cargar los resultados en el DataTable
                            dataTables.Add(dt)  ' Añadir el DataTable a la lista
                        Loop While reader.NextResult() ' Continuar leyendo si hay más resultados

                    End Using

                    ' Extraer los parámetros de salida (si existen)
                    For Each param As SqlParameter In cmd.Parameters
                        If param.Direction = ParameterDirection.Output OrElse param.Direction = ParameterDirection.InputOutput Then
                            parametrosSalida.Add(param)  ' Guardamos los parámetros de salida
                        End If
                    Next

                    ' Verificar si hay parámetros de salida y construir un mensaje
                    If parametrosSalida.Count > 0 Then
                        salida = "Parámetros de salida: "
                        For Each param As SqlParameter In parametrosSalida
                            salida &= param.ParameterName & " = " & param.Value.ToString() & ", "
                        Next
                        salida = salida.TrimEnd(","c, " "c)
                    Else
                        salida = "Sin parámetros de salida."
                    End If

                Catch ex As Exception
                    ' Manejo de excepciones en caso de error al ejecutar el SP
                    salida = "Error: " & ex.Message
                Finally
                    conexion.Close()  ' Cerrar la conexión
                End Try
            End Using
        End Using

        ' Devolver la lista de DataTables, la respuesta (mensaje de salida), y los parámetros de salida
        Return (dataTables, salida, parametrosSalida)
    End Function


    Public Sub ImprimeTicket(ByVal nventa As Integer)
        If ConsultaParametro("imprimeTicket") = "S" Then
            Dim fechaActual As Date = Date.Now
            Dim formato As String = "yyyy-MM-dd HH:mm:ss"
            Dim fechaFormateada As String = fechaActual.ToString(formato)
            Dim ticket As clsFunciones.crearTicket = New clsFunciones.crearTicket()

            ticket.textoCentro(ConsultaParametro("nombreEmpresa"))
            ticket.textoCentro("****************************************")
            ticket.textoCentro("TICKET DE VENTA")
            If pv = 1 Then
                ticket.textoIzquierda("No. de Ticket: " & nventa.ToString())

                ticket.textoIzquierda("Fecha: " & fechaFormateada)
                ticket.textoIzquierda("Le atendió: " & nameUsuarioActual)
                ticket.textoIzquierda(" ")
                ticket.lineasGuion()

                ticket.EncabezadoVenta()
                ticket.lineasGuion()

                For i = 0 To frmPuntoDeVentaMejorado.DataGridView1.Rows.Count - 1
                    Dim arti As String = String.Concat(frmPuntoDeVentaMejorado.DataGridView1.Rows(i).Cells(0).Value.ToString(), "-", frmPuntoDeVentaMejorado.DataGridView1.Rows(i).Cells(1).Value.ToString())
                    If arti.Length < 15 Then
                        Dim spaces As String = ""
                        For j = arti.Length To 15
                            spaces &= "-"
                        Next
                        arti &= spaces
                    End If
                    ticket.agregarArticulo(arti.Substring(0, 15), Double.Parse(String.Format("{0:N2}", frmPuntoDeVentaMejorado.DataGridView1.Rows(i).Cells(3).Value)), Integer.Parse(frmPuntoDeVentaMejorado.DataGridView1.Rows(i).Cells(2).Value.ToString()), Double.Parse(String.Format("{0:N2}", frmPuntoDeVentaMejorado.DataGridView1.Rows(i).Cells(4).Value)))

                Next
            End If

            ticket.lineasGuion()
            ticket.agregaTotales("Total: ", Double.Parse(frmCobrar.txttotal.Text))
            ticket.textoIzquierda(" ")
            ticket.agregaTotales("Efectivo: ", Double.Parse(frmCobrar.txtpago.Text))
            ticket.agregaTotales("Cambio: ", Double.Parse(frmCobrar.txtcambio.Text))
            ticket.textoIzquierda(" ")

            ticket.textoIzquierda(" ")
            ticket.textoCentro("**********************************")
            ticket.textoCentro("*     Gracias por preferirnos    *")
            ticket.textoCentro("**********************************")
            ticket.textoIzquierda(" ")

            ticket.imprimirTicket(ConsultaParametro("nombreImpresora"))
        End If
    End Sub

    Public Sub ImprimeTicketCuadre(ByVal inicial As Decimal, ByVal sistema As Decimal, ByVal fisico As Decimal, ByVal diferencia As Decimal)
        If ConsultaParametro("imprimeTicketCuadre") = "S" Then
            Dim fechaActual As Date = Date.Now
            Dim formato As String = "yyyy-MM-dd HH:mm:ss"
            Dim fechaFormateada As String = fechaActual.ToString(formato)
            Dim ticket As clsFunciones.crearTicket = New clsFunciones.crearTicket()

            ticket.textoCentro(ConsultaParametro("nombreEmpresa"))
            ticket.textoCentro("****************************************")
            ticket.textoCentro("TICKET DE CUADRE")
            If pv = 1 Then
                ticket.textoIzquierda("Fecha: " & fechaFormateada)
                ticket.textoIzquierda("Usuario: " & nameUsuarioActual)
                ticket.textoIzquierda(" ")
                ticket.lineasGuion()
                ticket.textoIzquierda(" ")
            End If

            ticket.agregaTotales("Inicial: ", inicial)
            ticket.textoIzquierda(" ")
            ticket.agregaTotales("Sistema: ", sistema)
            ticket.textoIzquierda(" ")
            ticket.agregaTotales("Físico: ", fisico)
            ticket.textoIzquierda(" ")
            ticket.agregaTotales("Diferencia: ", diferencia)

            ticket.textoIzquierda(" ")
            ticket.textoCentro("**********************************")
            ticket.textoCentro("*            Farmavela           *")
            ticket.textoCentro("**********************************")
            ticket.textoIzquierda(" ")

            ticket.imprimirTicket(ConsultaParametro("nombreImpresora"))
        End If
    End Sub

End Module
