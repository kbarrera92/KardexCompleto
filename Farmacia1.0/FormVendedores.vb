Imports System.Data.SqlClient
Imports Serilog

Public Class FormVendedores
    Private esNuevo As Boolean = False
    Private Sub FormVendedores_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CargarVendedores()
    End Sub

    Private Sub DesactivarVendedor(ByVal idVendedor As Integer)

        Try

            openConnection()

            Using cmd As New SqlCommand("sp_MantenimientoVendedor", conn)

                cmd.CommandType = CommandType.StoredProcedure

                cmd.Parameters.Add("@Accion", SqlDbType.VarChar, 20).Value = "DESACTIVAR"
                cmd.Parameters.Add("@mensaje", SqlDbType.VarChar, 200).Value = DBNull.Value

                cmd.Parameters.Add("@idVendedor", SqlDbType.Int).Value =
                idVendedor

                cmd.ExecuteNonQuery()

            End Using

            MessageBox.Show(
            "Vendedor desactivado correctamente.",
            "Éxito",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information)

            CargarVendedores()

        Catch ex As Exception

            Log.Error($"Error al desactivar vendedor. Error: {ex.Message}")

            MessageBox.Show(
            "Hubo un error en el procesamiento",
            "Error",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

        Finally

            closeConnection()

        End Try

    End Sub

    Private Sub CargarVendedores()

        Try

            openConnection()

            Using cmd As New SqlCommand("sp_MantenimientoVendedor", conn)

                cmd.CommandType = CommandType.StoredProcedure

                cmd.Parameters.Add("@Accion", SqlDbType.VarChar, 20).Value = "LISTAR"
                cmd.Parameters.Add("@mensaje", SqlDbType.VarChar, 200).Value = DBNull.Value

                Using da As New SqlDataAdapter(cmd)

                    Dim dt As New DataTable()

                    da.Fill(dt)

                    DataGridView1.DataSource = dt

                End Using

            End Using

        Catch ex As Exception

            Log.Error($"Error al cargar vendedores. Error: {ex.Message}")

            MessageBox.Show(
                "No fue posible cargar los vendedores.",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)

        Finally

            closeConnection()

        End Try

    End Sub

    Private Sub CrearVendedor()

        Try

            openConnection()

            Using cmd As New SqlCommand("sp_MantenimientoVendedor", conn)

                cmd.CommandType = CommandType.StoredProcedure

                cmd.Parameters.Add("@Accion", SqlDbType.VarChar, 20).Value = "CREAR"
                cmd.Parameters.Add("@mensaje", SqlDbType.VarChar, 200).Value = DBNull.Value

                cmd.Parameters.Add("@idVendedor", SqlDbType.Int).Value =
                DBNull.Value

                cmd.Parameters.Add("@nombre", SqlDbType.VarChar, 150).Value =
                TextBoxNombre.Text.Trim()

                cmd.Parameters.Add("@telefono", SqlDbType.VarChar, 8).Value =
                If(String.IsNullOrWhiteSpace(TextBoxTelefono.Text),
                   DBNull.Value,
                   TextBoxTelefono.Text.Trim())

                cmd.Parameters.Add("@estado", SqlDbType.Bit).Value =
                If(ComboBoxEstado.Text = "Activo", True, False)

                cmd.ExecuteNonQuery()

            End Using

            MessageBox.Show(
            "Vendedor registrado correctamente.",
            "Éxito",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information)

            CargarVendedores()

        Catch ex As Exception

            Log.Error($"Error al crear vendedor. Error: {ex.Message}")

            MessageBox.Show(
            "Hubo un error en el procesamiento",
            "Error",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

        Finally

            closeConnection()

        End Try

    End Sub

    Private Sub ActualizarVendedor(ByVal idVendedor As Integer)

        Try

            openConnection()

            Using cmd As New SqlCommand("sp_MantenimientoVendedor", conn)

                cmd.CommandType = CommandType.StoredProcedure

                cmd.Parameters.Add("@Accion", SqlDbType.VarChar, 20).Value = "ACTUALIZAR"
                cmd.Parameters.Add("@mensaje", SqlDbType.VarChar, 200).Value = DBNull.Value

                cmd.Parameters.Add("@idVendedor", SqlDbType.Int).Value =
                CInt(idVendedor)

                cmd.Parameters.Add("@nombre", SqlDbType.VarChar, 150).Value =
                TextBoxNombre.Text.Trim()

                cmd.Parameters.Add("@telefono", SqlDbType.VarChar, 8).Value =
                If(String.IsNullOrWhiteSpace(TextBoxTelefono.Text),
                   DBNull.Value,
                   TextBoxTelefono.Text.Trim())

                cmd.ExecuteNonQuery()

            End Using

            MessageBox.Show(
            "Vendedor actualizado correctamente.",
            "Éxito",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information)

            CargarVendedores()

        Catch ex As Exception

            Log.Error($"Error al actualizar vendedor. Error: {ex.Message}")

            MessageBox.Show(
            "Hubo un error en el procesamiento",
            "Error",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

        Finally

            closeConnection()

        End Try

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles ButtonNuevo.Click
        esNuevo = True
        TextBoxNombre.Clear()
        TextBoxTelefono.Clear()
        ComboBoxEstado.SelectedIndex = 0
        TextBoxNombre.Select()
    End Sub

    Private Sub ButtonSalir_Click(sender As Object, e As EventArgs) Handles ButtonSalir.Click
        Close()
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        esNuevo = False
        Dim nombreVendedor As String = Convert.ToString(DataGridView1.CurrentRow.Cells(1).Value)
        Dim telefonoVendedor As String = Convert.ToString(DataGridView1.CurrentRow.Cells(2).Value)
        Dim estado As String = Convert.ToString(DataGridView1.CurrentRow.Cells(3).Value)

        TextBoxNombre.Text = nombreVendedor
        TextBoxTelefono.Text = telefonoVendedor
        ComboBoxEstado.SelectedIndex = ComboBoxEstado.FindStringExact(If(estado, "Activo", "Inactivo"))

    End Sub

    Private Sub ButtonGrabar_Click(sender As Object, e As EventArgs) Handles ButtonGrabar.Click
        Dim idVendedor As Integer = Convert.ToInt32(DataGridView1.CurrentRow.Cells(0).Value)

        Try
            If esNuevo Then
                CrearVendedor()
            Else
                ActualizarVendedor(idVendedor)
            End If
        Catch ex As Exception
            Log.Error($"Error al actualizar vendedor. Error: {ex.Message}")

            MessageBox.Show(
            "Hubo un error en el procesamiento",
            "Error",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ButtonDesactivar_Click(sender As Object, e As EventArgs) Handles ButtonDesactivar.Click
        Dim idVendedor As Integer = DataGridView1.CurrentRow.Cells(0).Value

        DesactivarVendedor(idVendedor)
    End Sub
End Class