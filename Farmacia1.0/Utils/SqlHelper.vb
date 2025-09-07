Imports System.Data.SqlClient

Public Class SqlHelper
    Public Shared Function ExecuteStoredProcedure(spName As String,
                                              Optional parameters As List(Of SqlParameter) = Nothing) As SpResult

        Dim ds As New DataSet()
        Dim outputValues As New Dictionary(Of String, Object)()

        Using cmd As New SqlCommand(spName, conn)
            cmd.CommandType = CommandType.StoredProcedure

            If parameters IsNot Nothing Then
                cmd.Parameters.AddRange(parameters.ToArray())
            End If

            Dim da As New SqlDataAdapter(cmd)
            da.Fill(ds)

            For Each p As SqlParameter In cmd.Parameters
                If p.Direction = ParameterDirection.Output OrElse
               p.Direction = ParameterDirection.InputOutput OrElse
               p.Direction = ParameterDirection.ReturnValue Then
                    outputValues(p.ParameterName) = p.Value
                End If
            Next
        End Using

        Return New SpResult With {
            .Data = ds,
            .OutputParams = outputValues
        }

    End Function
End Class
