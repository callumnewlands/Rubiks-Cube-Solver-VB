Public Class Processing
    Private _cube As Cube

    Public Sub New(ByRef cube As Cube)
        InitializeComponent()
        Me._cube = cube
    End Sub

    Private Sub Processing_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        lblText.Visible = True
        lblText.Refresh()
        'copy the initial cube before solving it
        Dim scrambledCorners() As Corner = Copy(_cube.Corners)
        Dim scrambledEdges() As Edge = Copy(_cube.Edges)
        _cube.Solve(Me)

        Dim outputForm As New _3DOutput(New Cube(scrambledCorners, scrambledEdges, _cube.Instructions))
        outputForm.Show()
        Me.Close()
    End Sub

End Class
