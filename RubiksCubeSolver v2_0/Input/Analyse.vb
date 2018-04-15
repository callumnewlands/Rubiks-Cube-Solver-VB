Imports RubiksCubeSolver_v2_0.Helpers.PublicFunctions
Imports RubiksCubeSolver_v2_0.Helpers.PublicConstants

Public Module Analyse

    ''' <summary> Returns true if the cube is fully possible </summary>
    Public Function CubePossible(ByVal stickers(,) As Char) As Boolean
        If Not NineOfEachColourSticker(stickers) Then Return False
        If Not EdgesPossible(stickers) Then Return False
        If Not CornersPossible(stickers) Then Return False
        Return CornerRotationsPossible(stickers)
    End Function

    ''' <summary> Returns true if there are 9 of each colour sticker </summary>
    Private Function NineOfEachColourSticker(ByVal stickers(,) As Char) As Boolean
        If AnythingElseInArray(NumberOfEachColour(stickers), 9) Then
            MsgBox("You have not entered the correct number of each colour. Are you sure you entered all the squares correctly?")
            Return False
        End If
        Return True
    End Function

    ''' <summary> 
    ''' Returns the number of each letter in a 2d char. array(,) as an array(0 To 5) i.e. ("bgorwy") b:0, g:1 etc... 
    ''' </summary>
    Public Function NumberOfEachColour(ByVal chararray(,) As Char) As Integer()
        If chararray.Length < 1 Then Return Nothing

        Dim number(0 To 5) As Integer
        number.Zero()
        For i = 0 To UBound(chararray, 1)
            For j = 0 To UBound(chararray, 2)
                number(InStr(FACE_COLOURS.ToUpper(), chararray(i, j)) - 1) += 1
            Next
        Next
        Return number
    End Function

    ''' <summary> Returns true if all the edges are possible </summary>
    Private Function EdgesPossible(ByVal stickers(,) As Char) As Boolean
        For faceNumber = 0 To 5
            For edgeNumber = 1 To 7 Step 2
                Dim stickerColour As Char = stickers(faceNumber, edgeNumber)
                Dim oppColour As Char = Opposite(stickerColour).ToString()
                Dim adjacentSticker As Sticker = AdjacentEdge(faceNumber, edgeNumber)
                Dim adjacentStickerColour As Char = stickers(adjacentSticker.FaceNumber, adjacentSticker.StickerNumber)
                If adjacentStickerColour = stickerColour Or adjacentStickerColour = oppColour Then
                    DisplayEdgeErrorMessage(stickers, {faceNumber, adjacentSticker.FaceNumber})
                    Return False
                End If
            Next edgeNumber
        Next faceNumber
        Return True
    End Function

    Private Sub DisplayEdgeErrorMessage(ByVal stickers(,) As Char, ByVal wrongFaceColours() As Integer)
        Dim wrongFace1, wrongFace2 As String
        wrongFace1 = ColourCharToWord(stickers(wrongFaceColours(0), 4)).ToLower()
        wrongFace2 = ColourCharToWord(stickers(wrongFaceColours(1), 4)).ToLower()
        MsgBox("The middle cube on the " & wrongFace1 & "/" & wrongFace2 & " edge can't be that. Are you sure you entered it correctly?")
    End Sub

    ''' <summary> Returns true if all the corners are possible </summary>
    Private Function CornersPossible(ByVal stickers(,) As Char) As Boolean
        For faceNumber = 0 To 5
            For cornerNumber = 0 To 8 Step 2
                If cornerNumber = MIDDLE_STICKER Then Continue For
                Dim otherTwoStickers() As Sticker = AdjacentCorners(faceNumber, cornerNumber)
                '3 stickers per corner
                Dim sticker0Colour As Char = stickers(faceNumber, cornerNumber)
                Dim sticker1Colour As Char = stickers(otherTwoStickers(0).FaceNumber, otherTwoStickers(0).StickerNumber)
                Dim sticker2Colour As Char = stickers(otherTwoStickers(1).FaceNumber, otherTwoStickers(1).StickerNumber)
                If Not CornerPossible({sticker0Colour, sticker1Colour, sticker2Colour}) Then
                    DisplayCornerErrorMessage(stickers,
                                              {faceNumber, otherTwoStickers(0).FaceNumber, otherTwoStickers(1).FaceNumber})
                    Return False
                End If
            Next cornerNumber
        Next faceNumber
        Return True
    End Function

    Private Function CornerPossible(ByVal cornerColours() As Char) As Boolean
        Dim oppColour As Char = Opposite(cornerColours(0)).ToString()
        'if any pair of adjacent corner stickers are the same or opposite colours, return false
        Return Not (cornerColours(1) = cornerColours(0) Or
            cornerColours(2) = cornerColours(0) Or
            cornerColours(1) = oppColour Or
            cornerColours(2) = oppColour Or
            cornerColours(1) = cornerColours(2))
    End Function

    Private Sub DisplayCornerErrorMessage(ByVal stickers(,) As Char, ByVal wrongFaces() As Integer)
        Dim wrongFace1, wrongFace2, wrongFace3 As String
        wrongFace1 = ColourCharToWord(stickers(wrongFaces(0), 4)).ToLower()
        wrongFace2 = ColourCharToWord(stickers(wrongFaces(1), 4)).ToLower()
        wrongFace3 = ColourCharToWord(stickers(wrongFaces(2), 4)).ToLower()
        MsgBox("The cube on the " & wrongFace1 & "/" & wrongFace2 & "/" & wrongFace3 & " corner can't be that. Are you sure you entered it correctly?")
    End Sub


    ''' <summary> Returns true if all the corners have their stickers in the right order </summary>
    Private Function CornerRotationsPossible(ByVal stickers(,) As Char) As Boolean
        For faceNumber = 0 To 5
            For cornerNumber = 0 To 8 Step 2
                If cornerNumber = MIDDLE_STICKER Then Continue For

                Dim cornerStickers() As Sticker = AdjacentCorners(faceNumber, cornerNumber)
                Dim firstStickerColour As Char = stickers(faceNumber, cornerNumber)
                Dim secondStickerColour As Char = stickers(cornerStickers(0).FaceNumber, cornerStickers(0).StickerNumber)
                Dim thirdStickerColour As Char = stickers(cornerStickers(1).FaceNumber, cornerStickers(1).StickerNumber)

                If firstStickerColour <> "W" And firstStickerColour <> "Y" Then Continue For

                If thirdStickerColour <> getCorrectThirdCornerStickerColour(firstStickerColour, secondStickerColour) Then
                    DisplayCornerErrorMessage(stickers,
                                               {faceNumber, cornerStickers(0).FaceNumber, cornerStickers(1).FaceNumber})
                    Return False
                End If
            Next cornerNumber
        Next faceNumber
        Return True
    End Function

    Private Function getCorrectThirdCornerStickerColour(ByVal firstStickerColour As Char, ByVal secondStickerColour As Char) As Char
        Dim correctThirdStickerColour As Char
        If firstStickerColour = "W" Then
            Select Case secondStickerColour
                Case "R" : correctThirdStickerColour = "G"
                Case "O" : correctThirdStickerColour = "B"
                Case "G" : correctThirdStickerColour = "O"
                Case "B" : correctThirdStickerColour = "R"
            End Select
        ElseIf firstStickerColour = "Y" Then
            Select Case secondStickerColour
                Case "R" : correctThirdStickerColour = "B"
                Case "O" : correctThirdStickerColour = "G"
                Case "G" : correctThirdStickerColour = "R"
                Case "B" : correctThirdStickerColour = "O"
            End Select
        End If
        Return correctThirdStickerColour
    End Function

    '''<summary> Returns the 2nd sticker that makes up an edge piece </summary>
    Public Function AdjacentEdge(ByVal a As Integer, ByVal b As Integer) As Sticker
        'Adjacent sides = (0,1)(5,7) (0,3)(1,1) (0,5)(3,1) (0,7)(2,1)
        '(a,b)(x,y)       (1,1)(0,3) (1,3)(5,3) (1,5)(2,3) (1,7)(4,3)
        '                 (2,1)(0,7) (2,3)(1,5) (2,5)(3,3) (2,7)(4,1)
        '                 (3,1)(0,5) (3,3)(2,5) (3,5)(5,5) (3,7)(4,5)
        '                 (4,1)(2,7) (4,3)(1,7) (4,5)(3,7) (4,7)(5,1)
        '                 (5,1)(4,7) (5,3)(1,3) (5,5)(3,5) (5,7)(0,1)
        Dim x, y As Integer
        Select Case a
            Case 0
                Select Case b
                    Case 1 : x = 5 : y = 7
                    Case 3 : x = 1 : y = 1
                    Case 5 : x = 3 : y = 1
                    Case 7 : x = 2 : y = 1
                End Select
            Case 1
                Select Case b
                    Case 1 : x = 0 : y = 3
                    Case 3 : x = 5 : y = 3
                    Case 5 : x = 2 : y = 3
                    Case 7 : x = 4 : y = 3
                End Select
            Case 2
                Select Case b
                    Case 1 : x = 0 : y = 7
                    Case 3 : x = 1 : y = 5
                    Case 5 : x = 3 : y = 3
                    Case 7 : x = 4 : y = 1
                End Select
            Case 3
                Select Case b
                    Case 1 : x = 0 : y = 5
                    Case 3 : x = 2 : y = 5
                    Case 5 : x = 5 : y = 5
                    Case 7 : x = 4 : y = 5
                End Select
            Case 4
                Select Case b
                    Case 1 : x = 2 : y = 7
                    Case 3 : x = 1 : y = 7
                    Case 5 : x = 3 : y = 7
                    Case 7 : x = 5 : y = 1
                End Select
            Case 5
                Select Case b
                    Case 1 : x = 4 : y = 7
                    Case 3 : x = 1 : y = 3
                    Case 5 : x = 3 : y = 5
                    Case 7 : x = 0 : y = 1
                End Select
        End Select
        Return New Sticker(x, y)
    End Function


    '''<summary> Returns the 2nd and 3rd stickers that makes up a corner piece </summary>
    Public Function AdjacentCorners(ByVal a As Integer, ByVal b As Integer) As Sticker()
        'Adjacent Coners = (0,0)(1,0)(5,6) (0,2)(5,8)(3,2) (0,6)(2,0)(1,2) (0,8)(3,0)(2,2)
        '(a,b)(w,x)(y,z)   (1,0)(5,6)(0,0) (1,2)(0,6)(2,0) (1,6)(4,6)(5,0) (1,8)(2,6)(4,0)
        '                  (2,0)(1,2)(0,6) (2,2)(0,8)(3,0) (2,6)(4,0)(1,8) (2,8)(3,6)(4,2)
        '                  (3,0)(2,2)(0,8) (3,2)(0,2)(5,8) (3,6)(4,2)(2,8) (3,8)(5,2)(4,8)
        '                  (4,0)(1,8)(2,6) (4,2)(2,8)(3,6) (4,6)(5,0)(1,6) (4,8)(3,8)(5,2)
        '                  (5,0)(1,6)(4,6) (5,2)(4,8)(3,8) (5,6)(0,0)(1,0) (5,8)(3,2)(0,2)

        Dim w, x, y, z As Integer
        Select Case a
            Case 0
                Select Case b
                    Case 0 : w = 1 : x = 0 : y = 5 : z = 6
                    Case 2 : w = 5 : x = 8 : y = 3 : z = 2
                    Case 6 : w = 2 : x = 0 : y = 1 : z = 2
                    Case 8 : w = 3 : x = 0 : y = 2 : z = 2
                End Select
            Case 1
                Select Case b
                    Case 0 : w = 5 : x = 6 : y = 0 : z = 0
                    Case 2 : w = 0 : x = 6 : y = 2 : z = 0
                    Case 6 : w = 4 : x = 6 : y = 5 : z = 0
                    Case 8 : w = 2 : x = 6 : y = 4 : z = 0
                End Select
            Case 2
                Select Case b
                    Case 0 : w = 1 : x = 2 : y = 0 : z = 6
                    Case 2 : w = 0 : x = 8 : y = 3 : z = 0
                    Case 6 : w = 4 : x = 0 : y = 1 : z = 8
                    Case 8 : w = 3 : x = 6 : y = 4 : z = 2
                End Select
            Case 3
                Select Case b
                    Case 0 : w = 2 : x = 2 : y = 0 : z = 8
                    Case 2 : w = 0 : x = 2 : y = 5 : z = 8
                    Case 6 : w = 4 : x = 2 : y = 2 : z = 8
                    Case 8 : w = 5 : x = 2 : y = 4 : z = 8
                End Select
            Case 4
                Select Case b
                    Case 0 : w = 1 : x = 8 : y = 2 : z = 6
                    Case 2 : w = 2 : x = 8 : y = 3 : z = 6
                    Case 6 : w = 5 : x = 0 : y = 1 : z = 6
                    Case 8 : w = 3 : x = 8 : y = 5 : z = 2
                End Select
            Case 5
                Select Case b
                    Case 0 : w = 1 : x = 6 : y = 4 : z = 6
                    Case 2 : w = 4 : x = 8 : y = 3 : z = 8
                    Case 6 : w = 0 : x = 0 : y = 1 : z = 0
                    Case 8 : w = 3 : x = 2 : y = 0 : z = 2
                End Select
        End Select

        Return {New Sticker(w, x), New Sticker(y, z)}
    End Function
End Module
