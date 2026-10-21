<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormVerTurnos
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
        Me.pnlEncabezado = New System.Windows.Forms.Panel()
        Me.lblSubtitulo = New System.Windows.Forms.Label()
        Me.lblTitulo = New System.Windows.Forms.Label()
        Me.pnlFiltros = New System.Windows.Forms.Panel()
        Me.lblNotaFiltro = New System.Windows.Forms.Label()
        Me.btnActualizar = New System.Windows.Forms.Button()
        Me.dtpHasta = New System.Windows.Forms.DateTimePicker()
        Me.lblHasta = New System.Windows.Forms.Label()
        Me.dtpDesde = New System.Windows.Forms.DateTimePicker()
        Me.lblDesde = New System.Windows.Forms.Label()
        Me.cboEstado = New System.Windows.Forms.ComboBox()
        Me.lblEstadoFiltro = New System.Windows.Forms.Label()
        Me.cboSucursal = New System.Windows.Forms.ComboBox()
        Me.lblSucursal = New System.Windows.Forms.Label()
        Me.tlpResumen = New System.Windows.Forms.TableLayoutPanel()
        Me.pnlCardAbiertos = New System.Windows.Forms.Panel()
        Me.lblAbiertos = New System.Windows.Forms.Label()
        Me.lblAbiertosTitulo = New System.Windows.Forms.Label()
        Me.pnlCardCerrados = New System.Windows.Forms.Panel()
        Me.lblCerrados = New System.Windows.Forms.Label()
        Me.lblCerradosTitulo = New System.Windows.Forms.Label()
        Me.pnlCardFaltantes = New System.Windows.Forms.Panel()
        Me.lblFaltantes = New System.Windows.Forms.Label()
        Me.lblFaltantesTitulo = New System.Windows.Forms.Label()
        Me.pnlCardNeta = New System.Windows.Forms.Panel()
        Me.lblDiferenciaNeta = New System.Windows.Forms.Label()
        Me.lblDiferenciaNetaTitulo = New System.Windows.Forms.Label()
        Me.pnlPie = New System.Windows.Forms.Panel()
        Me.lblTotalTurnos = New System.Windows.Forms.Label()
        Me.btnImprimir = New System.Windows.Forms.Button()
        Me.btnCerrar = New System.Windows.Forms.Button()
        Me.pnlDetalle = New System.Windows.Forms.Panel()
        Me.lblDetalleNota = New System.Windows.Forms.Label()
        Me.tlpDetalle = New System.Windows.Forms.TableLayoutPanel()
        Me.lblDetalleEstado = New System.Windows.Forms.Label()
        Me.lblDetalleTitulo = New System.Windows.Forms.Label()
        Me.dgvTurnos = New System.Windows.Forms.DataGridView()
        Me.lblSinDatos = New System.Windows.Forms.Label()
        Me.pnlEncabezado.SuspendLayout()
        Me.pnlFiltros.SuspendLayout()
        Me.tlpResumen.SuspendLayout()
        Me.pnlCardAbiertos.SuspendLayout()
        Me.pnlCardCerrados.SuspendLayout()
        Me.pnlCardFaltantes.SuspendLayout()
        Me.pnlCardNeta.SuspendLayout()
        Me.pnlPie.SuspendLayout()
        Me.pnlDetalle.SuspendLayout()
        CType(Me.dgvTurnos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.dgvTurnos.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlEncabezado
        '
        Me.pnlEncabezado.Controls.Add(Me.lblSubtitulo)
        Me.pnlEncabezado.Controls.Add(Me.lblTitulo)
        Me.pnlEncabezado.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlEncabezado.Location = New System.Drawing.Point(0, 0)
        Me.pnlEncabezado.Name = "pnlEncabezado"
        Me.pnlEncabezado.Size = New System.Drawing.Size(1180, 60)
        Me.pnlEncabezado.TabIndex = 5
        Me.pnlEncabezado.Tag = "G"
        '
        'lblTitulo
        '
        Me.lblTitulo.AutoSize = True
        Me.lblTitulo.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitulo.Location = New System.Drawing.Point(16, 6)
        Me.lblTitulo.Name = "lblTitulo"
        Me.lblTitulo.Size = New System.Drawing.Size(158, 25)
        Me.lblTitulo.TabIndex = 0
        Me.lblTitulo.Tag = "FO"
        Me.lblTitulo.Text = "TURNOS DE CAJA"
        '
        'lblSubtitulo
        '
        Me.lblSubtitulo.AutoSize = True
        Me.lblSubtitulo.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSubtitulo.Location = New System.Drawing.Point(18, 34)
        Me.lblSubtitulo.Name = "lblSubtitulo"
        Me.lblSubtitulo.Size = New System.Drawing.Size(299, 19)
        Me.lblSubtitulo.TabIndex = 1
        Me.lblSubtitulo.Tag = "FO"
        Me.lblSubtitulo.Text = "Aperturas y cierres de caja por sucursal y vendedor"
        '
        'pnlFiltros
        '
        Me.pnlFiltros.Controls.Add(Me.lblNotaFiltro)
        Me.pnlFiltros.Controls.Add(Me.btnActualizar)
        Me.pnlFiltros.Controls.Add(Me.dtpHasta)
        Me.pnlFiltros.Controls.Add(Me.lblHasta)
        Me.pnlFiltros.Controls.Add(Me.dtpDesde)
        Me.pnlFiltros.Controls.Add(Me.lblDesde)
        Me.pnlFiltros.Controls.Add(Me.cboEstado)
        Me.pnlFiltros.Controls.Add(Me.lblEstadoFiltro)
        Me.pnlFiltros.Controls.Add(Me.cboSucursal)
        Me.pnlFiltros.Controls.Add(Me.lblSucursal)
        Me.pnlFiltros.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlFiltros.Location = New System.Drawing.Point(0, 60)
        Me.pnlFiltros.Name = "pnlFiltros"
        Me.pnlFiltros.Size = New System.Drawing.Size(1180, 68)
        Me.pnlFiltros.TabIndex = 4
        '
        'lblSucursal
        '
        Me.lblSucursal.AutoSize = True
        Me.lblSucursal.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSucursal.Location = New System.Drawing.Point(16, 14)
        Me.lblSucursal.Name = "lblSucursal"
        Me.lblSucursal.Size = New System.Drawing.Size(64, 19)
        Me.lblSucursal.TabIndex = 0
        Me.lblSucursal.Text = "Sucursal"
        '
        'cboSucursal
        '
        Me.cboSucursal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSucursal.FormattingEnabled = True
        Me.cboSucursal.Location = New System.Drawing.Point(86, 10)
        Me.cboSucursal.Name = "cboSucursal"
        Me.cboSucursal.Size = New System.Drawing.Size(200, 26)
        Me.cboSucursal.TabIndex = 1
        '
        'lblEstadoFiltro
        '
        Me.lblEstadoFiltro.AutoSize = True
        Me.lblEstadoFiltro.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEstadoFiltro.Location = New System.Drawing.Point(306, 14)
        Me.lblEstadoFiltro.Name = "lblEstadoFiltro"
        Me.lblEstadoFiltro.Size = New System.Drawing.Size(51, 19)
        Me.lblEstadoFiltro.TabIndex = 2
        Me.lblEstadoFiltro.Text = "Estado"
        '
        'cboEstado
        '
        Me.cboEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboEstado.FormattingEnabled = True
        Me.cboEstado.Items.AddRange(New Object() {"Todos", "Abiertos", "Cerrados"})
        Me.cboEstado.Location = New System.Drawing.Point(363, 10)
        Me.cboEstado.Name = "cboEstado"
        Me.cboEstado.Size = New System.Drawing.Size(120, 26)
        Me.cboEstado.TabIndex = 3
        '
        'lblDesde
        '
        Me.lblDesde.AutoSize = True
        Me.lblDesde.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDesde.Location = New System.Drawing.Point(503, 14)
        Me.lblDesde.Name = "lblDesde"
        Me.lblDesde.Size = New System.Drawing.Size(48, 19)
        Me.lblDesde.TabIndex = 4
        Me.lblDesde.Text = "Desde"
        '
        'dtpDesde
        '
        Me.dtpDesde.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short
        Me.dtpDesde.Location = New System.Drawing.Point(557, 10)
        Me.dtpDesde.Name = "dtpDesde"
        Me.dtpDesde.Size = New System.Drawing.Size(110, 25)
        Me.dtpDesde.TabIndex = 5
        '
        'lblHasta
        '
        Me.lblHasta.AutoSize = True
        Me.lblHasta.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHasta.Location = New System.Drawing.Point(687, 14)
        Me.lblHasta.Name = "lblHasta"
        Me.lblHasta.Size = New System.Drawing.Size(45, 19)
        Me.lblHasta.TabIndex = 6
        Me.lblHasta.Text = "Hasta"
        '
        'dtpHasta
        '
        Me.dtpHasta.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short
        Me.dtpHasta.Location = New System.Drawing.Point(738, 10)
        Me.dtpHasta.Name = "dtpHasta"
        Me.dtpHasta.Size = New System.Drawing.Size(110, 25)
        Me.dtpHasta.TabIndex = 7
        '
        'btnActualizar
        '
        Me.btnActualizar.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnActualizar.Location = New System.Drawing.Point(868, 7)
        Me.btnActualizar.Name = "btnActualizar"
        Me.btnActualizar.Size = New System.Drawing.Size(150, 32)
        Me.btnActualizar.TabIndex = 8
        Me.btnActualizar.Tag = "WB"
        Me.btnActualizar.Text = "Actualizar (F5)"
        Me.btnActualizar.UseVisualStyleBackColor = True
        '
        'lblNotaFiltro
        '
        Me.lblNotaFiltro.AutoSize = True
        Me.lblNotaFiltro.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNotaFiltro.Location = New System.Drawing.Point(16, 44)
        Me.lblNotaFiltro.Name = "lblNotaFiltro"
        Me.lblNotaFiltro.Size = New System.Drawing.Size(390, 15)
        Me.lblNotaFiltro.TabIndex = 9
        Me.lblNotaFiltro.Text = "Las fechas filtran por apertura. Los turnos abiertos se muestran siempre."
        '
        'tlpResumen
        '
        Me.tlpResumen.ColumnCount = 4
        Me.tlpResumen.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.tlpResumen.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.tlpResumen.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.tlpResumen.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.tlpResumen.Controls.Add(Me.pnlCardAbiertos, 0, 0)
        Me.tlpResumen.Controls.Add(Me.pnlCardCerrados, 1, 0)
        Me.tlpResumen.Controls.Add(Me.pnlCardFaltantes, 2, 0)
        Me.tlpResumen.Controls.Add(Me.pnlCardNeta, 3, 0)
        Me.tlpResumen.Dock = System.Windows.Forms.DockStyle.Top
        Me.tlpResumen.Location = New System.Drawing.Point(0, 128)
        Me.tlpResumen.Name = "tlpResumen"
        Me.tlpResumen.Padding = New System.Windows.Forms.Padding(10, 0, 10, 6)
        Me.tlpResumen.RowCount = 1
        Me.tlpResumen.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpResumen.Size = New System.Drawing.Size(1180, 84)
        Me.tlpResumen.TabIndex = 3
        '
        'pnlCardAbiertos
        '
        Me.pnlCardAbiertos.BackColor = System.Drawing.Color.AliceBlue
        Me.pnlCardAbiertos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlCardAbiertos.Controls.Add(Me.lblAbiertos)
        Me.pnlCardAbiertos.Controls.Add(Me.lblAbiertosTitulo)
        Me.pnlCardAbiertos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlCardAbiertos.Location = New System.Drawing.Point(16, 3)
        Me.pnlCardAbiertos.Name = "pnlCardAbiertos"
        Me.pnlCardAbiertos.Size = New System.Drawing.Size(278, 69)
        Me.pnlCardAbiertos.TabIndex = 0
        '
        'lblAbiertosTitulo
        '
        Me.lblAbiertosTitulo.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblAbiertosTitulo.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAbiertosTitulo.Location = New System.Drawing.Point(0, 0)
        Me.lblAbiertosTitulo.Name = "lblAbiertosTitulo"
        Me.lblAbiertosTitulo.Padding = New System.Windows.Forms.Padding(10, 6, 0, 0)
        Me.lblAbiertosTitulo.Size = New System.Drawing.Size(276, 26)
        Me.lblAbiertosTitulo.TabIndex = 0
        Me.lblAbiertosTitulo.Text = "Cajas abiertas ahora"
        '
        'lblAbiertos
        '
        Me.lblAbiertos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblAbiertos.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAbiertos.Location = New System.Drawing.Point(0, 26)
        Me.lblAbiertos.Name = "lblAbiertos"
        Me.lblAbiertos.Padding = New System.Windows.Forms.Padding(8, 0, 0, 0)
        Me.lblAbiertos.Size = New System.Drawing.Size(276, 41)
        Me.lblAbiertos.TabIndex = 1
        Me.lblAbiertos.Text = "0"
        Me.lblAbiertos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlCardCerrados
        '
        Me.pnlCardCerrados.BackColor = System.Drawing.Color.AliceBlue
        Me.pnlCardCerrados.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlCardCerrados.Controls.Add(Me.lblCerrados)
        Me.pnlCardCerrados.Controls.Add(Me.lblCerradosTitulo)
        Me.pnlCardCerrados.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlCardCerrados.Location = New System.Drawing.Point(300, 3)
        Me.pnlCardCerrados.Name = "pnlCardCerrados"
        Me.pnlCardCerrados.Size = New System.Drawing.Size(278, 69)
        Me.pnlCardCerrados.TabIndex = 1
        '
        'lblCerradosTitulo
        '
        Me.lblCerradosTitulo.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblCerradosTitulo.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCerradosTitulo.Location = New System.Drawing.Point(0, 0)
        Me.lblCerradosTitulo.Name = "lblCerradosTitulo"
        Me.lblCerradosTitulo.Padding = New System.Windows.Forms.Padding(10, 6, 0, 0)
        Me.lblCerradosTitulo.Size = New System.Drawing.Size(276, 26)
        Me.lblCerradosTitulo.TabIndex = 0
        Me.lblCerradosTitulo.Text = "Turnos cerrados en el periodo"
        '
        'lblCerrados
        '
        Me.lblCerrados.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblCerrados.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCerrados.Location = New System.Drawing.Point(0, 26)
        Me.lblCerrados.Name = "lblCerrados"
        Me.lblCerrados.Padding = New System.Windows.Forms.Padding(8, 0, 0, 0)
        Me.lblCerrados.Size = New System.Drawing.Size(276, 41)
        Me.lblCerrados.TabIndex = 1
        Me.lblCerrados.Text = "0"
        Me.lblCerrados.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlCardFaltantes
        '
        Me.pnlCardFaltantes.BackColor = System.Drawing.Color.AliceBlue
        Me.pnlCardFaltantes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlCardFaltantes.Controls.Add(Me.lblFaltantes)
        Me.pnlCardFaltantes.Controls.Add(Me.lblFaltantesTitulo)
        Me.pnlCardFaltantes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlCardFaltantes.Location = New System.Drawing.Point(584, 3)
        Me.pnlCardFaltantes.Name = "pnlCardFaltantes"
        Me.pnlCardFaltantes.Size = New System.Drawing.Size(278, 69)
        Me.pnlCardFaltantes.TabIndex = 2
        '
        'lblFaltantesTitulo
        '
        Me.lblFaltantesTitulo.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblFaltantesTitulo.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFaltantesTitulo.Location = New System.Drawing.Point(0, 0)
        Me.lblFaltantesTitulo.Name = "lblFaltantesTitulo"
        Me.lblFaltantesTitulo.Padding = New System.Windows.Forms.Padding(10, 6, 0, 0)
        Me.lblFaltantesTitulo.Size = New System.Drawing.Size(276, 26)
        Me.lblFaltantesTitulo.TabIndex = 0
        Me.lblFaltantesTitulo.Text = "Cierres con faltante"
        '
        'lblFaltantes
        '
        Me.lblFaltantes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblFaltantes.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFaltantes.Location = New System.Drawing.Point(0, 26)
        Me.lblFaltantes.Name = "lblFaltantes"
        Me.lblFaltantes.Padding = New System.Windows.Forms.Padding(8, 0, 0, 0)
        Me.lblFaltantes.Size = New System.Drawing.Size(276, 41)
        Me.lblFaltantes.TabIndex = 1
        Me.lblFaltantes.Text = "0"
        Me.lblFaltantes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlCardNeta
        '
        Me.pnlCardNeta.BackColor = System.Drawing.Color.AliceBlue
        Me.pnlCardNeta.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlCardNeta.Controls.Add(Me.lblDiferenciaNeta)
        Me.pnlCardNeta.Controls.Add(Me.lblDiferenciaNetaTitulo)
        Me.pnlCardNeta.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlCardNeta.Location = New System.Drawing.Point(868, 3)
        Me.pnlCardNeta.Name = "pnlCardNeta"
        Me.pnlCardNeta.Size = New System.Drawing.Size(296, 69)
        Me.pnlCardNeta.TabIndex = 3
        '
        'lblDiferenciaNetaTitulo
        '
        Me.lblDiferenciaNetaTitulo.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblDiferenciaNetaTitulo.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDiferenciaNetaTitulo.Location = New System.Drawing.Point(0, 0)
        Me.lblDiferenciaNetaTitulo.Name = "lblDiferenciaNetaTitulo"
        Me.lblDiferenciaNetaTitulo.Padding = New System.Windows.Forms.Padding(10, 6, 0, 0)
        Me.lblDiferenciaNetaTitulo.Size = New System.Drawing.Size(294, 26)
        Me.lblDiferenciaNetaTitulo.TabIndex = 0
        Me.lblDiferenciaNetaTitulo.Text = "Diferencia neta de los cierres"
        '
        'lblDiferenciaNeta
        '
        Me.lblDiferenciaNeta.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblDiferenciaNeta.Font = New System.Drawing.Font("Segoe UI", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDiferenciaNeta.Location = New System.Drawing.Point(0, 26)
        Me.lblDiferenciaNeta.Name = "lblDiferenciaNeta"
        Me.lblDiferenciaNeta.Padding = New System.Windows.Forms.Padding(8, 0, 0, 0)
        Me.lblDiferenciaNeta.Size = New System.Drawing.Size(294, 41)
        Me.lblDiferenciaNeta.TabIndex = 1
        Me.lblDiferenciaNeta.Text = "0.00"
        Me.lblDiferenciaNeta.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlPie
        '
        Me.pnlPie.Controls.Add(Me.lblTotalTurnos)
        Me.pnlPie.Controls.Add(Me.btnImprimir)
        Me.pnlPie.Controls.Add(Me.btnCerrar)
        Me.pnlPie.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlPie.Location = New System.Drawing.Point(0, 604)
        Me.pnlPie.Name = "pnlPie"
        Me.pnlPie.Size = New System.Drawing.Size(1180, 56)
        Me.pnlPie.TabIndex = 2
        '
        'lblTotalTurnos
        '
        Me.lblTotalTurnos.AutoSize = True
        Me.lblTotalTurnos.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalTurnos.Location = New System.Drawing.Point(16, 18)
        Me.lblTotalTurnos.Name = "lblTotalTurnos"
        Me.lblTotalTurnos.Size = New System.Drawing.Size(60, 19)
        Me.lblTotalTurnos.TabIndex = 0
        Me.lblTotalTurnos.Text = "0 turnos"
        '
        'btnImprimir
        '
        Me.btnImprimir.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnImprimir.Enabled = False
        Me.btnImprimir.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnImprimir.Location = New System.Drawing.Point(934, 9)
        Me.btnImprimir.Name = "btnImprimir"
        Me.btnImprimir.Size = New System.Drawing.Size(150, 38)
        Me.btnImprimir.TabIndex = 1
        Me.btnImprimir.Tag = "DB"
        Me.btnImprimir.Text = "Imprimir cuadre"
        Me.btnImprimir.UseVisualStyleBackColor = True
        '
        'btnCerrar
        '
        Me.btnCerrar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCerrar.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCerrar.Location = New System.Drawing.Point(1094, 9)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(70, 38)
        Me.btnCerrar.TabIndex = 2
        Me.btnCerrar.Tag = "WB"
        Me.btnCerrar.Text = "Cerrar"
        Me.btnCerrar.UseVisualStyleBackColor = True
        '
        'pnlDetalle
        '
        Me.pnlDetalle.BackColor = System.Drawing.Color.AliceBlue
        Me.pnlDetalle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlDetalle.Controls.Add(Me.lblDetalleNota)
        Me.pnlDetalle.Controls.Add(Me.tlpDetalle)
        Me.pnlDetalle.Controls.Add(Me.lblDetalleEstado)
        Me.pnlDetalle.Controls.Add(Me.lblDetalleTitulo)
        Me.pnlDetalle.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlDetalle.Location = New System.Drawing.Point(850, 212)
        Me.pnlDetalle.Name = "pnlDetalle"
        Me.pnlDetalle.Padding = New System.Windows.Forms.Padding(12, 10, 12, 10)
        Me.pnlDetalle.Size = New System.Drawing.Size(330, 392)
        Me.pnlDetalle.TabIndex = 1
        '
        'lblDetalleTitulo
        '
        Me.lblDetalleTitulo.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblDetalleTitulo.Font = New System.Drawing.Font("Segoe UI", 13.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDetalleTitulo.Location = New System.Drawing.Point(12, 10)
        Me.lblDetalleTitulo.Name = "lblDetalleTitulo"
        Me.lblDetalleTitulo.Size = New System.Drawing.Size(304, 30)
        Me.lblDetalleTitulo.TabIndex = 0
        Me.lblDetalleTitulo.Text = "Detalle del turno"
        Me.lblDetalleTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDetalleEstado
        '
        Me.lblDetalleEstado.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblDetalleEstado.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDetalleEstado.Location = New System.Drawing.Point(12, 40)
        Me.lblDetalleEstado.Name = "lblDetalleEstado"
        Me.lblDetalleEstado.Size = New System.Drawing.Size(304, 24)
        Me.lblDetalleEstado.TabIndex = 1
        Me.lblDetalleEstado.Text = "Seleccione un turno"
        Me.lblDetalleEstado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tlpDetalle
        '
        Me.tlpDetalle.AutoSize = True
        Me.tlpDetalle.ColumnCount = 2
        Me.tlpDetalle.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpDetalle.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpDetalle.Dock = System.Windows.Forms.DockStyle.Top
        Me.tlpDetalle.Location = New System.Drawing.Point(12, 64)
        Me.tlpDetalle.Name = "tlpDetalle"
        Me.tlpDetalle.RowCount = 1
        Me.tlpDetalle.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tlpDetalle.Size = New System.Drawing.Size(304, 0)
        Me.tlpDetalle.TabIndex = 2
        '
        'lblDetalleNota
        '
        Me.lblDetalleNota.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblDetalleNota.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDetalleNota.Location = New System.Drawing.Point(12, 64)
        Me.lblDetalleNota.Name = "lblDetalleNota"
        Me.lblDetalleNota.Padding = New System.Windows.Forms.Padding(0, 10, 0, 0)
        Me.lblDetalleNota.Size = New System.Drawing.Size(304, 316)
        Me.lblDetalleNota.TabIndex = 3
        Me.lblDetalleNota.Text = "Seleccione un turno de la lista para ver su desglose."
        '
        'dgvTurnos
        '
        Me.dgvTurnos.AllowUserToAddRows = False
        Me.dgvTurnos.AllowUserToDeleteRows = False
        Me.dgvTurnos.AllowUserToResizeRows = False
        Me.dgvTurnos.AutoGenerateColumns = False
        Me.dgvTurnos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvTurnos.BackgroundColor = System.Drawing.Color.White
        Me.dgvTurnos.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvTurnos.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.dgvTurnos.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.DarkBlue
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.DarkBlue
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White
        Me.dgvTurnos.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvTurnos.ColumnHeadersHeight = 34
        Me.dgvTurnos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightSteelBlue
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.DarkBlue
        Me.dgvTurnos.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgvTurnos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvTurnos.EnableHeadersVisualStyles = False
        Me.dgvTurnos.GridColor = System.Drawing.Color.Gainsboro
        Me.dgvTurnos.Location = New System.Drawing.Point(0, 212)
        Me.dgvTurnos.MultiSelect = False
        Me.dgvTurnos.Name = "dgvTurnos"
        Me.dgvTurnos.ReadOnly = True
        Me.dgvTurnos.RowHeadersVisible = False
        Me.dgvTurnos.RowTemplate.Height = 30
        Me.dgvTurnos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvTurnos.Size = New System.Drawing.Size(850, 392)
        Me.dgvTurnos.TabIndex = 0
        '
        'lblSinDatos
        '
        Me.lblSinDatos.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblSinDatos.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSinDatos.Location = New System.Drawing.Point(0, 70)
        Me.lblSinDatos.Name = "lblSinDatos"
        Me.lblSinDatos.Size = New System.Drawing.Size(850, 60)
        Me.lblSinDatos.TabIndex = 0
        Me.lblSinDatos.Text = "No hay turnos con estos filtros."
        Me.lblSinDatos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblSinDatos.Visible = False
        Me.dgvTurnos.Controls.Add(Me.lblSinDatos)
        '
        'FormVerTurnos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCerrar
        Me.ClientSize = New System.Drawing.Size(1180, 660)
        Me.Controls.Add(Me.dgvTurnos)
        Me.Controls.Add(Me.pnlDetalle)
        Me.Controls.Add(Me.pnlPie)
        Me.Controls.Add(Me.tlpResumen)
        Me.Controls.Add(Me.pnlFiltros)
        Me.Controls.Add(Me.pnlEncabezado)
        Me.KeyPreview = True
        Me.MinimumSize = New System.Drawing.Size(980, 560)
        Me.Name = "FormVerTurnos"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Turnos de caja"
        Me.pnlEncabezado.ResumeLayout(False)
        Me.pnlEncabezado.PerformLayout()
        Me.pnlFiltros.ResumeLayout(False)
        Me.pnlFiltros.PerformLayout()
        Me.tlpResumen.ResumeLayout(False)
        Me.pnlCardAbiertos.ResumeLayout(False)
        Me.pnlCardCerrados.ResumeLayout(False)
        Me.pnlCardFaltantes.ResumeLayout(False)
        Me.pnlCardNeta.ResumeLayout(False)
        Me.pnlPie.ResumeLayout(False)
        Me.pnlPie.PerformLayout()
        Me.pnlDetalle.ResumeLayout(False)
        Me.pnlDetalle.PerformLayout()
        CType(Me.dgvTurnos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.dgvTurnos.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlEncabezado As Panel
    Friend WithEvents lblTitulo As Label
    Friend WithEvents lblSubtitulo As Label
    Friend WithEvents pnlFiltros As Panel
    Friend WithEvents lblSucursal As Label
    Friend WithEvents cboSucursal As ComboBox
    Friend WithEvents lblEstadoFiltro As Label
    Friend WithEvents cboEstado As ComboBox
    Friend WithEvents lblDesde As Label
    Friend WithEvents dtpDesde As DateTimePicker
    Friend WithEvents lblHasta As Label
    Friend WithEvents dtpHasta As DateTimePicker
    Friend WithEvents btnActualizar As Button
    Friend WithEvents lblNotaFiltro As Label
    Friend WithEvents tlpResumen As TableLayoutPanel
    Friend WithEvents pnlCardAbiertos As Panel
    Friend WithEvents lblAbiertosTitulo As Label
    Friend WithEvents lblAbiertos As Label
    Friend WithEvents pnlCardCerrados As Panel
    Friend WithEvents lblCerradosTitulo As Label
    Friend WithEvents lblCerrados As Label
    Friend WithEvents pnlCardFaltantes As Panel
    Friend WithEvents lblFaltantesTitulo As Label
    Friend WithEvents lblFaltantes As Label
    Friend WithEvents pnlCardNeta As Panel
    Friend WithEvents lblDiferenciaNetaTitulo As Label
    Friend WithEvents lblDiferenciaNeta As Label
    Friend WithEvents pnlPie As Panel
    Friend WithEvents lblTotalTurnos As Label
    Friend WithEvents btnImprimir As Button
    Friend WithEvents btnCerrar As Button
    Friend WithEvents pnlDetalle As Panel
    Friend WithEvents lblDetalleTitulo As Label
    Friend WithEvents lblDetalleEstado As Label
    Friend WithEvents tlpDetalle As TableLayoutPanel
    Friend WithEvents lblDetalleNota As Label
    Friend WithEvents dgvTurnos As DataGridView
    Friend WithEvents lblSinDatos As Label
End Class
