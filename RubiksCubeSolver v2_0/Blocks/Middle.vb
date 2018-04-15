Imports RubiksCubeSolver_v2_0.Helpers

<Serializable()> Public Class Middle
    Inherits Block
    Public Sub New()
        ReDim Preserve Colours(0)
    End Sub

    Public Overrides Function ToString() As String
        Return Name + ", [" + Colours(0).ToString() + "], " + Position.ToString + ", 1st:" + PrimaryFace.ToString() + "-->" + Rotation.ToString()
    End Function

    Public Overrides Function HasColour(colour As FaceColour) As Boolean
        Return Colours(0) = colour
    End Function

    Public Overrides Function CorrectRotation(cubieFaceColour As FaceColour, cubeFaceColour As FaceColour) As Boolean
        Return True
    End Function

    Public Overrides Function Correct(cube As Cube) As Boolean
        Return True
    End Function

    Public Overrides Function CorrectForFace(faceColour As FaceColour, cube As Cube) As Boolean
        Return True
    End Function

    Public Overrides Function CorrectlyOnOppositeFace(cubieFaceColour As FaceColour, cube As Cube) As Boolean
        Return False
    End Function

    Public Overrides Sub SetColoursFromColourString(colourString As String)
        Throw New NotImplementedException()
    End Sub

End Class
