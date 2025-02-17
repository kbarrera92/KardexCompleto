Imports System.Data.SqlClient
Public Class frmVerVentas

    Dim sql As String = "SELECT idSucursal, nombreSuc FROM SUCURSAL"
    Dim ds As DataSet
    Sub llenarDTSMP()
        Dim cmd As SqlCommand
        Dim da As SqlDataAdapter
        Dim dt As DataTable
        ds = New dsReportes

        Try
            openConnection()
            cmd = New SqlCommand()
            With cmd
                .CommandText = "sp_ventasSuc"
                .CommandType = CommandType.StoredProcedure
                .Connection = conn
                .Parameters.AddWithValue("suc", CInt(ComboBox1.SelectedValue.ToString))

            End With
            dt = ds.Tables("dtVentasXsuc")

            da = New SqlDataAdapter(cmd)
            da.FillSchema(ds.Tables("dtVentasXsuc"), SchemaType.Source)
            da.Fill(ds.Tables("dtVentasXsuc"))

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
                .CommandText = "sp_ventasxuser"
                .CommandType = CommandType.StoredProcedure
                .Connection = conn
                .Parameters.AddWithValue("suc", CInt(ComboBox1.SelectedValue.ToString))
                .Parameters.AddWithValue("us", CInt(ComboBox2.SelectedValue.ToString))
            End With
            dt = ds.Tables("dtVentasXsuc")

            da = New SqlDataAdapter(cmd)
            da.FillSchema(ds.Tables("dtVentasXsuc"), SchemaType.Source)
            da.Fill(ds.Tables("dtVentasXsuc"))

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

    Sub llenarDTSMP3()
        Dim cmd As SqlCommand
        Dim da As SqlDataAdapter
        Dim dt As DataTable
        ds = New dsReportes

        Try
            openConnection()
            cmd = New SqlCommand()
            With cmd
                .CommandText = "sp_ventasxusuariofecha"
                .CommandType = CommandType.StoredProcedure
                .Connection = conn
                .Parameters.AddWithValue("suc", CInt(ComboBox1.SelectedValue.ToString))
                .Parameters.AddWithValue("us", CInt(ComboBox2.SelectedValue.ToString))
                .Parameters.AddWithValue("fecha", DateTimePicker1.Value)
            End With
            dt = ds.Tables("dtVentasXsuc")

            da = New SqlDataAdapter(cmd)
            da.FillSchema(ds.Tables("dtVentasXsuc"), SchemaType.Source)
            da.Fill(ds.Tables("dtVentasXsuc"))

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

    Sub llenarDTSMP4()
        Dim cmd As SqlCommand
        Dim da As SqlDataAdapter
        Dim dt As DataTable
        ds = New dsReportes

        Try
            openConnection()
            cmd = New SqlCommand()
            With cmd
                .CommandText = "ventasdiariassuc"
                .CommandType = CommandType.StoredProcedure
                .Connection = conn
                .Parameters.AddWithValue("suc", CInt(ComboBox1.SelectedValue.ToString))

                .Parameters.AddWithValue("fecha", DateTimePicker1.Value)
            End With
            dt = ds.Tables("dtVentasXsuc")

            da = New SqlDataAdapter(cmd)
            da.FillSchema(ds.Tables("dtVentasXsuc"), SchemaType.Source)
            da.Fill(ds.Tables("dtVentasXsuc"))

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

    Sub llenarDTSMP5()
        Dim cmd As SqlCommand
        Dim da As SqlDataAdapter
        Dim dt As DataTable
        ds = New dsReportes

        Try
            openConnection()
            cmd = New SqlCommand()
            With cmd
                .CommandText = "sp_ventasxusuariofecha"
                .CommandType = CommandType.StoredProcedure
                .Connection = conn
                .Parameters.AddWithValue("suc", sucActual)
                .Parameters.AddWithValue("us", usuarioActual)
                .Parameters.AddWithValue("fecha", DateTimePicker1.Value)
            End With
            dt = ds.Tables("dtVentasXsuc")

            da = New SqlDataAdapter(cmd)
            da.FillSchema(ds.Tables("dtVentasXsuc"), SchemaType.Source)
            da.Fill(ds.Tables("dtVentasXsuc"))

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

    Private Sub frmVerVentas_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If nombreRol = "VENDEDOR" Then
            ComboBox1.Enabled = False
            CheckBox1.Enabled = False
            Button1.Enabled = False
        Else
            ComboBox1.Enabled = True
            CheckBox1.Enabled = True
            Button1.Enabled = True
        End If

        ComboBox1.DataSource = updateCm(sql)
        ComboBox1.DisplayMember = updateCm(sql).Columns(1).ToString
        ComboBox1.ValueMember = updateCm(sql).Columns(0).ToString
        ComboBox1.SelectedIndex = -1
    End Sub


    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            ComboBox2.Enabled = True
            'Combobox categoría
            Dim sql2 As String = "SELECT idUsuario, nombreUsuario FROM USUARIO WHERE sucursal = " & CInt(ComboBox1.SelectedValue.ToString)
            ComboBox2.DataSource = updateCm(sql2)
            ComboBox2.DisplayMember = updateCm(sql2).Columns(1).ToString
            ComboBox2.ValueMember = updateCm(sql2).Columns(0).ToString
            ComboBox2.SelectedIndex = -1
        Else
            ComboBox2.Enabled = False
        End If
    End Sub


    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            DateTimePicker1.Enabled = True
            'Combobox categoría

        Else
            DateTimePicker1.Enabled = False
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If nombreRol = "VENDEDOR" Then
            llenarDTSMP5()
        Else
            If CheckBox1.Checked = True And CheckBox2.Checked = False Then
                llenarDTSMP2()
            Else
                If CheckBox1.Checked = True And CheckBox2.Checked = True Then
                    llenarDTSMP3()
                Else
                    If CheckBox1.Checked = False And CheckBox2.Checked = True Then
                        llenarDTSMP4()
                    Else
                        llenarDTSMP()
                    End If

                End If

            End If
        End If
        
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Try
            Dim dt As DataTable
            dt = ds.Tables("dtVentasXsuc")
            dt.Clear()
            DataGridView1.DataSource = dt
        Catch ex As NullReferenceException
            MessageBox.Show("No se ha elegido ninguna sucursal", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If DataGridView1.Rows.Count > 0 Then
            Try
                Dim informe As New rptVentas

                informe.SetDataSource(ds.Tables("dtVentasXsuc"))
                informe.SetParameterValue(0, ConsultaParametro("nombreEmpresa"))
                informe.SetParameterValue(1, ConsultaParametro("eslogan"))
                frmVerReportes.CrystalReportViewer1.ReportSource = informe
                frmVerReportes.Show()
            Catch ex As Exception

            End Try
        Else
            MessageBox.Show("No se eligió ninguna sucursal", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Warning)

        End If
    End Sub

    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.Enter Then
            frmVerDetalleVentas.TextBox1.Text = DataGridView1.CurrentRow.Cells(0).Value
            frmVerDetalleVentas.Show()

        End If
    End Sub

    
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If MessageBox.Show("¿Desea eliminar este registro?" & vbCrLf & "Se eliminarán los datos asociados a él.", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim sqlDelete As String = "DELETE FROM VENTA WHERE nVenta = @nv"
            Dim cmd As SqlCommand
            Try
                cmd = New SqlCommand(sqlDelete, conn)

                cmd.Parameters.AddWithValue("nv", CInt(DataGridView1.CurrentRow.Cells(0).Value))

                openConnection()
                cmd.ExecuteNonQuery()
                closeConnection()
                MsgBox("Registro eliminado exitosamente", MsgBoxStyle.Information, "Eliminado")
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub
End Class