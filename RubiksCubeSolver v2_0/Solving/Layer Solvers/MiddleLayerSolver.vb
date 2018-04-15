Imports RubiksCubeSolver_v2_0.Helpers

Public Class MiddleLayerSolver

    Private cube As Cube

    Sub New(ByRef cubeToBeSolved As Cube)
        cube = cubeToBeSolved
    End Sub

    Public Sub Solve()

        If Not MiddleRowCorrect() Then DoMiddleRow()

    End Sub

    Private Function MiddleRowCorrect() As Boolean
        Dim middleEdges() As Edge = cube.MiddleEdgesClockwise

        If Not (middleEdges(0).HasColour(cube.FaceColours(MoveFaces.FRONT)) And
            middleEdges(0).HasColour(cube.FaceColours(MoveFaces.LEFT))) Then Return False

        For i = 1 To 3
            If Not (middleEdges(i).HasColour(cube.FaceColours(i)) And
                middleEdges(i).HasColour(cube.FaceColours(i + 1))) Then Return False
        Next
        Return True

    End Function

    Private Sub DoMiddleRow()
        Dim edgesWhenSolved() As Edge = MiddleEdgesWhenSolved()
        For Each edge In edgesWhenSolved
            If Not edge.Correct(cube) Then PutEdgeInMiddle(edge)
        Next
    End Sub

    Private Function MiddleEdgesWhenSolved()
        Dim edges(3) As Edge
        Dim index As Integer = 0
        For Each cubie In cube.Edges
            If Not (cubie.HasColour(cube.CurrentOrientation.Top) Or
                cubie.HasColour(cube.CurrentOrientation.Bottom)) Then
                edges(index) = cubie
                index += 1
            End If
        Next
        Return edges
    End Function

    Private Sub PutEdgeInMiddle(ByVal edge As Edge)
        If edge.Layer = Layer.MIDDLE Then
            Dim leftFace As FaceColour = ClockwiseFace(edge)
            cube.RotateFaceToFront(leftFace)
            Algorithms.MiddleRightAlgorithm(cube)
            PutEdgeFromBottomToMiddle(edge)
        ElseIf edge.Layer = Layer.BOTTOM Then
            PutEdgeFromBottomToMiddle(edge)
        Else
            Throw New StageNotSuccessfulException("The top layer is not complete")
        End If
    End Sub

    'Clockwise when viewed top-down
    Private Function ClockwiseFace(ByVal edge As Edge) As FaceColour
        Dim faces() As FaceColour = cube.FaceColours
        Select Case (edge.Position.x + edge.Position.z)
            Case -2 'Back left edge
                Return faces(MoveFaces.BACK)
            Case 0
                If edge.Position.x = -1 Then ' Front left edge
                    Return faces(MoveFaces.LEFT)
                ElseIf edge.Position.x = 1 Then ' Back right edge
                    Return faces(MoveFaces.RIGHT)
                End If
            Case 2 ' Front right edge
                Return faces(MoveFaces.FRONT)
        End Select
        Throw New ArgumentException("edge has invalid position vector")
    End Function

    'Private Function AnticlockwiseFace(ByVal edge As Edge) As FaceColour
    '    Dim faces() As FaceColour = cube.FaceColours
    '    Select Case (edge.Position.x + edge.Position.z)
    '        Case -2 'Back left edge
    '            Return faces(MoveFaces.LEFT)
    '        Case 0
    '            If edge.Position.x = -1 Then ' Front left edge
    '                Return faces(MoveFaces.FRONT)
    '            ElseIf edge.Position.x = 1 Then ' Back right edge
    '                Return faces(MoveFaces.BACK)
    '            End If
    '        Case 2 ' Front right edge
    '            Return faces(MoveFaces.RIGHT)
    '    End Select
    '    Throw New ArgumentException("edge has invalid position vector")
    'End Function

    Private Sub PutEdgeFromBottomToMiddle(ByVal edge As Edge)

        If edge.Layer <> Layer.BOTTOM Then Throw New ArgumentException("The cubie is not on the bottom layer.")


        Dim sideFaces() As FaceColour = GetEdgeFaceColours(edge)
        Dim clockwiseFace As FaceColour = sideFaces(0)
        Dim anticlockwiseFace As FaceColour = sideFaces(1)


        While Not edge.Correct(cube)
            If edge.CorrectForFace(anticlockwiseFace, cube) Then
                cube.RotateFaceToFront(anticlockwiseFace)
                Algorithms.MiddleRightAlgorithm(cube)
            ElseIf edge.CorrectForFace(clockwiseFace, cube) Then
                cube.RotateFaceToFront(clockwiseFace)
                Algorithms.MiddleLeftAlgorithm(cube)
            Else
                cube.RotateFace(cube.BottomFace, Direction.ANTICLOCKWISE)
            End If
        End While
    End Sub

    ''' <returns> {clockwise, anticlockwise} viewed from bottom of the cube </returns>
    Private Function GetEdgeFaceColours(ByVal edge As Edge) As FaceColour()


        Dim cubieColours(1) As FaceColour
        For i = 0 To 1
            cubieColours(i) = edge.Colours(i)
        Next

        For face As MoveFaces = MoveFaces.LEFT To MoveFaces.FRONT
            Dim clockwiseFace As MoveFaces = face Mod 4 + 1 'Clockwise when viewed from top

            If cube.FaceColours(face) = cubieColours(0) And cube.FaceColours(clockwiseFace) = cubieColours(1) Or
                cube.FaceColours(face) = cubieColours(1) And cube.FaceColours(clockwiseFace) = cubieColours(0) Then
                Return {cube.FaceColours(face), cube.FaceColours(clockwiseFace)}
            End If
        Next

        Throw New ArgumentException("The edge is not a middle piece")
    End Function

End Class
