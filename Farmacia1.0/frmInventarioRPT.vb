Imports System.Data.SqlClient
Public Class frmInventarioRPT
    Dim sql As String = "SELECT idSucursal, nombreSuc FROM SUCURSAL"
    Dim condicion As String = " WHERE idSucursal = " + sucActual.ToString()
    Dim sql2 As String = String.Format("EXEC spExistencias @OPCION = {0}", 1)
    Dim ds As dsReportes

    Sub llenarDTSMP()
        Dim cmd As SqlCommand
        Dim da As SqlDataAdapter
        Dim dt As DataTable
        ds = New dsReportes

        Try
            openConnection()
            cmd = New SqlCommand()
            With cmd
                .CommandText = "verInventarioxsuc"
                .CommandType = CommandType.StoredProcedure
                .Connection = conn
                .Parameters.AddWithValue("suc", CInt(ComboBox1.SelectedValue.ToString))
                
            End With
            dt = ds.Tables("dtInventarioGeneral")

            da = New SqlDataAdapter(cmd)
            da.FillSchema(ds.Tables("dtInventarioGeneral"), SchemaType.Source)
            da.Fill(ds.Tables("dtInventarioGeneral"))

            For i = 0 To dt.Columns.Count - 1
                DataGridView1.Columns(i).DataPropertyName = dt.Columns(i).ToString
            Next

            dv = dt.DefaultView
            DataGridView1.DataSource = dv
        Catch ex As NullReferenceException
            MessageBox.Show("No se ha elegido ninguna sucursal", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Catch ex As Exception
            MessageBox.Show("Error al cargar los datos" & vbCrLf & "Error: " & ex.ToString)
        Finally
            closeConnection()

        End Try
    End Sub

    Sub llenarDTSMP2()
        Dim cmd As SqlCommand
        Dim da As SqlDataAdapter
        Dim dt As DataTable
        ds = New dsReportes

        Try
            openConnection()
            cmd = New SqlCommand()
            With cmd
                .CommandText = "verinventarioxcat"
                .CommandType = CommandType.StoredProcedure
                .Connection = conn
                .Parameters.AddWithValue("suc", CInt(ComboBox1.SelectedValue.ToString))
                .Parameters.AddWithValue("cat", ComboBox2.SelectedValue.ToString)
            End With
            dt = ds.Tables("dtInventarioGeneral")

            da = New SqlDataAdapter(cmd)
            da.Fill(ds.Tables("dtInventarioGeneral"))

            For i = 0 To dt.Columns.Count - 1
                DataGridView1.Columns(i).DataPropertyName = dt.Columns(i).ToString
            Next

            dv = dt.DefaultView
            DataGridView1.DataSource = dv
        Catch ex As NullReferenceException
            MessageBox.Show("No se ha elegido ninguna sucursal", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Catch ex As Exception
            MessageBox.Show("Error al cargar los datos" & vbCrLf & "Error: " & ex.ToString)
        Finally
            closeConnection()

        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If CheckBox1.Checked = True Then
            llenarDTSMP2()
        Else
            llenarDTSMP()
        End If

    End Sub

    Private Sub frmInventarioRPT_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If nombreRol <> "ADMINISTRADOR" Then
            sql = sql + condicion
        End If
        ComboBox1.DataSource = updateCm(sql)
        ComboBox1.DisplayMember = updateCm(sql).Columns(1).ToString
        ComboBox1.ValueMember = updateCm(sql).Columns(0).ToString
        ComboBox1.SelectedIndex = -1

        
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            Dim dt As DataTable
            dt = ds.Tables("dtInventarioGeneral")
            dt.Clear()
            DataGridView1.DataSource = dt
        Catch ex As NullReferenceException
            MessageBox.Show("No se ha elegido ninguna sucursal", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
        
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If DataGridView1.Rows.Count > 0 Then
            Dim informe As New rptInventario2

            informe.SetDataSource(ds.Tables("dtInventarioGeneral"))
            informe.SetParameterValue("sucursal", If(nombreRol <> "Administrador", nameSucActual, ComboBox1.Text))
            informe.SetParameterValue("laboratorio", ComboBox2.Text)

            frmVerReportes.CrystalReportViewer1.ReportSource = informe
            frmVerReportes.Show()
        Else
            MessageBox.Show("No se eligió ninguna sucursal", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Warning)

        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            ComboBox2.Enabled = True
            'Combobox categoría
            ComboBox2.DataSource = updateCm(sql2)
            ComboBox2.DisplayMember = updateCm(sql2).Columns(0).ToString
            ComboBox2.ValueMember = updateCm(sql2).Columns(0).ToString
            ComboBox2.SelectedIndex = -1
        Else
            ComboBox2.Enabled = False
        End If
    End Sub
End Class