Imports RubiksCubeSolver_v2_0.Helpers

Public Class BottomLayerSolver

    Private cube As Cube

    Sub New(ByRef cubeToBeSolved As Cube)
        cube = cubeToBeSolved
    End Sub

    Public Sub Solve()
        If Not cube.Complete() Then SolveBottomLayer()
    End Sub

    Private Sub SolveBottomLayer()
        If Not BottomFaceComplete() Then
            If Not BottomCrossComplete() Then DoCross()
            DoCorners()
        End If
        RotateBottomFaceCorrectly()
    End Sub

    Private Function BottomFaceComplete() As Boolean
        Dim bottomCubies() As Block = cube.BlocksByColour(cube.BottomFace)
        For Each cubie In bottomCubies
            If Not cubie.CorrectForFace(cube.BottomFace, cube) Then Return False
        Next
        Return True
    End Function

    Private Function BottomCrossComplete() As Boolean
        Dim faceEdges As Edge() = cube.BlocksByColour(cube.BottomFace).Extract(Of Edge)()
        For Each edgeCubie In faceEdges

            If Not edgeCubie.CorrectForFace(cube.BottomFace, cube) Or (Not edgeCubie.Correct(cube) And Not EdgesOfBottomCrossInRightOrder()) Then Return False
        Next
        Return True
    End Function

    Private Function EdgesOfBottomCrossInRightOrder() As Boolean

        cube.RotateFaceToTop(cube.BottomFace)
        Dim edgesClockwise As Edge() = cube.TopEdgesClockwise

        For i As MoveFaces = MoveFaces.LEFT To MoveFaces.FRONT
            If edgesClockwise(i - 1).HasColour(MoveFaces.LEFT) Then
                edgesClockwise = edgesClockwise.Rotate(i - 1)
                Exit For
            End If
        Next
        For i = MoveFaces.LEFT To MoveFaces.FRONT
            If Not edgesClockwise(i - 1).HasColour(cube.FaceColours(i)) Then
                cube.RotateFaceToTop(cube.BottomFace)
                Return False
            End If
        Next
        cube.RotateFaceToTop(cube.BottomFace)
        Return True

    End Function

    Private Sub DoCross()
        cube.RotateFaceToTop(cube.BottomFace)
        DoBottomStage_L()
        DoBottomStage_Line()
        DoBottomStage_Cross()
        PermuteTopCrossEdges()
        cube.RotateFaceToTop(cube.BottomFace)
    End Sub

    'cube is upside down
    Private Sub DoBottomStage_L()
        If cube.CorrectTopEdges.Length >= 2 Then Return 'L is done
        Algorithms.BottomCrossAlgorithm(cube)
    End Sub

    'cube is upside down
    Private Sub DoBottomStage_Line()

        Dim correctBottomEdges() As Edge = cube.CorrectTopEdges()

        If cube.CorrectTopEdges.Length >= 3 Or
            New Vector2x1(correctBottomEdges(0).Position.x, correctBottomEdges(0).Position.z).Dot(New Vector2x1(correctBottomEdges(1).Position.x, correctBottomEdges(1).Position.z)) <> 0 Then
            Return ' line is done
        End If

        While (correctBottomEdges(0).Position.x <> -1 Or correctBottomEdges(1).Position.z <> -1) And
                (correctBottomEdges(1).Position.x <> -1 Or correctBottomEdges(0).Position.z <> -1)
            cube.RotateFace(cube.TopFace, Direction.ANTICLOCKWISE)
        End While

        Algorithms.BottomCrossAlgorithm(cube)
    End Sub

    'cube is upside down
    Private Sub DoBottomStage_Cross()

        If cube.CorrectTopEdges.Length = 4 Then Return ' Cross is done
        Dim correctBottomEdges() As Edge = cube.CorrectTopEdges()

        While IsNotLine(correctBottomEdges)
            cube.RotateFace(cube.TopFace, Direction.ANTICLOCKWISE)
        End While
        Algorithms.BottomCrossAlgorithm(cube)
    End Sub

    Private Function IsNotLine(ByVal correctCrossEdges()) As Boolean
        Select Case correctCrossEdges.Length
            Case 2
                Return (correctCrossEdges(0).Position.x <> -1 Or correctCrossEdges(1).Position.x <> 1) And
                (correctCrossEdges(1).Position.x <> -1 Or correctCrossEdges(0).Position.x <> 1)
            Case 3
                Return (correctCrossEdges(0).Position.x <> -1 Or correctCrossEdges(1).Position.x <> 1) And
                (correctCrossEdges(0).Position.x <> -1 Or correctCrossEdges(2).Position.x <> 1) And
                (correctCrossEdges(1).Position.x <> -1 Or correctCrossEdges(0).Position.x <> 1) And
                (correctCrossEdges(1).Position.x <> -1 Or correctCrossEdges(2).Position.x <> 1) And
                (correctCrossEdges(2).Position.x <> -1 Or correctCrossEdges(0).Position.x <> 1) And
                (correctCrossEdges(2).Position.x <> -1 Or correctCrossEdges(1).Position.x <> 1)
            Case Else
                Throw New ArgumentException("2 edge faces are not solved - error in BottomLayerSolver")
        End Select
    End Function

    Private Sub PermuteTopCrossEdges()


        Dim crossEdges() As Edge = cube.TopEdgesClockwise()

        Dim crossColours(3) As FaceColour
        For edge = 0 To 3
            crossColours(edge) = crossEdges(edge).SideColour(cube.TopFace)
        Next

        If crossColours(0) = Opposite(crossColours(2)) Or crossColours(1) = Opposite(crossColours(3)) Then

            For faceColour = 1 To 4
                If crossColours(0) = cube.FaceColours(faceColour) And crossColours(1) = cube.FaceColours(faceColour Mod 4 + 1) Then Return 'edges are correct
            Next

            Algorithms.BottomEdgeAlgorithmRight(cube)
            cube.RotateFace(cube.TopFace, Direction.ANTICLOCKWISE)
            Algorithms.BottomEdgeAlgorithmRight(cube)

            Return
        End If

        Dim indexOfAnticlockwiseEdge As Integer

        For edge = 0 To 3
            For faceColour = 1 To 4
                If crossColours(edge) = cube.FaceColours(faceColour) And
                    crossColours((edge + 1) Mod 4) = cube.FaceColours(faceColour Mod 4 + 1) Then
                    indexOfAnticlockwiseEdge = edge
                End If
            Next
        Next

        cube.RotateFace(cube.TopFace, indexOfAnticlockwiseEdge - 1)
        Algorithms.BottomEdgeAlgorithmRight(cube)
    End Sub

    Private Sub DoCorners()
        PositionBottomCorners()
        RotateBottomFaceCorrectly()
        OrientateBottomCorners()
    End Sub

    Private Sub PositionBottomCorners()

        RotateBottomFaceCorrectly()
        cube.RotateFaceToTop(cube.BottomFace)

        Dim corners() As Corner = cube.TopCornersClockwise
        Dim correctlyPositionedCorner As Corner
        Dim nextClockwiseCorner As Corner = Nothing
        Dim cornerPositionIndex As Integer

        For cornerPositionIndex = 0 To 3
            Dim corner As Corner = corners(cornerPositionIndex)
            nextClockwiseCorner = corners((cornerPositionIndex + 1) Mod 4)
            If Not CornerIsCorrectlyPositioned(corner) Then Continue For
            If CornerIsCorrectlyPositioned(nextClockwiseCorner) Then
                cube.RotateFaceToTop(cube.BottomFace)
                Return
            Else
                correctlyPositionedCorner = corner
                Exit For
            End If
        Next

        If correctlyPositionedCorner Is Nothing Then
            Algorithms.BottomClockwiseCornerAlgorithm(cube)
            cube.RotateFaceToTop(cube.BottomFace)
            PositionBottomCorners()
            Return
        End If

        PermuteCorners(correctlyPositionedCorner, nextClockwiseCorner, cornerPositionIndex)

        cube.RotateFaceToTop(cube.BottomFace)
    End Sub

    Private Function CornerIsCorrectlyPositioned(ByVal corner As Corner) As Boolean
        Dim sideFaces() As FaceColour = FacesEitherSideOfCorner(corner)
        Return corner.HasColour(sideFaces(0)) And corner.HasColour(sideFaces(1))
    End Function

    Private Sub PermuteCorners(correctlyPositionedCorner As Corner, nextClockwiseCorner As Corner, cornerPositionIndex As Integer)

        Dim anticlockwiseFace As FaceColour = FacesEitherSideOfCorner(correctlyPositionedCorner)(0)
        If nextClockwiseCorner.HasColour(anticlockwiseFace) Then
            'Rotate anticlockwise
            Select Case cornerPositionIndex
                Case 1 : cube.RotateFace(cube.TopFace, Direction.ANTICLOCKWISE)
                Case 2 : cube.RotateFace(cube.TopFace, Direction.HALF_TURN)
                Case 3 : cube.RotateFace(cube.TopFace, Direction.CLOCKWISE)
            End Select
            Algorithms.BottomAnticlockwiseCornerAlgorithm(cube)
        Else
            'Rotate clockwise
            Select Case cornerPositionIndex
                Case 0 : cube.RotateFace(cube.TopFace, Direction.ANTICLOCKWISE)
                Case 1 : cube.RotateFace(cube.TopFace, Direction.HALF_TURN)
                Case 2 : cube.RotateFace(cube.TopFace, Direction.CLOCKWISE)
            End Select
            Algorithms.BottomClockwiseCornerAlgorithm(cube)
        End If

    End Sub

    'returns {anticlockwise, clockwise} viewed from top
    Private Function FacesEitherSideOfCorner(ByVal cubie As Corner) As FaceColour()
        Dim faces() As FaceColour = cube.FaceColours
        Select Case (cubie.Position.x + cubie.Position.z)
            Case -2 'Back left corner
                Return {faces(MoveFaces.LEFT), faces(MoveFaces.BACK)}
            Case 0
                If cubie.Position.x = -1 Then ' Front left corner
                    Return {faces(MoveFaces.FRONT), faces(MoveFaces.LEFT)}
                ElseIf cubie.Position.x = 1 Then ' Back right corner
                    Return {faces(MoveFaces.BACK), faces(MoveFaces.RIGHT)}
                End If
            Case 2 ' Front right corner
                Return {faces(MoveFaces.RIGHT), faces(MoveFaces.FRONT)}
        End Select
        Throw New ArgumentException("corner position is invalid")
    End Function

    Private Sub OrientateBottomCorners()

        cube.RotateFaceToTop(cube.BottomFace)

        Dim numberOfIncorrectCorners As Integer = cube.IncorrectTopCorners.Length

        Select Case numberOfIncorrectCorners
            Case 4
                Orientate4Corners()
            Case 2
                Orientate2Corners()
            Case 3
                Orientate3Corners()
            Case 0
                Return
            Case Else
                Throw New StageNotSuccessfulException("Error in previous stages - invalid cube")
        End Select

        cube.RotateFaceToTop(cube.BottomFace)
    End Sub

    Private Sub Orientate4Corners()
        Dim originalOrientation As CubeOrientation = New CubeOrientation(cube.CurrentOrientation)

        Dim incorrectCorners() As Corner = cube.IncorrectTopCorners()
        Dim facesCommonToBoth() As FaceColour
        Dim faceBothRotatedTowards As FaceColour

        For i = 0 To 3
            'will only work if incorrectCorners is clockwise
            facesCommonToBoth = GetFacesContainingBothCorners(incorrectCorners(i), incorrectCorners(i + 1))
            faceBothRotatedTowards = GetFaceBothRotatedTowards(incorrectCorners(i), incorrectCorners(i + 1))
            If faceBothRotatedTowards <> FaceColour.None Then Exit For
        Next

        cube.RotateFaceToTop(faceBothRotatedTowards)
        If faceBothRotatedTowards = facesCommonToBoth(0) Then
            cube.RotateFaceToLeft(facesCommonToBoth(1))
        ElseIf faceBothRotatedTowards = facesCommonToBoth(1) Then
            cube.RotateFaceToLeft(facesCommonToBoth(0))
        Else
            Throw New StageNotSuccessfulException()
        End If

        Algorithms.BottomEdgeAlgorithmRight(cube)
        Algorithms.BottomEdgeAlgorithmLeft(cube)

        cube.RotateFaceToTop(originalOrientation.Top)
        cube.RotateFaceToFront(originalOrientation.Front)

        cube.RotateFaceToTop(cube.BottomFace)
        OrientateBottomCorners()
        cube.RotateFaceToTop(cube.BottomFace)
    End Sub

    Private Sub Orientate2Corners()
        Dim originalOrientation As CubeOrientation = New CubeOrientation(cube.CurrentOrientation)

        Dim correctCorners() As Corner = cube.CorrectTopCorners()

        If correctCorners.Length <> 2 Then Throw New InvalidOperationException

        If CornersAreOpposite(correctCorners(0), correctCorners(1)) Then OrientateOppositeCorners()

        Dim incorrectCorners() As Corner = cube.IncorrectTopCorners()
        Dim facesCommonToBoth() As FaceColour = GetFacesContainingBothCorners(incorrectCorners(0), incorrectCorners(1))
        Dim faceBothRotatedTowards As FaceColour = GetFaceBothRotatedTowards(incorrectCorners(0), incorrectCorners(1))

        cube.RotateFaceToTop(faceBothRotatedTowards)
        If faceBothRotatedTowards = facesCommonToBoth(0) Then
            cube.RotateFaceToLeft(facesCommonToBoth(1))
        ElseIf faceBothRotatedTowards = facesCommonToBoth(1) Then
            cube.RotateFaceToLeft(facesCommonToBoth(0))
        Else
            Throw New StageNotSuccessfulException()
        End If

        Algorithms.BottomEdgeAlgorithmRight(cube)
        Algorithms.BottomEdgeAlgorithmLeft(cube)

        cube.RotateFaceToTop(originalOrientation.Top)
        cube.RotateFaceToFront(originalOrientation.Front)

    End Sub

    Private Function CornersAreOpposite(ByVal corner1 As Corner, ByVal corner2 As Corner) As Boolean
        Return ((corner1.Position.x + corner1.Position.z) - (corner2.Position.x + corner2.Position.z)) Mod 4 = 0
    End Function


    Private Sub OrientateOppositeCorners()

        Dim bottomFaceColour As FaceColour = cube.TopFace
        Dim incorrectCorners() As Corner = cube.IncorrectTopCorners()

        cube.Rotate(Direction.ANTICLOCKWISE, Axis.Z)
        Dim cornerOnTopFace As Corner
        Do
            cube.Rotate(Direction.ANTICLOCKWISE, Axis.X)
            cornerOnTopFace = IIf(incorrectCorners(0).OnFace(cube.TopFace, cube), incorrectCorners(0), incorrectCorners(1))
        Loop While Not cornerOnTopFace.CorrectRotation(bottomFaceColour, cube.TopFace)
        Algorithms.BottomEdgeAlgorithmRight(cube)
        Algorithms.BottomEdgeAlgorithmLeft(cube)
        cube.Rotate(Direction.CLOCKWISE, Axis.Z)

    End Sub

    Private Function GetFaceBothRotatedTowards(ByVal corner1 As Corner, ByVal corner2 As Corner) As FaceColour
        Dim face As FaceColour = FaceColour.None
        If corner1.PrimaryFace = corner2.PrimaryFace And corner1.Rotation = corner2.Rotation Then
            face = corner1.Rotation
        ElseIf corner1.SecondaryFace = corner2.SecondaryFace And corner1.SecondaryRotation = corner2.SecondaryRotation Then
            face = corner1.SecondaryRotation
        Else
            Dim corner1Faces() As FaceColour = FacesEitherSideOfCorner(corner1)
            corner1Faces.Append(cube.TopFace)
            'probably needs an except method written
            face = corner1Faces.Except({corner1.Rotation, corner1.SecondaryRotation})(0)
        End If
        If corner1.OnFace(face, cube) And corner2.OnFace(face, cube) Then Return face
        Return FaceColour.None
    End Function

    Private Function GetFacesContainingBothCorners(ByVal corner1 As Corner, ByVal corner2 As Corner) As FaceColour()
        Dim corner1Faces() As FaceColour = FacesEitherSideOfCorner(corner1)
        Dim corner2Faces() As FaceColour = FacesEitherSideOfCorner(corner2)
        Dim commonFaces() As FaceColour = corner1Faces.Intersect(corner2Faces)
        commonFaces.Append(cube.TopFace)
        Return commonFaces
    End Function



    Private Sub Orientate3Corners()

        cube.Rotate(Direction.ANTICLOCKWISE, Axis.Z)

        Dim leftTopEdgeCorners(1) As Corner
        Dim count As Integer

        Do
            cube.Rotate(Direction.CLOCKWISE, Axis.X)
            count = 0
            For Each corner In cube.Corners
                If corner.Position = New Vector3x1(-1, 1, 1) Or corner.Position = New Vector3x1(-1, 1, -1) Then
                    leftTopEdgeCorners(count) = corner
                    count += 1
                End If
            Next
        Loop While (leftTopEdgeCorners(0).Correct(cube) Or leftTopEdgeCorners(1).Correct(cube))

        Algorithms.BottomEdgeAlgorithmRight(cube)
        Algorithms.BottomEdgeAlgorithmLeft(cube)

        cube.Rotate(Direction.CLOCKWISE, Axis.Z)
        If cube.CorrectTopCorners.Length <> 4 Then
            cube.RotateFaceToTop(cube.BottomFace)
            OrientateBottomCorners()
        End If

        cube.RotateFaceToTop(cube.BottomFace)

    End Sub

    Private Sub RotateBottomFaceCorrectly()

        cube.RotateFaceToTop(cube.BottomFace)
        Dim edgesClockwise As Edge() = cube.TopEdgesClockwise
        Dim offset As Integer = 0
        For i As MoveFaces = MoveFaces.LEFT To MoveFaces.FRONT
            If edgesClockwise(i - 1).HasColour(cube.FaceColours(MoveFaces.LEFT)) Then
                offset = i - 1
                Exit For
            End If
        Next
        If offset = 3 Then offset = -1
        cube.RotateFace(cube.TopFace, offset)
        cube.RotateFaceToTop(cube.BottomFace)

    End Sub

End Class

