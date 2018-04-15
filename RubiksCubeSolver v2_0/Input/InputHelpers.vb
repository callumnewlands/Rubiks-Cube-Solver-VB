Imports PublicValues = RubiksCubeSolver_v2_0.MyPublic.PublicConstants

Module InputHelpers




End Module


'Public Sub SetButtonColourOfBlankButton(ByRef box As Button, ByVal sticker As Sticker, ByRef stickerColourChanged(,) As Boolean)
'    Dim colour As Color = GetUserColour()
'    If colour = NO_CHANGE_COLOUR Then
'        stickerColourChanged(sticker.FaceNumber, sticker.StickerNumber) = False
'        Return
'    End If
'    box.BackColor = colour
'End Sub

'Public Sub SetButtonColour(ByRef box As Button)
'    Dim colour As Color = GetUserColour()
'    If colour = NO_CHANGE_COLOUR Then Return
'    box.BackColor = colour
'End Sub

'Private Function GetUserColour() As Color
'    Dim colourPickerObject As New ColourPicker
'    colourPickerObject.ShowDialog()
'    colourPickerObject.Close()
'    Return colourPickerObject.colour
'End Function

'Public Sub ResetStickerColourChanged(ByRef stickerColourChanged(,) As Boolean)
'    For i = 0 To 5
'        For j = 0 To 8
'            If j = 4 Then
'                stickerColourChanged(i, j) = True
'            Else
'                stickerColourChanged(i, j) = False
'            End If
'        Next
'    Next
'End Sub


'stickersArray(0, 4) = "W"
'stickersArray(1, 4) = "G"
'stickersArray(2, 4) = "R"
'stickersArray(3, 4) = "B"
'stickersArray(4, 4) = "Y"
'stickersArray(5, 4) = "O"
'For i = 0 To 5
'    For j = 0 To 8
'        If j <> 4 Then
'            stickersArray(i, j) = vbNullChar
'        End If
'    Next
'Next