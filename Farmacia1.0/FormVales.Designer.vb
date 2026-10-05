<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormVales
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
        Me.lblTitulo = New System.Windows.Forms.Label()
        Me.lblSucursal = New System.Windows.Forms.Label()
        Me.lblVendedor = New System.Windows.Forms.Label()
        Me.ComboBoxVendedor = New System.Windows.Forms.ComboBox()
        Me.lblFecha = New System.Windows.Forms.Label()
        Me.lblBuscar = New System.Windows.Forms.Label()
        Me.txtbuscapro = New System.Windows.Forms.TextBox()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.codpro = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dpro = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.exist = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.marca = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.pres = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.preciopro = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.clNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.clCodigo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.clDescripcion = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.clCant = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.clPrecio = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.clSubt = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lblCodigo = New System.Windows.Forms.Label()
        Me.txtcodpro = New System.Windows.Forms.TextBox()
        Me.lblDescripcion = New System.Windows.Forms.Label()
        Me.txtdescpro = New System.Windows.Forms.TextBox()
        Me.lblPrecio = New System.Windows.Forms.Label()
        Me.txtprecio = New System.Windows.Forms.TextBox()
        Me.lblExistencia = New System.Windows.Forms.Label()
        Me.txtexistencia = New System.Windows.Forms.TextBox()
        Me.lblCantidad = New System.Windows.Forms.Label()
        Me.txtcantidad = New System.Windows.Forms.TextBox()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.txttotal = New System.Windows.Forms.TextBox()
        Me.btnAgregar = New System.Windows.Forms.Button()
        Me.btnQuitar = New System.Windows.Forms.Button()
        Me.btnDescartar = New System.Windows.Forms.Button()
        Me.btnGrabar = New System.Windows.Forms.Button()
        Me.btnSalir = New System.Windows.Forms.Button()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblTitulo
        '
        Me.lblTitulo.AutoSize = True
        Me.lblTitulo.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitulo.Location = New System.Drawing.Point(12, 9)
        Me.lblTitulo.Name = "lblTitulo"
        Me.lblTitulo.Size = New System.Drawing.Size(231, 26)
        Me.lblTitulo.TabIndex = 0
        Me.lblTitulo.Text = "Vale de mercadería"
        '
        'lblSucursal
        '
        Me.lblSucursal.AutoSize = True
        Me.lblSucursal.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSucursal.Location = New System.Drawing.Point(14, 45)
        Me.lblSucursal.Name = "lblSucursal"
        Me.lblSucursal.Size = New System.Drawing.Size(74, 18)
        Me.lblSucursal.TabIndex = 1
        Me.lblSucursal.Text = "Sucursal:"
        '
        'lblVendedor
        '
        Me.lblVendedor.AutoSize = True
        Me.lblVendedor.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVendedor.Location = New System.Drawing.Point(14, 76)
        Me.lblVendedor.Name = "lblVendedor"
        Me.lblVendedor.Size = New System.Drawing.Size(77, 18)
        Me.lblVendedor.TabIndex = 2
        Me.lblVendedor.Text = "Vendedor"
        '
        'ComboBoxVendedor
        '
        Me.ComboBoxVendedor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxVendedor.FormattingEnabled = True
        Me.ComboBoxVendedor.Location = New System.Drawing.Point(15, 97)
        Me.ComboBoxVendedor.Name = "ComboBoxVendedor"
        Me.ComboBoxVendedor.Size = New System.Drawing.Size(320, 21)
        Me.ComboBoxVendedor.TabIndex = 0
        '
        'lblFecha
        '
        Me.lblFecha.AutoSize = True
        Me.lblFecha.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFecha.Location = New System.Drawing.Point(360, 100)
        Me.lblFecha.Name = "lblFecha"
        Me.lblFecha.Size = New System.Drawing.Size(55, 18)
        Me.lblFecha.TabIndex = 4
        Me.lblFecha.Text = "Fecha:"
        '
        'lblBuscar
        '
        Me.lblBuscar.AutoSize = True
        Me.lblBuscar.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBuscar.Location = New System.Drawing.Point(14, 136)
        Me.lblBuscar.Name = "lblBuscar"
        Me.lblBuscar.Size = New System.Drawing.Size(128, 18)
        Me.lblBuscar.TabIndex = 5
        Me.lblBuscar.Text = "Buscar producto:"
        '
        'txtbuscapro
        '
        Me.txtbuscapro.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbuscapro.Location = New System.Drawing.Point(15, 157)
        Me.txtbuscapro.Name = "txtbuscapro"
        Me.txtbuscapro.Size = New System.Drawing.Size(560, 24)
        Me.txtbuscapro.TabIndex = 1
        Me.txtbuscapro.Tag = "ES"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AutoGenerateColumns = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.codpro, Me.dpro, Me.exist, Me.marca, Me.pres, Me.preciopro})
        Me.DataGridView1.Location = New System.Drawing.Point(15, 187)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(560, 280)
        Me.DataGridView1.TabIndex = 2
        '
        'codpro
        '
        Me.codpro.DataPropertyName = "idProducto"
        Me.codpro.HeaderText = "Código"
        Me.codpro.Name = "codpro"
        Me.codpro.ReadOnly = True
        Me.codpro.Width = 60
        '
        'dpro
        '
        Me.dpro.DataPropertyName = "dProducto"
        Me.dpro.HeaderText = "Descripción"
        Me.dpro.Name = "dpro"
        Me.dpro.ReadOnly = True
        Me.dpro.Width = 200
        '
        'exist
        '
        Me.exist.DataPropertyName = "Existencia"
        Me.exist.HeaderText = "Existencia"
        Me.exist.Name = "exist"
        Me.exist.ReadOnly = True
        Me.exist.Width = 70
        '
        'marca
        '
        Me.marca.DataPropertyName = "laboratorio"
        Me.marca.HeaderText = "Marca"
        Me.marca.Name = "marca"
        Me.marca.ReadOnly = True
        Me.marca.Width = 90
        '
        'pres
        '
        Me.pres.DataPropertyName = "presentacion"
        Me.pres.HeaderText = "Presentación"
        Me.pres.Name = "pres"
        Me.pres.ReadOnly = True
        Me.pres.Width = 90
        '
        'preciopro
        '
        Me.preciopro.DataPropertyName = "precio"
        Me.preciopro.HeaderText = "Precio"
        Me.preciopro.Name = "preciopro"
        Me.preciopro.ReadOnly = True
        Me.preciopro.Width = 60
        '
        'DataGridView2
        '
        Me.DataGridView2.AllowUserToAddRows = False
        Me.DataGridView2.AllowUserToDeleteRows = False
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.clNo, Me.clCodigo, Me.clDescripcion, Me.clCant, Me.clPrecio, Me.clSubt})
        Me.DataGridView2.Location = New System.Drawing.Point(590, 157)
        Me.DataGridView2.MultiSelect = False
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.ReadOnly = True
        Me.DataGridView2.RowHeadersVisible = False
        Me.DataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView2.Size = New System.Drawing.Size(495, 310)
        Me.DataGridView2.TabIndex = 3
        '
        'clNo
        '
        Me.clNo.HeaderText = "No"
        Me.clNo.Name = "clNo"
        Me.clNo.ReadOnly = True
        Me.clNo.Width = 35
        '
        'clCodigo
        '
        Me.clCodigo.HeaderText = "Código"
        Me.clCodigo.Name = "clCodigo"
        Me.clCodigo.ReadOnly = True
        Me.clCodigo.Width = 60
        '
        'clDescripcion
        '
        Me.clDescripcion.HeaderText = "Descripción"
        Me.clDescripcion.Name = "clDescripcion"
        Me.clDescripcion.ReadOnly = True
        Me.clDescripcion.Width = 200
        '
        'clCant
        '
        Me.clCant.HeaderText = "Cant."
        Me.clCant.Name = "clCant"
        Me.clCant.ReadOnly = True
        Me.clCant.Width = 50
        '
        'clPrecio
        '
        Me.clPrecio.HeaderText = "Precio"
        Me.clPrecio.Name = "clPrecio"
        Me.clPrecio.ReadOnly = True
        Me.clPrecio.Width = 65
        '
        'clSubt
        '
        Me.clSubt.HeaderText = "Importe"
        Me.clSubt.Name = "clSubt"
        Me.clSubt.ReadOnly = True
        Me.clSubt.Width = 70
        '
        'lblCodigo
        '
        Me.lblCodigo.AutoSize = True
        Me.lblCodigo.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCodigo.Location = New System.Drawing.Point(14, 478)
        Me.lblCodigo.Name = "lblCodigo"
        Me.lblCodigo.Size = New System.Drawing.Size(54, 17)
        Me.lblCodigo.TabIndex = 20
        Me.lblCodigo.Text = "Código"
        '
        'txtcodpro
        '
        Me.txtcodpro.Location = New System.Drawing.Point(15, 498)
        Me.txtcodpro.Name = "txtcodpro"
        Me.txtcodpro.ReadOnly = True
        Me.txtcodpro.Size = New System.Drawing.Size(80, 20)
        Me.txtcodpro.TabIndex = 21
        Me.txtcodpro.TabStop = False
        Me.txtcodpro.Tag = "ES"
        '
        'lblDescripcion
        '
        Me.lblDescripcion.AutoSize = True
        Me.lblDescripcion.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescripcion.Location = New System.Drawing.Point(105, 478)
        Me.lblDescripcion.Name = "lblDescripcion"
        Me.lblDescripcion.Size = New System.Drawing.Size(84, 17)
        Me.lblDescripcion.TabIndex = 22
        Me.lblDescripcion.Text = "Descripción"
        '
        'txtdescpro
        '
        Me.txtdescpro.Location = New System.Drawing.Point(105, 498)
        Me.txtdescpro.Name = "txtdescpro"
        Me.txtdescpro.ReadOnly = True
        Me.txtdescpro.Size = New System.Drawing.Size(320, 20)
        Me.txtdescpro.TabIndex = 23
        Me.txtdescpro.TabStop = False
        Me.txtdescpro.Tag = "ES"
        '
        'lblPrecio
        '
        Me.lblPrecio.AutoSize = True
        Me.lblPrecio.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPrecio.Location = New System.Drawing.Point(435, 478)
        Me.lblPrecio.Name = "lblPrecio"
        Me.lblPrecio.Size = New System.Drawing.Size(48, 17)
        Me.lblPrecio.TabIndex = 24
        Me.lblPrecio.Text = "Precio"
        '
        'txtprecio
        '
        Me.txtprecio.Location = New System.Drawing.Point(435, 498)
        Me.txtprecio.Name = "txtprecio"
        Me.txtprecio.ReadOnly = True
        Me.txtprecio.Size = New System.Drawing.Size(80, 20)
        Me.txtprecio.TabIndex = 25
        Me.txtprecio.TabStop = False
        Me.txtprecio.Tag = "ES"
        Me.txtprecio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblExistencia
        '
        Me.lblExistencia.AutoSize = True
        Me.lblExistencia.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblExistencia.Location = New System.Drawing.Point(525, 478)
        Me.lblExistencia.Name = "lblExistencia"
        Me.lblExistencia.Size = New System.Drawing.Size(70, 17)
        Me.lblExistencia.TabIndex = 26
        Me.lblExistencia.Text = "Existencia"
        '
        'txtexistencia
        '
        Me.txtexistencia.Location = New System.Drawing.Point(525, 498)
        Me.txtexistencia.Name = "txtexistencia"
        Me.txtexistencia.ReadOnly = True
        Me.txtexistencia.Size = New System.Drawing.Size(70, 20)
        Me.txtexistencia.TabIndex = 27
        Me.txtexistencia.TabStop = False
        Me.txtexistencia.Tag = "ES"
        Me.txtexistencia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblCantidad
        '
        Me.lblCantidad.AutoSize = True
        Me.lblCantidad.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCantidad.Location = New System.Drawing.Point(605, 478)
        Me.lblCantidad.Name = "lblCantidad"
        Me.lblCantidad.Size = New System.Drawing.Size(64, 17)
        Me.lblCantidad.TabIndex = 28
        Me.lblCantidad.Text = "Cantidad"
        '
        'txtcantidad
        '
        Me.txtcantidad.Location = New System.Drawing.Point(605, 498)
        Me.txtcantidad.Name = "txtcantidad"
        Me.txtcantidad.Size = New System.Drawing.Size(70, 20)
        Me.txtcantidad.TabIndex = 4
        Me.txtcantidad.Tag = "ES"
        Me.txtcantidad.Text = "1"
        Me.txtcantidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblTotal
        '
        Me.lblTotal.AutoSize = True
        Me.lblTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotal.Location = New System.Drawing.Point(885, 478)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(60, 17)
        Me.lblTotal.TabIndex = 30
        Me.lblTotal.Text = "Total Q."
        '
        'txttotal
        '
        Me.txttotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txttotal.Location = New System.Drawing.Point(885, 496)
        Me.txttotal.Name = "txttotal"
        Me.txttotal.ReadOnly = True
        Me.txttotal.Size = New System.Drawing.Size(200, 32)
        Me.txttotal.TabIndex = 31
        Me.txttotal.TabStop = False
        Me.txttotal.Tag = "E"
        Me.txttotal.Text = "0.00"
        Me.txttotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btnAgregar
        '
        Me.btnAgregar.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAgregar.Location = New System.Drawing.Point(15, 545)
        Me.btnAgregar.Name = "btnAgregar"
        Me.btnAgregar.Size = New System.Drawing.Size(110, 36)
        Me.btnAgregar.TabIndex = 5
        Me.btnAgregar.Tag = "WB"
        Me.btnAgregar.Text = "Agregar"
        Me.btnAgregar.UseVisualStyleBackColor = True
        '
        'btnQuitar
        '
        Me.btnQuitar.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnQuitar.Location = New System.Drawing.Point(131, 545)
        Me.btnQuitar.Name = "btnQuitar"
        Me.btnQuitar.Size = New System.Drawing.Size(110, 36)
        Me.btnQuitar.TabIndex = 6
        Me.btnQuitar.Tag = "WB"
        Me.btnQuitar.Text = "Quitar"
        Me.btnQuitar.UseVisualStyleBackColor = True
        '
        'btnDescartar
        '
        Me.btnDescartar.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDescartar.Location = New System.Drawing.Point(247, 545)
        Me.btnDescartar.Name = "btnDescartar"
        Me.btnDescartar.Size = New System.Drawing.Size(110, 36)
        Me.btnDescartar.TabIndex = 7
        Me.btnDescartar.Tag = "WB"
        Me.btnDescartar.Text = "Descartar"
        Me.btnDescartar.UseVisualStyleBackColor = True
        '
        'btnGrabar
        '
        Me.btnGrabar.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGrabar.Location = New System.Drawing.Point(750, 545)
        Me.btnGrabar.Name = "btnGrabar"
        Me.btnGrabar.Size = New System.Drawing.Size(220, 36)
        Me.btnGrabar.TabIndex = 8
        Me.btnGrabar.Tag = "DB"
        Me.btnGrabar.Text = "Registrar vale"
        Me.btnGrabar.UseVisualStyleBackColor = True
        '
        'btnSalir
        '
        Me.btnSalir.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSalir.Location = New System.Drawing.Point(976, 545)
        Me.btnSalir.Name = "btnSalir"
        Me.btnSalir.Size = New System.Drawing.Size(109, 36)
        Me.btnSalir.TabIndex = 9
        Me.btnSalir.Tag = "WB"
        Me.btnSalir.Text = "Salir"
        Me.btnSalir.UseVisualStyleBackColor = True
        '
        'FormVales
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1100, 600)
        Me.Controls.Add(Me.btnSalir)
        Me.Controls.Add(Me.btnGrabar)
        Me.Controls.Add(Me.btnDescartar)
        Me.Controls.Add(Me.btnQuitar)
        Me.Controls.Add(Me.btnAgregar)
        Me.Controls.Add(Me.txttotal)
        Me.Controls.Add(Me.lblTotal)
        Me.Controls.Add(Me.txtcantidad)
        Me.Controls.Add(Me.lblCantidad)
        Me.Controls.Add(Me.txtexistencia)
        Me.Controls.Add(Me.lblExistencia)
        Me.Controls.Add(Me.txtprecio)
        Me.Controls.Add(Me.lblPrecio)
        Me.Controls.Add(Me.txtdescpro)
        Me.Controls.Add(Me.lblDescripcion)
        Me.Controls.Add(Me.txtcodpro)
        Me.Controls.Add(Me.lblCodigo)
        Me.Controls.Add(Me.DataGridView2)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.txtbuscapro)
        Me.Controls.Add(Me.lblBuscar)
        Me.Controls.Add(Me.lblFecha)
        Me.Controls.Add(Me.ComboBoxVendedor)
        Me.Controls.Add(Me.lblVendedor)
        Me.Controls.Add(Me.lblSucursal)
        Me.Controls.Add(Me.lblTitulo)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "FormVales"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Vale de mercadería"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblTitulo As Label
    Friend WithEvents lblSucursal As Label
    Friend WithEvents lblVendedor As Label
    Friend WithEvents ComboBoxVendedor As ComboBox
    Friend WithEvents lblFecha As Label
    Friend WithEvents lblBuscar As Label
    Friend WithEvents txtbuscapro As TextBox
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents codpro As DataGridViewTextBoxColumn
    Friend WithEvents dpro As DataGridViewTextBoxColumn
    Friend WithEvents exist As DataGridViewTextBoxColumn
    Friend WithEvents marca As DataGridViewTextBoxColumn
    Friend WithEvents pres As DataGridViewTextBoxColumn
    Friend WithEvents preciopro As DataGridViewTextBoxColumn
    Friend WithEvents DataGridView2 As DataGridView
    Friend WithEvents clNo As DataGridViewTextBoxColumn
    Friend WithEvents clCodigo As DataGridViewTextBoxColumn
    Friend WithEvents clDescripcion As DataGridViewTextBoxColumn
    Friend WithEvents clCant As DataGridViewTextBoxColumn
    Friend WithEvents clPrecio As DataGridViewTextBoxColumn
    Friend WithEvents clSubt As DataGridViewTextBoxColumn
    Friend WithEvents lblCodigo As Label
    Friend WithEvents txtcodpro As TextBox
    Friend WithEvents lblDescripcion As Label
    Friend WithEvents txtdescpro As TextBox
    Friend WithEvents lblPrecio As Label
    Friend WithEvents txtprecio As TextBox
    Friend WithEvents lblExistencia As Label
    Friend WithEvents txtexistencia As TextBox
    Friend WithEvents lblCantidad As Label
    Friend WithEvents txtcantidad As TextBox
    Friend WithEvents lblTotal As Label
    Friend WithEvents txttotal As TextBox
    Friend WithEvents btnAgregar As Button
    Friend WithEvents btnQuitar As Button
    Friend WithEvents btnDescartar As Button
    Friend WithEvents btnGrabar As Button
    Friend WithEvents btnSalir As Button
End Class
