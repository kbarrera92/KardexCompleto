Public Class Estilos
    Public Shared Sub AplicarEstilos(ByVal parent As Control)
        For Each ctrl As Control In parent.Controls
            If TypeOf ctrl Is TextBox Then
                Dim textbox As TextBox = CType(ctrl, TextBox)
                textbox.BorderStyle = BorderStyle.FixedSingle
                textbox.BackColor = Color.WhiteSmoke
                textbox.ForeColor = Color.DarkBlue

                If textbox.Tag = "E" Then
                    textbox.BackColor = Color.DarkBlue
                    textbox.ForeColor = Color.White
                End If

                If textbox.Tag <> "PS" Then
                    textbox.Font = New Font("Segoe UI", 10, FontStyle.Bold)
                End If
            End If

            If TypeOf ctrl Is ComboBox Then
                Dim comboBox As ComboBox = CType(ctrl, ComboBox)
                comboBox.BackColor = Color.WhiteSmoke
                comboBox.ForeColor = Color.DarkBlue
                comboBox.Font = New Font("Segoe UI", 10, FontStyle.Bold)
            End If

            If TypeOf ctrl Is Label Then
                Dim label As Label = CType(ctrl, Label)
                label.ForeColor = Color.DarkBlue

                If label.Tag <> "PS" Then
                    label.Font = New Font("Segoe UI", 10, FontStyle.Bold)
                End If
            End If

            If TypeOf ctrl Is Button Then
                Dim button As Button = CType(ctrl, Button)
                button.FlatStyle = FlatStyle.Flat
                button.FlatAppearance.BorderColor = Color.White
                button.FlatAppearance.BorderSize = 1

                Dim estilos As String() = button.Tag.ToString().Split(","c)
                For Each estilo As String In estilos
                    Select Case estilo.ToUpper()
                        Case "DB"
                            With button
                                .ForeColor = Color.White
                                .BackColor = Color.DarkBlue
                            End With
                        Case "SL" 'Standar Letter Size
                            button.Font = New Font("Segoe UI", 10, FontStyle.Regular)
                        Case "WB"
                            button.FlatAppearance.BorderColor = Color.DarkBlue
                    End Select
                Next

            End If

            If TypeOf ctrl Is GroupBox Then
                Dim groupBox As GroupBox = CType(ctrl, GroupBox)
                groupBox.BackColor = Color.DarkBlue
                groupBox.Font = New Font("Segoe UI", 10, FontStyle.Regular)
                groupBox.ForeColor = Color.White
            End If

            If TypeOf ctrl Is ToolStrip Then
                Dim toolStrip As ToolStrip = CType(ctrl, ToolStrip)
                toolStrip.BackColor = Color.DarkBlue
            End If


            If ctrl.HasChildren Then
                AplicarEstilos(ctrl)
            End If
        Next
    End Sub

    Public Shared Sub AplicarEstilosToolStrip(ByVal toolStrip As ToolStrip)
        For Each item As ToolStripItem In toolStrip.Items
            If TypeOf item Is ToolStripButton Then
                Dim tsButton As ToolStripButton = CType(item, ToolStripButton)
                tsButton.Font = New Font("Segoe UI", 10, FontStyle.Bold)
                tsButton.ForeColor = Color.White
                tsButton.AutoSize = True
            End If

            If TypeOf item Is ToolStripSplitButton Then
                Dim splitButton As ToolStripSplitButton = CType(item, ToolStripSplitButton)
                splitButton.Font = New Font("Segoe UI", 10, FontStyle.Bold)
                splitButton.ForeColor = Color.White
                splitButton.AutoSize = True
            End If
        Next
    End Sub

End Class
