
Public Class Camera

    Public Const FRONT_ANGLE As Single = -63.0
    Public Const RADIUS As Single = 10
    Public Property Pitch As Single
    Public Property Rotation As Single

    Public Sub New()
        Rotation = -60.0
        Pitch = 2
    End Sub

    Public Sub Update(ByVal PositionUpdated As Boolean, ByVal deltaTime As Single, ByVal mouseDown As Boolean, Optional MousePos As Mouse = Nothing)

        Static StoredMousePos As New Mouse

        If PositionUpdated = True Then
            StoredMousePos.X = MousePos.X
            StoredMousePos.Y = MousePos.Y
        End If

        Dim cameraSpeed As Single = 0.07 * deltaTime

        If Not mouseDown Then

            If Pitch > 2 + (cameraSpeed * 2.2) Then
                Pitch -= cameraSpeed * 2.2
            ElseIf Pitch < 2 - (cameraSpeed * 2.2) Then
                Pitch += cameraSpeed * 2.2
            ElseIf Pitch >= 2 - (cameraSpeed * 2.2) And Pitch <= 2 + (cameraSpeed * 2.2) Then
                Pitch = 2
            End If
        End If

        If mouseDown Then
            Dim xPosition As Integer = OpenTK.Input.Mouse.GetCursorState.X
            Dim yPosition As Integer = OpenTK.Input.Mouse.GetCursorState.Y
            Dim xOffset As Integer = xPosition - StoredMousePos.X
            Dim yOffset As Integer = yPosition - StoredMousePos.Y
            Dim sensitivity As Single = 0.1

            StoredMousePos.X = xPosition
            StoredMousePos.Y = yPosition

            If Pitch <= 25 And Pitch >= -20 Then
                Pitch += (yOffset * sensitivity)
            ElseIf Pitch > 25 Then
                Pitch = 25
            ElseIf Pitch < -20 Then
                Pitch = -20
            End If

            If (Rotation - (xOffset * sensitivity * 0.5)) > (FRONT_ANGLE - 6) And
                (Rotation - (xOffset * sensitivity * 0.5)) < (FRONT_ANGLE + 6) Then
                Rotation -= (xOffset * sensitivity * 0.5)
            End If
        End If
    End Sub


End Class
