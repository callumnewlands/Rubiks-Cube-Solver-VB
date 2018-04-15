Public Class OutputInstructionList
    Inherits List(Of Instruction)

    Public Property currentInstructionPtr As Integer

    Public Sub New(ByVal instructions As InstructionList)
        For i = 0 To instructions.Count - 1
            Me.Add(instructions.ElementAt(i))
        Next
    End Sub

    Public ReadOnly Property CurrentInstruction() As Instruction
        Get
            Try
                Return ElementAt(currentInstructionPtr)
            Catch
                Return Nothing
            End Try
        End Get
    End Property

    Public ReadOnly Property PreviousInstruction() As Instruction
        Get
            Try
                Return ElementAt(currentInstructionPtr - 1)
            Catch
                Return Nothing
            End Try
        End Get
    End Property
End Class
