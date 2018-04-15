<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ColourPicker
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ColourPicker))
        Me.CancelColour = New System.Windows.Forms.Button()
        Me.ConfirmColour = New System.Windows.Forms.Button()
        Me.Green = New System.Windows.Forms.RadioButton()
        Me.Orange = New System.Windows.Forms.RadioButton()
        Me.Yellow = New System.Windows.Forms.RadioButton()
        Me.White = New System.Windows.Forms.RadioButton()
        Me.Blue = New System.Windows.Forms.RadioButton()
        Me.Red = New System.Windows.Forms.RadioButton()
        Me.SuspendLayout()
        '
        'CancelColour
        '
        Me.CancelColour.Location = New System.Drawing.Point(90, 98)
        Me.CancelColour.Name = "CancelColour"
        Me.CancelColour.Size = New System.Drawing.Size(75, 23)
        Me.CancelColour.TabIndex = 15
        Me.CancelColour.Text = "Cancel?"
        Me.CancelColour.UseVisualStyleBackColor = True
        '
        'ConfirmColour
        '
        Me.ConfirmColour.Location = New System.Drawing.Point(9, 98)
        Me.ConfirmColour.Name = "ConfirmColour"
        Me.ConfirmColour.Size = New System.Drawing.Size(75, 23)
        Me.ConfirmColour.TabIndex = 14
        Me.ConfirmColour.Text = "Confirm?"
        Me.ConfirmColour.UseVisualStyleBackColor = True
        '
        'Green
        '
        Me.Green.Appearance = System.Windows.Forms.Appearance.Button
        Me.Green.AutoSize = True
        Me.Green.BackColor = System.Drawing.Color.Lime
        Me.Green.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Green.Location = New System.Drawing.Point(85, 39)
        Me.Green.Name = "Green"
        Me.Green.Size = New System.Drawing.Size(46, 23)
        Me.Green.TabIndex = 13
        Me.Green.TabStop = True
        Me.Green.Text = "Green"
        Me.Green.UseVisualStyleBackColor = False
        '
        'Orange
        '
        Me.Orange.Appearance = System.Windows.Forms.Appearance.Button
        Me.Orange.AutoSize = True
        Me.Orange.BackColor = System.Drawing.Color.DarkOrange
        Me.Orange.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Orange.Location = New System.Drawing.Point(79, 68)
        Me.Orange.Name = "Orange"
        Me.Orange.Size = New System.Drawing.Size(52, 23)
        Me.Orange.TabIndex = 12
        Me.Orange.TabStop = True
        Me.Orange.Text = "Orange"
        Me.Orange.UseVisualStyleBackColor = False
        '
        'Yellow
        '
        Me.Yellow.Appearance = System.Windows.Forms.Appearance.Button
        Me.Yellow.AutoSize = True
        Me.Yellow.BackColor = System.Drawing.Color.Yellow
        Me.Yellow.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Yellow.Location = New System.Drawing.Point(83, 10)
        Me.Yellow.Name = "Yellow"
        Me.Yellow.Size = New System.Drawing.Size(48, 23)
        Me.Yellow.TabIndex = 11
        Me.Yellow.TabStop = True
        Me.Yellow.Text = "Yellow"
        Me.Yellow.UseVisualStyleBackColor = False
        '
        'White
        '
        Me.White.Appearance = System.Windows.Forms.Appearance.Button
        Me.White.AutoSize = True
        Me.White.BackColor = System.Drawing.Color.White
        Me.White.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.White.Location = New System.Drawing.Point(36, 39)
        Me.White.Name = "White"
        Me.White.Size = New System.Drawing.Size(45, 23)
        Me.White.TabIndex = 10
        Me.White.TabStop = True
        Me.White.Text = "White"
        Me.White.UseVisualStyleBackColor = False
        '
        'Blue
        '
        Me.Blue.Appearance = System.Windows.Forms.Appearance.Button
        Me.Blue.AutoSize = True
        Me.Blue.BackColor = System.Drawing.Color.Blue
        Me.Blue.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Blue.ForeColor = System.Drawing.Color.White
        Me.Blue.Location = New System.Drawing.Point(36, 68)
        Me.Blue.Name = "Blue"
        Me.Blue.Size = New System.Drawing.Size(38, 23)
        Me.Blue.TabIndex = 9
        Me.Blue.TabStop = True
        Me.Blue.Text = "Blue"
        Me.Blue.UseVisualStyleBackColor = False
        '
        'Red
        '
        Me.Red.Appearance = System.Windows.Forms.Appearance.Button
        Me.Red.AutoSize = True
        Me.Red.BackColor = System.Drawing.Color.Red
        Me.Red.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Red.Location = New System.Drawing.Point(36, 10)
        Me.Red.Name = "Red"
        Me.Red.Size = New System.Drawing.Size(37, 23)
        Me.Red.TabIndex = 8
        Me.Red.TabStop = True
        Me.Red.Text = "Red"
        Me.Red.UseVisualStyleBackColor = False
        '
        'ColourPicker
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(174, 131)
        Me.Controls.Add(Me.CancelColour)
        Me.Controls.Add(Me.ConfirmColour)
        Me.Controls.Add(Me.Green)
        Me.Controls.Add(Me.Orange)
        Me.Controls.Add(Me.Yellow)
        Me.Controls.Add(Me.White)
        Me.Controls.Add(Me.Blue)
        Me.Controls.Add(Me.Red)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ColourPicker"
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Colour Picker"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CancelColour As System.Windows.Forms.Button
    Friend WithEvents ConfirmColour As System.Windows.Forms.Button
    Friend WithEvents Green As System.Windows.Forms.RadioButton
    Friend WithEvents Orange As System.Windows.Forms.RadioButton
    Friend WithEvents Yellow As System.Windows.Forms.RadioButton
    Friend WithEvents White As System.Windows.Forms.RadioButton
    Friend WithEvents Blue As System.Windows.Forms.RadioButton
    Friend WithEvents Red As System.Windows.Forms.RadioButton
End Class
