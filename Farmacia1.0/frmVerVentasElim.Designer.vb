<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmVerVentasElim
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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.nventa = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.fecha = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.usuario = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.total = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.documento = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.suc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cliente = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.efec = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.tarj = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.aut = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.fechElim = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.nventa, Me.fecha, Me.usuario, Me.total, Me.documento, Me.suc, Me.cliente, Me.efec, Me.tarj, Me.aut, Me.fechElim})
        Me.DataGridView1.Location = New System.Drawing.Point(12, 21)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(1223, 446)
        Me.DataGridView1.TabIndex = 7
        '
        'nventa
        '
        Me.nventa.HeaderText = "No. de Venta"
        Me.nventa.Name = "nventa"
        Me.nventa.ReadOnly = True
        Me.nventa.Width = 107
        '
        'fecha
        '
        Me.fecha.HeaderText = "Fecha"
        Me.fecha.Name = "fecha"
        Me.fecha.ReadOnly = True
        Me.fecha.Width = 108
        '
        'usuario
        '
        Me.usuario.HeaderText = "Usuario"
        Me.usuario.Name = "usuario"
        Me.usuario.ReadOnly = True
        Me.usuario.Width = 150
        '
        'total
        '
        Me.total.HeaderText = "Total"
        Me.total.Name = "total"
        Me.total.ReadOnly = True
        Me.total.Width = 107
        '
        'documento
        '
        Me.documento.HeaderText = "Factura"
        Me.documento.Name = "documento"
        Me.documento.ReadOnly = True
        Me.documento.Width = 107
        '
        'suc
        '
        Me.suc.HeaderText = "Sucursal"
        Me.suc.Name = "suc"
        Me.suc.ReadOnly = True
        Me.suc.Width = 200
        '
        'cliente
        '
        Me.cliente.HeaderText = "Cliente"
        Me.cliente.Name = "cliente"
        Me.cliente.ReadOnly = True
        Me.cliente.Width = 200
        '
        'efec
        '
        Me.efec.HeaderText = "Efectivo"
        Me.efec.Name = "efec"
        Me.efec.ReadOnly = True
        Me.efec.Width = 107
        '
        'tarj
        '
        Me.tarj.HeaderText = "Tarjeta"
        Me.tarj.Name = "tarj"
        Me.tarj.ReadOnly = True
        Me.tarj.Width = 107
        '
        'aut
        '
        Me.aut.HeaderText = "Autorización"
        Me.aut.Name = "aut"
        Me.aut.ReadOnly = True
        Me.aut.Width = 108
        '
        'fechElim
        '
        Me.fechElim.HeaderText = "Fecha de Eliminación"
        Me.fechElim.Name = "fechElim"
        Me.fechElim.ReadOnly = True
        Me.fechElim.Width = 107
        '
        'frmVerVentasElim
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1247, 480)
        Me.Controls.Add(Me.DataGridView1)
        Me.Name = "frmVerVentasElim"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Listado de ventas eliminadas"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents nventa As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents fecha As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents usuario As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents total As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents documento As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents suc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cliente As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents efec As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents tarj As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents aut As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents fechElim As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
