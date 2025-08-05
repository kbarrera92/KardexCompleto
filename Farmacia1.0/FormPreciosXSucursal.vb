Imports System.Data.SqlClient
Imports Serilog

Public Class FormPreciosXSucursal
    Public Property dtPrecios As DataTable
    Private Sub FormPreciosXSucursal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim codigoProducto As String = frmCatalogoProducto.txtcod.Text.Trim()
            Dim precio_default As Decimal = Convert.ToDecimal(frmCatalogoProducto.txtprecio.Text.Trim())
            Dim codigoProductoEnviado As Object
            If codigoProducto = "" Then
                codigoProductoEnviado = DBNull.Value
            Else
                codigoProductoEnviado = Convert.ToInt32(codigoProducto)
            End If
            Dim parametros As New List(Of SqlParameter) From {
                New SqlParameter("@opcion", SqlDbType.SmallInt) With {.Value = 1},
                New SqlParameter("@msg", SqlDbType.VarChar, 200) With {.Direction = ParameterDirection.Output},
                New SqlParameter("@codigo_producto", SqlDbType.Int) With {.Value = codigoProductoEnviado},
                New SqlParameter("@precio_default", SqlDbType.Decimal) With {.Value = precio_default}
            }

            DataGridView1.AutoGenerateColumns = False
            DataGridView1.ReadOnly = False
            DataGridView1.Columns("idSucursal").ReadOnly = True
            DataGridView1.Columns("Sucursal").ReadOnly = True
            DataGridView1.Columns("precio").ReadOnly = False
            ListarSucursalesPrecios(parametros)
        Catch ex As Exception
            Log.Error($"Ocurrio un error. Error: {ex.Message}")
        End Try


    End Sub

    Private Sub ListarSucursalesPrecios(parametros As List(Of SqlParameter))
        Try
            Dim paramtodb As String = String.Empty
            For Each param As SqlParameter In parametros
                paramtodb &= param.ParameterName & "=" & If(param.Value, """") & vbCrLf
            Next
            Log.Information($"Parametros enviados al sp: {vbCrLf}{paramtodb}")

            Dim resultado = EjecutarStoredProcedureMultiple("sp_mantPreciosXSucursal", parametros)
            Dim tablaResultado = resultado.Item1(0)

            For i = 0 To tablaResultado.Columns.Count - 1
                DataGridView1.Columns(i).DataPropertyName = tablaResultado.Columns(i).ToString
            Next

            tablaResultado.Columns("precio").ReadOnly = False
            DataGridView1.DataSource = tablaResultado
        Catch ex As Exception
            Log.Error($"Ocurrio un error. Error: {ex.Message}")
        End Try


    End Sub

    Private Sub DataGridView1_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles DataGridView1.EditingControlShowing
        Dim columna As Integer = DataGridView1.CurrentCell.ColumnIndex

        ' Verifica si estás editando la columna "Precio" (ajusta el índice o nombre si es necesario)
        If DataGridView1.Columns(columna).Name = "precio" Then
            Dim txt As TextBox = CType(e.Control, TextBox)

            ' Primero quitamos cualquier manejador anterior para evitar duplicados
            RemoveHandler txt.KeyPress, AddressOf SoloNumerosDecimal
            AddHandler txt.KeyPress, AddressOf SoloNumerosDecimal
        End If
    End Sub

    Private Sub SoloNumerosDecimal(sender As Object, e As KeyPressEventArgs)
        Dim txt As TextBox = CType(sender, TextBox)

        ' Permitir teclas de control como borrar
        If Char.IsControl(e.KeyChar) Then
            Exit Sub
        End If

        ' Permitir solo un punto decimal
        If e.KeyChar = "."c AndAlso txt.Text.Contains(".") Then
            e.Handled = True
            Exit Sub
        End If

        ' Permitir dígitos numéricos
        If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> "."c Then
            e.Handled = True
        End If
    End Sub

    Private Sub DataGridView1_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles DataGridView1.CellValidating
        If DataGridView1.Columns(e.ColumnIndex).Name = "precio" Then
            Dim value As String = e.FormattedValue.ToString()
            Dim result As Decimal

            If Not Decimal.TryParse(value, result) Then
                MessageBox.Show("Ingrese un valor numérico válido para el precio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                e.Cancel = True
            ElseIf Decimal.Parse(value) <= 0 Then
                MessageBox.Show("Ingrese un valor numérico válido para el precio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub btnGrabarPrecios_Click(sender As Object, e As EventArgs) Handles btnGrabarPrecios.Click
        GrabaPrecios()
    End Sub

    Private Sub GrabaPrecios()
        Try
            Dim dt As New DataTable()
            dt.Columns.Add("idSucursal", GetType(Integer))
            dt.Columns.Add("precio", GetType(Decimal))

            For Each row As DataGridViewRow In DataGridView1.Rows
                If Not row.IsNewRow Then
                    Dim idSucursal As Integer = Convert.ToInt32(row.Cells("idSucursal").Value)
                    Dim precio As Decimal = Convert.ToDecimal(row.Cells("precio").Value)
                    dt.Rows.Add(idSucursal, precio)
                End If
            Next

            ' Asignamos la DataTable a la propiedad pública
            dtPrecios = dt

            MessageBox.Show("Se grabaron los precios.\n Para confirmar, guarde o actualice el producto.", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Cierra el formulario y vuelve al formulario principal
            DialogResult = DialogResult.OK
            Close()
        Catch ex As Exception
            Log.Error($"Ocurrio un error. Error: {ex.Message}")
        End Try


    End Sub
End Class