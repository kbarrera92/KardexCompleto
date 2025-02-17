Imports System.Data.SqlClient
Public Class frmVerTraslados

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
                .CommandText = "getDetalleTraslado"
                .CommandType = CommandType.StoredProcedure
                .Connection = conn
                .Parameters.AddWithValue("suc", CInt(TextBox1.Text))

            End With
            dt = ds.Tables("dtDetTraslados")

            da = New SqlDataAdapter(cmd)
            da.FillSchema(ds.Tables("dtDetTraslados"), SchemaType.Source)
            da.Fill(ds.Tables("dtDetTraslados"))

        Catch ex As Exception
            MessageBox.Show("Error al cargar los datos" & vbCrLf & "Error: " & ex.ToString)
        Finally
            closeConnection()

        End Try
    End Sub

    Function getSucEntrada(ByVal nts As Integer) As String
        Dim sqlgetsucentrada As String = "SELECT ISNULL(S.nombreSuc,'''') FROM ENTXTRASLADO ET " _
                                         & "INNER JOIN SUCURSAL S " _
                                         & "ON ET.sucursal = S.idSucursal " _
                                         & "WHERE ET.nSalidaRelacionada = @nss"
        Dim cmd As SqlCommand
        Try
            cmd = New SqlCommand(sqlgetsucentrada, conn)
            cmd.Parameters.AddWithValue("nss", nts)
            openConnection()
            Return Convert.ToString(cmd.ExecuteScalar())
        Catch ex As Exception
            MsgBox(ex.Message)
            Return Nothing
        Finally
            closeConnection()
        End Try

    End Function

    Private Sub frmVerTraslados_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DataGridView1.AutoGenerateColumns = False
        filldgvestandarTraslados("getTraslados", DataGridView1, Me)
        lblCantidadRegistros.Text = String.Format("Cantidad de registros: {0}", DataGridView1.Rows.Count)
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        Try
            TextBox1.Text = DataGridView1.CurrentRow.Cells(0).Value
            TextBox2.Text = DataGridView1.CurrentRow.Cells(1).Value
            TextBox3.Text = DataGridView1.CurrentRow.Cells(2).Value
            TextBox5.Text = DataGridView1.CurrentRow.Cells(3).Value
            TextBox4.Text = getSucEntrada(CInt(TextBox1.Text))
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.Enter Then
            'Ver detalles del traslado
            frmVerDetalleTraslado.TextBox1.Text = DataGridView1.CurrentRow.Cells(0).Value
            frmVerDetalleTraslado.Show()
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If DataGridView1.SelectedRows.Count > 0 Then
            Try
                llenarDTSMP()
                Dim informe As New rptTraslado

                informe.SetDataSource(ds.Tables("dtDetTraslados"))
                informe.SetParameterValue("nTraslado", CInt(TextBox1.Text))
                informe.SetParameterValue(1, ConsultaParametro("nombreEmpresa"))

                frmVerReportes.CrystalReportViewer1.ReportSource = informe
                frmVerReportes.Show()
            Catch ex As Exception

            End Try

        Else
            MsgBox("Debe seleccionar un traslado para imprimir su reporte", MsgBoxStyle.Exclamation, "Faltan datos")
        End If
    End Sub

    Sub filldgvestandarTraslados(ByVal str As String, ByVal dgv As DataGridView, ByVal frm As Form, Optional ByVal opt As Char = "S")
        Dim cmd As SqlCommand
        Dim da As SqlDataAdapter
        Dim dt As New DataTable

        Try
            openConnection()
            cmd = New SqlCommand()

            With cmd
                .CommandText = str
                .CommandType = CommandType.StoredProcedure
                .Connection = conn
                .Parameters.AddWithValue("sucursal", sucActual)
                .Parameters.AddWithValue("usuario", nameUsuarioActual)
                .Parameters.AddWithValue("rolUsuario", nombreRol)
                .Parameters.AddWithValue("opt", opt)
            End With

            da = New SqlDataAdapter(cmd)
            da.Fill(dt)

            For i = 0 To dt.Columns.Count - 1
                dgv.Columns(i).DataPropertyName = dt.Columns(i).ToString
            Next

            dv = dt.DefaultView
            dgv.DataSource = dv

        Catch ex As Exception
            MsgBox("Error al cargar los datos" & vbCrLf & "Error: " & ex.ToString)
        Finally
            closeConnection()
        End Try

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            filldgvestandarTraslados("getTraslados", DataGridView1, Me, "N")
            lblCantidadRegistros.Text = String.Format("Cantidad de registros: {0}", DataGridView1.Rows.Count)
        Else
            filldgvestandarTraslados("getTraslados", DataGridView1, Me)
            lblCantidadRegistros.Text = String.Format("Cantidad de registros: {0}", DataGridView1.Rows.Count)
        End If
    End Sub
End Class