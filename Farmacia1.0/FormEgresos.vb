Imports System.Data.SqlClient
Imports Serilog

Public Class FormEgresos
    Private _errorProvider As New ErrorProvider()
    Private _validator As EgresoValidator
    Private sqlSucursal As String = "SELECT idSucursal, nombreSuc FROM SUCURSAL"

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink

        _validator = New EgresoValidator(_errorProvider)
    End Sub

    Private Sub ConfigurarControles()
        ' Configurar eventos de validación
        AddHandler TextBoxDescripcion.Validating, AddressOf TextBoxDescripcion_Validating
        AddHandler TextBoxTotalEgreso.Validating, AddressOf TextBoxTotalEgreso_Validating
        AddHandler ComboBoxCategoria.Validating, AddressOf ComboBoxCategoria_Validating
        AddHandler ComboBoxSucursal.Validating, AddressOf ComboBoxSucursal_Validating
        AddHandler DateTimePickerFechaEgreso.Validating, AddressOf DateTimePickerFechaEgreso_Validating
        AddHandler TextBoxUsuarioRegistra.Validating, AddressOf TextBoxUsuarioRegistra_Validating

        ' Otras configuraciones...
        CargarCategorias()
    End Sub

    Private Sub CargarCategorias()
        Try
            ' Asumiendo que tienes una conexión a la BD
            openConnection()
            Dim comando As New SqlCommand("SELECT IdCategoriaEgreso, nombreCategoriaEgreso FROM CATEGORIAEGRESO WHERE estado = @estado")
            comando.Parameters.AddWithValue("@estado", True)
            comando.Connection = conn
            Dim adaptador As New SqlDataAdapter(comando)
            Dim tablaCategoria As New DataTable()

            adaptador.Fill(tablaCategoria)

            ' Configurar el ComboBox
            ComboBoxCategoria.DataSource = tablaCategoria
            ComboBoxCategoria.DisplayMember = "nombreCategoriaEgreso"
            ComboBoxCategoria.ValueMember = "IdCategoriaEgreso"
            ComboBoxCategoria.SelectedIndex = -1 ' Ninguna categoría seleccionada inicialmente
        Catch ex As Exception
            Log.Error($"Ocurrio un error. Error: {ex.Message}")
            MessageBox.Show("Error al cargar las categorías.",
                       "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FormEgresos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            If nombreRol = "ADMINISTRADOR" Then
                ComboBoxSucursal.Enabled = True
            End If

            TextBoxSumatoria.Text = SumarColumnaDataGridView(DataGridViewEgresos, "totalEgreso").ToString()
            ConfigurarControles()
            With DataGridViewEgresos
                .EnableHeadersVisualStyles = False
                .ColumnHeadersDefaultCellStyle.BackColor = Color.DarkBlue
                .ColumnHeadersDefaultCellStyle.ForeColor = Color.White
                .ColumnHeadersDefaultCellStyle.Font = New Font("Arial Black", 10)
                .DefaultCellStyle.SelectionBackColor = Color.LightBlue
                .DefaultCellStyle.SelectionForeColor = Color.DarkBlue
                .DefaultCellStyle.ForeColor = Color.DarkBlue
                .BorderStyle = BorderStyle.None
                .CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical
            End With

            ComboBoxSucursal.DataSource = updateList(sqlSucursal)
            ComboBoxSucursal.ValueMember = updateList(sqlSucursal).Columns(0).ToString
            ComboBoxSucursal.DisplayMember = updateList(sqlSucursal).Columns(1).ToString
            LimpiarFormulario()
        Catch ex As Exception
            Log.Error($"Ocurrió un error. Error: {ex.Message}")
        End Try

    End Sub

    Function updateList(ByVal sql As String) As DataTable
        Dim da As SqlDataAdapter
        Dim dt As New DataTable

        Try
            openConnection()
            da = New SqlDataAdapter(sql, conn)
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            MsgBox(ex.Message)
            Return Nothing
        End Try
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles ButtonGuardar.Click
        Try
            If MessageBox.Show("¿Desea grabar este registro?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                Return
            End If

            Dim sucursal As Integer
            If nombreRol = "ADMINISTRADOR" Then
                sucursal = If(ComboBoxSucursal.SelectedValue IsNot Nothing, Convert.ToInt32(ComboBoxSucursal.SelectedValue), 0)
            Else
                sucursal = sucActual
            End If

            Dim gasto As New Egreso With {
            .Fecha = DateTimePickerFechaEgreso.Value,
            .IdCategoria = If(ComboBoxCategoria.SelectedValue IsNot Nothing, Convert.ToInt32(ComboBoxCategoria.SelectedValue), 0),
            .Descripcion = TextBoxDescripcion.Text,
            .Total = If(Decimal.TryParse(TextBoxTotalEgreso.Text, Nothing), Decimal.Parse(TextBoxTotalEgreso.Text), 0),
            .IdUsuario = TextBoxUsuarioRegistra.Text,
            .IdSucursal = sucursal
            }

            Dim controles As New Dictionary(Of String, Control) From {
                {"Fecha", DateTimePickerFechaEgreso},
                {"Categoria", ComboBoxCategoria},
                {"Descripcion", TextBoxDescripcion},
                {"Total", TextBoxTotalEgreso},
                {"Sucursal", ComboBoxSucursal},
                {"UsuarioRegistra", TextBoxUsuarioRegistra}
            }

            If _validator.ValidarGasto(gasto, controles) Then
                ' Guardar en la base de datos
                If GuardarGasto(gasto, "I") Then
                    LimpiarFormulario()
                    ListarEgresos("L", DateTime.Now, sucActual)
                    TextBoxSumatoria.Text = SumarColumnaDataGridView(DataGridViewEgresos, "totalEgreso").ToString()
                    DibujaTarjetasResumen()
                    Me.BringToFront()
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Function GuardarGasto(gasto As Egreso, opcion As Char) As Boolean
        Try
            Dim idEgreso As Object = If(DataGridViewEgresos.Rows.Count = 0, DBNull.Value, DataGridViewEgresos.CurrentRow.Cells(0).Value)

            Dim parametros As New List(Of SqlParameter) From {
                New SqlParameter("@opcion", SqlDbType.Char) With {.Value = opcion},
                New SqlParameter("@idEgreso", SqlDbType.Int) With {.Value = idEgreso},
                New SqlParameter("@fechaEgreso", SqlDbType.DateTime, 150) With {.Value = DateTimePickerFechaEgreso.Value},
                New SqlParameter("@concepto", SqlDbType.VarChar, 100) With {.Value = TextBoxDescripcion.Text},
                New SqlParameter("@usuarioRegistra", SqlDbType.VarChar, 20) With {.Value = TextBoxUsuarioRegistra.Text},
                New SqlParameter("@total", SqlDbType.VarChar, 150) With {.Value = TextBoxTotalEgreso.Text},
                New SqlParameter("@sucursal", SqlDbType.Int) With {.Value = ComboBoxSucursal.SelectedValue},
                New SqlParameter("@categoria", SqlDbType.Int) With {.Value = ComboBoxCategoria.SelectedValue},
                New SqlParameter("@msg", SqlDbType.VarChar, 200) With {.Direction = ParameterDirection.Output},
                New SqlParameter("@returnValue", SqlDbType.Int) With {.Direction = ParameterDirection.ReturnValue}
            }

            Dim paramtodb As String = String.Empty
            For Each param As SqlParameter In parametros
                paramtodb &= param.ParameterName & "=" & If(param.Value, """") & vbCrLf
            Next
            Log.Information($"Parametros enviados al sp: {vbCrLf}{paramtodb}")

            Dim resultado = EjecutarStoredProcedureMultiple("sp_mantEgresos", parametros)

            Dim codigoRetorno As Integer = Convert.ToInt32(parametros.Find(Function(p) p.ParameterName = "@returnValue").Value)
            Dim mensajeSalida As String = parametros.Find(Function(p) p.ParameterName = "@msg").Value.ToString()
            MessageBox.Show(mensajeSalida, "Resultado", MessageBoxButtons.OK, IIf(codigoRetorno = 0, MessageBoxIcon.Information, MessageBoxIcon.Error))
            Return If(codigoRetorno = 0, True, False)
        Catch ex As Exception
            Return False
            Log.Error($"Ocurrio un error. Error: {ex.Message}")
        End Try

    End Function

    Private Sub DescartarGasto(opcion As Char)
        Try
            Dim idEgreso As Object = If(DataGridViewEgresos.Rows.Count = 0, DBNull.Value, DataGridViewEgresos.CurrentRow.Cells(0).Value)

            Dim parametros As New List(Of SqlParameter) From {
                New SqlParameter("@opcion", SqlDbType.Char) With {.Value = opcion},
                New SqlParameter("@idEgreso", SqlDbType.Int) With {.Value = idEgreso},
                New SqlParameter("@msg", SqlDbType.VarChar, 200) With {.Direction = ParameterDirection.Output},
                New SqlParameter("@returnValue", SqlDbType.Int) With {.Direction = ParameterDirection.ReturnValue}
            }

            Dim paramtodb As String = String.Empty
            For Each param As SqlParameter In parametros
                paramtodb &= param.ParameterName & "=" & If(param.Value, """") & vbCrLf
            Next
            Log.Information($"Parametros enviados al sp: {vbCrLf}{paramtodb}")

            Dim resultado = EjecutarStoredProcedureMultiple("sp_mantEgresos", parametros)

            Dim codigoRetorno As Integer = Convert.ToInt32(parametros.Find(Function(p) p.ParameterName = "@returnValue").Value)
            Dim mensajeSalida As String = parametros.Find(Function(p) p.ParameterName = "@msg").Value.ToString()
            MessageBox.Show(mensajeSalida, "Resultado", MessageBoxButtons.OK, IIf(codigoRetorno = 0, MessageBoxIcon.Information, MessageBoxIcon.Error))

        Catch ex As Exception

            Log.Error($"Ocurrio un error. Error: {ex.Message}")
        End Try

    End Sub

    Private Sub LimpiarFormulario()
        Try
            DateTimePickerFechaEgreso.Value = Date.Now
            ComboBoxCategoria.SelectedValue = -1
            ComboBoxSucursal.SelectedValue = -1
            TextBoxDescripcion.Clear()
            TextBoxTotalEgreso.Clear()
            TextBoxUsuarioRegistra.Text = nameUsuarioActual

        Catch ex As Exception
            Log.Error($"Ocurrió un error. Error: {ex.Message}")
        End Try

    End Sub

    Private Sub ListarEgresos(opcion As String, fecha As DateTime, sucursal As Integer)
        Try
            Dim parametros As New List(Of SqlParameter) From {
                New SqlParameter("@opcion", SqlDbType.Char) With {.Value = opcion},
                New SqlParameter("@fechaEgreso", SqlDbType.DateTime) With {.Value = fecha},
                New SqlParameter("@sucursal", SqlDbType.Int) With {.Value = sucursal}
            }

            Dim paramtodb As String = String.Empty
            For Each param As SqlParameter In parametros
                paramtodb &= param.ParameterName & "=" & If(param.Value, """") & vbCrLf
            Next
            Log.Information($"Parametros enviados al sp: {vbCrLf}{paramtodb}")

            Dim resultado = EjecutarStoredProcedureMultiple("sp_mantEgresos", parametros)
            Dim dataTables As List(Of DataTable) = resultado.Item1
            If dataTables.Count > 0 AndAlso dataTables(0).Rows.Count > 0 Then
                Dim dt As DataTable = dataTables(0)
                For i = 0 To dt.Columns.Count - 1
                    DataGridViewEgresos.Columns(i).DataPropertyName = dt.Columns(i).ToString
                Next

                dv = dt.DefaultView
                DataGridViewEgresos.DataSource = dv
            End If

        Catch ex As Exception
            MessageBox.Show("Hubo un error al cargar los datos. Revise el log del programa", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Log.Error($"Ocurrió un error. Error: {ex.Message}")
        End Try


    End Sub

    Private Sub DateTimePickerFechaEgreso_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles DateTimePickerFechaEgreso.Validating

    End Sub

    Private Sub ComboBoxCategoria_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ComboBoxCategoria.Validating

    End Sub

    Private Sub TextBoxDescripcion_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles TextBoxDescripcion.Validating

    End Sub

    Private Sub TextBoxTotalEgreso_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles TextBoxTotalEgreso.Validating

    End Sub

    Private Sub TextBoxUsuarioRegistra_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles TextBoxUsuarioRegistra.Validating

    End Sub

    Private Sub ComboBoxSucursal_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ComboBoxSucursal.Validating

    End Sub

    Private Sub ButtonLimpiar_Click(sender As Object, e As EventArgs) Handles ButtonLimpiar.Click
        Try
            LimpiarFormulario()
            ListarEgresos("L", DateTime.Now, sucActual)
            TextBoxSumatoria.Text = SumarColumnaDataGridView(DataGridViewEgresos, "totalEgreso").ToString()
        Catch ex As Exception
            Log.Error($"Ocurrió un error. Error: {ex.Message}")
        End Try
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            If MessageBox.Show("¿Desea eliminar este registro?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                Return
            End If

            If DataGridViewEgresos.CurrentRow IsNot Nothing AndAlso DataGridViewEgresos.CurrentRow.Index >= 0 Then
                DescartarGasto("D")
                ListarEgresos("L", DateTime.Now, sucActual)
                TextBoxSumatoria.Text = SumarColumnaDataGridView(DataGridViewEgresos, "totalEgreso").ToString()
                DibujaTarjetasResumen()
                BringToFront()
            Else
                MessageBox.Show("Debe seleccionar una fila para descartarla", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch ex As Exception
            Log.Error($"Ocurrió un error. Error {ex.Message}")
        End Try


    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            Dim dv As DataView = TryCast(DataGridViewEgresos.DataSource, DataView)

            If dv IsNot Nothing Then
                Dim dtVacio As DataTable = dv.Table.Clone()
                Dim dvVacio As New DataView(dtVacio)
                DataGridViewEgresos.DataSource = dvVacio
            Else
                DataGridViewEgresos.Rows.Clear()
            End If

            ListarEgresos("L", DateTimePickerFiltro.Value, sucActual)
            TextBoxSumatoria.Text = SumarColumnaDataGridView(DataGridViewEgresos, "totalEgreso").ToString()
        Catch ex As Exception
            Log.Error($"Ocurrió un error. Error: {ex.Message}")
        End Try

    End Sub
End Class