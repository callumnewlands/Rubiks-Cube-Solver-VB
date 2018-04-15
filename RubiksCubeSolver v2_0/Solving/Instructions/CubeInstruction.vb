Imports RubiksCubeSolver_v2_0.Helpers

<Serializable()> Public Class CubeInstruction
    Inherits Instruction

    Public Sub New(ByVal axis As Axis, ByVal direction As Direction)
        Move = axis
        Me.Direction = direction
    End Sub

    Public Overrides Function ToString() As String
        Dim instructionString As String

        Select Case Move
            Case Axis.X : instructionString = "X"
            Case Axis.Y : instructionString = "Y"
            Case Axis.Z : instructionString = "Z"
            Case Else
                Throw New ArgumentException("Invalid axis")
        End Select

        Select Case Direction
            Case Direction.CLOCKWISE : instructionString += ""
            Case Direction.ANTICLOCKWISE : instructionString += "'"
            Case Direction.HALF_TURN : instructionString += "2"
            Case Else
                Throw New ArgumentException("Invalid direction")
        End Select
        Return instructionString
    End Function

    Public Overrides Function Copy() As Instruction
        Return New CubeInstruction(Me.Move, Me.Direction)
    End Function
End Class
