Imports System.Data.SqlClient
Imports Serilog

Module UserMigrationOnce
    Public Sub MigrarContrasenasAHash()
        openConnection()

        Dim toMigrate As New List(Of (Id As Integer, PlainPwd As String))()
        Using fetchCmd As New SqlCommand(
        "SELECT idUsuario, contraUsuario 
           FROM USUARIO
          WHERE contraUsuario IS NOT NULL AND TRIM(contraUsuario) <> ''", conn)
            Using reader = fetchCmd.ExecuteReader()
                While reader.Read()
                    toMigrate.Add((
                      Id:=CInt(reader("idUsuario")),
                      PlainPwd:=reader("contraUsuario").ToString()
                    ))
                End While
            End Using
        End Using

        Const updateSql = "
        UPDATE USUARIO
           SET PasswordSalt   = @salt,
               PasswordHash   = @hash
         WHERE idUsuario      = @id"

        For Each u In toMigrate
            Dim salt() = GenerateSalt()
            Dim hash() = HashPassword(u.PlainPwd, salt)

            Using updCmd As New SqlCommand(updateSql, conn)
                updCmd.Parameters.Add("@salt", SqlDbType.VarBinary, 128).Value = salt
                updCmd.Parameters.Add("@hash", SqlDbType.VarBinary, 256).Value = hash
                updCmd.Parameters.Add("@id", SqlDbType.Int).Value = u.Id
                updCmd.ExecuteNonQuery()
            End Using
        Next

        closeConnection()

        MsgBox("Migración completada:" & vbCrLf &
           $"  • Usuarios procesados: {toMigrate.Count}{vbCrLf}" &
           "  • contrasenas planas puestas en NULL",
           MsgBoxStyle.Information, "Migración finalizada")
    End Sub

End Module
