Imports RubiksCubeSolver_v2_0.Helpers

Public Module Matrices
    Public ReadOnly iVector As Vector3x1 = New Vector3x1(1, 0, 0)
    Public ReadOnly jVector As Vector3x1 = New Vector3x1(0, 1, 0)
    Public ReadOnly kVector As Vector3x1 = New Vector3x1(0, 0, 1)
    Public ReadOnly i4Vector As Vector4x1 = New Vector4x1(1, 0, 0, 0)
    Public ReadOnly j4Vector As Vector4x1 = New Vector4x1(0, 1, 0, 0)
    Public ReadOnly k4Vector As Vector4x1 = New Vector4x1(0, 0, 1, 0)
    Public ReadOnly l4Vector As Vector4x1 = New Vector4x1(0, 0, 0, 1)

    Public MustInherit Class Matrix
        Private _columns As Integer
        Public Property NoOfColumns As Integer
            Get
                Return _columns
            End Get
            Protected Set(ByVal value As Integer)
                _columns = value
            End Set
        End Property
        Private _rows As Integer
        Public Property NoOfRows As Integer
            Get
                Return _rows
            End Get
            Protected Set(ByVal value As Integer)
                _rows = value
            End Set
        End Property

    End Class

    <Serializable()> Public MustInherit Class Vector

        Private _rows
        Public Property NoOfRows
            Get
                Return _rows
            End Get
            Protected Set(value)
                _rows = value
            End Set
        End Property

        Public MustOverride Overrides Function ToString() As String

    End Class

    Public Class Matrix3x3
        Inherits Matrix


        Public Sub New()
            Me.NoOfColumns = 3
            Me.NoOfRows = 3
            Me.Row1 = iVector
            Me.Row2 = jVector
            Me.Row3 = kVector
        End Sub
        Public Sub New(ByVal mat As Matrix3x3)
            Me.Row1 = mat.Row1
            Me.Row2 = mat.Row2
            Me.Row3 = mat.Row3
        End Sub
        Public Sub New(ByVal row1 As Vector3x1, ByVal row2 As Vector3x1, ByVal row3 As Vector3x1)
            Me.NoOfColumns = 3
            Me.NoOfRows = 3
            Me.Row1 = row1
            Me.Row2 = row2
            Me.Row3 = row3
        End Sub

        Public Shared Operator *(ByVal mat1 As Matrix3x3, ByVal mat2 As Matrix3x3) As Matrix3x3
            Return New Matrix3x3(New Vector3x1(mat1.Row1.Dot(mat2.Column1),
                                               mat1.Row1.Dot(mat2.Column2),
                                               mat1.Row1.Dot(mat2.Column3)),
                                 New Vector3x1(mat1.Row2.Dot(mat2.Column1),
                                               mat1.Row2.Dot(mat2.Column2),
                                               mat1.Row2.Dot(mat2.Column3)),
                                 New Vector3x1(mat1.Row3.Dot(mat2.Column1),
                                               mat1.Row3.Dot(mat2.Column2),
                                               mat1.Row3.Dot(mat2.Column3)))
        End Operator

        Public Shared Operator *(ByVal mat As Matrix3x3, ByVal vec As Vector3x1) As Vector3x1
            Return New Vector3x1(mat.Row1.Dot(vec), mat.Row2.Dot(vec), mat.Row3.Dot(vec))
        End Operator

        Private _row1 As Vector3x1
        Private _row2 As Vector3x1
        Private _row3 As Vector3x1
        Public Property Row1() As Vector3x1
            Get
                Return _row1
            End Get
            Set(ByVal value As Vector3x1)
                _row1 = value
            End Set
        End Property
        Public Property Row2() As Vector3x1
            Get
                Return _row2
            End Get
            Set(ByVal value As Vector3x1)
                _row2 = value
            End Set
        End Property
        Public Property Row3() As Vector3x1
            Get
                Return _row3
            End Get
            Set(ByVal value As Vector3x1)
                _row3 = value
            End Set
        End Property

        Public Property Column1() As Vector3x1
            Get
                Return New Vector3x1(_row1.x, _row2.x, _row3.x)
            End Get
            Set(value As Vector3x1)
                _row1.x = value.x
                _row2.x = value.y
                _row3.x = value.z
            End Set
        End Property
        Public Property Column2() As Vector3x1
            Get
                Return New Vector3x1(_row1.y, _row2.y, _row3.y)
            End Get
            Set(value As Vector3x1)
                _row1.y = value.x
                _row2.y = value.y
                _row3.y = value.z
            End Set
        End Property
        Public Property Column3() As Vector3x1
            Get
                Return New Vector3x1(_row1.z, _row2.z, _row3.z)
            End Get
            Set(value As Vector3x1)
                _row1.z = value.x
                _row2.z = value.y
                _row3.z = value.z
            End Set
        End Property

        Public Overrides Function ToString() As String
            Return "{" + Row1.ToString() + vbNewLine + " " + Row2.ToString() + vbNewLine + " " + Row3.ToString() + "}"
        End Function

    End Class

    Public Class Matrix4x4
        Inherits Matrix


        Public Sub New()
            Me.NoOfColumns = 4
            Me.NoOfRows = 4
            Me.Row1 = i4Vector
            Me.Row2 = j4Vector
            Me.Row3 = k4Vector
            Me.Row4 = l4Vector
        End Sub
        Public Sub New(ByVal mat As Matrix4x4)
            Me.NoOfColumns = 4
            Me.NoOfRows = 4
            Me.Row1 = mat.Row1
            Me.Row2 = mat.Row2
            Me.Row3 = mat.Row3
            Me.Row4 = mat.Row4
        End Sub
        Public Sub New(ByVal row1 As Vector4x1, ByVal row2 As Vector4x1, ByVal row3 As Vector4x1, ByVal row4 As Vector4x1)
            Me.NoOfColumns = 4
            Me.NoOfRows = 4
            Me.Row1 = row1
            Me.Row2 = row2
            Me.Row3 = row3
            Me.Row4 = row4
        End Sub
        Public Sub New(ByVal mat As OpenTK.Matrix4)
            Me.NoOfColumns = 4
            Me.NoOfRows = 4
            Me.Row1 = New Vector4x1(mat.Column0)
            Me.Row2 = New Vector4x1(mat.Column1)
            Me.Row3 = New Vector4x1(mat.Column2)
            Me.Row4 = New Vector4x1(mat.Column3)
        End Sub

        Public Shared Operator *(ByVal mat As Matrix4x4, ByVal vec As Vector4x1) As Vector4x1
            Return New Vector4x1(mat.Row1.Dot(vec), mat.Row2.Dot(vec), mat.Row3.Dot(vec), mat.Row4.Dot(vec))
        End Operator

        Private _row1 As Vector4x1
        Private _row2 As Vector4x1
        Private _row3 As Vector4x1
        Private _row4 As Vector4x1
        Public Property Row1() As Vector4x1
            Get
                Return _row1
            End Get
            Set(ByVal value As Vector4x1)
                _row1 = value
            End Set
        End Property
        Public Property Row2() As Vector4x1
            Get
                Return _row2
            End Get
            Set(ByVal value As Vector4x1)
                _row2 = value
            End Set
        End Property
        Public Property Row3() As Vector4x1
            Get
                Return _row3
            End Get
            Set(ByVal value As Vector4x1)
                _row3 = value
            End Set
        End Property
        Public Property Row4() As Vector4x1
            Get
                Return _row4
            End Get
            Set(ByVal value As Vector4x1)
                _row4 = value
            End Set
        End Property

    End Class

    Public Class Vector2x1
        Inherits Vector

        Public Sub New()
            Me.NoOfRows = 2
            Me.x = 0
            Me.y = 0
        End Sub

        Public Sub New(ByVal x As Single, ByVal y As Single)
            Me.NoOfRows = 2
            Me.x = x
            Me.y = y
        End Sub

        Public Shared Operator =(ByVal vec1 As Vector2x1, ByVal vec2 As Vector2x1) As Boolean
            Return vec1.x = vec2.x And vec1.y = vec2.y
        End Operator
        Public Shared Operator <>(ByVal vec1 As Vector2x1, ByVal vec2 As Vector2x1) As Boolean
            Return Not vec1 = vec2
        End Operator

        Private _x As Single
        Private _y As Single
        Public Property x As Single
            Get
                Return _x
            End Get
            Set(ByVal value As Single)
                _x = Math.Round(value, 5)
            End Set
        End Property
        Public Property y As Single
            Get
                Return _y
            End Get
            Set(ByVal value As Single)
                _y = Math.Round(value, 5)
            End Set
        End Property
        Public Overrides Function ToString() As String
            Throw New NotImplementedException()
        End Function

        Public Function Dot(vec As Vector2x1) As Single
            Return (Me.x * vec.x + Me.y * vec.y)
        End Function

    End Class

    <Serializable()> Public Class Vector3x1
        Inherits Vector

        Public Sub New()
            Me.NoOfRows = 3
            Me.x = 0
            Me.y = 0
            Me.z = 0
        End Sub
        Public Sub New(ByVal x As Single, ByVal y As Single, ByVal z As Single)
            Me.NoOfRows = 3
            Me.x = x
            Me.y = y
            Me.z = z
        End Sub
        Public Sub New(ByVal vec As Vector3x1)
            Me.NoOfRows = 3
            Me.x = vec.x
            Me.y = vec.y
            Me.z = vec.z
        End Sub
        Public Sub New(ByVal vec As OpenTK.Vector3)
            Me.NoOfRows = 3
            Me.x = vec.X
            Me.y = vec.Y
            Me.z = vec.Z
        End Sub


        Private _x As Single
        Private _y As Single
        Private _z As Single
        Public Property x As Single
            Get
                Return _x
            End Get
            Set(ByVal value As Single)
                _x = Math.Round(value, 5)
            End Set
        End Property
        Public Property y As Single
            Get
                Return _y
            End Get
            Set(ByVal value As Single)
                _y = Math.Round(value, 5)
            End Set
        End Property
        Public Property z As Single
            Get
                Return _z
            End Get
            Set(ByVal value As Single)
                _z = Math.Round(value, 5)
            End Set
        End Property

        Public Shared Operator =(ByVal vec1 As Vector3x1, ByVal vec2 As Vector3x1)
            Return vec1.x = vec2.x And vec1.y = vec2.y And vec1.z = vec2.z
        End Operator
        Public Shared Operator <>(ByVal vec1 As Vector3x1, ByVal vec2 As Vector3x1)
            Return Not vec1 = vec2
        End Operator

        Public Overrides Function ToString() As String
            Return "[" + x.ToString() + ", " + y.ToString() + ", " + z.ToString() + "]"
        End Function

        Public Function Dot(vec As Vector3x1) As Single
            Return (Me.x * vec.x + Me.y * vec.y + Me.z * vec.z)
        End Function
    End Class

    Public Class Vector4x1
        Inherits Vector

        Public Sub New()
            Me.NoOfRows = 4
            Me.x = 0
            Me.y = 0
            Me.z = 0
            Me.w = 0
        End Sub
        Public Sub New(ByVal x As Single, ByVal y As Single, ByVal z As Single, ByVal w As Single)
            Me.NoOfRows = 4
            Me.x = x
            Me.y = y
            Me.z = z
            Me.w = w
        End Sub
        Public Sub New(ByVal vec As Vector4x1)
            Me.NoOfRows = 4
            Me.x = vec.x
            Me.y = vec.y
            Me.z = vec.z
            Me.w = vec.w
        End Sub
        Public Sub New(ByVal vec As OpenTK.Vector4)
            Me.NoOfRows = 4
            Me.x = vec.X
            Me.y = vec.Y
            Me.z = vec.Z
            Me.w = vec.W
        End Sub

        Private _x As Single
        Private _y As Single
        Private _z As Single
        Private _w As Single
        Public Property x As Single
            Get
                Return _x
            End Get
            Set(ByVal value As Single)
                _x = Math.Round(value, 10)
            End Set
        End Property
        Public Property y As Single
            Get
                Return _y
            End Get
            Set(ByVal value As Single)
                _y = Math.Round(value, 10)
            End Set
        End Property
        Public Property z As Single
            Get
                Return _z
            End Get
            Set(ByVal value As Single)
                _z = Math.Round(value, 10)
            End Set
        End Property
        Public Property w As Single
            Get
                Return _w
            End Get
            Set(ByVal value As Single)
                _w = Math.Round(value, 10)
            End Set
        End Property

        Public Shared Operator =(ByVal vec1 As Vector4x1, ByVal vec2 As Vector4x1)
            Return vec1.x = vec2.x And vec1.y = vec2.y And vec1.z = vec2.z And vec1.w = vec2.w
        End Operator
        Public Shared Operator <>(ByVal vec1 As Vector4x1, ByVal vec2 As Vector4x1)
            Return Not vec1 = vec2
        End Operator

        Public Overrides Function ToString() As String
            Throw New NotImplementedException()
        End Function

        Public Function Dot(vec As Vector4x1) As Single
            Return (Me.x * vec.x + Me.y * vec.y + Me.z * vec.z + Me.w * vec.w)
        End Function
    End Class

    Public Function GetRotationMatrix(ByVal axis As Axis, ByVal angle As Single) As Matrix3x3

        Select Case axis
            Case axis.X
                Return New Matrix3x3(New Vector3x1(1, 0, 0),
                                     New Vector3x1(0, Math.Cos(angle), -Math.Sin(angle)),
                                     New Vector3x1(0, Math.Sin(angle), Math.Cos(angle)))
            Case axis.Y
                Return New Matrix3x3(New Vector3x1(Math.Cos(angle), 0, Math.Sin(angle)),
                                     New Vector3x1(0, 1, 0),
                                     New Vector3x1(-Math.Sin(angle), 0, Math.Cos(angle)))
            Case axis.Z
                Return New Matrix3x3(New Vector3x1(Math.Cos(angle), -Math.Sin(angle), 0),
                                     New Vector3x1(Math.Sin(angle), Math.Cos(angle), 0),
                                     New Vector3x1(0, 0, 1))
            Case Else
                MsgBox("Error with rotation Axis")
                Return New Matrix3x3()
        End Select
    End Function

End Module
