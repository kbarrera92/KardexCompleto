<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormVerVales
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
        Me.lblSucursal = New System.Windows.Forms.Label()
        Me.ComboBoxSucursal = New System.Windows.Forms.ComboBox()
        Me.lblVendedor = New System.Windows.Forms.Label()
        Me.ComboBoxVendedor = New System.Windows.Forms.ComboBox()
        Me.lblEstado = New System.Windows.Forms.Label()
        Me.ComboBoxEstado = New System.Windows.Forms.ComboBox()
        Me.btnBuscar = New System.Windows.Forms.Button()
        Me.btnSalir = New System.Windows.Forms.Button()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.lblDetalle = New System.Windows.Forms.Label()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.colPagar = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.lblSeleccionado = New System.Windows.Forms.Label()
        Me.lblSaldoVale = New System.Windows.Forms.Label()
        Me.btnPagarSeleccionados = New System.Windows.Forms.Button()
        Me.btnPagarTodo = New System.Windows.Forms.Button()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblSucursal
        '
        Me.lblSucursal.AutoSize = True
        Me.lblSucursal.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSucursal.Location = New System.Drawing.Point(14, 9)
        Me.lblSucursal.Name = "lblSucursal"
        Me.lblSucursal.Size = New System.Drawing.Size(71, 17)
        Me.lblSucursal.TabIndex = 0
        Me.lblSucursal.Text = "Sucursal"
        '
        'ComboBoxSucursal
        '
        Me.ComboBoxSucursal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxSucursal.FormattingEnabled = True
        Me.ComboBoxSucursal.Location = New System.Drawing.Point(15, 30)
        Me.ComboBoxSucursal.Name = "ComboBoxSucursal"
        Me.ComboBoxSucursal.Size = New System.Drawing.Size(220, 21)
        Me.ComboBoxSucursal.TabIndex = 0
        '
        'lblVendedor
        '
        Me.lblVendedor.AutoSize = True
        Me.lblVendedor.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVendedor.Location = New System.Drawing.Point(250, 9)
        Me.lblVendedor.Name = "lblVendedor"
        Me.lblVendedor.Size = New System.Drawing.Size(78, 17)
        Me.lblVendedor.TabIndex = 2
        Me.lblVendedor.Text = "Vendedor"
        '
        'ComboBoxVendedor
        '
        Me.ComboBoxVendedor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxVendedor.FormattingEnabled = True
        Me.ComboBoxVendedor.Location = New System.Drawing.Point(251, 30)
        Me.ComboBoxVendedor.Name = "ComboBoxVendedor"
        Me.ComboBoxVendedor.Size = New System.Drawing.Size(250, 21)
        Me.ComboBoxVendedor.TabIndex = 1
        '
        'lblEstado
        '
        Me.lblEstado.AutoSize = True
        Me.lblEstado.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEstado.Location = New System.Drawing.Point(516, 9)
        Me.lblEstado.Name = "lblEstado"
        Me.lblEstado.Size = New System.Drawing.Size(58, 17)
        Me.lblEstado.TabIndex = 4
        Me.lblEstado.Text = "Estado"
        '
        'ComboBoxEstado
        '
        Me.ComboBoxEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxEstado.FormattingEnabled = True
        Me.ComboBoxEstado.Items.AddRange(New Object() {"Todos", "PENDIENTE", "VENCIDO", "PAGADO"})
        Me.ComboBoxEstado.Location = New System.Drawing.Point(517, 30)
        Me.ComboBoxEstado.Name = "ComboBoxEstado"
        Me.ComboBoxEstado.Size = New System.Drawing.Size(150, 21)
        Me.ComboBoxEstado.TabIndex = 2
        '
        'btnBuscar
        '
        Me.btnBuscar.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBuscar.Location = New System.Drawing.Point(685, 24)
        Me.btnBuscar.Name = "btnBuscar"
        Me.btnBuscar.Size = New System.Drawing.Size(100, 32)
        Me.btnBuscar.TabIndex = 3
        Me.btnBuscar.Tag = "DB"
        Me.btnBuscar.Text = "Buscar"
        Me.btnBuscar.UseVisualStyleBackColor = True
        '
        'btnSalir
        '
        Me.btnSalir.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSalir.Location = New System.Drawing.Point(985, 24)
        Me.btnSalir.Name = "btnSalir"
        Me.btnSalir.Size = New System.Drawing.Size(100, 32)
        Me.btnSalir.TabIndex = 4
        Me.btnSalir.Tag = "WB"
        Me.btnSalir.Text = "Salir"
        Me.btnSalir.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(15, 70)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(1070, 215)
        Me.DataGridView1.TabIndex = 5
        '
        'lblDetalle
        '
        Me.lblDetalle.AutoSize = True
        Me.lblDetalle.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDetalle.Location = New System.Drawing.Point(14, 296)
        Me.lblDetalle.Name = "lblDetalle"
        Me.lblDetalle.Size = New System.Drawing.Size(299, 17)
        Me.lblDetalle.TabIndex = 6
        Me.lblDetalle.Text = "Productos del vale (marque para pagar)"
        '
        'DataGridView2
        '
        Me.DataGridView2.AllowUserToAddRows = False
        Me.DataGridView2.AllowUserToDeleteRows = False
        Me.DataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colPagar})
        Me.DataGridView2.Location = New System.Drawing.Point(15, 318)
        Me.DataGridView2.MultiSelect = False
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.RowHeadersVisible = False
        Me.DataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView2.Size = New System.Drawing.Size(1070, 200)
        Me.DataGridView2.TabIndex = 6
        '
        'colPagar
        '
        Me.colPagar.HeaderText = "Pagar"
        Me.colPagar.Name = "colPagar"
        '
        'lblSeleccionado
        '
        Me.lblSeleccionado.AutoSize = True
        Me.lblSeleccionado.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSeleccionado.Location = New System.Drawing.Point(14, 536)
        Me.lblSeleccionado.Name = "lblSeleccionado"
        Me.lblSeleccionado.Size = New System.Drawing.Size(232, 20)
        Me.lblSeleccionado.TabIndex = 8
        Me.lblSeleccionado.Text = "Monto seleccionado: Q 0.00"
        '
        'lblSaldoVale
        '
        Me.lblSaldoVale.AutoSize = True
        Me.lblSaldoVale.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSaldoVale.Location = New System.Drawing.Point(14, 566)
        Me.lblSaldoVale.Name = "lblSaldoVale"
        Me.lblSaldoVale.Size = New System.Drawing.Size(184, 20)
        Me.lblSaldoVale.TabIndex = 9
        Me.lblSaldoVale.Text = "Saldo del vale: Q 0.00"
        '
        'btnPagarSeleccionados
        '
        Me.btnPagarSeleccionados.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPagarSeleccionados.Location = New System.Drawing.Point(600, 534)
        Me.btnPagarSeleccionados.Name = "btnPagarSeleccionados"
        Me.btnPagarSeleccionados.Size = New System.Drawing.Size(230, 44)
        Me.btnPagarSeleccionados.TabIndex = 7
        Me.btnPagarSeleccionados.Tag = "DB"
        Me.btnPagarSeleccionados.Text = "Pagar seleccionados"
        Me.btnPagarSeleccionados.UseVisualStyleBackColor = True
        '
        'btnPagarTodo
        '
        Me.btnPagarTodo.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPagarTodo.Location = New System.Drawing.Point(840, 534)
        Me.btnPagarTodo.Name = "btnPagarTodo"
        Me.btnPagarTodo.Size = New System.Drawing.Size(245, 44)
        Me.btnPagarTodo.TabIndex = 8
        Me.btnPagarTodo.Tag = "DB"
        Me.btnPagarTodo.Text = "Pagar todo lo pendiente"
        Me.btnPagarTodo.UseVisualStyleBackColor = True
        '
        'FormVerVales
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1100, 600)
        Me.Controls.Add(Me.btnPagarTodo)
        Me.Controls.Add(Me.btnPagarSeleccionados)
        Me.Controls.Add(Me.lblSaldoVale)
        Me.Controls.Add(Me.lblSeleccionado)
        Me.Controls.Add(Me.DataGridView2)
        Me.Controls.Add(Me.lblDetalle)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.btnSalir)
        Me.Controls.Add(Me.btnBuscar)
        Me.Controls.Add(Me.ComboBoxEstado)
        Me.Controls.Add(Me.lblEstado)
        Me.Controls.Add(Me.ComboBoxVendedor)
        Me.Controls.Add(Me.lblVendedor)
        Me.Controls.Add(Me.ComboBoxSucursal)
        Me.Controls.Add(Me.lblSucursal)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "FormVerVales"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Consulta y pago de vales"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblSucursal As Label
    Friend WithEvents ComboBoxSucursal As ComboBox
    Friend WithEvents lblVendedor As Label
    Friend WithEvents ComboBoxVendedor As ComboBox
    Friend WithEvents lblEstado As Label
    Friend WithEvents ComboBoxEstado As ComboBox
    Friend WithEvents btnBuscar As Button
    Friend WithEvents btnSalir As Button
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents colIdVale As DataGridViewTextBoxColumn
    Friend WithEvents colFecha As DataGridViewTextBoxColumn
    Friend WithEvents colVence As DataGridViewTextBoxColumn
    Friend WithEvents colSucursal As DataGridViewTextBoxColumn
    Friend WithEvents colVendedor As DataGridViewTextBoxColumn
    Friend WithEvents colTotal As DataGridViewTextBoxColumn
    Friend WithEvents colSaldo As DataGridViewTextBoxColumn
    Friend WithEvents colEstadoVale As DataGridViewTextBoxColumn
    Friend WithEvents lblDetalle As Label
    Friend WithEvents DataGridView2 As DataGridView
    Friend WithEvents colPagar As DataGridViewCheckBoxColumn
    Friend WithEvents colNDetalle As DataGridViewTextBoxColumn
    Friend WithEvents colProducto As DataGridViewTextBoxColumn
    Friend WithEvents colDProducto As DataGridViewTextBoxColumn
    Friend WithEvents colCantidad As DataGridViewTextBoxColumn
    Friend WithEvents colPrecio As DataGridViewTextBoxColumn
    Friend WithEvents colSubtotal As DataGridViewTextBoxColumn
    Friend WithEvents colPagado As DataGridViewCheckBoxColumn
    Friend WithEvents lblSeleccionado As Label
    Friend WithEvents lblSaldoVale As Label
    Friend WithEvents btnPagarSeleccionados As Button
    Friend WithEvents btnPagarTodo As Button
End Class
