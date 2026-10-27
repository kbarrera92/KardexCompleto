Imports System.Data.SqlClient
Imports Serilog

Public Class frmVerDetalleVentas

    Private Sub CargaDetalles()
        fillDGVSP("getDetallesVentas", DataGridView1, Me, CInt(TextBox1.Text))
    End Sub

    ''' <summary>
    ''' Ejecuta un SP de mantenimiento de detalle (devuelve @message y return value) y muestra el resultado.
    ''' </summary>
    Private Sub EjecutaSpDetalle(ByVal nombreSP As String, ByVal parametros As List(Of SqlParameter))
        parametros.Add(New SqlParameter("@message", SqlDbType.VarChar, 200) With {.Direction = ParameterDirection.Output})
        parametros.Add(New SqlParameter("@returnValue", SqlDbType.Int) With {.Direction = ParameterDirection.ReturnValue})

        Dim paramtodb As String = String.Empty
        For Each param As SqlParameter In parametros
            If param.Direction = ParameterDirection.Input Then
                paramtodb &= param.ParameterName & "=" & If(param.Value, """") & vbCrLf
            End If
        Next
        Log.Information($"Parametros enviados al sp {nombreSP}: {vbCrLf}{paramtodb}")

        EjecutarStoredProcedureMultiple(nombreSP, parametros)

        Dim codigoRetorno As Integer = Convert.ToInt32(parametros.Find(Function(p) p.ParameterName = "@returnValue").Value)
        Dim mensajeSalida As String = parametros.Find(Function(p) p.ParameterName = "@message").Value.ToString()
        MessageBox.Show(mensajeSalida, "Resultado", MessageBoxButtons.OK, If(codigoRetorno = 0, MessageBoxIcon.Information, MessageBoxIcon.Error))
    End Sub

    Sub updateDet(ByVal cantidad As Integer, ByVal precio As Decimal)
        Try
            Dim parametros As New List(Of SqlParameter) From {
                New SqlParameter("@idVenta", SqlDbType.Int) With {.Value = CInt(TextBox1.Text)},
                New SqlParameter("@nDetalle", SqlDbType.SmallInt) With {.Value = Convert.ToInt16(DataGridView1.CurrentRow.Cells(0).Value)},
                New SqlParameter("@cantidad", SqlDbType.Int) With {.Value = cantidad},
                New SqlParameter("@precio", SqlDbType.Decimal) With {.Precision = 18, .Scale = 2, .Value = precio}
            }
            EjecutaSpDetalle("sp_modificaDetalleVenta", parametros)
        Catch ex As Exception
            Log.Error($"Ocurrio un error. Error: {ex.Message}, Trace: {ex.StackTrace}")
            MsgBox("Ocurrió un error. Revise el log del programa", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub EliminaDetalle()
        Try
            Dim parametros As New List(Of SqlParameter) From {
                New SqlParameter("@idVenta", SqlDbType.Int) With {.Value = CInt(TextBox1.Text)},
                New SqlParameter("@nDetalle", SqlDbType.SmallInt) With {.Value = Convert.ToInt16(DataGridView1.CurrentRow.Cells(0).Value)}
            }
            EjecutaSpDetalle("sp_eliminaDetalleVenta", parametros)
        Catch ex As Exception
            Log.Error($"Ocurrio un error. Error: {ex.Message}, Trace: {ex.StackTrace}")
            MsgBox("Ocurrió un error. Revise el log del programa", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub frmVerDetalleVentas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If nombreRol = "VENDEDOR" Then
            DataGridView1.Columns(2).ReadOnly = True
            DataGridView1.Columns(3).ReadOnly = True
            DataGridView1.Columns(4).ReadOnly = True
        End If
        CargaDetalles()
    End Sub

    Private Sub DataGridView1_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit
        Dim cantidad As Integer
        Dim precio As Decimal
        If Integer.TryParse(Convert.ToString(DataGridView1.CurrentRow.Cells(2).Value), cantidad) AndAlso
           Decimal.TryParse(Convert.ToString(DataGridView1.CurrentRow.Cells(3).Value), precio) Then
            updateDet(cantidad, precio)
        Else
            MessageBox.Show("Cantidad o precio inválidos", "Datos incorrectos", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
        'Recarga siempre: revierte la celda si falló y muestra el subtotal calculado por el SP
        CargaDetalles()
    End Sub

    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.Delete AndAlso nombreRol <> "VENDEDOR" AndAlso DataGridView1.CurrentRow IsNot Nothing Then
            e.Handled = True
            If MessageBox.Show("¿Desea eliminar este detalle?" & vbCrLf & "Se devolverán las existencias y se ajustará el total de la venta.", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                EliminaDetalle()
                CargaDetalles()
            End If
        End If
    End Sub

End Class
