Imports RubiksCubeSolver_v2_0.Helpers

Public Class TopLayerSolver

    Private cube As Cube

    Sub New(ByRef cubeToBeSolved As Cube)
        cube = cubeToBeSolved
    End Sub

    Public Sub Solve()

        Dim colourOfCompletedFace As FaceColour = FaceColour.None
        colourOfCompletedFace = GetAnyCompleteFace()

        If colourOfCompletedFace = FaceColour.None Then
            Dim colourOfRotatedCompletedFace As FaceColour = GetAnyRotatedCompleteFace()
            If colourOfRotatedCompletedFace = FaceColour.None Then
                DoTopFace()
            Else
                cube.RotateFaceToTop(colourOfRotatedCompletedFace)
            End If
            RotateTopFaceCorrectly()
        Else
            cube.RotateFaceToTop(colourOfCompletedFace)
        End If

    End Sub

    Private Sub DoTopFace()
        Dim colourOfCross As FaceColour = FaceColour.None
        colourOfCross = GetAnyCrossWithCorrectEdges()
        If colourOfCross = FaceColour.None Then
            DoTopCross()
        Else
            cube.RotateFaceToTop(colourOfCross)
        End If
        DoTopCorners()
    End Sub

    Private Sub DoTopCross()
        MakeWrongEdgedCrossOnBottomFace()
        MakeCorrectCrossFromOppositeCross()
    End Sub

    Private Sub DoTopCorners()
        Dim topCornersWhenSolved() As Corner = cube.BlocksByColour(cube.TopFace).Extract(Of Corner)()
        For Each corner In topCornersWhenSolved
            RotateTopFaceCorrectly()
            If Not corner.Correct(cube) Then PutCornerIntoCorrectPlaceOnTopFace(corner)
        Next
    End Sub

    Private Sub MakeWrongEdgedCrossOnBottomFace()
        Dim topColourEdges() As Edge = cube.BlocksByColour(cube.TopFace).Extract(Of Edge)()
        cube.RotateFaceToTop(cube.BottomFace)
        For Each edge In topColourEdges
            If Not edge.CorrectlyOnOppositeFace(cube.BottomFace, cube) Then PutEdgeIntoEmptySpaceOnTopFace(edge, cube.BottomFace)
        Next
        cube.RotateFaceToTop(cube.BottomFace)
    End Sub

    Private Sub PutEdgeIntoEmptySpaceOnTopFace(ByVal edge As Edge, ByVal crossColour As FaceColour)
        Dim edgeBeingDisplaced As Edge = GetEdgeBeingDisplaced(edge)

        While edgeBeingDisplaced.CorrectlyOnOppositeFace(crossColour, cube) And edgeBeingDisplaced.HasColour(crossColour)
            cube.RotateFace(cube.TopFace, Direction.CLOCKWISE)
            edgeBeingDisplaced = GetEdgeBeingDisplaced(edge)
        End While

        Select Case edge.Layer
            Case Layer.TOP
                RotateForwardsTopLayerEdgeToTop(edge)
            Case Layer.MIDDLE
                RotateMiddleLayerEdgeToTop(edge)
            Case Layer.BOTTOM
                If edge.CorrectRotation(crossColour, cube.BottomFace) Then
                    RotateDownwardsBottomLayerEdgeToTop(edge)
                Else
                    RotateForwardsBottomLayerEdgeToTop(edge)
                End If
        End Select
    End Sub

    Private Function GetEdgeBeingDisplaced(ByVal edgeBeingMoved As Edge)

        Dim possibleEdges() As Edge = cube.TopEdgesClockwise

        For Each edge In possibleEdges
            If edge.OnFace(FaceToRotateForCrossEdge(edgeBeingMoved), cube) Then Return edge
        Next

        Throw New Exception("Didn't return an edge being displaced - code error")
    End Function

    Private Sub RotateForwardsTopLayerEdgeToTop(edge As Edge)
        If edge.Layer <> Layer.TOP Then Throw New ArgumentException("The cubie is not on the top layer.")
        cube.RotateFace(FaceToRotateForCrossEdge(edge), Direction.CLOCKWISE)
        cube.RotateFace(cube.TopFace, Direction.ANTICLOCKWISE)
        RotateMiddleLayerEdgeToTop(edge)
    End Sub

    Private Sub RotateMiddleLayerEdgeToTop(edge As Edge)
        If edge.Layer <> Layer.MIDDLE Then Throw New ArgumentException("The cubie is not on the middle layer.")

        Dim faceToRotate As FaceColour = FaceToRotateForCrossEdge(edge)
        Dim direction As Direction = DirectionForMiddleLayerEdgeToTop(edge, faceToRotate, cube)
        cube.RotateFace(faceToRotate, direction)
    End Sub

    Private Function DirectionForMiddleLayerEdgeToTop(ByVal cubie As Edge, ByVal faceToRotate As FaceColour, ByVal cube As Cube) As Direction
        Dim faces() As FaceColour = cube.FaceColours
        Select Case faceToRotate
            Case faces(MoveFaces.LEFT)
                Return cubie.Position.z
            Case faces(MoveFaces.BACK)
                Return -cubie.Position.x
            Case faces(MoveFaces.RIGHT)
                Return -cubie.Position.z
            Case faces(MoveFaces.FRONT)
                Return cubie.Position.x
            Case Else
                Return Direction.NO_CHANGE
        End Select
    End Function

    Private Sub RotateDownwardsBottomLayerEdgeToTop(ByVal edge As Edge)
        If edge.Layer <> Layer.BOTTOM Then Throw New ArgumentException("The cubie is not on the bottom layer.")

        Dim faceToRotate As FaceColour = FaceToRotateForCrossEdge(edge)
        cube.RotateFace(faceToRotate, Direction.HALF_TURN)
    End Sub

    Private Sub RotateForwardsBottomLayerEdgeToTop(ByVal edge As Edge)
        If edge.Layer <> Layer.BOTTOM Then Throw New ArgumentException("The cubie is not on the bottom layer.")
        cube.RotateFace(cube.TopFace, Direction.CLOCKWISE)
        Dim originalFace As FaceColour = FaceToRotateForCrossEdge(edge)
        cube.RotateFace(originalFace, Direction.CLOCKWISE)
        RotateMiddleLayerEdgeToTop(edge)
        cube.RotateFace(originalFace, Direction.ANTICLOCKWISE)
    End Sub

    Private Function FaceToRotateForCrossEdge(ByVal cubie As Edge) As FaceColour
        Return IIf(cubie.Layer = Layer.MIDDLE, FaceToRotateForMiddleLayerEdge(cubie, cube.BottomFace),
                   FaceToRotateForNotMiddleLayerEdge(cubie))
    End Function

    Private Function FaceToRotateForMiddleLayerEdge(ByVal cubie As Edge, ByVal colourThatWillPointUp As FaceColour) As FaceColour
        Dim faces() As FaceColour = cube.FaceColours
        If cubie.PrimaryFace <> colourThatWillPointUp Then Return cubie.Rotation

        Dim faceOne, faceTwo As FaceColour
        Select Case (cubie.Position.x + cubie.Position.z)
            Case -2 'Back left edge
                faceOne = faces(MoveFaces.LEFT)
                faceTwo = faces(MoveFaces.BACK)
            Case 0
                If cubie.Position.x = -1 Then ' Front left edge
                    faceOne = faces(MoveFaces.FRONT)
                    faceTwo = faces(MoveFaces.LEFT)
                ElseIf cubie.Position.x = 1 Then ' Back right edge
                    faceOne = faces(MoveFaces.BACK)
                    faceTwo = faces(MoveFaces.RIGHT)
                End If
            Case 2 ' Front right edge
                faceOne = faces(MoveFaces.RIGHT)
                faceTwo = faces(MoveFaces.FRONT)
        End Select
        Return IIf(faceOne = cubie.Rotation, faceTwo, faceOne)
    End Function

    Private Function FaceToRotateForNotMiddleLayerEdge(ByVal cubie As Edge) As FaceColour
        Dim faces() As FaceColour = cube.FaceColours
        Select Case cubie.Position.x
            Case -1 ' left edge cubie
                Return faces(MoveFaces.LEFT)
            Case 0
                If cubie.Position.z = -1 Then ' back edge cubie
                    Return faces(MoveFaces.BACK)
                Else ' front edge cubie
                    Return faces(MoveFaces.FRONT)
                End If
            Case 1 ' right edge cubie
                Return faces(MoveFaces.RIGHT)
        End Select
        Throw New ArgumentException("edge.position is invalid")
    End Function

    Private Sub MakeCorrectCrossFromOppositeCross()

        Dim crossEdges() As Edge = cube.BlocksByColour(cube.TopFace).Extract(Of Edge)()

        For Each edge In crossEdges
            Dim edgeSideColour As FaceColour = edge.SideColour(cube.TopFace)

            While Not edge.OnFace(edgeSideColour, cube)
                cube.RotateFace(cube.BottomFace, Direction.ANTICLOCKWISE)
            End While
            cube.RotateFace(edgeSideColour, Direction.HALF_TURN)
        Next

    End Sub


    Private Sub PutCornerIntoCorrectPlaceOnTopFace(ByVal corner As Corner)
        RotateTopFaceCorrectly()

        Select Case corner.Layer
            Case Layer.TOP
                If corner.Correct(cube) Then Return
                Dim faceCornerIsOn As FaceColour = FacesEitherSideOfCorner(corner)(0)
                cube.RotateFace(faceCornerIsOn, Direction.ANTICLOCKWISE)
                cube.RotateFace(cube.BottomFace, Direction.HALF_TURN)
                cube.RotateFace(faceCornerIsOn, Direction.CLOCKWISE)
                RotateCornerFromBottomLayerToTop(corner)
            Case Layer.BOTTOM
                RotateCornerFromBottomLayerToTop(corner)
            Case Else
                Throw New ArgumentException("corner is not on top or bottom layer")
        End Select
    End Sub

    Private Sub RotateCornerFromBottomLayerToTop(ByVal corner As Corner)

        RotateBottomFaceSoCornerLinesUpWithTopCross(corner)

        Dim facesToRotate() As FaceColour = FacesEitherSideOfCorner(corner)
        'when viewed from topFace
        Dim anticlockwiseFace As FaceColour = facesToRotate(0)
        Dim clockwiseFace As FaceColour = facesToRotate(1)

        If TopCornerFacingDownwards(corner) Then
            Algorithms.MoveTopCornerPointingDownwardsFromBottomToTop(clockwiseFace, anticlockwiseFace, cube)
        Else
            Dim faceRotatedTowards As FaceColour = FaceBottomCornerIsRotatedTowards(corner, cube.TopFace)
            If faceRotatedTowards = clockwiseFace Then
                Algorithms.MoveTopCornerPointingLeftFromBottomToTop(anticlockwiseFace, cube)
            ElseIf faceRotatedTowards = anticlockwiseFace Then
                Algorithms.MoveTopCornerPointingRightFromBottomToTop(clockwiseFace, cube)
            End If
        End If

    End Sub

    Private Function TopCornerFacingDownwards(ByVal corner As Corner)
        Return corner.CorrectlyOnOppositeFace(cube.TopFace, cube)
    End Function

    Private Sub RotateBottomFaceSoCornerLinesUpWithTopCross(ByVal corner As Corner)

        Dim cornerSideFaces() As FaceColour = corner.SideColours(cube.TopFace)
        Dim cubeSideFaces() As FaceColour = FacesEitherSideOfCorner(corner)
        Do Until (cubeSideFaces(0) = cornerSideFaces(0) And cubeSideFaces(1) = cornerSideFaces(1)) Or
            (cubeSideFaces(0) = cornerSideFaces(1) And cubeSideFaces(1) = cornerSideFaces(0))
            cube.RotateFace(cube.BottomFace, Direction.ANTICLOCKWISE)
            cubeSideFaces = FacesEitherSideOfCorner(corner)
        Loop
    End Sub

    Private Function FaceBottomCornerIsRotatedTowards(ByVal corner As Corner, ByVal mainColour As FaceColour) As FaceColour
        If mainColour = corner.PrimaryFace Then
            Return corner.Rotation
        ElseIf mainColour = corner.SecondaryFace Then
            Return corner.SecondaryRotation
        Else
            Dim sideFaces() As FaceColour = FacesEitherSideOfCorner(corner)
            Dim facesAroundCorner() As FaceColour = {sideFaces(0), sideFaces(1), cube.BottomFace}
            For Each face In facesAroundCorner
                If face.ToString() = corner.Rotation Or face.ToString() = corner.SecondaryRotation Then Continue For
                Return face
            Next
        End If
        Throw New ArgumentException("error in corner")
    End Function

    'returns {anticlockwise, clockwise}
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
        Throw New ArgumentException("corner.position is invalid")
    End Function

    Private Sub RotateTopFaceCorrectly()

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

    End Sub

    ''' <summary>
    ''' Gets the colour of the first complete face. If non complete, returns FaceColour.None
    ''' </summary>
    Private Function GetAnyCompleteFace() As FaceColour

        For face As FaceColour = Helpers.FaceColour.W To Helpers.FaceColour.O
            Dim faceBlocks As Block() = cube.BlocksByColour(face)
            For Each block In faceBlocks
                If Not block.Correct(cube) Then Exit For 'check next face
                If faceBlocks.IsLastElement(block) Then Return face
            Next
        Next
        Return FaceColour.None

    End Function

    ''' <summary>
    ''' Gets the colour of the first complete face regardless of it's rotation. If non complete, returns FaceColour.None
    ''' </summary>
    Private Function GetAnyRotatedCompleteFace() As FaceColour

        For face As FaceColour = Helpers.FaceColour.W To Helpers.FaceColour.O
            Dim faceBlocks As Block() = cube.BlocksByColour(face)
            For Each block In faceBlocks
                If Not block.CorrectForFace(face, cube) Or Not EdgesOfFaceInRightOrder(face) Then Exit For 'check next face
                If faceBlocks.IsLastElement(block) Then Return face
            Next
        Next
        Return FaceColour.None

    End Function

    Private Function EdgesOfFaceInRightOrder(ByVal face As FaceColour) As Boolean
        Dim originalTop As FaceColour = cube.CurrentOrientation.Top
        cube.RotateFaceToTop(face)

        Dim faceBlocksClockwise As Block() = cube.TopBlocksClockwise
        ReDim Preserve faceBlocksClockwise(7) ' removes middle block
        Dim edgesClockwise As Edge() = faceBlocksClockwise.Extract(Of Edge)()

        For i As MoveFaces = MoveFaces.LEFT To MoveFaces.FRONT
            If edgesClockwise(i - 1).HasColour(cube.FaceColours(MoveFaces.LEFT)) Then
                edgesClockwise = edgesClockwise.Rotate(i - 1)
                faceBlocksClockwise = faceBlocksClockwise.Rotate(2 * i - 2)
                Exit For
            End If
        Next
        For i = MoveFaces.LEFT To MoveFaces.FRONT
            If Not faceBlocksClockwise(2 * i - 1).HasColour(cube.FaceColours(i)) Or
                Not faceBlocksClockwise(2 * i - 2).HasColour(cube.FaceColours(i)) Or
                Not faceBlocksClockwise((2 * i) Mod 8).HasColour(cube.FaceColours(i)) Then
                cube.RotateFaceToTop(originalTop)
                Return False
            End If
        Next

        cube.RotateFaceToTop(originalTop)
        Return True
    End Function


    ''' <summary>
    ''' Gets the colour of the first complete cross. If non complete, returns nothing.
    ''' </summary>
    Private Function GetAnyCrossWithCorrectEdges() As FaceColour

        For face As FaceColour = Helpers.FaceColour.W To Helpers.FaceColour.O
            Dim faceEdges As Edge() = cube.BlocksByColour(face).Extract(Of Edge)()
            For Each edgeCubie In faceEdges

                If Not edgeCubie.CorrectForFace(face, cube) Or
                    (Not edgeCubie.Correct(cube) And Not EdgesOfCrossInRightOrder(face)) Then Exit For 'check next face
                If faceEdges.IsLastElement(edgeCubie) Then Return face
            Next
        Next
        Return FaceColour.None

    End Function

    Private Function EdgesOfCrossInRightOrder(ByVal crossFace As FaceColour) As Boolean

        Dim originalTop As FaceColour = cube.CurrentOrientation.Top
        cube.RotateFaceToTop(crossFace)
        Dim edgesClockwise As Edge() = cube.TopEdgesClockwise

        For i As MoveFaces = MoveFaces.LEFT To MoveFaces.FRONT
            If edgesClockwise(i - 1).HasColour(MoveFaces.LEFT) Then
                edgesClockwise = edgesClockwise.Rotate(i - 1)
                Exit For
            End If
        Next
        For i = MoveFaces.LEFT To MoveFaces.FRONT
            If Not edgesClockwise(i - 1).HasColour(cube.FaceColours(i)) Then
                cube.RotateFaceToTop(originalTop)
                Return False
            End If
        Next
        cube.RotateFaceToTop(originalTop)
        Return True

    End Function

End Class
