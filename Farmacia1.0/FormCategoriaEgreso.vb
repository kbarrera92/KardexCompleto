Imports System.Data.SqlClient

Public Class FormCategoriaEgreso
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
End Class