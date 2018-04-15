Imports RubiksCubeSolver_v2_0.Helpers

<Serializable()> Public MustInherit Class Instruction

    Public Property Move As Integer
    Public Property Direction As Direction

    Public MustOverride Overrides Function ToString() As String

    Public MustOverride Function Copy() As Instruction

End Class