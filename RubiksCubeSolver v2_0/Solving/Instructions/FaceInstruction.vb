Imports RubiksCubeSolver_v2_0.Helpers

<Serializable()> Public Class FaceInstruction
    Inherits Instruction

    Public Sub New(ByVal face As MoveFaces, ByVal direction As Direction)
        Move = face
        Me.Direction = direction
    End Sub

    Public Overrides Function ToString() As String
        Dim instructionString As String
        Select Case Move
            Case MoveFaces.TOP : instructionString = "U"
            Case MoveFaces.LEFT : instructionString = "L"
            Case MoveFaces.BACK : instructionString = "B"
            Case MoveFaces.RIGHT : instructionString = "R"
            Case MoveFaces.FRONT : instructionString = "F"
            Case MoveFaces.BOTTOM : instructionString = "D"
            Case Else
                Throw New ArgumentException("Invalid face")
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
        Return New FaceInstruction(Me.Move, Me.Direction)
    End Function
End Class
