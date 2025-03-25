<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormCategoriaEgreso
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
        Me.txtId = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtNombre = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.chkEstado = New System.Windows.Forms.CheckBox()
        Me.dgvCategorias = New System.Windows.Forms.DataGridView()
        Me.idcategoriaegreso = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.nombreCategoria = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.estadoCategoria = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.ButtonLimpiar = New System.Windows.Forms.Button()
        Me.btnGuardar = New System.Windows.Forms.Button()
        Me.chkMostrarInactivos = New System.Windows.Forms.CheckBox()
        Me.lblContador = New System.Windows.Forms.Label()
        Me.txtBuscar = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        CType(Me.dgvCategorias, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtId
        '
        Me.txtId.Location = New System.Drawing.Point(12, 23)
        Me.txtId.Name = "txtId"
        Me.txtId.ReadOnly = True
        Me.txtId.Size = New System.Drawing.Size(185, 20)
        Me.txtId.TabIndex = 14
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(9, 6)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(66, 13)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "Id Categoría"
        '
        'txtNombre
        '
        Me.txtNombre.Location = New System.Drawing.Point(236, 23)
        Me.txtNombre.Name = "txtNombre"
        Me.txtNombre.Size = New System.Drawing.Size(400, 20)
        Me.txtNombre.TabIndex = 16
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(233, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(93, 13)
        Me.Label1.TabIndex = 15
        Me.Label1.Text = "Nombre categoría"
        '
        'chkEstado
        '
        Me.chkEstado.AutoSize = True
        Me.chkEstado.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkEstado.Location = New System.Drawing.Point(685, 19)
        Me.chkEstado.Name = "chkEstado"
        Me.chkEstado.Size = New System.Drawing.Size(79, 24)
        Me.chkEstado.TabIndex = 17
        Me.chkEstado.Text = "Estado"
        Me.chkEstado.UseVisualStyleBackColor = True
        '
        'dgvCategorias
        '
        Me.dgvCategorias.AllowUserToAddRows = False
        Me.dgvCategorias.AllowUserToDeleteRows = False
        Me.dgvCategorias.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvCategorias.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvCategorias.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.idcategoriaegreso, Me.nombreCategoria, Me.estadoCategoria})
        Me.dgvCategorias.Location = New System.Drawing.Point(12, 120)
        Me.dgvCategorias.Name = "dgvCategorias"
        Me.dgvCategorias.ReadOnly = True
        Me.dgvCategorias.Size = New System.Drawing.Size(752, 225)
        Me.dgvCategorias.TabIndex = 18
        '
        'idcategoriaegreso
        '
        Me.idcategoriaegreso.HeaderText = "Id Categoría"
        Me.idcategoriaegreso.Name = "idcategoriaegreso"
        Me.idcategoriaegreso.ReadOnly = True
        '
        'nombreCategoria
        '
        Me.nombreCategoria.HeaderText = "Nombre Categoría"
        Me.nombreCategoria.Name = "nombreCategoria"
        Me.nombreCategoria.ReadOnly = True
        '
        'estadoCategoria
        '
        Me.estadoCategoria.HeaderText = "Estado"
        Me.estadoCategoria.Name = "estadoCategoria"
        Me.estadoCategoria.ReadOnly = True
        '
        'ButtonLimpiar
        '
        Me.ButtonLimpiar.Location = New System.Drawing.Point(451, 49)
        Me.ButtonLimpiar.Name = "ButtonLimpiar"
        Me.ButtonLimpiar.Size = New System.Drawing.Size(92, 23)
        Me.ButtonLimpiar.TabIndex = 20
        Me.ButtonLimpiar.Text = "Limpiar"
        Me.ButtonLimpiar.UseVisualStyleBackColor = True
        '
        'btnGuardar
        '
        Me.btnGuardar.Location = New System.Drawing.Point(549, 49)
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.Size = New System.Drawing.Size(87, 23)
        Me.btnGuardar.TabIndex = 19
        Me.btnGuardar.Text = "Guardar"
        Me.btnGuardar.UseVisualStyleBackColor = True
        '
        'chkMostrarInactivos
        '
        Me.chkMostrarInactivos.AutoSize = True
        Me.chkMostrarInactivos.Location = New System.Drawing.Point(236, 55)
        Me.chkMostrarInactivos.Name = "chkMostrarInactivos"
        Me.chkMostrarInactivos.Size = New System.Drawing.Size(106, 17)
        Me.chkMostrarInactivos.TabIndex = 21
        Me.chkMostrarInactivos.Text = "Mostrar inactivos"
        Me.chkMostrarInactivos.UseVisualStyleBackColor = True
        '
        'lblContador
        '
        Me.lblContador.BackColor = System.Drawing.Color.DarkBlue
        Me.lblContador.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblContador.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblContador.ForeColor = System.Drawing.Color.White
        Me.lblContador.Location = New System.Drawing.Point(0, 348)
        Me.lblContador.Name = "lblContador"
        Me.lblContador.Size = New System.Drawing.Size(776, 36)
        Me.lblContador.TabIndex = 22
        Me.lblContador.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtBuscar
        '
        Me.txtBuscar.Location = New System.Drawing.Point(12, 94)
        Me.txtBuscar.Name = "txtBuscar"
        Me.txtBuscar.Size = New System.Drawing.Size(624, 20)
        Me.txtBuscar.TabIndex = 24
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 77)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(40, 13)
        Me.Label3.TabIndex = 23
        Me.Label3.Text = "Buscar"
        '
        'FormCategoriaEgreso
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(776, 384)
        Me.Controls.Add(Me.txtBuscar)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lblContador)
        Me.Controls.Add(Me.chkMostrarInactivos)
        Me.Controls.Add(Me.ButtonLimpiar)
        Me.Controls.Add(Me.btnGuardar)
        Me.Controls.Add(Me.dgvCategorias)
        Me.Controls.Add(Me.chkEstado)
        Me.Controls.Add(Me.txtNombre)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtId)
        Me.Controls.Add(Me.Label6)
        Me.Name = "FormCategoriaEgreso"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Categoría de Egresos"
        CType(Me.dgvCategorias, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtId As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents txtNombre As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents chkEstado As CheckBox
    Friend WithEvents dgvCategorias As DataGridView
    Friend WithEvents ButtonLimpiar As Button
    Friend WithEvents btnGuardar As Button
    Friend WithEvents idcategoriaegreso As DataGridViewTextBoxColumn
    Friend WithEvents nombreCategoria As DataGridViewTextBoxColumn
    Friend WithEvents estadoCategoria As DataGridViewCheckBoxColumn
    Friend WithEvents chkMostrarInactivos As CheckBox
    Friend WithEvents lblContador As Label
    Friend WithEvents txtBuscar As TextBox
    Friend WithEvents Label3 As Label
End Class
