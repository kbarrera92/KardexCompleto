Imports System.Data.SqlClient
Public Class frmTrasiegos2

    Dim numTrasiego, numEntT As Integer
    Dim fechaT As String

    Dim table1 As DataTable
    Dim table2 As DataTable


    'Procedimiento para insertar la salida x trasiego
    Sub insertTrasiegoSal()
        Dim sql As String = "INSERT INTO TRASIEGOSSAL VALUES(@fecha, @usuario, @sucursal);"
        Dim cmd As SqlCommand

        cmd = New SqlCommand(sql, conn)

        cmd.Parameters.AddWithValue("fecha", CDate(fechaT))
        cmd.Parameters.AddWithValue("usuario", CInt(usuarioActual))
        cmd.Parameters.AddWithValue("sucursal", CInt(sucActual))

        Try
            openConnection()
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show("Hubo un error al guardar el trasiego" & vbCrLf & "Error: " & ex.Message, "Algo salió mal", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            closeConnection()
            numTrasiego = getCorrelativoTrasiego("SELECT IDENT_CURRENT ('TRASIEGOSSAL') AS Current_Identity")
            gDetSxT()
        End Try

    End Sub

    

    Sub gDetSxT()
        Dim queryID As String = "INSERT INTO DETSALIDAXT VALUES(@no, @trans, @prod, @cant, @precio, @subt);"
        Dim comand As SqlCommand

        comand = New SqlCommand(queryID, conn)

        Try
            For i = 0 To DataGridView1.Rows.Count - 1
                comand.Parameters.Clear()

                comand.Parameters.AddWithValue("no", i + 1)
                comand.Parameters.AddWithValue("trans", numTrasiego)
                comand.Parameters.AddWithValue("prod", DataGridView1.Rows(i).Cells(0).Value)
                comand.Parameters.AddWithValue("cant", DataGridView1.Rows(i).Cells(1).Value)
                comand.Parameters.AddWithValue("subt", 0)
                comand.Parameters.AddWithValue("precio", 0)


                openConnection()
                comand.ExecuteNonQuery()
                closeConnection()


            Next
            'MessageBox.Show("Transacción realizada correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MsgBox("Error: " & ex.ToString)
        Finally

        End Try
    End Sub

    'Procedimiento para insertar la entrada x trasiego
    Sub insertTrasiegoEnt()
        Dim sql As String = "INSERT INTO TRASIEGOE VALUES(@fecha, @usuario, @sucursal);"
        Dim cmd As SqlCommand

        cmd = New SqlCommand(sql, conn)

        cmd.Parameters.AddWithValue("fecha", CDate(fechaT))
        cmd.Parameters.AddWithValue("usuario", CInt(usuarioActual))
        cmd.Parameters.AddWithValue("sucursal", CInt(sucActual))

        Try
            openConnection()
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show("Hubo un error al guardar el trasiego" & vbCrLf & "Error: " & ex.Message, "Algo salió mal", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            closeConnection()
            numEntT = getCorrelativoTrasiego("SELECT IDENT_CURRENT ('TRASIEGOE') AS Current_Identity")
            gDetExT()
        End Try

    End Sub

    Sub gDetExT()
        Dim queryID As String = "INSERT INTO DETENTRADAXT VALUES(@no, @trans, @prod,  @cant, @precio, @subt);"
        Dim comand As SqlCommand

        comand = New SqlCommand(queryID, conn)

        Try
            For i = 0 To DataGridView2.Rows.Count - 1
                comand.Parameters.Clear()

                comand.Parameters.AddWithValue("no", i + 1)
                comand.Parameters.AddWithValue("trans", numEntT)
                comand.Parameters.AddWithValue("prod", DataGridView2.Rows(i).Cells(0).Value)
                comand.Parameters.AddWithValue("cant", DataGridView2.Rows(i).Cells(1).Value)
                comand.Parameters.AddWithValue("subt", 0)
                comand.Parameters.AddWithValue("precio", 0)


                openConnection()
                comand.ExecuteNonQuery()
                closeConnection()


            Next
            MessageBox.Show("Transacción realizada correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MsgBox("Error: " & ex.ToString)
        
        End Try
    End Sub


    Private Sub frmTrasiegos2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label3.Text = Format(DateTime.Now, "dd/MM/yyyy")

    End Sub

    Private Sub RectangleShape2_Click(sender As Object, e As EventArgs) Handles RectangleShape2.Click

        fechaT = Label3.Text
        If Val(txtItemsST.Text) > Val(txtItemsET.Text) Then
            MsgBox("Faltan datos en los detalles de las entradas", MsgBoxStyle.Critical, "Faltan datos")
        Else
            If Val(txtItemsET.Text) > Val(txtItemsST.Text) Then
                MsgBox("Faltan datos en los detalles de las salidas", MsgBoxStyle.Critical, "Faltan datos")
            Else
                If Val(txtItemsST.Text) = 0 Or Val(txtItemsET.Text) = 0 Then
                    MsgBox("Falta elegir productos para realizar el trasiego", MsgBoxStyle.Critical, "Faltan datos")
                Else
                    insertTrasiegoSal()
                    insertTrasiegoEnt()
                    DataGridView1.Rows.Clear()
                    DataGridView2.Rows.Clear()
                    txtItemsET.Clear()
                    txtItemsST.Clear()
                    txtCantProdET.Clear()
                    txtCantProST.Clear()
                End If
            End If
        End If
    End Sub

    Private Sub frmTrasiegos2_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyData
            Case Keys.F5
                datosreq = 3
                frmProductos.Show()
                enQueDGV = 1

                codDTS = Nothing
                cantDTS = Nothing
                descDTS = Nothing
                presDTS = Nothing
                medDTS = Nothing
            Case Keys.F8
                datosreq = 3
                frmProductos.Show()
                enQueDGV = 2

                codDTS = Nothing
                cantDTS = Nothing
                descDTS = Nothing
                presDTS = Nothing
                medDTS = Nothing
            Case Keys.Escape
                If MessageBox.Show("¿Desea salir de esta ventana? ¡Se perderán los cambios!", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    Me.Close()
                End If
        End Select
    End Sub

    Private Sub txtCantProST_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCantProST.KeyDown

        If e.KeyData = Keys.Enter Then
            If IsNothing(codDTS) Then
                MsgBox("No se ha seleccionado ningún producto", MsgBoxStyle.Critical, "Error")
                txtCantProST.Clear()

            Else
                If Val(txtCantProST.Text) > 0 And Val(txtCantProST.Text) <= exisDTS Then
                    DataGridView1.Rows.Add(codDTS, txtCantProST.Text, descDTS, presDTS, medDTS)
                    txtItemsST.Text = DataGridView1.Rows.Count
                    txtCantProST.Clear()
                Else
                    MsgBox("Cantidad inválida", MsgBoxStyle.Critical, "Error")
                End If
            End If
        End If

    End Sub

    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyData = Keys.Delete Then
            If DataGridView1.Rows.Count > 0 Then
                DataGridView1.Rows.RemoveAt(DataGridView1.CurrentRow.Index)
                txtItemsST.Text = DataGridView1.Rows.Count
            End If
        End If
    End Sub

    Private Sub DataGridView2_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView2.KeyDown
        If e.KeyData = Keys.Delete Then
            If DataGridView2.Rows.Count > 0 Then
                DataGridView2.Rows.RemoveAt(DataGridView2.CurrentRow.Index)
                txtItemsET.Text = DataGridView2.Rows.Count
            End If
        End If
    End Sub

    Private Sub txtCantProdET_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCantProdET.KeyDown

        If e.KeyData = Keys.Enter Then
            If IsNothing(codDTS) Then
                MsgBox("No se ha seleccionado ningún producto", MsgBoxStyle.Critical, "Error")
                txtCantProdET.Clear()

            Else
                If Val(txtCantProdET.Text) > 0 Then
                    DataGridView2.Rows.Add(codDTS, txtCantProdET.Text, descDTS, presDTS, medDTS)
                    txtItemsET.Text = DataGridView2.Rows.Count
                    txtCantProdET.Clear()
                Else
                    MsgBox("Cantidad inválida", MsgBoxStyle.Critical, "Error")
                End If
            End If
        End If

    End Sub


    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        RectangleShape2_Click(RectangleShape2, Nothing)
    End Sub

    
End Class