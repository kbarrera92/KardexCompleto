Imports System.Data.SqlClient

''' <summary>
''' Consulta de turnos de caja (abiertos y cerrados) con filtros por sucursal, estado y fechas.
''' A la derecha muestra el desglose del cuadre del turno seleccionado; los turnos abiertos muestran montos parciales.
''' </summary>
Public Class FormVerTurnos

    Private Const FormatoDiferencia As String = "+#,##0.00;-#,##0.00;0.00"

    Private cargando As Boolean = True

    Private lblVendedorValor As Label, lblUsuarioValor As Label, lblAperturaValor As Label, lblCierreValor As Label
    Private lblInicialValor As Label, lblVentasValor As Label, lblTarjetaValor As Label, lblEgresosValor As Label
    Private lblEsperadoValor As Label, lblContadoValor As Label, lblDiferenciaValor As Label

    Private Sub FormVerTurnos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ConfigurarColumnas()
            ConfigurarDetalle()
            CargarSucursales()
            cboEstado.SelectedIndex = 0
            dtpDesde.Value = Date.Today.AddDays(-30)
            dtpHasta.Value = Date.Today
            Estilos.AplicarEstilos(Me)
            'Estilos.AplicarEstilos pinta todos los labels de azul; estos van en gris por ser texto secundario
            lblNotaFiltro.ForeColor = Color.DimGray
            lblSinDatos.ForeColor = Color.DimGray
        Catch ex As Exception
            Serilog.Log.Error($"No se pudo iniciar el listado de turnos. Error: {ex.Message}")
        End Try

        cargando = False
        CargarTurnos()
    End Sub

    'Las columnas se crean aquí y no en el .Designer.vb: el diseñador de Visual Studio
    'las borraba al regenerar el archivo.
    Private Sub ConfigurarColumnas()
        dgvTurnos.AutoGenerateColumns = False
        dgvTurnos.Columns.Clear()
        AgregarColumna("colTurno", "Turno", "idTurno", 6, 70, DataGridViewContentAlignment.MiddleCenter)
        AgregarColumna("colEstado", "Estado", "estadoTexto", 9, 80, DataGridViewContentAlignment.MiddleCenter, negrita:=True)
        AgregarColumna("colSucursal", "Sucursal", "nombreSuc", 13, 90)
        AgregarColumna("colVendedor", "Vendedor", "vendedor", 16, 100)
        AgregarColumna("colApertura", "Apertura", "fechaApertura", 14, 115, DataGridViewContentAlignment.MiddleCenter, "dd/MM/yy HH:mm")
        AgregarColumna("colCierre", "Cierre", "fechaCierre", 14, 115, DataGridViewContentAlignment.MiddleCenter, "dd/MM/yy HH:mm", "En curso")
        AgregarColumna("colEsperado", "Esperado", "efectivoEsperado", 10, 80, DataGridViewContentAlignment.MiddleRight, "N2")
        AgregarColumna("colContado", "Contado", "totalFisico", 10, 80, DataGridViewContentAlignment.MiddleRight, "N2", "—")
        AgregarColumna("colDiferencia", "Diferencia", "diferencia", 10, 80, DataGridViewContentAlignment.MiddleRight, FormatoDiferencia, "—", True)
    End Sub

    Private Sub AgregarColumna(ByVal nombre As String, ByVal titulo As String, ByVal propiedad As String, ByVal peso As Single, ByVal minimo As Integer,
                               Optional ByVal alineacion As DataGridViewContentAlignment = DataGridViewContentAlignment.MiddleLeft,
                               Optional ByVal formato As String = "", Optional ByVal nulo As String = "", Optional ByVal negrita As Boolean = False)
        Dim columna As New DataGridViewTextBoxColumn With {
            .Name = nombre,
            .HeaderText = titulo,
            .DataPropertyName = propiedad,
            .ReadOnly = True,
            .FillWeight = peso,
            .MinimumWidth = minimo,
            .SortMode = DataGridViewColumnSortMode.Automatic
        }
        columna.DefaultCellStyle.Alignment = alineacion
        If formato <> "" Then columna.DefaultCellStyle.Format = formato
        If nulo <> "" Then columna.DefaultCellStyle.NullValue = nulo
        If negrita Then columna.DefaultCellStyle.Font = New Font("Segoe UI", 10.0!, FontStyle.Bold)
        dgvTurnos.Columns.Add(columna)
    End Sub

    'Las filas del desglose se crean aquí para no repetir 11 pares de labels en el .Designer.vb
    Private Sub ConfigurarDetalle()
        tlpDetalle.SuspendLayout()
        tlpDetalle.RowStyles.Clear()
        tlpDetalle.RowCount = 0
        tlpDetalle.ColumnStyles.Clear()
        tlpDetalle.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 140.0!))
        tlpDetalle.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0!))

        lblVendedorValor = FilaDetalle("Vendedor")
        lblUsuarioValor = FilaDetalle("Usuario")
        lblAperturaValor = FilaDetalle("Apertura")
        lblCierreValor = FilaDetalle("Cierre")
        SeparadorDetalle()
        lblInicialValor = FilaDetalle("(+) Efectivo inicial")
        lblVentasValor = FilaDetalle("(+) Ventas")
        lblTarjetaValor = FilaDetalle("(-) Venta POS")
        lblEgresosValor = FilaDetalle("(-) Egresos")
        SeparadorDetalle()
        lblEsperadoValor = FilaDetalle("Efectivo esperado", True)
        lblContadoValor = FilaDetalle("Efectivo contado", True)
        lblDiferenciaValor = FilaDetalle("Diferencia", True)
        tlpDetalle.ResumeLayout(True)
    End Sub

    Private Function FilaDetalle(ByVal titulo As String, Optional ByVal negrita As Boolean = False) As Label
        Dim fuente As New Font("Segoe UI", 10.0!, If(negrita, FontStyle.Bold, FontStyle.Regular))
        Dim fila As Integer = tlpDetalle.RowCount
        tlpDetalle.RowCount = fila + 1
        tlpDetalle.RowStyles.Add(New RowStyle(SizeType.AutoSize))

        tlpDetalle.Controls.Add(New Label With {
            .Text = titulo, .AutoSize = True, .Font = fuente, .Anchor = AnchorStyles.Left, .Margin = New Padding(0, 4, 0, 4)
        }, 0, fila)

        Dim valor As New Label With {
            .Text = "", .AutoSize = True, .Font = fuente, .Anchor = AnchorStyles.Right, .TextAlign = ContentAlignment.MiddleRight,
            .MaximumSize = New Size(160, 0), .Margin = New Padding(0, 4, 0, 4)
        }
        tlpDetalle.Controls.Add(valor, 1, fila)
        Return valor
    End Function

    Private Sub SeparadorDetalle()
        Dim fila As Integer = tlpDetalle.RowCount
        tlpDetalle.RowCount = fila + 1
        tlpDetalle.RowStyles.Add(New RowStyle(SizeType.AutoSize))
        Dim linea As New Label With {
            .AutoSize = False, .Height = 1, .BackColor = Color.DarkBlue, .Anchor = AnchorStyles.Left Or AnchorStyles.Right, .Margin = New Padding(0, 6, 0, 6)
        }
        tlpDetalle.Controls.Add(linea, 0, fila)
        tlpDetalle.SetColumnSpan(linea, 2)
    End Sub

    Private Sub CargarSucursales()
        Dim dtSuc As DataTable = updateCm("SELECT idSucursal, nombreSuc FROM SUCURSAL ORDER BY nombreSuc")
        If dtSuc Is Nothing Then Return

        Dim filaTodas As DataRow = dtSuc.NewRow()
        filaTodas("idSucursal") = 0
        filaTodas("nombreSuc") = "-- Todas las sucursales --"
        dtSuc.Rows.InsertAt(filaTodas, 0)

        cboSucursal.DataSource = dtSuc
        cboSucursal.ValueMember = "idSucursal"
        cboSucursal.DisplayMember = "nombreSuc"
        cboSucursal.SelectedIndex = 0
    End Sub

    Private Function FilaActual() As DataRowView
        If dgvTurnos.CurrentRow Is Nothing Then Return Nothing
        Return TryCast(dgvTurnos.CurrentRow.DataBoundItem, DataRowView)
    End Function

    Private Shared Function EsAbierto(ByVal fila As DataRowView) As Boolean
        Return fila("estado").ToString() = "A"
    End Function

    Private Shared Function Monto(ByVal fila As DataRowView, ByVal columna As String) As Decimal
        Return If(IsDBNull(fila(columna)), 0D, Convert.ToDecimal(fila(columna)))
    End Function

    Private Sub CargarTurnos()
        If cargando Then Return

        'Si desde > hasta se invierten, en lugar de mostrar una lista vacía sin explicación
        If dtpDesde.Value.Date > dtpHasta.Value.Date Then
            cargando = True
            Dim desde As Date = dtpDesde.Value
            dtpDesde.Value = dtpHasta.Value
            dtpHasta.Value = desde
            cargando = False
        End If

        Dim seleccionado As Integer = -1
        Dim actual As DataRowView = FilaActual()
        If actual IsNot Nothing Then seleccionado = Convert.ToInt32(actual("idTurno"))

        Cursor = Cursors.WaitCursor
        Try
            Dim dt As New DataTable()
            Using cmd As New SqlCommand("sp_listaTurnosCaja", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@sucursal", If(cboSucursal.SelectedValue Is Nothing, 0, Convert.ToInt32(cboSucursal.SelectedValue)))
                cmd.Parameters.Add("@estado", SqlDbType.Char, 1).Value = EstadoFiltro()
                cmd.Parameters.Add("@desde", SqlDbType.Date).Value = dtpDesde.Value.Date
                cmd.Parameters.Add("@hasta", SqlDbType.Date).Value = dtpHasta.Value.Date

                openConnection()
                Using adapter As New SqlDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
            End Using

            dgvTurnos.DataSource = dt
            lblSinDatos.Visible = (dt.Rows.Count = 0)
            lblTotalTurnos.Text = If(dt.Rows.Count = 1, "1 turno", $"{dt.Rows.Count} turnos")
            ActualizaResumen(dt)
            RestauraSeleccion(seleccionado)
            MostrarDetalle()
        Catch ex As Exception
            Serilog.Log.Error($"No se pudieron cargar los turnos de caja. Error: {ex.Message}")
            MessageBox.Show("No se pudieron cargar los turnos: " & ex.Message, "Algo salió mal", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            closeConnection()
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Function EstadoFiltro() As Object
        Select Case cboEstado.SelectedIndex
            Case 1
                Return "A"
            Case 2
                Return "C"
            Case Else
                Return DBNull.Value
        End Select
    End Function

    'Tras actualizar (F5) se mantiene el turno que el usuario estaba viendo
    Private Sub RestauraSeleccion(ByVal idTurno As Integer)
        If idTurno = -1 Then Return
        For Each fila As DataGridViewRow In dgvTurnos.Rows
            Dim item As DataRowView = TryCast(fila.DataBoundItem, DataRowView)
            If item IsNot Nothing AndAlso Convert.ToInt32(item("idTurno")) = idTurno Then
                dgvTurnos.CurrentCell = fila.Cells(0)
                Return
            End If
        Next
    End Sub

    Private Sub ActualizaResumen(ByVal dt As DataTable)
        Dim abiertos As Integer = 0, cerrados As Integer = 0, faltantes As Integer = 0
        Dim montoFaltantes As Decimal = 0D, diferenciaNeta As Decimal = 0D

        For Each fila As DataRow In dt.Rows
            If fila("estado").ToString() = "A" Then
                abiertos += 1
            Else
                cerrados += 1
                Dim dif As Decimal = If(IsDBNull(fila("diferencia")), 0D, Convert.ToDecimal(fila("diferencia")))
                diferenciaNeta += dif
                If dif < 0 Then
                    faltantes += 1
                    montoFaltantes += dif
                End If
            End If
        Next

        lblAbiertos.Text = abiertos.ToString()
        lblAbiertos.ForeColor = If(abiertos > 0, Color.Green, Color.DarkBlue)
        lblCerrados.Text = cerrados.ToString()
        lblFaltantes.Text = If(faltantes = 0, "0", $"{faltantes}  ·  {montoFaltantes.ToString("N2")}")
        lblFaltantes.ForeColor = If(faltantes > 0, Color.Red, Color.DarkBlue)
        lblDiferenciaNeta.Text = diferenciaNeta.ToString(FormatoDiferencia)
        lblDiferenciaNeta.ForeColor = ColorDiferencia(diferenciaNeta)
    End Sub

    Private Shared Function ColorDiferencia(ByVal dif As Decimal) As Color
        Return If(dif < 0, Color.Red, If(dif > 0, Color.DarkOrange, Color.Green))
    End Function

    Private Sub MostrarDetalle()
        Dim fila As DataRowView = FilaActual()
        btnImprimir.Enabled = (fila IsNot Nothing AndAlso Not EsAbierto(fila))

        If fila Is Nothing Then
            lblDetalleTitulo.Text = "Detalle del turno"
            lblDetalleEstado.Text = "Seleccione un turno"
            lblDetalleEstado.ForeColor = Color.DimGray
            lblDetalleNota.Text = "Seleccione un turno de la lista para ver su desglose."
            lblDetalleNota.ForeColor = Color.DimGray
            For Each valor As Label In {lblVendedorValor, lblUsuarioValor, lblAperturaValor, lblCierreValor, lblInicialValor, lblVentasValor,
                                         lblTarjetaValor, lblEgresosValor, lblEsperadoValor, lblContadoValor, lblDiferenciaValor}
                valor.Text = ""
            Next
            Return
        End If

        Dim abierto As Boolean = EsAbierto(fila)
        lblDetalleTitulo.Text = $"Turno {fila("idTurno")}"
        lblDetalleEstado.Text = $"{fila("estadoTexto")} · {fila("nombreSuc")}"
        lblDetalleEstado.ForeColor = If(abierto, Color.Green, Color.DarkBlue)

        lblVendedorValor.Text = fila("vendedor").ToString()
        lblUsuarioValor.Text = fila("usuario").ToString()
        lblAperturaValor.Text = Convert.ToDateTime(fila("fechaApertura")).ToString("dd/MM/yyyy HH:mm")
        lblCierreValor.Text = If(IsDBNull(fila("fechaCierre")), "En curso", Convert.ToDateTime(fila("fechaCierre")).ToString("dd/MM/yyyy HH:mm"))
        lblInicialValor.Text = Monto(fila, "saldoInicial").ToString("N2")
        lblVentasValor.Text = Monto(fila, "totalVentas").ToString("N2")
        lblTarjetaValor.Text = Monto(fila, "totalTarjeta").ToString("N2")
        lblEgresosValor.Text = Monto(fila, "totalEgresos").ToString("N2")
        lblEsperadoValor.Text = Monto(fila, "efectivoEsperado").ToString("N2")

        If abierto Then
            lblContadoValor.Text = "—"
            lblDiferenciaValor.Text = "—"
            lblDiferenciaValor.ForeColor = Color.DarkBlue
            lblDetalleNota.Text = "Turno en curso. Los montos son parciales y se actualizan con F5; el efectivo contado y la diferencia se calculan al cerrar la caja."
            lblDetalleNota.ForeColor = Color.DimGray
        Else
            Dim dif As Decimal = Monto(fila, "diferencia")
            lblContadoValor.Text = Monto(fila, "totalFisico").ToString("N2")
            lblDiferenciaValor.Text = dif.ToString(FormatoDiferencia)
            lblDiferenciaValor.ForeColor = ColorDiferencia(dif)
            'El texto acompaña al color para que el estado no dependa solo de distinguirlo
            lblDetalleNota.Text = If(dif < 0, "Falta efectivo", If(dif > 0, "Sobra efectivo", "Caja cuadrada"))
            lblDetalleNota.ForeColor = lblDiferenciaValor.ForeColor
        End If
    End Sub

    Private Sub dgvTurnos_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvTurnos.CellFormatting
        If e.RowIndex < 0 Then Return
        Dim fila As DataRowView = TryCast(dgvTurnos.Rows(e.RowIndex).DataBoundItem, DataRowView)
        If fila Is Nothing Then Return

        Dim abierto As Boolean = EsAbierto(fila)
        If abierto Then e.CellStyle.BackColor = Color.Honeydew

        Select Case dgvTurnos.Columns(e.ColumnIndex).DataPropertyName
            Case "estadoTexto"
                e.CellStyle.ForeColor = If(abierto, Color.Green, Color.DimGray)
            Case "diferencia"
                If Not abierto Then e.CellStyle.ForeColor = ColorDiferencia(Monto(fila, "diferencia"))
        End Select
    End Sub

    Private Sub dgvTurnos_SelectionChanged(sender As Object, e As EventArgs) Handles dgvTurnos.SelectionChanged
        If Not cargando Then MostrarDetalle()
    End Sub

    Private Sub Filtro_Changed(sender As Object, e As EventArgs) Handles cboSucursal.SelectedIndexChanged, cboEstado.SelectedIndexChanged, dtpDesde.ValueChanged, dtpHasta.ValueChanged
        CargarTurnos()
    End Sub

    Private Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.Click
        CargarTurnos()
    End Sub

    Private Sub FormVerTurnos_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F5 Then
            e.Handled = True
            CargarTurnos()
        End If
    End Sub

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        Dim fila As DataRowView = FilaActual()
        If fila Is Nothing Then
            MessageBox.Show("Seleccione un turno para imprimir su cuadre", "Imprimir cuadre", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        If EsAbierto(fila) Then
            MessageBox.Show("El turno sigue abierto: el cuadre se imprime al cerrar la caja", "Imprimir cuadre", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        ImprimeTicketCuadre(Monto(fila, "saldoInicial"), Monto(fila, "totalVentas"), Monto(fila, "totalTarjeta"), Monto(fila, "totalEgresos"),
                            Monto(fila, "totalFisico"), Monto(fila, "diferencia"))
    End Sub

    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        Close()
    End Sub
End Class
