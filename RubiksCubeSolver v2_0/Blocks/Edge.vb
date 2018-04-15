Imports RubiksCubeSolver_v2_0.Helpers

<Serializable()> Public Class Edge
    Inherits Block

    Public Sub New()
        ReDim Preserve Colours(1)
    End Sub

    Public Overrides Function ToString() As String
        Return Name + ", [" + Colours(0).ToString() + ", " + Colours(1).ToString() + "], " + Position.ToString + ", 1st:" + PrimaryFace.ToString() + "-->" + Rotation.ToString()
    End Function

    Public Overrides Function HasColour(colour As FaceColour) As Boolean
        For col = 0 To 1
            If Colours(col) = colour Then Return True
        Next
        Return False
    End Function

    Public ReadOnly Property SideColour(ByVal topColour As FaceColour) As FaceColour

        Get
            If Not HasColour(topColour) Then Throw New ArgumentException("edge does not contain topcolour")
            For Each colour In Colours
                If colour <> topColour Then Return colour
            Next
            Throw New Exception("edge only contains topcolour == error in assigning colours")
        End Get
    End Property

    Public ReadOnly Property EdgeColour(ByVal cube As Cube) As FaceColour
        Get
            Dim colourIndex As Integer
            If PrimaryFace = cube.TopFace Then
                colourIndex = 1
            Else
                colourIndex = 0
            End If
            Return Colours(colourIndex)
        End Get
    End Property

    Public Overrides Function CorrectRotation(cubieFaceColour As FaceColour, cubeFaceColour As FaceColour) As Boolean
        If Not HasColour(cubieFaceColour) Then Return False
        Select Case cubieFaceColour
            Case FaceColour.W, FaceColour.Y
                If Rotation <> cubeFaceColour Then Return False
            Case FaceColour.R, FaceColour.O
                If cubieFaceColour = PrimaryFace Then
                    If Rotation <> cubeFaceColour Then Return False
                Else
                    If Rotation = cubeFaceColour Then Return False
                End If
            Case FaceColour.B, FaceColour.G
                If Rotation = cubeFaceColour Then Return False
        End Select
        Return True
    End Function


    Public Overrides Function Correct(cube As Cube) As Boolean
        Return OnFace(Colours(0), cube) And OnFace(Colours(1), cube) And
            CorrectRotation(Colours(0), Colours(0)) And CorrectRotation(Colours(1), Colours(1))
    End Function

    Public Overrides Sub SetColoursFromColourString(colourString As String)
        For i = 0 To 1
            Colours(i) = ColourChar2FaceNumber(colourString(i))
        Next
    End Sub
End Class
