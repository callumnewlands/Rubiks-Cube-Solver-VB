Imports System.IO
Imports RubiksCubeSolver_v2_0.Helpers

Public Class Input

    ReadOnly buttonColours As Color() = {Color.White, Color.Lime, Color.Red, Color.Blue, Color.Yellow, Color.DarkOrange}

    ''' <summary> Handles click events from any of the colour selection boxes </summary>
    Private Sub ColourChange_Click(sender As Object, e As EventArgs) Handles a1.Click, b1.Click, c1.Click, d1.Click,
        f1.Click, g1.Click, h1.Click, i1.Click, a2.Click, b2.Click, c2.Click, d2.Click, f2.Click, g2.Click, h2.Click,
        i2.Click, a3.Click, b3.Click, c3.Click, d3.Click, f3.Click, g3.Click, h3.Click, i3.Click, a4.Click, b4.Click,
        c4.Click, d4.Click, f4.Click, g4.Click, h4.Click, i4.Click, a5.Click, b5.Click, c5.Click, d5.Click, f5.Click,
        g5.Click, h5.Click, i5.Click, a6.Click, b6.Click, c6.Click, d6.Click, f6.Click, g6.Click, h6.Click, i6.Click

        DirectCast(sender, Button).BackColor = GetNextButtonColour(sender)

    End Sub

    ''' <summary>  Returns the next colour in the sequence for a specific button  </summary>
    Private Function GetNextButtonColour(ByVal button As Button) As Color
        Return buttonColours((Array.IndexOf(buttonColours, button.BackColor) + 1) Mod 6)
    End Function

    Private Sub btnSolve_Click(sender As Object, e As EventArgs) Handles btnSolve.Click
        btnReset.Enabled = False
        Dim stickerColours As Char(,) = ConvertButtonColoursToCharArray(Controls)
        If Not Analyse.CubePossible(stickerColours) Then
            btnReset.Enabled = True
            Return
        End If

        Dim cube As New Cube(stickerColours)
        Dim processingWindow As New Processing(cube)
        processingWindow.Show()
        Me.Close()
    End Sub

    ''' <summary> Reads each of the sticker buttons and returns an array of their colours </summary>
    Private Function ConvertButtonColoursToCharArray(ByVal controls As Control.ControlCollection) As Char(,)
        Dim stickersArray(5, 8) As Char
        For face = 0 To 5
            For Each sticker As Char In STICKER_LETTERS.ToCharArray()
                Dim buttonName As String = sticker & (face + 1).ToString() 'e.g. "a2"
                Dim button As Button = controls.Find(buttonName, True)(0)
                Dim stickerNumber As Integer = InStr(STICKER_LETTERS, sticker) - 1
                'InStr() used to get (position of sticker in "abcdefghi" - 1) == y-coordinate of sticker
                stickersArray(face, stickerNumber) = GetButtonColour(button, controls)
            Next
        Next
        Return stickersArray
    End Function

    '''<summary> Gets the colour character of a specific button </summary>
    Private Function GetButtonColour(ByVal box As Button, ByVal controls As Control.ControlCollection) As Char
        Dim buttonColour As Char = Mid(box.BackColor.ToString(), 8, 1)
        'e.g. if box.BackColor.ToString = "Colour [White]", Mid(8, 1) = "W"
        If buttonColour = "D" Then
            buttonColour = "O" 'converts DarkOrange to "O"(for Orange)
        ElseIf buttonColour = "L" Then
            buttonColour = "G" 'converts Lime to "G"(for Green)
        End If
        Return buttonColour
    End Function

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        ResetBoxColours(Controls)
        ResetStickersArray(_stickers)
    End Sub

    Private Sub btnMain_Click(sender As Object, e As EventArgs) Handles btnMain.Click
        Dim main As New MainMenu
        main.Show()
        Me.Close()
    End Sub

    ''' <summary> Resets all the colours of the buttons to their default colours </summary>
    Private Sub ResetBoxColours(ByVal controls As Control.ControlCollection)
        For Each faceChar As Char In STICKER_LETTERS_NO_MIDDLE.ToCharArray()
            For faceNumber = 1 To 6
                Dim face As Helpers.FaceColour = faceNumber - 1
                Dim buttonName As String = faceChar & faceNumber.ToString() 'e.g. "b5"
                Dim button As Button = controls.Find(buttonName, True)(0)
                button.BackColor = buttonColours(face)
            Next
        Next
    End Sub

    Private Sub ResetStickersArray(ByRef stickersArray(,) As Char)
        For i = 0 To 5
            Dim face As Helpers.FaceColour = i
            For j = 0 To 8
                stickersArray(i, j) = face.ToString()
            Next
        Next
    End Sub

    '-----------------------------NOT INCLUDED IN FINAL VERSION------------------------------------------------------

    Private _stickers(0 To 5, 0 To 8) As Char
    Private Sub Input_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ResetStickersArray(_stickers)
    End Sub

    Private Sub btnTest_Click(sender As Object, e As EventArgs) Handles btnTest.Click
        Dim fileName As String = InputBox("Input name of file (without path/extention)")
        If fileName = Nothing Then Exit Sub

        ReadTestCube("testCubes\" & fileName & ".txt", _stickers)
        btnReset.Enabled = False
        If Not CubePossible(_stickers) Then
            MsgBox("Cube Not Possible")
            Return
        End If
        Dim cube As New Cube(_stickers)
        Dim processingWindow As New Processing(cube)
        processingWindow.Show()
        Me.Close()
    End Sub
    Private Sub ReadTestCube(ByVal filePath As String, ByRef stickersArray(,) As Char)
        Dim fReader As StreamReader
        Try
            fReader = New StreamReader(filePath)
        Catch ex As FileNotFoundException
            Dim fileName As String = InputBox("Input name of file (without path/extention)")
            If fileName = Nothing Then
                Exit Sub
            End If
            ReadTestCube("testCubes\" & fileName & ".txt", stickersArray)
            Return
        End Try
        Dim faceString As String
        For face = 0 To 5
            faceString = fReader.ReadLine
            For sticker = 0 To 8
                stickersArray(face, sticker) = faceString(sticker)
            Next
        Next
        fReader.Close()
    End Sub
    Private Sub btnRndTest_Click(sender As Object, e As EventArgs) Handles btnRndTest.Click
        Randomize()
        ReadTestCube("testCubes\completed.txt", _stickers)
        Dim cube As New Cube(_stickers)

        For i = 0 To CInt(Int(Rnd() * 49)) + 10
            cube.RotateFace(CInt(Int(Rnd() * 6)), CInt(Int(Rnd() * 4)) - 1)
        Next
        cube.Instructions.Clear()
        Dim processingWindow As New Processing(cube)
        processingWindow.Show()
        Me.Close()
    End Sub
    '-------------------------------------UP TO HERE------------------------------------------------

End Class

