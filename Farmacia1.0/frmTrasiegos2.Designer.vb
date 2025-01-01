<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTrasiegos2
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.codEnt = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cantEnt = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.descEnt = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.presEnt = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.umedEnt = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.codSal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cantSal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.descSal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.presSal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.umedSal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ShapeContainer1 = New Microsoft.VisualBasic.PowerPacks.ShapeContainer()
        Me.RectangleShape2 = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.RectangleShape1 = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtItemsST = New System.Windows.Forms.TextBox()
        Me.txtItemsET = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtCantProST = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtCantProdET = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel8.SuspendLayout()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel6.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel7.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel5.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Location = New System.Drawing.Point(12, 12)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1197, 516)
        Me.Panel1.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Panel4)
        Me.Panel2.Controls.Add(Me.Panel3)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1197, 516)
        Me.Panel2.TabIndex = 0
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.Panel8)
        Me.Panel4.Controls.Add(Me.Panel6)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel4.Location = New System.Drawing.Point(598, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(599, 516)
        Me.Panel4.TabIndex = 1
        '
        'Panel8
        '
        Me.Panel8.Controls.Add(Me.DataGridView2)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel8.Location = New System.Drawing.Point(0, 45)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(599, 471)
        Me.Panel8.TabIndex = 1
        '
        'DataGridView2
        '
        Me.DataGridView2.AllowUserToAddRows = False
        Me.DataGridView2.AllowUserToDeleteRows = False
        Me.DataGridView2.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.DarkOliveGreen
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView2.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.codEnt, Me.cantEnt, Me.descEnt, Me.presEnt, Me.umedEnt})
        Me.DataGridView2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView2.EnableHeadersVisualStyles = False
        Me.DataGridView2.Location = New System.Drawing.Point(0, 0)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.ReadOnly = True
        Me.DataGridView2.RowHeadersVisible = False
        Me.DataGridView2.Size = New System.Drawing.Size(599, 471)
        Me.DataGridView2.TabIndex = 0
        '
        'codEnt
        '
        Me.codEnt.HeaderText = "CÓDIGO"
        Me.codEnt.Name = "codEnt"
        Me.codEnt.ReadOnly = True
        Me.codEnt.Width = 70
        '
        'cantEnt
        '
        Me.cantEnt.HeaderText = "CANTIDAD"
        Me.cantEnt.Name = "cantEnt"
        Me.cantEnt.ReadOnly = True
        Me.cantEnt.Width = 75
        '
        'descEnt
        '
        Me.descEnt.HeaderText = "DESCRIPCIÓN"
        Me.descEnt.Name = "descEnt"
        Me.descEnt.ReadOnly = True
        Me.descEnt.Width = 230
        '
        'presEnt
        '
        Me.presEnt.HeaderText = "PRESENTACIÓN"
        Me.presEnt.Name = "presEnt"
        Me.presEnt.ReadOnly = True
        Me.presEnt.Width = 115
        '
        'umedEnt
        '
        Me.umedEnt.HeaderText = "UMEDIDA"
        Me.umedEnt.Name = "umedEnt"
        Me.umedEnt.ReadOnly = True
        Me.umedEnt.Width = 105
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.Label2)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Location = New System.Drawing.Point(0, 0)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(599, 45)
        Me.Panel6.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(13, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(579, 29)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Presione F8 para agregar productos a la entrada"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Panel7)
        Me.Panel3.Controls.Add(Me.Panel5)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(594, 516)
        Me.Panel3.TabIndex = 0
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.DataGridView1)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel7.Location = New System.Drawing.Point(0, 45)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(594, 471)
        Me.Panel7.TabIndex = 1
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.DarkOliveGreen
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.codSal, Me.cantSal, Me.descSal, Me.presSal, Me.umedSal})
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.EnableHeadersVisualStyles = False
        Me.DataGridView1.Location = New System.Drawing.Point(0, 0)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.Size = New System.Drawing.Size(594, 471)
        Me.DataGridView1.TabIndex = 0
        '
        'codSal
        '
        Me.codSal.HeaderText = "CÓDIGO"
        Me.codSal.Name = "codSal"
        Me.codSal.ReadOnly = True
        Me.codSal.Width = 70
        '
        'cantSal
        '
        Me.cantSal.HeaderText = "CANTIDAD"
        Me.cantSal.Name = "cantSal"
        Me.cantSal.ReadOnly = True
        Me.cantSal.Width = 75
        '
        'descSal
        '
        Me.descSal.HeaderText = "DESCRIPCIÓN"
        Me.descSal.Name = "descSal"
        Me.descSal.ReadOnly = True
        Me.descSal.Width = 230
        '
        'presSal
        '
        Me.presSal.HeaderText = "PRESENTACIÓN"
        Me.presSal.Name = "presSal"
        Me.presSal.ReadOnly = True
        Me.presSal.Width = 115
        '
        'umedSal
        '
        Me.umedSal.HeaderText = "UMEDIDA"
        Me.umedSal.Name = "umedSal"
        Me.umedSal.ReadOnly = True
        Me.umedSal.Width = 105
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.Label1)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(0, 0)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(594, 45)
        Me.Panel5.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(13, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(561, 29)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Presione F5 para agregar productos a la salida"
        '
        'ShapeContainer1
        '
        Me.ShapeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.ShapeContainer1.Margin = New System.Windows.Forms.Padding(0)
        Me.ShapeContainer1.Name = "ShapeContainer1"
        Me.ShapeContainer1.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {Me.RectangleShape2, Me.RectangleShape1})
        Me.ShapeContainer1.Size = New System.Drawing.Size(1221, 664)
        Me.ShapeContainer1.TabIndex = 1
        Me.ShapeContainer1.TabStop = False
        '
        'RectangleShape2
        '
        Me.RectangleShape2.BackColor = System.Drawing.Color.DarkOliveGreen
        Me.RectangleShape2.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque
        Me.RectangleShape2.BorderColor = System.Drawing.Color.White
        Me.RectangleShape2.BorderWidth = 2
        Me.RectangleShape2.CornerRadius = 5
        Me.RectangleShape2.Location = New System.Drawing.Point(401, 593)
        Me.RectangleShape2.Name = "RectangleShape2"
        Me.RectangleShape2.Size = New System.Drawing.Size(419, 47)
        '
        'RectangleShape1
        '
        Me.RectangleShape1.BorderColor = System.Drawing.Color.White
        Me.RectangleShape1.BorderWidth = 2
        Me.RectangleShape1.CornerRadius = 5
        Me.RectangleShape1.Location = New System.Drawing.Point(470, 539)
        Me.RectangleShape1.Name = "RectangleShape1"
        Me.RectangleShape1.Size = New System.Drawing.Size(280, 47)
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(545, 548)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(131, 29)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "01/01/2019"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.DarkOliveGreen
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(491, 603)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(238, 29)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "IMPRIME Y GRABA"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.Location = New System.Drawing.Point(12, 545)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(95, 31)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Items:"
        '
        'txtItemsST
        '
        Me.txtItemsST.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemsST.Location = New System.Drawing.Point(12, 580)
        Me.txtItemsST.Name = "txtItemsST"
        Me.txtItemsST.ReadOnly = True
        Me.txtItemsST.Size = New System.Drawing.Size(95, 38)
        Me.txtItemsST.TabIndex = 5
        Me.txtItemsST.Text = "0"
        Me.txtItemsST.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtItemsET
        '
        Me.txtItemsET.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemsET.Location = New System.Drawing.Point(1114, 580)
        Me.txtItemsET.Name = "txtItemsET"
        Me.txtItemsET.ReadOnly = True
        Me.txtItemsET.Size = New System.Drawing.Size(95, 38)
        Me.txtItemsET.TabIndex = 7
        Me.txtItemsET.Text = "0"
        Me.txtItemsET.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.White
        Me.Label6.Location = New System.Drawing.Point(1114, 545)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(95, 31)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "Items:"
        '
        'txtCantProST
        '
        Me.txtCantProST.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCantProST.Location = New System.Drawing.Point(182, 580)
        Me.txtCantProST.Name = "txtCantProST"
        Me.txtCantProST.Size = New System.Drawing.Size(140, 38)
        Me.txtCantProST.TabIndex = 9
        Me.txtCantProST.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.White
        Me.Label7.Location = New System.Drawing.Point(182, 545)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(140, 31)
        Me.Label7.TabIndex = 8
        Me.Label7.Text = "Cantidad:"
        '
        'txtCantProdET
        '
        Me.txtCantProdET.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCantProdET.Location = New System.Drawing.Point(899, 580)
        Me.txtCantProdET.Name = "txtCantProdET"
        Me.txtCantProdET.Size = New System.Drawing.Size(140, 38)
        Me.txtCantProdET.TabIndex = 11
        Me.txtCantProdET.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.White
        Me.Label8.Location = New System.Drawing.Point(899, 545)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(140, 31)
        Me.Label8.TabIndex = 10
        Me.Label8.Text = "Cantidad:"
        '
        'frmTrasiegos2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.RoyalBlue
        Me.ClientSize = New System.Drawing.Size(1221, 664)
        Me.Controls.Add(Me.txtCantProdET)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtCantProST)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtItemsET)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtItemsST)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.ShapeContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.Name = "frmTrasiegos2"
        Me.Opacity = 0.9R
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmTrasiegos2"
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ShapeContainer1 As Microsoft.VisualBasic.PowerPacks.ShapeContainer
    Friend WithEvents RectangleShape1 As Microsoft.VisualBasic.PowerPacks.RectangleShape
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents RectangleShape2 As Microsoft.VisualBasic.PowerPacks.RectangleShape
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtItemsST As System.Windows.Forms.TextBox
    Friend WithEvents txtItemsET As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents codEnt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cantEnt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents descEnt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents presEnt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents umedEnt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents codSal As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cantSal As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents descSal As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents presSal As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents umedSal As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents txtCantProST As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtCantProdET As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
End Class
