Imports Serilog

Public Class Estilos
    Public Shared Sub AplicarEstilos(ByVal parent As Control)
        Try
            For Each ctrl As Control In parent.Controls
                If TypeOf ctrl Is TextBox Then
                    Dim textbox As TextBox = CType(ctrl, TextBox)
                    textbox.BorderStyle = BorderStyle.FixedSingle
                    textbox.BackColor = Color.WhiteSmoke
                    textbox.ForeColor = Color.DarkBlue

                    Dim estilos As String() = If(textbox.Tag IsNot Nothing, textbox.Tag.ToString().Split(","c), New String() {})
                    For Each estilo As String In estilos
                        Select Case estilo.ToUpper()
                            Case "E"
                                textbox.BackColor = Color.DarkBlue
                                textbox.ForeColor = Color.White
                            Case "ES"
                                textbox.Font = New Font("Segoe UI", 10, FontStyle.Bold)
                            Case "SE"
                                Return

                        End Select

                    Next
                End If

                If TypeOf ctrl Is ComboBox Then
                    Dim comboBox As ComboBox = CType(ctrl, ComboBox)
                    comboBox.BackColor = Color.WhiteSmoke
                    comboBox.ForeColor = Color.DarkBlue
                    comboBox.Font = New Font("Segoe UI", 10, FontStyle.Bold)
                    Continue For
                End If

                If TypeOf ctrl Is Label Then
                    Dim label As Label = CType(ctrl, Label)
                    label.ForeColor = Color.DarkBlue

                    Dim estilos As String() = If(label.Tag IsNot Nothing, label.Tag.ToString().Split(","c), New String() {})
                    For Each estilo As String In estilos
                        Select Case estilo.ToUpper()
                            Case "PS"
                                Return
                            Case "FO"
                                label.ForeColor = Color.White
                            Case Else
                                label.Font = New Font("Segoe UI", 10, FontStyle.Bold)
                                label.ForeColor = Color.DarkBlue
                        End Select
                    Next

                End If

                If TypeOf ctrl Is Button Then
                    Dim button As Button = CType(ctrl, Button)
                    button.FlatStyle = FlatStyle.Flat
                    button.FlatAppearance.BorderColor = Color.White
                    button.FlatAppearance.BorderSize = 1

                    Dim estilos As String() = If(button.Tag IsNot Nothing, button.Tag.ToString().Split(","c), New String() {})
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
                            Case "FO"
                                button.BackColor = Color.WhiteSmoke
                            Case "SE"
                                Return
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
                    Dim estilos As String() = If(toolStrip.Tag IsNot Nothing, toolStrip.Tag.ToString().Split(","c), New String() {})
                    For Each estilo As String In estilos
                        Select Case estilo.ToUpper()
                            Case "G"
                                toolStrip.BackColor = Color.DarkBlue

                        End Select
                    Next

                End If

                If TypeOf ctrl Is Panel Then
                    Dim panel As Panel = CType(ctrl, Panel)

                    Dim estilos As String() = If(panel.Tag IsNot Nothing, panel.Tag.ToString().Split(","c), New String() {})
                    For Each estilo As String In estilos
                        Select Case estilo.ToUpper()
                            Case "G"
                                panel.BackColor = Color.DarkBlue

                        End Select
                    Next
                End If

                If ctrl.HasChildren Then
                    AplicarEstilos(ctrl)
                End If
            Next
        Catch ex As Exception
            Log.Error($"Ocurrió un error. Error: {ex.Message}")
        End Try

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
