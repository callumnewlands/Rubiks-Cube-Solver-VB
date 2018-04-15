Imports PublicFuncs = RubiksCubeSolver_v2_0.MyPublic.PublicFunctions
Imports FaceColours = RubiksCubeSolver_v2_0.MyPublic.PublicConstants.FaceColour
Module CubeStateChecks



End Module


'Public Function CheckBottomCross(ByRef cube As Cube, ByRef Instructions As String) As Boolean
'    Dim bottom As FaceColours
'    bottom = PublicFuncs.ColourChar2FaceNumber(MyPublic.Opposite(cube.CurrentOrientation.Top))
'    Moves.RotateFaceToTop(bottom, cube, Instructions)
'    Console.WriteLine("CUBE FLIPPED UPSIDE DOWN: bottom -> cube.top")
'    cube.CurrentOrientation.Top = bottom.ToString()

'    Dim faceBlocks(8) As Block
'    faceBlocks = BlocksByColour(bottom.ToString, cube)
'    For i = 0 To 7
'        If faceBlocks(i).GetType() = GetType(Edge) Then
'            If Not OnFace(faceBlocks(i), bottom, cube.CurrentOrientation) Or
'                Not CheckCubieRotation(faceBlocks(i), bottom) Then
'                Console.WriteLine(bottom.ToString & " cross not complete")
'                Return False
'            Else
'                If i = 7 Then
'                    Console.WriteLine(bottom.ToString & " cross complete!")
'                    Return True
'                End If
'            End If
'        End If
'    Next
'    Return False
'End Function


'Public Function CheckMiddleRow(ByRef cube As Cube) As Boolean

'    Dim middleEdges() As Edge = MiddleEdgesClockwise(cube)
'    Dim faceColours() As Char = cube.FaceChars

'    If Not CheckCubieColours(middleEdges(0), faceColours(4)) Or
'        Not CheckCubieColours(middleEdges(0), faceColours(1)) Then
'        Console.WriteLine("Middle row not correct")
'        Return False
'    End If

'    For i = 1 To 3
'        If Not CheckCubieColours(middleEdges(i), faceColours(i)) Or
'            Not CheckCubieColours(middleEdges(i), faceColours(i + 1)) Then
'            Console.WriteLine("Middle row not correct")
'            Return False
'        End If
'    Next

'    Console.WriteLine("Middle row correct")
'    Return True
'End Function

'Public Function CheckComplete(ByRef cube As Cube) As Boolean
'    For face = 0 To 5
'        Dim faceColour As FaceColours = face
'        Dim faceBlocks() As Block = BlocksByColour(faceColour.ToString, cube)

'        For i = 0 To 7
'            If Not OnFace(faceBlocks(i), faceColour, cube.CurrentOrientation) Or
'                Not CheckCubieRotation(faceBlocks(i), faceColour) Then
'                Console.WriteLine(faceColour.ToString & " face NOT complete!")
'                Return False
'            End If
'        Next

'    Next

'    Return True
'End Function



'Public Function CheckForAnyCompleteFace(ByRef cube As Cube, ByRef Instructions As String) As Boolean

'    For face = 0 To 5
'        Dim faceBlocks(8) As Block
'        Dim faceColour As FaceColours
'        faceColour = face
'        faceBlocks = BlocksByColour(faceColour.ToString, cube)
'        For i = 0 To 7
'            If Not OnFace(faceBlocks(i), faceColour, cube.CurrentOrientation) Or Not CheckCubieRotation(faceBlocks(i), faceColour) Then Exit For 'next face
'            If i <> 7 Then Continue For
'            RotateFaceToTop(faceColour, cube, Instructions)
'            cube.CurrentOrientation.Top = faceColour.ToString
'            Console.WriteLine(faceColour.ToString & " face complete!")
'            Return True
'        Next
'    Next
'    Console.WriteLine("No faces complete")
'    Return False
'End Function


'Public Function CheckForAnyCross(ByRef cube As Cube, ByRef Instructions As String) As Boolean
'    For face = 0 To 5
'        Dim faceBlocks(8) As Block
'        Dim faceColour As FaceColours
'        faceColour = face
'        faceBlocks = BlocksByColour(faceColour.ToString, cube)
'        For i = 0 To 7
'            If faceBlocks(i).GetType() = GetType(Edge) Then
'                If Not OnFace(faceBlocks(i), faceColour, cube.CurrentOrientation) Or Not CheckCubieRotation(faceBlocks(i), faceColour) Then
'                    'Try next face
'                    'Console.WriteLine(faceColour.ToString & " cross not complete")
'                    Exit For
'                End If
'                If i = 7 Then
'                    RotateFaceToTop(faceColour, cube, Instructions)
'                    cube.CurrentOrientation.Top = faceColour.ToString
'                    Console.WriteLine(faceColour.ToString & " cross complete!")
'                    Return True
'                End If
'            End If
'        Next
'    Next
'    Console.WriteLine("No crosses complete")
'    Return False
'End Function


'Public Function CheckEdgesOfTopCross(ByRef cube As Cube) As Boolean

'    Dim faceColours() As Char = cube.FaceChars
'    Dim topEdges() As Edge = TopEdgesClockwise(cube)

'    Dim start As Integer
'    For i = 1 To 4
'        If CheckCubieColours(topEdges(0), faceColours(i)) Then
'            start = i
'        End If
'    Next
'    For i = 0 To 3
'        'Console.WriteLine(topEdges(i).Name & " -> " & faceColours(((start + i - 1) Mod 4) + 1).ToString() & ": " & PublicFuncs.CheckCubieColours(topEdges(i), faceColours(((start + i - 1) Mod 4) + 1)).ToString())
'        If Not CheckCubieColours(topEdges(i), faceColours(((start + i - 1) Mod 4) + 1)) Then
'            Console.WriteLine("Edges of cross in wrong order")
'            Return False
'        End If
'    Next
'    Console.WriteLine("Edges of cross in right order")
'    Return True
'End Function

'Public Function CheckCornersOfTopFace(ByRef cube As Cube) As Boolean
'    'remarks: ONLY WORKS PROVIDED cube.top HAS THE CORRECT CUBIES ALREADY
'    'TODo ^^ this could be a problem as this is called whithout checking corner location from solvecube

'    Dim topBlocks() As Block = TopBlocksClockwise(cube)
'    Dim faceColours() As Char = cube.FaceChars

'    Dim start As Integer
'    For i = 1 To 4
'        If CheckCubieColours(topBlocks(1), faceColours(i)) Then
'            start = i
'            Exit For
'        End If
'    Next
'    For i = 0 To 7

'        If topBlocks(i).Position.y <> 1 Then
'            Return False
'        End If

'        If topBlocks(i).GetType() = GetType(Edge) Then
'            If Not CheckCubieColours(topBlocks(i), faceColours((start - 2 + (i + 1) / 2) Mod 4 + 1)) Then
'                Console.WriteLine("Edges of " & cube.CurrentOrientation.Top & " face in wrong place")
'                Return False
'            End If
'        ElseIf topBlocks(i).GetType() = GetType(Corner) Then
'            'checks colours of corners - rotation already checked/not needed checked
'            Dim clockwiseEdge, anticlockwiseEdge As Edge
'            Dim clockwiseEdgeColour, anticlockwiseEdgeColour As Char
'            clockwiseEdge = topBlocks(i + 1)
'            anticlockwiseEdge = topBlocks((i + 7) Mod 8)
'            If clockwiseEdge.Colours(0) = cube.CurrentOrientation.Top Then
'                clockwiseEdgeColour = clockwiseEdge.Colours(1)
'            Else
'                clockwiseEdgeColour = clockwiseEdge.Colours(0)
'            End If
'            If anticlockwiseEdge.Colours(0) = cube.CurrentOrientation.Top Then
'                anticlockwiseEdgeColour = anticlockwiseEdge.Colours(1)
'            Else
'                anticlockwiseEdgeColour = anticlockwiseEdge.Colours(0)
'            End If
'            'Console.WriteLine(anticlockwiseEdge.Name & "-" & topBlocks(i).Name & "-" & clockwiseEdge.Name)
'            If Not CheckCubieColours(topBlocks(i), clockwiseEdgeColour) Or
'                 Not CheckCubieColours(topBlocks(i), anticlockwiseEdgeColour) Then
'                Console.WriteLine("Edges of " & cube.CurrentOrientation.Top & " face in wrong place")
'                Return False
'            End If
'        End If
'    Next

'    Console.WriteLine("Edges of " & cube.CurrentOrientation.Top & " face in right place")
'    Return True
'End Function

'Public Function CheckTopFaceRotation(ByRef cube As Cube) As Boolean
'    'remarks - only works if cube.top edges are correct

'    Dim topEdges() As Edge = TopEdgesClockwise(cube, 1)
'    Dim faceColours() As Char = cube.FaceChars

'    For i = 1 To 4
'        'Console.WriteLine(topEdges(i).Name & " " & faceColours(i).ToString())
'        If Not CheckCubieColours(topEdges(i), faceColours(i)) Then
'            Console.WriteLine("cube.top face in wrong orientation")
'            Return False
'        End If
'    Next
'    'Else
'    'Console.WriteLine("cube.top face in wrong orientation")
'    'Return False
'    'End If
'    Console.WriteLine("cube.top face in right orientation")
'    Return True
'End Function


''is this needed or could CheckSidesOfTopFace() do the same job?
'Public Function CheckTopFace(ByRef cube As Cube) As Boolean
'    Dim faceBlocks(8) As Block
'    Dim faceColour As FaceColours
'    faceColour = PublicFuncs.ColourChar2FaceNumber(cube.CurrentOrientation.Top)
'    faceBlocks = BlocksByColour(faceColour.ToString, cube)

'    For i = 0 To 7
'        If Not OnFace(faceBlocks(i), faceColour, cube.CurrentOrientation) Or Not CheckCubieRotation(faceBlocks(i), faceColour) Then
'            Console.WriteLine(faceColour.ToString & " face not complete")
'            Return False
'        Else
'            If i = 7 Then
'                'RotateFaceToTop(faceColour, _corners, _edges, _top, _front)
'                '_top = faceColour.ToString
'                Console.WriteLine(faceColour.ToString & " face complete!")
'                Return True
'            End If
'        End If
'    Next
'    Return False
'End Function

'Public Function TopCornerCorrect(ByVal cubie As Corner, ByVal topFace As FaceColours, ByVal cube As Cube)
'    If Not OnFace(cubie, topFace, cube.CurrentOrientation) Then
'        Return False
'    ElseIf Not CheckCubieRotation(cubie, topFace) Then
'        Return False
'    Else

'        'Only edges will be correct
'        Dim topBlocks() As Block = TopBlocksClockwise(cube)

'        Dim index As Integer

'        'For i = 0 To 7
'        '    If topBlocks(i).Name = cubie.Name Then
'        '        index = i
'        '    End If
'        'Next

'        Select Case (cubie.Position.x + cubie.Position.z)
'            Case -2 'Back left corner
'                index = 2
'            Case 0
'                If cubie.Position.x = -1 Then ' cube.front left corner
'                    index = 0
'                ElseIf cubie.Position.x = 1 Then ' Back right corner
'                    index = 4
'                End If
'            Case 2 ' cube.front right corner
'                index = 6
'        End Select


'        Dim clockwiseEdge, anticlockwiseEdge As Edge
'        Dim clockwiseEdgeColour, anticlockwiseEdgeColour As Char
'        clockwiseEdge = topBlocks(index + 1)
'        anticlockwiseEdge = topBlocks((index + 7) Mod 8)
'        If clockwiseEdge.Colours(0) = cube.CurrentOrientation.Top Then
'            clockwiseEdgeColour = clockwiseEdge.Colours(1)
'        Else
'            clockwiseEdgeColour = clockwiseEdge.Colours(0)
'        End If
'        If anticlockwiseEdge.Colours(0) = cube.CurrentOrientation.Top Then
'            anticlockwiseEdgeColour = anticlockwiseEdge.Colours(1)
'        Else
'            anticlockwiseEdgeColour = anticlockwiseEdge.Colours(0)
'        End If
'        'Console.WriteLine(anticlockwiseEdge.Name & "-" & topBlocks(i).Name & "-" & clockwiseEdge.Name)
'        If Not CheckCubieColours(cubie, clockwiseEdgeColour) Or
'             Not CheckCubieColours(cubie, anticlockwiseEdgeColour) Then
'            Return False
'        Else
'            Return True
'        End If
'    End If
'End Function