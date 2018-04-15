Public Class MainMenu
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Dim inputForm As New Input()
        inputForm.Show()
        Me.Close()
    End Sub

    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        LoadCube()
    End Sub

    Private Sub btnCredits_Click(sender As Object, e As EventArgs) Handles btnCredits.Click
        Dim creditsForm As New Credits()
        creditsForm.ShowDialog()
    End Sub

    Private Sub LoadCube()
        Dim fileBrowser As New OpenFileDialog()

        fileBrowser.Filter = "cube files (*.cube)|*.cube"
        fileBrowser.ShowDialog()
        Try
            Dim filePath As String = fileBrowser.FileName
            If filePath = "" Then Return
            Dim outputForm As New _3DOutput(filePath)
            outputForm.Show()
            Me.Close()
        Catch ex As Exception
            MsgBox("Cannot Read File: " & ex.Message)
        End Try
    End Sub

End Class