Imports RubiksCubeSolver_v2_0.MyPublic.PublicConstants
Imports PublicFuncs = RubiksCubeSolver_v2_0.MyPublic.PublicFunctions
Imports FaceColours = RubiksCubeSolver_v2_0.MyPublic.PublicConstants.FaceColour



Module TopLayerSteps

    ''End Sub



    'METHOD FOR TOP LAYER
    'Private Sub DoCrossPermutation(ByRef cube As Cube, ByRef Instructions As String)

    '    Dim faces() As FaceColours = cube.FaceColours
    '    Dim faceColours() As Char = cube.FaceChars
    '    Dim topEdges() As Edge = TopEdgesClockwise(cube)
    '    Dim colourIndex As Integer
    '    If topEdges(0).PrimaryFace = cube.CurrentOrientation.Top Then
    '        colourIndex = 1
    '    Else
    '        colourIndex = 0
    '    End If

    '    Dim colourOfFrontCubie As Char = topEdges(0).Colours(colourIndex)
    '    Dim index As Integer
    '    For i = 1 To 4
    '        If faceColours(i) = colourOfFrontCubie Then
    '            index = i
    '            Exit For
    '        End If
    '    Next


    '    Dim clockwiseIndex As Integer = (index Mod 4) + 1
    '    Dim clockwiseEdge As Edge = topEdges(1)
    '    If Not ClockwiseEdgeCorrectColour(clockwiseEdge.Colours(colourIndex), faceColours(clockwiseIndex)) Then
    '        RotateFace(faces(1), Direction.HALF_TURN, cube, Instructions)
    '        RotateFace(faces(3), Direction.HALF_TURN, cube, Instructions)
    '        RotateFace(faces(0), Direction.HALF_TURN, cube, Instructions)
    '        RotateFace(faces(1), Direction.HALF_TURN, cube, Instructions)
    '        RotateFace(faces(3), Direction.HALF_TURN, cube, Instructions)
    '    ElseIf topEdges(0).Colours(colourIndex) = PublicFuncs.Opposite(topEdges(1).Colours(colourIndex)) Or
    '        topEdges(1).Colours(colourIndex) = PublicFuncs.Opposite(topEdges(2).Colours(colourIndex)) Or
    '        topEdges(2).Colours(colourIndex) = PublicFuncs.Opposite(topEdges(3).Colours(colourIndex)) Or
    '        topEdges(3).Colours(colourIndex) = PublicFuncs.Opposite(topEdges(0).Colours(colourIndex)) Then
    '        'else use same method as bottom cross to permute edges
    '        PermuteTopCrossEdges(cube, Instructions)
    '    End If
    'End Sub

    'Private Function ClockwiseEdgeCorrectColour(actualClockwiseColour As Char, correctClockwiseColour As Char) As Boolean
    '    Return actualClockwiseColour = correctClockwiseColour
    'End Function


    'Public Sub OrientateTopFace(ByRef cube As Cube, ByRef Instructions As String)
    '    Dim topFace As FaceColours
    '    topFace = PublicFuncs.ColourChar2FaceNumber(cube.CurrentOrientation.Top)
    '    Dim topBlocks() As Block = BlocksByColour(cube.CurrentOrientation.Top, cube)
    '    Dim topLeftEdge As New Edge
    '    For Each cubie In topBlocks
    '        If cubie.GetType() = GetType(Edge) And cubie.Position.x = -1 Then
    '            topLeftEdge = cubie
    '            Exit For
    '        End If
    '    Next
    '    Dim direction As Direction
    '    Dim faceColours() As Char = cube.FaceChars
    '    'Dim instructionString As String = ""
    '    For i = 1 To 4
    '        If CheckCubieColours(topLeftEdge, faceColours(i)) Then
    '            Select Case i
    '                Case 1 : direction = Direction.NO_CHANGE ': instructionString = ""
    '                Case 2 : direction = Direction.CLOCKWISE ': instructionString = "U "
    '                Case 3 : direction = Direction.HALF_TURN ': instructionString = "U2 "
    '                Case 4 : direction = Direction.ANTICLOCKWISE ': instructionString = "U' "
    '            End Select
    '        End If
    '    Next

    '    RotateFace(topFace, direction, cube, Instructions)

    '    'goto func5

    'End Sub

End Module



'Private Sub PutNextCornerOnTopFace(ByRef rightCubies() As Corner, ByRef wrongCubies() As Corner, ByRef cube As Cube, ByRef Instructions As String)
'    Dim cubie As Corner
'    cubie = wrongCubies(0)

'    Dim faceColours() As Char = cube.FaceChars
'    Dim faces() As FaceColours = cube.FaceColours

'    If cubie.Position.y = -1 Then
'        Dim colourIndex As Integer
'        For i = 0 To 2
'            If cubie.Colours(i) = cube.CurrentOrientation.Top Then
'                colourIndex = i
'                Exit For
'            End If
'        Next

'        Dim faceCubieRotatedTowards As FaceColours

'        Dim anticlockwiseFace, clockwiseFace As FaceColours

'        Select Case (cubie.Position.x + cubie.Position.z)
'            Case -2 'Back left corner
'                anticlockwiseFace = faces(1)
'                clockwiseFace = faces(2)
'            Case 0
'                If cubie.Position.x = -1 Then ' Front left corner
'                    anticlockwiseFace = faces(4)
'                    clockwiseFace = faces(1)
'                ElseIf cubie.Position.x = 1 Then ' Back right corner
'                    anticlockwiseFace = faces(2)
'                    clockwiseFace = faces(3)
'                End If
'            Case 2 ' Front right corner
'                anticlockwiseFace = faces(3)
'                clockwiseFace = faces(4)
'        End Select

'        Select Case colourIndex
'            Case 0
'                faceCubieRotatedTowards = PublicFuncs.ColourChar2FaceNumber(cubie.Rotation)
'            Case 1
'                faceCubieRotatedTowards = PublicFuncs.ColourChar2FaceNumber(cubie.SecondaryRotation)
'            Case 2
'                Dim facesAroundCorner() As FaceColours = {clockwiseFace, anticlockwiseFace, faces(5)}

'                For Each face In facesAroundCorner
'                    If face.ToString() <> cubie.Rotation And face.ToString() <> cubie.SecondaryRotation Then
'                        faceCubieRotatedTowards = face
'                        Exit For
'                    End If
'                Next
'        End Select


'        If faceCubieRotatedTowards = clockwiseFace Then
'            'if left put put underneath where it should be (bottom left):

'            RotateFaceToFront(anticlockwiseFace, cube, Instructions)
'            faceColours = cube.FaceChars
'            For i = 0 To 5
'                faces(i) = PublicFuncs.ColourChar2FaceNumber(faceColours(i))
'            Next
'            RotateTopFaceForCorner(cubie, faces, cube, Instructions)
'            RotateFace(faces(1), Direction.CLOCKWISE, cube, Instructions)
'            RotateFace(faces(5), Direction.CLOCKWISE, cube, Instructions)
'            RotateFace(faces(1), Direction.ANTICLOCKWISE, cube, Instructions)
'        ElseIf faceCubieRotatedTowards = anticlockwiseFace Then
'            'if right put underneath where it should be (bottom right):

'            RotateFaceToFront(clockwiseFace, cube, Instructions)
'            faceColours = cube.FaceChars
'            For i = 0 To 5
'                faces(i) = PublicFuncs.ColourChar2FaceNumber(faceColours(i))
'            Next
'            RotateTopFaceForCorner(cubie, faces, cube, Instructions)
'            RotateFace(faces(3), Direction.ANTICLOCKWISE, cube, Instructions)
'            RotateFace(faces(5), Direction.ANTICLOCKWISE, cube, Instructions)
'            RotateFace(faces(3), Direction.CLOCKWISE, cube, Instructions)
'        Else
'            'if bottom put underneath corner (bottom right):

'            RotateFaceToFront(clockwiseFace, cube, Instructions)
'            faceColours = cube.FaceChars
'            For i = 0 To 5
'                faces(i) = PublicFuncs.ColourChar2FaceNumber(faceColours(i))
'            Next
'            RotateTopFaceForCorner(cubie, faces, cube, Instructions)
'            RotateFace(faces(3), Direction.ANTICLOCKWISE, cube, Instructions)
'            RotateFace(faces(5), Direction.CLOCKWISE, cube, Instructions)
'            RotateFace(faces(3), Direction.CLOCKWISE, cube, Instructions)
'            RotateFace(faces(4), Direction.CLOCKWISE, cube, Instructions)
'            RotateFace(faces(5), Direction.HALF_TURN, cube, Instructions)
'            RotateFace(faces(4), Direction.ANTICLOCKWISE, cube, Instructions)

'        End If

'        'remove from wrong corners
'        'add to rightcorners

'        wrongCubies = RemoveBlockFromCornerArray(cubie, wrongCubies)
'        For i = 0 To 3
'            If rightCubies(i) Is Nothing Then
'                rightCubies(i) = cubie
'                Exit For
'            End If
'        Next

'    ElseIf cubie.Position.y = 1 Then

'        Dim newFront As FaceColours
'        Select Case (cubie.Position.x + cubie.Position.z)
'            Case -2 'Back left corner
'                newFront = faces(2)
'            Case 0
'                If cubie.Position.x = -1 Then ' Front left corner
'                    newFront = faces(1)
'                ElseIf cubie.Position.x = 1 Then ' Back right corner
'                    newFront = faces(3)
'                End If
'            Case 2 ' Front right corner
'                newFront = faces(4)
'        End Select

'        RotateFaceToFront(newFront, cube, Instructions)
'        faceColours = cube.FaceChars
'        For i = 0 To 5
'            faces(i) = PublicFuncs.ColourChar2FaceNumber(faceColours(i))
'        Next
'        RotateFace(faces(3), Direction.ANTICLOCKWISE, cube, Instructions)
'        RotateFace(faces(5), Direction.ANTICLOCKWISE, cube, Instructions)
'        RotateFace(faces(3), Direction.CLOCKWISE, cube, Instructions)

'        PutNextCornerOnTopFace(rightCubies, wrongCubies, cube, Instructions)

'    End If

'End Sub







'Private Sub RotateTopFaceForCorner(ByVal corner As Corner, ByVal faces() As FaceColours, ByRef cube As Cube, ByRef Instructions As String)
'    'rotates top face so that the correct corner of the top face is above the corner passed as an argument

'    Dim topEdges() As Edge = TopEdgesClockwise(cube)
'    Dim cornerColours(1) As Char
'    Dim colourIndex As Integer
'    If topEdges(0).Colours(0) = cube.CurrentOrientation.Top Then
'        colourIndex = 1
'    Else
'        colourIndex = 0
'    End If

'    Dim cornerLocation As Integer

'    For i = 0 To 3
'        cornerColours(0) = topEdges(i).Colours(colourIndex)
'        cornerColours(1) = topEdges((i + 1) Mod 4).Colours(colourIndex)

'        Dim colourIndexOne, colourIndexTwo As Integer

'        If corner.Colours(0) = cube.CurrentOrientation.Top Then
'            colourIndexOne = 1
'            colourIndexTwo = 2
'        ElseIf corner.Colours(1) = cube.CurrentOrientation.Top Then
'            colourIndexOne = 0
'            colourIndexTwo = 2
'        ElseIf corner.Colours(2) = cube.CurrentOrientation.Top Then
'            colourIndexOne = 0
'            colourIndexTwo = 1
'        End If

'        'if the corner cubie colour matches the colours of the edge pair
'        If corner.Colours(colourIndexOne) = cornerColours(0) And corner.Colours(colourIndexTwo) = cornerColours(1) Or
'            corner.Colours(colourIndexOne) = cornerColours(1) And corner.Colours(colourIndexTwo) = cornerColours(0) Then
'            cornerLocation = i
'            Exit For
'        End If

'    Next


'    Dim cubiePosition As Integer

'    Select Case (corner.Position.x + corner.Position.z)
'        Case -2 'Back left corner
'            cubiePosition = 0
'        Case 0
'            If corner.Position.x = -1 Then ' Front left corner
'                cubiePosition = 3
'            ElseIf corner.Position.x = 1 Then ' Back right corner
'                cubiePosition = 1
'            End If
'        Case 2 ' Front right corner
'            cubiePosition = 2
'    End Select

'    Dim rotationDifference As Integer = cornerLocation - cubiePosition

'    If Math.Abs(rotationDifference) <> 3 And Math.Abs(rotationDifference) <> 2 Then
'        RotateFace(faces(0), rotationDifference, cube, Instructions)
'    ElseIf Math.Abs(rotationDifference) = 2 Then
'        RotateFace(faces(0), Direction.HALF_TURN, cube, Instructions)
'    ElseIf rotationDifference = -3 Then
'        RotateFace(faces(0), Direction.ANTICLOCKWISE, cube, Instructions)
'    ElseIf rotationDifference = 3 Then
'        RotateFace(faces(0), Direction.CLOCKWISE, cube, Instructions)
'    End If

'End Sub









''---
''xxx
''---
'Private Sub MakeTopLine(ByRef rightCubies() As Edge, ByRef rightIndex As Integer, ByRef wrongCubies() As Edge, ByRef cube As Cube, ByRef Instructions As String)

'    Dim faces() As FaceColours = cube.FaceColours

'    Dim topCubie As Edge = rightCubies(0)
'    Dim colourIndex As Integer
'    If topCubie.PrimaryFace = cube.CurrentOrientation.Top Then
'        colourIndex = 1
'    Else
'        colourIndex = 0
'    End If

'    Dim colourOfTopCubie As Char = topCubie.Colours(colourIndex)
'    Dim oppositeEdgeCubie As Edge = GetOppositeTopEdge(wrongCubies, rightCubies, colourIndex, colourOfTopCubie)
'    Dim topFace As FaceColours = PublicFuncs.ColourChar2FaceNumber(cube.CurrentOrientation.Top)
'    Dim oneMove As Boolean = True
'    Dim direction As Direction = Direction.NO_CHANGE
'    Dim cubieCorrectlyOnTopFace As Boolean = False
'    Dim faceToRotate, secondFaceToRotate As FaceColours

'    If oppositeEdgeCubie.Position.y = 0 Then

'        faceToRotate = GetFaceToRotateForMakingTopCross(oppositeEdgeCubie, cube)
'        direction = GetDirectionToPutEdgeFromMiddleToTop(oppositeEdgeCubie, faceToRotate, faces)

'    ElseIf oppositeEdgeCubie.Position.y = -1 Then
'        If TopEdgeUpsideDownOnBottomLayer(oppositeEdgeCubie, cube.CurrentOrientation) Then

'            faceToRotate = GetFaceToRotateForMakingTopCross(oppositeEdgeCubie, cube)
'            direction = Direction.HALF_TURN

'        Else
'            oneMove = False
'            Select Case oppositeEdgeCubie.Position.x
'                Case -1 ' left edge cubie
'                    faceToRotate = faces(1)
'                    secondFaceToRotate = faces(2)
'                Case 0
'                    If oppositeEdgeCubie.Position.z = -1 Then ' back edge cubie
'                        faceToRotate = faces(2)
'                        secondFaceToRotate = faces(3)
'                    Else ' front edge cubie
'                        faceToRotate = faces(4)
'                        secondFaceToRotate = faces(1)
'                    End If
'                Case 1 ' right edge cubie
'                    faceToRotate = faces(3)
'                    secondFaceToRotate = faces(4)
'            End Select
'            direction = Direction.CLOCKWISE
'        End If
'    ElseIf oppositeEdgeCubie.Position.y = 1 Then

'        ReDim Preserve wrongCubies(0 To wrongCubies.Length)
'        wrongCubies(wrongCubies.Length - 1) = oppositeEdgeCubie
'        RemoveBlockFromEdgeArray(oppositeEdgeCubie, rightCubies)

'        faceToRotate = GetFaceToRotateForMakingTopCross(oppositeEdgeCubie, cube)

'        If CheckCubieRotation(oppositeEdgeCubie, topFace) Then
'            'rotate top 

'            Dim topDirection As Direction
'            Select Case faceToRotate
'                Case faces(1) ' left
'                    Select Case topCubie.Position.x
'                        Case 0
'                            topDirection = topCubie.Position.z
'                        Case 1 ' right edge cubie
'                            topDirection = Direction.NO_CHANGE
'                    End Select
'                Case faces(2) ' back
'                    Select Case topCubie.Position.z
'                        Case 0
'                            topDirection = -topCubie.Position.x
'                        Case 1 ' front edge cubie
'                            topDirection = Direction.NO_CHANGE
'                    End Select
'                Case faces(3) ' right
'                    Select Case topCubie.Position.x
'                        Case -1 ' left edge cubie
'                            topDirection = Direction.NO_CHANGE
'                        Case 0
'                            topDirection = -topCubie.Position.z
'                    End Select
'                Case faces(4) ' front
'                    Select Case topCubie.Position.z
'                        Case -1 ' back edge cubie
'                            topDirection = Direction.NO_CHANGE
'                        Case 0
'                            topDirection = topCubie.Position.x
'                    End Select
'            End Select

'            RotateFace(faceToRotate, Direction.CLOCKWISE, cube, Instructions)
'            RotateFace(topFace, topDirection, cube, Instructions)
'            RotateFace(faceToRotate, Direction.ANTICLOCKWISE, cube, Instructions)
'            cubieCorrectlyOnTopFace = True
'        Else
'            RotateFace(faceToRotate, Direction.CLOCKWISE, cube, Instructions)

'            For Each wrongCubie In wrongCubies
'                If wrongCubie.Colours(colourIndex) = PublicFuncs.Opposite(colourOfTopCubie) Then
'                    oppositeEdgeCubie = wrongCubie
'                    Exit For
'                End If
'            Next
'            If oppositeEdgeCubie Is Nothing Then
'                For Each rightCubie In rightCubies
'                    If rightCubie.Colours(colourIndex) = PublicFuncs.Opposite(colourOfTopCubie) Then
'                        oppositeEdgeCubie = rightCubie
'                        Exit For
'                    End If
'                Next
'            End If

'            faceToRotate = GetFaceToRotateForMakingTopCross(oppositeEdgeCubie, cube)
'            Select Case faceToRotate
'                Case faces(1) ' left
'                    direction = oppositeEdgeCubie.Position.z
'                Case faces(2) ' back
'                    direction = -oppositeEdgeCubie.Position.x
'                Case faces(3) ' right
'                    direction = -oppositeEdgeCubie.Position.z
'                Case faces(4) ' front
'                    direction = oppositeEdgeCubie.Position.x
'            End Select

'        End If
'    End If
'    If Not cubieCorrectlyOnTopFace Then
'        Dim topDirection As Direction
'        Select Case faceToRotate
'            Case faces(1) ' left
'                Select Case topCubie.Position.x
'                    Case -1 ' left edge cubie
'                        topDirection = Direction.HALF_TURN
'                    Case 0
'                        topDirection = topCubie.Position.z
'                    Case 1 ' right edge cubie
'                        topDirection = Direction.NO_CHANGE
'                End Select
'            Case faces(2) ' back
'                Select Case topCubie.Position.z
'                    Case -1 ' back edge cubie
'                        topDirection = Direction.HALF_TURN
'                    Case 0
'                        topDirection = topCubie.Position.x
'                    Case 1 ' front edge cubie
'                        topDirection = Direction.NO_CHANGE
'                End Select
'            Case faces(3) ' right
'                Select Case topCubie.Position.x
'                    Case -1 ' left edge cubie
'                        topDirection = Direction.NO_CHANGE
'                    Case 0
'                        topDirection = -topCubie.Position.z
'                    Case 1 ' right edge cubie
'                        topDirection = Direction.HALF_TURN
'                End Select
'            Case faces(4) ' front
'                Select Case topCubie.Position.z
'                    Case -1 ' back edge cubie
'                        topDirection = Direction.NO_CHANGE
'                    Case 0
'                        topDirection = -topCubie.Position.x
'                    Case 1 ' front edge cubie
'                        topDirection = Direction.HALF_TURN
'                End Select
'        End Select

'        RotateFace(topFace, topDirection, cube, Instructions)
'        RotateFace(faceToRotate, direction, cube, Instructions)
'        If Not oneMove Then
'            RotateFace(topFace, Direction.CLOCKWISE, cube, Instructions)
'            RotateFace(secondFaceToRotate, Direction.ANTICLOCKWISE, cube, Instructions)
'        End If
'    End If
'    wrongCubies = RemoveBlockFromEdgeArray(oppositeEdgeCubie, wrongCubies)
'    rightCubies(rightIndex) = oppositeEdgeCubie
'    rightIndex += 1
'End Sub

'Private Function GetOppositeTopEdge(ByVal wrongCubies() As Edge, ByVal rightCubies() As Edge, ByVal colourIndex As Integer, ByVal colourOfTopCubie As Char)
'    For Each wrongCubie In wrongCubies
'        If wrongCubie.Colours(colourIndex) = PublicFuncs.Opposite(colourOfTopCubie) Then
'            Return wrongCubie
'        End If
'    Next
'    For Each rightCubie In rightCubies
'        If rightCubie.Colours(colourIndex) = PublicFuncs.Opposite(colourOfTopCubie) Then
'            Return rightCubie
'        End If
'    Next
'    Return Nothing
'End Function

''---
''xxx
''-x-
'Private Sub MakeTopT(ByRef rightCubies() As Edge, ByRef rightIndex As Integer, ByRef wrongCubies() As Edge, ByRef numberOnTopFace As Integer, ByVal topEdgesWhenSolved() As Edge, ByRef cube As Cube, ByRef Instructions As String)
'    Dim topFace As FaceColours = PublicFuncs.ColourChar2FaceNumber(cube.CurrentOrientation.Top)

'    Dim colourIndex As Integer
'    If rightCubies(0).PrimaryFace = cube.CurrentOrientation.Top Then
'        colourIndex = 1
'    Else
'        colourIndex = 0
'    End If

'    If (rightCubies(0).Position.x = rightCubies(1).Position.x Or rightCubies(0).Position.z = rightCubies(1).Position.z) And
'        rightCubies(0).Colours(colourIndex) = PublicFuncs.Opposite(rightCubies(1).Colours(colourIndex)) Then

'        PutNextWrongEdgeOnTopFace(rightCubies, wrongCubies, colourIndex, topEdgesWhenSolved, topFace, cube, Instructions)
'    Else
'        'Line but wrong colours or L

'        MakeTopLine(rightCubies, rightIndex, wrongCubies, cube, Instructions)
'    End If
'    Dim wrongIndex As Integer = 0

'    rightIndex = 0
'    ReDim wrongCubies(0 To 3)
'    For Each cubie In topEdgesWhenSolved
'        If Not OnFace(cubie, topFace, cube.CurrentOrientation) Or Not CheckCubieRotation(cubie, topFace) Then
'            wrongCubies(wrongIndex) = cubie
'            wrongIndex += 1
'        Else
'            rightCubies(rightIndex) = cubie
'            rightIndex += 1
'        End If
'    Next
'    ReDim Preserve wrongCubies(0 To wrongIndex - 1)

'    numberOnTopFace = rightIndex
'    If numberOnTopFace = 2 Then
'        MakeTopT(rightCubies, rightIndex, wrongCubies, numberOnTopFace, topEdgesWhenSolved, cube, Instructions)
'    End If
'End Sub

''-x-
''xxx
''-x-
'Private Sub MakeTopCross(ByRef rightCubies() As Edge, ByRef rightIndex As Integer, ByRef wrongCubies() As Edge, ByRef numberOnTopFace As Integer, ByVal topEdgesWhenSolved() As Edge, ByRef cube As Cube, ByRef Instructions As String)


'    Dim topFace As FaceColours = PublicFuncs.ColourChar2FaceNumber(cube.CurrentOrientation.Top)

'    Dim colourIndex As Integer
'    If rightCubies(0).PrimaryFace = cube.CurrentOrientation.Top Then
'        colourIndex = 1
'    Else
'        colourIndex = 0
'    End If

'    PutNextWrongEdgeOnTopFace(rightCubies, wrongCubies, colourIndex, topEdgesWhenSolved, topFace, cube, Instructions)

'    Dim wrongIndex As Integer = 0
'    rightIndex = 0
'    ReDim wrongCubies(0 To 3)
'    For Each cubie In topEdgesWhenSolved
'        If Not OnFace(cubie, topFace, cube.CurrentOrientation) Or Not CheckCubieRotation(cubie, topFace) Then
'            wrongCubies(wrongIndex) = cubie
'            wrongIndex += 1
'        Else
'            rightCubies(rightIndex) = cubie
'            rightIndex += 1
'        End If
'    Next
'    ReDim Preserve wrongCubies(0 To wrongIndex - 1)
'    numberOnTopFace = rightIndex
'    If numberOnTopFace = 3 Then
'        MakeTopCross(rightCubies, rightIndex, wrongCubies, numberOnTopFace, topEdgesWhenSolved, cube, Instructions)
'    End If

'End Sub
'Private Sub PutNextWrongEdgeOnTopFace(ByRef rightCubies() As Edge, ByRef wrongCubies() As Edge, ByVal colourIndex As Integer, ByVal topEdgesWhenSolved() As Edge, ByVal topFace As FaceColours, ByRef cube As Cube, ByRef Instructions As String)

'    Dim currentCubie As Edge = wrongCubies(0)
'    Dim faceToRotate, secondFaceToRotate As FaceColours
'    Dim direction As Direction = Direction.NO_CHANGE
'    Dim oneMove As Boolean
'    oneMove = True

'    Dim faces() As FaceColours = cube.FaceColours
'    If currentCubie.Position.y = 0 Then
'        faceToRotate = GetFaceToRotateForMakingTopCross(currentCubie, cube)
'        direction = GetDirectionToPutEdgeFromMiddleToTop(currentCubie, faceToRotate, faces)

'    ElseIf currentCubie.Position.y = -1 Then
'        If currentCubie.PrimaryFace = cube.CurrentOrientation.Top And currentCubie.Rotation = cube.CurrentOrientation.Bottom Or
'            currentCubie.PrimaryFace <> cube.CurrentOrientation.Top And currentCubie.Rotation <> cube.CurrentOrientation.Bottom Then
'            faceToRotate = GetFaceToRotateForMakingTopCross(currentCubie, cube)
'            direction = Direction.HALF_TURN
'        Else
'            oneMove = False
'            Select Case currentCubie.Position.x
'                Case -1 ' left edge cubie
'                    faceToRotate = faces(1)
'                    secondFaceToRotate = faces(2)
'                Case 0
'                    If currentCubie.Position.z = -1 Then ' back edge cubie
'                        faceToRotate = faces(2)
'                        secondFaceToRotate = faces(3)
'                    Else ' front edge cubie
'                        faceToRotate = faces(4)
'                        secondFaceToRotate = faces(1)
'                    End If
'                Case 1 ' right edge cubie
'                    faceToRotate = faces(3)
'                    secondFaceToRotate = faces(4)
'            End Select
'            direction = Direction.CLOCKWISE
'        End If
'    ElseIf currentCubie.Position.y = 1 Then

'        ReDim Preserve wrongCubies(0 To wrongCubies.Length)
'        wrongCubies(wrongCubies.Length - 1) = currentCubie
'        RemoveBlockFromEdgeArray(currentCubie, rightCubies)

'        faceToRotate = GetFaceToRotateForMakingTopCross(currentCubie, cube)

'        'will always be wrongly rotated as only wrongly rotated cubies on top face will be a
'        RotateFace(faceToRotate, Direction.CLOCKWISE, cube, Instructions)

'        For Each wrongCubie In wrongCubies
'            If wrongCubie.Colours(colourIndex) = currentCubie.Colours(colourIndex) Then
'                currentCubie = wrongCubie
'                Exit For
'            End If
'        Next
'        'If currentCubie Is Nothing Then
'        '    For Each rightCubie In rightCubies
'        '    If rightCubie.Colours(colourIndex) = currentCubie.Colours(colourIndex) Then
'        '        currentCubie = rightCubie
'        '        Exit For
'        '    End If
'        '    Next
'        'End If

'        faceToRotate = GetFaceToRotateForMakingTopCross(currentCubie, cube)
'        Select Case faceToRotate
'            Case faces(1) ' left
'                direction = currentCubie.Position.z
'            Case faces(2) ' back
'                direction = -currentCubie.Position.x
'            Case faces(3) ' right
'                direction = -currentCubie.Position.z
'            Case faces(4) ' front
'                direction = currentCubie.Position.x
'        End Select

'    End If
'    '||||...... up to here||||

'    'get colour of currentcubiie
'    'find the topcubie with the correct colour + 1
'    'orientate top face 

'    Dim currentCubieColour As Char = currentCubie.Colours(colourIndex)
'    Dim clockwiseCubieColour As Char
'    Dim clockwiseCubie As Edge = Nothing

'    Dim facecolours() As Char = cube.FaceChars
'    For i = 1 To 4
'        If FaceColours(i) = currentCubieColour Then
'            clockwiseCubieColour = FaceColours((i Mod 4) + 1)
'            Exit For
'        End If
'    Next

'    For Each rightCubie In rightCubies
'        If rightCubie.Colours(colourIndex) = clockwiseCubieColour Then
'            clockwiseCubie = rightCubie
'            Exit For
'        End If
'    Next

'    Dim topDirection As Direction
'    Select Case faceToRotate
'        Case faces(1) ' left
'            Select Case clockwiseCubie.Position.z
'                Case -1 ' back edge cubie
'                    topDirection = Direction.NO_CHANGE
'                Case 0
'                    topDirection = clockwiseCubie.Position.x
'                Case 1 ' front edge cubie
'                    topDirection = Direction.HALF_TURN
'            End Select
'        Case faces(2) ' back
'            Select Case clockwiseCubie.Position.x
'                Case -1 ' left edge cubie
'                    topDirection = Direction.HALF_TURN
'                Case 0
'                    topDirection = clockwiseCubie.Position.z
'                Case 1 ' right edge cubie
'                    topDirection = Direction.NO_CHANGE
'            End Select
'        Case faces(3) ' right
'            Select Case clockwiseCubie.Position.z
'                Case -1 ' back edge cubie
'                    topDirection = Direction.HALF_TURN
'                Case 0
'                    topDirection = -clockwiseCubie.Position.x
'                Case 1 ' front edge cubie
'                    topDirection = Direction.NO_CHANGE
'            End Select
'        Case faces(4) ' front
'            Select Case clockwiseCubie.Position.x
'                Case -1 ' left edge cubie
'                    topDirection = Direction.NO_CHANGE
'                Case 0
'                    topDirection = -clockwiseCubie.Position.z
'                Case 1 ' right edge cubie
'                    topDirection = Direction.HALF_TURN
'            End Select
'    End Select

'    RotateFace(topFace, topDirection, cube, Instructions)
'    RotateFace(faceToRotate, direction, cube, Instructions)
'    If Not oneMove Then
'        RotateFace(topFace, Direction.CLOCKWISE, cube, Instructions)
'        RotateFace(secondFaceToRotate, Direction.ANTICLOCKWISE, cube, Instructions)
'    End If


'Public Sub DoTopCross(ByRef cube As Cube, ByRef Instructions As String)

'    Dim topBlocksWhenSolved() As Block = BlocksByColour(cube.CurrentOrientation.Top, cube)
'    Dim topEdgesWhenSolved() As Edge = ExtractEdgesFromBlocks(topBlocksWhenSolved)
'    Dim rightCubies() As Edge = GetCorrectTopEdges(topEdgesWhenSolved, cube)
'    Dim wrongCubies() As Edge = GetIncorrectTopEdges(topEdgesWhenSolved, cube)
'    'replace this with a pointer
'    Dim currentRightCubieIndex = CountNotNothing(rightCubies)

'    Do
'        DoNextTopEdge(rightCubies, currentRightCubieIndex, wrongCubies, topEdgesWhenSolved, cube, Instructions)
'    Loop While CountNotNothing(rightCubies) < 4

'End Sub

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

'Public Function TopEdgeCubieCorrect(ByVal cubie As Edge, ByVal cube As Cube) As Boolean
'    Dim topFace As FaceColours = PublicFuncs.ColourChar2FaceNumber(cube.CurrentOrientation.Top)
'    Return OnFace(cubie, topFace, cube.CurrentOrientation) And CheckCubieRotation(cubie, topFace)
'End Function

'Private Sub DoNextTopEdge(ByRef rightCubies() As Edge, ByRef currentRightCubieIndex As Integer, ByRef wrongCubies() As Edge, ByVal topEdgesWhenSolved() As Edge, ByRef cube As Cube, ByRef Instructions As String)

'    Dim currentNumberOfCorrectEdges As Integer = CountNotNothing(rightCubies)
'    Select Case currentNumberOfCorrectEdges
'        Case 0
'            MakeTopDash(rightCubies, currentRightCubieIndex, wrongCubies, cube, Instructions)
'        Case 1
'            MakeTopLine(rightCubies, currentRightCubieIndex, wrongCubies, cube, Instructions)
'        Case 2
'            MakeTopT(rightCubies, currentRightCubieIndex, wrongCubies, currentNumberOfCorrectEdges, topEdgesWhenSolved, cube, Instructions)
'        Case 3
'            MakeTopCross(rightCubies, currentRightCubieIndex, wrongCubies, currentNumberOfCorrectEdges, topEdgesWhenSolved, cube, Instructions)
'        Case 4
'            DoCrossPermutation(cube, Instructions)
'    End Select
'End Sub

''---
''-xx
''---
'Private Sub MakeTopDash(ByRef rightCubies() As Edge, ByRef rightIndex As Integer, ByRef wrongCubies() As Edge, ByRef cube As Cube, ByRef Instructions As String)
'    'if any cubies on middle layer
'    Try
'        MoveTopEdgeFromMiddleToTop(wrongCubies, rightCubies, rightIndex, cube, Instructions)
'    Catch ex As StageNotSuccessfulException
'        Try
'            MoveUpsideDownTopEdgeFromBottomLayerToTop(wrongCubies, rightCubies, rightIndex, cube, Instructions)
'        Catch ex2 As StageNotSuccessfulException
'            PutTopEdgeOnTop(wrongCubies, rightCubies, rightIndex, cube, Instructions)
'        End Try
'    End Try

'End Sub

'Private Sub MoveTopEdgeFromMiddleToTop(ByRef wrongCubies() As Edge, ByRef rightCubies() As Edge, ByRef rightIndex As Integer, ByRef cube As Cube, ByRef Instructions As String)

'    'if any cubies on middle layer
'    For Each cubie In wrongCubies
'        If cubie.Position.y = 0 Then
'            Dim faceToRotate As FaceColours = GetFaceToRotateForMakingTopCross(cubie, cube)
'            Dim faces() As FaceColours = cube.FaceColours
'            Dim direction As Direction = GetDirectionToPutEdgeFromMiddleToTop(cubie, faceToRotate, faces)

'            RotateFace(faceToRotate, direction, cube, Instructions)
'            wrongCubies = RemoveBlockFromEdgeArray(cubie, wrongCubies)
'            rightCubies(rightIndex) = cubie
'            rightIndex += 1

'            Return
'        End If
'    Next
'    Throw New StageNotSuccessfulException
'End Sub

'Private Function TopEdgeUpsideDownOnBottomLayer(ByVal cubie As Edge, ByVal orientation As CubeOrientation) As Boolean
'    Return cubie.PrimaryFace = orientation.Top And cubie.Rotation = orientation.Bottom Or cubie.PrimaryFace <> orientation.Top And cubie.Rotation <> orientation.Bottom
'End Function

'Private Sub MoveUpsideDownTopEdgeFromBottomLayerToTop(ByRef wrongCubies() As Edge, ByRef rightCubies() As Edge, ByRef rightIndex As Integer, ByRef cube As Cube, ByRef Instructions As String)
'    For Each cubie In wrongCubies
'        If TopEdgeUpsideDownOnBottomLayer(cubie, cube.CurrentOrientation) Then
'            Dim faceToRotate As FaceColours = GetFaceToRotateForMakingTopCross(cubie, cube)
'            RotateFace(faceToRotate, Direction.HALF_TURN, cube, Instructions)
'            wrongCubies = RemoveBlockFromEdgeArray(cubie, wrongCubies)
'            rightCubies(rightIndex) = cubie
'            rightIndex += 1
'            Return
'        End If
'    Next
'    Throw New StageNotSuccessfulException
'End Sub

'Private Sub PutTopEdgeOnTop(ByRef wrongCubies() As Edge, ByRef rightCubies() As Edge, ByRef rightIndex As Integer, ByRef cube As Cube, ByRef Instructions As String)
'    Dim faceToRotate, secondFaceToRotate As FaceColours
'    Dim faces() As FaceColours = cube.FaceColours
'    Dim cubie As Edge = wrongCubies(0)
'    Select Case cubie.Position.x
'        Case -1 ' left edge cubie
'            faceToRotate = faces(1)
'            secondFaceToRotate = faces(2)
'        Case 0
'            If cubie.Position.z = -1 Then ' back edge cubie
'                faceToRotate = faces(2)
'                secondFaceToRotate = faces(3)
'            Else ' front edge cubie
'                faceToRotate = faces(4)
'                secondFaceToRotate = faces(1)
'            End If
'        Case 1 ' right edge cubie
'            faceToRotate = faces(3)
'            secondFaceToRotate = faces(4)
'    End Select
'    RotateFace(faceToRotate, Direction.CLOCKWISE, cube, Instructions)
'    RotateFace(secondFaceToRotate, Direction.ANTICLOCKWISE, cube, Instructions)
'    wrongCubies = RemoveBlockFromEdgeArray(cubie, wrongCubies)
'    rightCubies(rightIndex) = cubie
'    rightIndex += 1
'End Sub

'Public Sub DoTopCorners(ByRef cube As Cube, ByRef Instructions As String)

'    Dim topBlocksWhenSolved() As Block = BlocksByColour(cube.CurrentOrientation.Top, cube)
'    Dim topCornersWhenSolved(0 To 3) As Corner
'    Dim index As Integer
'    For Each block In topBlocksWhenSolved
'        If block.GetType = GetType(Corner) Then
'            topCornersWhenSolved(index) = block
'            index += 1
'        End If
'    Next

'    Dim wrongCubies(0 To 3), rightCubies(0 To 3) As Corner
'    Dim topFace As FaceColours = PublicFuncs.ColourChar2FaceNumber(cube.CurrentOrientation.Top)
'    'move to seperate sub? - probbaly,
'    Dim wrongIndex As Integer = 0
'    Dim rightIndex As Integer = 0
'    For Each topCubie In topCornersWhenSolved
'        If Not TopCornerCorrect(topCubie, topFace, cube) Then
'            wrongCubies(wrongIndex) = topCubie
'            wrongIndex += 1
'        Else
'            rightCubies(rightIndex) = topCubie
'            rightIndex += 1
'        End If
'    Next
'    ReDim Preserve wrongCubies(0 To wrongIndex - 1)
'    Dim numberOnTopFace = rightIndex

'    Dim faceColours() As Char = cube.FaceChars
'    Dim faces() As FaceColours = cube.FaceColours

'    While numberOnTopFace < 4
'        PutNextCornerOnTopFace(rightCubies, wrongCubies, cube, Instructions)
'        numberOnTopFace += 1
'    End While

'End Sub
