Public Class EgresoValidator
    ' Referencia al ErrorProvider
    Private ReadOnly _errorProvider As ErrorProvider

    ' Constructor que recibe el ErrorProvider del formulario
    Public Sub New(errorProvider As ErrorProvider)
        _errorProvider = errorProvider
    End Sub

    ' Método para validar todos los campos
    Public Function ValidarGasto(gasto As Egreso, controles As Dictionary(Of String, Control)) As Boolean
        ' Limpiar errores previos
        _errorProvider.Clear()

        Dim esValido As Boolean = True

        ' Validar cada propiedad y configurar el error en el control correspondiente
        If Not ValidarFecha(gasto.Fecha, controles("Fecha")) Then esValido = False
        If Not ValidarCategoria(gasto.IdCategoria, controles("Categoria")) Then esValido = False
        If Not ValidarDescripcion(gasto.Descripcion, controles("Descripcion")) Then esValido = False
        If Not ValidarTotal(gasto.Total, controles("Total")) Then esValido = False
        If Not ValidarSucursal(gasto.IdSucursal, controles("Sucursal")) Then esValido = False
        If Not ValidarUsuarioRegistra(gasto.IdUsuario, controles("UsuarioRegistra")) Then esValido = False

        Return esValido
    End Function

    Private Function ValidarUsuarioRegistra(usuarioRegistra As String, control As Control) As Boolean
        If String.IsNullOrWhiteSpace(usuarioRegistra) Then
            _errorProvider.SetError(control, "El campo usuario no puede estar vacío")
            Return False
        ElseIf usuarioRegistra.Length > 50 Then
            _errorProvider.SetError(control, "El campo usuario excede el límite permitido")
            Return False
        End If

        _errorProvider.SetError(control, "")
        Return True
    End Function

    ' Métodos individuales para cada validación
    Private Function ValidarFecha(fecha As DateTime, control As Control) As Boolean
        If fecha.Date > DateTime.Now.Date Then
            _errorProvider.SetError(control, "La fecha no puede ser futura")
            Return False
        End If

        _errorProvider.SetError(control, "")
        Return True
    End Function

    Private Function ValidarCategoria(idCategoria As Integer, control As Control) As Boolean
        If idCategoria <= 0 Then
            _errorProvider.SetError(control, "Debe seleccionar una categoría")
            Return False
        End If

        _errorProvider.SetError(control, "")
        Return True
    End Function

    Private Function ValidarDescripcion(descripcion As String, control As Control) As Boolean
        If String.IsNullOrWhiteSpace(descripcion) Then
            _errorProvider.SetError(control, "La descripción no puede estar vacía")
            Return False
        ElseIf descripcion.Length > 100 Then
            _errorProvider.SetError(control, "La descripción excede el límite permitido")
            Return False
        End If

        _errorProvider.SetError(control, "")
        Return True
    End Function

    Private Function ValidarTotal(total As Decimal, control As Control) As Boolean
        If total <= 0 Then
            _errorProvider.SetError(control, "El total debe ser mayor que cero")
            Return False
        End If

        _errorProvider.SetError(control, "")
        Return True
    End Function

    Private Function ValidarSucursal(idSucursal As Integer, control As Control) As Boolean
        If idSucursal <= 0 Then
            _errorProvider.SetError(control, "Debe seleccionar una sucursal")
            Return False
        End If

        _errorProvider.SetError(control, "")
        Return True
    End Function

    ' Métodos individuales para validar controles (para usar con eventos Validating)
    Public Sub ValidarControlFecha(control As DateTimePicker)
        ValidarFecha(control.Value, control)
    End Sub

    Public Sub ValidarControlCategoria(control As ComboBox)
        Dim idCategoria As Integer = If(control.SelectedValue IsNot Nothing, Convert.ToInt32(control.SelectedValue), 0)
        ValidarCategoria(idCategoria, control)
    End Sub

    Public Sub ValidarControlDescripcion(control As TextBox)
        ValidarDescripcion(control.Text, control)
    End Sub

    Public Sub ValidarControlTotal(control As TextBox)
        Dim total As Decimal
        If Not Decimal.TryParse(control.Text, total) Then
            _errorProvider.SetError(control, "El total debe ser un valor numérico")
        Else
            ValidarTotal(total, control)
        End If
    End Sub

    Public Sub ValidarControlSucursal(control As ComboBox)
        Dim idSucursal As Integer = If(control.SelectedValue IsNot Nothing, Convert.ToInt32(control.SelectedValue), 0)
        ValidarSucursal(idSucursal, control)
    End Sub
End Class