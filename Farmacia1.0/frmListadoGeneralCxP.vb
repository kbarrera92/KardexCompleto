Public Class frmListadoGeneralCxP

    Dim sql As String = "SELECT idCuenta, concepto, fechaInicio, fechaLimite, totalCuenta, saldoCuenta, E.estadoCXP, p.rzProveedor, idCompra FROM CUENTAXPAGAR C " _
                        & "INNER JOIN ESTADOCXP E " _
                        & "ON estado = E.idEstadoCXP " _
                        & "INNER JOIN PROVEEDOR P " _
                        & "ON C.idProveedor = P.idProveedor " _
                        & "WHERE C.estado = 100"

    Private Sub frmListadoGeneralCxP_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        fillDGV(sql, DataGridView1, Me)
    End Sub
End Class