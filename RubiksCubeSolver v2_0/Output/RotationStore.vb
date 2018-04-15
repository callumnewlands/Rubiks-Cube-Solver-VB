Imports OpenTK

Public Class RotationStore
    Public Property PreviousRotations As Matrix4 = Matrix4.Identity
    Public Property CurrentAxis As Vector3
    Public Property CurrentMaxAngle As Single

    Public Sub Add(ByVal axis As Vector3, ByVal maxAngle As Single)

        If CurrentAxis <> Nothing Then
            PreviousRotations *= Matrix4.CreateFromAxisAngle(CurrentAxis, CurrentMaxAngle)
        End If

        CurrentAxis = axis
        CurrentMaxAngle = maxAngle

    End Sub

    Public Function GetTotalMatrix(ByVal displayAnglePercentage As Single) As Matrix4

        If displayAnglePercentage >= 1 Then FinishCurrentRotation()

        Dim rotationMatrix As Matrix4 = Matrix4.Identity
        rotationMatrix *= PreviousRotations

        If CurrentAxis <> Nothing Then
            rotationMatrix *= Matrix4.CreateFromAxisAngle(CurrentAxis, displayAnglePercentage * CurrentMaxAngle)
        End If
        Return rotationMatrix
    End Function

    Private Sub FinishCurrentRotation()

        If CurrentAxis <> Nothing Then
            PreviousRotations *= Matrix4.CreateFromAxisAngle(CurrentAxis, CurrentMaxAngle)
        End If
        CurrentAxis = Nothing
        CurrentMaxAngle = Nothing
    End Sub

End Class
