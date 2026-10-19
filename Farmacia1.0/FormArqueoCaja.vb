Imports System.Data.SqlClient
Imports System.Globalization

''' <summary>
''' Arqueo de caja del último turno cerrado de la sucursal: conteo de monedas/billetes por denominación (izquierda)
''' contra el efectivo que debería haber según el sistema (derecha). Se guarda en ARQUEOCAJA y se imprime.
''' </summary>
Public Class FormArqueoCaja

    Private Shared ReadOnly Denominaciones As Decimal() = {0.25D, 0.5D, 1D, 5D, 10D, 20D, 50D, 100D, 200D}

    Private idTurno As Integer
    Private vendedor As String = ""
    Private sobrante As Decimal, inicial As Decimal, venta As Decimal, tarjeta As Decimal, helados As Decimal, bebidas As Decimal, egresos As Decimal
    Private cargando As Boolean = True

    ''' <summary>Efectivo de la farmacia a entregar: sin el fondo inicial para cambio ni el efectivo de helados y bebidas.</summary>
    Private ReadOnly Property TotalEfectivo As Decimal
        Get
            Return sobrante + inicial + venta - tarjeta - helados - bebidas - egresos
        End Get
    End Property

    ''' <summary>Lo que debe haber al contar la caja: el conteo incluye el fondo inicial y el efectivo de helados y bebidas.</summary>
    Private ReadOnly Property EfectivoEnCaja As Decimal
        Get
            Return TotalEfectivo + helados + bebidas
        End Get
    End Property

    Private Sub FormArqueoCaja_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Estilos.AplicarEstilos(Me)

        For Each den In Denominaciones
            dgvConteo.Rows.Add(den, 0, 0D)
        Next

        If Not CargaTurno() Then
            'Cerrar dentro de Load, con el formulario aún en Show, lanza ObjectDisposedException
            BeginInvoke(New Action(AddressOf Close))
            Return
        End If

        cargando = False
        Recalcula()
    End Sub

    Private Shared Function SucursalLocal() As Integer
        Dim sucursal As Integer
        If Not Integer.TryParse(ConsultaParametro("codigoSucursal"), sucursal) Then sucursal = sucActual
        Return sucursal
    End Function

    ''' <summary>Carga el último turno cerrado de la sucursal y, si ya tiene arqueo, sus cantidades. False si no se pudo.</summary>
    Private Function CargaTurno() As Boolean
        Try
            Using cmd As New SqlCommand("sp_mantArqueoCaja", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@opt", "U")
                cmd.Parameters.Add("@msg", SqlDbType.VarChar, 200).Direction = ParameterDirection.Output
                cmd.Parameters.Add("@rc", SqlDbType.Int).Direction = ParameterDirection.Output
                cmd.Parameters.AddWithValue("@sucursal", SucursalLocal())

                Dim ds As New DataSet()
                openConnection()
                Using adapter As New SqlDataAdapter(cmd)
                    adapter.Fill(ds)
                End Using

                If CInt(cmd.Parameters("@rc").Value) <> 0 OrElse ds.Tables.Count = 0 OrElse ds.Tables(0).Rows.Count = 0 Then
                    MessageBox.Show(cmd.Parameters("@msg").Value.ToString(), "Arqueo de caja", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return False
                End If

                Dim fila As DataRow = ds.Tables(0).Rows(0)
                idTurno = CInt(fila("idTurno"))
                vendedor = fila("vendedor").ToString()
                sobrante = Convert.ToDecimal(fila("sobrante"))
                inicial = Convert.ToDecimal(fila("saldoInicial"))
                venta = Convert.ToDecimal(fila("totalVenta"))
                tarjeta = Convert.ToDecimal(fila("totalTarjeta"))
                helados = Convert.ToDecimal(fila("totalHelados"))
                bebidas = Convert.ToDecimal(fila("totalBebidas"))
                egresos = Convert.ToDecimal(fila("totalEgresos"))
                lblTurno.Text = $"Turno {idTurno} - {vendedor} - cierre {Convert.ToDateTime(fila("fechaCierre")):yyyy-MM-dd HH:mm}"

                If ds.Tables.Count > 1 Then
                    For Each guardado As DataRow In ds.Tables(1).Rows
                        For Each filaGrid As DataGridViewRow In dgvConteo.Rows
                            If Convert.ToDecimal(filaGrid.Cells(colDenominacion.Index).Value) = Convert.ToDecimal(guardado("denominacion")) Then
                                filaGrid.Cells(colCantidad.Index).Value = Convert.ToInt32(guardado("cantidad"))
                            End If
                        Next
                    Next
                End If
            End Using
            Return True
        Catch ex As Exception
            Serilog.Log.Error($"No se pudo cargar el turno para el arqueo. Error: {ex.Message}")
            MessageBox.Show("No se pudo cargar el turno para el arqueo: " & ex.Message, "Algo salió mal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            closeConnection()
        End Try
    End Function

    Private Function CantidadFila(fila As DataGridViewRow) As Integer
        Dim cantidad As Integer
        If Integer.TryParse(Convert.ToString(fila.Cells(colCantidad.Index).Value), cantidad) AndAlso cantidad > 0 Then Return cantidad
        Return 0
    End Function

    Private Function Conteo() As List(Of KeyValuePair(Of Decimal, Integer))
        Return dgvConteo.Rows.Cast(Of DataGridViewRow)().
            Select(Function(f) New KeyValuePair(Of Decimal, Integer)(Convert.ToDecimal(f.Cells(colDenominacion.Index).Value), CantidadFila(f))).
            ToList()
    End Function

    Private Function TotalArqueo() As Decimal
        Return Conteo().Sum(Function(p) p.Key * p.Value)
    End Function

    Private Sub Recalcula()
        If cargando Then Return

        For Each fila As DataGridViewRow In dgvConteo.Rows
            fila.Cells(colSubtotal.Index).Value = Convert.ToDecimal(fila.Cells(colDenominacion.Index).Value) * CantidadFila(fila)
        Next

        Dim total = TotalArqueo()
        Dim dif = total - EfectivoEnCaja
        lblTotalArqueo.Text = total.ToString("N2")
        lblSobrante.Text = sobrante.ToString("N2")
        lblInicial.Text = inicial.ToString("N2")
        lblVenta.Text = venta.ToString("N2")
        lblTarjeta.Text = tarjeta.ToString("N2")
        lblHelados.Text = helados.ToString("N2")
        lblBebidas.Text = bebidas.ToString("N2")
        lblEgresos.Text = egresos.ToString("N2")
        lblTotalEfectivo.Text = TotalEfectivo.ToString("N2")
        lblEfectivoCaja.Text = EfectivoEnCaja.ToString("N2")
        lblDiferencia.Text = dif.ToString("N2")
        lblDiferencia.ForeColor = If(dif < 0, Color.Red, If(dif > 0, Color.DarkOrange, Color.Green))
    End Sub

    Private Sub dgvConteo_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvConteo.CellEndEdit
        If e.ColumnIndex <> colCantidad.Index Then Return

        'Entrada no válida (vacía, texto, negativa) vuelve a 0 en lugar de arrastrar un valor que no se cuenta
        Dim fila = dgvConteo.Rows(e.RowIndex)
        fila.Cells(colCantidad.Index).Value = CantidadFila(fila)
        Recalcula()
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        dgvConteo.EndEdit()
        Recalcula()

        If MessageBox.Show("¿Desea guardar el arqueo?", "Arqueo de caja", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
            Return
        End If

        Dim detalle = String.Join(";", Conteo().Select(Function(p) p.Key.ToString("0.##", CultureInfo.InvariantCulture) & ":" & p.Value))

        Try
            Using cmd As New SqlCommand("sp_mantArqueoCaja", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@opt", "G")
                cmd.Parameters.Add("@msg", SqlDbType.VarChar, 200).Direction = ParameterDirection.Output
                cmd.Parameters.Add("@rc", SqlDbType.Int).Direction = ParameterDirection.Output
                cmd.Parameters.AddWithValue("@sucursal", SucursalLocal())
                cmd.Parameters.AddWithValue("@idTurno", idTurno)
                cmd.Parameters.AddWithValue("@usuario", usuarioActual)
                cmd.Parameters.AddWithValue("@detalle", detalle)

                openConnection()
                cmd.ExecuteNonQuery()

                If CInt(cmd.Parameters("@rc").Value) <> 0 Then
                    MessageBox.Show(cmd.Parameters("@msg").Value.ToString(), "No se guardó el arqueo", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If
            End Using
        Catch ex As Exception
            Serilog.Log.Error($"No se pudo guardar el arqueo del turno {idTurno}. Error: {ex.Message}")
            MessageBox.Show("No se guardó el arqueo: " & ex.Message, "Algo salió mal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        Finally
            closeConnection()
        End Try

        Dim total = TotalArqueo()
        ImprimeTicketArqueo(idTurno, vendedor, Conteo(), total, sobrante, inicial, venta, tarjeta, helados, bebidas, egresos, TotalEfectivo, total - EfectivoEnCaja)
        MessageBox.Show("El arqueo se guardó correctamente", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        Close()
    End Sub
End Class
