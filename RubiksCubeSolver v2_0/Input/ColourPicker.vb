Public Class ColourPicker
    Public colour As Color

    Public Sub New()
        colour = NO_CHANGE_COLOUR
        InitializeComponent()
    End Sub

    Private Sub ConfirmColour_Click(sender As Object, e As EventArgs) Handles ConfirmColour.Click
        Select Case True
            Case Red.Checked
                colour = Color.Red
            Case Blue.Checked
                colour = Color.Blue
            Case Yellow.Checked
                colour = Color.Yellow
            Case Orange.Checked
                colour = Color.DarkOrange
            Case Green.Checked
                colour = Color.Lime
            Case White.Checked
                colour = Color.White
            Case Else
                MsgBox("Error")
                colour = Color.LightGray
        End Select
        Me.Close()
    End Sub

    Private Sub ColourPicker_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        colour = NO_CHANGE_COLOUR
        ConfirmColour.Focus()
    End Sub

    Private Sub CancelColour_Click(sender As Object, e As EventArgs) Handles CancelColour.Click
        Me.Close()
    End Sub
End Class