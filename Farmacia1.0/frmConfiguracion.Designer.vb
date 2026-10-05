<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConfiguracion
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
        Me.cmbSucursal = New System.Windows.Forms.ComboBox()
        Me.gbEmpresa = New System.Windows.Forms.GroupBox()
        Me.txtCliente = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtEslogan = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtNombreEmpresa = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.gbSucursal = New System.Windows.Forms.GroupBox()
        Me.btnLogo = New System.Windows.Forms.Button()
        Me.txtLogoPath = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.numHorasDiferencia = New System.Windows.Forms.NumericUpDown()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.numCantTickets = New System.Windows.Forms.NumericUpDown()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.chkImprimeTicketCuadre = New System.Windows.Forms.CheckBox()
        Me.chkImprimeTicket = New System.Windows.Forms.CheckBox()
        Me.txtSucursalFisica = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.gbEquipo = New System.Windows.Forms.GroupBox()
        Me.cmbImpresora = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.btnGuardar = New System.Windows.Forms.Button()
        Me.btnCerrar = New System.Windows.Forms.Button()
        Me.gbEmpresa.SuspendLayout()
        Me.gbSucursal.SuspendLayout()
        CType(Me.numHorasDiferencia, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numCantTickets, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbEquipo.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblSucursal
        '
        Me.lblSucursal.AutoSize = True
        Me.lblSucursal.Location = New System.Drawing.Point(12, 15)
        Me.lblSucursal.Name = "lblSucursal"
        Me.lblSucursal.Size = New System.Drawing.Size(89, 13)
        Me.lblSucursal.TabIndex = 0
        Me.lblSucursal.Text = "Sucursal a editar:"
        '
        'cmbSucursal
        '
        Me.cmbSucursal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSucursal.FormattingEnabled = True
        Me.cmbSucursal.Location = New System.Drawing.Point(120, 12)
        Me.cmbSucursal.Name = "cmbSucursal"
        Me.cmbSucursal.Size = New System.Drawing.Size(316, 21)
        Me.cmbSucursal.TabIndex = 1
        '
        'gbEmpresa
        '
        Me.gbEmpresa.Controls.Add(Me.txtCliente)
        Me.gbEmpresa.Controls.Add(Me.Label3)
        Me.gbEmpresa.Controls.Add(Me.txtEslogan)
        Me.gbEmpresa.Controls.Add(Me.Label2)
        Me.gbEmpresa.Controls.Add(Me.txtNombreEmpresa)
        Me.gbEmpresa.Controls.Add(Me.Label1)
        Me.gbEmpresa.Location = New System.Drawing.Point(12, 45)
        Me.gbEmpresa.Name = "gbEmpresa"
        Me.gbEmpresa.Size = New System.Drawing.Size(436, 112)
        Me.gbEmpresa.TabIndex = 2
        Me.gbEmpresa.TabStop = False
        Me.gbEmpresa.Text = "Empresa (aplica a todas las sucursales)"
        '
        'txtCliente
        '
        Me.txtCliente.Location = New System.Drawing.Point(141, 76)
        Me.txtCliente.MaxLength = 100
        Me.txtCliente.Name = "txtCliente"
        Me.txtCliente.Size = New System.Drawing.Size(283, 20)
        Me.txtCliente.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 79)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(39, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Tag = "FO"
        Me.Label3.Text = "Cliente"
        '
        'txtEslogan
        '
        Me.txtEslogan.Location = New System.Drawing.Point(141, 49)
        Me.txtEslogan.MaxLength = 200
        Me.txtEslogan.Name = "txtEslogan"
        Me.txtEslogan.Size = New System.Drawing.Size(283, 20)
        Me.txtEslogan.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 52)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(45, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Tag = "FO"
        Me.Label2.Text = "Eslogan"
        '
        'txtNombreEmpresa
        '
        Me.txtNombreEmpresa.Location = New System.Drawing.Point(141, 22)
        Me.txtNombreEmpresa.MaxLength = 200
        Me.txtNombreEmpresa.Name = "txtNombreEmpresa"
        Me.txtNombreEmpresa.Size = New System.Drawing.Size(283, 20)
        Me.txtNombreEmpresa.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(87, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Tag = "FO"
        Me.Label1.Text = "Nombre empresa"
        '
        'gbSucursal
        '
        Me.gbSucursal.Controls.Add(Me.btnLogo)
        Me.gbSucursal.Controls.Add(Me.txtLogoPath)
        Me.gbSucursal.Controls.Add(Me.Label7)
        Me.gbSucursal.Controls.Add(Me.numHorasDiferencia)
        Me.gbSucursal.Controls.Add(Me.Label6)
        Me.gbSucursal.Controls.Add(Me.numCantTickets)
        Me.gbSucursal.Controls.Add(Me.Label5)
        Me.gbSucursal.Controls.Add(Me.chkImprimeTicketCuadre)
        Me.gbSucursal.Controls.Add(Me.chkImprimeTicket)
        Me.gbSucursal.Controls.Add(Me.txtSucursalFisica)
        Me.gbSucursal.Controls.Add(Me.Label4)
        Me.gbSucursal.Location = New System.Drawing.Point(12, 163)
        Me.gbSucursal.Name = "gbSucursal"
        Me.gbSucursal.Size = New System.Drawing.Size(436, 188)
        Me.gbSucursal.TabIndex = 3
        Me.gbSucursal.TabStop = False
        Me.gbSucursal.Text = "Sucursal"
        '
        'btnLogo
        '
        Me.btnLogo.Location = New System.Drawing.Point(388, 150)
        Me.btnLogo.Name = "btnLogo"
        Me.btnLogo.Size = New System.Drawing.Size(36, 22)
        Me.btnLogo.TabIndex = 10
        Me.btnLogo.Text = "..."
        Me.btnLogo.UseVisualStyleBackColor = True
        '
        'txtLogoPath
        '
        Me.txtLogoPath.Location = New System.Drawing.Point(141, 151)
        Me.txtLogoPath.MaxLength = 300
        Me.txtLogoPath.Name = "txtLogoPath"
        Me.txtLogoPath.Size = New System.Drawing.Size(241, 20)
        Me.txtLogoPath.TabIndex = 9
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(12, 154)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(58, 13)
        Me.Label7.TabIndex = 8
        Me.Label7.Tag = "FO"
        Me.Label7.Text = "Logo (ruta)"
        '
        'numHorasDiferencia
        '
        Me.numHorasDiferencia.Location = New System.Drawing.Point(141, 116)
        Me.numHorasDiferencia.Maximum = New Decimal(New Integer() {23, 0, 0, 0})
        Me.numHorasDiferencia.Minimum = New Decimal(New Integer() {23, 0, 0, -2147483648})
        Me.numHorasDiferencia.Name = "numHorasDiferencia"
        Me.numHorasDiferencia.Size = New System.Drawing.Size(60, 20)
        Me.numHorasDiferencia.TabIndex = 7
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 123)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(84, 13)
        Me.Label6.TabIndex = 6
        Me.Label6.Tag = "FO"
        Me.Label6.Text = "Horas diferencia"
        '
        'numCantTickets
        '
        Me.numCantTickets.Location = New System.Drawing.Point(372, 121)
        Me.numCantTickets.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.numCantTickets.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numCantTickets.Name = "numCantTickets"
        Me.numCantTickets.Size = New System.Drawing.Size(52, 20)
        Me.numCantTickets.TabIndex = 5
        Me.numCantTickets.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(240, 123)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(83, 13)
        Me.Label5.TabIndex = 4
        Me.Label5.Tag = "FO"
        Me.Label5.Text = "Cantidad tickets"
        '
        'chkImprimeTicketCuadre
        '
        Me.chkImprimeTicketCuadre.AutoSize = True
        Me.chkImprimeTicketCuadre.Location = New System.Drawing.Point(15, 92)
        Me.chkImprimeTicketCuadre.Name = "chkImprimeTicketCuadre"
        Me.chkImprimeTicketCuadre.Size = New System.Drawing.Size(141, 17)
        Me.chkImprimeTicketCuadre.TabIndex = 3
        Me.chkImprimeTicketCuadre.Text = "Imprimir ticket de cuadre"
        Me.chkImprimeTicketCuadre.UseVisualStyleBackColor = True
        '
        'chkImprimeTicket
        '
        Me.chkImprimeTicket.AutoSize = True
        Me.chkImprimeTicket.Location = New System.Drawing.Point(15, 61)
        Me.chkImprimeTicket.Name = "chkImprimeTicket"
        Me.chkImprimeTicket.Size = New System.Drawing.Size(135, 17)
        Me.chkImprimeTicket.TabIndex = 2
        Me.chkImprimeTicket.Text = "Imprimir ticket de venta"
        Me.chkImprimeTicket.UseVisualStyleBackColor = True
        '
        'txtSucursalFisica
        '
        Me.txtSucursalFisica.Location = New System.Drawing.Point(141, 25)
        Me.txtSucursalFisica.MaxLength = 100
        Me.txtSucursalFisica.Name = "txtSucursalFisica"
        Me.txtSucursalFisica.Size = New System.Drawing.Size(283, 20)
        Me.txtSucursalFisica.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 28)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(86, 13)
        Me.Label4.TabIndex = 0
        Me.Label4.Tag = "FO"
        Me.Label4.Text = "Nombre sucursal"
        '
        'gbEquipo
        '
        Me.gbEquipo.Controls.Add(Me.cmbImpresora)
        Me.gbEquipo.Controls.Add(Me.Label8)
        Me.gbEquipo.Location = New System.Drawing.Point(12, 357)
        Me.gbEquipo.Name = "gbEquipo"
        Me.gbEquipo.Size = New System.Drawing.Size(436, 56)
        Me.gbEquipo.TabIndex = 4
        Me.gbEquipo.TabStop = False
        Me.gbEquipo.Text = "Este equipo (solo afecta a esta PC)"
        '
        'cmbImpresora
        '
        Me.cmbImpresora.FormattingEnabled = True
        Me.cmbImpresora.Location = New System.Drawing.Point(141, 22)
        Me.cmbImpresora.Name = "cmbImpresora"
        Me.cmbImpresora.Size = New System.Drawing.Size(283, 21)
        Me.cmbImpresora.TabIndex = 1
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(12, 25)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(53, 13)
        Me.Label8.TabIndex = 0
        Me.Label8.Tag = "FO"
        Me.Label8.Text = "Impresora"
        '
        'btnGuardar
        '
        Me.btnGuardar.Location = New System.Drawing.Point(272, 425)
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.Size = New System.Drawing.Size(85, 30)
        Me.btnGuardar.TabIndex = 5
        Me.btnGuardar.Text = "Guardar"
        Me.btnGuardar.UseVisualStyleBackColor = True
        '
        'btnCerrar
        '
        Me.btnCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCerrar.Location = New System.Drawing.Point(363, 425)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(85, 30)
        Me.btnCerrar.TabIndex = 6
        Me.btnCerrar.Text = "Cerrar"
        Me.btnCerrar.UseVisualStyleBackColor = True
        '
        'frmConfiguracion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCerrar
        Me.ClientSize = New System.Drawing.Size(460, 467)
        Me.Controls.Add(Me.btnCerrar)
        Me.Controls.Add(Me.btnGuardar)
        Me.Controls.Add(Me.gbEquipo)
        Me.Controls.Add(Me.gbSucursal)
        Me.Controls.Add(Me.gbEmpresa)
        Me.Controls.Add(Me.cmbSucursal)
        Me.Controls.Add(Me.lblSucursal)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmConfiguracion"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Configuración"
        Me.gbEmpresa.ResumeLayout(False)
        Me.gbEmpresa.PerformLayout()
        Me.gbSucursal.ResumeLayout(False)
        Me.gbSucursal.PerformLayout()
        CType(Me.numHorasDiferencia, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numCantTickets, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbEquipo.ResumeLayout(False)
        Me.gbEquipo.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblSucursal As Label
    Friend WithEvents cmbSucursal As ComboBox
    Friend WithEvents gbEmpresa As GroupBox
    Friend WithEvents txtCliente As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txtEslogan As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txtNombreEmpresa As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents gbSucursal As GroupBox
    Friend WithEvents btnLogo As Button
    Friend WithEvents txtLogoPath As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents numHorasDiferencia As NumericUpDown
    Friend WithEvents Label6 As Label
    Friend WithEvents numCantTickets As NumericUpDown
    Friend WithEvents Label5 As Label
    Friend WithEvents chkImprimeTicketCuadre As CheckBox
    Friend WithEvents chkImprimeTicket As CheckBox
    Friend WithEvents txtSucursalFisica As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents gbEquipo As GroupBox
    Friend WithEvents cmbImpresora As ComboBox
    Friend WithEvents Label8 As Label
    Friend WithEvents btnGuardar As Button
    Friend WithEvents btnCerrar As Button
End Class
