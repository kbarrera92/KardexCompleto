Imports System.Data.SqlClient
Imports Serilog

Public Class FormReporteBonificaciones
    Dim ds As DataSet
    Dim sqlSucursal As String = "SELECT idSucursal, nombreSuc FROM SUCURSAL"

    Private Sub FormReporteBonificaciones_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Log.Information("Cargando sucursales y vendedores en formulario de Reporte de bonificaciones")
        Try
            CargarSucursales()
            CargarVendedores()
        Catch ex As Exception
            Log.Error($"Ocurrió un error. Error: {ex.Message}")
        End Try
    End Sub

    Private Sub CargarSucursales()
        Dim dtSuc As DataTable = updateCm(sqlSucursal)

        Dim filaTodas As DataRow = dtSuc.NewRow()
        filaTodas("idSucursal") = 0
        filaTodas("nombreSuc") = "-- Todas las sucursales --"
        dtSuc.Rows.InsertAt(filaTodas, 0)

        ComboBoxSucursal.DataSource = dtSuc
        ComboBoxSucursal.ValueMember = "idSucursal"
        ComboBoxSucursal.DisplayMember = "nombreSuc"
        ComboBoxSucursal.SelectedIndex = 0
    End Sub

    Private Sub CargarVendedores()
        Dim dtVend As New DataTable

        Try
            openConnection()

            Using cmd As New SqlCommand("sp_MantenimientoVendedor", conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@Accion", SqlDbType.VarChar, 20).Value = "LISTAR"
                cmd.Parameters.Add("@mensaje", SqlDbType.VarChar, 200).Value = DBNull.Value

                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dtVend)
                End Using
            End Using

        Catch ex As Exception
            Log.Error($"Error al cargar vendedores. Error: {ex.Message}")
            MessageBox.Show("No fue posible cargar los vendedores.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            closeConnection()
        End Try

        Dim filaTodos As DataRow = dtVend.NewRow()
        filaTodos(0) = 0
        filaTodos(1) = "-- Todos los vendedores --"
        dtVend.Rows.InsertAt(filaTodos, 0)

        ComboBoxVendedor.DataSource = dtVend
        ComboBoxVendedor.ValueMember = dtVend.Columns(0).ColumnName
        ComboBoxVendedor.DisplayMember = dtVend.Columns(1).ColumnName
        ComboBoxVendedor.SelectedIndex = 0
    End Sub

    Sub llenarDTSBonificaciones()
        Dim cmd As SqlCommand
        Dim da As SqlDataAdapter
        ds = New dsBonificaciones

        Try
            openConnection()
            cmd = New SqlCommand()
            With cmd
                .CommandText = "sp_bonificacionesXvendedor"
                .CommandType = CommandType.StoredProcedure
                .Connection = conn
                .Parameters.AddWithValue("fechainicial", DateTimePicker1.Value)
                .Parameters.AddWithValue("fechafinal", DateTimePicker2.Value)

                If Convert.ToInt32(ComboBoxVendedor.SelectedValue) = 0 Then
                    .Parameters.AddWithValue("vendedor", DBNull.Value)
                Else
                    .Parameters.AddWithValue("vendedor", Convert.ToInt32(ComboBoxVendedor.SelectedValue))
                End If

                If Convert.ToInt32(ComboBoxSucursal.SelectedValue) = 0 Then
                    .Parameters.AddWithValue("sucursal", DBNull.Value)
                Else
                    .Parameters.AddWithValue("sucursal", Convert.ToInt32(ComboBoxSucursal.SelectedValue))
                End If
            End With

            da = New SqlDataAdapter(cmd)
            da.Fill(ds.Tables("dtBonificaciones"))
        Catch ex As Exception
            MessageBox.Show("Error al cargar los datos")
        Finally
            closeConnection()
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            llenarDTSBonificaciones()

            Dim informe As New rptBonificaciones
            informe.SetDataSource(ds.Tables("dtBonificaciones"))
            informe.SetParameterValue("inicio", DateTimePicker1.Value.Date)
            informe.SetParameterValue("fin", DateTimePicker2.Value.Date)
            informe.SetParameterValue("vendedor", If(String.IsNullOrEmpty(ComboBoxVendedor.Text), String.Empty, ComboBoxVendedor.Text))

            frmVerReportes.CrystalReportViewer1.ReportSource = informe
            frmVerReportes.Show()
        Catch ex As Exception
            MessageBox.Show($"Error al cargar el reporte. Error: {ex.Message}")
        End Try
    End Sub
End Class
