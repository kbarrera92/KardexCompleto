<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMenu
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
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.ShapeContainer1 = New Microsoft.VisualBasic.PowerPacks.ShapeContainer()
        Me.RectangleShape1 = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnRecibirTraslado = New System.Windows.Forms.Button()
        Me.Button11 = New System.Windows.Forms.Button()
        Me.Button12 = New System.Windows.Forms.Button()
        Me.Button9 = New System.Windows.Forms.Button()
        Me.Button8 = New System.Windows.Forms.Button()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.btnTraslados = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1212, 232)
        Me.Panel1.TabIndex = 0
        '
        'PictureBox1
        '
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox1.Image = Global.Farmacia1._0.My.Resources.Resources.farma
        Me.PictureBox1.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(1212, 232)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'ShapeContainer1
        '
        Me.ShapeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.ShapeContainer1.Margin = New System.Windows.Forms.Padding(0)
        Me.ShapeContainer1.Name = "ShapeContainer1"
        Me.ShapeContainer1.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {Me.RectangleShape1})
        Me.ShapeContainer1.Size = New System.Drawing.Size(1212, 611)
        Me.ShapeContainer1.TabIndex = 1
        Me.ShapeContainer1.TabStop = False
        '
        'RectangleShape1
        '
        Me.RectangleShape1.BorderColor = System.Drawing.Color.MediumBlue
        Me.RectangleShape1.BorderWidth = 3
        Me.RectangleShape1.CornerRadius = 5
        Me.RectangleShape1.Location = New System.Drawing.Point(63, 258)
        Me.RectangleShape1.Name = "RectangleShape1"
        Me.RectangleShape1.Size = New System.Drawing.Size(1086, 323)
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Bahnschrift", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.MediumBlue
        Me.Label1.Location = New System.Drawing.Point(101, 243)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(204, 29)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Menú de opciones"
        '
        'btnRecibirTraslado
        '
        Me.btnRecibirTraslado.Enabled = False
        Me.btnRecibirTraslado.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnRecibirTraslado.FlatAppearance.BorderSize = 2
        Me.btnRecibirTraslado.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DeepSkyBlue
        Me.btnRecibirTraslado.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRecibirTraslado.Font = New System.Drawing.Font("Bahnschrift", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRecibirTraslado.ForeColor = System.Drawing.Color.Blue
        Me.btnRecibirTraslado.Image = Global.Farmacia1._0.My.Resources.Resources.Folder_Data_icon
        Me.btnRecibirTraslado.Location = New System.Drawing.Point(778, 433)
        Me.btnRecibirTraslado.Name = "btnRecibirTraslado"
        Me.btnRecibirTraslado.Size = New System.Drawing.Size(145, 122)
        Me.btnRecibirTraslado.TabIndex = 3
        Me.btnRecibirTraslado.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnRecibirTraslado.UseVisualStyleBackColor = True
        '
        'Button11
        '
        Me.Button11.Enabled = False
        Me.Button11.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.Button11.FlatAppearance.BorderSize = 2
        Me.Button11.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DeepSkyBlue
        Me.Button11.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button11.Font = New System.Drawing.Font("Bahnschrift", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button11.ForeColor = System.Drawing.Color.Blue
        Me.Button11.Image = Global.Farmacia1._0.My.Resources.Resources.SEO_icon
        Me.Button11.Location = New System.Drawing.Point(941, 291)
        Me.Button11.Name = "Button11"
        Me.Button11.Size = New System.Drawing.Size(145, 122)
        Me.Button11.TabIndex = 5
        Me.Button11.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.Button11.UseVisualStyleBackColor = True
        '
        'Button12
        '
        Me.Button12.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.Button12.FlatAppearance.BorderSize = 2
        Me.Button12.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DeepSkyBlue
        Me.Button12.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button12.Font = New System.Drawing.Font("Bahnschrift", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button12.ForeColor = System.Drawing.Color.Blue
        Me.Button12.Image = Global.Farmacia1._0.My.Resources.Resources.Close_2_icon
        Me.Button12.Location = New System.Drawing.Point(941, 433)
        Me.Button12.Name = "Button12"
        Me.Button12.Size = New System.Drawing.Size(145, 122)
        Me.Button12.TabIndex = 3
        Me.Button12.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.Button12.UseVisualStyleBackColor = True
        '
        'Button9
        '
        Me.Button9.Enabled = False
        Me.Button9.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.Button9.FlatAppearance.BorderSize = 2
        Me.Button9.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DeepSkyBlue
        Me.Button9.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button9.Font = New System.Drawing.Font("Bahnschrift", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button9.ForeColor = System.Drawing.Color.Blue
        Me.Button9.Image = Global.Farmacia1._0.My.Resources.Resources.Pay_Per_Click_icon
        Me.Button9.Location = New System.Drawing.Point(778, 291)
        Me.Button9.Name = "Button9"
        Me.Button9.Size = New System.Drawing.Size(145, 122)
        Me.Button9.TabIndex = 2
        Me.Button9.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.Button9.UseVisualStyleBackColor = True
        '
        'Button8
        '
        Me.Button8.Enabled = False
        Me.Button8.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.Button8.FlatAppearance.BorderSize = 2
        Me.Button8.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DeepSkyBlue
        Me.Button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button8.Font = New System.Drawing.Font("Bahnschrift", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button8.ForeColor = System.Drawing.Color.Blue
        Me.Button8.Image = Global.Farmacia1._0.My.Resources.Resources.Developer_icon
        Me.Button8.Location = New System.Drawing.Point(615, 433)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(145, 122)
        Me.Button8.TabIndex = 3
        Me.Button8.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.Button8.UseVisualStyleBackColor = True
        '
        'Button7
        '
        Me.Button7.Enabled = False
        Me.Button7.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.Button7.FlatAppearance.BorderSize = 2
        Me.Button7.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DeepSkyBlue
        Me.Button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button7.Font = New System.Drawing.Font("Bahnschrift", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button7.ForeColor = System.Drawing.Color.Blue
        Me.Button7.Image = Global.Farmacia1._0.My.Resources.Resources.Reports_icon
        Me.Button7.Location = New System.Drawing.Point(615, 291)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(145, 122)
        Me.Button7.TabIndex = 2
        Me.Button7.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.Button7.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.Enabled = False
        Me.Button6.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.Button6.FlatAppearance.BorderSize = 2
        Me.Button6.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DeepSkyBlue
        Me.Button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button6.Font = New System.Drawing.Font("Bahnschrift", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button6.ForeColor = System.Drawing.Color.Blue
        Me.Button6.Image = Global.Farmacia1._0.My.Resources.Resources.trasiego
        Me.Button6.Location = New System.Drawing.Point(452, 433)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(145, 122)
        Me.Button6.TabIndex = 3
        Me.Button6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.Button6.UseVisualStyleBackColor = True
        '
        'btnTraslados
        '
        Me.btnTraslados.Enabled = False
        Me.btnTraslados.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.btnTraslados.FlatAppearance.BorderSize = 2
        Me.btnTraslados.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DeepSkyBlue
        Me.btnTraslados.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTraslados.Font = New System.Drawing.Font("Bahnschrift", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTraslados.ForeColor = System.Drawing.Color.MediumBlue
        Me.btnTraslados.Image = Global.Farmacia1._0.My.Resources.Resources.traslado
        Me.btnTraslados.Location = New System.Drawing.Point(452, 291)
        Me.btnTraslados.Name = "btnTraslados"
        Me.btnTraslados.Size = New System.Drawing.Size(145, 122)
        Me.btnTraslados.TabIndex = 2
        Me.btnTraslados.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnTraslados.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Enabled = False
        Me.Button4.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.Button4.FlatAppearance.BorderSize = 2
        Me.Button4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DeepSkyBlue
        Me.Button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button4.Font = New System.Drawing.Font("Bahnschrift", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button4.ForeColor = System.Drawing.Color.MediumBlue
        Me.Button4.Image = Global.Farmacia1._0.My.Resources.Resources.Cash_Register_icon__1_
        Me.Button4.Location = New System.Drawing.Point(289, 433)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(145, 122)
        Me.Button4.TabIndex = 3
        Me.Button4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Enabled = False
        Me.Button3.FlatAppearance.BorderColor = System.Drawing.Color.MediumBlue
        Me.Button3.FlatAppearance.BorderSize = 2
        Me.Button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DeepSkyBlue
        Me.Button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button3.Font = New System.Drawing.Font("Bahnschrift", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.ForeColor = System.Drawing.Color.MediumBlue
        Me.Button3.Image = Global.Farmacia1._0.My.Resources.Resources.Status_mail_task_icon__1_
        Me.Button3.Location = New System.Drawing.Point(289, 291)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(145, 122)
        Me.Button3.TabIndex = 2
        Me.Button3.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Enabled = False
        Me.Button2.FlatAppearance.BorderColor = System.Drawing.Color.Blue
        Me.Button2.FlatAppearance.BorderSize = 2
        Me.Button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DeepSkyBlue
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Font = New System.Drawing.Font("Bahnschrift", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.ForeColor = System.Drawing.Color.MediumBlue
        Me.Button2.Image = Global.Farmacia1._0.My.Resources.Resources.cash_register_icon
        Me.Button2.Location = New System.Drawing.Point(126, 433)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(145, 122)
        Me.Button2.TabIndex = 3
        Me.Button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.FlatAppearance.BorderColor = System.Drawing.Color.MediumBlue
        Me.Button1.FlatAppearance.BorderSize = 2
        Me.Button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DeepSkyBlue
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Bahnschrift", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.MediumBlue
        Me.Button1.Image = Global.Farmacia1._0.My.Resources.Resources.User_login_man
        Me.Button1.Location = New System.Drawing.Point(126, 291)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(145, 122)
        Me.Button1.TabIndex = 2
        Me.Button1.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.Button1.UseVisualStyleBackColor = True
        '
        'frmMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1212, 611)
        Me.Controls.Add(Me.Button11)
        Me.Controls.Add(Me.Button12)
        Me.Controls.Add(Me.btnRecibirTraslado)
        Me.Controls.Add(Me.Button9)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button8)
        Me.Controls.Add(Me.Button7)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.btnTraslados)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.ShapeContainer1)
        Me.MaximizeBox = False
        Me.Name = "frmMenu"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Menú Principal"
        Me.Panel1.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents ShapeContainer1 As Microsoft.VisualBasic.PowerPacks.ShapeContainer
    Friend WithEvents RectangleShape1 As Microsoft.VisualBasic.PowerPacks.RectangleShape
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents btnTraslados As System.Windows.Forms.Button
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents Button8 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Button9 As System.Windows.Forms.Button
    Friend WithEvents btnRecibirTraslado As System.Windows.Forms.Button
    Friend WithEvents Button12 As System.Windows.Forms.Button
    Friend WithEvents Button11 As System.Windows.Forms.Button

End Class
