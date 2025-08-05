Imports System.Security.Cryptography

Module PasswordHelper
    Public Function GenerateSalt() As Byte()
        Dim salt(15) As Byte
        Using rng As New RNGCryptoServiceProvider()
            rng.GetBytes(salt)
        End Using
        Return salt
    End Function

    Public Function HashPassword(password As String, salt As Byte()) As Byte()
        Using pbkdf2 As New Rfc2898DeriveBytes(password, salt, 100000)
            Return pbkdf2.GetBytes(32)
        End Using
    End Function

End Module
