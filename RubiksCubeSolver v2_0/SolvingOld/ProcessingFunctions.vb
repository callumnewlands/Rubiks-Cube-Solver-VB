Imports RubiksCubeSolver_v2_0.MyPublic

Module ProcessingFunctions





End Module



'Public Function RemoveBlockFromEdgeArray(ByVal cubie As Block, ByRef array() As Edge) As Edge()
'    Dim newArray(0 To array.Length - 2) As Edge
'    Dim index As Integer
'    For i = 0 To array.Length - 1
'        Try
'            If array(i).Name <> cubie.Name Then
'                newArray(index) = array(i)
'                index += 1
'            End If
'        Catch ex As NullReferenceException

'        End Try
'    Next
'    Return newArray
'End Function

'Public Function RemoveBlockFromCornerArray(ByVal cubie As Block, ByRef array() As Corner) As Corner()
'    Dim newArray(0 To array.Length - 2) As Corner
'    Dim index As Integer
'    For i = 0 To array.Length - 1
'        Try
'            If array(i).Name <> cubie.Name Then
'                newArray(index) = array(i)
'                index += 1
'            End If
'        Catch ex As NullReferenceException

'        End Try
'    Next
'    Return newArray
'End Function

'Private Function RemoveBlanksFromArray(ByVal array() As String) As String()

'    Dim newArray(0 To array.Length - 1) As String
'    Dim index As Integer = 0

'    For i = 0 To array.Length - 1
'        If array(i) <> "" And array(i) <> Nothing Then
'            newArray(index) = array(i)
'            index += 1
'        End If
'    Next

'    ReDim Preserve newArray(0 To index - 1)

'    Return newArray
'End Function

'Public Function ExtractEdges(ByVal blocks() As Block) As Edge()
'    Dim edges(blocks.Length) As Edge
'    Dim index As Integer = 0
'    For Each block In blocks
'        If block.GetType = GetType(Edge) Then
'            edges(index) = block
'            index += 1
'        End If
'    Next
'    ReDim Preserve edges(index - 1)
'    Return edges
'End Function
'Public Function ExtractCorners(ByVal blocks() As Block) As Corner()
'    Dim corners(blocks.Length) As Corner
'    Dim index As Integer = 0
'    For Each block In blocks
'        If block.GetType = GetType(Corner) Then
'            corners(index) = block
'            index += 1
'        End If
'    Next
'    ReDim Preserve corners(index - 1)
'    Return corners
'End Function


'''' <summary>
'''' Checks if a cubie is rotated correctly onto the given face. 
'''' Given that it is already on the face.
'''' I.e. that the blue side of a blue cubie is on the blue face of the cube
'''' </summary>
'Public Function CheckCubieRotation(ByVal cubie As Block, ByVal faceColour As FaceColours)

'    If cubie.GetType() = GetType(Edge) Then
'        'Console.WriteLine(cubie.Name & " " & cubie.Position(0) & cubie.Position(1) & cubie.Position(2) & " is edge")
'        Select Case faceColour
'            Case FaceColours.W, FaceColours.Y
'                If cubie.Rotation <> faceColour.ToString Then
'                    Return False
'                End If
'            Case FaceColours.R, FaceColours.O

'                If faceColour.ToString() = cubie.PrimaryFace Then
'                    If cubie.Rotation <> faceColour.ToString Then
'                        Return False
'                    End If
'                Else
'                    If cubie.Rotation = faceColour.ToString Then
'                        Return False
'                    End If
'                End If
'            Case FaceColours.B, FaceColours.G
'                If cubie.Rotation = faceColour.ToString Then
'                    Return False
'                End If
'        End Select
'    ElseIf cubie.GetType() = GetType(Corner) Then
'        Dim tempCorner As Corner = cubie
'        Select Case faceColour
'            Case FaceColours.W, FaceColours.Y
'                If tempCorner.Rotation <> faceColour.ToString Then
'                    Return False
'                End If
'            Case FaceColours.R, FaceColours.O
'                If tempCorner.Rotation = faceColour.ToString Or tempCorner.SecondaryRotation <> faceColour.ToString Then
'                    Return False
'                End If
'            Case FaceColours.B, FaceColours.G
'                If tempCorner.Rotation = faceColour.ToString Or tempCorner.SecondaryRotation = faceColour.ToString Then
'                    Return False
'                End If
'        End Select
'    End If
'    Return True
'End Function


'''' <summary>
'''' Analyses whether a particular cubie is on a particular face of the cube
'''' </summary>
'''' <param name="cubie">The cubie to be checked</param>
'Public Function OnFace(ByVal cubie As Block, ByVal face As FaceColours, ByVal currentOrientation As CubeOrientation) As Boolean

'    Dim faces As Char()
'    faces = PublicFuncs.GetFaceColoursFromOrientation(currentOrientation)

'    ' 0 = Top Face
'    ' 5 = bottom face
'    ' 1 = left face
'    ' 2 = back face
'    ' 3 = right face
'    ' 4 = front face
'    If face.ToString = faces(0) And cubie.Position.y = 1 Or
'                face.ToString = faces(5) And cubie.Position.y = -1 Or
'                face.ToString = faces(4) And cubie.Position.z = 1 Or
'                face.ToString = faces(2) And cubie.Position.z = -1 Or
'                face.ToString = faces(1) And cubie.Position.x = -1 Or
'                face.ToString = faces(3) And cubie.Position.x = 1 Then
'        Return True
'    Else
'        Return False
'    End If
'End Function

'''' <returns> 
'''' startIndex = left edge 
'''' </returns>
'Public Function TopEdgesClockwise(ByVal cube As Cube, Optional startIndex As Integer = 0) As Edge()
'    Dim topBlocks(0 To 7) As Block
'    topBlocks = BlocksByColour(cube.CurrentOrientation.Top, cube)
'    Dim topEdges(0 To startIndex + 3) As Edge
'    Dim index As Integer
'    For i = 0 To 7
'        If topBlocks(i).GetType() = GetType(Edge) Then
'            Select Case topBlocks(i).Position.x
'                Case -1 ' left edge cubie
'                    index = 0
'                Case 0
'                    If topBlocks(i).Position.z = -1 Then ' back edge cubie
'                        index = 1
'                    Else ' front edge cubie
'                        index = 3
'                    End If
'                Case 1 ' right edge cubie
'                    index = 2
'            End Select
'            topEdges(startIndex + index) = topBlocks(i)
'        End If
'    Next
'    Return topEdges
'End Function

'Public Function TopCornersClockwise(ByVal cube As Cube) As Corner()
'    Dim topBlocks(0 To 7) As Block
'    topBlocks = BlocksByColour(cube.CurrentOrientation.Top, cube)
'    Dim topCorners(0 To 3) As Corner

'    Dim index As Integer
'    For Each cubie In topBlocks
'        If cubie.GetType() = GetType(Corner) Then
'            Select Case (cubie.Position.x + cubie.Position.z)
'                Case -2 'Back left corner
'                    index = 1
'                Case 0
'                    If cubie.Position.x = -1 Then ' Front left corner
'                        index = 0
'                    ElseIf cubie.Position.x = 1 Then ' Back right corner
'                        index = 2
'                    End If
'                Case 2 ' Front right corner
'                    index = 3
'            End Select
'            topCorners(index) = cubie
'        End If
'    Next
'    Return topCorners
'End Function


'Public Function BlocksByColour(ByVal colour As Char, ByVal corners As Corner(), ByVal edges As Edge()) As Block()

'    Dim cubies(0 To 7) As Block
'    Dim count As Integer = 0
'    'Console.WriteLine(vbNewLine & colour)
'    For Each corner In corners
'        If CheckCubieColours(corner, colour) Then
'            cubies(count) = corner
'            'Console.WriteLine(corner.Name & " " & corner.Position(0) & corner.Position(1) & corner.Position(2))
'            count += 1
'        End If
'    Next
'    For Each edge In edges
'        If CheckCubieColours(edge, colour) Then
'            cubies(count) = edge
'            'Console.WriteLine(edge.Name & " " & edge.Position(0) & edge.Position(1) & edge.Position(2))
'            count += 1
'        End If
'    Next
'    Return cubies
'End Function

'Private Function GetCorrectTopEdges(ByVal edges() As Edge, ByVal cube As Cube) As Edge()
'    Dim rightCubies(edges.Length - 1) As Edge
'    Dim rightIndex As Integer = 0
'    For Each cubie In edges
'        If TopEdgeCubieCorrect(cubie, cube) Then
'            rightCubies(rightIndex) = cubie
'            rightIndex += 1
'        End If
'    Next
'    Return rightCubies
'End Function

'Private Function GetIncorrectTopEdges(ByVal edges() As Edge, ByVal cube As Cube) As Edge()
'    Dim wrongCubies(3) As Edge
'    Dim wrongIndex As Integer = 0
'    For Each cubie In edges
'        If Not TopEdgeCubieCorrect(cubie, cube) Then
'            wrongCubies(wrongIndex) = cubie
'            wrongIndex += 1
'        End If
'    Next
'    ReDim Preserve wrongCubies(0 To wrongIndex - 1)
'    Return wrongCubies
'End Function


'''' <returns> 
'''' index 0 = front left corner
'''' </returns>
'Public Function TopBlocksClockwise(ByVal cube As Cube) As Block()
'    Dim topBlocks() As Block
'    Dim topBlocksCopy(0 To 7) As Block
'    topBlocks = BlocksByColour(cube.CurrentOrientation.Top, cube)
'    Array.Copy(topBlocks, topBlocksCopy, 8)


'    Dim index As Integer = 0
'    For i = 0 To 7
'        Dim cubie As Block = topBlocksCopy(i)
'        If cubie.GetType() = GetType(Edge) Then
'            Select Case cubie.Position.x
'                Case -1 ' left edge cubie
'                    index = 1
'                Case 0
'                    If cubie.Position.z = -1 Then ' back edge cubie
'                        index = 3
'                    Else ' front edge cubie
'                        index = 7
'                    End If
'                Case 1 ' right edge cubie
'                    index = 5
'            End Select
'        ElseIf cubie.GetType() = GetType(Corner) Then
'            Select Case (cubie.Position.x + cubie.Position.z)
'                Case -2 'Back left corner
'                    index = 2
'                Case 0
'                    If cubie.Position.x = -1 Then ' Front left corner
'                        index = 0
'                    ElseIf cubie.Position.x = 1 Then ' Back right corner
'                        index = 4
'                    End If
'                Case 2 ' Front right corner
'                    index = 6
'            End Select
'        End If
'        topBlocks(index) = cubie
'    Next

'    Return topBlocks
'End Function


' NEW CODE TRANSPOSED FROM OLD CODE BUT DOESNT DO WHAT I THOUGHT IT DID
' IT ONLY WORKS IF THE TOP FACE CONTAINS ALL THE CORRECT COLOUR CUBES
'Public Function GetTopBlocksClockwise(cube As Cube) As Block()
'    Dim faceBlocks As Block() = cube.BlocksByColour(cube.TopFace)
'    Dim topBlocks(7) As Block

'    For Each block In faceBlocks
'        Dim index As Integer
'        If block.GetType() = GetType(Edge) Then
'            index = TopEdgePositionToClockwiseIndex(block.Position)
'        ElseIf block.GetType() = GetType(Corner) Then
'            index = TopCornerPositionToClockwiseIndex(block.Position)
'        End If
'        topBlocks(index) = block
'    Next

'    Return topBlocks
'End Function