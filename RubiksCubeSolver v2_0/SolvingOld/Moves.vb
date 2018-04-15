Imports RubiksCubeSolver_v2_0.MyPublic.PublicConstants

Module Moves

End Module



'Public Sub RotateFaceToLeft(ByVal newLeft As MyPublic.FaceColours, ByRef cube As Cube, ByRef Instructions As String)
'    Dim faces() As Char = cube.FaceChars
'    Dim direction As Direction
'    Dim axis As Axis

'    Select Case newLeft.ToString
'        Case faces(MoveFaces.TOP) ' current top
'            axis = Axis.Z
'            direction = Direction.ANTICLOCKWISE
'            cube.CurrentOrientation.Top = faces(MoveFaces.RIGHT)
'        Case faces(MoveFaces.LEFT) ' current left
'            axis = Axis.Z
'            direction = Direction.NO_CHANGE
'        Case faces(MoveFaces.BACK) ' current back
'            axis = Axis.Y
'            direction = Direction.ANTICLOCKWISE
'            cube.CurrentOrientation.Front = faces(MoveFaces.LEFT)
'        Case faces(MoveFaces.RIGHT) ' current right
'            axis = Axis.Y
'            direction = Direction.HALF_TURN
'            cube.CurrentOrientation.Front = faces(MoveFaces.BACK)
'        Case faces(MoveFaces.FRONT) ' current front
'            axis = Axis.Y
'            direction = Direction.CLOCKWISE
'            cube.CurrentOrientation.Front = faces(MoveFaces.RIGHT)
'        Case faces(MoveFaces.BOTTOM) ' current bottom
'            axis = Axis.Z
'            direction = Direction.CLOCKWISE
'            cube.CurrentOrientation.Top = faces(MoveFaces.TOP)
'    End Select

'    RotateCube(direction, axis, cube, Instructions)

'End Sub

'''' <summary>
'''' Rotates the cube so that a specific face is on front
'''' </summary>
'Public Sub RotateFaceToFront(ByVal newFront As MyPublic.FaceColours, ByRef cube As Cube, ByRef Instructions As String)
'    Dim faces() As Char = cube.FaceChars
'    Dim direction As Direction
'    Dim axis As Axis

'    Select Case newFront.ToString
'        Case faces(MoveFaces.TOP) ' current top
'            axis = Axis.X
'            cube.CurrentOrientation.Top = faces(MoveFaces.BACK)
'            direction = Direction.ANTICLOCKWISE
'        Case faces(MoveFaces.LEFT) ' current left
'            axis = Axis.Y
'            direction = Direction.ANTICLOCKWISE
'        Case faces(MoveFaces.BACK) ' current back
'            axis = Axis.Y
'            direction = Direction.HALF_TURN
'        Case faces(MoveFaces.RIGHT) ' current right
'            axis = Axis.Y
'            direction = Direction.CLOCKWISE
'        Case faces(MoveFaces.FRONT) ' current front
'            axis = Axis.X
'            direction = Direction.NO_CHANGE
'        Case faces(MoveFaces.BOTTOM) ' current bottom
'            axis = Axis.X
'            cube.CurrentOrientation.Top = faces(MoveFaces.FRONT)
'            direction = Direction.CLOCKWISE
'    End Select

'    RotateCube(direction, axis, cube, Instructions)

'    cube.CurrentOrientation.Front = newFront.ToString()

'End Sub
'Public Sub RotateFace(ByVal face As MyPublic.FaceColours, ByVal direction As Direction)
'    If direction = Direction.NO_CHANGE Then
'        Return
'    End If

'    Dim faces() As Char = cube.FaceChars
'    ' Direction of rotation of the instruction, i.e. with regards to the face not the axis
'    Dim directionOfInstruction As Direction
'    directionOfInstruction = direction
'    Dim axis As Axis
'    Dim coordinateOfFaceOnAxis As Integer
'    Select Case face.ToString
'        Case faces(MoveFaces.TOP)
'            axis = Axis.Y
'            coordinateOfFaceOnAxis = 1
'            Instructions += "U"
'        Case faces(MoveFaces.LEFT)
'            axis = Axis.X
'            If direction <> Direction.HALF_TURN Then
'                direction = -direction
'            End If
'            coordinateOfFaceOnAxis = -1
'            Instructions += "L"
'        Case faces(MoveFaces.BACK)
'            axis = Axis.Z
'            If direction <> Direction.HALF_TURN Then
'                direction = -direction
'            End If
'            coordinateOfFaceOnAxis = -1
'            Instructions += "B"
'        Case faces(MoveFaces.RIGHT)
'            axis = Axis.X
'            coordinateOfFaceOnAxis = 1
'            Instructions += "R"
'        Case faces(MoveFaces.FRONT)
'            axis = Axis.Z
'            coordinateOfFaceOnAxis = 1
'            Instructions += "F"
'        Case faces(MoveFaces.BOTTOM)
'            axis = Axis.Y
'            If direction <> Direction.HALF_TURN Then
'                direction = -direction
'            End If
'            coordinateOfFaceOnAxis = -1
'            Instructions += "D"
'    End Select
'    Select Case directionOfInstruction
'        Case Direction.CLOCKWISE : Instructions += "  "
'        Case Direction.ANTICLOCKWISE : Instructions += "' "
'        Case Direction.HALF_TURN : Instructions += "2 "
'    End Select
'    Dim angle As Double
'    angle = (Math.PI / 2) * direction
'    Dim rotationMatrix As Matrix3x3
'    rotationMatrix = GetRotationMatrix(axis, angle)
'    Dim faceColours() As Char = ""
'    If face.ToString() <> cube.CurrentOrientation.Front And face.ToString() <> MyPublic.Opposite(cube.CurrentOrientation.Front) Then
'        faceColours = MyPublic.GetFaceColoursFromOrientation(New CubeOrientation(face.ToString(), cube.CurrentOrientation.Front))
'    ElseIf face.ToString() = cube.CurrentOrientation.Front Then
'        faceColours = MyPublic.GetFaceColoursFromOrientation(New CubeOrientation(face.ToString(), cube.CurrentOrientation.Bottom))
'    ElseIf face.ToString() = MyPublic.Opposite(cube.CurrentOrientation.Front) Then
'        faceColours = MyPublic.GetFaceColoursFromOrientation(New CubeOrientation(face.ToString(), cube.CurrentOrientation.Top))
'    End If

'    For Each cubie In cube.Corners
'        Dim coordinate As Integer
'        Select Case axis
'            Case Axis.X : coordinate = cubie.Position.x
'            Case Axis.Y : coordinate = cubie.Position.y
'            Case Axis.Z : coordinate = cubie.Position.z
'        End Select

'        If coordinate <> coordinateOfFaceOnAxis Then
'            Continue For
'        End If

'        cubie.Position = rotationMatrix * cubie.Position

'        Dim currentFaceForRotation As Integer
'        For i = 1 To 4
'            If cubie.Rotation = faceColours(i) Then
'                currentFaceForRotation = i
'            End If
'        Next
'        Dim currentFaceForSecondaryRotation As Integer
'        For i = 1 To 4
'            If cubie.SecondaryRotation = faceColours(i) Then
'                currentFaceForSecondaryRotation = i
'            End If
'        Next
'        If cubie.Rotation = face.ToString() Then
'            cubie.SecondaryRotation = GetNewBlockRotation(directionOfInstruction, faceColours, currentFaceForSecondaryRotation)
'        ElseIf cubie.SecondaryRotation = face.ToString() Then
'            cubie.Rotation = GetNewBlockRotation(directionOfInstruction, faceColours, currentFaceForRotation)
'        Else
'            cubie.Rotation = GetNewBlockRotation(directionOfInstruction, faceColours, currentFaceForRotation)
'            cubie.SecondaryRotation = GetNewBlockRotation(directionOfInstruction, faceColours, currentFaceForSecondaryRotation)
'        End If
'    Next

'    For Each cubie In cube.Edges
'        Dim coordinate As Integer
'        Select Case axis
'            Case Axis.X : coordinate = cubie.Position.x
'            Case Axis.Y : coordinate = cubie.Position.y
'            Case Axis.Z : coordinate = cubie.Position.z
'        End Select
'        If coordinate <> coordinateOfFaceOnAxis Then
'            Continue For
'        End If
'        cubie.Position = rotationMatrix * cubie.Position
'        If cubie.Rotation <> face.ToString() Then

'            Dim currentFace As Integer
'            For i = 1 To 4
'                If cubie.Rotation = faceColours(i) Then
'                    currentFace = i
'                End If
'            Next
'            cubie.Rotation = GetNewBlockRotation(directionOfInstruction, faceColours, currentFace)
'        End If
'    Next
'End Sub






'Private Function GetNewBlockRotation(ByVal directionOfRotation As Direction, ByVal faceColours As Char(), ByVal currentRotation As Integer) As Char
'    Select Case directionOfRotation
'        Case Direction.CLOCKWISE
'            Return faceColours((currentRotation Mod 4) + 1)
'        Case Direction.ANTICLOCKWISE
'            Return faceColours(((currentRotation + 2) Mod 4) + 1)
'        Case Direction.HALF_TURN
'            Return faceColours(((currentRotation + 1) Mod 4) + 1)
'        Case Else
'            Return Nothing
'    End Select
'End Function

'''' <summary>
'''' Rotates the whole cube in a specified direction
'''' </summary>
'Private Sub RotateCube(ByVal direction As Direction, ByVal axis As Axis, ByRef cube As Cube, ByRef Instructions As String)

'    If direction = Direction.NO_CHANGE Then
'        Return
'    End If

'    Dim angle As Double = (Math.PI / 2) * direction

'    Dim rotationMatrix As Matrix3x3
'    rotationMatrix = GetRotationMatrix(axis, angle)

'    Select Case axis
'        Case Axis.X : Instructions += "X"
'        Case Axis.Y : Instructions += "Y"
'        Case Axis.Z : Instructions += "Z"
'    End Select

'    Select Case direction
'        Case Direction.CLOCKWISE : Instructions += "  "
'        Case Direction.ANTICLOCKWISE : Instructions += "' "
'        Case Direction.HALF_TURN : Instructions += "2 "
'    End Select

'    For Each cubie In cube.CornersAndEdges
'        cubie.Position = rotationMatrix * cubie.Position
'    Next

'End Sub
