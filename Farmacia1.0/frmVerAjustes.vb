Imports System.Data.SqlClient

Public Class frmVerAjustes

    Dim total As Double = 0
    Dim ds As DataSet

    Sub actualizarTotal()
        Dim sql As String = "UPDATE AJUSTE SET total = @tot WHERE nAjuste = @trans"
        Dim comando As SqlCommand

        Try
            comando = New SqlCommand(sql, conn)
            comando.Parameters.AddWithValue("tot", total)
            comando.Parameters.AddWithValue("trans", CInt(txtnajuste.Text))

            openConnection()
            comando.ExecuteNonQuery()
            closeConnection()
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "Algo salió mal")
        End Try
    End Sub

    Function calcularTotal() As Double
        Dim tot As Double = 0
        For i = 0 To DataGridView2.Rows.Count - 1
            tot = tot + DataGridView2.Rows(i).Cells(5).Value
        Next
        Return tot
    End Function

    Sub actualizarDetalle()
        Dim sql As String = "UPDATE DETAJUSTE SET precio = @prec, cantidad = @cant, subtotal = @subt WHERE (ndetajuste = @det AND najuste = @trans)"
        Dim comando As SqlCommand

        Try
            comando = New SqlCommand(sql, conn)
            With comando.Parameters
                .AddWithValue("prec", CDbl(DataGridView2.CurrentRow.Cells(4).Value))
                .AddWithValue("cant", CDbl(DataGridView2.CurrentRow.Cells(3).Value))
                .AddWithValue("det", CDbl(DataGridView2.CurrentRow.Cells(0).Value))
                .AddWithValue("trans", CInt(txtnajuste.Text))
                .AddWithValue("subt", CDbl(DataGridView2.CurrentRow.Cells(5).Value))
            End With

            openConnection()
            comando.ExecuteNonQuery()
            'MsgBox("El detalle se actualizó correctamente", MsgBoxStyle.Information, "Correcto")
            'limpiar()
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "Algo salió mal")

        Finally
            closeConnection()
            fillDGVSP("detAjustes", DataGridView2, Me, CInt(txtnajuste.Text))

        End Try
    End Sub

    Private Sub frmVerAjustes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DataGridView1.AutoGenerateColumns = False
        DataGridView2.AutoGenerateColumns = False

        filldgvestandar("getAjustes", DataGridView1, Me)
    End Sub





    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If nombreRol = "BODEGUERO" Then
            MessageBox.Show("No tiene permisos para realizar esta acción", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            If MessageBox.Show("¿Desea eliminar este ajuste permanentemente?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                Try
                    Dim sqlEliminarAj As String = "DELETE FROM AJUSTE WHERE nAjuste = @na"
                    Dim cmd As SqlCommand
                    cmd = New SqlCommand(sqlEliminarAj, conn)

                    cmd.Parameters.AddWithValue("na", CInt(txtnajuste.Text))

                    openConnection()
                    cmd.ExecuteNonQuery()
                    closeConnection()
                    MsgBox("El registro se eliminó correctamente", MsgBoxStyle.Information, "Eliminado")
                Catch ex As Exception
                    MsgBox("Algo salío mal" & vbCrLf & "Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
                Finally
                    txtnajuste.Clear()
                    txtfecha.Clear()
                    txtconcep.Clear()
                    txtsuc.Clear()
                    txttipoa.Clear()
                    DataGridView2.DataSource = Nothing
                    DataGridView2.Rows.Clear()
                    filldgvestandar("getAjustes", DataGridView1, Me)
                End Try

            End If
        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim fecha As Date

        fecha = txtfecha.Text

        If IsDate(fecha) Then
            Try
                Dim sqlUpdateA As String = "UPDATE AJUSTE SET fecha = @fecha, concepto = @con WHERE nAjuste = @na"
                Dim cmd As SqlCommand
                cmd = New SqlCommand(sqlUpdateA, conn)

                cmd.Parameters.AddWithValue("fecha", Format(fecha, "dd/MM/yyyy"))
                cmd.Parameters.AddWithValue("con", txtconcep.Text)
                cmd.Parameters.AddWithValue("na", CInt(txtnajuste.Text))

                openConnection()
                cmd.ExecuteNonQuery()
                closeConnection()
                MsgBox("Cambios guardados", MsgBoxStyle.Information, "Modificado")
            Catch ex As Exception
                MsgBox("No se guardaron los cambios", MsgBoxStyle.Critical, "Error")
            Finally
                filldgvestandar("getAjustes", DataGridView1, Me)
            End Try
        End If
    End Sub

    Private Sub btnVerDetalles_Click(sender As Object, e As EventArgs) Handles btnVerDetalles.Click
        Try
            fillDGVSP("detAjustes", DataGridView2, Me, CInt(txtnajuste.Text))
        Catch ex As Exception

        End Try

    End Sub

    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.Enter Then
            Try
                txtnajuste.Text = DataGridView1.CurrentRow.Cells(0).Value
                txtfecha.Text = Format(DataGridView1.CurrentRow.Cells(1).Value, "dd/MM/yyyy")
                txtconcep.Text = DataGridView1.CurrentRow.Cells(4).Value
                txtsuc.Text = DataGridView1.CurrentRow.Cells(2).Value
                txttipoa.Text = DataGridView1.CurrentRow.Cells(6).Value
                e.SuppressKeyPress = True
            Catch ex As Exception

            End Try
        End If
    End Sub





    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub DataGridView2_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellEndEdit
        DataGridView2.CurrentRow.Cells(5).Value = FormatNumber(CDbl(DataGridView2.CurrentRow.Cells(4).Value) * CDbl(DataGridView2.CurrentRow.Cells(3).Value), 2)
        actualizarDetalle()
        total = FormatNumber(calcularTotal(), 2)
        actualizarTotal()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If nombreRol = "BODEGUERO" Then
            MessageBox.Show("No tiene permisos para realizar esta acción", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            If DataGridView2.Rows.Count = 0 Or DataGridView2.SelectedRows.Count = 0 Then
                MsgBox("Debe seleccionar un detalle para eliminar", MsgBoxStyle.Exclamation, "Faltan datos")
            Else
                Try
                    Dim ndet As Integer = DataGridView2.CurrentRow.Cells(0).Value
                    Dim ntrans As Integer = CInt(txtnajuste.Text)

                    Dim cmd As SqlCommand
                    Dim sqlDeleteDet As String = "DELETE FROM DETAJUSTE WHERE ndetajuste = @ndet AND najuste = @najuste"

                    cmd = New SqlCommand(sqlDeleteDet, conn)

                    With cmd.Parameters
                        .AddWithValue("ndet", ndet)
                        .AddWithValue("najuste", ntrans)
                    End With

                    openConnection()
                    cmd.ExecuteNonQuery()
                    closeConnection()
                    MsgBox("Eliminado correctamente", MsgBoxStyle.Information, "Borrado")
                Catch ex As Exception
                    MsgBox("No se puede borrar este detalle" & vbCrLf & "Error: " & ex.Message, MsgBoxStyle.Critical, "Error")
                Finally
                    fillDGVSP("detAjustes", DataGridView2, Me, CInt(txtnajuste.Text))
                    total = FormatNumber(calcularTotal(), 2)
                End Try
            End If
        End If

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If DataGridView1.SelectedRows.Count > 0 Then
            llenarDTSMP()
            Dim informe As New rptAjuste

            informe.SetDataSource(ds.Tables("dtDetAjuste"))
            informe.SetParameterValue("nAjuste", CInt(txtnajuste.Text))
            informe.SetParameterValue("fecha", txtfecha.Text)
            informe.SetParameterValue("concepto", txtconcep.Text)
            informe.SetParameterValue("tipo", txttipoa.Text)
            informe.SetParameterValue("sucursal", txtsuc.Text)

            frmVerReportes.CrystalReportViewer1.ReportSource = informe
            frmVerReportes.Show()
        Else
            MsgBox("Debe seleccionar un ajuste para imprimir su reporte", MsgBoxStyle.Exclamation, "Faltan datos")
        End If
    End Sub

    Sub llenarDTSMP()
        Dim cmd As SqlCommand
        Dim da As SqlDataAdapter
        Dim dt As DataTable
        ds = New dsReportes

        Try
            openConnection()
            cmd = New SqlCommand()
            With cmd
                .CommandText = "detAjustes"
                .CommandType = CommandType.StoredProcedure
                .Connection = conn
                .Parameters.AddWithValue("suc", CInt(txtnajuste.Text))

            End With
            dt = ds.Tables("dtDetAjuste")

            da = New SqlDataAdapter(cmd)
            da.Fill(ds.Tables("dtDetAjuste"))

        Catch ex As Exception
            MessageBox.Show("Error al cargar los datos" & vbCrLf & "Error: " & ex.ToString)
        Finally
            closeConnection()

        End Try
    End Sub
End Class