Namespace Helpers

    Public Module PublicConstants

        Public Const STICKER_LETTERS_NO_MIDDLE As String = "abcdfghi"
        Public Const STICKER_LETTERS As String = "abcdefghi"
        Public Const FACE_COLOURS As String = "bgorwy"
        Public Const MIDDLE_STICKER As Integer = 4

        Public ReadOnly CORNER_NAMES(0 To 7) As String
        Public ReadOnly EDGE_NAMES(0 To 11) As String

        Public Sub FillNameArrays()
            FillCornerNames()
            FillEdgeNames()
        End Sub

        Private Sub FillCornerNames()
            CORNER_NAMES(0) = "WRB"
            CORNER_NAMES(1) = "WOB"
            CORNER_NAMES(2) = "WOG"
            CORNER_NAMES(3) = "WRG"
            CORNER_NAMES(4) = "YRB"
            CORNER_NAMES(5) = "YRG"
            CORNER_NAMES(6) = "YOG"
            CORNER_NAMES(7) = "YOB"
        End Sub

        Private Sub FillEdgeNames()
            EDGE_NAMES(0) = "WR"
            EDGE_NAMES(1) = "WB"
            EDGE_NAMES(2) = "WO"
            EDGE_NAMES(3) = "WG"
            EDGE_NAMES(4) = "RB"
            EDGE_NAMES(5) = "OB"
            EDGE_NAMES(6) = "OG"
            EDGE_NAMES(7) = "RG"
            EDGE_NAMES(8) = "YR"
            EDGE_NAMES(9) = "YG"
            EDGE_NAMES(10) = "YO"
            EDGE_NAMES(11) = "YB"
        End Sub

        Public Enum FaceColour
            W
            G
            R
            B
            Y
            O
            None
        End Enum

        Public Enum Axis
            X
            Y
            Z
        End Enum

        Public Enum MoveFaces
            TOP
            LEFT
            BACK
            RIGHT
            FRONT
            BOTTOM
        End Enum

        Public Enum Direction
            CLOCKWISE = -1
            NO_CHANGE = 0
            ANTICLOCKWISE = 1
            HALF_TURN = 2
        End Enum

        Public Enum Layer
            TOP = 1
            MIDDLE = 0
            BOTTOM = -1
        End Enum

    End Module

    Module PublicFunctions

        ''' <summary> Converts colour char to the name of the colour </summary>
        Public Function ColourCharToWord(ByVal colourchar As Char) As String
            Static colours As New Dictionary(Of Char, String)
            If colours.Count = 0 Then colours = GetColourDictionary()
            Try
                Return colours.Item(colourchar)
            Catch ex As KeyNotFoundException
                Return "Error"
            End Try
        End Function
        Private Function GetColourDictionary() As Dictionary(Of Char, String)
            'uses a dictionary of character keys to colour strings for faster lookup
            Dim colours As New Dictionary(Of Char, String) From {
                {"W", "White"},
                {"R", "Red"},
                {"B", "Blue"},
                {"Y", "Yellow"},
                {"O", "Orange"},
                {"G", "Green"}
            }
            Return colours
        End Function

        ''' <summary> Converts colour char to face number </summary>
        Public Function ColourChar2FaceNumber(ByVal colourchar As Char) As FaceColour
            Return [Enum].Parse(GetType(FaceColour), colourchar, True)
        End Function

        Public Function ColourChar2FaceNumber(ByVal colourChars() As Char) As FaceColour()
            Dim array(colourChars.Length - 1) As FaceColour
            For i = 0 To colourChars.Length - 1
                array(i) = ColourChar2FaceNumber(colourChars(i))
            Next
            Return array
        End Function

        ''' <summary> Converts face number to colour char </summary>
        Public Function FaceNumber2ColourChar(ByVal number As Integer) As Char
            Dim color As FaceColour = number
            Return color.ToString()
        End Function

        Public Sub WriteBlocksToFile(ByVal blocks As Block(), Optional filename As String = "Blocks.txt")
            FileOpen(1, filename, OpenMode.Output)
            For Each block In blocks
                PrintLine(1, block.ToString())
            Next
            FileClose(1)
        End Sub

        ''' <summary> Returns the opposte coloured face to the colour given as an argument </summary>
        Public Function Opposite(ByVal colour As Char) As Char
            Select Case colour
                Case "W"
                    Return "Y"
                Case "R"
                    Return "O"
                Case "B"
                    Return "G"
                Case "Y"
                    Return "W"
                Case "O"
                    Return "R"
                Case "G"
                    Return "B"
                Case Else
                    Return "-"
            End Select
        End Function

        Public Function Opposite(ByVal colour As FaceColour) As FaceColour
            Return ColourChar2FaceNumber(Opposite(FaceNumber2ColourChar(colour)))
        End Function

        ' (0 = top, 1 = left, 2 = back, 3 = right, 4 = front 5 = bottom)
        Public Function GetFaceColoursFromOrientation(ByVal currentOrientation As CubeOrientation) As FaceColour()
            Dim faces(0 To 5) As FaceColour
            faces(0) = currentOrientation.Top
            faces(5) = currentOrientation.Bottom

            Dim sideFaceColoursClockwise() As FaceColour = GetSideFaceColours(currentOrientation.Top)

            Dim positionOfFront As Integer
            positionOfFront = LinearSearch(sideFaceColoursClockwise, currentOrientation.Front)

            For i = 1 To 4
                faces(i) = sideFaceColoursClockwise((i + positionOfFront) Mod 4)
            Next
            Return faces
        End Function

        Private Function GetSideFaceColours(ByVal top As FaceColour) As FaceColour()
            Dim sideFaceColoursClockwise(3) As FaceColour
            Select Case top
                Case FaceColour.W
                    sideFaceColoursClockwise(0) = FaceColour.R
                    sideFaceColoursClockwise(1) = FaceColour.G
                    sideFaceColoursClockwise(2) = FaceColour.O
                    sideFaceColoursClockwise(3) = FaceColour.B
                Case FaceColour.Y
                    sideFaceColoursClockwise(3) = FaceColour.R
                    sideFaceColoursClockwise(2) = FaceColour.G
                    sideFaceColoursClockwise(1) = FaceColour.O
                    sideFaceColoursClockwise(0) = FaceColour.B
                Case FaceColour.R
                    sideFaceColoursClockwise(0) = FaceColour.B
                    sideFaceColoursClockwise(1) = FaceColour.Y
                    sideFaceColoursClockwise(2) = FaceColour.G
                    sideFaceColoursClockwise(3) = FaceColour.W
                Case FaceColour.O
                    sideFaceColoursClockwise(3) = FaceColour.B
                    sideFaceColoursClockwise(2) = FaceColour.Y
                    sideFaceColoursClockwise(1) = FaceColour.G
                    sideFaceColoursClockwise(0) = FaceColour.W
                Case FaceColour.B
                    sideFaceColoursClockwise(0) = FaceColour.Y
                    sideFaceColoursClockwise(1) = FaceColour.R
                    sideFaceColoursClockwise(2) = FaceColour.W
                    sideFaceColoursClockwise(3) = FaceColour.O
                Case FaceColour.G
                    sideFaceColoursClockwise(3) = FaceColour.Y
                    sideFaceColoursClockwise(2) = FaceColour.R
                    sideFaceColoursClockwise(1) = FaceColour.W
                    sideFaceColoursClockwise(0) = FaceColour.O
            End Select
            Return sideFaceColoursClockwise
        End Function

    End Module
End Namespace

'Public Function ConvertEdges(ByVal Cube(,) As Char, ByVal edges() As Edge, ByVal currentOrientation As CubeOrientation, ByRef stickerArray(,) As Char) As Edge()

'    Dim sticker As Char
'    Dim faceColour As Char

'    Dim topBool As Boolean
'    'set cube to swrrttop
'    If Cube(0, 4) = currentOrientation.Top Then
'        topBool = True
'    Else
'        topBool = False
'    End If

'    'Converts top then bottom
'    'For i = 0 To 1
'    ' for each the top and side four faces
'    For face = 0 To 4
'        ' for each corner sticker
'        For edgeSticker = 1 To 7 Step 2
'            ' searches the top half of the cube only
'            If face = 0 Or (face > 0 And edgeSticker < 4) Then
'                ' gets the sticker colour
'                sticker = Cube(face, edgeSticker)

'                Dim edgePair As New EdgePair

'                edgePair.Edges(0).FaceNumber = face : edgePair.Edges(0).StickerNumber = edgeSticker
'                edgePair = AdjacentEdgesWR(edgePair.Edges(0))
'                Dim secondSticker As Char = Cube(edgePair.Edges(1).FaceNumber, edgePair.Edges(1).StickerNumber)
'                If (sticker = "W" Or sticker = "Y") Or ((sticker = "R" Or sticker = "O") And (secondSticker <> "W" And secondSticker <> "Y")) Then

'                    'sets the primary sticker of edgePair
'                    edgePair.Edges(0).FaceNumber = face : edgePair.Edges(0).StickerNumber = edgeSticker
'                    'gets the colour of the cube face that the primary sticker is on
'                    faceColour = Cube(edgePair.Edges(0).FaceNumber, 4)
'                    edgePair = AdjacentEdgesWR(edgePair.Edges(0))

'                    Dim edgeString As String = ""
'                    Dim edgeColours(1) As Char
'                    Dim colourStore, colourStore2 As Char
'                    colourStore = Cube(edgePair.Edges(0).FaceNumber, edgePair.Edges(0).StickerNumber)
'                    colourStore2 = Cube(edgePair.Edges(1).FaceNumber, edgePair.Edges(1).StickerNumber)

'                    If (colourStore = "W" Or colourStore = "Y") Then
'                        edgeColours(0) = colourStore
'                        edgeColours(1) = colourStore2
'                    ElseIf (colourStore2 = "W" Or colourStore2 = "Y") Then
'                        edgeColours(0) = colourStore2
'                        edgeColours(1) = colourStore
'                    ElseIf (colourStore = "R" Or colourStore = "O") Then
'                        edgeColours(0) = colourStore
'                        edgeColours(1) = colourStore2
'                    ElseIf (colourStore2 = "R" Or colourStore2 = "O") Then
'                        edgeColours(0) = colourStore2
'                        edgeColours(1) = colourStore
'                    End If

'                    For k = 0 To 1
'                        edgeString += edgeColours(k)
'                    Next k

'                    For l = 0 To UBound(EDGE_NAMES)
'                        If EDGE_NAMES(l) = edgeString Then
'                            'edges(l).PrimaryFace = sticker
'                            If topBool Then
'                                Select Case face
'                                    Case 0 ' Top
'                                        edges(l).Position.y = 1
'                                        Select Case edgeSticker
'                                            Case 1
'                                                edges(l).Position.x = 0
'                                                edges(l).Position.z = -1
'                                            Case 3
'                                                edges(l).Position.x = -1
'                                                edges(l).Position.z = 0
'                                            Case 5
'                                                edges(l).Position.x = 1
'                                                edges(l).Position.z = 0
'                                            Case 7
'                                                edges(l).Position.x = 0
'                                                edges(l).Position.z = 1
'                                        End Select
'                                    Case 1 ' Left
'                                        edges(l).Position.x = -1
'                                        If edgeSticker = 1 Then
'                                            edges(l).Position.y = 1
'                                            edges(l).Position.z = 0
'                                        ElseIf edgeSticker = 3 Then
'                                            edges(l).Position.y = 0
'                                            edges(l).Position.z = -1
'                                        End If
'                                    Case 2 ' Back
'                                        edges(l).Position.z = -1
'                                        If edgeSticker = 1 Then
'                                            edges(l).Position.x = 0
'                                            edges(l).Position.y = 1
'                                        ElseIf edgeSticker = 3 Then
'                                            edges(l).Position.x = 1
'                                            edges(l).Position.y = 0
'                                        End If
'                                    Case 3 ' Right
'                                        edges(l).Position.x = 1
'                                        If edgeSticker = 1 Then
'                                            edges(l).Position.y = 1
'                                            edges(l).Position.z = 0
'                                        ElseIf edgeSticker = 3 Then
'                                            edges(l).Position.y = 0
'                                            edges(l).Position.z = 1
'                                        End If
'                                    Case 4 ' Front
'                                        edges(l).Position.z = 1
'                                        If edgeSticker = 1 Then
'                                            edges(l).Position.x = 0
'                                            edges(l).Position.y = 1
'                                        ElseIf edgeSticker = 3 Then
'                                            edges(l).Position.x = -1
'                                            edges(l).Position.y = 0
'                                        End If
'                                End Select
'                            Else

'                                'lower half
'                                Select Case face
'                                    Case 0 ' Top
'                                        edges(l).Position.y = -1
'                                        Select Case edgeSticker
'                                            Case 1
'                                                edges(l).Position.x = 0
'                                                edges(l).Position.z = -1
'                                            Case 3
'                                                edges(l).Position.x = 1
'                                                edges(l).Position.z = 0
'                                            Case 5
'                                                edges(l).Position.x = -1
'                                                edges(l).Position.z = 0
'                                            Case 7
'                                                edges(l).Position.x = 0
'                                                edges(l).Position.z = 1
'                                        End Select
'                                    Case 1 ' Left
'                                        edges(l).Position.x = 1
'                                        If edgeSticker = 1 Then
'                                            edges(l).Position.y = -1
'                                            edges(l).Position.z = 0
'                                        ElseIf edgeSticker = 3 Then
'                                            edges(l).Position.y = 0
'                                            edges(l).Position.z = -1
'                                        End If
'                                    Case 2 ' Back
'                                        edges(l).Position.z = -1
'                                        If edgeSticker = 1 Then
'                                            edges(l).Position.x = 0
'                                            edges(l).Position.y = -1
'                                        ElseIf edgeSticker = 3 Then
'                                            edges(l).Position.x = -1
'                                            edges(l).Position.y = 0
'                                        End If
'                                    Case 3 ' Right
'                                        edges(l).Position.x = -1
'                                        If edgeSticker = 1 Then
'                                            edges(l).Position.y = -1
'                                            edges(l).Position.z = 0
'                                        ElseIf edgeSticker = 3 Then
'                                            edges(l).Position.y = 0
'                                            edges(l).Position.z = 1
'                                        End If
'                                    Case 4 ' Front
'                                        edges(l).Position.z = 1
'                                        If edgeSticker = 1 Then
'                                            edges(l).Position.x = 0
'                                            edges(l).Position.y = -1
'                                        ElseIf edgeSticker = 3 Then
'                                            edges(l).Position.x = 1
'                                            edges(l).Position.y = 0
'                                        End If
'                                End Select
'                            End If

'                            edges(l).Rotation = faceColour
'                            For m = 0 To 1
'                                edges(l).Colours(m) = edgeColours(m)
'                                'Console.WriteLine("L: {0}, COl{2}: {1}", l, corners(l).Colours(m), m)
'                            Next m

'                            Exit For
'                        ElseIf l = UBound(EDGE_NAMES) And EDGE_NAMES(l) <> edgeString Then
'                            MsgBox("EdgeString not recognised: " & edgeString)
'                        End If
'                    Next l
'                End If
'            End If
'        Next edgeSticker
'    Next face
'    WriteEdgesToFile(edges)
'    'GiveRespect(stickerArray, Opposite(top), front)
'    'Next

'    Return edges
'End Function

'Private Function ConvertCorners(ByVal cube(,) As Char, ByVal corners() As Corner, ByVal top As Char, ByVal front As Char, ByRef stickerArray(,) As Char) As Corner()

'    For i = 0 To 1
'        For face = 0 To 4
'            For cornerSticker = 0 To 8 Step 2

'                If cornerSticker = MIDDLE_STICKER Then Continue For

'                ' searches the top half of the cube only
'                If face <> 0 And cornerSticker > MIDDLE_STICKER Then Continue For

'                Dim sticker As Char
'                sticker = StickersWRTTop(face, cornerSticker)
'                If sticker <> "W" And sticker <> "Y" Then Continue For

'                Dim cornerTri As New CornerTriplet
'                cornerTri.Corners(0).FaceNumber = face : cornerTri.Corners(0).StickerNumber = cornerSticker

'                'gets the colour of the cube face that the primary sticker is on
'                Dim faceColour As Char
'                faceColour = StickersWRTTop(cornerTri.Corners(0).FaceNumber, 4)

'                ' Returns a CornerTriplet Object
'                cornerTri = AdjacentCornersWR(cornerTri.Corners(0))
'                Dim cornerColours(2) As Char
'                Dim secondaryFaceColour, secondaryFaceRotation As Char

'                For cornerBlockSticker = 0 To 2
'                    Dim colourStore As Char
'                    colourStore = StickersWRTTop(cornerTri.Corners(cornerBlockSticker).FaceNumber, cornerTri.Corners(cornerBlockSticker).StickerNumber)
'                    If colourStore = "W" Or colourStore = "Y" Then
'                        cornerColours(0) = colourStore
'                    ElseIf colourStore = "R" Or colourStore = "O" Then
'                        secondaryFaceColour = colourStore
'                        secondaryFaceRotation = StickersWRTTop(cornerTri.Corners(cornerBlockSticker).FaceNumber, 4)
'                        cornerColours(1) = colourStore
'                    ElseIf colourStore = "G" Or colourStore = "B" Then
'                        cornerColours(2) = colourStore
'                    Else
'                        MsgBox("ERROR: " + cornerBlockSticker.ToString() + ", " + colourStore.ToString())
'                    End If
'                Next cornerBlockSticker


'                Dim cornerString As String
'                cornerString = ""
'                For k = 0 To 2
'                    cornerString += cornerColours(k)
'                Next k

'                For cornerNumber = 0 To UBound(CORNER_NAMES)
'                    If CORNER_NAMES(cornerNumber) = cornerString Then
'                        corners(cornerNumber).SecondaryFace = secondaryFaceColour
'                        Dim topBool As Boolean
'                        If i = 0 Then
'                            topBool = True
'                        Else
'                            topBool = False
'                        End If
'                        SetCornerPosition(face, cornerSticker, cornerNumber, corners, topBool)
'                        corners(cornerNumber).Rotation = faceColour
'                        corners(cornerNumber).SecondaryRotation = secondaryFaceRotation
'                        WriteCornersToFile(corners)
'                        For m = 0 To 2
'                            corners(cornerNumber).Colours(m) = cornerColours(m)
'                        Next m
'                        Exit For
'                    End If
'                Next cornerNumber
'            Next cornerSticker
'        Next face
'        WriteCornersToFile(corners)
'        GiveRespect(stickerArray, Opposite(top), front)
'    Next
'    Return corners
'End Function

': pass cube(,) around; Todo split for edges and corners - wont be called twice
''Public Function CastFromCharArray(ByVal array() As Char) As FaceColours()
''    Dim cast(array.Length - 1) As FaceColours
''    For i = 0 To array.Length - 1
''        cast(i) = ColourChar2FaceNumber(array(i))
''    Next
''    Return cast
''End Function


''Returns associated face colours to face passed as an argument (0 = self, 1-4 = sides, 5 = opposite)
''Public Function AssociatedFaces(face As Char) As Char()
''    Dim faces(0 To 5) As Char
''    Dim x As Integer = 1
''    faces(0) = face
''    faces(5) = Opposite(face)
''    For Each j In FACE_COLOURS.ToUpper().ToCharArray()
''        If j <> faces(0) And j <> faces(5) Then
''            faces(x) = j
''            x += 1
''        End If
''    Next
''    Return faces
''End Function
