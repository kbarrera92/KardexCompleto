Imports System.Data.SqlClient
Imports Serilog

Public Class FormCategoriaEgreso
    Private _errorProvider As New ErrorProvider()
    Private _validator As EgresoValidator

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink

        _validator = New EgresoValidator(_errorProvider)
    End Sub

    Private Sub FormCategoriaEgreso_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ConfigurarDataGridView()
        CargarCategorias()
        LimpiarControles()
    End Sub

    Private Sub ConfigurarDataGridView()
        dgvCategorias.AutoGenerateColumns = True
        dgvCategorias.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvCategorias.MultiSelect = False
        dgvCategorias.ReadOnly = True
        dgvCategorias.AllowUserToAddRows = False
        dgvCategorias.AllowUserToDeleteRows = False
        dgvCategorias.AllowUserToResizeRows = False
    End Sub

    Private Sub CargarCategorias()
        Try
            Dim incluirInactivos As Integer = If(chkMostrarInactivos.Checked, 1, 0)

            openConnection()
            Using cmd As New SqlCommand("sp_mantCategoriasEgresos", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@incluirInactivos", incluirInactivos)
                cmd.Parameters.AddWithValue("@opcion", "L")


                ' Crear el adaptador y llenar el DataTable
                Dim adaptador As New SqlDataAdapter(cmd)
                Dim tablaCategorias As New DataTable()
                adaptador.Fill(tablaCategorias)

                For i = 0 To tablaCategorias.Columns.Count - 1
                    dgvCategorias.Columns(i).DataPropertyName = tablaCategorias.Columns(i).ToString
                Next

                dv = tablaCategorias.DefaultView
                dgvCategorias.DataSource = dv

            End Using

            ' Mostrar contador de registros
            ActualizarContador()

        Catch ex As Exception
            MessageBox.Show("Error al cargar las categorías: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ActualizarContador()
        ' Mostrar el número de registros en un label
        lblContador.Text = $"Total de registros: {dgvCategorias.Rows.Count}"
    End Sub

    Private Sub LimpiarControles()
        ' Limpiar campos de edición
        txtId.Clear()
        txtNombre.Clear()
        chkEstado.Checked = True

        ' Habilitar/deshabilitar controles según corresponda
        txtNombre.Enabled = True
        btnGuardar.Text = "Guardar"

        ' Enfocar el campo nombre
        txtNombre.Focus()
    End Sub

    Private Sub dgvCategorias_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvCategorias.CellClick
        Try
            txtId.Text = dgvCategorias.CurrentRow.Cells(0).Value.ToString()
            txtNombre.Text = dgvCategorias.CurrentRow.Cells(1).Value.ToString()
            chkEstado.Checked = dgvCategorias.CurrentRow.Cells(2).Value
            btnGuardar.Text = "Actualizar"
        Catch ex As Exception
            Log.Error($"Ocurrio un error. Error: {ex.Message}")
        End Try
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Dim opcion As String = String.Empty
        opcion = If(btnGuardar.Text = "Guardar", "I", "U")

        Dim controles As New Dictionary(Of String, Control) From {
            {"NombreCategoria", txtNombre}
        }

        If _validator.ValidarCategoriaEgreso(txtNombre.Text, controles) Then

            If opcion = "I" Then
                Dim parametros As New List(Of SqlParameter) From {
                    New SqlParameter("@opcion", SqlDbType.Char) With {.Value = "I"},
                    New SqlParameter("@msg", SqlDbType.VarChar, 200) With {.Direction = ParameterDirection.Output},
                    New SqlParameter("@returnValue", SqlDbType.Int) With {.Direction = ParameterDirection.ReturnValue},
                    New SqlParameter("@nombreCategoriaIngreso", SqlDbType.VarChar, 20) With {.Value = txtNombre.Text}
                }

                If GuardarCategoria(parametros) Then
                    CargarCategorias()
                    LimpiarControles()
                End If
            Else
                Dim parametros As New List(Of SqlParameter) From {
                    New SqlParameter("@opcion", SqlDbType.Char) With {.Value = "U"},
                    New SqlParameter("@msg", SqlDbType.VarChar, 200) With {.Direction = ParameterDirection.Output},
                    New SqlParameter("@idCategoriaEgreso", SqlDbType.Int) With {.Value = txtId.Text},
                    New SqlParameter("@estado", SqlDbType.Bit) With {.Value = chkEstado.Checked},
                    New SqlParameter("@nombreCategoriaIngreso", SqlDbType.VarChar, 20) With {.Value = txtNombre.Text},
                    New SqlParameter("@returnValue", SqlDbType.Int) With {.Direction = ParameterDirection.ReturnValue}
                }

                If GuardarCategoria(parametros) Then
                    CargarCategorias()
                    LimpiarControles()
                End If
            End If



        End If
    End Sub

    Private Function GuardarCategoria(parametros As List(Of SqlParameter))
        Try

            Dim paramtodb As String = String.Empty
            For Each param As SqlParameter In parametros
                paramtodb &= param.ParameterName & "=" & If(param.Value, """") & vbCrLf
            Next
            Log.Information($"Parametros enviados al sp: {vbCrLf}{paramtodb}")

            Dim resultado = EjecutarStoredProcedureMultiple("sp_mantCategoriasEgresos", parametros)

            Dim codigoRetorno As Integer = Convert.ToInt32(parametros.Find(Function(p) p.ParameterName = "@returnValue").Value)
            Dim mensajeSalida As String = parametros.Find(Function(p) p.ParameterName = "@msg").Value.ToString()
            MessageBox.Show(mensajeSalida, "Resultado", MessageBoxButtons.OK, IIf(codigoRetorno = 0, MessageBoxIcon.Information, MessageBoxIcon.Error))
            Return If(codigoRetorno = 0, True, False)

        Catch ex As Exception
            Return False
            Log.Error($"Ocurrio un error. Error: {ex.Message}")
        End Try
    End Function

    Private Sub ButtonLimpiar_Click(sender As Object, e As EventArgs) Handles ButtonLimpiar.Click
        LimpiarControles()
    End Sub

    Private Sub txtBuscar_TextChanged(sender As Object, e As EventArgs) Handles txtBuscar.TextChanged
        Try
            dv.RowFilter = String.Format("Convert(nombreCategoriaEgreso, 'System.String') LIKE '%{0}%'", Trim(txtBuscar.Text))
        Catch ex As Exception
            Log.Error($"Ocurrió un error al buscar. Error {ex.Message}")
        End Try
    End Sub

    Private Sub chkMostrarInactivos_CheckedChanged(sender As Object, e As EventArgs) Handles chkMostrarInactivos.CheckedChanged
        CargarCategorias()
    End Sub
End Class