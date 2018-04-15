Imports RubiksCubeSolver_v2_0.Helpers.PublicConstants
Imports RubiksCubeSolver_v2_0.Helpers.PublicConstants.Movecube.FaceColours
Imports RubiksCubeSolver_v2_0.Helpers.PublicConstants.Direction
Module Algorithms

    Public Sub MoveTopCornerPointingDownwardsFromBottomToTop(ByVal clockwiseFace As FaceColour, ByVal anticlockwiseFace As FaceColour, ByRef cube As Cube)
        cube.RotateFace(anticlockwiseFace, Direction.ANTICLOCKWISE)
        cube.RotateFace(cube.BottomFace, Direction.CLOCKWISE)
        cube.RotateFace(anticlockwiseFace, Direction.CLOCKWISE)
        cube.RotateFace(clockwiseFace, Direction.CLOCKWISE)
        cube.RotateFace(cube.BottomFace, Direction.HALF_TURN)
        cube.RotateFace(clockwiseFace, Direction.ANTICLOCKWISE)
    End Sub

    Public Sub MoveTopCornerPointingLeftFromBottomToTop(ByVal anticlockwiseFace As FaceColour, ByRef cube As Cube)
        cube.RotateFace(cube.BottomFace, Direction.ANTICLOCKWISE)
        cube.RotateFace(anticlockwiseFace, Direction.ANTICLOCKWISE)
        cube.RotateFace(cube.BottomFace, Direction.CLOCKWISE)
        cube.RotateFace(anticlockwiseFace, Direction.CLOCKWISE)
    End Sub

    Public Sub MoveTopCornerPointingRightFromBottomToTop(ByVal clockwiseFace As FaceColour, ByRef cube As Cube)
        cube.RotateFace(cube.BottomFace, Direction.CLOCKWISE)
        cube.RotateFace(clockwiseFace, Direction.CLOCKWISE)
        cube.RotateFace(cube.BottomFace, Direction.ANTICLOCKWISE)
        cube.RotateFace(clockwiseFace, Direction.ANTICLOCKWISE)
    End Sub

    Public Sub MiddleLeftAlgorithm(ByRef cube As Cube)
        cube.RotateFace(cube.FaceColours(MoveFaces.BOTTOM), CLOCKWISE)
        cube.RotateFace(cube.FaceColours(MoveFaces.LEFT), CLOCKWISE)
        cube.RotateFace(cube.FaceColours(MoveFaces.BOTTOM), ANTICLOCKWISE)
        cube.RotateFace(cube.FaceColours(MoveFaces.LEFT), ANTICLOCKWISE)
        cube.RotateFace(cube.FaceColours(MoveFaces.BOTTOM), ANTICLOCKWISE)
        cube.RotateFace(cube.FaceColours(MoveFaces.FRONT), ANTICLOCKWISE)
        cube.RotateFace(cube.FaceColours(MoveFaces.BOTTOM), CLOCKWISE)
        cube.RotateFace(cube.FaceColours(MoveFaces.FRONT), CLOCKWISE)
    End Sub

    Public Sub MiddleRightAlgorithm(ByRef cube As Cube)
        cube.RotateFace(cube.FaceColours(MoveFaces.BOTTOM), ANTICLOCKWISE)
        cube.RotateFace(cube.FaceColours(MoveFaces.RIGHT), ANTICLOCKWISE)
        cube.RotateFace(cube.FaceColours(MoveFaces.BOTTOM), CLOCKWISE)
        cube.RotateFace(cube.FaceColours(MoveFaces.RIGHT), CLOCKWISE)
        cube.RotateFace(cube.FaceColours(MoveFaces.BOTTOM), CLOCKWISE)
        cube.RotateFace(cube.FaceColours(MoveFaces.FRONT), CLOCKWISE)
        cube.RotateFace(cube.FaceColours(MoveFaces.BOTTOM), ANTICLOCKWISE)
        cube.RotateFace(cube.FaceColours(MoveFaces.FRONT), ANTICLOCKWISE)
    End Sub

    Public Sub BottomCrossAlgorithm(ByRef cube As Cube)
        cube.RotateFace(cube.FaceColours(MoveFaces.FRONT), CLOCKWISE)
        cube.RotateFace(cube.FaceColours(MoveFaces.RIGHT), CLOCKWISE)
        cube.RotateFace(cube.FaceColours(MoveFaces.TOP), CLOCKWISE)
        cube.RotateFace(cube.FaceColours(MoveFaces.RIGHT), ANTICLOCKWISE)
        cube.RotateFace(cube.FaceColours(MoveFaces.TOP), ANTICLOCKWISE)
        cube.RotateFace(cube.FaceColours(MoveFaces.FRONT), ANTICLOCKWISE)
    End Sub

    Public Sub BottomEdgeAlgorithmRight(ByRef cube As Cube)
        cube.RotateFace(cube.FaceColours(MoveFaces.RIGHT), CLOCKWISE)
        cube.RotateFace(cube.FaceColours(MoveFaces.TOP), CLOCKWISE)
        cube.RotateFace(cube.FaceColours(MoveFaces.RIGHT), ANTICLOCKWISE)
        cube.RotateFace(cube.FaceColours(MoveFaces.TOP), CLOCKWISE)
        cube.RotateFace(cube.FaceColours(MoveFaces.RIGHT), CLOCKWISE)
        cube.RotateFace(cube.FaceColours(MoveFaces.TOP), HALF_TURN)
        cube.RotateFace(cube.FaceColours(MoveFaces.RIGHT), ANTICLOCKWISE)
    End Sub

    Public Sub BottomEdgeAlgorithmLeft(ByRef cube As Cube)
        cube.RotateFace(cube.FaceColours(MoveFaces.LEFT), ANTICLOCKWISE)
        cube.RotateFace(cube.FaceColours(MoveFaces.TOP), ANTICLOCKWISE)
        cube.RotateFace(cube.FaceColours(MoveFaces.LEFT), CLOCKWISE)
        cube.RotateFace(cube.FaceColours(MoveFaces.TOP), ANTICLOCKWISE)
        cube.RotateFace(cube.FaceColours(MoveFaces.LEFT), ANTICLOCKWISE)
        cube.RotateFace(cube.FaceColours(MoveFaces.TOP), HALF_TURN)
        cube.RotateFace(cube.FaceColours(MoveFaces.LEFT), CLOCKWISE)
    End Sub

    Public Sub BottomAnticlockwiseCornerAlgorithm(ByRef cube As Cube)
        cube.RotateFace(cube.FaceColours(MoveFaces.RIGHT), CLOCKWISE)
        cube.RotateFace(cube.FaceColours(MoveFaces.TOP), ANTICLOCKWISE)
        cube.RotateFace(cube.FaceColours(MoveFaces.LEFT), ANTICLOCKWISE)
        cube.RotateFace(cube.FaceColours(MoveFaces.TOP), CLOCKWISE)
        cube.RotateFace(cube.FaceColours(MoveFaces.RIGHT), ANTICLOCKWISE)
        cube.RotateFace(cube.FaceColours(MoveFaces.TOP), ANTICLOCKWISE)
        cube.RotateFace(cube.FaceColours(MoveFaces.LEFT), CLOCKWISE)
    End Sub

    Public Sub BottomClockwiseCornerAlgorithm(ByRef cube As Cube)
        cube.RotateFace(cube.FaceColours(MoveFaces.LEFT), ANTICLOCKWISE)
        cube.RotateFace(cube.FaceColours(MoveFaces.TOP), CLOCKWISE)
        cube.RotateFace(cube.FaceColours(MoveFaces.RIGHT), CLOCKWISE)
        cube.RotateFace(cube.FaceColours(MoveFaces.TOP), ANTICLOCKWISE)
        cube.RotateFace(cube.FaceColours(MoveFaces.LEFT), CLOCKWISE)
        cube.RotateFace(cube.FaceColours(MoveFaces.TOP), CLOCKWISE)
        cube.RotateFace(cube.FaceColours(MoveFaces.RIGHT), ANTICLOCKWISE)
    End Sub
End Module