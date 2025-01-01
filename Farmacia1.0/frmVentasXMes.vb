Imports System.Data.SqlClient
Public Class frmVentasXMes

    Dim sqlSuc As String = "SELECT idSucursal, nombreSuc FROM SUCURSAL"
    Dim sqlUsuario As String
    Sub llebarcmbanio()
        For i = 2019 To DateTime.Now.Year
            ComboBox1.Items.Add(i)
        Next
    End Sub

    Dim ds As DataSet
    Sub calculartotal()
        Dim tot As Double = 0.0
        For i = 0 To DataGridView1.Rows.Count - 1
            tot = tot + DataGridView1.Rows(i).Cells(6).Value
        Next
        TextBox1.Text = FormatNumber(tot, 2)
    End Sub

    'POR MES CON USUARIO
    Sub llenarDTSMP()
        Dim cmd As SqlCommand
        Dim da As SqlDataAdapter
        Dim dt As DataTable
        ds = New dsReportes

        Try
            openConnection()
            cmd = New SqlCommand()
            With cmd
                .CommandText = "sp_cortexmesusuario"
                .CommandType = CommandType.StoredProcedure
                .Connection = conn
                .Parameters.AddWithValue("suc", CInt(ComboBox3.SelectedValue.ToString))
                .Parameters.AddWithValue("mes", (ComboBox2.SelectedIndex + 1))
                .Parameters.AddWithValue("anio", CInt(ComboBox1.Text))
                .Parameters.AddWithValue("user", CInt(ComboBox4.SelectedValue.ToString))
            End With
            dt = ds.Tables("dtcortecaja")

            da = New SqlDataAdapter(cmd)
            da.FillSchema(ds.Tables("dtcortecaja"), SchemaType.Source)
            da.Fill(ds.Tables("dtcortecaja"))

            For i = 0 To dt.Columns.Count - 1
                DataGridView1.Columns(i).DataPropertyName = dt.Columns(i).ToString
            Next

            dv = dt.DefaultView
            DataGridView1.DataSource = dv
        Catch ex As NullReferenceException
            MessageBox.Show("No se ha elegido un valor obligatorio", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Catch ex As Exception
            MessageBox.Show("Error al cargar los datos" & vbCrLf & "Error: " & ex.ToString)
        Finally
            closeConnection()

        End Try
    End Sub

    'POR MES SIN USUARIO
    Sub llenarDTSMP1()
        Dim cmd As SqlCommand
        Dim da As SqlDataAdapter
        Dim dt As DataTable
        ds = New dsReportes

        Try
            openConnection()
            cmd = New SqlCommand()
            With cmd
                .CommandText = "sp_cortexmes"
                .CommandType = CommandType.StoredProcedure
                .Connection = conn
                .Parameters.AddWithValue("suc", CInt(ComboBox3.SelectedValue.ToString))
                .Parameters.AddWithValue("mes", (ComboBox2.SelectedIndex + 1))
                .Parameters.AddWithValue("anio", CInt(ComboBox1.Text))

            End With
            dt = ds.Tables("dtcortecaja")

            da = New SqlDataAdapter(cmd)
            da.Fill(ds.Tables("dtcortecaja"))

            For i = 0 To dt.Columns.Count - 1
                DataGridView1.Columns(i).DataPropertyName = dt.Columns(i).ToString
            Next

            dv = dt.DefaultView
            DataGridView1.DataSource = dv
        Catch ex As NullReferenceException
            MessageBox.Show("No se ha elegido un valor obligatorio", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Catch ex As Exception
            MessageBox.Show("Error al cargar los datos" & vbCrLf & "Error: " & ex.ToString)
        Finally
            closeConnection()

        End Try
    End Sub

    'POR INTERVALO CON USUARIO
    Sub llenarDTSMP2()
        Dim cmd As SqlCommand
        Dim da As SqlDataAdapter
        Dim dt As DataTable
        ds = New dsReportes

        Try
            openConnection()
            cmd = New SqlCommand()
            With cmd
                .CommandText = "sp_cortexmesintervalousuario"
                .CommandType = CommandType.StoredProcedure
                .Connection = conn
                .Parameters.AddWithValue("suc", CInt(ComboBox3.SelectedValue.ToString))
                .Parameters.AddWithValue("fi", DateTimePicker1.Value)
                .Parameters.AddWithValue("ff", DateTimePicker2.Value)
                .Parameters.AddWithValue("user", CInt(ComboBox4.SelectedValue.ToString))

            End With
            dt = ds.Tables("dtcortecaja")

            da = New SqlDataAdapter(cmd)
            da.Fill(ds.Tables("dtcortecaja"))

            For i = 0 To dt.Columns.Count - 1
                DataGridView1.Columns(i).DataPropertyName = dt.Columns(i).ToString
            Next

            dv = dt.DefaultView
            DataGridView1.DataSource = dv
        Catch ex As NullReferenceException
            MessageBox.Show("No se ha elegido un valor obligatorio", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Catch ex As Exception
            MessageBox.Show("Error al cargar los datos" & vbCrLf & "Error: " & ex.ToString)
        Finally
            closeConnection()

        End Try
    End Sub

    'POR INTERVALO SIN USUARIO
    Sub llenarDTSMP3()
        Dim cmd As SqlCommand
        Dim da As SqlDataAdapter
        Dim dt As DataTable
        ds = New dsReportes

        Try
            openConnection()
            cmd = New SqlCommand()
            With cmd
                .CommandText = "sp_cortexmesintervalo"
                .CommandType = CommandType.StoredProcedure
                .Connection = conn
                .Parameters.AddWithValue("suc", CInt(ComboBox3.SelectedValue.ToString))
                .Parameters.AddWithValue("fi", DateTimePicker1.Value)
                .Parameters.AddWithValue("ff", DateTimePicker2.Value)

            End With
            dt = ds.Tables("dtcortecaja")

            da = New SqlDataAdapter(cmd)
            da.Fill(ds.Tables("dtcortecaja"))

            For i = 0 To dt.Columns.Count - 1
                DataGridView1.Columns(i).DataPropertyName = dt.Columns(i).ToString
            Next

            dv = dt.DefaultView
            DataGridView1.DataSource = dv
        Catch ex As NullReferenceException
            MessageBox.Show("No se ha elegido un valor obligatorio", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Catch ex As Exception
            MessageBox.Show("Error al cargar los datos" & vbCrLf & "Error: " & ex.ToString)
        Finally
            closeConnection()

        End Try
    End Sub

    Private Sub frmVentasXMes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        llebarcmbanio()
        RadioButton1.Checked = True

        'LLenar combobox sucursal
        ComboBox3.DataSource = updateCm(sqlSuc)
        ComboBox3.ValueMember = updateCm(sqlSuc).Columns(0).ToString
        ComboBox3.DisplayMember = updateCm(sqlSuc).Columns(1).ToString


    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked = True Then
            ComboBox1.Enabled = True
            ComboBox2.Enabled = True
            DateTimePicker1.Enabled = False
            DateTimePicker2.Enabled = False
        Else
            ComboBox1.Enabled = False
            ComboBox2.Enabled = False
            DateTimePicker1.Enabled = True
            DateTimePicker2.Enabled = True
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        sqlUsuario = "SELECT idUsuario, nombreUsuario FROM USUARIO WHERE sucursal = " & CInt(ComboBox3.SelectedValue.ToString)
        If CheckBox1.Checked = True Then
            ComboBox4.DataSource = updateCm(sqlUsuario)
            ComboBox4.ValueMember = updateCm(sqlUsuario).Columns(0).ToString
            ComboBox4.DisplayMember = updateCm(sqlUsuario).Columns(1).ToString
            ComboBox4.Enabled = True
        Else
            ComboBox4.DataSource = Nothing
            ComboBox4.Enabled = False
        End If
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If RadioButton1.Checked = True Then
            If CheckBox1.Checked = True Then
                llenarDTSMP()
            Else
                llenarDTSMP1()
            End If
            calculartotal()
        Else
            If CheckBox1.Checked = True Then
                llenarDTSMP2()
            Else
                llenarDTSMP3()
            End If
            calculartotal()
        End If
    End Sub
End Class