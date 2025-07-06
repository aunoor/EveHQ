Imports EveHQ.CoreLib

Namespace Controls.DBConfigs
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class DBCCorpInfoConfig
        Inherits DevComponents.DotNetBar.Office2007Form

        'Form overrides dispose to clean up the component list.
        <System.Diagnostics.DebuggerNonUserCode()>
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
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DBCCorpInfoConfig))
            Me.btnCancel = New DevComponents.DotNetBar.ButtonX
            Me.btnAccept = New DevComponents.DotNetBar.ButtonX
            Me.cboCorps = New DevComponents.DotNetBar.Controls.ComboBoxEx
            Me.lblDefaultCorp = New DevComponents.DotNetBar.LabelX
            Me.SuspendLayout()
            '
            'btnCancel
            '
            Me.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
            Me.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
            Me.btnCancel.Location = New System.Drawing.Point(177, 49)
            Me.btnCancel.Name = "btnCancel"
            Me.btnCancel.Size = New System.Drawing.Size(75, 23)
            Me.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
            Me.btnCancel.TabIndex = 11
            Me.btnCancel.Text = "Cancel"
            '
            'btnAccept
            '
            Me.btnAccept.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
            Me.btnAccept.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
            Me.btnAccept.Location = New System.Drawing.Point(96, 49)
            Me.btnAccept.Name = "btnAccept"
            Me.btnAccept.Size = New System.Drawing.Size(75, 23)
            Me.btnAccept.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
            Me.btnAccept.TabIndex = 14
            Me.btnAccept.Text = "Accept"
            '
            'cboCorps
            '
            Me.cboCorps.DisplayMember = "Text"
            Me.cboCorps.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
            Me.cboCorps.FormattingEnabled = True
            Me.cboCorps.ItemHeight = 15
            Me.cboCorps.Location = New System.Drawing.Point(84, 12)
            Me.cboCorps.Name = "cboCorps"
            Me.cboCorps.Size = New System.Drawing.Size(168, 21)
            Me.cboCorps.Sorted = True
            Me.cboCorps.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
            Me.cboCorps.TabIndex = 13
            '
            'lblDefaultCorp
            '
            Me.lblDefaultCorp.AutoSize = True
            '
            '
            '
            Me.lblDefaultCorp.BackgroundStyle.Class = ""
            Me.lblDefaultCorp.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
            Me.lblDefaultCorp.Location = New System.Drawing.Point(12, 17)
            Me.lblDefaultCorp.Name = "lblDefaultCorp"
            Me.lblDefaultCorp.Size = New System.Drawing.Size(65, 16)
            Me.lblDefaultCorp.TabIndex = 12
            Me.lblDefaultCorp.Text = "Default Corporation:"
            '
            'DBCCorpInfoConfig
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(264, 81)
            Me.Controls.Add(Me.btnCancel)
            Me.Controls.Add(Me.btnAccept)
            Me.Controls.Add(Me.cboCorps)
            Me.Controls.Add(Me.lblDefaultCorp)
            Me.DoubleBuffered = True
            Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "DBCCorpInfoConfig"
            Me.ShowIcon = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Corporation Info Configuration"
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub

        Public Sub New()

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            ' Load the combo box with the corp info
            cboCorps.BeginUpdate()
            cboCorps.Items.Clear()
            For Each corp As EveHQ.CoreLib.Corporation In HQ.Settings.Corporations.Values
                '                If corp.Active = True Then
                cboCorps.Items.Add(corp.Name)
                '                End If
            Next
            cboCorps.EndUpdate()

        End Sub
        Friend WithEvents btnCancel As DevComponents.DotNetBar.ButtonX
        Friend WithEvents btnAccept As DevComponents.DotNetBar.ButtonX
        Friend WithEvents cboCorps As DevComponents.DotNetBar.Controls.ComboBoxEx
        Friend WithEvents lblDefaultCorp As DevComponents.DotNetBar.LabelX
    End Class
End Namespace
