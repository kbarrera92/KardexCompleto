<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCatalogoProducto
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.codigo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.desc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.compo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.presentacion = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.atera = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.indi = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.contra = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.obser = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.prov = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.med = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cat = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lab = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.precio = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cost = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.fechReg = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.est = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.barcode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.stockmin = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ShapeContainer1 = New Microsoft.VisualBasic.PowerPacks.ShapeContainer()
        Me.RectangleShape1 = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtcod = New System.Windows.Forms.TextBox()
        Me.txtdesc = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtcomp = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtat = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtindi = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtcontra = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtpres = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtobs = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtmed = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtlab = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.cmbpro = New System.Windows.Forms.ComboBox()
        Me.cmbcat = New System.Windows.Forms.ComboBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.txtEstanteria = New System.Windows.Forms.TextBox()
        Me.txtutilidad = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtprecio = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtcosto = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.txtbarcode = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.txtstockmin = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.ComboBox1)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.TextBox1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1255, 70)
        Me.Panel1.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(950, 17)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(73, 15)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Bucar por:"
        '
        'ComboBox1
        '
        Me.ComboBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Items.AddRange(New Object() {"Código", "Descripción", "Composición", "Marca", "Proveedor", "Laboratorio", "Presentación", "Categoría", "Barras"})
        Me.ComboBox1.Location = New System.Drawing.Point(953, 35)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(285, 24)
        Me.ComboBox1.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(13, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(213, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Buscar porductos en el catálogo"
        '
        'TextBox1
        '
        Me.TextBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(13, 35)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(922, 26)
        Me.TextBox1.TabIndex = 0
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.BackgroundColor = System.Drawing.Color.White
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.codigo, Me.desc, Me.compo, Me.presentacion, Me.atera, Me.indi, Me.contra, Me.obser, Me.prov, Me.med, Me.cat, Me.lab, Me.precio, Me.cost, Me.fechReg, Me.est, Me.barcode, Me.stockmin})
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Top
        Me.DataGridView1.Location = New System.Drawing.Point(0, 70)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.Size = New System.Drawing.Size(1255, 270)
        Me.DataGridView1.TabIndex = 1
        '
        'codigo
        '
        Me.codigo.HeaderText = "Código"
        Me.codigo.Name = "codigo"
        Me.codigo.ReadOnly = True
        '
        'desc
        '
        Me.desc.HeaderText = "Descripción"
        Me.desc.Name = "desc"
        Me.desc.ReadOnly = True
        Me.desc.Width = 200
        '
        'compo
        '
        Me.compo.HeaderText = "Composición"
        Me.compo.Name = "compo"
        Me.compo.ReadOnly = True
        Me.compo.Width = 200
        '
        'presentacion
        '
        Me.presentacion.HeaderText = "Presentación"
        Me.presentacion.Name = "presentacion"
        Me.presentacion.ReadOnly = True
        Me.presentacion.Width = 150
        '
        'atera
        '
        Me.atera.HeaderText = "Acción Terapeutica"
        Me.atera.Name = "atera"
        Me.atera.ReadOnly = True
        Me.atera.Width = 250
        '
        'indi
        '
        Me.indi.HeaderText = "Indicaciones"
        Me.indi.Name = "indi"
        Me.indi.ReadOnly = True
        Me.indi.Width = 300
        '
        'contra
        '
        Me.contra.HeaderText = "Contraindicaciones"
        Me.contra.Name = "contra"
        Me.contra.ReadOnly = True
        Me.contra.Width = 300
        '
        'obser
        '
        Me.obser.HeaderText = "Observaciones"
        Me.obser.Name = "obser"
        Me.obser.ReadOnly = True
        Me.obser.Width = 300
        '
        'prov
        '
        Me.prov.HeaderText = "Proveedor"
        Me.prov.Name = "prov"
        Me.prov.ReadOnly = True
        Me.prov.Width = 150
        '
        'med
        '
        Me.med.HeaderText = "Medida"
        Me.med.Name = "med"
        Me.med.ReadOnly = True
        Me.med.Width = 120
        '
        'cat
        '
        Me.cat.HeaderText = "Categoría"
        Me.cat.Name = "cat"
        Me.cat.ReadOnly = True
        Me.cat.Width = 150
        '
        'lab
        '
        Me.lab.HeaderText = "Laboratorio"
        Me.lab.Name = "lab"
        Me.lab.ReadOnly = True
        Me.lab.Width = 300
        '
        'precio
        '
        Me.precio.HeaderText = "Precio"
        Me.precio.Name = "precio"
        Me.precio.ReadOnly = True
        '
        'cost
        '
        Me.cost.HeaderText = "Costo"
        Me.cost.Name = "cost"
        Me.cost.ReadOnly = True
        '
        'fechReg
        '
        Me.fechReg.HeaderText = "Fecha de Registro"
        Me.fechReg.Name = "fechReg"
        Me.fechReg.ReadOnly = True
        '
        'est
        '
        Me.est.HeaderText = "Estantería"
        Me.est.Name = "est"
        Me.est.ReadOnly = True
        '
        'barcode
        '
        Me.barcode.HeaderText = "Código de barras"
        Me.barcode.Name = "barcode"
        Me.barcode.ReadOnly = True
        '
        'stockmin
        '
        Me.stockmin.HeaderText = "Stock Min"
        Me.stockmin.Name = "stockmin"
        Me.stockmin.ReadOnly = True
        '
        'ShapeContainer1
        '
        Me.ShapeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.ShapeContainer1.Margin = New System.Windows.Forms.Padding(0)
        Me.ShapeContainer1.Name = "ShapeContainer1"
        Me.ShapeContainer1.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {Me.RectangleShape1})
        Me.ShapeContainer1.Size = New System.Drawing.Size(1255, 714)
        Me.ShapeContainer1.TabIndex = 2
        Me.ShapeContainer1.TabStop = False
        '
        'RectangleShape1
        '
        Me.RectangleShape1.BorderColor = System.Drawing.Color.MediumBlue
        Me.RectangleShape1.BorderWidth = 2
        Me.RectangleShape1.CornerRadius = 4
        Me.RectangleShape1.Location = New System.Drawing.Point(10, 355)
        Me.RectangleShape1.Name = "RectangleShape1"
        Me.RectangleShape1.Size = New System.Drawing.Size(983, 335)
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(23, 369)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(136, 15)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Código del producto"
        '
        'txtcod
        '
        Me.txtcod.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcod.Location = New System.Drawing.Point(26, 387)
        Me.txtcod.Name = "txtcod"
        Me.txtcod.Size = New System.Drawing.Size(133, 26)
        Me.txtcod.TabIndex = 1
        '
        'txtdesc
        '
        Me.txtdesc.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdesc.Location = New System.Drawing.Point(26, 439)
        Me.txtdesc.Name = "txtdesc"
        Me.txtdesc.Size = New System.Drawing.Size(391, 26)
        Me.txtdesc.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(23, 421)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(83, 15)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "Descripción"
        '
        'txtcomp
        '
        Me.txtcomp.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcomp.Location = New System.Drawing.Point(26, 492)
        Me.txtcomp.Name = "txtcomp"
        Me.txtcomp.Size = New System.Drawing.Size(391, 26)
        Me.txtcomp.TabIndex = 5
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(23, 474)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(94, 15)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "Composición "
        '
        'txtat
        '
        Me.txtat.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtat.Location = New System.Drawing.Point(26, 545)
        Me.txtat.Name = "txtat"
        Me.txtat.Size = New System.Drawing.Size(295, 26)
        Me.txtat.TabIndex = 6
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(23, 527)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(125, 15)
        Me.Label7.TabIndex = 11
        Me.Label7.Text = "Acción terapeutica"
        '
        'txtindi
        '
        Me.txtindi.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtindi.Location = New System.Drawing.Point(26, 650)
        Me.txtindi.Name = "txtindi"
        Me.txtindi.Size = New System.Drawing.Size(295, 26)
        Me.txtindi.TabIndex = 8
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(23, 632)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(88, 15)
        Me.Label8.TabIndex = 13
        Me.Label8.Text = "Indicaciones"
        '
        'txtcontra
        '
        Me.txtcontra.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcontra.Location = New System.Drawing.Point(332, 545)
        Me.txtcontra.Name = "txtcontra"
        Me.txtcontra.Size = New System.Drawing.Size(301, 26)
        Me.txtcontra.TabIndex = 12
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(329, 527)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(130, 15)
        Me.Label10.TabIndex = 17
        Me.Label10.Text = "Contraindicaciones"
        '
        'txtpres
        '
        Me.txtpres.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtpres.Location = New System.Drawing.Point(26, 597)
        Me.txtpres.Name = "txtpres"
        Me.txtpres.Size = New System.Drawing.Size(295, 26)
        Me.txtpres.TabIndex = 7
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(23, 579)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(91, 15)
        Me.Label11.TabIndex = 19
        Me.Label11.Text = "Presentación"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(329, 579)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(72, 15)
        Me.Label12.TabIndex = 21
        Me.Label12.Text = "Proveedor"
        '
        'txtobs
        '
        Me.txtobs.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtobs.Location = New System.Drawing.Point(645, 387)
        Me.txtobs.Multiline = True
        Me.txtobs.Name = "txtobs"
        Me.txtobs.Size = New System.Drawing.Size(333, 78)
        Me.txtobs.TabIndex = 15
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(642, 369)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(101, 15)
        Me.Label13.TabIndex = 23
        Me.Label13.Text = "Observaciones"
        '
        'txtmed
        '
        Me.txtmed.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtmed.Location = New System.Drawing.Point(645, 492)
        Me.txtmed.Name = "txtmed"
        Me.txtmed.Size = New System.Drawing.Size(333, 26)
        Me.txtmed.TabIndex = 16
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(642, 474)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(55, 15)
        Me.Label14.TabIndex = 25
        Me.Label14.Text = "Medida"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(642, 527)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(69, 15)
        Me.Label15.TabIndex = 27
        Me.Label15.Text = "Categoría"
        '
        'txtlab
        '
        Me.txtlab.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtlab.Location = New System.Drawing.Point(645, 597)
        Me.txtlab.Name = "txtlab"
        Me.txtlab.Size = New System.Drawing.Size(333, 26)
        Me.txtlab.TabIndex = 18
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(642, 579)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(81, 15)
        Me.Label16.TabIndex = 29
        Me.Label16.Text = "Laboratorio"
        '
        'cmbpro
        '
        Me.cmbpro.DisplayMember = "idProveedor"
        Me.cmbpro.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbpro.FormattingEnabled = True
        Me.cmbpro.Location = New System.Drawing.Point(332, 597)
        Me.cmbpro.Name = "cmbpro"
        Me.cmbpro.Size = New System.Drawing.Size(301, 28)
        Me.cmbpro.TabIndex = 13
        Me.cmbpro.ValueMember = "idProveedor"
        '
        'cmbcat
        '
        Me.cmbcat.DisplayMember = "idCategoria"
        Me.cmbcat.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbcat.FormattingEnabled = True
        Me.cmbcat.Location = New System.Drawing.Point(645, 544)
        Me.cmbcat.Name = "cmbcat"
        Me.cmbcat.Size = New System.Drawing.Size(333, 28)
        Me.cmbcat.TabIndex = 17
        Me.cmbcat.ValueMember = "idCategoria"
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.MediumBlue
        Me.GroupBox1.Controls.Add(Me.Button4)
        Me.GroupBox1.Controls.Add(Me.Button3)
        Me.GroupBox1.Controls.Add(Me.Button2)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.White
        Me.GroupBox1.Location = New System.Drawing.Point(1006, 355)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(242, 66)
        Me.GroupBox1.TabIndex = 21
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Acciones"
        '
        'Button4
        '
        Me.Button4.FlatAppearance.BorderSize = 2
        Me.Button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button4.Image = Global.Farmacia1._0.My.Resources.Resources.Windows_Close_Program_icon
        Me.Button4.Location = New System.Drawing.Point(136, 20)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(37, 34)
        Me.Button4.TabIndex = 3
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.FlatAppearance.BorderSize = 2
        Me.Button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button3.Image = Global.Farmacia1._0.My.Resources.Resources.trash_icon
        Me.Button3.Location = New System.Drawing.Point(93, 20)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(37, 34)
        Me.Button3.TabIndex = 2
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.FlatAppearance.BorderSize = 2
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Image = Global.Farmacia1._0.My.Resources.Resources.Save_icon__2_
        Me.Button2.Location = New System.Drawing.Point(50, 20)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(37, 34)
        Me.Button2.TabIndex = 1
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.FlatAppearance.BorderSize = 2
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Image = Global.Farmacia1._0.My.Resources.Resources.new_file_icon
        Me.Button1.Location = New System.Drawing.Point(7, 20)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(37, 34)
        Me.Button1.TabIndex = 0
        Me.Button1.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.MediumBlue
        Me.GroupBox2.Controls.Add(Me.Button5)
        Me.GroupBox2.Controls.Add(Me.txtEstanteria)
        Me.GroupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.Color.White
        Me.GroupBox2.Location = New System.Drawing.Point(1006, 427)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(242, 196)
        Me.GroupBox2.TabIndex = 20
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Estantería"
        '
        'Button5
        '
        Me.Button5.ForeColor = System.Drawing.Color.Black
        Me.Button5.Image = Global.Farmacia1._0.My.Resources.Resources.Zoom_icon
        Me.Button5.Location = New System.Drawing.Point(37, 151)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(176, 27)
        Me.Button5.TabIndex = 1
        Me.Button5.Text = "   Ver estanterías"
        Me.Button5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button5.UseVisualStyleBackColor = True
        '
        'txtEstanteria
        '
        Me.txtEstanteria.Font = New System.Drawing.Font("Microsoft Sans Serif", 70.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEstanteria.Location = New System.Drawing.Point(37, 32)
        Me.txtEstanteria.Name = "txtEstanteria"
        Me.txtEstanteria.Size = New System.Drawing.Size(176, 113)
        Me.txtEstanteria.TabIndex = 0
        Me.txtEstanteria.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtutilidad
        '
        Me.txtutilidad.BackColor = System.Drawing.Color.Blue
        Me.txtutilidad.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtutilidad.ForeColor = System.Drawing.Color.Yellow
        Me.txtutilidad.Location = New System.Drawing.Point(430, 492)
        Me.txtutilidad.Name = "txtutilidad"
        Me.txtutilidad.Size = New System.Drawing.Size(203, 26)
        Me.txtutilidad.TabIndex = 11
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(427, 474)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(57, 15)
        Me.Label17.TabIndex = 73
        Me.Label17.Text = "Utilidad"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(427, 421)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(43, 15)
        Me.Label18.TabIndex = 72
        Me.Label18.Text = "Costo"
        '
        'txtprecio
        '
        Me.txtprecio.BackColor = System.Drawing.Color.Blue
        Me.txtprecio.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtprecio.ForeColor = System.Drawing.Color.Yellow
        Me.txtprecio.Location = New System.Drawing.Point(430, 387)
        Me.txtprecio.Name = "txtprecio"
        Me.txtprecio.Size = New System.Drawing.Size(203, 26)
        Me.txtprecio.TabIndex = 9
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(427, 369)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(48, 15)
        Me.Label19.TabIndex = 70
        Me.Label19.Text = "Precio"
        '
        'txtcosto
        '
        Me.txtcosto.BackColor = System.Drawing.Color.Blue
        Me.txtcosto.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcosto.ForeColor = System.Drawing.Color.Yellow
        Me.txtcosto.Location = New System.Drawing.Point(430, 439)
        Me.txtcosto.Name = "txtcosto"
        Me.txtcosto.Size = New System.Drawing.Size(203, 26)
        Me.txtcosto.TabIndex = 10
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(642, 632)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(119, 15)
        Me.Label9.TabIndex = 77
        Me.Label9.Text = "Fecha de registro"
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1.Location = New System.Drawing.Point(645, 651)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(333, 26)
        Me.DateTimePicker1.TabIndex = 19
        '
        'txtbarcode
        '
        Me.txtbarcode.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbarcode.Location = New System.Drawing.Point(165, 387)
        Me.txtbarcode.Name = "txtbarcode"
        Me.txtbarcode.Size = New System.Drawing.Size(211, 26)
        Me.txtbarcode.TabIndex = 2
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(162, 369)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(117, 15)
        Me.Label20.TabIndex = 79
        Me.Label20.Text = "Código de barras"
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(382, 385)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(35, 29)
        Me.Button6.TabIndex = 3
        Me.Button6.UseVisualStyleBackColor = True
        '
        'txtstockmin
        '
        Me.txtstockmin.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtstockmin.Location = New System.Drawing.Point(332, 650)
        Me.txtstockmin.Name = "txtstockmin"
        Me.txtstockmin.Size = New System.Drawing.Size(301, 26)
        Me.txtstockmin.TabIndex = 14
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(329, 632)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(125, 15)
        Me.Label6.TabIndex = 82
        Me.Label6.Text = "Existencía mínima"
        '
        'frmCatalogoProducto
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1255, 714)
        Me.Controls.Add(Me.txtstockmin)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.txtbarcode)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txtcosto)
        Me.Controls.Add(Me.txtutilidad)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.txtprecio)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmbcat)
        Me.Controls.Add(Me.cmbpro)
        Me.Controls.Add(Me.txtlab)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.txtmed)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.txtobs)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.txtpres)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.txtcontra)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txtindi)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtat)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtcomp)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtdesc)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtcod)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.ShapeContainer1)
        Me.Name = "frmCatalogoProducto"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Catálogo de productos"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents ShapeContainer1 As Microsoft.VisualBasic.PowerPacks.ShapeContainer
    Friend WithEvents RectangleShape1 As Microsoft.VisualBasic.PowerPacks.RectangleShape
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtcod As System.Windows.Forms.TextBox
    Friend WithEvents txtdesc As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtcomp As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtat As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtindi As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtcontra As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtpres As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtobs As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtmed As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtlab As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents cmbpro As System.Windows.Forms.ComboBox
    Friend WithEvents cmbcat As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtutilidad As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtprecio As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtcosto As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtEstanteria As System.Windows.Forms.TextBox
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents txtbarcode As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents codigo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents desc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents compo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents presentacion As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents atera As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents indi As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents contra As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents obser As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents prov As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents med As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cat As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lab As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents precio As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cost As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents fechReg As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents est As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents barcode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents stockmin As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents txtstockmin As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
End Class
