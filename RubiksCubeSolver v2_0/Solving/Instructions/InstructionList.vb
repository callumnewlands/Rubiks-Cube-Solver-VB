Imports RubiksCubeSolver_v2_0.Helpers

<Serializable()> Public Class InstructionList
    Inherits List(Of Instruction)

    Public Sub AddFaceTurn(ByVal face As MoveFaces, ByVal direction As Direction)
        If direction = Direction.NO_CHANGE Then Return

        Me.Add(New FaceInstruction(face, direction))

    End Sub

    Public Sub AddOrientationChange(ByVal axis As Axis, ByVal direction As Direction)
        If direction = Direction.NO_CHANGE Then Return

        Me.Add(New CubeInstruction(axis, direction))

    End Sub


    Public Sub Optimise()

        Dim anySimplificationsMade As Boolean = False
        Do
            anySimplificationsMade = False
            Dim ptr As Integer = 0

            While ptr < Me.Count - 1
                Dim currentInstruction As Instruction = Me.ElementAt(ptr)
                Dim nextInstruction As Instruction = Me.ElementAt(ptr + 1)

                If currentInstruction.GetType() <> nextInstruction.GetType() Then
                    ptr += 1
                    Continue While
                End If
                If currentInstruction.Move <> nextInstruction.Move Then
                    ptr += 1
                    Continue While
                End If

                Dim directionSum As Integer = Math.Abs((currentInstruction.Direction + nextInstruction.Direction) Mod 4)
                If directionSum = 3 Then directionSum = -1
                currentInstruction.Direction = directionSum
                If currentInstruction.Direction = Direction.NO_CHANGE Then
                    Me.RemoveAt(ptr)
                    ptr -= 1
                End If
                Me.RemoveAt(ptr + 1)
                ptr += 1
                anySimplificationsMade = True
            End While
        Loop While anySimplificationsMade

    End Sub

    Public Function Copy() As InstructionList
        Dim copyList As New InstructionList
        For i = 0 To Me.Count - 1
            copyList.Add(Me.ElementAt(i))
        Next
        Return copyList
    End Function

    Public Overrides Function ToString() As String
        Dim rtn As String = ""
        For i = 0 To Me.Count - 1
            rtn += Me.ElementAt(i).ToString + ", "
        Next
        Return rtn
    End Function

End Class
