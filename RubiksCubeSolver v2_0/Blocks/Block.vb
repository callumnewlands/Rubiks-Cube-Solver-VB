Imports RubiksCubeSolver_v2_0.Helpers

<Serializable()> Public MustInherit Class Block
    Private _name As String
    Public Property Name() As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
        End Set
    End Property

    Private _position As New Vector3x1()
    Public Property Position As Vector3x1
        Get
            Return _position
        End Get
        Set(value As Vector3x1)
            _position = New Vector3x1(value)
        End Set
    End Property

    Public ReadOnly Property Layer As Layer
        Get
            Return Position.y
        End Get
    End Property

    ''' <summary> The colour of the face that the primary face is on </summary>
    Private _rotation As FaceColour
    Public Property Rotation() As FaceColour
        Get
            Return _rotation
        End Get
        Set(value As FaceColour)
            _rotation = value
        End Set
    End Property

    Private _colours() As FaceColour = {FaceColour.None, FaceColour.None, FaceColour.None}
    Public Property Colours(ByVal index As Integer) As FaceColour
        Get
            Return _colours(index)
        End Get
        Set(value As FaceColour)
            _colours(index) = value
        End Set
    End Property
    Public Property Colours() As FaceColour()
        Get
            Return _colours
        End Get
        Set(value As FaceColour())
            _colours = value
        End Set
    End Property

    Public MustOverride Sub SetColoursFromColourString(ByVal colourString As String)

    Public ReadOnly Property PrimaryFace() As FaceColour
        Get
            Return _colours(0)
        End Get
    End Property

    Public MustOverride Overrides Function ToString() As String

    Public MustOverride Function HasColour(ByVal colour As FaceColour) As Boolean

    Public Function OnFace(ByVal face As FaceColour, ByVal cube As Cube) As Boolean

        Dim faces As FaceColour() = cube.FaceColours

        Return face = faces(MoveFaces.TOP) And Position.y = 1 Or
               face = faces(MoveFaces.BOTTOM) And Position.y = -1 Or
               face = faces(MoveFaces.FRONT) And Position.z = 1 Or
               face = faces(MoveFaces.BACK) And Position.z = -1 Or
               face = faces(MoveFaces.LEFT) And Position.x = -1 Or
               face = faces(MoveFaces.RIGHT) And Position.x = 1
    End Function

    ''' <summary>
    ''' Checks if the orientation of a given face of a cube matches a given face colour
    ''' </summary>
    Public MustOverride Function CorrectRotation(ByVal cubieFaceColour As FaceColour, ByVal cubeFaceColour As FaceColour) As Boolean

    ''' <summary>
    ''' Checks if a cubie is correct for the given face
    ''' e.g. if face = blue, checks that the cubie is on the blue face, 
    ''' and that the blue side of a blue cubie is on the blue face of the cube
    ''' </summary>
    Public Overridable Function CorrectForFace(ByVal face As FaceColour, ByVal cube As Cube) As Boolean

        Return OnFace(face, cube) And CorrectRotation(face, face)
    End Function

    ''' <summary>
    ''' Checks if an edge is 'correctly' on the opposite face to what it should be 
    ''' (e.g. if edgeFaceColour = White, it checks if the edge's white face is on the yellow face
    ''' </summary>
    Public Overridable Function CorrectlyOnOppositeFace(ByVal cubieFaceColour As FaceColour, ByVal cube As Cube) As Boolean
        Dim cubeFaceColour As FaceColour = Opposite(cubieFaceColour)
        Return OnFace(cubeFaceColour, cube) And CorrectRotation(cubieFaceColour, cubeFaceColour)
    End Function

    ''' <summary>
    ''' Checks if a cubie is in its correct position and orientation
    ''' </summary>
    Public MustOverride Function Correct(ByVal cube As Cube) As Boolean

End Class

