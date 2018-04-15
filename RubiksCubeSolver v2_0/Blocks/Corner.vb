Imports RubiksCubeSolver_v2_0.Helpers

<Serializable()> Public Class Corner
    Inherits Block

    Private _secondaryFace As FaceColour
    Public Property SecondaryFace() As FaceColour
        Get
            Return _secondaryFace
        End Get
        Set(value As FaceColour)
            _secondaryFace = value
        End Set
    End Property

    Private _secondaryFaceRotation As FaceColour
    Public Property SecondaryRotation() As FaceColour
        Get
            Return _secondaryFaceRotation
        End Get
        Set(value As FaceColour)
            _secondaryFaceRotation = value
        End Set
    End Property

    Public ReadOnly Property SideColours(ByVal topColour As FaceColour) As FaceColour()
        Get
            If Not HasColour(topColour) Then Throw New ArgumentException("corner does not contain topcolour")
            Dim sideCols(1) As FaceColour
            Dim index As Integer = 0
            For Each colour In Colours
                If colour = topColour Then Continue For
                sideCols(index) = colour
                index += 1
            Next
            Return sideCols
            Throw New Exception("edge does not contain the requested colour")
        End Get
    End Property

    Public Overrides Function ToString() As String
        Return Name + ", [" + Colours(0).ToString() + ", " + Colours(1).ToString() + ", " + Colours(2).ToString() + "], " + Position.ToString + ", 1st:" + PrimaryFace.ToString() + "-->" + Rotation.ToString() + ", 2nd:" + SecondaryFace.ToString() + "-->" + SecondaryRotation.ToString()
    End Function

    Public Overrides Function HasColour(colour As FaceColour) As Boolean
        For col = 0 To 2
            If Colours(col) = colour Then Return True
        Next
        Return False
    End Function

    Public Overrides Function CorrectRotation(cubieFaceColour As FaceColour, cubeFaceColour As FaceColour) As Boolean
        If Not HasColour(cubieFaceColour) Then Return False
        Select Case cubieFaceColour
            Case FaceColour.W, FaceColour.Y
                If Rotation <> cubeFaceColour Then Return False
            Case FaceColour.R, FaceColour.O
                If Rotation = cubeFaceColour Or SecondaryRotation <> cubeFaceColour Then Return False
            Case FaceColour.B, FaceColour.G
                If Rotation = cubeFaceColour Or SecondaryRotation = cubeFaceColour Then Return False
        End Select
        Return True
    End Function

    Public Overrides Function Correct(cube As Cube) As Boolean
        Return OnFace(Colours(0), cube) And OnFace(Colours(1), cube) And OnFace(Colours(2), cube) And
            CorrectRotation(Colours(0), Colours(0)) And CorrectRotation(Colours(1), Colours(1)) And CorrectRotation(Colours(2), Colours(2))
    End Function

    Public Overrides Sub SetColoursFromColourString(colourString As String)
        For i = 0 To 2
            Colours(i) = ColourChar2FaceNumber(colourString(i))
        Next
    End Sub
End Class
