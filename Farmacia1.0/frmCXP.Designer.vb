<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCXP
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
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.cuenta = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.concep = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.fi = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.fl = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.tot = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.saldo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.estado = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.nestado = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.idProv = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.prov = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.nit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.compra = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtncuenta = New System.Windows.Forms.TextBox()
        Me.txtconcep = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtcodpro = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtnit = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtrz = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtsaldo = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txttot = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtcompra = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtestado = New System.Windows.Forms.TextBox()
        Me.txtsttus = New System.Windows.Forms.TextBox()
        Me.txtfi = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtfl = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtdias = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.Button2 = New System.Windows.Forms.Button()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cuenta, Me.concep, Me.fi, Me.fl, Me.tot, Me.saldo, Me.estado, Me.nestado, Me.idProv, Me.prov, Me.nit, Me.compra})
        Me.DataGridView1.Location = New System.Drawing.Point(12, 73)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(879, 244)
        Me.DataGridView1.TabIndex = 0
        '
        'cuenta
        '
        Me.cuenta.HeaderText = "No. de cuenta"
        Me.cuenta.Name = "cuenta"
        Me.cuenta.ReadOnly = True
        '
        'concep
        '
        Me.concep.HeaderText = "Concepto"
        Me.concep.Name = "concep"
        Me.concep.ReadOnly = True
        Me.concep.Width = 250
        '
        'fi
        '
        Me.fi.HeaderText = "Fecha de registro"
        Me.fi.Name = "fi"
        Me.fi.ReadOnly = True
        '
        'fl
        '
        Me.fl.HeaderText = "Fecha límite"
        Me.fl.Name = "fl"
        Me.fl.ReadOnly = True
        '
        'tot
        '
        Me.tot.HeaderText = "Total"
        Me.tot.Name = "tot"
        Me.tot.ReadOnly = True
        '
        'saldo
        '
        Me.saldo.HeaderText = "Saldo"
        Me.saldo.Name = "saldo"
        Me.saldo.ReadOnly = True
        '
        'estado
        '
        Me.estado.HeaderText = "Estado"
        Me.estado.Name = "estado"
        Me.estado.ReadOnly = True
        '
        'nestado
        '
        Me.nestado.HeaderText = "Estado"
        Me.nestado.Name = "nestado"
        Me.nestado.ReadOnly = True
        Me.nestado.Width = 120
        '
        'idProv
        '
        Me.idProv.HeaderText = "Código Prov"
        Me.idProv.Name = "idProv"
        Me.idProv.ReadOnly = True
        '
        'prov
        '
        Me.prov.HeaderText = "Proveedor"
        Me.prov.Name = "prov"
        Me.prov.ReadOnly = True
        Me.prov.Width = 200
        '
        'nit
        '
        Me.nit.HeaderText = "NIT"
        Me.nit.Name = "nit"
        Me.nit.ReadOnly = True
        '
        'compra
        '
        Me.compra.HeaderText = "No. de compra"
        Me.compra.Name = "compra"
        Me.compra.ReadOnly = True
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.MediumBlue
        Me.GroupBox1.Controls.Add(Me.TextBox1)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.White
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(659, 55)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Buscar cuentas por pagar"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(16, 19)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(629, 23)
        Me.TextBox1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(18, 389)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(110, 17)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "No. de cuenta"
        '
        'txtncuenta
        '
        Me.txtncuenta.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtncuenta.Location = New System.Drawing.Point(21, 407)
        Me.txtncuenta.Name = "txtncuenta"
        Me.txtncuenta.Size = New System.Drawing.Size(134, 23)
        Me.txtncuenta.TabIndex = 4
        '
        'txtconcep
        '
        Me.txtconcep.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtconcep.Location = New System.Drawing.Point(164, 407)
        Me.txtconcep.Name = "txtconcep"
        Me.txtconcep.Size = New System.Drawing.Size(512, 23)
        Me.txtconcep.TabIndex = 6
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(161, 389)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(76, 17)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Concepto"
        '
        'txtcodpro
        '
        Me.txtcodpro.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcodpro.Location = New System.Drawing.Point(21, 463)
        Me.txtcodpro.Name = "txtcodpro"
        Me.txtcodpro.Size = New System.Drawing.Size(134, 23)
        Me.txtcodpro.TabIndex = 8
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(18, 445)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(137, 17)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Código proveedor"
        '
        'txtnit
        '
        Me.txtnit.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtnit.Location = New System.Drawing.Point(164, 463)
        Me.txtnit.Name = "txtnit"
        Me.txtnit.Size = New System.Drawing.Size(134, 23)
        Me.txtnit.TabIndex = 10
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(161, 445)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(113, 17)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "NIT Proveedor"
        '
        'txtrz
        '
        Me.txtrz.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtrz.Location = New System.Drawing.Point(304, 463)
        Me.txtrz.Name = "txtrz"
        Me.txtrz.Size = New System.Drawing.Size(372, 23)
        Me.txtrz.TabIndex = 12
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(301, 445)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(101, 17)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Razón social"
        '
        'txtsaldo
        '
        Me.txtsaldo.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtsaldo.Location = New System.Drawing.Point(164, 524)
        Me.txtsaldo.Name = "txtsaldo"
        Me.txtsaldo.Size = New System.Drawing.Size(134, 23)
        Me.txtsaldo.TabIndex = 16
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(161, 506)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(103, 17)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "Saldo cuenta"
        '
        'txttot
        '
        Me.txttot.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txttot.Location = New System.Drawing.Point(21, 524)
        Me.txttot.Name = "txttot"
        Me.txttot.Size = New System.Drawing.Size(134, 23)
        Me.txttot.TabIndex = 14
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(18, 506)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(140, 17)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = "Total de la cuenta"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(299, 506)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(114, 17)
        Me.Label8.TabIndex = 13
        Me.Label8.Text = "No. de compra"
        '
        'txtcompra
        '
        Me.txtcompra.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcompra.Location = New System.Drawing.Point(302, 524)
        Me.txtcompra.Name = "txtcompra"
        Me.txtcompra.Size = New System.Drawing.Size(134, 23)
        Me.txtcompra.TabIndex = 14
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(442, 506)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(58, 17)
        Me.Label9.TabIndex = 15
        Me.Label9.Text = "Estado"
        '
        'txtestado
        '
        Me.txtestado.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtestado.Location = New System.Drawing.Point(445, 524)
        Me.txtestado.Name = "txtestado"
        Me.txtestado.Size = New System.Drawing.Size(134, 23)
        Me.txtestado.TabIndex = 16
        '
        'txtsttus
        '
        Me.txtsttus.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtsttus.Location = New System.Drawing.Point(585, 524)
        Me.txtsttus.Name = "txtsttus"
        Me.txtsttus.Size = New System.Drawing.Size(91, 23)
        Me.txtsttus.TabIndex = 17
        '
        'txtfi
        '
        Me.txtfi.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtfi.Location = New System.Drawing.Point(726, 407)
        Me.txtfi.Name = "txtfi"
        Me.txtfi.Size = New System.Drawing.Size(134, 23)
        Me.txtfi.TabIndex = 19
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(723, 389)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(136, 17)
        Me.Label10.TabIndex = 18
        Me.Label10.Text = "Fecha de registro"
        '
        'txtfl
        '
        Me.txtfl.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtfl.Location = New System.Drawing.Point(726, 463)
        Me.txtfl.Name = "txtfl"
        Me.txtfl.Size = New System.Drawing.Size(134, 23)
        Me.txtfl.TabIndex = 21
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(723, 445)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(95, 17)
        Me.Label11.TabIndex = 20
        Me.Label11.Text = "Fecha límite"
        '
        'txtdias
        '
        Me.txtdias.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdias.Location = New System.Drawing.Point(726, 524)
        Me.txtdias.Name = "txtdias"
        Me.txtdias.Size = New System.Drawing.Size(134, 23)
        Me.txtdias.TabIndex = 23
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(723, 506)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(113, 17)
        Me.Label12.TabIndex = 22
        Me.Label12.Text = "Días restantes"
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(21, 333)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(134, 34)
        Me.Button1.TabIndex = 24
        Me.Button1.Text = "Ver detalles"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.Location = New System.Drawing.Point(164, 333)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(134, 34)
        Me.Button3.TabIndex = 26
        Me.Button3.Text = "Salir"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel1.Location = New System.Drawing.Point(678, 53)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(144, 13)
        Me.LinkLabel1.TabIndex = 27
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Ver cuentas canceladas"
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(307, 333)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(166, 34)
        Me.Button2.TabIndex = 26
        Me.Button2.Text = "Ver cuentas por mes"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'frmCXP
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(903, 562)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.txtdias)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.txtfl)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.txtfi)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txtsttus)
        Me.Controls.Add(Me.txtestado)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txtsaldo)
        Me.Controls.Add(Me.txtcompra)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txttot)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtrz)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtnit)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtcodpro)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtconcep)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtncuenta)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Name = "frmCXP"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Listado de cuentas por pagar"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtncuenta As System.Windows.Forms.TextBox
    Friend WithEvents txtconcep As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtcodpro As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtnit As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtrz As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtsaldo As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txttot As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtcompra As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtestado As System.Windows.Forms.TextBox
    Friend WithEvents txtsttus As System.Windows.Forms.TextBox
    Friend WithEvents txtfi As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtfl As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtdias As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents cuenta As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents concep As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents fi As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents fl As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents tot As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents saldo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents estado As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents nestado As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents idProv As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents prov As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents nit As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents compra As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Friend WithEvents Button2 As System.Windows.Forms.Button
End Class
