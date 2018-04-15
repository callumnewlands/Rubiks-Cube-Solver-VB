Imports OpenTK

Structure Vertex3D
    Public Shared ReadOnly SizeInBytes As Integer = (Vector3.SizeInBytes + Vector4.SizeInBytes + Vector3.SizeInBytes)

    Public position As Vector3
    Public colour As Vector4
    Public normal As Vector3

    Public Property Color() As Color
        Get
            Return Drawing.Color.FromArgb(CInt(colour.W * 255), CInt(colour.X * 255), CInt(colour.Y * 255), CInt(colour.Z * 255))
        End Get
        Set(ByVal value As Color)
            Me.colour = New Vector4(value.R / 255, value.G / 255, value.B / 255, value.A / 255)
        End Set
    End Property

    Public Sub New(ByVal position As Vector3, ByVal colour As Vector4, ByVal normal As Vector3)
        Me.position = position
        Me.colour = colour
        Me.normal = normal
    End Sub
    Public Sub New(ByVal position As Vector3, ByVal colour As Color, ByVal normal As Vector3)
        Me.position = position
        'Converts colour(ARGB) to vector4(R, G, B, A)
        Me.colour = New Vector4(colour.R / 255, colour.G / 255, colour.B / 255, colour.A / 255)
        Me.normal = normal
    End Sub

End Structure
