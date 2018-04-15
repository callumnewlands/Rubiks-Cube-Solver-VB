Imports OpenTK
Imports OpenTK.Graphics.OpenGL
Imports RubiksCubeSolver_v2_0.Helpers.PublicConstants
Imports System.Runtime.Serialization.Formatters.Binary

Public Class _3DOutput

    Const pad As Single = 5 / 100
    ReadOnly projection As OpenTK.Matrix4 = Matrix4.CreateOrthographic(4 / 3 * 7, 7, 1, 1000)
    ReadOnly initalPositions() As OpenTK.Vector3
    ReadOnly initalRotations() As OpenTK.Quaternion
    ReadOnly initalScrambledCube As Cube
    ReadOnly instructions As OutputInstructionList

    Private shaderProgram As ShaderProgram
    Private camera As Camera
    Private deltaTime As Single = 0
    Private isMouseDown As Boolean
    Private currentCube As Cube
    Private cubieRotations(26) As OutputBlock
    Private showingRotation As Boolean = False


    Public Sub New(ByVal cube As Cube)
        InitializeComponent()
        initalScrambledCube = cube
        initalScrambledCube.CurrentOrientation = New CubeOrientation(FaceColour.W, FaceColour.R)
        initalPositions = GetInitialPositions()
        initalRotations = GetInitialRotations()
        camera = New Camera()
        instructions = New OutputInstructionList(cube.Instructions)
        UpdateInstructionLabels()
        currentCube = New Cube(initalScrambledCube)
        For i = 0 To 26
            cubieRotations(i) = New OutputBlock
        Next
    End Sub

    Public Sub New(ByVal filePath As String)
        InitializeComponent()

        Dim fStream As New IO.FileStream(filePath, IO.FileMode.Open)
        Dim formatter As New BinaryFormatter()
        initalScrambledCube = DirectCast(formatter.Deserialize(fStream), Cube)
        instructions = New OutputInstructionList(initalScrambledCube.Instructions)
        initalPositions = GetInitialPositions()
        initalRotations = GetInitialRotations()
        camera = New Camera()
        UpdateInstructionLabels()
        currentCube = New Cube(initalScrambledCube)
        For i = 0 To 26
            cubieRotations(i) = New OutputBlock
        Next
        Try
            Dim endPtr = GetCurrentInstructionPtr(filePath)
            If endPtr = 0 Then Exit Try
            For i = 0 To endPtr - 1
                If instructions.currentInstructionPtr < instructions.Count Then
                    DoNextStage()
                    instructions.currentInstructionPtr += 1
                    UpdateInstructionLabels()
                End If
            Next
            btnUndo.Enabled = True
        Catch ex As ReadUnsuccessfulException
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub GlControl1_Load(sender As Object, e As EventArgs) Handles GlControl1.Load
        LoadGraphics()

        If shaderProgram.Handle = -1 Then
            lblError.Visible = True
            Return
        End If
        LoadGeometry()
    End Sub

    ''' <summary>  Loads the shaders and sets up the viewport  </summary>
    Private Sub LoadGraphics()
        GL.Viewport(0, 0, GlControl1.Width, GlControl1.Height)
        GL.Enable(EnableCap.DepthTest)
        GL.DepthFunc(DepthFunction.Lequal)
        sldrAmbient.Value = 23
        shaderProgram = New ShaderProgram()
    End Sub

    ''' <summary>  Loads the lighting for the scene </summary>
    Private Sub LoadLighting()
        Dim colour As OpenTK.Graphics.Color4 = New OpenTK.Graphics.Color4(255, 255, 255, 255)
        Dim ambient As Vector4 = New Vector4(colour.R / 255, colour.G / 255, colour.B / 255, 1)
        Dim lightColorLoc As Integer = GL.GetUniformLocation(shaderProgram.Handle, "lightColor")
        GL.Uniform4(lightColorLoc, ambient.X, ambient.Y, ambient.Z, ambient.W)

        Static ambientStrength As Single = 0.95
        ambientStrength = sldrAmbient.Value / 20
        Dim ambientStrengthLoc As Integer = GL.GetUniformLocation(shaderProgram.Handle, "ambientStrength")
        GL.Uniform1(ambientStrengthLoc, ambientStrength)
    End Sub

    ''' <summary> Loads the vertices and vertexAttributes  </summary>
    Private Sub LoadGeometry()
        Dim vertices3d As Vertex3D() = CubeVertices()
        Dim indices As UInteger() = CubeIndices()

        shaderProgram.VAO = GL.GenVertexArray()
        shaderProgram.VBO = GL.GenBuffer()
        shaderProgram.EBO = GL.GenBuffer()

        ' Bind the Vertex Array Object
        GL.BindVertexArray(shaderProgram.VAO)
        ' copy our vertices to a buffer for OpenGL to use
        GL.BindBuffer(BufferTarget.ArrayBuffer, shaderProgram.VBO)
        GL.BufferData(BufferTarget.ArrayBuffer, CType(Vertex3D.SizeInBytes * vertices3d.Length, IntPtr), vertices3d,
                      BufferUsageHint.StaticDraw)
        ' copy the indeces to a buffer for OpenGL to use
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, shaderProgram.EBO)
        GL.BufferData(BufferTarget.ElementArrayBuffer, CType(4 * indices.Length, IntPtr), indices, BufferUsageHint.StaticDraw)
        ' set vertex attribute pointers to access them in the shaders
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, False, Vertex3D.SizeInBytes, 0)
        GL.EnableVertexAttribArray(0)
        GL.VertexAttribPointer(1, 4, VertexAttribPointerType.Float, False, Vertex3D.SizeInBytes, Vector3.SizeInBytes)
        GL.EnableVertexAttribArray(1)
        GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, False, Vertex3D.SizeInBytes,
                               Vector3.SizeInBytes + Vector4.SizeInBytes)
        GL.EnableVertexAttribArray(2)
        ' Unbind the Vertex Buffer Object
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0)
        ' Unbind the Vertex Array Object
        GL.BindVertexArray(0)
        GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill)
    End Sub

    Private Function CubeVertices() As Vertex3D()
        Return New Vertex3D(0 To 23) {
            New Vertex3D(New Vector3(0, 0, 0), Color.DarkOrange, New Vector3(0, 0, -1)),
            New Vertex3D(New Vector3(1, 0, 0), Color.DarkOrange, New Vector3(0, 0, -1)),
            New Vertex3D(New Vector3(1, 1, 0), Color.DarkOrange, New Vector3(0, 0, -1)),
            New Vertex3D(New Vector3(0, 1, 0), Color.DarkOrange, New Vector3(0, 0, -1)),
            New Vertex3D(New Vector3(0, 0, 1), Color.Red, New Vector3(0, 0, 1)),
            New Vertex3D(New Vector3(1, 0, 1), Color.Red, New Vector3(0, 0, 1)),
            New Vertex3D(New Vector3(1, 1, 1), Color.Red, New Vector3(0, 0, 1)),
            New Vertex3D(New Vector3(0, 1, 1), Color.Red, New Vector3(0, 0, 1)),
            New Vertex3D(New Vector3(0, 0, 0), Color.Green, New Vector3(-1, 0, 0)),
            New Vertex3D(New Vector3(1, 0, 0), Color.Blue, New Vector3(1, 0, 0)),
            New Vertex3D(New Vector3(1, 1, 0), Color.Blue, New Vector3(1, 0, 0)),
            New Vertex3D(New Vector3(0, 1, 0), Color.Green, New Vector3(-1, 0, 0)),
            New Vertex3D(New Vector3(0, 0, 1), Color.Green, New Vector3(-1, 0, 0)),
            New Vertex3D(New Vector3(1, 0, 1), Color.Blue, New Vector3(1, 0, 0)),
            New Vertex3D(New Vector3(1, 1, 1), Color.Blue, New Vector3(1, 0, 0)),
            New Vertex3D(New Vector3(0, 1, 1), Color.Green, New Vector3(-1, 0, 0)),
            New Vertex3D(New Vector3(0, 0, 0), Color.Yellow, New Vector3(0, -1, 0)),
            New Vertex3D(New Vector3(1, 0, 0), Color.Yellow, New Vector3(0, -1, 0)),
            New Vertex3D(New Vector3(1, 1, 0), Color.White, New Vector3(0, 1, 0)),
            New Vertex3D(New Vector3(0, 1, 0), Color.White, New Vector3(0, 1, 0)),
            New Vertex3D(New Vector3(0, 0, 1), Color.Yellow, New Vector3(0, -1, 0)),
            New Vertex3D(New Vector3(1, 0, 1), Color.Yellow, New Vector3(0, -1, 0)),
            New Vertex3D(New Vector3(1, 1, 1), Color.White, New Vector3(0, 1, 0)),
            New Vertex3D(New Vector3(0, 1, 1), Color.White, New Vector3(0, 1, 0))}
    End Function

    Private Function CubeIndices() As UInteger()

        Return New UInteger(0 To 35) {
            4, 5, 6, 4, 6, 7,
            8, 11, 15, 8, 12, 15,
            16, 20, 21, 16, 17, 21,
            9, 10, 14, 9, 13, 14,
            19, 18, 22, 19, 22, 23,
            0, 1, 2, 0, 2, 3}
    End Function

    ''' <summary> gets the inital position vectors for each cubie </summary>
    Private Function GetInitialPositions() As Vector3()
        Dim corners() As Corner = initalScrambledCube.Corners
        Dim edges() As Edge = initalScrambledCube.Edges

        Dim Positions(26) As Vector3

        For i = 0 To 7
            Positions(i) = New Vector3 With {
                .X = corners(i).Position.x * (1 + pad) - 0.5,
                .Y = corners(i).Position.y * (1 + pad) - 0.5,
                .Z = corners(i).Position.z * (1 + pad) - 0.5
            }
        Next

        For i = 0 To 11
            Positions(i + 8) = New Vector3 With {
                .X = edges(i).Position.x * (1 + pad) - 0.5,
                .Y = edges(i).Position.y * (1 + pad) - 0.5,
                .Z = edges(i).Position.z * (1 + pad) - 0.5
            }
        Next

        Positions(20) = New Vector3(-0.5, 0.5 + pad, -0.5)
        Positions(21) = New Vector3(-1.5 - pad, -0.5, -0.5)
        Positions(22) = New Vector3(-0.5, -0.5, -1.5 - pad)
        Positions(23) = New Vector3(0.5 + pad, -0.5, -0.5)
        Positions(24) = New Vector3(-0.5, -0.5, 0.5 + pad)
        Positions(25) = New Vector3(-0.5, -1.5 - pad, -0.5)
        Positions(26) = New Vector3(-0.5, -0.5, -0.5)
        Return Positions
    End Function

    ''' <summary> gets the inital rotation quaternions for each cubie </summary>
    Private Function GetInitialRotations() As Quaternion()
        Dim initalBlocks() As Block = initalScrambledCube.CornersAndEdgesAndMiddles.Copy()

        Dim initialCubieRotations(26) As Quaternion

        Dim cubie As Block
        For i = 0 To 19
            cubie = initalBlocks(i)
            Dim rotationFirstAxis As Quaternion = GetRotationFirstAxis(cubie)
            Dim rotationSecondAxis As Quaternion = GetRotationSecondAxis(cubie, rotationFirstAxis)
            initialCubieRotations(i) = rotationSecondAxis * rotationFirstAxis
        Next

        For i = 20 To 26
            initialCubieRotations(i) = Quaternion.Identity
        Next

        Return initialCubieRotations

    End Function


    ''' <summary> gets the quaternion that puts the primary face correct </summary>
    Private Function GetRotationFirstAxis(ByVal cubie As Block) As Quaternion
        Dim rotationVector As Vector3 = New Vector3

        Select Case cubie.PrimaryFace
            Case FaceColour.W
                Select Case cubie.Rotation
                    Case FaceColour.Y : rotationVector.X = 2
                    Case FaceColour.R : rotationVector.X = 1
                    Case FaceColour.O : rotationVector.X = -1
                    Case FaceColour.G : rotationVector.Z = 1
                    Case FaceColour.B : rotationVector.Z = -1
                End Select
            Case FaceColour.Y
                Select Case cubie.Rotation
                    Case FaceColour.W : rotationVector.X = 2
                    Case FaceColour.R : rotationVector.X = -1
                    Case FaceColour.O : rotationVector.X = 1
                    Case FaceColour.G : rotationVector.Z = -1
                    Case FaceColour.B : rotationVector.Z = 1
                End Select
            Case FaceColour.R
                Select Case cubie.Rotation
                    Case FaceColour.W : rotationVector.X = -1
                    Case FaceColour.Y : rotationVector.X = 1
                    Case FaceColour.O : rotationVector.X = 2
                    Case FaceColour.G : rotationVector.Y = -1
                    Case FaceColour.B : rotationVector.Y = 1
                End Select
            Case FaceColour.O
                Select Case cubie.Rotation
                    Case FaceColour.W : rotationVector.X = 1
                    Case FaceColour.Y : rotationVector.X = -1
                    Case FaceColour.R : rotationVector.X = 2
                    Case FaceColour.G : rotationVector.Y = 1
                    Case FaceColour.B : rotationVector.Y = -1
                End Select
        End Select

        Dim rotationFirstAxis As Quaternion = New Quaternion With {
            .X = rotationVector.X * Math.Sin(Math.Abs(rotationVector.X) * MathHelper.PiOver2 / 2),
            .Y = rotationVector.Y * Math.Sin(Math.Abs(rotationVector.Y) * MathHelper.PiOver2 / 2),
            .Z = rotationVector.Z * Math.Sin(Math.Abs(rotationVector.Z) * MathHelper.PiOver2 / 2)
        }

        Dim vecSum As Integer = Math.Abs(rotationVector.X) + Math.Abs(rotationVector.Y) + Math.Abs(rotationVector.Z)
        rotationFirstAxis.W = Math.Cos(vecSum * MathHelper.PiOver2 / 2)

        If rotationFirstAxis.Length = rotationFirstAxis.W Then
            rotationFirstAxis = Quaternion.Identity
        End If

        Return rotationFirstAxis

    End Function


    Private Function GetRotationSecondAxis(cubie As Block, rotationFirstAxis As Quaternion) As Quaternion

        Dim whiteNormalAfterFirstRotation As Vector3 = New Vector3(0, 1, 0)
        Dim redNormalAfterFirstRotation As Vector3 = New Vector3(0, 0, 1)
        RecalculateNormalsAfterFirstRotation(whiteNormalAfterFirstRotation, redNormalAfterFirstRotation,
                                             rotationFirstAxis)

        Dim desiredWhiteNormal, desiredRedNormal As Vector3
        GetNormalsFromCubie(cubie, desiredWhiteNormal, desiredRedNormal)

        Dim angle As Single
        Dim axis As Vector3
        angle = 0
        If whiteNormalAfterFirstRotation = desiredWhiteNormal Then
            'rotate around white normal
            axis = whiteNormalAfterFirstRotation
            angle = GetAngleForInitialRotation(axis, redNormalAfterFirstRotation, desiredRedNormal)
        ElseIf redNormalAfterFirstRotation = desiredRedNormal Then
            'rotate around red normal
            axis = redNormalAfterFirstRotation
            angle = GetAngleForInitialRotation(axis, whiteNormalAfterFirstRotation, desiredWhiteNormal)
        End If

        Dim rotationSecondAxis As Quaternion
        rotationSecondAxis = Quaternion.Identity
        If angle <> 0 Then
            rotationSecondAxis.X = Math.Abs(axis.X) * Math.Sin(angle / 2)
            rotationSecondAxis.Y = Math.Abs(axis.Y) * Math.Sin(angle / 2)
            rotationSecondAxis.Z = Math.Abs(axis.Z) * Math.Sin(angle / 2)
            rotationSecondAxis.W = Math.Cos(angle / 2)
        Else
            rotationSecondAxis = Quaternion.Identity
        End If

        Return rotationSecondAxis
    End Function

    Private Sub RecalculateNormalsAfterFirstRotation(ByRef whiteNormal As Vector3, ByRef redNormal As Vector3, ByVal firstRotation As Quaternion)

        Dim rotationMatrix As Matrix4
        rotationMatrix = Matrix4.CreateTranslation(New Vector3(-0.5, -0.5, -0.5))
        rotationMatrix *= Matrix4.CreateFromQuaternion(firstRotation)
        rotationMatrix *= Matrix4.CreateTranslation(-(New Vector3(-0.5, -0.5, -0.5)))

        Dim myRotMatrix As Matrices.Matrix4x4
        myRotMatrix = New Matrix4x4(rotationMatrix)
        myRotMatrix.Row1.w = 0
        myRotMatrix.Row2.w = 0
        myRotMatrix.Row3.w = 0

        Dim myRedNormal, myWhiteNormal As Matrices.Vector4x1
        myWhiteNormal = New Vector4x1(New Vector4(whiteNormal, 1))
        myRedNormal = New Vector4x1(New Vector4(redNormal, 1))

        myWhiteNormal = myRotMatrix * myWhiteNormal
        myRedNormal = myRotMatrix * myRedNormal

        whiteNormal = New Vector3(Math.Round(myWhiteNormal.x, 4), Math.Round(myWhiteNormal.y, 4),
                                  Math.Round(myWhiteNormal.z, 4))
        redNormal = New Vector3(Math.Round(myRedNormal.x, 4), Math.Round(myRedNormal.y, 4),
                                Math.Round(myRedNormal.z, 4))

    End Sub

    Private Sub GetNormalsFromCubie(ByVal cubie As Block, ByRef whiteNormal As Vector3, ByRef redNormal As Vector3)

        Dim secondaryRotation, secondaryFace As FaceColour

        If cubie.GetType() = GetType(Corner) Then
            Dim cornerCubie As Corner = cubie
            secondaryRotation = cornerCubie.SecondaryRotation
            secondaryFace = cornerCubie.SecondaryFace
        ElseIf cubie.GetType() = GetType(Edge) Then
            For Each colour In cubie.Colours
                If colour <> cubie.PrimaryFace Then
                    secondaryFace = colour
                    Exit For
                End If
            Next
            For Each face As FaceColour In initalScrambledCube.FaceColours
                If cubie.CorrectRotation(secondaryFace, face) And cubie.OnFace(face, initalScrambledCube) Then
                    secondaryRotation = face
                    Exit For
                End If
            Next
        End If

        Dim whiteSet, redSet As Boolean
        whiteSet = False
        redSet = False
        Select Case cubie.PrimaryFace
            Case FaceColour.W, FaceColour.Y
                Select Case cubie.Rotation
                    Case FaceColour.W : whiteNormal = New Vector3(0, 1, 0)
                    Case FaceColour.Y : whiteNormal = New Vector3(0, -1, 0)
                    Case FaceColour.R : whiteNormal = New Vector3(0, 0, 1)
                    Case FaceColour.O : whiteNormal = New Vector3(0, 0, -1)
                    Case FaceColour.G : whiteNormal = New Vector3(-1, 0, 0)
                    Case FaceColour.B : whiteNormal = New Vector3(1, 0, 0)
                End Select
                If cubie.PrimaryFace = FaceColour.Y Then
                    whiteNormal = -whiteNormal
                End If
                whiteSet = True
            Case FaceColour.R, FaceColour.O
                Select Case cubie.Rotation
                    Case FaceColour.W : redNormal = New Vector3(0, 1, 0)
                    Case FaceColour.Y : redNormal = New Vector3(0, -1, 0)
                    Case FaceColour.R : redNormal = New Vector3(0, 0, 1)
                    Case FaceColour.O : redNormal = New Vector3(0, 0, -1)
                    Case FaceColour.G : redNormal = New Vector3(-1, 0, 0)
                    Case FaceColour.B : redNormal = New Vector3(1, 0, 0)
                End Select
                If cubie.PrimaryFace = FaceColour.O Then
                    redNormal = -redNormal
                End If
                redSet = True
        End Select
        Select Case secondaryFace
            Case FaceColour.R, FaceColour.O
                Select Case secondaryRotation
                    Case FaceColour.W : redNormal = New Vector3(0, 1, 0)
                    Case FaceColour.Y : redNormal = New Vector3(0, -1, 0)
                    Case FaceColour.R : redNormal = New Vector3(0, 0, 1)
                    Case FaceColour.O : redNormal = New Vector3(0, 0, -1)
                    Case FaceColour.G : redNormal = New Vector3(-1, 0, 0)
                    Case FaceColour.B : redNormal = New Vector3(1, 0, 0)
                End Select
                If secondaryFace = FaceColour.O Then
                    redNormal = -redNormal
                End If
                redSet = True
            Case FaceColour.G, FaceColour.B
                Dim greenNormal As Vector3
                Select Case secondaryRotation
                    Case FaceColour.W : greenNormal = New Vector3(0, 1, 0)
                    Case FaceColour.Y : greenNormal = New Vector3(0, -1, 0)
                    Case FaceColour.R : greenNormal = New Vector3(0, 0, 1)
                    Case FaceColour.O : greenNormal = New Vector3(0, 0, -1)
                    Case FaceColour.G : greenNormal = New Vector3(-1, 0, 0)
                    Case FaceColour.B : greenNormal = New Vector3(1, 0, 0)
                End Select
                If secondaryFace = FaceColour.B Then
                    greenNormal = -greenNormal
                End If
                If whiteSet Then
                    redNormal = Vector3.Cross(whiteNormal, greenNormal)
                ElseIf redSet Then
                    whiteNormal = -Vector3.Cross(redNormal, greenNormal)
                Else
                    Throw New NotImplementedException("GetNormalsFromCubie: primary normal not set!")
                End If
        End Select
    End Sub

    Private Function GetAngleForInitialRotation(axis As Vector3, currentPosition As Vector3, desiredPosition As Vector3) As Single

        Dim angle As Single
        If currentPosition = desiredPosition Then
            angle = 0
        ElseIf Math.Abs(Vector3.Dot(currentPosition, desiredPosition)) = 1 Then
            angle = MathHelper.Pi
        Else
            'collapse the vectors onto 1 2D plane, perpendicular to axis of rotation

            Dim current2d, correct2d As Vector2
            If axis.X <> 0 Then
                current2d = New Vector2(-currentPosition.Z, currentPosition.Y)
                correct2d = New Vector2(-desiredPosition.Z, desiredPosition.Y)
            ElseIf axis.Y <> 0 Then
                current2d = New Vector2(currentPosition.X, -currentPosition.Z)
                correct2d = New Vector2(desiredPosition.X, -desiredPosition.Z)
            ElseIf axis.Z <> 0 Then
                current2d = New Vector2(currentPosition.X, currentPosition.Y)
                correct2d = New Vector2(desiredPosition.X, desiredPosition.Y)
            End If

            If current2d.Y <> 0 Then
                If current2d.Y = correct2d.X Then
                    angle = -MathHelper.PiOver2
                Else
                    angle = MathHelper.PiOver2
                End If
            ElseIf current2d.X <> 0 Then
                If current2d.X = correct2d.Y Then
                    angle = MathHelper.PiOver2
                Else
                    angle = -MathHelper.PiOver2
                End If
            End If

        End If
        Return angle
    End Function

    ''' <summary> Called by GlControl1.Invalidate() - updates the GL viewport </summary>
    Private Sub GlControl1_Paint(sender As Object, e As PaintEventArgs) Handles GlControl1.Paint

        GL.ClearColor(Color.CornflowerBlue)
        GL.Clear(ClearBufferMask.ColorBufferBit Or ClearBufferMask.DepthBufferBit)
        If shaderProgram.Handle = -1 Then Return

        Static previousFrame As Decimal
        UpdateDeltaTime(previousFrame)

        Dim camX As Single = Math.Sin(camera.Rotation / 10) * Camera.RADIUS
        Dim camZ As Single = Math.Cos(camera.Rotation / 10) * Camera.RADIUS
        Dim view As Matrix4 = Matrix4.LookAt(New Vector3(camX, camera.Pitch, camZ), New Vector3(0, 0, 0),
                                             New Vector3(0, 1, 0))

        'sends view matrix to shaders
        GL.Uniform3(GL.GetUniformLocation(shaderProgram.Handle, "viewPos"), camX, camera.Pitch, camZ)

        RenderCube(view, projection)
        camera.Update(False, deltaTime, isMouseDown)

        previousFrame = DateTime.Now.Ticks / 10000

        GlControl1.SwapBuffers() 'swaps the buffer being drawn to and the buffer being shown (updates the viewport)
        GlControl1.Invalidate() ' calls paint

    End Sub

    Private Sub UpdateDeltaTime(ByVal previousFrame As Decimal)
        Dim currentFrame As Decimal
        Static firsttime As Boolean = True

        If firsttime Then
            deltaTime = 5
            firsttime = False
        End If
        Do
            currentFrame = DateTime.Now.Ticks / 10000
            deltaTime = currentFrame - previousFrame
        Loop While deltaTime = 0
    End Sub

    ''' <summary> Renders a cube onto the secondary buffer </summary>
    Private Sub RenderCube(ByRef view As Matrix4, ByRef projection As Matrix4)

        shaderProgram.Use()

        LoadLighting()

        GL.UniformMatrix4(GL.GetUniformLocation(shaderProgram.Handle, "projection"), False, projection)
        GL.UniformMatrix4(GL.GetUniformLocation(shaderProgram.Handle, "view"), False, view)

        GL.Enable(EnableCap.DepthTest)
        GL.DepthFunc(DepthFunction.Less)

        Static displayAnglePercent As Double = 0.0
        Dim rotationSpeed As Single = sldrSpeed.Value / 1000
        If showingRotation Then
            displayAnglePercent += deltaTime * rotationSpeed
            If displayAnglePercent >= 1 Then
                displayAnglePercent = 1
                showingRotation = False
                btnNext.Enabled = True
                btnUndo.Enabled = True
            End If
        Else
            displayAnglePercent = 0
        End If

        For i = 0 To 26
            RenderCubie(i, displayAnglePercent)
        Next

    End Sub

    ''' <summary> renders the cubie represented by a given index </summary>
    Private Sub RenderCubie(ByVal index As Integer, ByVal displayAnglePercent As Double)

        GL.BindVertexArray(shaderProgram.VAO)

        GL.Uniform1(GL.GetUniformLocation(shaderProgram.Handle, "pad"), pad)

        Dim model As Matrix4 = Matrix4.CreateTranslation(initalPositions(26))
        model *= Matrix4.CreateFromQuaternion(initalRotations(index))
        model *= Matrix4.CreateTranslation(-initalPositions(26))
        model *= Matrix4.CreateTranslation(initalPositions(index))

        'model matrix passed to shaders to set colours in correct places
        Dim modelUniformLoc As Integer = GL.GetUniformLocation(shaderProgram.Handle, "colourMat")
        GL.UniformMatrix4(modelUniformLoc, False, model)

        model *= cubieRotations(index).Rotations.GetTotalMatrix(displayAnglePercent)
        GL.UniformMatrix4(GL.GetUniformLocation(shaderProgram.Handle, "model"), False, model)

        GL.DrawElements(PrimitiveType.Triangles, 36, DrawElementsType.UnsignedInt, 0)

    End Sub

    Private Sub btnUndo_Hover(sender As Object, e As EventArgs) Handles btnUndo.MouseEnter, btnUndo.MouseHover
        lblPrev.ForeColor = Color.Red
    End Sub
    Private Sub btnNext_Hover(sender As Object, e As EventArgs) Handles btnNext.MouseEnter, btnNext.MouseHover
        lblNext.ForeColor = Color.Red
    End Sub
    Private Sub btnUndo_MouseLeave(sender As Object, e As EventArgs) Handles btnUndo.MouseLeave
        lblPrev.ForeColor = Color.DarkGray
    End Sub
    Private Sub btnNext_MouseLeave(sender As Object, e As EventArgs) Handles btnNext.MouseLeave
        lblNext.ForeColor = Color.Black
    End Sub

    Private Sub UpdateInstructionLabels()

        If instructions.PreviousInstruction IsNot Nothing Then
            lblPrev.Text = instructions.PreviousInstruction.ToString()
        Else
            lblPrev.Text = "- -"
            btnUndo.Enabled = False
        End If
        If instructions.CurrentInstruction IsNot Nothing Then
            lblNext.Text = instructions.CurrentInstruction.ToString()
        Else
            lblNext.Text = "- -"
            btnNext.Enabled = False
        End If
        lblPrev.Refresh()
        lblNext.Refresh()
    End Sub

    Private Sub GlControl1_MouseDown(sender As Object, e As MouseEventArgs) Handles GlControl1.MouseDown
        Dim mousePos As New Mouse
        If Not isMouseDown Then
            mousePos.X = OpenTK.Input.Mouse.GetCursorState.X
            mousePos.Y = OpenTK.Input.Mouse.GetCursorState.Y
        End If
        isMouseDown = True
        camera.Update(True, deltaTime, isMouseDown, mousePos)
    End Sub
    Private Sub GlControl1_MouseUp(sender As Object, e As MouseEventArgs) Handles GlControl1.MouseUp
        isMouseDown = False
    End Sub

    Private Sub btnUndo_Click(sender As Object, e As EventArgs) Handles btnUndo.Click

        If shaderProgram.Handle <> -1 Then btnUndo.Enabled = False

        If instructions.currentInstructionPtr > 0 Then
            UndoLastStage()
            instructions.currentInstructionPtr -= 1
            UpdateInstructionLabels()
        End If

        If lblNext.Text <> "- -" Then
            btnNext.Enabled = True
        End If
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click

        If shaderProgram.Handle <> -1 Then btnNext.Enabled = False

        If instructions.currentInstructionPtr < instructions.Count Then
            DoNextStage()
            instructions.currentInstructionPtr += 1
            UpdateInstructionLabels()
        End If

        If lblPrev.Text <> "- -" Then
            btnUndo.Enabled = True
        End If

    End Sub

    Private Sub DoNextStage()
        DoInstruction(instructions.CurrentInstruction)
    End Sub


    Private Sub UndoLastStage()

        Dim reverseInstruction As Instruction
        reverseInstruction = instructions.PreviousInstruction.Copy()
        reverseInstruction.Direction = -reverseInstruction.Direction
        DoInstruction(reverseInstruction)

    End Sub

    Private Sub DoInstruction(ByVal instruction As Instruction)
        showingRotation = True
        If instruction.GetType() = GetType(FaceInstruction) Then
            RotateFace(instruction.Move, instruction.Direction)
        ElseIf instruction.GetType = GetType(CubeInstruction) Then
            RotateCube(instruction.Move, instruction.Direction)
        Else
            Throw New ArgumentException("Invalid instruction type")
        End If

    End Sub

    ''' <summary> addds the rotation matrices to the correct rotation stores in order to rotate a given face </summary>
    Private Sub RotateFace(ByVal face As MoveFaces, ByVal direction As Direction)

        Dim axis As Vector3

        Select Case face
            Case MoveFaces.RIGHT : axis = New Vector3(1, 0, 0)
            Case MoveFaces.LEFT : axis = New Vector3(-1, 0, 0)
            Case MoveFaces.TOP : axis = New Vector3(0, 1, 0)
            Case MoveFaces.BOTTOM : axis = New Vector3(0, -1, 0)
            Case MoveFaces.FRONT : axis = New Vector3(0, 0, 1)
            Case MoveFaces.BACK : axis = New Vector3(0, 0, -1)
            Case Else
                Throw New ArgumentException("invalid face")
        End Select

        For i = 0 To 26
            If Not currentCube.CornersAndEdgesAndMiddles(i).OnFace(currentCube.FaceColours(face), currentCube) Then Continue For
            cubieRotations(i).Rotations.Add(axis, IIf(Math.Abs(direction) =
                                                      Direction.HALF_TURN, -direction, direction) * MathHelper.PiOver2)
            cubieRotations(i).BeingRotated = True
            currentCube.RotateFace(currentCube.FaceColours(face), direction)
        Next

    End Sub

    ''' <summary> addds the rotation matrices to the correct rotation stores in order to rotate the cube </summary>
    Private Sub RotateCube(ByVal axis As Axis, ByVal direction As Direction)
        Dim axisVector As Vector3
        Select Case axis
            Case Axis.X : axisVector = New Vector3(1, 0, 0)
            Case Axis.Y : axisVector = New Vector3(0, 1, 0)
            Case Axis.Z : axisVector = New Vector3(0, 0, 1)
            Case Else
                Throw New ArgumentException("Invalid axis")
        End Select
        Dim angle As Single = MathHelper.PiOver2 * IIf(Math.Abs(direction) = Direction.HALF_TURN, -direction, direction)
        For i = 0 To 26
            cubieRotations(i).Rotations.Add(axisVector, angle)
        Next
        currentCube.Rotate(direction, axis)
    End Sub

    Private Sub UpdateOrientation(ByVal axis As Axis, ByVal direction As Direction)
        Dim faces() As FaceColour = FacesAroundAxisClockwise(axis)
        If axis <> Axis.Y Then currentCube.TopFace = faces((Array.IndexOf(faces, currentCube.TopFace) + 4 + direction) Mod 4)
        If axis <> Axis.Z Then currentCube.FrontFace = faces((Array.IndexOf(faces, currentCube.FrontFace) + 4 + direction) Mod 4)
    End Sub

    Private Function FacesAroundAxisClockwise(ByVal axis As Axis) As FaceColour()
        Dim faceColours() As FaceColour = Helpers.GetFaceColoursFromOrientation(currentCube.CurrentOrientation)
        Select Case axis
            Case Axis.X
                Return {faceColours(MoveFaces.FRONT), faceColours(MoveFaces.TOP), faceColours(MoveFaces.BACK),
                    faceColours(MoveFaces.BOTTOM)}
            Case Axis.Y
                Return {faceColours(MoveFaces.FRONT), faceColours(MoveFaces.LEFT), faceColours(MoveFaces.BACK),
                    faceColours(MoveFaces.RIGHT)}
            Case Axis.Z
                Return {faceColours(MoveFaces.TOP), faceColours(MoveFaces.RIGHT), faceColours(MoveFaces.BOTTOM),
                    faceColours(MoveFaces.LEFT)}
            Case Else
                Throw New ArgumentException("Invalid Axis")
        End Select
    End Function
    Private Sub _3DOutput_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        shaderProgram.Dispose()
    End Sub

    Private Sub btnMain_Click(sender As Object, e As EventArgs) Handles btnMain.Click

        Try
            GlControl1.Visible = False
            SaveRoutine()
        Catch ex As SaveCancelledException
            GlControl1.Visible = True
            Return
        End Try

        Dim main As New MainMenu
        main.Show()
        Me.Close()

    End Sub

    Private Sub SaveRoutine()

        Dim save As MsgBoxResult = MsgBox("Do you want to save?" & vbNewLine & vbNewLine &
                                          "Yes = save and exit" & vbNewLine &
                                          "No = exit without saving" & vbNewLine &
                                          "Cancel = don't exit", MsgBoxStyle.YesNoCancel, "Save?")
        If save = MsgBoxResult.Yes Then
            Try
                SaveCube()
            Catch ex As TaskCanceledException
                Throw New SaveCancelledException
            Catch ex As WriteUnsuccessfulException
                btnMain.PerformClick()
                Throw New SaveCancelledException
            End Try
        ElseIf save = MsgBoxResult.Cancel Then
            Throw New SaveCancelledException
        End If
    End Sub

    Private Sub SaveCube()
        Dim fileBrowser As New SaveFileDialog()
        fileBrowser.Filter = "cube files (*.cube)|*.cube"
        fileBrowser.ShowDialog()
        Dim filePath As String = fileBrowser.FileName
        If filePath = Nothing Then Throw New TaskCanceledException
        initalScrambledCube.Save(filePath)
        WriteCurrentInstructionPtr(filePath)
    End Sub

    Private Sub WriteCurrentInstructionPtr(ByVal filePath As String)
        If filePath.Substring(filePath.Length - 5) <> ".cube" Then filePath += ".cube"
        filePath += ".ptr"
        Try
            Dim fStream As New IO.FileStream(filePath, IO.FileMode.Create)
            Dim formatter As New BinaryFormatter()
            formatter.Serialize(fStream, instructions.currentInstructionPtr)
            fStream.Close()
        Catch ex As Exception
            Throw New WriteUnsuccessfulException("CurrentInstructionPtr attempted to be written before cube file")
        End Try
    End Sub

    Private Function GetCurrentInstructionPtr(ByVal filePath As String) As Integer
        filePath += ".ptr"
        Try
            Dim fStream As New IO.FileStream(filePath, IO.FileMode.Open)
            Dim formatter As New BinaryFormatter()
            Return formatter.Deserialize(fStream)
        Catch ex As Exception
            Throw New ReadUnsuccessfulException(".ptr file not found, cannot load saved instruction")
        End Try
    End Function

End Class
