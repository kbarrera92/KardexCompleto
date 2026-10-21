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
        Me.lblTurno = New System.Windows.Forms.Label()
        Me.dgvConteo = New System.Windows.Forms.DataGridView()
        Me.colDenominacion = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCantidad = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colSubtotal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lblTotalArqueoTitulo = New System.Windows.Forms.Label()
        Me.lblTotalArqueo = New System.Windows.Forms.Label()
        Me.lblSobranteTitulo = New System.Windows.Forms.Label()
        Me.lblInicialTitulo = New System.Windows.Forms.Label()
        Me.lblInicial = New System.Windows.Forms.Label()
        Me.lblEfectivoCajaTitulo = New System.Windows.Forms.Label()
        Me.lblEfectivoCaja = New System.Windows.Forms.Label()
        Me.lblSobrante = New System.Windows.Forms.Label()
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
        Me.lblTotalEfectivoTitulo = New System.Windows.Forms.Label()
        Me.lblTotalEfectivo = New System.Windows.Forms.Label()
        Me.lblDiferenciaTitulo = New System.Windows.Forms.Label()
        Me.lblDiferencia = New System.Windows.Forms.Label()
        Me.btnGuardar = New System.Windows.Forms.Button()
        Me.btnCerrar = New System.Windows.Forms.Button()
        CType(Me.dgvConteo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblTurno
        '
        Me.lblTurno.AutoSize = True
        Me.lblTurno.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTurno.Location = New System.Drawing.Point(12, 12)
        Me.lblTurno.Name = "lblTurno"
        Me.lblTurno.Size = New System.Drawing.Size(60, 20)
        Me.lblTurno.TabIndex = 0
        Me.lblTurno.Text = "Turno:"
        '
        'dgvConteo
        '
        Me.dgvConteo.AllowUserToAddRows = False
        Me.dgvConteo.AllowUserToDeleteRows = False
        Me.dgvConteo.AllowUserToResizeColumns = False
        Me.dgvConteo.AllowUserToResizeRows = False
        Me.dgvConteo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvConteo.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colDenominacion, Me.colCantidad, Me.colSubtotal})
        Me.dgvConteo.Location = New System.Drawing.Point(16, 45)
        Me.dgvConteo.MultiSelect = False
        Me.dgvConteo.Name = "dgvConteo"
        Me.dgvConteo.RowHeadersVisible = False
        Me.dgvConteo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgvConteo.Size = New System.Drawing.Size(340, 298)
        Me.dgvConteo.TabIndex = 1
        '
        'colDenominacion
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle1.Format = "0.00"
        Me.colDenominacion.DefaultCellStyle = DataGridViewCellStyle1
        Me.colDenominacion.HeaderText = "Arqueo caja"
        Me.colDenominacion.Name = "colDenominacion"
        Me.colDenominacion.ReadOnly = True
        Me.colDenominacion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'colCantidad
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.colCantidad.DefaultCellStyle = DataGridViewCellStyle2
        Me.colCantidad.HeaderText = "Cantidad"
        Me.colCantidad.Name = "colCantidad"
        Me.colCantidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'colSubtotal
        '
        Me.colSubtotal.DefaultCellStyle = DataGridViewCellStyle1
        Me.colSubtotal.HeaderText = "Subtotal"
        Me.colSubtotal.Name = "colSubtotal"
        Me.colSubtotal.ReadOnly = True
        Me.colSubtotal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colSubtotal.Width = 120
        '
        'lblTotalArqueoTitulo
        '
        Me.lblTotalArqueoTitulo.AutoSize = True
        Me.lblTotalArqueoTitulo.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalArqueoTitulo.Location = New System.Drawing.Point(16, 360)
        Me.lblTotalArqueoTitulo.Name = "lblTotalArqueoTitulo"
        Me.lblTotalArqueoTitulo.Size = New System.Drawing.Size(64, 20)
        Me.lblTotalArqueoTitulo.TabIndex = 2
        Me.lblTotalArqueoTitulo.Text = "TOTAL"
        '
        'lblTotalArqueo
        '
        Me.lblTotalArqueo.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalArqueo.Location = New System.Drawing.Point(196, 360)
        Me.lblTotalArqueo.Name = "lblTotalArqueo"
        Me.lblTotalArqueo.Size = New System.Drawing.Size(160, 20)
        Me.lblTotalArqueo.TabIndex = 3
        Me.lblTotalArqueo.Text = "0.00"
        Me.lblTotalArqueo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblSobranteTitulo
        '
        Me.lblSobranteTitulo.AutoSize = True
        Me.lblSobranteTitulo.Location = New System.Drawing.Point(390, 45)
        Me.lblSobranteTitulo.Name = "lblSobranteTitulo"
        Me.lblSobranteTitulo.Size = New System.Drawing.Size(59, 13)
        Me.lblSobranteTitulo.TabIndex = 4
        Me.lblSobranteTitulo.Text = "Sobrante +"
        '
        'lblInicialTitulo
        '
        Me.lblInicialTitulo.AutoSize = True
        Me.lblInicialTitulo.Location = New System.Drawing.Point(390, 75)
        Me.lblInicialTitulo.Name = "lblInicialTitulo"
        Me.lblInicialTitulo.Size = New System.Drawing.Size(84, 13)
        Me.lblInicialTitulo.TabIndex = 22
        Me.lblInicialTitulo.Text = "Efectivo inicial +"
        '
        'lblInicial
        '
        Me.lblInicial.Location = New System.Drawing.Point(530, 75)
        Me.lblInicial.Name = "lblInicial"
        Me.lblInicial.Size = New System.Drawing.Size(130, 13)
        Me.lblInicial.TabIndex = 23
        Me.lblInicial.Text = "0.00"
        Me.lblInicial.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblEfectivoCajaTitulo
        '
        Me.lblEfectivoCajaTitulo.AutoSize = True
        Me.lblEfectivoCajaTitulo.Location = New System.Drawing.Point(390, 295)
        Me.lblEfectivoCajaTitulo.Name = "lblEfectivoCajaTitulo"
        Me.lblEfectivoCajaTitulo.Size = New System.Drawing.Size(141, 13)
        Me.lblEfectivoCajaTitulo.TabIndex = 24
        Me.lblEfectivoCajaTitulo.Text = "Efectivo en caja (con fondo)"
        '
        'lblEfectivoCaja
        '
        Me.lblEfectivoCaja.Location = New System.Drawing.Point(530, 295)
        Me.lblEfectivoCaja.Name = "lblEfectivoCaja"
        Me.lblEfectivoCaja.Size = New System.Drawing.Size(130, 13)
        Me.lblEfectivoCaja.TabIndex = 25
        Me.lblEfectivoCaja.Text = "0.00"
        Me.lblEfectivoCaja.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblSobrante
        '
        Me.lblSobrante.Location = New System.Drawing.Point(530, 45)
        Me.lblSobrante.Name = "lblSobrante"
        Me.lblSobrante.Size = New System.Drawing.Size(130, 13)
        Me.lblSobrante.TabIndex = 5
        Me.lblSobrante.Text = "0.00"
        Me.lblSobrante.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblVentaTitulo
        '
        Me.lblVentaTitulo.AutoSize = True
        Me.lblVentaTitulo.Location = New System.Drawing.Point(390, 105)
        Me.lblVentaTitulo.Name = "lblVentaTitulo"
        Me.lblVentaTitulo.Size = New System.Drawing.Size(70, 13)
        Me.lblVentaTitulo.TabIndex = 6
        Me.lblVentaTitulo.Text = "Total venta +"
        '
        'lblVenta
        '
        Me.lblVenta.Location = New System.Drawing.Point(530, 105)
        Me.lblVenta.Name = "lblVenta"
        Me.lblVenta.Size = New System.Drawing.Size(130, 13)
        Me.lblVenta.TabIndex = 7
        Me.lblVenta.Text = "0.00"
        Me.lblVenta.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTarjetaTitulo
        '
        Me.lblTarjetaTitulo.AutoSize = True
        Me.lblTarjetaTitulo.Location = New System.Drawing.Point(390, 135)
        Me.lblTarjetaTitulo.Name = "lblTarjetaTitulo"
        Me.lblTarjetaTitulo.Size = New System.Drawing.Size(66, 13)
        Me.lblTarjetaTitulo.TabIndex = 8
        Me.lblTarjetaTitulo.Text = "Venta POS -"
        '
        'lblTarjeta
        '
        Me.lblTarjeta.Location = New System.Drawing.Point(530, 135)
        Me.lblTarjeta.Name = "lblTarjeta"
        Me.lblTarjeta.Size = New System.Drawing.Size(130, 13)
        Me.lblTarjeta.TabIndex = 9
        Me.lblTarjeta.Text = "0.00"
        Me.lblTarjeta.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblHeladosTitulo
        '
        Me.lblHeladosTitulo.AutoSize = True
        Me.lblHeladosTitulo.Location = New System.Drawing.Point(390, 165)
        Me.lblHeladosTitulo.Name = "lblHeladosTitulo"
        Me.lblHeladosTitulo.Size = New System.Drawing.Size(52, 13)
        Me.lblHeladosTitulo.TabIndex = 10
        Me.lblHeladosTitulo.Text = "Helados -"
        '
        'lblHelados
        '
        Me.lblHelados.Location = New System.Drawing.Point(530, 165)
        Me.lblHelados.Name = "lblHelados"
        Me.lblHelados.Size = New System.Drawing.Size(130, 13)
        Me.lblHelados.TabIndex = 11
        Me.lblHelados.Text = "0.00"
        Me.lblHelados.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblBebidasTitulo
        '
        Me.lblBebidasTitulo.AutoSize = True
        Me.lblBebidasTitulo.Location = New System.Drawing.Point(390, 195)
        Me.lblBebidasTitulo.Name = "lblBebidasTitulo"
        Me.lblBebidasTitulo.Size = New System.Drawing.Size(51, 13)
        Me.lblBebidasTitulo.TabIndex = 12
        Me.lblBebidasTitulo.Text = "Bebidas -"
        '
        'lblBebidas
        '
        Me.lblBebidas.Location = New System.Drawing.Point(530, 195)
        Me.lblBebidas.Name = "lblBebidas"
        Me.lblBebidas.Size = New System.Drawing.Size(130, 13)
        Me.lblBebidas.TabIndex = 13
        Me.lblBebidas.Text = "0.00"
        Me.lblBebidas.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblEgresosTitulo
        '
        Me.lblEgresosTitulo.AutoSize = True
        Me.lblEgresosTitulo.Location = New System.Drawing.Point(390, 225)
        Me.lblEgresosTitulo.Name = "lblEgresosTitulo"
        Me.lblEgresosTitulo.Size = New System.Drawing.Size(51, 13)
        Me.lblEgresosTitulo.TabIndex = 14
        Me.lblEgresosTitulo.Text = "Egresos -"
        '
        'lblEgresos
        '
        Me.lblEgresos.Location = New System.Drawing.Point(530, 225)
        Me.lblEgresos.Name = "lblEgresos"
        Me.lblEgresos.Size = New System.Drawing.Size(130, 13)
        Me.lblEgresos.TabIndex = 15
        Me.lblEgresos.Text = "0.00"
        Me.lblEgresos.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotalEfectivoTitulo
        '
        Me.lblTotalEfectivoTitulo.AutoSize = True
        Me.lblTotalEfectivoTitulo.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalEfectivoTitulo.Location = New System.Drawing.Point(390, 262)
        Me.lblTotalEfectivoTitulo.Name = "lblTotalEfectivoTitulo"
        Me.lblTotalEfectivoTitulo.Size = New System.Drawing.Size(117, 20)
        Me.lblTotalEfectivoTitulo.TabIndex = 16
        Me.lblTotalEfectivoTitulo.Text = "Total efectivo"
        '
        'lblTotalEfectivo
        '
        Me.lblTotalEfectivo.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalEfectivo.Location = New System.Drawing.Point(520, 262)
        Me.lblTotalEfectivo.Name = "lblTotalEfectivo"
        Me.lblTotalEfectivo.Size = New System.Drawing.Size(140, 20)
        Me.lblTotalEfectivo.TabIndex = 17
        Me.lblTotalEfectivo.Text = "0.00"
        Me.lblTotalEfectivo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDiferenciaTitulo
        '
        Me.lblDiferenciaTitulo.AutoSize = True
        Me.lblDiferenciaTitulo.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDiferenciaTitulo.Location = New System.Drawing.Point(390, 365)
        Me.lblDiferenciaTitulo.Name = "lblDiferenciaTitulo"
        Me.lblDiferenciaTitulo.Size = New System.Drawing.Size(91, 20)
        Me.lblDiferenciaTitulo.TabIndex = 18
        Me.lblDiferenciaTitulo.Text = "Diferencia"
        '
        'lblDiferencia
        '
        Me.lblDiferencia.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDiferencia.Location = New System.Drawing.Point(520, 365)
        Me.lblDiferencia.Name = "lblDiferencia"
        Me.lblDiferencia.Size = New System.Drawing.Size(140, 20)
        Me.lblDiferencia.TabIndex = 19
        Me.lblDiferencia.Text = "0.00"
        Me.lblDiferencia.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnGuardar
        '
        Me.btnGuardar.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGuardar.Location = New System.Drawing.Point(390, 408)
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.Size = New System.Drawing.Size(180, 42)
        Me.btnGuardar.TabIndex = 20
        Me.btnGuardar.Text = "Guardar e imprimir"
        Me.btnGuardar.UseVisualStyleBackColor = True
        '
        'btnCerrar
        '
        Me.btnCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCerrar.Location = New System.Drawing.Point(580, 408)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(80, 42)
        Me.btnCerrar.TabIndex = 21
        Me.btnCerrar.Text = "Cerrar"
        Me.btnCerrar.UseVisualStyleBackColor = True
        '
        'FormArqueoCaja
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCerrar
        Me.ClientSize = New System.Drawing.Size(680, 465)
        Me.Controls.Add(Me.btnCerrar)
        Me.Controls.Add(Me.btnGuardar)
        Me.Controls.Add(Me.lblEfectivoCaja)
        Me.Controls.Add(Me.lblEfectivoCajaTitulo)
        Me.Controls.Add(Me.lblInicial)
        Me.Controls.Add(Me.lblInicialTitulo)
        Me.Controls.Add(Me.lblDiferencia)
        Me.Controls.Add(Me.lblDiferenciaTitulo)
        Me.Controls.Add(Me.lblTotalEfectivo)
        Me.Controls.Add(Me.lblTotalEfectivoTitulo)
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
        Me.Controls.Add(Me.lblSobrante)
        Me.Controls.Add(Me.lblSobranteTitulo)
        Me.Controls.Add(Me.lblTotalArqueo)
        Me.Controls.Add(Me.lblTotalArqueoTitulo)
        Me.Controls.Add(Me.dgvConteo)
        Me.Controls.Add(Me.lblTurno)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormArqueoCaja"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Arqueo de caja"
        CType(Me.dgvConteo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblTurno As Label
    Friend WithEvents dgvConteo As DataGridView
    Friend WithEvents colDenominacion As DataGridViewTextBoxColumn
    Friend WithEvents colCantidad As DataGridViewTextBoxColumn
    Friend WithEvents colSubtotal As DataGridViewTextBoxColumn
    Friend WithEvents lblTotalArqueoTitulo As Label
    Friend WithEvents lblTotalArqueo As Label
    Friend WithEvents lblSobranteTitulo As Label
    Friend WithEvents lblInicialTitulo As Label
    Friend WithEvents lblInicial As Label
    Friend WithEvents lblEfectivoCajaTitulo As Label
    Friend WithEvents lblEfectivoCaja As Label
    Friend WithEvents lblSobrante As Label
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
    Friend WithEvents lblTotalEfectivoTitulo As Label
    Friend WithEvents lblTotalEfectivo As Label
    Friend WithEvents lblDiferenciaTitulo As Label
    Friend WithEvents lblDiferencia As Label
    Friend WithEvents btnGuardar As Button
    Friend WithEvents btnCerrar As Button
End Class
