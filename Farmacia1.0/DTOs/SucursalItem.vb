Public Class SucursalItem
    Public Property IdSucursal As Integer
    Public Property NombreSucursal As String

    Public Overrides Function ToString() As String
        Return $"{IdSucursal} - {NombreSucursal}"
    End Function
End Class
