<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCacheCreator
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmCacheCreator))
        Me.txtServerName = New System.Windows.Forms.TextBox()
        Me.btnGenerateCache = New System.Windows.Forms.Button()
        Me.btnCheckDB = New System.Windows.Forms.Button()
        Me.btnCheckMarketGroup = New System.Windows.Forms.Button()
        Me.gbCheckingTools = New System.Windows.Forms.GroupBox()
        Me.gbEveHQCacheGeneration = New System.Windows.Forms.GroupBox()
        Me.TextBoxYamlFiles = New System.Windows.Forms.TextBox()
        Me.ButtonYamlFind = New System.Windows.Forms.Button()
        Me.TextBoxEveDbLocation = New System.Windows.Forms.TextBox()
        Me.ButtonEveDbFind = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.OpenFileDialogDb = New System.Windows.Forms.OpenFileDialog()
        Me.FolderBrowserDialogYaml = New System.Windows.Forms.FolderBrowserDialog()
        Me.gbCheckingTools.SuspendLayout()
        Me.gbEveHQCacheGeneration.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtServerName
        '
        Me.txtServerName.Location = New System.Drawing.Point(106, 12)
        Me.txtServerName.Name = "txtServerName"
        Me.txtServerName.Size = New System.Drawing.Size(221, 20)
        Me.txtServerName.TabIndex = 2
        Me.txtServerName.Text = "localhost\SQL2008E"
        '
        'btnGenerateCache
        '
        Me.btnGenerateCache.Location = New System.Drawing.Point(9, 184)
        Me.btnGenerateCache.Name = "btnGenerateCache"
        Me.btnGenerateCache.Size = New System.Drawing.Size(299, 23)
        Me.btnGenerateCache.TabIndex = 5
        Me.btnGenerateCache.Text = "Generate All Cache Files"
        Me.btnGenerateCache.UseVisualStyleBackColor = True
        '
        'btnCheckDB
        '
        Me.btnCheckDB.Location = New System.Drawing.Point(9, 155)
        Me.btnCheckDB.Name = "btnCheckDB"
        Me.btnCheckDB.Size = New System.Drawing.Size(299, 23)
        Me.btnCheckDB.TabIndex = 6
        Me.btnCheckDB.Text = "Check SQL Database"
        Me.btnCheckDB.UseVisualStyleBackColor = True
        '
        'btnCheckMarketGroup
        '
        Me.btnCheckMarketGroup.Location = New System.Drawing.Point(6, 19)
        Me.btnCheckMarketGroup.Name = "btnCheckMarketGroup"
        Me.btnCheckMarketGroup.Size = New System.Drawing.Size(287, 23)
        Me.btnCheckMarketGroup.TabIndex = 9
        Me.btnCheckMarketGroup.Text = "Check Market Groups"
        Me.btnCheckMarketGroup.UseVisualStyleBackColor = True
        '
        'gbCheckingTools
        '
        Me.gbCheckingTools.Controls.Add(Me.btnCheckMarketGroup)
        Me.gbCheckingTools.Location = New System.Drawing.Point(9, 221)
        Me.gbCheckingTools.Name = "gbCheckingTools"
        Me.gbCheckingTools.Size = New System.Drawing.Size(299, 58)
        Me.gbCheckingTools.TabIndex = 11
        Me.gbCheckingTools.TabStop = False
        Me.gbCheckingTools.Text = "Checking Tools"
        '
        'gbEveHQCacheGeneration
        '
        Me.gbEveHQCacheGeneration.Controls.Add(Me.TextBoxYamlFiles)
        Me.gbEveHQCacheGeneration.Controls.Add(Me.ButtonYamlFind)
        Me.gbEveHQCacheGeneration.Controls.Add(Me.TextBoxEveDbLocation)
        Me.gbEveHQCacheGeneration.Controls.Add(Me.ButtonEveDbFind)
        Me.gbEveHQCacheGeneration.Controls.Add(Me.gbCheckingTools)
        Me.gbEveHQCacheGeneration.Controls.Add(Me.btnGenerateCache)
        Me.gbEveHQCacheGeneration.Controls.Add(Me.btnCheckDB)
        Me.gbEveHQCacheGeneration.Location = New System.Drawing.Point(12, 12)
        Me.gbEveHQCacheGeneration.Name = "gbEveHQCacheGeneration"
        Me.gbEveHQCacheGeneration.Size = New System.Drawing.Size(318, 291)
        Me.gbEveHQCacheGeneration.TabIndex = 12
        Me.gbEveHQCacheGeneration.TabStop = False
        Me.gbEveHQCacheGeneration.Text = "EveHQ Cache Generation"
        '
        'TextBoxYamlFiles
        '
        Me.TextBoxYamlFiles.AcceptsReturn = True
        Me.TextBoxYamlFiles.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxYamlFiles.Location = New System.Drawing.Point(9, 39)
        Me.TextBoxYamlFiles.Multiline = True
        Me.TextBoxYamlFiles.Name = "TextBoxYamlFiles"
        Me.TextBoxYamlFiles.ReadOnly = True
        Me.TextBoxYamlFiles.Size = New System.Drawing.Size(137, 62)
        Me.TextBoxYamlFiles.TabIndex = 15
        '
        'ButtonYamlFind
        '
        Me.ButtonYamlFind.Location = New System.Drawing.Point(172, 39)
        Me.ButtonYamlFind.Name = "ButtonYamlFind"
        Me.ButtonYamlFind.Size = New System.Drawing.Size(129, 23)
        Me.ButtonYamlFind.TabIndex = 14
        Me.ButtonYamlFind.Text = "Find yaml files"
        Me.ButtonYamlFind.UseVisualStyleBackColor = True
        '
        'TextBoxEveDbLocation
        '
        Me.TextBoxEveDbLocation.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxEveDbLocation.Location = New System.Drawing.Point(9, 107)
        Me.TextBoxEveDbLocation.Name = "TextBoxEveDbLocation"
        Me.TextBoxEveDbLocation.ReadOnly = True
        Me.TextBoxEveDbLocation.Size = New System.Drawing.Size(137, 13)
        Me.TextBoxEveDbLocation.TabIndex = 13
        Me.TextBoxEveDbLocation.Text = "eve.db file not found"
        '
        'ButtonEveDbFind
        '
        Me.ButtonEveDbFind.Location = New System.Drawing.Point(172, 107)
        Me.ButtonEveDbFind.Name = "ButtonEveDbFind"
        Me.ButtonEveDbFind.Size = New System.Drawing.Size(129, 23)
        Me.ButtonEveDbFind.TabIndex = 12
        Me.ButtonEveDbFind.Text = "Find eve.db"
        Me.ButtonEveDbFind.UseVisualStyleBackColor = True
        '
        'OpenFileDialogDb
        '
        Me.OpenFileDialogDb.FileName = "OpenFileDialogDb"
        '
        'FolderBrowserDialogYaml
        '
        Me.FolderBrowserDialogYaml.RootFolder = System.Environment.SpecialFolder.MyComputer
        Me.FolderBrowserDialogYaml.ShowNewFolderButton = False
        '
        'FrmCacheCreator
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(342, 311)
        Me.Controls.Add(Me.gbEveHQCacheGeneration)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FrmCacheCreator"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "EveHQ Cache Creator"
        Me.gbCheckingTools.ResumeLayout(False)
        Me.gbEveHQCacheGeneration.ResumeLayout(False)
        Me.gbEveHQCacheGeneration.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtServerName As System.Windows.Forms.TextBox
    Friend WithEvents btnGenerateCache As System.Windows.Forms.Button
    Friend WithEvents btnCheckDB As System.Windows.Forms.Button
    Friend WithEvents btnCheckMarketGroup As System.Windows.Forms.Button
    Friend WithEvents gbCheckingTools As System.Windows.Forms.GroupBox
    Friend WithEvents gbEveHQCacheGeneration As System.Windows.Forms.GroupBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents ButtonEveDbFind As Button
    Friend WithEvents OpenFileDialogDb As OpenFileDialog
    Friend WithEvents TextBoxEveDbLocation As TextBox
    Friend WithEvents ButtonYamlFind As Button
    Friend WithEvents TextBoxYamlFiles As TextBox
    Friend WithEvents FolderBrowserDialogYaml As FolderBrowserDialog
End Class
