Imports RubiksCubeSolver_v2_0.Helpers.PublicFunctions
Imports RubiksCubeSolver_v2_0.Helpers.PublicConstants

Public Module SetCubieProperties

    ''' <summary>
    ''' Returns the colourString for a cornerTriplet, and sets secondaryFaceColour and secondaryFaceRotation
    ''' </summary>
    Public Function GetCornerColours(ByVal cube(,) As Char, ByVal cornerTri As CornerTriplet, ByRef secondaryFaceColour As FaceColour, ByRef secondaryFaceRotation As FaceColour) As String

        Dim cornerColours(2) As Char
        For cornerBlockSticker = 0 To 2
            Dim colourStore As Char
            colourStore = cube(cornerTri.Corners(cornerBlockSticker).FaceNumber,
                               cornerTri.Corners(cornerBlockSticker).StickerNumber)
            If colourStore = "W" Or colourStore = "Y" Then
                cornerColours(0) = colourStore
            ElseIf colourStore = "R" Or colourStore = "O" Then
                secondaryFaceColour = ColourChar2FaceNumber(colourStore)
                secondaryFaceRotation = ColourChar2FaceNumber(cube(cornerTri.Corners(cornerBlockSticker).FaceNumber, 4))
                cornerColours(1) = colourStore
            ElseIf colourStore = "G" Or colourStore = "B" Then
                cornerColours(2) = colourStore
            Else
                MsgBox("ERROR: " + cornerBlockSticker.ToString() + ", " + colourStore.ToString())
            End If
        Next cornerBlockSticker
        Dim cornerString As String
        cornerString = ""
        For k = 0 To 2
            cornerString += cornerColours(k)
        Next k
        Return cornerString
    End Function

    ''' <summary> Returns the colourString for a edgePair </summary>
    Public Function GetEdgeColours(ByVal cube(,) As Char, ByVal edgePair As EdgePair) As String
        Dim edgeString As String = ""
        Dim edgeColours(1) As Char
        Dim colourStore, colourStore2 As Char
        colourStore = cube(edgePair.Edges(0).FaceNumber, edgePair.Edges(0).StickerNumber)
        colourStore2 = cube(edgePair.Edges(1).FaceNumber, edgePair.Edges(1).StickerNumber)

        If (colourStore = "W" Or colourStore = "Y") Then
            edgeColours(0) = colourStore
            edgeColours(1) = colourStore2
        ElseIf (colourStore2 = "W" Or colourStore2 = "Y") Then
            edgeColours(0) = colourStore2
            edgeColours(1) = colourStore
        ElseIf (colourStore = "R" Or colourStore = "O") Then
            edgeColours(0) = colourStore
            edgeColours(1) = colourStore2
        ElseIf (colourStore2 = "R" Or colourStore2 = "O") Then
            edgeColours(0) = colourStore2
            edgeColours(1) = colourStore
        End If

        For k = 0 To 1
            edgeString += edgeColours(k)
        Next k
        Return edgeString
    End Function

    ''' <summary> 
    ''' Returns the position vector for a corner on the top half of the cube.
    ''' From the face and location of its primary face
    ''' </summary>
    Public Function GetTopCornerPosition(ByVal face As Integer, ByVal sticker As Integer) As Vector3x1
        Dim position As New Vector3x1
        position.y = 1
        Select Case face
            Case MoveFaces.TOP
                position.x = (sticker Mod 6) - 1
                position.z = Math.Sign(sticker - 4)
            Case MoveFaces.LEFT
                position.x = -1
                position.z = 1 - Math.Abs(sticker - 2)
            Case MoveFaces.BACK
                position.x = Math.Abs(sticker - 2) - 1
                position.z = -1
            Case MoveFaces.RIGHT
                position.x = 1
                position.z = 1 - sticker
            Case MoveFaces.FRONT
                position.x = sticker - 1
                position.z = 1
        End Select
        Return position
    End Function

    ''' <summary> 
    ''' Returns the position vector for a corner on the bottom half of the cube.
    ''' From the face and location of its primary face
    ''' </summary>
    Public Function GetBottomCornerPosition(ByVal face As Integer, ByVal sticker As Integer) As Vector3x1
        Dim position As New Vector3x1
        position.y = -1
        Select Case face
            Case MoveFaces.TOP
                position.x = Math.Abs((sticker Mod 6) - 2) - 1
                position.z = Math.Sign(sticker - 4)
            Case MoveFaces.LEFT
                position.x = 1
                position.z = 1 - Math.Abs(sticker - 2)
            Case MoveFaces.BACK
                position.x = sticker - 1
                position.z = -1
            Case MoveFaces.RIGHT
                position.x = -1
                position.z = 1 - sticker
            Case MoveFaces.FRONT
                position.x = Math.Abs(sticker - 2) - 1
                position.z = 1
        End Select
        Return position
    End Function

    ''' <summary> 
    ''' Returns the position vector for an edge on the top half of the cube.
    ''' From the face and location of its primary face
    ''' </summary>
    Public Function GetTopEdgePosition(ByVal face As Integer, ByVal sticker As Integer) As Vector3x1
        Dim position As New Vector3x1
        Select Case face
            Case MoveFaces.TOP
                position.x = ((sticker Mod 6) Mod 3) - 1
                position.y = 1
                Select Case sticker
                    Case 1 : position.z = -1
                    Case 3 : position.z = 0
                    Case 5 : position.z = 0
                    Case 7 : position.z = 1
                End Select
            Case MoveFaces.LEFT
                position.x = -1
                position.y = Math.Sign(3 - sticker)
                position.z = sticker Mod 3 - 1
            Case MoveFaces.BACK
                position.x = Math.Sign(sticker - 1)
                position.y = Math.Sign(3 - sticker)
                position.z = -1
            Case MoveFaces.RIGHT
                position.x = 1
                position.y = Math.Sign(3 - sticker)
                position.z = Math.Sign(sticker - 1)
            Case MoveFaces.FRONT
                position.x = sticker Mod 3 - 1
                position.y = Math.Sign(3 - sticker)
                position.z = 1
        End Select
        Return position
    End Function

    ''' <summary> 
    ''' Returns the position vector for an edge on the bottom half of the cube.
    ''' From the face and location of its primary face
    ''' </summary>
    Public Function GetBottomEdgePosition(ByVal face As Integer, ByVal sticker As Integer) As Vector3x1
        Dim position As New Vector3x1
        Select Case face
            Case 0 ' Top
                position.x = -(((sticker Mod 3) Mod 6) - 1)
                position.y = -1
                Select Case sticker
                    Case 1 : position.z = -1
                    Case 3 : position.z = 0
                    Case 5 : position.z = 0
                    Case 7 : position.z = 1
                End Select
            Case 1 ' Left
                position.x = 1
                position.y = -(sticker Mod 3)
                position.z = (sticker Mod 3) - 1
            Case 2 ' Back
                position.x = (sticker Mod 3) - 1
                position.y = -(sticker Mod 3)
                position.z = -1
            Case 3 ' Right
                position.x = -1
                position.y = -(sticker Mod 3)
                position.z = Math.Sign(sticker - 1)
            Case 4 ' Front
                position.x = Math.Sign(sticker - 1)
                position.y = -(sticker Mod 3)
                position.z = 1
        End Select
        Return position
    End Function

    ''' <summary> 
    ''' Orientates the stickerArray to match a given cubeOrientation
    ''' </summary>
    Public Function OrientateStickerArray(ByVal stickerArray(,) As Char, ByVal cubeOrientation As CubeOrientation) As Char(,)
        Dim transformedArray(4, 8) As Char
        ' each face can be in 1 of 4 rotations : 4 methods
        Try
            If cubeOrientation.Front = Opposite(cubeOrientation.Top) Then
                Throw New ArgumentException("The specified top and front face combination is impossible." & vbNewLine &
                                            "Top: " & cubeOrientation.Top & vbNewLine & "Front: " & cubeOrientation.Front)
            End If
        Catch ex As ArgumentException
            MsgBox(ex.Message)
            Return Nothing
        End Try


        Dim faceColours As FaceColour() = GetFaceColoursFromOrientation(cubeOrientation)

        For sideFaceNumber = 0 To 4
            Dim faceColour As FaceColour = faceColours(sideFaceNumber)

            Dim faceStickerStore(9) As Char
            For i = 0 To 8
                faceStickerStore(i) = stickerArray(faceColour, i)
            Next

            transformedArray = MapFaceOntoTransformedArray(sideFaceNumber, faceColour, faceStickerStore,
                                                           cubeOrientation, transformedArray)
        Next
        Return transformedArray
    End Function

    Private Function MapFaceOntoTransformedArray(ByVal sideFaceNumber As Integer, ByVal faceColour As FaceColour, ByVal faceStickerStore() As Char, ByVal cubeOrientation As CubeOrientation, ByVal transformedArray(,) As Char) As Char(,)
        Select Case cubeOrientation.Top
            Case FaceColour.W
                Select Case faceColour
                    Case FaceColour.O : UpsideDown(transformedArray, sideFaceNumber, faceStickerStore)
                    Case cubeOrientation.Top : TopFace(transformedArray, sideFaceNumber, faceStickerStore, cubeOrientation)
                    Case Else : NoRotation(transformedArray, sideFaceNumber, faceStickerStore)
                End Select
            Case FaceColour.Y
                Select Case faceColour
                    Case FaceColour.O : NoRotation(transformedArray, sideFaceNumber, faceStickerStore)
                    Case cubeOrientation.Top : TopFace(transformedArray, sideFaceNumber, faceStickerStore, cubeOrientation)
                    Case Else : UpsideDown(transformedArray, sideFaceNumber, faceStickerStore)
                End Select
            Case FaceColour.G
                Select Case faceColour
                    Case cubeOrientation.Top : TopFace(transformedArray, sideFaceNumber, faceStickerStore, cubeOrientation)
                    Case Else : RotateLeft(transformedArray, sideFaceNumber, faceStickerStore)
                End Select
            Case FaceColour.B
                Select Case faceColour
                    Case cubeOrientation.Top : TopFace(transformedArray, sideFaceNumber, faceStickerStore, cubeOrientation)
                    Case Else : RotateRight(transformedArray, sideFaceNumber, faceStickerStore)
                End Select
            Case FaceColour.R
                Select Case faceColour
                    Case FaceColour.Y : NoRotation(transformedArray, sideFaceNumber, faceStickerStore)
                    Case FaceColour.W : UpsideDown(transformedArray, sideFaceNumber, faceStickerStore)
                    Case FaceColour.B : RotateLeft(transformedArray, sideFaceNumber, faceStickerStore)
                    Case FaceColour.G : RotateRight(transformedArray, sideFaceNumber, faceStickerStore)
                    Case cubeOrientation.Top : TopFace(transformedArray, sideFaceNumber, faceStickerStore, cubeOrientation)
                End Select
            Case FaceColour.O
                Select Case faceColour
                    Case FaceColour.Y : UpsideDown(transformedArray, sideFaceNumber, faceStickerStore)
                    Case FaceColour.W : NoRotation(transformedArray, sideFaceNumber, faceStickerStore)
                    Case FaceColour.B : RotateRight(transformedArray, sideFaceNumber, faceStickerStore)
                    Case FaceColour.G : RotateLeft(transformedArray, sideFaceNumber, faceStickerStore)
                    Case cubeOrientation.Top : TopFace(transformedArray, sideFaceNumber, faceStickerStore, cubeOrientation)
                End Select
        End Select

        Return transformedArray
    End Function

    Private Sub NoRotation(ByRef stickerArray As Char(,), ByVal faceNumber As Integer, ByVal faceStickers As Char())
        For i = 0 To 8
            stickerArray(faceNumber, i) = faceStickers(i)
        Next
    End Sub
    Private Sub UpsideDown(ByRef stickerArray As Char(,), ByVal faceNumber As Integer, ByVal faceStickers As Char())
        For i = 0 To 8
            stickerArray(faceNumber, i) = faceStickers(8 - i)
        Next
    End Sub
    Private Sub RotateLeft(ByRef stickerArray As Char(,), ByVal faceNumber As Integer, ByVal faceStickers As Char())
        Dim count As Integer = 6
        For i = 0 To 8
            stickerArray(faceNumber, i) = faceStickers(count Mod 10)
            count += 7
        Next
    End Sub
    Private Sub RotateRight(ByRef stickerArray As Char(,), ByVal faceNumber As Integer, ByVal faceStickers As Char())
        Dim count As Integer = 2
        For i = 0 To 8
            stickerArray(faceNumber, i) = faceStickers(count Mod 10)
            count += 3
        Next
    End Sub
    Private Sub TopFace(ByRef stickerArray As Char(,), ByVal faceNumber As Integer, ByVal faceStickers As Char(), ByVal currentOrientation As CubeOrientation)
        If currentOrientation.Front = FaceColour.B Then
            RotateLeft(stickerArray, faceNumber, faceStickers)
        ElseIf currentOrientation.Front = FaceColour.G Then
            RotateRight(stickerArray, faceNumber, faceStickers)
        ElseIf currentOrientation.Front = FaceColour.W Then
            If currentOrientation.Top = FaceColour.O Then
                NoRotation(stickerArray, faceNumber, faceStickers)
            Else
                UpsideDown(stickerArray, faceNumber, faceStickers)
            End If
        ElseIf currentOrientation.Front = FaceColour.Y Then
            If currentOrientation.Top = FaceColour.O Then
                UpsideDown(stickerArray, faceNumber, faceStickers)
            Else
                NoRotation(stickerArray, faceNumber, faceStickers)
            End If
        ElseIf currentOrientation.Front = FaceColour.R Then
            Select Case currentOrientation.Top
                Case FaceColour.W : NoRotation(stickerArray, faceNumber, faceStickers)
                Case FaceColour.Y : UpsideDown(stickerArray, faceNumber, faceStickers)
                Case FaceColour.G : RotateLeft(stickerArray, faceNumber, faceStickers)
                Case FaceColour.B : RotateRight(stickerArray, faceNumber, faceStickers)
            End Select
        ElseIf currentOrientation.Front = FaceColour.O Then
            Select Case currentOrientation.Top
                Case FaceColour.W : UpsideDown(stickerArray, faceNumber, faceStickers)
                Case FaceColour.Y : NoRotation(stickerArray, faceNumber, faceStickers)
                Case FaceColour.G : RotateRight(stickerArray, faceNumber, faceStickers)
                Case FaceColour.B : RotateLeft(stickerArray, faceNumber, faceStickers)
            End Select
        End If
    End Sub

End Module
