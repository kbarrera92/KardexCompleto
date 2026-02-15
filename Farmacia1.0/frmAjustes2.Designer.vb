<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAjustes2
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.ComboBoxSucursal = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ComboBoxTipoAjuste = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.mskfecha = New System.Windows.Forms.MaskedTextBox()
        Me.lblNoCompra = New System.Windows.Forms.Label()
        Me.LineShape1 = New Microsoft.VisualBasic.PowerPacks.LineShape()
        Me.ShapeContainer1 = New Microsoft.VisualBasic.PowerPacks.ShapeContainer()
        Me.LineShape2 = New Microsoft.VisualBasic.PowerPacks.LineShape()
        Me.txtconcep = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.btnsalir = New System.Windows.Forms.Button()
        Me.btnnuevaventa = New System.Windows.Forms.Button()
        Me.btnagregard = New System.Windows.Forms.Button()
        Me.btneliminard = New System.Windows.Forms.Button()
        Me.btnregistrarc = New System.Windows.Forms.Button()
        Me.cmbProveedor = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtdescpro = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtcodpro = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtcantidad = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtexistencia = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtprecio = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.codpro = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dpro = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.exist = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.marca = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.pres = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.preciopro = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.prov = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.medida = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.txtbuscapro = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txttotal = New System.Windows.Forms.TextBox()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.no = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.descPro = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cant = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.precio = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.subt = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.txtbarcode = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ComboBoxSucursal
        '
        Me.ComboBoxSucursal.DisplayMember = "nombreSuc"
        Me.ComboBoxSucursal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxSucursal.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxSucursal.FormattingEnabled = True
        Me.ComboBoxSucursal.Location = New System.Drawing.Point(19, 28)
        Me.ComboBoxSucursal.Name = "ComboBoxSucursal"
        Me.ComboBoxSucursal.Size = New System.Drawing.Size(284, 28)
        Me.ComboBoxSucursal.TabIndex = 108
        Me.ComboBoxSucursal.ValueMember = "idSucursal"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(15, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(79, 20)
        Me.Label1.TabIndex = 107
        Me.Label1.Text = "Sucursal"
        '
        'ComboBoxTipoAjuste
        '
        Me.ComboBoxTipoAjuste.DisplayMember = "tipoAjuste"
        Me.ComboBoxTipoAjuste.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxTipoAjuste.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxTipoAjuste.FormattingEnabled = True
        Me.ComboBoxTipoAjuste.Location = New System.Drawing.Point(309, 28)
        Me.ComboBoxTipoAjuste.Name = "ComboBoxTipoAjuste"
        Me.ComboBoxTipoAjuste.Size = New System.Drawing.Size(171, 28)
        Me.ComboBoxTipoAjuste.TabIndex = 106
        Me.ComboBoxTipoAjuste.ValueMember = "idTipoAjuste"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(305, 5)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(122, 20)
        Me.Label7.TabIndex = 105
        Me.Label7.Text = "Tipo de ajuste"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(482, 5)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(64, 20)
        Me.Label3.TabIndex = 104
        Me.Label3.Text = "Fecha:"
        '
        'mskfecha
        '
        Me.mskfecha.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mskfecha.Location = New System.Drawing.Point(486, 28)
        Me.mskfecha.Mask = "00/00/0000"
        Me.mskfecha.Name = "mskfecha"
        Me.mskfecha.Size = New System.Drawing.Size(122, 26)
        Me.mskfecha.TabIndex = 103
        Me.mskfecha.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.mskfecha.ValidatingType = GetType(Date)
        '
        'lblNoCompra
        '
        Me.lblNoCompra.AutoSize = True
        Me.lblNoCompra.Font = New System.Drawing.Font("Lucida Handwriting", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNoCompra.ForeColor = System.Drawing.SystemColors.MenuHighlight
        Me.lblNoCompra.Location = New System.Drawing.Point(645, 13)
        Me.lblNoCompra.Name = "lblNoCompra"
        Me.lblNoCompra.Size = New System.Drawing.Size(192, 41)
        Me.lblNoCompra.TabIndex = 102
        Me.lblNoCompra.Text = "Ajuste No."
        '
        'LineShape1
        '
        Me.LineShape1.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.LineShape1.BorderWidth = 3
        Me.LineShape1.Name = "LineShape1"
        Me.LineShape1.X1 = 19
        Me.LineShape1.X2 = 1212
        Me.LineShape1.Y1 = 121
        Me.LineShape1.Y2 = 121
        '
        'ShapeContainer1
        '
        Me.ShapeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.ShapeContainer1.Margin = New System.Windows.Forms.Padding(0)
        Me.ShapeContainer1.Name = "ShapeContainer1"
        Me.ShapeContainer1.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {Me.LineShape2, Me.LineShape1})
        Me.ShapeContainer1.Size = New System.Drawing.Size(1284, 676)
        Me.ShapeContainer1.TabIndex = 109
        Me.ShapeContainer1.TabStop = False
        '
        'LineShape2
        '
        Me.LineShape2.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.LineShape2.BorderWidth = 3
        Me.LineShape2.Name = "LineShape2"
        Me.LineShape2.X1 = 13
        Me.LineShape2.X2 = 1245
        Me.LineShape2.Y1 = 559
        Me.LineShape2.Y2 = 559
        '
        'txtconcep
        '
        Me.txtconcep.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtconcep.Location = New System.Drawing.Point(18, 82)
        Me.txtconcep.Name = "txtconcep"
        Me.txtconcep.Size = New System.Drawing.Size(595, 26)
        Me.txtconcep.TabIndex = 111
        Me.txtconcep.Tag = "ES"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(15, 59)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(86, 20)
        Me.Label8.TabIndex = 110
        Me.Label8.Text = "Concepto"
        '
        'btnsalir
        '
        Me.btnsalir.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnsalir.FlatAppearance.BorderSize = 2
        Me.btnsalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnsalir.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsalir.ForeColor = System.Drawing.Color.Blue
        Me.btnsalir.Location = New System.Drawing.Point(1122, 79)
        Me.btnsalir.Name = "btnsalir"
        Me.btnsalir.Size = New System.Drawing.Size(90, 34)
        Me.btnsalir.TabIndex = 116
        Me.btnsalir.Tag = "WB"
        Me.btnsalir.Text = "Salir"
        Me.btnsalir.UseVisualStyleBackColor = True
        '
        'btnnuevaventa
        '
        Me.btnnuevaventa.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnnuevaventa.FlatAppearance.BorderSize = 2
        Me.btnnuevaventa.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnnuevaventa.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnnuevaventa.Location = New System.Drawing.Point(637, 79)
        Me.btnnuevaventa.Name = "btnnuevaventa"
        Me.btnnuevaventa.Size = New System.Drawing.Size(90, 34)
        Me.btnnuevaventa.TabIndex = 112
        Me.btnnuevaventa.Tag = "WB"
        Me.btnnuevaventa.Text = "Nuevo"
        Me.btnnuevaventa.UseVisualStyleBackColor = True
        '
        'btnagregard
        '
        Me.btnagregard.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnagregard.FlatAppearance.BorderSize = 2
        Me.btnagregard.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnagregard.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnagregard.Location = New System.Drawing.Point(733, 79)
        Me.btnagregard.Name = "btnagregard"
        Me.btnagregard.Size = New System.Drawing.Size(90, 34)
        Me.btnagregard.TabIndex = 112
        Me.btnagregard.Tag = "WB"
        Me.btnagregard.Text = "Reg. Det"
        Me.btnagregard.UseVisualStyleBackColor = True
        '
        'btneliminard
        '
        Me.btneliminard.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btneliminard.FlatAppearance.BorderSize = 2
        Me.btneliminard.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btneliminard.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btneliminard.Location = New System.Drawing.Point(829, 79)
        Me.btneliminard.Name = "btneliminard"
        Me.btneliminard.Size = New System.Drawing.Size(90, 34)
        Me.btneliminard.TabIndex = 112
        Me.btneliminard.Tag = "WB"
        Me.btneliminard.Text = "Elim. Det"
        Me.btneliminard.UseVisualStyleBackColor = True
        '
        'btnregistrarc
        '
        Me.btnregistrarc.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnregistrarc.FlatAppearance.BorderSize = 2
        Me.btnregistrarc.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnregistrarc.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnregistrarc.Location = New System.Drawing.Point(925, 79)
        Me.btnregistrarc.Name = "btnregistrarc"
        Me.btnregistrarc.Size = New System.Drawing.Size(90, 34)
        Me.btnregistrarc.TabIndex = 112
        Me.btnregistrarc.Tag = "WB"
        Me.btnregistrarc.Text = "Registrar"
        Me.btnregistrarc.UseVisualStyleBackColor = True
        '
        'cmbProveedor
        '
        Me.cmbProveedor.DisplayMember = "rzProveedor"
        Me.cmbProveedor.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbProveedor.FormattingEnabled = True
        Me.cmbProveedor.Location = New System.Drawing.Point(12, 640)
        Me.cmbProveedor.Name = "cmbProveedor"
        Me.cmbProveedor.Size = New System.Drawing.Size(254, 28)
        Me.cmbProveedor.TabIndex = 122
        Me.cmbProveedor.ValueMember = "idProveedor"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(6, 616)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(90, 20)
        Me.Label6.TabIndex = 121
        Me.Label6.Text = "Proveedor"
        '
        'txtdescpro
        '
        Me.txtdescpro.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdescpro.Location = New System.Drawing.Point(107, 587)
        Me.txtdescpro.Name = "txtdescpro"
        Me.txtdescpro.Size = New System.Drawing.Size(472, 26)
        Me.txtdescpro.TabIndex = 120
        Me.txtdescpro.Tag = "ES"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(104, 564)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(132, 20)
        Me.Label5.TabIndex = 119
        Me.Label5.Text = "Desc. Producto"
        '
        'txtcodpro
        '
        Me.txtcodpro.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcodpro.Location = New System.Drawing.Point(12, 587)
        Me.txtcodpro.Name = "txtcodpro"
        Me.txtcodpro.Size = New System.Drawing.Size(89, 26)
        Me.txtcodpro.TabIndex = 118
        Me.txtcodpro.Tag = "ES"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(9, 564)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(65, 20)
        Me.Label4.TabIndex = 117
        Me.Label4.Text = "Código"
        '
        'txtcantidad
        '
        Me.txtcantidad.BackColor = System.Drawing.Color.Blue
        Me.txtcantidad.Font = New System.Drawing.Font("Microsoft Sans Serif", 35.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcantidad.ForeColor = System.Drawing.Color.Yellow
        Me.txtcantidad.Location = New System.Drawing.Point(1118, 608)
        Me.txtcantidad.Name = "txtcantidad"
        Me.txtcantidad.Size = New System.Drawing.Size(138, 60)
        Me.txtcantidad.TabIndex = 128
        Me.txtcantidad.Tag = "E"
        Me.txtcantidad.Text = "0"
        Me.txtcantidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(1115, 588)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(86, 20)
        Me.Label19.TabIndex = 127
        Me.Label19.Text = "Cantidad:"
        '
        'txtexistencia
        '
        Me.txtexistencia.BackColor = System.Drawing.Color.Blue
        Me.txtexistencia.Font = New System.Drawing.Font("Microsoft Sans Serif", 35.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtexistencia.ForeColor = System.Drawing.Color.Yellow
        Me.txtexistencia.Location = New System.Drawing.Point(959, 608)
        Me.txtexistencia.Name = "txtexistencia"
        Me.txtexistencia.Size = New System.Drawing.Size(138, 60)
        Me.txtexistencia.TabIndex = 126
        Me.txtexistencia.Tag = "E"
        Me.txtexistencia.Text = "0"
        Me.txtexistencia.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(956, 588)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(96, 20)
        Me.Label16.TabIndex = 125
        Me.Label16.Text = "Existencia:"
        '
        'txtprecio
        '
        Me.txtprecio.BackColor = System.Drawing.Color.Blue
        Me.txtprecio.Font = New System.Drawing.Font("Microsoft Sans Serif", 35.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtprecio.ForeColor = System.Drawing.Color.Yellow
        Me.txtprecio.Location = New System.Drawing.Point(648, 608)
        Me.txtprecio.Name = "txtprecio"
        Me.txtprecio.Size = New System.Drawing.Size(291, 60)
        Me.txtprecio.TabIndex = 124
        Me.txtprecio.Tag = "E"
        Me.txtprecio.Text = "0.00"
        Me.txtprecio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(645, 588)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(61, 20)
        Me.Label15.TabIndex = 123
        Me.Label15.Text = "Costo:"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.codpro, Me.dpro, Me.exist, Me.marca, Me.pres, Me.preciopro, Me.prov, Me.medida})
        Me.DataGridView1.Location = New System.Drawing.Point(16, 188)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(597, 361)
        Me.DataGridView1.TabIndex = 131
        '
        'codpro
        '
        Me.codpro.HeaderText = "Código"
        Me.codpro.Name = "codpro"
        Me.codpro.ReadOnly = True
        Me.codpro.Width = 80
        '
        'dpro
        '
        Me.dpro.HeaderText = "Descripción"
        Me.dpro.Name = "dpro"
        Me.dpro.ReadOnly = True
        Me.dpro.Width = 300
        '
        'exist
        '
        Me.exist.HeaderText = "Existencia"
        Me.exist.Name = "exist"
        Me.exist.ReadOnly = True
        Me.exist.Width = 90
        '
        'marca
        '
        Me.marca.HeaderText = "Marca"
        Me.marca.Name = "marca"
        Me.marca.ReadOnly = True
        '
        'pres
        '
        Me.pres.HeaderText = "Presentación"
        Me.pres.Name = "pres"
        Me.pres.ReadOnly = True
        Me.pres.Width = 200
        '
        'preciopro
        '
        Me.preciopro.HeaderText = "Costo"
        Me.preciopro.Name = "preciopro"
        Me.preciopro.ReadOnly = True
        '
        'prov
        '
        Me.prov.HeaderText = "Proveedor"
        Me.prov.Name = "prov"
        Me.prov.ReadOnly = True
        '
        'medida
        '
        Me.medida.HeaderText = "Medida"
        Me.medida.Name = "medida"
        Me.medida.ReadOnly = True
        '
        'txtbuscapro
        '
        Me.txtbuscapro.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbuscapro.Location = New System.Drawing.Point(17, 156)
        Me.txtbuscapro.Name = "txtbuscapro"
        Me.txtbuscapro.Size = New System.Drawing.Size(597, 26)
        Me.txtbuscapro.TabIndex = 130
        Me.txtbuscapro.Tag = "ES"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(13, 132)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(146, 20)
        Me.Label2.TabIndex = 129
        Me.Label2.Text = "Buscar producto:"
        '
        'txttotal
        '
        Me.txttotal.BackColor = System.Drawing.Color.Blue
        Me.txttotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 30.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txttotal.ForeColor = System.Drawing.Color.Yellow
        Me.txttotal.Location = New System.Drawing.Point(1042, 497)
        Me.txttotal.Name = "txttotal"
        Me.txttotal.Size = New System.Drawing.Size(204, 53)
        Me.txttotal.TabIndex = 133
        Me.txttotal.Tag = "E"
        Me.txttotal.Text = "0.00"
        Me.txttotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'DataGridView2
        '
        Me.DataGridView2.AllowUserToAddRows = False
        Me.DataGridView2.AllowUserToDeleteRows = False
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView2.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.no, Me.DataGridViewTextBoxColumn1, Me.descPro, Me.cant, Me.precio, Me.subt})
        Me.DataGridView2.Location = New System.Drawing.Point(620, 188)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.ReadOnly = True
        Me.DataGridView2.Size = New System.Drawing.Size(626, 309)
        Me.DataGridView2.TabIndex = 132
        '
        'no
        '
        Me.no.HeaderText = "No"
        Me.no.Name = "no"
        Me.no.ReadOnly = True
        Me.no.Width = 50
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "Código"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Width = 80
        '
        'descPro
        '
        Me.descPro.HeaderText = "Descripción"
        Me.descPro.Name = "descPro"
        Me.descPro.ReadOnly = True
        Me.descPro.Width = 230
        '
        'cant
        '
        Me.cant.HeaderText = "Cant."
        Me.cant.Name = "cant"
        Me.cant.ReadOnly = True
        Me.cant.Width = 70
        '
        'precio
        '
        Me.precio.HeaderText = "Precio"
        Me.precio.Name = "precio"
        Me.precio.ReadOnly = True
        Me.precio.Width = 80
        '
        'subt
        '
        Me.subt.HeaderText = "Importe"
        Me.subt.Name = "subt"
        Me.subt.ReadOnly = True
        '
        'Button1
        '
        Me.Button1.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.Button1.FlatAppearance.BorderSize = 2
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(1021, 79)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(95, 34)
        Me.Button1.TabIndex = 112
        Me.Button1.Tag = "WB"
        Me.Button1.Text = "Descartar"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel1.Location = New System.Drawing.Point(1068, 34)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(144, 17)
        Me.LinkLabel1.TabIndex = 134
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Ver listado de ajustes"
        '
        'txtbarcode
        '
        Me.txtbarcode.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbarcode.Location = New System.Drawing.Point(272, 640)
        Me.txtbarcode.Name = "txtbarcode"
        Me.txtbarcode.Size = New System.Drawing.Size(274, 26)
        Me.txtbarcode.TabIndex = 136
        Me.txtbarcode.Tag = "ES"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(269, 617)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(76, 20)
        Me.Label9.TabIndex = 135
        Me.Label9.Text = "Barcode"
        '
        'Button2
        '
        Me.Button2.Image = Global.Farmacia1._0.My.Resources.Resources.Save_icon__2_
        Me.Button2.Location = New System.Drawing.Point(547, 639)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(32, 28)
        Me.Button2.TabIndex = 137
        Me.Button2.Tag = "WB"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'frmAjustes2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1284, 676)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.txtbarcode)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.txttotal)
        Me.Controls.Add(Me.DataGridView2)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.txtbuscapro)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtcantidad)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.txtexistencia)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.txtprecio)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.cmbProveedor)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtdescpro)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtcodpro)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.btnregistrarc)
        Me.Controls.Add(Me.btneliminard)
        Me.Controls.Add(Me.btnagregard)
        Me.Controls.Add(Me.btnsalir)
        Me.Controls.Add(Me.btnnuevaventa)
        Me.Controls.Add(Me.txtconcep)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.ComboBoxSucursal)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ComboBoxTipoAjuste)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.mskfecha)
        Me.Controls.Add(Me.lblNoCompra)
        Me.Controls.Add(Me.ShapeContainer1)
        Me.Name = "frmAjustes2"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Ajustes de inventario por sucursal"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ComboBoxSucursal As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxTipoAjuste As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents mskfecha As System.Windows.Forms.MaskedTextBox
    Friend WithEvents lblNoCompra As System.Windows.Forms.Label
    Friend WithEvents txtconcep As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents btnsalir As System.Windows.Forms.Button
    Friend WithEvents btnnuevaventa As System.Windows.Forms.Button
    Friend WithEvents btnagregard As System.Windows.Forms.Button
    Friend WithEvents btneliminard As System.Windows.Forms.Button
    Friend WithEvents btnregistrarc As System.Windows.Forms.Button
    Friend WithEvents cmbProveedor As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtdescpro As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtcodpro As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtcantidad As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtexistencia As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtprecio As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents txtbuscapro As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txttotal As System.Windows.Forms.TextBox
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents no As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents descPro As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cant As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents precio As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents subt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Friend WithEvents codpro As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dpro As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents exist As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents marca As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents pres As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents preciopro As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents prov As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents medida As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents txtbarcode As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Button2 As Button
    Private WithEvents LineShape1 As PowerPacks.LineShape
    Private WithEvents ShapeContainer1 As PowerPacks.ShapeContainer
    Private WithEvents LineShape2 As PowerPacks.LineShape
End Class
