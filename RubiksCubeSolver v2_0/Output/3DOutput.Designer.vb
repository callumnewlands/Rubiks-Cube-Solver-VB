<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class _3DOutput
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(_3DOutput))
        Me.GlControl1 = New OpenTK.GLControl()
        Me.btnUndo = New System.Windows.Forms.Button()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.lblPrev = New System.Windows.Forms.Label()
        Me.lblNext = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnMain = New System.Windows.Forms.Button()
        Me.sldrAmbient = New System.Windows.Forms.TrackBar()
        Me.sldrSpeed = New System.Windows.Forms.TrackBar()
        Me.lblBrightness = New System.Windows.Forms.Label()
        Me.lblSpeed = New System.Windows.Forms.Label()
        Me.lblError = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        CType(Me.sldrAmbient, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sldrSpeed, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GlControl1
        '
        Me.GlControl1.BackColor = System.Drawing.Color.Black
        Me.GlControl1.Location = New System.Drawing.Point(12, 12)
        Me.GlControl1.Name = "GlControl1"
        Me.GlControl1.Size = New System.Drawing.Size(800, 600)
        Me.GlControl1.TabIndex = 0
        Me.GlControl1.VSync = False
        '
        'btnUndo
        '
        Me.btnUndo.Enabled = False
        Me.btnUndo.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUndo.Location = New System.Drawing.Point(306, 618)
        Me.btnUndo.Name = "btnUndo"
        Me.btnUndo.Size = New System.Drawing.Size(95, 60)
        Me.btnUndo.TabIndex = 1
        Me.btnUndo.Text = "<"
        Me.btnUndo.UseVisualStyleBackColor = True
        '
        'btnNext
        '
        Me.btnNext.Font = New System.Drawing.Font("Microsoft Sans Serif", 27.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNext.Location = New System.Drawing.Point(407, 618)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(95, 60)
        Me.btnNext.TabIndex = 2
        Me.btnNext.Text = ">"
        Me.btnNext.UseVisualStyleBackColor = True
        '
        'lblPrev
        '
        Me.lblPrev.AutoSize = True
        Me.lblPrev.Font = New System.Drawing.Font("Microsoft Sans Serif", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPrev.ForeColor = System.Drawing.Color.DarkGray
        Me.lblPrev.Location = New System.Drawing.Point(40, 11)
        Me.lblPrev.Name = "lblPrev"
        Me.lblPrev.Size = New System.Drawing.Size(57, 39)
        Me.lblPrev.TabIndex = 5
        Me.lblPrev.Text = "- - "
        Me.lblPrev.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblNext
        '
        Me.lblNext.AutoSize = True
        Me.lblNext.Font = New System.Drawing.Font("Microsoft Sans Serif", 25.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNext.ForeColor = System.Drawing.Color.Black
        Me.lblNext.Location = New System.Drawing.Point(103, 11)
        Me.lblNext.Name = "lblNext"
        Me.lblNext.Size = New System.Drawing.Size(57, 39)
        Me.lblNext.TabIndex = 6
        Me.lblNext.Text = "- - "
        Me.lblNext.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lblPrev)
        Me.Panel1.Controls.Add(Me.lblNext)
        Me.Panel1.Location = New System.Drawing.Point(579, 618)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(208, 60)
        Me.Panel1.TabIndex = 7
        '
        'btnMain
        '
        Me.btnMain.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMain.Location = New System.Drawing.Point(12, 629)
        Me.btnMain.Name = "btnMain"
        Me.btnMain.Size = New System.Drawing.Size(101, 39)
        Me.btnMain.TabIndex = 8
        Me.btnMain.Text = "Main Menu"
        Me.btnMain.UseVisualStyleBackColor = True
        '
        'sldrAmbient
        '
        Me.sldrAmbient.Location = New System.Drawing.Point(839, 172)
        Me.sldrAmbient.Maximum = 30
        Me.sldrAmbient.Minimum = 5
        Me.sldrAmbient.Name = "sldrAmbient"
        Me.sldrAmbient.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.sldrAmbient.Size = New System.Drawing.Size(45, 130)
        Me.sldrAmbient.TabIndex = 1
        Me.sldrAmbient.TickStyle = System.Windows.Forms.TickStyle.None
        Me.sldrAmbient.Value = 23
        '
        'sldrSpeed
        '
        Me.sldrSpeed.Location = New System.Drawing.Point(835, 342)
        Me.sldrSpeed.Maximum = 19
        Me.sldrSpeed.Minimum = 1
        Me.sldrSpeed.Name = "sldrSpeed"
        Me.sldrSpeed.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.sldrSpeed.Size = New System.Drawing.Size(45, 130)
        Me.sldrSpeed.TabIndex = 5
        Me.sldrSpeed.Value = 10
        '
        'lblBrightness
        '
        Me.lblBrightness.Location = New System.Drawing.Point(818, 137)
        Me.lblBrightness.Name = "lblBrightness"
        Me.lblBrightness.Size = New System.Drawing.Size(66, 32)
        Me.lblBrightness.TabIndex = 9
        Me.lblBrightness.Text = "Cube " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Brightness"
        Me.lblBrightness.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSpeed
        '
        Me.lblSpeed.Location = New System.Drawing.Point(818, 307)
        Me.lblSpeed.Name = "lblSpeed"
        Me.lblSpeed.Size = New System.Drawing.Size(66, 32)
        Me.lblSpeed.TabIndex = 10
        Me.lblSpeed.Text = "Animation Speed"
        Me.lblSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblError
        '
        Me.lblError.AutoSize = True
        Me.lblError.BackColor = System.Drawing.SystemColors.Window
        Me.lblError.Font = New System.Drawing.Font("Microsoft Sans Serif", 72.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblError.ForeColor = System.Drawing.Color.Red
        Me.lblError.Location = New System.Drawing.Point(15, 87)
        Me.lblError.Name = "lblError"
        Me.lblError.Size = New System.Drawing.Size(794, 432)
        Me.lblError.TabIndex = 11
        Me.lblError.Text = "3D Output Is " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Not Supported " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "By Your Graphics" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Library"
        Me.lblError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblError.Visible = False
        '
        '_3DOutput
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(898, 686)
        Me.Controls.Add(Me.lblError)
        Me.Controls.Add(Me.lblSpeed)
        Me.Controls.Add(Me.lblBrightness)
        Me.Controls.Add(Me.sldrSpeed)
        Me.Controls.Add(Me.sldrAmbient)
        Me.Controls.Add(Me.btnMain)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnNext)
        Me.Controls.Add(Me.btnUndo)
        Me.Controls.Add(Me.GlControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "_3DOutput"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Output"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.sldrAmbient, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sldrSpeed, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GlControl1 As OpenTK.GLControl
    Friend WithEvents btnUndo As Button
    Friend WithEvents btnNext As Button
    Friend WithEvents lblPrev As Label
    Friend WithEvents lblNext As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents btnMain As Button
    Friend WithEvents sldrAmbient As TrackBar
    Friend WithEvents sldrSpeed As TrackBar
    Friend WithEvents lblBrightness As Label
    Friend WithEvents lblSpeed As Label
    Friend WithEvents lblError As Label
End Class
