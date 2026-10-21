<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormArqueoCaja
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.pnlEncabezado = New System.Windows.Forms.Panel()
        Me.lblTurno = New System.Windows.Forms.Label()
        Me.lblTitulo = New System.Windows.Forms.Label()
        Me.lblConteoTitulo = New System.Windows.Forms.Label()
        Me.lblSistemaTitulo = New System.Windows.Forms.Label()
        Me.dgvConteo = New System.Windows.Forms.DataGridView()
        Me.colDenominacion = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCantidad = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colSubtotal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lblSobranteTitulo = New System.Windows.Forms.Label()
        Me.lblSobrante = New System.Windows.Forms.Label()
        Me.lblInicialTitulo = New System.Windows.Forms.Label()
        Me.lblInicial = New System.Windows.Forms.Label()
        Me.lblVentaTitulo = New System.Windows.Forms.Label()
        Me.lblVenta = New System.Windows.Forms.Label()
        Me.lblTarjetaTitulo = New System.Windows.Forms.Label()
        Me.lblTarjeta = New System.Windows.Forms.Label()
        Me.lblHeladosTitulo = New System.Windows.Forms.Label()
        Me.lblHelados = New System.Windows.Forms.Label()
        Me.lblBebidasTitulo = New System.Windows.Forms.Label()
        Me.lblBebidas = New System.Windows.Forms.Label()
        Me.lblEgresosTitulo = New System.Windows.Forms.Label()
        Me.lblEgresos = New System.Windows.Forms.Label()
        Me.lblSeparador = New System.Windows.Forms.Label()
        Me.lblTotalEfectivoTitulo = New System.Windows.Forms.Label()
        Me.lblTotalEfectivo = New System.Windows.Forms.Label()
        Me.lblEfectivoCajaTitulo = New System.Windows.Forms.Label()
        Me.lblEfectivoCaja = New System.Windows.Forms.Label()
        Me.pnlResumen = New System.Windows.Forms.Panel()
        Me.lblEstado = New System.Windows.Forms.Label()
        Me.lblDiferencia = New System.Windows.Forms.Label()
        Me.lblDiferenciaTitulo = New System.Windows.Forms.Label()
        Me.lblEsperado = New System.Windows.Forms.Label()
        Me.lblEsperadoTitulo = New System.Windows.Forms.Label()
        Me.lblTotalArqueo = New System.Windows.Forms.Label()
        Me.lblTotalArqueoTitulo = New System.Windows.Forms.Label()
        Me.btnGuardar = New System.Windows.Forms.Button()
        Me.btnCerrar = New System.Windows.Forms.Button()
        Me.pnlEncabezado.SuspendLayout()
        CType(Me.dgvConteo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlResumen.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlEncabezado
        '
        Me.pnlEncabezado.Controls.Add(Me.lblTurno)
        Me.pnlEncabezado.Controls.Add(Me.lblTitulo)
        Me.pnlEncabezado.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlEncabezado.Location = New System.Drawing.Point(0, 0)
        Me.pnlEncabezado.Name = "pnlEncabezado"
        Me.pnlEncabezado.Size = New System.Drawing.Size(720, 60)
        Me.pnlEncabezado.TabIndex = 3
        Me.pnlEncabezado.Tag = "G"
        '
        'lblTitulo
        '
        Me.lblTitulo.AutoSize = True
        Me.lblTitulo.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitulo.Location = New System.Drawing.Point(16, 6)
        Me.lblTitulo.Name = "lblTitulo"
        Me.lblTitulo.Size = New System.Drawing.Size(157, 25)
        Me.lblTitulo.TabIndex = 0
        Me.lblTitulo.Tag = "FO"
        Me.lblTitulo.Text = "ARQUEO DE CAJA"
        '
        'lblTurno
        '
        Me.lblTurno.AutoSize = True
        Me.lblTurno.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTurno.Location = New System.Drawing.Point(18, 34)
        Me.lblTurno.Name = "lblTurno"
        Me.lblTurno.Size = New System.Drawing.Size(48, 19)
        Me.lblTurno.TabIndex = 1
        Me.lblTurno.Tag = "FO"
        Me.lblTurno.Text = "Turno:"
        '
        'lblConteoTitulo
        '
        Me.lblConteoTitulo.AutoSize = True
        Me.lblConteoTitulo.Location = New System.Drawing.Point(16, 72)
        Me.lblConteoTitulo.Name = "lblConteoTitulo"
        Me.lblConteoTitulo.Size = New System.Drawing.Size(124, 19)
        Me.lblConteoTitulo.TabIndex = 4
        Me.lblConteoTitulo.Tag = "T"
        Me.lblConteoTitulo.Text = "Conteo de efectivo"
        '
        'lblSistemaTitulo
        '
        Me.lblSistemaTitulo.AutoSize = True
        Me.lblSistemaTitulo.Location = New System.Drawing.Point(390, 72)
        Me.lblSistemaTitulo.Name = "lblSistemaTitulo"
        Me.lblSistemaTitulo.Size = New System.Drawing.Size(113, 19)
        Me.lblSistemaTitulo.TabIndex = 5
        Me.lblSistemaTitulo.Tag = "T"
        Me.lblSistemaTitulo.Text = "Según el sistema"
        '
        'dgvConteo
        '
        Me.dgvConteo.AllowUserToAddRows = False
        Me.dgvConteo.AllowUserToDeleteRows = False
        Me.dgvConteo.AllowUserToResizeColumns = False
        Me.dgvConteo.AllowUserToResizeRows = False
        Me.dgvConteo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvConteo.BackgroundColor = System.Drawing.Color.White
        Me.dgvConteo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvConteo.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvConteo.ColumnHeadersHeight = 28
        Me.dgvConteo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvConteo.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colDenominacion, Me.colCantidad, Me.colSubtotal})
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightSteelBlue
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.DarkBlue
        Me.dgvConteo.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgvConteo.Location = New System.Drawing.Point(16, 98)
        Me.dgvConteo.MultiSelect = False
        Me.dgvConteo.Name = "dgvConteo"
        Me.dgvConteo.RowHeadersVisible = False
        Me.dgvConteo.RowTemplate.Height = 26
        Me.dgvConteo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgvConteo.Size = New System.Drawing.Size(340, 322)
        Me.dgvConteo.TabIndex = 0
        '
        'colDenominacion
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle3.Format = "N2"
        Me.colDenominacion.DefaultCellStyle = DataGridViewCellStyle3
        Me.colDenominacion.HeaderText = "Denominación"
        Me.colDenominacion.Name = "colDenominacion"
        Me.colDenominacion.ReadOnly = True
        Me.colDenominacion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'colCantidad
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.White
        Me.colCantidad.DefaultCellStyle = DataGridViewCellStyle4
        Me.colCantidad.HeaderText = "Cantidad"
        Me.colCantidad.Name = "colCantidad"
        Me.colCantidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'colSubtotal
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle5.Format = "N2"
        Me.colSubtotal.DefaultCellStyle = DataGridViewCellStyle5
        Me.colSubtotal.HeaderText = "Subtotal"
        Me.colSubtotal.Name = "colSubtotal"
        Me.colSubtotal.ReadOnly = True
        Me.colSubtotal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'lblSobranteTitulo
        '
        Me.lblSobranteTitulo.AutoSize = True
        Me.lblSobranteTitulo.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSobranteTitulo.Location = New System.Drawing.Point(390, 100)
        Me.lblSobranteTitulo.Name = "lblSobranteTitulo"
        Me.lblSobranteTitulo.Size = New System.Drawing.Size(81, 19)
        Me.lblSobranteTitulo.TabIndex = 6
        Me.lblSobranteTitulo.Text = "(+) Sobrante"
        '
        'lblSobrante
        '
        Me.lblSobrante.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSobrante.Location = New System.Drawing.Point(530, 100)
        Me.lblSobrante.Name = "lblSobrante"
        Me.lblSobrante.Size = New System.Drawing.Size(160, 20)
        Me.lblSobrante.TabIndex = 7
        Me.lblSobrante.Text = "0.00"
        Me.lblSobrante.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblInicialTitulo
        '
        Me.lblInicialTitulo.AutoSize = True
        Me.lblInicialTitulo.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInicialTitulo.Location = New System.Drawing.Point(390, 126)
        Me.lblInicialTitulo.Name = "lblInicialTitulo"
        Me.lblInicialTitulo.Size = New System.Drawing.Size(113, 19)
        Me.lblInicialTitulo.TabIndex = 8
        Me.lblInicialTitulo.Text = "(+) Efectivo inicial"
        '
        'lblInicial
        '
        Me.lblInicial.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInicial.Location = New System.Drawing.Point(530, 126)
        Me.lblInicial.Name = "lblInicial"
        Me.lblInicial.Size = New System.Drawing.Size(160, 20)
        Me.lblInicial.TabIndex = 9
        Me.lblInicial.Text = "0.00"
        Me.lblInicial.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblVentaTitulo
        '
        Me.lblVentaTitulo.AutoSize = True
        Me.lblVentaTitulo.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVentaTitulo.Location = New System.Drawing.Point(390, 152)
        Me.lblVentaTitulo.Name = "lblVentaTitulo"
        Me.lblVentaTitulo.Size = New System.Drawing.Size(93, 19)
        Me.lblVentaTitulo.TabIndex = 10
        Me.lblVentaTitulo.Text = "(+) Total venta"
        '
        'lblVenta
        '
        Me.lblVenta.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVenta.Location = New System.Drawing.Point(530, 152)
        Me.lblVenta.Name = "lblVenta"
        Me.lblVenta.Size = New System.Drawing.Size(160, 20)
        Me.lblVenta.TabIndex = 11
        Me.lblVenta.Text = "0.00"
        Me.lblVenta.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTarjetaTitulo
        '
        Me.lblTarjetaTitulo.AutoSize = True
        Me.lblTarjetaTitulo.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTarjetaTitulo.Location = New System.Drawing.Point(390, 178)
        Me.lblTarjetaTitulo.Name = "lblTarjetaTitulo"
        Me.lblTarjetaTitulo.Size = New System.Drawing.Size(91, 19)
        Me.lblTarjetaTitulo.TabIndex = 12
        Me.lblTarjetaTitulo.Text = "(-) Venta POS"
        '
        'lblTarjeta
        '
        Me.lblTarjeta.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTarjeta.Location = New System.Drawing.Point(530, 178)
        Me.lblTarjeta.Name = "lblTarjeta"
        Me.lblTarjeta.Size = New System.Drawing.Size(160, 20)
        Me.lblTarjeta.TabIndex = 13
        Me.lblTarjeta.Text = "0.00"
        Me.lblTarjeta.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblHeladosTitulo
        '
        Me.lblHeladosTitulo.AutoSize = True
        Me.lblHeladosTitulo.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeladosTitulo.Location = New System.Drawing.Point(390, 204)
        Me.lblHeladosTitulo.Name = "lblHeladosTitulo"
        Me.lblHeladosTitulo.Size = New System.Drawing.Size(77, 19)
        Me.lblHeladosTitulo.TabIndex = 14
        Me.lblHeladosTitulo.Text = "(-) Helados"
        '
        'lblHelados
        '
        Me.lblHelados.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHelados.Location = New System.Drawing.Point(530, 204)
        Me.lblHelados.Name = "lblHelados"
        Me.lblHelados.Size = New System.Drawing.Size(160, 20)
        Me.lblHelados.TabIndex = 15
        Me.lblHelados.Text = "0.00"
        Me.lblHelados.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblBebidasTitulo
        '
        Me.lblBebidasTitulo.AutoSize = True
        Me.lblBebidasTitulo.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBebidasTitulo.Location = New System.Drawing.Point(390, 230)
        Me.lblBebidasTitulo.Name = "lblBebidasTitulo"
        Me.lblBebidasTitulo.Size = New System.Drawing.Size(76, 19)
        Me.lblBebidasTitulo.TabIndex = 16
        Me.lblBebidasTitulo.Text = "(-) Bebidas"
        '
        'lblBebidas
        '
        Me.lblBebidas.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBebidas.Location = New System.Drawing.Point(530, 230)
        Me.lblBebidas.Name = "lblBebidas"
        Me.lblBebidas.Size = New System.Drawing.Size(160, 20)
        Me.lblBebidas.TabIndex = 17
        Me.lblBebidas.Text = "0.00"
        Me.lblBebidas.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblEgresosTitulo
        '
        Me.lblEgresosTitulo.AutoSize = True
        Me.lblEgresosTitulo.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEgresosTitulo.Location = New System.Drawing.Point(390, 256)
        Me.lblEgresosTitulo.Name = "lblEgresosTitulo"
        Me.lblEgresosTitulo.Size = New System.Drawing.Size(76, 19)
        Me.lblEgresosTitulo.TabIndex = 18
        Me.lblEgresosTitulo.Text = "(-) Egresos"
        '
        'lblEgresos
        '
        Me.lblEgresos.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEgresos.Location = New System.Drawing.Point(530, 256)
        Me.lblEgresos.Name = "lblEgresos"
        Me.lblEgresos.Size = New System.Drawing.Size(160, 20)
        Me.lblEgresos.TabIndex = 19
        Me.lblEgresos.Text = "0.00"
        Me.lblEgresos.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblSeparador
        '
        Me.lblSeparador.BackColor = System.Drawing.Color.DarkBlue
        Me.lblSeparador.Location = New System.Drawing.Point(390, 284)
        Me.lblSeparador.Name = "lblSeparador"
        Me.lblSeparador.Size = New System.Drawing.Size(300, 1)
        Me.lblSeparador.TabIndex = 20
        '
        'lblTotalEfectivoTitulo
        '
        Me.lblTotalEfectivoTitulo.AutoSize = True
        Me.lblTotalEfectivoTitulo.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalEfectivoTitulo.Location = New System.Drawing.Point(390, 292)
        Me.lblTotalEfectivoTitulo.Name = "lblTotalEfectivoTitulo"
        Me.lblTotalEfectivoTitulo.Size = New System.Drawing.Size(97, 19)
        Me.lblTotalEfectivoTitulo.TabIndex = 21
        Me.lblTotalEfectivoTitulo.Text = "Total efectivo"
        '
        'lblTotalEfectivo
        '
        Me.lblTotalEfectivo.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalEfectivo.Location = New System.Drawing.Point(530, 292)
        Me.lblTotalEfectivo.Name = "lblTotalEfectivo"
        Me.lblTotalEfectivo.Size = New System.Drawing.Size(160, 20)
        Me.lblTotalEfectivo.TabIndex = 22
        Me.lblTotalEfectivo.Text = "0.00"
        Me.lblTotalEfectivo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblEfectivoCajaTitulo
        '
        Me.lblEfectivoCajaTitulo.AutoSize = True
        Me.lblEfectivoCajaTitulo.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEfectivoCajaTitulo.Location = New System.Drawing.Point(390, 320)
        Me.lblEfectivoCajaTitulo.Name = "lblEfectivoCajaTitulo"
        Me.lblEfectivoCajaTitulo.Size = New System.Drawing.Size(137, 19)
        Me.lblEfectivoCajaTitulo.TabIndex = 23
        Me.lblEfectivoCajaTitulo.Text = "(+) Helados y bebidas"
        '
        'lblEfectivoCaja
        '
        Me.lblEfectivoCaja.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEfectivoCaja.Location = New System.Drawing.Point(530, 320)
        Me.lblEfectivoCaja.Name = "lblEfectivoCaja"
        Me.lblEfectivoCaja.Size = New System.Drawing.Size(160, 20)
        Me.lblEfectivoCaja.TabIndex = 24
        Me.lblEfectivoCaja.Text = "0.00"
        Me.lblEfectivoCaja.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlResumen
        '
        Me.pnlResumen.BackColor = System.Drawing.Color.AliceBlue
        Me.pnlResumen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlResumen.Controls.Add(Me.lblEstado)
        Me.pnlResumen.Controls.Add(Me.lblDiferencia)
        Me.pnlResumen.Controls.Add(Me.lblDiferenciaTitulo)
        Me.pnlResumen.Controls.Add(Me.lblEsperado)
        Me.pnlResumen.Controls.Add(Me.lblEsperadoTitulo)
        Me.pnlResumen.Controls.Add(Me.lblTotalArqueo)
        Me.pnlResumen.Controls.Add(Me.lblTotalArqueoTitulo)
        Me.pnlResumen.Location = New System.Drawing.Point(16, 432)
        Me.pnlResumen.Name = "pnlResumen"
        Me.pnlResumen.Size = New System.Drawing.Size(688, 84)
        Me.pnlResumen.TabIndex = 25
        '
        'lblTotalArqueoTitulo
        '
        Me.lblTotalArqueoTitulo.AutoSize = True
        Me.lblTotalArqueoTitulo.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalArqueoTitulo.Location = New System.Drawing.Point(14, 8)
        Me.lblTotalArqueoTitulo.Name = "lblTotalArqueoTitulo"
        Me.lblTotalArqueoTitulo.Size = New System.Drawing.Size(51, 15)
        Me.lblTotalArqueoTitulo.TabIndex = 0
        Me.lblTotalArqueoTitulo.Text = "Contado"
        '
        'lblTotalArqueo
        '
        Me.lblTotalArqueo.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalArqueo.Location = New System.Drawing.Point(12, 28)
        Me.lblTotalArqueo.Name = "lblTotalArqueo"
        Me.lblTotalArqueo.Size = New System.Drawing.Size(200, 32)
        Me.lblTotalArqueo.TabIndex = 1
        Me.lblTotalArqueo.Text = "0.00"
        Me.lblTotalArqueo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblEsperadoTitulo
        '
        Me.lblEsperadoTitulo.AutoSize = True
        Me.lblEsperadoTitulo.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEsperadoTitulo.Location = New System.Drawing.Point(250, 8)
        Me.lblEsperadoTitulo.Name = "lblEsperadoTitulo"
        Me.lblEsperadoTitulo.Size = New System.Drawing.Size(98, 15)
        Me.lblEsperadoTitulo.TabIndex = 2
        Me.lblEsperadoTitulo.Text = "Esperado en caja"
        '
        'lblEsperado
        '
        Me.lblEsperado.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEsperado.Location = New System.Drawing.Point(248, 28)
        Me.lblEsperado.Name = "lblEsperado"
        Me.lblEsperado.Size = New System.Drawing.Size(200, 32)
        Me.lblEsperado.TabIndex = 3
        Me.lblEsperado.Text = "0.00"
        Me.lblEsperado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDiferenciaTitulo
        '
        Me.lblDiferenciaTitulo.AutoSize = True
        Me.lblDiferenciaTitulo.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDiferenciaTitulo.Location = New System.Drawing.Point(480, 8)
        Me.lblDiferenciaTitulo.Name = "lblDiferenciaTitulo"
        Me.lblDiferenciaTitulo.Size = New System.Drawing.Size(58, 15)
        Me.lblDiferenciaTitulo.TabIndex = 4
        Me.lblDiferenciaTitulo.Text = "Diferencia"
        '
        'lblDiferencia
        '
        Me.lblDiferencia.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDiferencia.Location = New System.Drawing.Point(478, 28)
        Me.lblDiferencia.Name = "lblDiferencia"
        Me.lblDiferencia.Size = New System.Drawing.Size(200, 32)
        Me.lblDiferencia.TabIndex = 5
        Me.lblDiferencia.Text = "0.00"
        Me.lblDiferencia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblEstado
        '
        Me.lblEstado.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEstado.Location = New System.Drawing.Point(480, 60)
        Me.lblEstado.Name = "lblEstado"
        Me.lblEstado.Size = New System.Drawing.Size(200, 20)
        Me.lblEstado.TabIndex = 6
        Me.lblEstado.Text = "Caja cuadrada"
        Me.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnGuardar
        '
        Me.btnGuardar.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGuardar.Location = New System.Drawing.Point(454, 526)
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.Size = New System.Drawing.Size(170, 38)
        Me.btnGuardar.TabIndex = 1
        Me.btnGuardar.Tag = "DB"
        Me.btnGuardar.Text = "Guardar e imprimir"
        Me.btnGuardar.UseVisualStyleBackColor = True
        '
        'btnCerrar
        '
        Me.btnCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCerrar.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCerrar.Location = New System.Drawing.Point(634, 526)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(70, 38)
        Me.btnCerrar.TabIndex = 2
        Me.btnCerrar.Tag = "WB"
        Me.btnCerrar.Text = "Cerrar"
        Me.btnCerrar.UseVisualStyleBackColor = True
        '
        'FormArqueoCaja
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCerrar
        Me.ClientSize = New System.Drawing.Size(720, 580)
        Me.Controls.Add(Me.btnCerrar)
        Me.Controls.Add(Me.btnGuardar)
        Me.Controls.Add(Me.pnlResumen)
        Me.Controls.Add(Me.lblEfectivoCaja)
        Me.Controls.Add(Me.lblEfectivoCajaTitulo)
        Me.Controls.Add(Me.lblTotalEfectivo)
        Me.Controls.Add(Me.lblTotalEfectivoTitulo)
        Me.Controls.Add(Me.lblSeparador)
        Me.Controls.Add(Me.lblEgresos)
        Me.Controls.Add(Me.lblEgresosTitulo)
        Me.Controls.Add(Me.lblBebidas)
        Me.Controls.Add(Me.lblBebidasTitulo)
        Me.Controls.Add(Me.lblHelados)
        Me.Controls.Add(Me.lblHeladosTitulo)
        Me.Controls.Add(Me.lblTarjeta)
        Me.Controls.Add(Me.lblTarjetaTitulo)
        Me.Controls.Add(Me.lblVenta)
        Me.Controls.Add(Me.lblVentaTitulo)
        Me.Controls.Add(Me.lblInicial)
        Me.Controls.Add(Me.lblInicialTitulo)
        Me.Controls.Add(Me.lblSobrante)
        Me.Controls.Add(Me.lblSobranteTitulo)
        Me.Controls.Add(Me.dgvConteo)
        Me.Controls.Add(Me.lblSistemaTitulo)
        Me.Controls.Add(Me.lblConteoTitulo)
        Me.Controls.Add(Me.pnlEncabezado)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormArqueoCaja"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Arqueo de caja"
        Me.pnlEncabezado.ResumeLayout(False)
        Me.pnlEncabezado.PerformLayout()
        CType(Me.dgvConteo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlResumen.ResumeLayout(False)
        Me.pnlResumen.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlEncabezado As Panel
    Friend WithEvents lblTitulo As Label
    Friend WithEvents lblTurno As Label
    Friend WithEvents lblConteoTitulo As Label
    Friend WithEvents lblSistemaTitulo As Label
    Friend WithEvents dgvConteo As DataGridView
    Friend WithEvents colDenominacion As DataGridViewTextBoxColumn
    Friend WithEvents colCantidad As DataGridViewTextBoxColumn
    Friend WithEvents colSubtotal As DataGridViewTextBoxColumn
    Friend WithEvents lblSobranteTitulo As Label
    Friend WithEvents lblSobrante As Label
    Friend WithEvents lblInicialTitulo As Label
    Friend WithEvents lblInicial As Label
    Friend WithEvents lblVentaTitulo As Label
    Friend WithEvents lblVenta As Label
    Friend WithEvents lblTarjetaTitulo As Label
    Friend WithEvents lblTarjeta As Label
    Friend WithEvents lblHeladosTitulo As Label
    Friend WithEvents lblHelados As Label
    Friend WithEvents lblBebidasTitulo As Label
    Friend WithEvents lblBebidas As Label
    Friend WithEvents lblEgresosTitulo As Label
    Friend WithEvents lblEgresos As Label
    Friend WithEvents lblSeparador As Label
    Friend WithEvents lblTotalEfectivoTitulo As Label
    Friend WithEvents lblTotalEfectivo As Label
    Friend WithEvents lblEfectivoCajaTitulo As Label
    Friend WithEvents lblEfectivoCaja As Label
    Friend WithEvents pnlResumen As Panel
    Friend WithEvents lblTotalArqueoTitulo As Label
    Friend WithEvents lblTotalArqueo As Label
    Friend WithEvents lblEsperadoTitulo As Label
    Friend WithEvents lblEsperado As Label
    Friend WithEvents lblDiferenciaTitulo As Label
    Friend WithEvents lblDiferencia As Label
    Friend WithEvents lblEstado As Label
    Friend WithEvents btnGuardar As Button
    Friend WithEvents btnCerrar As Button
End Class
