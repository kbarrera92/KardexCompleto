<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormEgresos
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.ButtonLimpiar = New System.Windows.Forms.Button()
        Me.ButtonGuardar = New System.Windows.Forms.Button()
        Me.TextBoxUsuarioRegistra = New System.Windows.Forms.TextBox()
        Me.TextBoxTotalEgreso = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.ComboBoxSucursal = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ComboBoxCategoria = New System.Windows.Forms.ComboBox()
        Me.TextBoxDescripcion = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DateTimePickerFechaEgreso = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.TextBoxSumatoria = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.DataGridViewEgresos = New System.Windows.Forms.DataGridView()
        Me.idEgreso = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.fechaEgreso = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.categoriaEgreso = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.descripcionCategoriaEgreso = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.totalEgreso = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.usuarioRegistraEgreso = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.sucursalEgreso = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DateTimePickerFiltro = New System.Windows.Forms.DateTimePicker()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.DataGridViewEgresos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.ButtonLimpiar)
        Me.Panel1.Controls.Add(Me.ButtonGuardar)
        Me.Panel1.Controls.Add(Me.TextBoxUsuarioRegistra)
        Me.Panel1.Controls.Add(Me.TextBoxTotalEgreso)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.ComboBoxSucursal)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.ComboBoxCategoria)
        Me.Panel1.Controls.Add(Me.TextBoxDescripcion)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.DateTimePickerFechaEgreso)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(903, 174)
        Me.Panel1.TabIndex = 0
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(816, 53)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 16
        Me.Button1.Text = "Descartar"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ButtonLimpiar
        '
        Me.ButtonLimpiar.Location = New System.Drawing.Point(501, 139)
        Me.ButtonLimpiar.Name = "ButtonLimpiar"
        Me.ButtonLimpiar.Size = New System.Drawing.Size(75, 23)
        Me.ButtonLimpiar.TabIndex = 15
        Me.ButtonLimpiar.Text = "Limpiar"
        Me.ButtonLimpiar.UseVisualStyleBackColor = True
        '
        'ButtonGuardar
        '
        Me.ButtonGuardar.Location = New System.Drawing.Point(591, 139)
        Me.ButtonGuardar.Name = "ButtonGuardar"
        Me.ButtonGuardar.Size = New System.Drawing.Size(75, 23)
        Me.ButtonGuardar.TabIndex = 14
        Me.ButtonGuardar.Text = "Guardar"
        Me.ButtonGuardar.UseVisualStyleBackColor = True
        '
        'TextBoxUsuarioRegistra
        '
        Me.TextBoxUsuarioRegistra.Location = New System.Drawing.Point(501, 71)
        Me.TextBoxUsuarioRegistra.Name = "TextBoxUsuarioRegistra"
        Me.TextBoxUsuarioRegistra.ReadOnly = True
        Me.TextBoxUsuarioRegistra.Size = New System.Drawing.Size(373, 20)
        Me.TextBoxUsuarioRegistra.TabIndex = 13
        '
        'TextBoxTotalEgreso
        '
        Me.TextBoxTotalEgreso.Location = New System.Drawing.Point(501, 28)
        Me.TextBoxTotalEgreso.Name = "TextBoxTotalEgreso"
        Me.TextBoxTotalEgreso.Size = New System.Drawing.Size(373, 20)
        Me.TextBoxTotalEgreso.TabIndex = 12
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(498, 11)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(34, 13)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Total:"
        '
        'ComboBoxSucursal
        '
        Me.ComboBoxSucursal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxSucursal.Enabled = False
        Me.ComboBoxSucursal.FormattingEnabled = True
        Me.ComboBoxSucursal.Location = New System.Drawing.Point(501, 112)
        Me.ComboBoxSucursal.Name = "ComboBoxSucursal"
        Me.ComboBoxSucursal.Size = New System.Drawing.Size(373, 21)
        Me.ComboBoxSucursal.TabIndex = 10
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(498, 95)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(51, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Sucursal:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(498, 55)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(83, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Usuario registra:"
        '
        'ComboBoxCategoria
        '
        Me.ComboBoxCategoria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxCategoria.FormattingEnabled = True
        Me.ComboBoxCategoria.Location = New System.Drawing.Point(16, 72)
        Me.ComboBoxCategoria.Name = "ComboBoxCategoria"
        Me.ComboBoxCategoria.Size = New System.Drawing.Size(441, 21)
        Me.ComboBoxCategoria.TabIndex = 6
        '
        'TextBoxDescripcion
        '
        Me.TextBoxDescripcion.Location = New System.Drawing.Point(16, 113)
        Me.TextBoxDescripcion.Name = "TextBoxDescripcion"
        Me.TextBoxDescripcion.Size = New System.Drawing.Size(441, 20)
        Me.TextBoxDescripcion.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(13, 96)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(66, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Descripción:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 55)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(57, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Categoría:"
        '
        'DateTimePickerFechaEgreso
        '
        Me.DateTimePickerFechaEgreso.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerFechaEgreso.Location = New System.Drawing.Point(16, 28)
        Me.DateTimePickerFechaEgreso.Name = "DateTimePickerFechaEgreso"
        Me.DateTimePickerFechaEgreso.Size = New System.Drawing.Size(441, 20)
        Me.DateTimePickerFechaEgreso.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(40, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Fecha:"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Button1)
        Me.Panel2.Controls.Add(Me.TextBoxSumatoria)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.GroupBox1)
        Me.Panel2.Controls.Add(Me.DataGridViewEgresos)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 174)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(903, 381)
        Me.Panel2.TabIndex = 1
        '
        'TextBoxSumatoria
        '
        Me.TextBoxSumatoria.BackColor = System.Drawing.Color.DarkBlue
        Me.TextBoxSumatoria.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBoxSumatoria.ForeColor = System.Drawing.Color.White
        Me.TextBoxSumatoria.Location = New System.Drawing.Point(682, 323)
        Me.TextBoxSumatoria.Name = "TextBoxSumatoria"
        Me.TextBoxSumatoria.Size = New System.Drawing.Size(208, 38)
        Me.TextBoxSumatoria.TabIndex = 6
        Me.TextBoxSumatoria.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(13, 4)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(45, 13)
        Me.Label7.TabIndex = 5
        Me.Label7.Text = "Filtros:"
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.DarkBlue
        Me.GroupBox1.Controls.Add(Me.Button2)
        Me.GroupBox1.Controls.Add(Me.DateTimePickerFiltro)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.White
        Me.GroupBox1.Location = New System.Drawing.Point(11, 23)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(446, 53)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Fecha"
        '
        'DataGridViewEgresos
        '
        Me.DataGridViewEgresos.AllowUserToAddRows = False
        Me.DataGridViewEgresos.AllowUserToDeleteRows = False
        Me.DataGridViewEgresos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridViewEgresos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewEgresos.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.idEgreso, Me.fechaEgreso, Me.categoriaEgreso, Me.descripcionCategoriaEgreso, Me.totalEgreso, Me.usuarioRegistraEgreso, Me.sucursalEgreso})
        Me.DataGridViewEgresos.Location = New System.Drawing.Point(11, 82)
        Me.DataGridViewEgresos.Name = "DataGridViewEgresos"
        Me.DataGridViewEgresos.ReadOnly = True
        Me.DataGridViewEgresos.Size = New System.Drawing.Size(879, 240)
        Me.DataGridViewEgresos.TabIndex = 0
        '
        'idEgreso
        '
        Me.idEgreso.HeaderText = "Id"
        Me.idEgreso.Name = "idEgreso"
        Me.idEgreso.ReadOnly = True
        '
        'fechaEgreso
        '
        Me.fechaEgreso.HeaderText = "Fecha"
        Me.fechaEgreso.Name = "fechaEgreso"
        Me.fechaEgreso.ReadOnly = True
        '
        'categoriaEgreso
        '
        Me.categoriaEgreso.HeaderText = "Categoría"
        Me.categoriaEgreso.Name = "categoriaEgreso"
        Me.categoriaEgreso.ReadOnly = True
        '
        'descripcionCategoriaEgreso
        '
        Me.descripcionCategoriaEgreso.HeaderText = "Descripción"
        Me.descripcionCategoriaEgreso.Name = "descripcionCategoriaEgreso"
        Me.descripcionCategoriaEgreso.ReadOnly = True
        '
        'totalEgreso
        '
        Me.totalEgreso.HeaderText = "Total"
        Me.totalEgreso.Name = "totalEgreso"
        Me.totalEgreso.ReadOnly = True
        '
        'usuarioRegistraEgreso
        '
        Me.usuarioRegistraEgreso.HeaderText = "Usuario registra"
        Me.usuarioRegistraEgreso.Name = "usuarioRegistraEgreso"
        Me.usuarioRegistraEgreso.ReadOnly = True
        '
        'sucursalEgreso
        '
        Me.sucursalEgreso.HeaderText = "Sucursal"
        Me.sucursalEgreso.Name = "sucursalEgreso"
        Me.sucursalEgreso.ReadOnly = True
        '
        'DateTimePickerFiltro
        '
        Me.DateTimePickerFiltro.Location = New System.Drawing.Point(9, 19)
        Me.DateTimePickerFiltro.Name = "DateTimePickerFiltro"
        Me.DateTimePickerFiltro.Size = New System.Drawing.Size(391, 20)
        Me.DateTimePickerFiltro.TabIndex = 0
        '
        'Button2
        '
        Me.Button2.ForeColor = System.Drawing.Color.DarkBlue
        Me.Button2.Location = New System.Drawing.Point(406, 19)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(24, 21)
        Me.Button2.TabIndex = 1
        Me.Button2.Text = "B"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'FormEgresos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(903, 555)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "FormEgresos"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Registro y Visualización de Egresos"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.DataGridViewEgresos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents TextBoxTotalEgreso As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents ComboBoxSucursal As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents ComboBoxCategoria As ComboBox
    Friend WithEvents TextBoxDescripcion As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents DateTimePickerFechaEgreso As DateTimePicker
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents DataGridViewEgresos As DataGridView
    Friend WithEvents Label7 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents TextBoxUsuarioRegistra As TextBox
    Friend WithEvents ButtonGuardar As Button
    Friend WithEvents idEgreso As DataGridViewTextBoxColumn
    Friend WithEvents fechaEgreso As DataGridViewTextBoxColumn
    Friend WithEvents categoriaEgreso As DataGridViewTextBoxColumn
    Friend WithEvents descripcionCategoriaEgreso As DataGridViewTextBoxColumn
    Friend WithEvents totalEgreso As DataGridViewTextBoxColumn
    Friend WithEvents usuarioRegistraEgreso As DataGridViewTextBoxColumn
    Friend WithEvents sucursalEgreso As DataGridViewTextBoxColumn
    Friend WithEvents ButtonLimpiar As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents TextBoxSumatoria As TextBox
    Friend WithEvents Button2 As Button
    Friend WithEvents DateTimePickerFiltro As DateTimePicker
End Class
