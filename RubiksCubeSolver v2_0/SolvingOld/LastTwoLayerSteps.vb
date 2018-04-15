Imports RubiksCubeSolver_v2_0.MyPublic.PublicConstants
Imports PublicFuncs = RubiksCubeSolver_v2_0.MyPublic.PublicFunctions
Imports FaceColours = RubiksCubeSolver_v2_0.MyPublic.PublicConstants.FaceColour

Module LastTwoLayerSteps



End Module


'Public Sub OrientateBottomCorners(ByRef cube As Cube, ByRef Instructions As String)
'    OrientateTopFace(cube, Instructions)
'    ' TODO: This is bad maybe rewrite at some point
'    ' maybe write function to check whether it needs to be rotated clockwise or anticlockwise instead of horrible if statemtns


'    Dim bottomCorners() As Corner = TopCornersClockwise(cube)

'    Dim faceColours() As Char = cube.FaceChars
'    Dim faces(0 To 5) As FaceColours
'    'Dim originalFaces() As Faces = faces
'    For i = 0 To 5
'        faces(i) = PublicFuncs.ColourChar2FaceNumber(faceColours(i))
'    Next

'    Dim numberOfWrongCorners As Integer = 0
'    For Each cubie In bottomCorners
'        If Not CheckCubieRotation(cubie, faces(0)) Then
'            numberOfWrongCorners += 1
'        End If
'    Next

'    For i = 0 To 3
'        Dim originalBottom As FaceColours
'        Dim cornerOne As Corner = bottomCorners(i)

'        faceColours = cube.FaceChars
'        For j = 0 To 5
'            faces(j) = PublicFuncs.ColourChar2FaceNumber(faceColours(j))
'        Next

'        If Not CheckCubieRotation(cornerOne, faces(0)) Then

'            Dim cornerTwo As Corner = bottomCorners((i + 1) Mod 4)
'            Dim faceOne As FaceColours = faces(0)
'            originalBottom = faceOne
'            Dim faceTwo As FaceColours = faces(i + 1)
'            Dim pairOfCorners As Boolean = True

'            If cornerOne.Rotation = cornerTwo.Rotation And
'            cornerOne.PrimaryFace = cornerTwo.PrimaryFace Then
'                'If cornerOne.PrimaryFace = cornerTwo.PrimaryFace Then
'                'rotate other face colour to top, primaryface to left
'                If faceOne = PublicFuncs.ColourChar2FaceNumber(cornerOne.PrimaryFace) Then
'                    RotateFaceToTop(faceTwo, cube, Instructions)
'                    RotateFaceToLeft(faceOne, cube, Instructions)
'                Else
'                    RotateFaceToTop(faceOne, cube, Instructions)
'                    RotateFaceToLeft(faceTwo, cube, Instructions)
'                End If
'            ElseIf cornerOne.SecondaryRotation = cornerTwo.SecondaryRotation And
'            cornerOne.SecondaryFace = cornerTwo.SecondaryFace Then
'                'rotate other face colour to top, secondaryFace to left
'                If faceOne = PublicFuncs.ColourChar2FaceNumber(cornerOne.SecondaryFace) Then
'                    RotateFaceToTop(faceTwo, cube, Instructions)
'                    RotateFaceToLeft(faceOne, cube, Instructions)
'                Else
'                    RotateFaceToTop(faceOne, cube, Instructions)
'                    RotateFaceToLeft(faceTwo, cube, Instructions)
'                End If
'            ElseIf cornerOne.Rotation = cornerTwo.Rotation Or
'            cornerOne.SecondaryRotation = cornerTwo.SecondaryRotation Then
'                'whichever of the faces is = primary or secondary face 
'                'then rotate that one to the top and the other left?
'                'maybe?
'                If faceOne = PublicFuncs.ColourChar2FaceNumber(cornerOne.PrimaryFace) Or
'                faceOne = PublicFuncs.ColourChar2FaceNumber(cornerOne.SecondaryFace) Or
'                faceOne = PublicFuncs.ColourChar2FaceNumber(cornerTwo.PrimaryFace) Or
'                faceOne = PublicFuncs.ColourChar2FaceNumber(cornerTwo.SecondaryFace) Then
'                    RotateFaceToTop(faceOne, cube, Instructions)
'                    RotateFaceToLeft(faceTwo, cube, Instructions)
'                Else
'                    RotateFaceToTop(faceTwo, cube, Instructions)
'                    RotateFaceToLeft(faceOne, cube, Instructions)
'                End If
'            ElseIf numberOfWrongCorners = 3 Then
'                '1 is correctly orientated
'                Console.WriteLine("Fish")
'                Dim clockwise As Boolean = False

'                If cornerOne.PrimaryFace = faceOne.ToString() Then
'                    If cornerOne.Rotation = faceTwo.ToString() Then
'                        clockwise = True
'                    End If
'                ElseIf cornerOne.SecondaryFace = faceOne.ToString() Then
'                    If cornerOne.SecondaryRotation = faceTwo.ToString() Then
'                        clockwise = True
'                    End If
'                ElseIf cornerOne.PrimaryFace = faceTwo.ToString() Then
'                    If cornerOne.Rotation <> faceOne.ToString() Then
'                        clockwise = True
'                    End If
'                ElseIf cornerOne.SecondaryFace = faceTwo.ToString() Then
'                    If cornerOne.SecondaryRotation <> faceOne.ToString() Then
'                        clockwise = True
'                    End If
'                Else
'                    'TODO THIS DOESNT WORK - maybe not needed now?
'                    If cornerOne.PrimaryFace <> faceOne.ToString() And cornerOne.SecondaryFace <> faceOne.ToString() Then
'                        clockwise = True
'                    End If
'                End If


'                If clockwise Then 'if cubie (i.e. all 3) needs rotated clockwise: rotate edge to right of fish
'                    If CheckCubieRotation(bottomCorners((i + 3) Mod 4), faceOne) Then
'                        faceOne = faces(i + 1)
'                        faceTwo = faces(0)
'                    ElseIf CheckCubieRotation(bottomCorners((i + 2) Mod 4), faceOne) Then
'                        faceOne = faces((i + 3) Mod 4 + 1)
'                        faceTwo = faces(0)
'                    ElseIf CheckCubieRotation(bottomCorners((i + 1) Mod 4), faceOne) Then
'                        faceOne = faces((i + 2) Mod 4 + 1)
'                        faceTwo = faces(0)
'                    End If
'                Else 'if cubie (i.e. all 3) needs rotated anticlockwise: rotate edge below fish
'                    If CheckCubieRotation(bottomCorners((i + 2) Mod 4), faceOne) Then
'                        faceOne = faces(i + 1)
'                        faceTwo = faces(0)
'                    ElseIf CheckCubieRotation(bottomCorners((i + 3) Mod 4), faceOne) Then
'                        faceOne = faces((i + 1) Mod 4 + 1)
'                        faceTwo = faces(0)
'                    ElseIf CheckCubieRotation(bottomCorners((i + 1) Mod 4), faceOne) Then
'                        'check this
'                        faceOne = faces((i + 3) Mod 4 + 1)
'                        faceTwo = faces(0)
'                    End If
'                End If

'                ' |Y|Y| | <-
'                ' |Y|Y|Y| <-Right of fish
'                ' | |Y| | <-
'                '  /\ /\
'                '  || ||
'                ' below fish

'                RotateFaceToTop(faceOne, cube, Instructions)
'                RotateFaceToLeft(faceTwo, cube, Instructions)

'                faceColours = cube.FaceChars

'                For j = 0 To 5
'                    faces(j) = PublicFuncs.ColourChar2FaceNumber(faceColours(j))
'                Next

'                Algorithms.BottomEdgeAlgorithmRight(faces, cube, Instructions)
'                Algorithms.BottomEdgeAlgorithmLeft(faces, cube, Instructions)


'                RotateFaceToTop(originalBottom, cube, Instructions)

'                OrientateBottomCorners(cube, Instructions)
'                Exit For

'            ElseIf Not CheckCubieRotation(bottomCorners((i + 2) Mod 4), faceOne) Then
'                If CheckCubieRotation(bottomCorners((i + 1) Mod 4), faceOne) And
'            CheckCubieRotation(bottomCorners((i + 3) Mod 4), faceOne) Then

'                    'current cubie is wronglyrotated
'                    'find which face is == top
'                    'rotate to it on top and otherface on left :)

'                    Dim newTop As FaceColours

'                    If cornerOne.PrimaryFace = faceOne.ToString() Then
'                        newTop = PublicFuncs.ColourChar2FaceNumber(cornerOne.Rotation)
'                    ElseIf cornerOne.SecondaryFace = faceOne.ToString Then
'                        newTop = PublicFuncs.ColourChar2FaceNumber(cornerOne.SecondaryRotation)
'                    Else
'                        Dim facesAroundCorner() As FaceColours = {faces(0), faces(i + 1), faces(i + 2)}
'                        For Each face In facesAroundCorner
'                            If face.ToString() <> cornerOne.Rotation And
'                            face.ToString() <> cornerOne.SecondaryRotation Then
'                                newTop = face
'                            End If
'                        Next

'                        If newTop = faceOne Then
'                            faceOne = faceTwo
'                        ElseIf newTop = Nothing Then
'                            MsgBox("Error - cannot find face of corner cubie that matches the bottom face")
'                            newTop = faceOne
'                        End If
'                    End If

'                    RotateFaceToTop(newTop, cube, Instructions)
'                    RotateFaceToLeft(faceOne, cube, Instructions)


'                    'NO: what Is it's 2 opposite corners only --> what do about this
'                    '   rotating any random pair appears to give 1 correct orientation
'                    '   but you want to flip it when the correct face is on top
'                    '   i.e. when solving red, you want a red cubie face on top and 
'                    '   the red face on the left :: doesnt matter whetehr it's top or bottom
'                End If
'            Else

'                'they dont need dealt with cos they dont make a pair maybe?

'                'BUT ALGO STILL BEING DONE ON NON-PAIRS
'                pairOfCorners = False

'            End If

'            faceColours = cube.FaceChars
'            For j = 0 To 5
'                faces(j) = PublicFuncs.ColourChar2FaceNumber(faceColours(j))
'            Next

'            If pairOfCorners Then
'                Algorithms.BottomEdgeAlgorithmRight(faces, cube, Instructions)
'                Algorithms.BottomEdgeAlgorithmLeft(faces, cube, Instructions)

'                If cube.CurrentOrientation.Top <> originalBottom.ToString() Then
'                    RotateFaceToTop(originalBottom, cube, Instructions)
'                End If

'                'is this a good idea??
'                If Not CheckComplete(cube) Then
'                    OrientateBottomCorners(cube, Instructions)
'                    Return
'                Else
'                    Return
'                End If
'            End If
'        End If
'    Next
'End Sub



'METHOD FOR BOTTOMLAYER?? MAYBE??
'Public Sub PermuteTopCrossEdges(ByRef cube As Cube, ByRef Instructions As String)

'    Dim topEdges() As Edge = TopEdgesClockwise(cube)
'    Dim topEdgeColours(0 To 3) As Char
'    For i = 0 To 3
'        For colour = 0 To 1
'            If topEdges(i).Colours(colour) <> cube.CurrentOrientation.Top Then
'                topEdgeColours(i) = topEdges(i).Colours(colour)
'            End If
'        Next
'    Next

'    Dim faceColours() As Char = cube.FaceChars
'    Dim faces(0 To 5) As FaceColours
'    For i = 0 To 5
'        faces(i) = PublicFuncs.ColourChar2FaceNumber(faceColours(i))
'    Next

'    If topEdgeColours(0) = PublicFuncs.Opposite(topEdgeColours(2)) Then
'        'opposites are correct
'        Console.WriteLine("LINE")
'        Algorithms.BottomEdgeAlgorithmRight(faces, cube, Instructions)
'        RotateFace(faces(0), Direction.ANTICLOCKWISE, cube, Instructions)
'        Algorithms.BottomEdgeAlgorithmRight(faces, cube, Instructions)
'    Else
'        Dim edgesCorrect As Boolean = False
'        For i = 0 To 3
'            If topEdgeColours(i) <> PublicFuncs.Opposite(topEdgeColours((i + 1) Mod 4)) Then
'                For face = 1 To 4
'                    If topEdgeColours(i) = faceColours(face) Then
'                        If topEdgeColours((i + 1) Mod 4) = faceColours(((face Mod 4) + 1)) Then
'                            Select Case i
'                                Case 0
'                                    RotateFace(faces(0), Direction.CLOCKWISE, cube, Instructions)
'                                Case 2
'                                    RotateFace(faces(0), Direction.ANTICLOCKWISE, cube, Instructions)
'                                Case 3
'                                    RotateFace(faces(0), Direction.HALF_TURN, cube, Instructions)
'                            End Select
'                            Algorithms.BottomEdgeAlgorithmRight(faces, cube, Instructions)
'                            edgesCorrect = True
'                            Exit For
'                        End If
'                    End If
'                Next face
'                If edgesCorrect Then
'                    Exit For
'                End If
'            End If
'        Next i
'    End If
'End Sub

'Public Sub PositionBottomCorners(ByRef cube As Cube, ByRef Instructions As String)

'    Dim bottomBlocks() As Block = TopBlocksClockwise(cube)

'    Dim faceColours() As Char = cube.FaceChars
'    Dim faces(0 To 5) As FaceColours
'    For i = 0 To 5
'        faces(i) = PublicFuncs.ColourChar2FaceNumber(faceColours(i))
'    Next

'    For i = 0 To 6 Step 2

'        Dim clockwiseEdge As Edge = bottomBlocks(i + 1)
'        Dim antiClockwiseEdge As Edge = bottomBlocks((i + 7) Mod 8)

'        Dim clockwiseEdgeColour, anticlockwiseEdgeColour As Char
'        If clockwiseEdge.Colours(0) = cube.CurrentOrientation.Top Then
'            clockwiseEdgeColour = clockwiseEdge.Colours(1)
'        Else
'            clockwiseEdgeColour = clockwiseEdge.Colours(0)
'        End If
'        If antiClockwiseEdge.Colours(0) = cube.CurrentOrientation.Top Then
'            anticlockwiseEdgeColour = antiClockwiseEdge.Colours(1)
'        Else
'            anticlockwiseEdgeColour = antiClockwiseEdge.Colours(0)
'        End If
'        'Console.WriteLine(anticlockwiseEdge.Name & "-" & topBlocks(i).Name & "-" & clockwiseEdge.Name)
'        If CheckCubieColours(bottomBlocks(i), clockwiseEdgeColour) And
'         CheckCubieColours(bottomBlocks(i), anticlockwiseEdgeColour) Then
'            ' topBlocks(i) is in the right place
'            ' work out whether we need to go clockwise or anticlockwise
'            Dim nextCorner As Integer = (i + 2) Mod 8

'            Dim clockwiseEdgeForNextCorner As Edge = bottomBlocks(nextCorner + 1)
'            Dim twiceClockwiseEdgeForNextCorner As Edge = bottomBlocks((nextCorner + 3) Mod 8)

'            Dim clockwiseEdgeColourForNextCorner, twiceClockwiseEdgeColourForNextCorner As Char
'            If clockwiseEdgeForNextCorner.Colours(0) = cube.CurrentOrientation.Top Then
'                clockwiseEdgeColourForNextCorner = clockwiseEdgeForNextCorner.Colours(1)
'            Else
'                clockwiseEdgeColourForNextCorner = clockwiseEdgeForNextCorner.Colours(0)
'            End If
'            If twiceClockwiseEdgeForNextCorner.Colours(0) = cube.CurrentOrientation.Top Then
'                twiceClockwiseEdgeColourForNextCorner = twiceClockwiseEdgeForNextCorner.Colours(1)
'            Else
'                twiceClockwiseEdgeColourForNextCorner = twiceClockwiseEdgeForNextCorner.Colours(0)
'            End If
'            Dim bottomFace As FaceColours = PublicFuncs.ColourChar2FaceNumber(cube.CurrentOrientation.Top)
'            If CheckCubieColours(bottomBlocks(nextCorner), clockwiseEdgeColourForNextCorner) And
'         CheckCubieColours(bottomBlocks(nextCorner), twiceClockwiseEdgeColourForNextCorner) Then
'                'corners need shifted clockwise
'                Select Case i
'                    Case 0 : RotateFace(bottomFace, Direction.ANTICLOCKWISE, cube, Instructions)
'                    Case 2 : RotateFace(bottomFace, Direction.HALF_TURN, cube, Instructions)
'                    Case 4 : RotateFace(bottomFace, Direction.CLOCKWISE, cube, Instructions)
'                End Select
'                Algorithms.BottomClockwiseCornerAlgorithm(faces, cube, Instructions)
'            Else
'                'corners need shifted anticlockwise
'                Select Case i
'                    Case 2 : RotateFace(bottomFace, Direction.ANTICLOCKWISE, cube, Instructions)
'                    Case 4 : RotateFace(bottomFace, Direction.HALF_TURN, cube, Instructions)
'                    Case 6 : RotateFace(bottomFace, Direction.CLOCKWISE, cube, Instructions)
'                End Select
'                Algorithms.BottomAnticlockwiseCornerAlgorithm(faces, cube, Instructions)
'            End If
'            Exit For
'        ElseIf i = 6 Then 'none in right place
'            Algorithms.BottomClockwiseCornerAlgorithm(faces, cube, Instructions)
'            PositionBottomCorners(cube, Instructions)
'            Exit For
'        End If
'    Next

'End Sub

'Public Sub DoMiddleRow(ByRef cube As Cube, ByRef Instructions As String)
'    Dim bottom As Char = cube.CurrentOrientation.Bottom
'    Dim bottomFace As FaceColours =
'    PublicFuncs.ColourChar2FaceNumber(bottom)
'    Dim MiddleEdgesWhenSolved(0 To 3) As Edge
'    Dim index As Integer = 0
'    For Each cubie In cube.Edges
'        If Not CheckCubieColours(cubie, cube.CurrentOrientation.Top) And
'        Not CheckCubieColours(cubie, bottom) Then
'            MiddleEdgesWhenSolved(index) = cubie
'            index += 1
'        End If
'    Next

'    For Each cubie In MiddleEdgesWhenSolved
'        If cubie.Position.y = -1 Then
'            Dim face As FaceColours
'            Dim otherFace As Char
'            If cubie.Rotation = bottom Then
'                face = PublicFuncs.ColourChar2FaceNumber(cubie.Colours(1))
'                otherFace = cubie.Colours(0)
'            Else
'                face = PublicFuncs.ColourChar2FaceNumber(cubie.Colours(0))
'                otherFace = cubie.Colours(1)
'            End If
'            RotateFaceToFront(face, cube, Instructions)

'            Dim direction As Direction
'            Select Case cubie.Position.x
'                Case -1 ' left edge cubie
'                    direction = direction.CLOCKWISE
'                Case 0
'                    If cubie.Position.z = -1 Then ' back edge cubie
'                        direction = direction.HALF_TURN
'                    Else ' front edge cubie
'                        direction = direction.NO_CHANGE
'                    End If
'                Case 1 ' right edge cubie
'                    direction = direction.ANTICLOCKWISE
'            End Select
'            RotateFace(bottomFace, direction, cube, Instructions)

'            Dim faceColours() As Char = cube.FaceChars
'            Dim faces() As FaceColours = cube.FaceColours

'            If otherFace = faceColours(1) Then
'                'Insert cubie into left
'                Algorithms.MiddleLeftAlgorithm(faces, cube, Instructions)
'            ElseIf otherFace = faceColours(3) Then
'                'Insert cubie into right
'                Algorithms.MiddleRightAlgorithm(faces, cube, Instructions)
'            Else
'                Console.WriteLine("Error, middle edge cubie is not in correct place to insert into middle row")
'            End If

'        Else
'            'TEST
'            Dim faceToLeft, faceToRight As Char
'            Dim faceColours() As Char = cube.FaceChars
'            'why isnt this used?
'            'Dim direction AsDirection
'            Select Case (cubie.Position.x + cubie.Position.z)
'                Case -2 'Back left corner
'                    faceToLeft = faceColours(2)
'                    faceToRight = faceColours(1)
'                Case 0
'                    If cubie.Position.x = -1 Then ' Front left corner
'                        faceToLeft = faceColours(1)
'                        faceToRight = faceColours(4)
'                    ElseIf cubie.Position.x = 1 Then ' Back right corner
'                        faceToLeft = faceColours(3)
'                        faceToRight = faceColours(2)
'                    End If
'                Case 2 ' Front right corner
'                    faceToLeft = faceColours(4)
'                    faceToRight = faceColours(3)
'            End Select

'            'exor
'            'If (Not (faceToLeft = cubie.PrimaryFace) And cubie.Rotation = faceToLeft) Or
'            '    ((faceToLeft = cubie.PrimaryFace) And Not (cubie.Rotation = faceToLeft)) Then
'            'If Not (faceToLeft = cubie.PrimaryFace And cubie.Rotation = faceToLeft) And
'            '    Not (faceToRight = cubie.PrimaryFace And cubie.Rotation = faceToRight) Then

'            If cubie.PrimaryFace = faceToLeft And cubie.Rotation <> faceToLeft Or
'            cubie.PrimaryFace = faceToRight And cubie.Rotation <> faceToRight Or
'            cubie.PrimaryFace <> faceToLeft And cubie.PrimaryFace <> faceToRight Or
'            Not (CheckCubieColours(cubie, faceToLeft) And CheckCubieColours(cubie, faceToRight)) Then

'                Dim face As FaceColours = PublicFuncs.ColourChar2FaceNumber(faceToLeft)
'                RotateFaceToFront(face, cube, Instructions)
'                faceColours = cube.FaceChars
'                Dim faces(0 To 5) As FaceColours
'                For i = 0 To 5
'                    faces(i) = PublicFuncs.ColourChar2FaceNumber(faceColours(i))
'                Next
'                'Insert cubie into right
'                Algorithms.MiddleRightAlgorithm(faces, cube, Instructions)


'                '       Recurse??? WILL THIS WORK????? TODO CHECK
'                DoMiddleRow(cube, Instructions)
'                Exit For
'            End If
'        End If
'        Console.WriteLine("Next Middle Edge")
'    Next


'End Sub
'Public Sub DoBottomCross(ByRef cube As Cube, ByRef Instructions As String)
'    Dim bottomFace As FaceColours =
'            PublicFuncs.ColourChar2FaceNumber(cube.CurrentOrientation.Top)
'    Dim bottomCrossCorrectRotated(0 To 3) As Edge
'    Dim index As Integer = 0
'    For Each cubie In cube.Edges
'        If CheckCubieColours(cubie, cube.CurrentOrientation.Top) Then
'            If CheckCubieRotation(cubie, bottomFace) Then
'                bottomCrossCorrectRotated(index) = cubie
'                index += 1
'            End If
'        End If
'    Next
'    ReDim Preserve bottomCrossCorrectRotated(0 To index - 1)
'    Dim faceColours() As Char = cube.FaceChars
'    Dim faces(0 To 5) As FaceColours
'    For i = 0 To 5
'        faces(i) = PublicFuncs.ColourChar2FaceNumber(faceColours(i))
'    Next
'    Select Case bottomCrossCorrectRotated.Length()
'        Case 0, 1  'middle only
'            Algorithms.BottomCrossAlgorithm(faces, cube, Instructions)
'            DoBottomCross(cube, Instructions)
'        Case 2
'            'corner or middle line
'            Dim bottomEdgesOrdered(0 To 3) As Edge
'            For i = 0 To bottomCrossCorrectRotated.Length - 1
'                Select Case bottomCrossCorrectRotated(i).Position.x
'                    Case -1 ' left edge cubie
'                        index = 0
'                    Case 0
'                        If bottomCrossCorrectRotated(i).Position.z = -1 Then ' back edge cubie
'                            index = 1
'                        Else ' front edge cubie
'                            index = 3
'                        End If
'                    Case 1 ' right edge cubie
'                        index = 2
'                End Select
'                bottomEdgesOrdered(index) = bottomCrossCorrectRotated(i)
'            Next

'            For i = 0 To bottomEdgesOrdered.Length - 1
'                If bottomEdgesOrdered(i) IsNot Nothing Then
'                    If bottomEdgesOrdered((i + 2) Mod 4) IsNot Nothing Then ' Bottom face in line configuration
'                        If i = 1 Or i = 3 Then
'                            RotateFace(bottomFace, Direction.CLOCKWISE, cube, Instructions)
'                        End If
'                        Algorithms.BottomCrossAlgorithm(faces, cube, Instructions)
'                        Exit For
'                    Else ' Bottom face in L config.
'                        Select Case i
'                            Case 0
'                                If bottomEdgesOrdered(1) Is Nothing Then
'                                    RotateFace(bottomFace, Direction.CLOCKWISE, cube, Instructions)
'                                End If
'                            Case 1 ' top right
'                                RotateFace(bottomFace, Direction.ANTICLOCKWISE, cube, Instructions)
'                            Case 2 ' bottom right
'                                RotateFace(bottomFace, Direction.HALF_TURN, cube, Instructions)
'                            Case 3 ' bottom left
'                                RotateFace(bottomFace, Direction.CLOCKWISE, cube, Instructions)
'                        End Select
'                        Algorithms.BottomCrossAlgorithm(faces, cube, Instructions)
'                        Algorithms.BottomCrossAlgorithm(faces, cube, Instructions)
'                        Exit For
'                    End If
'                End If
'            Next

'            'Case Else
'            '    Console.WriteLine("Hopefully the bottom cross is solved")
'    End Select
'End Sub
