<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BuildProcess
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
        Me.bgw = New System.ComponentModel.BackgroundWorker()
        Me.progressBar = New System.Windows.Forms.ProgressBar()
        Me.lblMessage = New System.Windows.Forms.Label()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.textBox = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'bgw
        '
        '
        'progressBar
        '
        Me.progressBar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.progressBar.Location = New System.Drawing.Point(14, 14)
        Me.progressBar.Name = "progressBar"
        Me.progressBar.Size = New System.Drawing.Size(881, 27)
        Me.progressBar.TabIndex = 0
        '
        'lblMessage
        '
        Me.lblMessage.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblMessage.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMessage.Location = New System.Drawing.Point(14, 48)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(881, 27)
        Me.lblMessage.TabIndex = 1
        Me.lblMessage.Text = "process message"
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.Enabled = False
        Me.btnExit.Location = New System.Drawing.Point(417, 409)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(75, 23)
        Me.btnExit.TabIndex = 2
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'textBox
        '
        Me.textBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.textBox.Font = New System.Drawing.Font("Consolas", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.textBox.Location = New System.Drawing.Point(14, 78)
        Me.textBox.MaxLength = 10485760
        Me.textBox.Multiline = True
        Me.textBox.Name = "textBox"
        Me.textBox.ReadOnly = True
        Me.textBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.textBox.Size = New System.Drawing.Size(881, 325)
        Me.textBox.TabIndex = 3
        '
        'BuildProcess
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(909, 444)
        Me.Controls.Add(Me.textBox)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.lblMessage)
        Me.Controls.Add(Me.progressBar)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "BuildProcess"
        Me.Text = "BuildProcess"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents bgw As System.ComponentModel.BackgroundWorker
    Friend WithEvents progressBar As ProgressBar
    Friend WithEvents lblMessage As Label
    Friend WithEvents btnExit As Button
    Friend WithEvents textBox As TextBox
End Class
