Imports System.Data.SqlClient
Imports System.Drawing.Printing

''' <summary>
''' Permite al administrador editar los parámetros de empresa y sucursal (tabla PARAMETRO)
''' y la impresora del equipo (My.Settings).
''' </summary>
Public Class frmConfiguracion

    Private cargando As Boolean = True

    Private Sub frmConfiguracion_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If nombreRol <> "ADMINISTRADOR" Then
            MessageBox.Show("No tiene permisos para este módulo", "No tiene permisos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Close()
            Return
        End If

        Estilos.AplicarEstilos(Me)

        For Each impresora As String In PrinterSettings.InstalledPrinters
            cmbImpresora.Items.Add(impresora)
        Next
        cmbImpresora.Text = ConsultaParametro("nombreImpresora")

        Try
            Dim dt As New DataTable()
            openConnection()
            Using da As New SqlDataAdapter("SELECT idSucursal, nombreSuc FROM SUCURSAL ORDER BY nombreSuc", conn)
                da.Fill(dt)
            End Using
            cmbSucursal.DisplayMember = "nombreSuc"
            cmbSucursal.ValueMember = "idSucursal"
            cmbSucursal.DataSource = dt
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Algo salió mal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Return
        Finally
            closeConnection()
        End Try

        cargando = False
        If sucActual <> 0 Then cmbSucursal.SelectedValue = sucActual
        CargaValores()
    End Sub

    Private Function IdSucursalElegida() As Integer
        Return CInt(cmbSucursal.SelectedValue)
    End Function

    Private Sub cmbSucursal_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSucursal.SelectedIndexChanged
        If Not cargando Then CargaValores()
    End Sub

    ''' <summary>Valores resueltos de la sucursal elegida, con App.config como respaldo.</summary>
    Private Sub CargaValores()
        Dim valores As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
        Try
            Using cmd As New SqlCommand("sp_consultaParametros", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@idSucursal", IdSucursalElegida())
                openConnection()
                Using reader = cmd.ExecuteReader()
                    While reader.Read()
                        valores(reader("clave").ToString()) = reader("valor").ToString()
                    End While
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("No se pudieron leer los parámetros: " & ex.Message, "Algo salió mal", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            closeConnection()
        End Try

        Dim valor = Function(clave As String) As String
                        Dim v As String = Nothing
                        If valores.TryGetValue(clave, v) Then Return v
                        Return Configuration.ConfigurationManager.AppSettings(clave)
                    End Function

        txtNombreEmpresa.Text = valor("nombreEmpresa")
        txtEslogan.Text = valor("eslogan")
        txtCliente.Text = valor("cliente")
        txtSucursalFisica.Text = valor("sucursalFisica")
        chkImprimeTicket.Checked = valor("imprimeTicket") = "S"
        chkImprimeTicketCuadre.Checked = valor("imprimeTicketCuadre") = "S"
        numCantTickets.Value = LimitaValor(valor("cantTickets"), numCantTickets)
        numHorasDiferencia.Value = LimitaValor(valor("horasDiferencia"), numHorasDiferencia)
        txtLogoPath.Text = valor("logoPath")
    End Sub

    Private Shared Function LimitaValor(texto As String, num As NumericUpDown) As Decimal
        Dim n As Integer
        Integer.TryParse(texto, n)
        Return Math.Min(num.Maximum, Math.Max(num.Minimum, n))
    End Function

    Private Sub btnLogo_Click(sender As Object, e As EventArgs) Handles btnLogo.Click
        Using dlg As New OpenFileDialog() With {.Filter = "Imágenes|*.jpg;*.jpeg;*.png;*.bmp|Todos los archivos|*.*"}
            If dlg.ShowDialog() = DialogResult.OK Then txtLogoPath.Text = dlg.FileName
        End Using
    End Sub

    Private Sub GrabaParametro(cmd As SqlCommand, clave As String, idSucursal As Integer?, valor As String)
        cmd.Parameters.Clear()
        cmd.Parameters.AddWithValue("@clave", clave)
        cmd.Parameters.AddWithValue("@idSucursal", If(idSucursal.HasValue, CObj(idSucursal.Value), DBNull.Value))
        cmd.Parameters.AddWithValue("@valor", valor)
        cmd.Parameters.AddWithValue("@usuario", usuarioActual)
        cmd.ExecuteNonQuery()
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If txtNombreEmpresa.Text.Trim() = "" OrElse txtSucursalFisica.Text.Trim() = "" Then
            MessageBox.Show("El nombre de la empresa y de la sucursal son obligatorios", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim idSuc = IdSucursalElegida()
        Dim sucursal As New Dictionary(Of String, String) From {
            {"sucursalFisica", txtSucursalFisica.Text.Trim()},
            {"imprimeTicket", If(chkImprimeTicket.Checked, "S", "N")},
            {"imprimeTicketCuadre", If(chkImprimeTicketCuadre.Checked, "S", "N")},
            {"cantTickets", CInt(numCantTickets.Value).ToString()},
            {"horasDiferencia", CInt(numHorasDiferencia.Value).ToString()},
            {"logoPath", txtLogoPath.Text.Trim()}
        }
        Dim empresa As New Dictionary(Of String, String) From {
            {"nombreEmpresa", txtNombreEmpresa.Text.Trim()},
            {"eslogan", txtEslogan.Text.Trim()},
            {"cliente", txtCliente.Text.Trim()}
        }

        Try
            openConnection()
            Using tx = conn.BeginTransaction()
                Using cmd As New SqlCommand("sp_grabaParametro", conn, tx)
                    cmd.CommandType = CommandType.StoredProcedure
                    For Each kv In empresa
                        GrabaParametro(cmd, kv.Key, Nothing, kv.Value)
                    Next
                    For Each kv In sucursal
                        GrabaParametro(cmd, kv.Key, idSuc, kv.Value)
                    Next
                End Using
                tx.Commit()
            End Using
        Catch ex As Exception
            MessageBox.Show("No se guardó la configuración: " & ex.Message, "Algo salió mal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        Finally
            closeConnection()
        End Try

        Try
            My.Settings.nombreImpresora = cmbImpresora.Text.Trim()
            My.Settings.Save()
        Catch ex As Exception
            MessageBox.Show("No se pudo guardar la impresora de este equipo: " & ex.Message, "Algo salió mal", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try

        ' La empresa es global, así que la caché de la sesión actual siempre se refresca.
        CargaParametros(sucActual)

        Dim params(3) As String
        params(0) = nameUsuarioActual
        params(1) = Environment.MachineName & " - " & Environment.UserName
        params(2) = String.Format("{0} actualizó la configuración de la sucursal {1}", nameUsuarioActual, cmbSucursal.Text)
        GrabaBitacora(params, grabaBitacoraSp)

        MessageBox.Show("Configuración guardada correctamente", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        Close()

    End Sub
End Class
