<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLogin
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
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.lblEnvironment = New System.Windows.Forms.Label()
        Me.lblOption = New System.Windows.Forms.Label()
        Me.lblInstanceUrl = New System.Windows.Forms.Label()
        Me.cmbEnvironment = New System.Windows.Forms.ComboBox()
        Me.cmbOption = New System.Windows.Forms.ComboBox()
        Me.lblMyUrl = New System.Windows.Forms.Label()
        Me.lblMessage = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnLogin = New System.Windows.Forms.Button()
        Me.btnBuild = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.Location = New System.Drawing.Point(12, 9)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(204, 37)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "Package Builder"
        '
        'lblEnvironment
        '
        Me.lblEnvironment.AutoSize = True
        Me.lblEnvironment.Location = New System.Drawing.Point(16, 60)
        Me.lblEnvironment.Name = "lblEnvironment"
        Me.lblEnvironment.Size = New System.Drawing.Size(66, 13)
        Me.lblEnvironment.TabIndex = 1
        Me.lblEnvironment.Text = "Environment"
        '
        'lblOption
        '
        Me.lblOption.AutoSize = True
        Me.lblOption.Location = New System.Drawing.Point(16, 87)
        Me.lblOption.Name = "lblOption"
        Me.lblOption.Size = New System.Drawing.Size(95, 13)
        Me.lblOption.TabIndex = 2
        Me.lblOption.Text = "Component Option"
        '
        'lblInstanceUrl
        '
        Me.lblInstanceUrl.AutoSize = True
        Me.lblInstanceUrl.Location = New System.Drawing.Point(16, 114)
        Me.lblInstanceUrl.Name = "lblInstanceUrl"
        Me.lblInstanceUrl.Size = New System.Drawing.Size(64, 13)
        Me.lblInstanceUrl.TabIndex = 3
        Me.lblInstanceUrl.Text = "Instance Url"
        '
        'cmbEnvironment
        '
        Me.cmbEnvironment.FormattingEnabled = True
        Me.cmbEnvironment.Items.AddRange(New Object() {"Production", "Sandbox"})
        Me.cmbEnvironment.Location = New System.Drawing.Point(124, 56)
        Me.cmbEnvironment.Name = "cmbEnvironment"
        Me.cmbEnvironment.Size = New System.Drawing.Size(121, 21)
        Me.cmbEnvironment.TabIndex = 4
        '
        'cmbOption
        '
        Me.cmbOption.FormattingEnabled = True
        Me.cmbOption.Items.AddRange(New Object() {"All Component", "Wildcard", "Exclude Managed", "No Packaged Components"})
        Me.cmbOption.Location = New System.Drawing.Point(124, 83)
        Me.cmbOption.Name = "cmbOption"
        Me.cmbOption.Size = New System.Drawing.Size(121, 21)
        Me.cmbOption.TabIndex = 5
        '
        'lblMyUrl
        '
        Me.lblMyUrl.Location = New System.Drawing.Point(124, 114)
        Me.lblMyUrl.Name = "lblMyUrl"
        Me.lblMyUrl.Size = New System.Drawing.Size(121, 13)
        Me.lblMyUrl.TabIndex = 6
        Me.lblMyUrl.Text = "no instance url"
        '
        'lblMessage
        '
        Me.lblMessage.Location = New System.Drawing.Point(19, 141)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(226, 76)
        Me.lblMessage.TabIndex = 7
        Me.lblMessage.Text = "message is here..."
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(36, 232)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 8
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnLogin
        '
        Me.btnLogin.Location = New System.Drawing.Point(156, 232)
        Me.btnLogin.Name = "btnLogin"
        Me.btnLogin.Size = New System.Drawing.Size(75, 23)
        Me.btnLogin.TabIndex = 9
        Me.btnLogin.Text = "Login"
        Me.btnLogin.UseVisualStyleBackColor = True
        '
        'btnBuild
        '
        Me.btnBuild.Location = New System.Drawing.Point(156, 232)
        Me.btnBuild.Name = "btnBuild"
        Me.btnBuild.Size = New System.Drawing.Size(75, 23)
        Me.btnBuild.TabIndex = 10
        Me.btnBuild.Text = "Build"
        Me.btnBuild.UseVisualStyleBackColor = True
        Me.btnBuild.Visible = False
        '
        'frmLogin
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(264, 267)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnBuild)
        Me.Controls.Add(Me.btnLogin)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.lblMessage)
        Me.Controls.Add(Me.lblMyUrl)
        Me.Controls.Add(Me.cmbOption)
        Me.Controls.Add(Me.cmbEnvironment)
        Me.Controls.Add(Me.lblInstanceUrl)
        Me.Controls.Add(Me.lblOption)
        Me.Controls.Add(Me.lblEnvironment)
        Me.Controls.Add(Me.lblTitle)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "frmLogin"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Package Builder Login"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblTitle As Label
    Friend WithEvents lblEnvironment As Label
    Friend WithEvents lblOption As Label
    Friend WithEvents lblInstanceUrl As Label
    Friend WithEvents cmbEnvironment As ComboBox
    Friend WithEvents cmbOption As ComboBox
    Friend WithEvents lblMyUrl As Label
    Friend WithEvents lblMessage As Label
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnLogin As Button
    Friend WithEvents btnBuild As Button
End Class
