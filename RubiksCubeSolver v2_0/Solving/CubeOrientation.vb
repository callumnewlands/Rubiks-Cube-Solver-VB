Imports RubiksCubeSolver_v2_0.Helpers

<Serializable()> Public Class CubeOrientation
    Public Property Top As FaceColour

    Public ReadOnly Property Bottom As FaceColour
        Get
            Return Opposite(Top)
        End Get
    End Property

    Public Property Front As FaceColour

    Public ReadOnly Property UpsideDown As CubeOrientation
        Get
            Return New CubeOrientation(Bottom, Front)
        End Get
    End Property

    Sub New(ByVal top As Char, ByVal front As Char)
        Me.Top = ColourChar2FaceNumber(top)
        Me.Front = ColourChar2FaceNumber(front)
    End Sub

    Sub New(ByVal top As FaceColour, ByVal front As FaceColour)
        Me.Top = top
        Me.Front = front
    End Sub

    Sub New(ByVal orientation As CubeOrientation)
        Top = orientation.Top
        Front = orientation.Front
    End Sub

    Public Overrides Function ToString() As String
        Return "Top: " + Top.ToString() + ", Front: " + Front.ToString()
    End Function

    Public Function Copy() As CubeOrientation

        Return New CubeOrientation(Top, Front)

    End Function

End Class
