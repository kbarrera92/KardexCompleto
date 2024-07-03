<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormRecibirTraslado
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.TextBoxFechaRecibe = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TextBoxFechaEnvia = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TextBoxSucursalRecibe = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBoxUsuarioRecibe = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBoxSucursalEnvia = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBoxUsuarioEnvia = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.iddetalle = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.idtraslado = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.idproducto = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.producto = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cantidad = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(9, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(132, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Código Traslado:"
        '
        'TextBox1
        '
        Me.TextBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 30.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(12, 30)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(270, 53)
        Me.TextBox1.TabIndex = 1
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Controls.Add(Me.TextBoxFechaRecibe)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.TextBoxFechaEnvia)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.TextBoxSucursalRecibe)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.TextBoxUsuarioRecibe)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.TextBoxSucursalEnvia)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.TextBoxUsuarioEnvia)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.TextBox1)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(297, 494)
        Me.Panel1.TabIndex = 2
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.SteelBlue
        Me.Button1.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Location = New System.Drawing.Point(15, 439)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(267, 43)
        Me.Button1.TabIndex = 14
        Me.Button1.Text = "Recibir Traslado"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'TextBoxFechaRecibe
        '
        Me.TextBoxFechaRecibe.Location = New System.Drawing.Point(15, 396)
        Me.TextBoxFechaRecibe.Name = "TextBoxFechaRecibe"
        Me.TextBoxFechaRecibe.ReadOnly = True
        Me.TextBoxFechaRecibe.Size = New System.Drawing.Size(267, 20)
        Me.TextBoxFechaRecibe.TabIndex = 13
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(12, 375)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(107, 17)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "Fecha recibe:"
        '
        'TextBoxFechaEnvia
        '
        Me.TextBoxFechaEnvia.Location = New System.Drawing.Point(15, 351)
        Me.TextBoxFechaEnvia.Name = "TextBoxFechaEnvia"
        Me.TextBoxFechaEnvia.ReadOnly = True
        Me.TextBoxFechaEnvia.Size = New System.Drawing.Size(267, 20)
        Me.TextBoxFechaEnvia.TabIndex = 11
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(12, 330)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(101, 17)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "Fecha envío:"
        '
        'TextBoxSucursalRecibe
        '
        Me.TextBoxSucursalRecibe.Location = New System.Drawing.Point(15, 282)
        Me.TextBoxSucursalRecibe.Name = "TextBoxSucursalRecibe"
        Me.TextBoxSucursalRecibe.ReadOnly = True
        Me.TextBoxSucursalRecibe.Size = New System.Drawing.Size(267, 20)
        Me.TextBoxSucursalRecibe.TabIndex = 9
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(12, 261)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(23, 17)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "A:"
        '
        'TextBoxUsuarioRecibe
        '
        Me.TextBoxUsuarioRecibe.Location = New System.Drawing.Point(15, 237)
        Me.TextBoxUsuarioRecibe.Name = "TextBoxUsuarioRecibe"
        Me.TextBoxUsuarioRecibe.ReadOnly = True
        Me.TextBoxUsuarioRecibe.Size = New System.Drawing.Size(267, 20)
        Me.TextBoxUsuarioRecibe.TabIndex = 7
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(12, 216)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(63, 17)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Recibe:"
        '
        'TextBoxSucursalEnvia
        '
        Me.TextBoxSucursalEnvia.Location = New System.Drawing.Point(15, 166)
        Me.TextBoxSucursalEnvia.Name = "TextBoxSucursalEnvia"
        Me.TextBoxSucursalEnvia.ReadOnly = True
        Me.TextBoxSucursalEnvia.Size = New System.Drawing.Size(267, 20)
        Me.TextBoxSucursalEnvia.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(12, 145)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 17)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Desde:"
        '
        'TextBoxUsuarioEnvia
        '
        Me.TextBoxUsuarioEnvia.Location = New System.Drawing.Point(15, 121)
        Me.TextBoxUsuarioEnvia.Name = "TextBoxUsuarioEnvia"
        Me.TextBoxUsuarioEnvia.ReadOnly = True
        Me.TextBoxUsuarioEnvia.Size = New System.Drawing.Size(267, 20)
        Me.TextBoxUsuarioEnvia.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 100)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 17)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Envía:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(316, 9)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(168, 17)
        Me.Label8.TabIndex = 3
        Me.Label8.Text = "Detalles del Traslado:"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.iddetalle, Me.idtraslado, Me.idproducto, Me.producto, Me.cantidad})
        Me.DataGridView1.Location = New System.Drawing.Point(319, 30)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(673, 452)
        Me.DataGridView1.TabIndex = 4
        '
        'iddetalle
        '
        Me.iddetalle.HeaderText = "Id. Detalle"
        Me.iddetalle.Name = "iddetalle"
        Me.iddetalle.ReadOnly = True
        '
        'idtraslado
        '
        Me.idtraslado.HeaderText = "No. Traslado"
        Me.idtraslado.Name = "idtraslado"
        Me.idtraslado.ReadOnly = True
        '
        'idproducto
        '
        Me.idproducto.HeaderText = "Código"
        Me.idproducto.Name = "idproducto"
        Me.idproducto.ReadOnly = True
        '
        'producto
        '
        Me.producto.HeaderText = "Producto"
        Me.producto.Name = "producto"
        Me.producto.ReadOnly = True
        '
        'cantidad
        '
        Me.cantidad.HeaderText = "Cantidad"
        Me.cantidad.Name = "cantidad"
        Me.cantidad.ReadOnly = True
        '
        'FormRecibirTraslado
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1004, 494)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "FormRecibirTraslado"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Recibir Traslado de Productos"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Button1 As Button
    Friend WithEvents TextBoxFechaRecibe As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents TextBoxFechaEnvia As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents TextBoxSucursalRecibe As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents TextBoxUsuarioRecibe As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents TextBoxSucursalEnvia As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents TextBoxUsuarioEnvia As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents iddetalle As DataGridViewTextBoxColumn
    Friend WithEvents idtraslado As DataGridViewTextBoxColumn
    Friend WithEvents idproducto As DataGridViewTextBoxColumn
    Friend WithEvents producto As DataGridViewTextBoxColumn
    Friend WithEvents cantidad As DataGridViewTextBoxColumn
End Class
