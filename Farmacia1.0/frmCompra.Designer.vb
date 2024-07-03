<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCompra
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
        Me.lblNoCompra = New System.Windows.Forms.Label()
        Me.ShapeContainer1 = New Microsoft.VisualBasic.PowerPacks.ShapeContainer()
        Me.LineShape1 = New Microsoft.VisualBasic.PowerPacks.LineShape()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtbuscapro = New System.Windows.Forms.TextBox()
        Me.mskfecha = New System.Windows.Forms.MaskedTextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.codpro = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dpro = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.exist = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.marca = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.pres = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.preciopro = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.prov = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.medida = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtcodpro = New System.Windows.Forms.TextBox()
        Me.txtdescpro = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtcantidad = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtexistencia = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtprecio = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.descPro = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cant = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.precio = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.subt = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnnuevaventa = New System.Windows.Forms.Button()
        Me.btnagregard = New System.Windows.Forms.Button()
        Me.btneliminard = New System.Windows.Forms.Button()
        Me.btnregistrarc = New System.Windows.Forms.Button()
        Me.btnsalir = New System.Windows.Forms.Button()
        Me.txttotal = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtFactura = New System.Windows.Forms.TextBox()
        Me.txtpres = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtmedida = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.btnDescartar = New System.Windows.Forms.Button()
        Me.txtFechaPago = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cmbProveedor = New System.Windows.Forms.ComboBox()
        Me.cmbFP = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblNoCompra
        '
        Me.lblNoCompra.AutoSize = True
        Me.lblNoCompra.Font = New System.Drawing.Font("Lucida Handwriting", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNoCompra.ForeColor = System.Drawing.SystemColors.MenuHighlight
        Me.lblNoCompra.Location = New System.Drawing.Point(13, 13)
        Me.lblNoCompra.Name = "lblNoCompra"
        Me.lblNoCompra.Size = New System.Drawing.Size(238, 41)
        Me.lblNoCompra.TabIndex = 1
        Me.lblNoCompra.Text = "Compra No. "
        '
        'ShapeContainer1
        '
        Me.ShapeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.ShapeContainer1.Margin = New System.Windows.Forms.Padding(0)
        Me.ShapeContainer1.Name = "ShapeContainer1"
        Me.ShapeContainer1.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {Me.LineShape1})
        Me.ShapeContainer1.Size = New System.Drawing.Size(1285, 676)
        Me.ShapeContainer1.TabIndex = 0
        Me.ShapeContainer1.TabStop = False
        '
        'LineShape1
        '
        Me.LineShape1.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.LineShape1.BorderWidth = 3
        Me.LineShape1.Name = "LineShape1"
        Me.LineShape1.X1 = 9
        Me.LineShape1.X2 = 1260
        Me.LineShape1.Y1 = 136
        Me.LineShape1.Y2 = 136
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(8, 139)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(146, 20)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Buscar producto:"
        '
        'txtbuscapro
        '
        Me.txtbuscapro.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbuscapro.Location = New System.Drawing.Point(12, 163)
        Me.txtbuscapro.Name = "txtbuscapro"
        Me.txtbuscapro.Size = New System.Drawing.Size(597, 26)
        Me.txtbuscapro.TabIndex = 7
        '
        'mskfecha
        '
        Me.mskfecha.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mskfecha.Location = New System.Drawing.Point(621, 25)
        Me.mskfecha.Mask = "00/00/0000"
        Me.mskfecha.Name = "mskfecha"
        Me.mskfecha.Size = New System.Drawing.Size(158, 26)
        Me.mskfecha.TabIndex = 5
        Me.mskfecha.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.mskfecha.ValidatingType = GetType(Date)
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(617, 2)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(64, 20)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Fecha:"
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
        Me.DataGridView1.Location = New System.Drawing.Point(12, 189)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.Size = New System.Drawing.Size(597, 269)
        Me.DataGridView1.TabIndex = 8
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
        Me.marca.Width = 200
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
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(9, 464)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(123, 20)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Cod. Producto"
        '
        'txtcodpro
        '
        Me.txtcodpro.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcodpro.Location = New System.Drawing.Point(12, 487)
        Me.txtcodpro.Name = "txtcodpro"
        Me.txtcodpro.Size = New System.Drawing.Size(119, 26)
        Me.txtcodpro.TabIndex = 10
        '
        'txtdescpro
        '
        Me.txtdescpro.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdescpro.Location = New System.Drawing.Point(137, 487)
        Me.txtdescpro.Name = "txtdescpro"
        Me.txtdescpro.Size = New System.Drawing.Size(472, 26)
        Me.txtdescpro.TabIndex = 12
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(134, 464)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(132, 20)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Desc. Producto"
        '
        'txtcantidad
        '
        Me.txtcantidad.BackColor = System.Drawing.Color.Blue
        Me.txtcantidad.Font = New System.Drawing.Font("Microsoft Sans Serif", 35.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcantidad.ForeColor = System.Drawing.Color.Yellow
        Me.txtcantidad.Location = New System.Drawing.Point(341, 611)
        Me.txtcantidad.Name = "txtcantidad"
        Me.txtcantidad.Size = New System.Drawing.Size(93, 60)
        Me.txtcantidad.TabIndex = 22
        Me.txtcantidad.Text = "0"
        Me.txtcantidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(338, 591)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(86, 20)
        Me.Label19.TabIndex = 21
        Me.Label19.Text = "Cantidad:"
        '
        'txtexistencia
        '
        Me.txtexistencia.BackColor = System.Drawing.Color.Blue
        Me.txtexistencia.Font = New System.Drawing.Font("Microsoft Sans Serif", 35.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtexistencia.ForeColor = System.Drawing.Color.Yellow
        Me.txtexistencia.Location = New System.Drawing.Point(229, 611)
        Me.txtexistencia.Name = "txtexistencia"
        Me.txtexistencia.Size = New System.Drawing.Size(93, 60)
        Me.txtexistencia.TabIndex = 20
        Me.txtexistencia.Text = "0"
        Me.txtexistencia.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(226, 591)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(96, 20)
        Me.Label16.TabIndex = 19
        Me.Label16.Text = "Existencia:"
        '
        'txtprecio
        '
        Me.txtprecio.BackColor = System.Drawing.Color.Blue
        Me.txtprecio.Font = New System.Drawing.Font("Microsoft Sans Serif", 35.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtprecio.ForeColor = System.Drawing.Color.Yellow
        Me.txtprecio.Location = New System.Drawing.Point(10, 611)
        Me.txtprecio.Name = "txtprecio"
        Me.txtprecio.Size = New System.Drawing.Size(205, 60)
        Me.txtprecio.TabIndex = 18
        Me.txtprecio.Text = "0.00"
        Me.txtprecio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(7, 591)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(61, 20)
        Me.Label15.TabIndex = 17
        Me.Label15.Text = "Costo:"
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
        Me.DataGridView2.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.descPro, Me.cant, Me.precio, Me.subt})
        Me.DataGridView2.Location = New System.Drawing.Point(633, 163)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.ReadOnly = True
        Me.DataGridView2.Size = New System.Drawing.Size(626, 367)
        Me.DataGridView2.TabIndex = 34
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
        Me.descPro.Width = 260
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
        'btnnuevaventa
        '
        Me.btnnuevaventa.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnnuevaventa.FlatAppearance.BorderSize = 2
        Me.btnnuevaventa.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnnuevaventa.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnnuevaventa.Location = New System.Drawing.Point(893, 21)
        Me.btnnuevaventa.Name = "btnnuevaventa"
        Me.btnnuevaventa.Size = New System.Drawing.Size(168, 31)
        Me.btnnuevaventa.TabIndex = 29
        Me.btnnuevaventa.Text = "Nueva Compra"
        Me.btnnuevaventa.UseVisualStyleBackColor = True
        '
        'btnagregard
        '
        Me.btnagregard.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnagregard.FlatAppearance.BorderSize = 2
        Me.btnagregard.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnagregard.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnagregard.Location = New System.Drawing.Point(730, 593)
        Me.btnagregard.Name = "btnagregard"
        Me.btnagregard.Size = New System.Drawing.Size(91, 66)
        Me.btnagregard.TabIndex = 30
        Me.btnagregard.Text = "Agregar detalle"
        Me.btnagregard.UseVisualStyleBackColor = True
        '
        'btneliminard
        '
        Me.btneliminard.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btneliminard.FlatAppearance.BorderSize = 2
        Me.btneliminard.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btneliminard.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btneliminard.Location = New System.Drawing.Point(827, 593)
        Me.btneliminard.Name = "btneliminard"
        Me.btneliminard.Size = New System.Drawing.Size(91, 66)
        Me.btneliminard.TabIndex = 31
        Me.btneliminard.Text = "Eliminar detalle"
        Me.btneliminard.UseVisualStyleBackColor = True
        '
        'btnregistrarc
        '
        Me.btnregistrarc.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnregistrarc.FlatAppearance.BorderSize = 2
        Me.btnregistrarc.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnregistrarc.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnregistrarc.Location = New System.Drawing.Point(924, 593)
        Me.btnregistrarc.Name = "btnregistrarc"
        Me.btnregistrarc.Size = New System.Drawing.Size(91, 66)
        Me.btnregistrarc.TabIndex = 32
        Me.btnregistrarc.Text = "Registrar compra"
        Me.btnregistrarc.UseVisualStyleBackColor = True
        '
        'btnsalir
        '
        Me.btnsalir.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnsalir.FlatAppearance.BorderSize = 2
        Me.btnsalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnsalir.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsalir.Location = New System.Drawing.Point(1021, 593)
        Me.btnsalir.Name = "btnsalir"
        Me.btnsalir.Size = New System.Drawing.Size(91, 66)
        Me.btnsalir.TabIndex = 33
        Me.btnsalir.Text = "Salir"
        Me.btnsalir.UseVisualStyleBackColor = True
        '
        'txttotal
        '
        Me.txttotal.BackColor = System.Drawing.Color.Blue
        Me.txttotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 30.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txttotal.ForeColor = System.Drawing.Color.Yellow
        Me.txttotal.Location = New System.Drawing.Point(1055, 530)
        Me.txttotal.Name = "txttotal"
        Me.txttotal.Size = New System.Drawing.Size(204, 53)
        Me.txttotal.TabIndex = 35
        Me.txttotal.Text = "0.00"
        Me.txttotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(453, 2)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(103, 20)
        Me.Label7.TabIndex = 2
        Me.Label7.Text = "Factura No."
        '
        'txtFactura
        '
        Me.txtFactura.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFactura.Location = New System.Drawing.Point(457, 25)
        Me.txtFactura.Name = "txtFactura"
        Me.txtFactura.Size = New System.Drawing.Size(158, 26)
        Me.txtFactura.TabIndex = 3
        '
        'txtpres
        '
        Me.txtpres.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtpres.Location = New System.Drawing.Point(13, 548)
        Me.txtpres.Name = "txtpres"
        Me.txtpres.Size = New System.Drawing.Size(296, 26)
        Me.txtpres.TabIndex = 14
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(10, 525)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(114, 20)
        Me.Label9.TabIndex = 13
        Me.Label9.Text = "Presentación"
        '
        'txtmedida
        '
        Me.txtmedida.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtmedida.Location = New System.Drawing.Point(315, 548)
        Me.txtmedida.Name = "txtmedida"
        Me.txtmedida.Size = New System.Drawing.Size(292, 26)
        Me.txtmedida.TabIndex = 16
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(312, 525)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(67, 20)
        Me.Label10.TabIndex = 15
        Me.Label10.Text = "Medida"
        '
        'btnDescartar
        '
        Me.btnDescartar.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnDescartar.FlatAppearance.BorderSize = 2
        Me.btnDescartar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDescartar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDescartar.Location = New System.Drawing.Point(1067, 21)
        Me.btnDescartar.Name = "btnDescartar"
        Me.btnDescartar.Size = New System.Drawing.Size(168, 31)
        Me.btnDescartar.TabIndex = 36
        Me.btnDescartar.Text = "Descartar Compra"
        Me.btnDescartar.UseVisualStyleBackColor = True
        '
        'txtFechaPago
        '
        Me.txtFechaPago.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFechaPago.Location = New System.Drawing.Point(621, 85)
        Me.txtFechaPago.Name = "txtFechaPago"
        Me.txtFechaPago.Size = New System.Drawing.Size(158, 29)
        Me.txtFechaPago.TabIndex = 40
        Me.txtFechaPago.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(617, 62)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(134, 20)
        Me.Label8.TabIndex = 39
        Me.Label8.Text = "Fecha de pago:"
        '
        'cmbProveedor
        '
        Me.cmbProveedor.DisplayMember = "idProveedor"
        Me.cmbProveedor.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbProveedor.FormattingEnabled = True
        Me.cmbProveedor.Location = New System.Drawing.Point(20, 85)
        Me.cmbProveedor.Name = "cmbProveedor"
        Me.cmbProveedor.Size = New System.Drawing.Size(425, 28)
        Me.cmbProveedor.TabIndex = 42
        Me.cmbProveedor.ValueMember = "idProveedor"
        '
        'cmbFP
        '
        Me.cmbFP.DisplayMember = "idFormaPago"
        Me.cmbFP.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbFP.FormattingEnabled = True
        Me.cmbFP.Location = New System.Drawing.Point(456, 85)
        Me.cmbFP.Name = "cmbFP"
        Me.cmbFP.Size = New System.Drawing.Size(159, 28)
        Me.cmbFP.TabIndex = 38
        Me.cmbFP.ValueMember = "idFormaPago"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(452, 63)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(130, 20)
        Me.Label1.TabIndex = 37
        Me.Label1.Text = "Forma de pago"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(14, 61)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(90, 20)
        Me.Label6.TabIndex = 41
        Me.Label6.Text = "Proveedor"
        '
        'Button1
        '
        Me.Button1.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.Button1.FlatAppearance.BorderSize = 2
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(1118, 593)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(91, 66)
        Me.Button1.TabIndex = 43
        Me.Button1.Text = "Ver Compras"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'frmCompra
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1285, 676)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.txtFechaPago)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.cmbProveedor)
        Me.Controls.Add(Me.cmbFP)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.btnDescartar)
        Me.Controls.Add(Me.txtmedida)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txtpres)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txtFactura)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txttotal)
        Me.Controls.Add(Me.btnsalir)
        Me.Controls.Add(Me.btnregistrarc)
        Me.Controls.Add(Me.btneliminard)
        Me.Controls.Add(Me.btnagregard)
        Me.Controls.Add(Me.btnnuevaventa)
        Me.Controls.Add(Me.DataGridView2)
        Me.Controls.Add(Me.txtcantidad)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.txtexistencia)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.txtprecio)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.txtdescpro)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtcodpro)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.mskfecha)
        Me.Controls.Add(Me.txtbuscapro)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblNoCompra)
        Me.Controls.Add(Me.ShapeContainer1)
        Me.Name = "frmCompra"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Nueva Compra"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblNoCompra As System.Windows.Forms.Label
    Friend WithEvents ShapeContainer1 As Microsoft.VisualBasic.PowerPacks.ShapeContainer
    Friend WithEvents LineShape1 As Microsoft.VisualBasic.PowerPacks.LineShape
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtbuscapro As System.Windows.Forms.TextBox
    Friend WithEvents mskfecha As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtcodpro As System.Windows.Forms.TextBox
    Friend WithEvents txtdescpro As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtcantidad As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtexistencia As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtprecio As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents descPro As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cant As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents precio As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents subt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnnuevaventa As System.Windows.Forms.Button
    Friend WithEvents btnagregard As System.Windows.Forms.Button
    Friend WithEvents btneliminard As System.Windows.Forms.Button
    Friend WithEvents btnregistrarc As System.Windows.Forms.Button
    Friend WithEvents btnsalir As System.Windows.Forms.Button
    Friend WithEvents txttotal As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtFactura As System.Windows.Forms.TextBox
    Friend WithEvents codpro As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dpro As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents exist As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents marca As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents pres As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents preciopro As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents prov As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents medida As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents txtpres As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtmedida As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents btnDescartar As System.Windows.Forms.Button
    Friend WithEvents txtFechaPago As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cmbProveedor As System.Windows.Forms.ComboBox
    Friend WithEvents cmbFP As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
End Class
