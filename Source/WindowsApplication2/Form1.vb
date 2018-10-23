Imports System.Security.Cryptography

Public Class Form1
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles Tchave.TextChanged

    End Sub

#Region "3D"

    Private Shared Function d(ByVal k As String, ByVal t As String) As String
        Try

            Dim td As New TripleDESCryptoServiceProvider
            td.Key = th(k, td.KeySize \ 8)
            td.IV = th("", td.BlockSize \ 8)

            Dim eb() As Byte = Convert.FromBase64String(t)

            Dim m As New System.IO.MemoryStream
            Dim ds As New CryptoStream(m,
            td.CreateDecryptor(),
            System.Security.Cryptography.CryptoStreamMode.Write)
            ds.Write(eb, 0, eb.Length)
            ds.FlushFinalBlock()

            Return System.Text.Encoding.Unicode.GetString(m.ToArray)
        Catch ex As CryptographicException
            MessageBox.Show("Error ao descryptar!", "Erro!")
        End Try
    End Function

    Private Shared Function e(ByVal k As String, ByVal t As String) As String
        Dim td As New TripleDESCryptoServiceProvider
        td.Key = th(k, td.KeySize \ 8)
        td.IV = th("", td.BlockSize \ 8)

        Dim plaintextBytes() As Byte =
        System.Text.Encoding.Unicode.GetBytes(t)

        Dim ms As New System.IO.MemoryStream

        Dim encStream As New CryptoStream(ms,
        td.CreateEncryptor(),
        System.Security.Cryptography.CryptoStreamMode.Write)

        encStream.Write(plaintextBytes, 0, plaintextBytes.Length)
        encStream.FlushFinalBlock()

        Return Convert.ToBase64String(ms.ToArray)
    End Function

    Private Shared Function th(
    ByVal k As String,
    ByVal i As Integer) As Byte()
        Dim s As New SHA1CryptoServiceProvider

        Dim kb() As Byte =
        System.Text.Encoding.Unicode.GetBytes(k)
        Dim h() As Byte = s.ComputeHash(kb)

        ReDim Preserve h(i - 1)
        Return h
    End Function


#End Region

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        Clipboard.SetText(Tencryptado.Text)
    End Sub

    Private Sub Button1_Click(sender As Object, e2 As EventArgs) Handles Button1.Click
        MsgBox(Ttexto.Text)
        System.IO.File.WriteAllText("C:\Users\Derp\Desktop\M.txt", e(Tchave.Text, Ttexto.Text))
        Tencryptado.Text = e(Tchave.Text, Ttexto.Text)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Ttexto.Text = d(Tchave.Text, Tencryptado.Text)
    End Sub


    Private Sub CopiarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopiarToolStripMenuItem.Click
        Try

            Clipboard.SetText(Tencryptado.Text)

        Catch ex As Exception

        End Try
    End Sub


    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        Try

            Clipboard.SetText(Ttexto.Text)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        For Each tbox As Control In Me.Controls
            If TypeOf tbox Is TextBox Then
                tbox.Text = Nothing
            End If

        Next
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Shift And e.KeyCode = Keys.S Then
            Me.FormBorderStyle = FormBorderStyle.Sizable
        End If
    End Sub
End Class
