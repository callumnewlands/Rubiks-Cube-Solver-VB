Imports RubiksCubeSolver_v2_0.Helpers
Imports System.Runtime.Serialization.Formatters.Binary

<Serializable()> Public Class Cube

    Public Property CurrentOrientation As CubeOrientation
    Public Property Corners() As Corner()
    Public Property Edges() As Edge()
    Public Property Middles() As Middle()
    Public Property Instructions As InstructionList

    Public Property TopFace As FaceColour
        Get
            Return CurrentOrientation.Top
        End Get
        Set(value As FaceColour)
            CurrentOrientation.Top = value
        End Set
    End Property

    Public ReadOnly Property BottomFace As FaceColour
        Get
            Return CurrentOrientation.Bottom
        End Get
    End Property

    Public Property FrontFace As FaceColour
        Get
            Return CurrentOrientation.Front
        End Get
        Set(value As FaceColour)
            CurrentOrientation.Front = value
        End Set
    End Property

    Public ReadOnly Property CornersAndEdgesAndMiddles As Block()
        Get
            If Me.Corners.Length < 1 Or Me.Edges.Length < 1 Or Me.Middles.Length < 1 Then Return Nothing
            Dim _cornersAndEdgesandmiddles(26) As Block
            For i = 0 To 7
                _cornersAndEdgesandmiddles(i) = Corners(i)
            Next
            For i = 0 To 11
                _cornersAndEdgesandmiddles(i + 8) = Edges(i)
            Next
            For i = 0 To 6
                _cornersAndEdgesandmiddles(i + 20) = Middles(i)
            Next
            Return _cornersAndEdgesandmiddles
        End Get
    End Property

    ReadOnly Property CorrectTopEdges() As Edge()
        Get
            Dim rightCubies(3) As Edge
            Dim index As Integer = 0
            Dim topEdges() As Edge = TopEdgesClockwise()
            For Each cubie In topEdges
                If cubie.CorrectForFace(TopFace, Me) Then
                    rightCubies(index) = cubie
                    index += 1
                End If
            Next
            ReDim Preserve rightCubies(index - 1)
            Return rightCubies
        End Get
    End Property

    ReadOnly Property IncorrectTopEdges() As Edge()
        Get
            Dim wrongCubies(3) As Edge
            Dim index As Integer = 0
            Dim topEdges() As Edge = TopEdgesClockwise()
            For Each cubie In topEdges
                If Not cubie.CorrectForFace(TopFace, Me) Then
                    wrongCubies(index) = cubie
                    index += 1
                End If
            Next
            ReDim Preserve wrongCubies(index - 1)
            Return wrongCubies
        End Get
    End Property

    ReadOnly Property CorrectTopCorners() As Corner()
        Get
            Dim rightCubies(3) As Corner
            Dim index As Integer = 0
            Dim topEdges() As Corner = TopCornersClockwise()
            For Each cubie In topEdges
                If cubie.CorrectForFace(TopFace, Me) Then
                    rightCubies(index) = cubie
                    index += 1
                End If
            Next
            ReDim Preserve rightCubies(index - 1)
            Return rightCubies
        End Get
    End Property

    ReadOnly Property IncorrectTopCorners() As Corner()
        Get
            Dim wrongCubies(3) As Corner
            Dim index As Integer = 0
            Dim topEdges() As Corner = TopCornersClockwise()
            For Each cubie In topEdges
                If Not cubie.CorrectForFace(TopFace, Me) Then
                    wrongCubies(index) = cubie
                    index += 1
                End If
            Next
            ReDim Preserve wrongCubies(index - 1)
            Return wrongCubies
        End Get
    End Property

    ''' <summary>
    ''' Returns an array of cubies with the correct colour stickers
    ''' </summary>
    Public ReadOnly Property BlocksByColour(ByVal colour As FaceColour) As Block()
        Get
            Dim cubies(0 To 8) As Block
            Dim count As Integer = 0
            For Each block In CornersAndEdgesAndMiddles
                If Not block.HasColour(colour) Then Continue For
                cubies(count) = block
                count += 1
            Next
            Return cubies
        End Get
    End Property

    Public ReadOnly Property TopEdgesClockwise() As Edge()
        Get
            Return TopBlocksClockwise().Extract(Of Edge)()
        End Get
    End Property

    Public ReadOnly Property TopCornersClockwise() As Corner()
        Get
            Return TopBlocksClockwise().Extract(Of Corner)()
        End Get
    End Property

    Public ReadOnly Property TopBlocksClockwise() As Block()
        Get
            Dim topBlocks(8) As Block

            For Each block In CornersAndEdgesAndMiddles
                If block.Position.y <> 1 Then Continue For

                Dim index As Integer
                If block.GetType() = GetType(Edge) Then
                    index = TopEdgePositionToClockwiseIndex(block.Position)
                ElseIf block.GetType() = GetType(Corner) Then
                    index = TopCornerPositionToClockwiseIndex(block.Position)
                ElseIf block.GetType() = GetType(Middle) Then
                    index = 8
                End If
                topBlocks(index) = block
            Next

            Return topBlocks
        End Get
    End Property

    ''' <summary> 
    ''' Converts a position vector to an index in an 8-element array to store the blocks on the top face 
    ''' </summary>
    Private Function TopCornerPositionToClockwiseIndex(pos As Vector3x1) As Integer
        Dim index As Integer
        Select Case (pos.x + pos.z)
            Case -2 'Back left corner
                index = 2
            Case 0
                If pos.x = -1 Then ' Front left corner
                    index = 0
                ElseIf pos.x = 1 Then ' Back right corner
                    index = 4
                End If
            Case 2 ' Front right corner
                index = 6
        End Select
        Return index
    End Function

    ''' <summary> 
    ''' Converts a position vector to an index in an 8-element array to store the blocks on the top face 
    ''' </summary>
    Private Function TopEdgePositionToClockwiseIndex(pos As Vector3x1) As Integer
        Dim index As Integer
        Select Case pos.x
            Case -1 ' left edge cubie
                index = 1
            Case 0
                If pos.z = -1 Then ' back edge cubie
                    index = 3
                Else ' front edge cubie
                    index = 7
                End If
            Case 1 ' right edge cubie
                index = 5
        End Select
        Return index
    End Function

    Public ReadOnly Property MiddleEdgesClockwise() As Edge()
        Get
            Dim middleEdges(0 To 3) As Edge
            Dim index As Integer
            For Each edge In Edges
                If edge.Layer <> Layer.MIDDLE Then Continue For
                Select Case (edge.Position.x + edge.Position.z)
                    Case -2 'Back left edge
                        index = 1
                    Case 0
                        If edge.Position.x = -1 Then ' Front left edge
                            index = 0
                        ElseIf edge.Position.x = 1 Then ' Back right edge
                            index = 2
                        End If
                    Case 2 ' Front right edge
                        index = 3
                End Select
                middleEdges(index) = edge
            Next
            Return middleEdges
        End Get
    End Property

    ReadOnly Property FaceColours As FaceColour()
        Get
            Return GetFaceColoursFromOrientation(CurrentOrientation)
        End Get
    End Property

    Public Sub New(ByVal cube As Cube)
        Me.CurrentOrientation = cube.CurrentOrientation.Copy()
        Me.Corners = cube.Corners.Copy()
        Me.Edges = cube.Edges.Copy()
        CreateMiddles()
        Me.Instructions = cube.Instructions.Copy()
    End Sub

    Public Sub New(ByVal corners() As Corner, ByVal edges() As Edge, ByVal instructions As InstructionList)
        Me.Corners = corners
        Me.Edges = edges
        CreateMiddles()
        Me.Instructions = instructions
    End Sub

    Public Sub New(ByVal stickers(,) As Char)

        CurrentOrientation = New CubeOrientation(CChar("W"), CChar("R"))
        Instructions = New InstructionList
        InitialiseBlocks()
        Dim originalData(,) As Char = Copy(stickers)

        'Converts top half of cube 
        Dim transformedData(,) As Char = OrientateStickerArray(originalData, CurrentOrientation)
        ConvertTopCorners(transformedData)
        ConvertTopEdges(transformedData)

        'Converts bottom half of cube 
        transformedData = OrientateStickerArray(originalData, CurrentOrientation.UpsideDown)
        ConvertTopCorners(transformedData)
        ConvertTopEdges(transformedData)

        CreateMiddles()
    End Sub

    Private Sub InitialiseBlocks()
        FillNameArrays()
        ReDim Corners(7)
        ReDim Edges(11)
        For i = 0 To 7
            Me.Corners(i) = New Corner With {.Name = CORNER_NAMES(i)}
        Next
        For i = 0 To 11
            Me.Edges(i) = New Edge With {.Name = EDGE_NAMES(i)}
        Next
    End Sub

    Private Sub CreateMiddles()
        ReDim Middles(6)
        For i = 0 To 6
            Me.Middles(i) = New Middle
        Next
        Middles(0).Name = "W"
        Middles(1).Name = "G"
        Middles(2).Name = "O"
        Middles(3).Name = "B"
        Middles(4).Name = "R"
        Middles(5).Name = "Y"
        Middles(6).Name = ""
        For i = 0 To 5
            Middles(i).Colours(0) = Helpers.ColourChar2FaceNumber(CChar(Middles(i).Name))
            Middles(i).Rotation = Middles(i).PrimaryFace
        Next
        Middles(0).Position = New Vector3x1(0, 1, 0)
        Middles(1).Position = New Vector3x1(-1, 0, 0)
        Middles(2).Position = New Vector3x1(0, 0, -1)
        Middles(3).Position = New Vector3x1(1, 0, 0)
        Middles(4).Position = New Vector3x1(0, 0, 1)
        Middles(5).Position = New Vector3x1(0, -1, 0)
        Middles(6).Position = New Vector3x1(0, 0, 0)
    End Sub

    Private Sub ConvertTopCorners(ByVal stickers(,) As Char)

        Dim convertingTopLayer As Boolean
        convertingTopLayer = (stickers(0, 4) = FaceNumber2ColourChar(CurrentOrientation.Top))

        For faceNumber = MoveFaces.TOP To MoveFaces.FRONT
            For cornerStickerNumber = 0 To 8 Step 2

                Dim cornerSticker As New Sticker(faceNumber, cornerStickerNumber)
                Dim stickerColour As Char = stickers(faceNumber, cornerStickerNumber)
                If Not (OnTopHalfOfCube(cornerSticker) And IsPrimaryCornerSticker(stickerColour)) Then Continue For

                Dim cornerTri As New CornerTriplet(cornerSticker)

                Dim secondaryFaceColour, secondaryFaceRotation As FaceColour
                'Gets the colour string for the cubie e.g. "YRG" and also gets the secondary face colour and rotation
                Dim cornerString As String = GetCornerColours(stickers, cornerTri, secondaryFaceColour,
                                                              secondaryFaceRotation)

                For cornerNumber = 0 To UBound(CORNER_NAMES)

                    If CORNER_NAMES(cornerNumber) <> cornerString Then Continue For

                    If convertingTopLayer Then
                        Corners(cornerNumber).Position = GetTopCornerPosition(faceNumber, cornerStickerNumber)
                    Else
                        Corners(cornerNumber).Position = GetBottomCornerPosition(faceNumber, cornerStickerNumber)
                    End If
                    Corners(cornerNumber).Rotation = ColourChar2FaceNumber(stickers(cornerTri.Corners(0).FaceNumber, 4))
                    Corners(cornerNumber).SecondaryFace = secondaryFaceColour
                    Corners(cornerNumber).SecondaryRotation = secondaryFaceRotation
                    Corners(cornerNumber).SetColoursFromColourString(cornerString)
                Next cornerNumber
            Next cornerStickerNumber
        Next faceNumber

    End Sub

    Private Sub ConvertTopEdges(ByVal stickers(,) As Char)

        Dim convertingTopLayer As Boolean
        convertingTopLayer = (stickers(0, 4) = FaceNumber2ColourChar(CurrentOrientation.Top))

        For faceNumber = MoveFaces.TOP To MoveFaces.FRONT
            For edgeStickerNumber = 1 To 7 Step 2

                Dim edgeSticker As New Sticker(faceNumber, edgeStickerNumber)
                Dim stickerColour As Char = stickers(faceNumber, edgeStickerNumber)
                Dim edgePair As New EdgePair(edgeSticker)
                Dim secondStickerColour As Char = stickers(edgePair.Edges(1).FaceNumber, edgePair.Edges(1).StickerNumber)

                If Not (OnTopHalfOfCube(edgeSticker) And
                    IsPrimaryEdgeSticker(stickerColour, secondStickerColour)) Then Continue For 'try next sticker

                Dim edgeString As String = GetEdgeColours(stickers, edgePair)
                For edgeNumber = 0 To UBound(EDGE_NAMES)

                    If EDGE_NAMES(edgeNumber) <> edgeString Then Continue For

                    If convertingTopLayer Then
                        Edges(edgeNumber).Position = GetTopEdgePosition(faceNumber, edgeStickerNumber)
                    Else
                        Edges(edgeNumber).Position = GetBottomEdgePosition(faceNumber, edgeStickerNumber)
                    End If
                    Edges(edgeNumber).Rotation = ColourChar2FaceNumber(stickers(edgePair.Edges(0).FaceNumber, 4))
                    Edges(edgeNumber).SetColoursFromColourString(edgeString)
                Next edgeNumber
            Next edgeStickerNumber
        Next faceNumber

    End Sub

    Private Function OnTopHalfOfCube(ByVal position As Sticker) As Boolean
        Return position.StickerNumber <> MIDDLE_STICKER And
            (position.FaceNumber = MoveFaces.TOP Or position.StickerNumber < MIDDLE_STICKER)
    End Function

    Private Function IsPrimaryCornerSticker(ByVal stickerColour As Char) As Boolean
        Return stickerColour = "W" Or stickerColour = "Y"
    End Function

    Private Function IsPrimaryEdgeSticker(ByVal stickerColour As Char, ByVal secondStickerColour As Char) As Boolean
        Return ((stickerColour = "W" Or stickerColour = "Y") Or
            ((stickerColour = "R" Or stickerColour = "O") And (secondStickerColour <> "W" And secondStickerColour <> "Y")))
    End Function

    Public Sub Solve(ByRef displayForm As Processing)

        If Complete() Then Return

        Try
            displayForm.lblStatus.Text = "Solving Top Layer"
            displayForm.lblStatus.Refresh()
            SolveTopLayer()

            displayForm.lblStatus.Text = "Solving Middle Layer"
            displayForm.lblStatus.Refresh()
            SolveMiddleLayer()

            displayForm.lblStatus.Text = "Solving Bottom Layer"
            displayForm.lblStatus.Refresh()
            SolveBottomLayer()

            If Not Complete() Then Throw New StageNotSuccessfulException()

        Catch ex As StageNotSuccessfulException
            Console.WriteLine(ex.Message)
            MsgBox("Your cube cannot be solved, this is likely because the stickers have been re-arranged, or one of the pieces has been removed and re-inserted in the wrong orientation. You will need to take the cube apart and re-assemble it in its solved state. If you want you can go throught the instructions the program generated, which will not solve it but may bring it closer to being solved.")
        End Try

        displayForm.lblStatus.Text = "Optimising Steps For Solving"
        displayForm.lblStatus.Refresh()
        Instructions.Optimise()

    End Sub

    Private Sub SolveTopLayer()
        Dim topLayerSolver As New TopLayerSolver(Me)
        topLayerSolver.Solve()
    End Sub

    Private Sub SolveMiddleLayer()
        Dim middleLayerSolver As New MiddleLayerSolver(Me)
        middleLayerSolver.Solve()
    End Sub

    Private Sub SolveBottomLayer()
        Dim bottomLayerSolver As New BottomLayerSolver(Me)
        bottomLayerSolver.Solve()
    End Sub

    Public ReadOnly Property Complete() As Boolean
        Get
            For face As FaceColour = Helpers.FaceColour.W To Helpers.FaceColour.O
                Dim faceBlocks As Block() = BlocksByColour(face)
                For Each block In faceBlocks
                    If Not block.Correct(Me) Then Return False
                Next
            Next
            Return True
        End Get
    End Property

    ''' <summary> Rotates the whole cube </summary>
    Public Sub Rotate(ByVal direction As Direction, ByVal axis As Axis)

        If direction = Direction.NO_CHANGE Then Return
        'used for undoing half-turns when ouputting
        If direction = -2 Then direction = Direction.HALF_TURN

        Dim angle As Double = (Math.PI / 2.0) * direction
        Dim rotationMatrix As Matrix3x3 = GetRotationMatrix(axis, angle)

        Instructions.AddOrientationChange(axis, direction)

        For Each cubie In CornersAndEdgesAndMiddles
            cubie.Position = rotationMatrix * cubie.Position
        Next

        UpdateOrientation(direction, axis)

    End Sub

    Private Sub UpdateOrientation(ByVal direction As Direction, ByVal axis As Axis)
        Dim faces() As FaceColour = FacesAroundAxisClockwise(axis)
        If axis <> Axis.Y Then TopFace = faces((Array.IndexOf(faces, TopFace) + 4 + direction) Mod 4)
        If axis <> Axis.Z Then FrontFace = faces((Array.IndexOf(faces, FrontFace) + 4 + direction) Mod 4)
    End Sub

    Private Function FacesAroundAxisClockwise(ByVal axis As Axis) As FaceColour()
        Select Case axis
            Case Axis.X
                Return {FaceColours(MoveFaces.FRONT), FaceColours(MoveFaces.TOP), FaceColours(MoveFaces.BACK),
                    FaceColours(MoveFaces.BOTTOM)}
            Case Axis.Y
                Return {FaceColours(MoveFaces.FRONT), FaceColours(MoveFaces.LEFT), FaceColours(MoveFaces.BACK),
                    FaceColours(MoveFaces.RIGHT)}
            Case Axis.Z
                Return {FaceColours(MoveFaces.TOP), FaceColours(MoveFaces.RIGHT), FaceColours(MoveFaces.BOTTOM),
                    FaceColours(MoveFaces.LEFT)}
            Case Else
                Throw New ArgumentException("Invalid Axis")
        End Select
    End Function

    ''' <summary> Rotates the cube so that a specific face is on top </summary>
    Public Sub RotateFaceToTop(ByVal newTop As Helpers.FaceColour)
        Dim faces() As FaceColour = FaceColours
        Select Case newTop
            Case faces(MoveFaces.TOP)
                Return
            Case faces(MoveFaces.LEFT)
                Me.Rotate(Direction.CLOCKWISE, Axis.Z)
            Case faces(MoveFaces.BACK)
                Me.Rotate(Direction.ANTICLOCKWISE, Axis.X)
                FrontFace = faces(MoveFaces.TOP)
            Case faces(MoveFaces.RIGHT)
                Me.Rotate(Direction.ANTICLOCKWISE, Axis.Z)
            Case faces(MoveFaces.FRONT)
                Me.Rotate(Direction.CLOCKWISE, Axis.X)
                FrontFace = faces(MoveFaces.BOTTOM)
            Case faces(MoveFaces.BOTTOM)
                Me.Rotate(Direction.HALF_TURN, Axis.Z)
        End Select
    End Sub

    ''' <summary> Rotates the cube so that a specific face is at the front </summary>
    Public Sub RotateFaceToFront(ByVal newFront As Helpers.FaceColour)
        Dim faces() As FaceColour = FaceColours
        Select Case newFront
            Case faces(MoveFaces.TOP)
                Me.Rotate(Direction.ANTICLOCKWISE, Axis.X)
                TopFace = faces(MoveFaces.BACK)
            Case faces(MoveFaces.LEFT)
                Me.Rotate(Direction.ANTICLOCKWISE, Axis.Y)
            Case faces(MoveFaces.BACK)
                Me.Rotate(Direction.HALF_TURN, Axis.Y)
            Case faces(MoveFaces.RIGHT)
                Me.Rotate(Direction.CLOCKWISE, Axis.Y)
            Case faces(MoveFaces.FRONT)
                Return
            Case faces(MoveFaces.BOTTOM)
                Me.Rotate(Direction.CLOCKWISE, Axis.X)
                TopFace = faces(MoveFaces.FRONT)
        End Select
    End Sub

    ''' <summary> Rotates the cube so that a specific face is on the left </summary>
    Public Sub RotateFaceToLeft(ByVal newLeft As Helpers.FaceColour)
        Dim faces() As FaceColour = FaceColours
        Select Case newLeft
            Case faces(MoveFaces.TOP)
                Me.Rotate(Direction.ANTICLOCKWISE, Axis.Z)
            Case faces(MoveFaces.LEFT)
                Return
            Case faces(MoveFaces.BACK)
                Me.Rotate(Direction.ANTICLOCKWISE, Axis.Y)
            Case faces(MoveFaces.RIGHT)
                Me.Rotate(Direction.HALF_TURN, Axis.Y)
            Case faces(MoveFaces.FRONT)
                Me.Rotate(Direction.CLOCKWISE, Axis.Y)
            Case faces(MoveFaces.BOTTOM)
                Me.Rotate(Direction.CLOCKWISE, Axis.Z)
        End Select
    End Sub

    Public Sub RotateFace(ByVal face As FaceColour, ByVal direction As Direction)

        If direction = Direction.NO_CHANGE Then Return
        If direction = -2 Then direction = Direction.HALF_TURN

        Dim mathematicalDirection As Direction = direction
        Dim axis As Axis

        Select Case face
            Case FaceColours(MoveFaces.TOP)
                axis = Axis.Y
            Case FaceColours(MoveFaces.LEFT)
                axis = Axis.X
                If direction <> Direction.HALF_TURN Then mathematicalDirection = -mathematicalDirection
            Case FaceColours(MoveFaces.BACK)
                axis = Axis.Z
                If direction <> Direction.HALF_TURN Then mathematicalDirection = -mathematicalDirection
            Case FaceColours(MoveFaces.RIGHT)
                axis = Axis.X
            Case FaceColours(MoveFaces.FRONT)
                axis = Axis.Z
            Case FaceColours(MoveFaces.BOTTOM)
                axis = Axis.Y
                If direction <> Direction.HALF_TURN Then mathematicalDirection = -mathematicalDirection
        End Select

        Dim angle As Double = (Math.PI / 2.0) * mathematicalDirection
        Dim rotationMatrix As Matrix3x3 = GetRotationMatrix(axis, angle)
        Instructions.AddFaceTurn(Array.IndexOf(FaceColours, face), direction)

        Dim faceColoursAsIfRotatedFaceIsTop() As FaceColour
        If face <> FrontFace And face <> Opposite(FrontFace) Then
            faceColoursAsIfRotatedFaceIsTop = Helpers.GetFaceColoursFromOrientation(New CubeOrientation(face, FrontFace))
        ElseIf face = FrontFace Then
            faceColoursAsIfRotatedFaceIsTop = Helpers.GetFaceColoursFromOrientation(New CubeOrientation(face, BottomFace))
        ElseIf face = Opposite(FrontFace) Then
            faceColoursAsIfRotatedFaceIsTop = Helpers.GetFaceColoursFromOrientation(New CubeOrientation(face, TopFace))
        End If

        For Each cubie In CornersAndEdgesAndMiddles
            If Not cubie.OnFace(face, Me) Then Continue For
            cubie.Position = rotationMatrix * cubie.Position
            cubie.Rotation = GetNewRotation(direction, cubie.Rotation, faceColoursAsIfRotatedFaceIsTop)
            If cubie.GetType() = GetType(Corner) Then DirectCast(cubie, Corner).SecondaryRotation =
                GetNewRotation(direction, DirectCast(cubie, Corner).SecondaryRotation, faceColoursAsIfRotatedFaceIsTop)
        Next

    End Sub

    ''' <summary> Gets the new rotation for a cubie from a given rotation </summary>
    Private Function GetNewRotation(ByVal direction As Direction, ByVal currentRotation As FaceColour, ByVal faceColoursAsIfRotatedFaceIsTop() As FaceColour) As FaceColour
        Dim currentFaceIndex As Integer = Array.IndexOf(faceColoursAsIfRotatedFaceIsTop, currentRotation)

        If currentFaceIndex = 0 Then Return currentRotation
        If currentFaceIndex = 5 Then Throw New ArgumentException("The cube is not on the face being rotated")

        Select Case direction
            Case Direction.CLOCKWISE
                Return faceColoursAsIfRotatedFaceIsTop((currentFaceIndex Mod 4) + 1)
            Case Direction.ANTICLOCKWISE
                Return faceColoursAsIfRotatedFaceIsTop(((currentFaceIndex + 2) Mod 4) + 1)
            Case Direction.HALF_TURN
                Return faceColoursAsIfRotatedFaceIsTop(((currentFaceIndex + 1) Mod 4) + 1)
            Case Else
                Return currentRotation
        End Select
    End Function

    Public Sub Save(ByVal filePath As String)
        If filePath.Substring(filePath.Length - 5) <> ".cube" Then filePath += ".cube"
        Try
            Dim fStream As New IO.FileStream(filePath, IO.FileMode.Create)
            WriteCubeToFile(fStream)
            fStream.Close()
        Catch ex As Exception
            MsgBox("Unable to save file, please try a different fileName" & vbNewLine & ex.Message)
            Throw New WriteUnsuccessfulException
        End Try
    End Sub

    Private Sub WriteCubeToFile(ByRef fStream As IO.FileStream)
        Dim formatter As New BinaryFormatter()
        formatter.Serialize(fStream, Me)
    End Sub

End Class
