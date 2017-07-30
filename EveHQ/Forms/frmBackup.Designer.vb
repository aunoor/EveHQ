Namespace Forms
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class FrmBackup
        Inherits DevComponents.DotNetBar.Office2007Form

        'Form overrides dispose to clean up the component list.
        <System.Diagnostics.DebuggerNonUserCode()> _
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer

        'NOTE: The following procedure is required by the Windows Form Designer
        'It can be modified using the Windows Form Designer.  
        'Do not modify it using the code editor.
        <System.Diagnostics.DebuggerStepThrough()> _
        Private Sub InitializeComponent()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmBackup))
		Me.lvwBackups = New System.Windows.Forms.ListView()
		Me.BackupDate = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
		Me.BackedUpFrom = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
		Me.BackedUpTo = CType(New System.Windows.Forms.ColumnHeader(),System.Windows.Forms.ColumnHeader)
		Me.btnScan = New System.Windows.Forms.Button()
		Me.gpEveBackup = New DevComponents.DotNetBar.Controls.GroupPanel()
		Me.btnResetBackup = New System.Windows.Forms.Button()
		Me.chkAuto = New System.Windows.Forms.CheckBox()
		Me.btnBackup = New System.Windows.Forms.Button()
		Me.lblBackupFreq = New System.Windows.Forms.Label()
		Me.lblNextBackupLbl = New System.Windows.Forms.Label()
		Me.nudDays = New System.Windows.Forms.NumericUpDown()
		Me.lblLastBackup = New System.Windows.Forms.Label()
		Me.lblBackupDays = New System.Windows.Forms.Label()
		Me.lblNextBackup = New System.Windows.Forms.Label()
		Me.lblBackupStart = New System.Windows.Forms.Label()
		Me.lblLastBackupLbl = New System.Windows.Forms.Label()
		Me.dtpStart = New System.Windows.Forms.DateTimePicker()
		Me.lblStartFormat = New System.Windows.Forms.Label()
		Me.gpEveRestore = New DevComponents.DotNetBar.Controls.GroupPanel()
		Me.btnRestore = New System.Windows.Forms.Button()
		Me.gpEveBackup.SuspendLayout
		CType(Me.nudDays,System.ComponentModel.ISupportInitialize).BeginInit
		Me.gpEveRestore.SuspendLayout
		Me.SuspendLayout
		'
		'lvwBackups
		'
		Me.lvwBackups.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.lvwBackups.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.BackupDate, Me.BackedUpFrom, Me.BackedUpTo})
		Me.lvwBackups.FullRowSelect = true
		Me.lvwBackups.GridLines = true
		Me.lvwBackups.Location = New System.Drawing.Point(10, 45)
		Me.lvwBackups.Margin = New System.Windows.Forms.Padding(10)
		Me.lvwBackups.Name = "lvwBackups"
		Me.lvwBackups.Size = New System.Drawing.Size(1214, 542)
		Me.lvwBackups.TabIndex = 1
		Me.lvwBackups.UseCompatibleStateImageBehavior = false
		Me.lvwBackups.View = System.Windows.Forms.View.Details
		'
		'BackupDate
		'
		Me.BackupDate.Text = "Backup Date"
		Me.BackupDate.Width = 100
		'
		'BackedUpFrom
		'
		Me.BackedUpFrom.Text = "Backed Up From"
		Me.BackedUpFrom.Width = 400
		'
		'BackedUpTo
		'
		Me.BackedUpTo.Text = "Backed Up To"
		Me.BackedUpTo.Width = 400
		'
		'btnScan
		'
		Me.btnScan.Location = New System.Drawing.Point(110, 3)
		Me.btnScan.Name = "btnScan"
		Me.btnScan.Size = New System.Drawing.Size(137, 30)
		Me.btnScan.TabIndex = 2
		Me.btnScan.TabStop = false
		Me.btnScan.Text = "Scan Backup Directory"
		Me.btnScan.UseVisualStyleBackColor = true
		'
		'gpEveBackup
		'
		Me.gpEveBackup.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.gpEveBackup.BackColor = System.Drawing.Color.White
		Me.gpEveBackup.CanvasColor = System.Drawing.SystemColors.Control
		Me.gpEveBackup.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
		Me.gpEveBackup.Controls.Add(Me.btnResetBackup)
		Me.gpEveBackup.Controls.Add(Me.chkAuto)
		Me.gpEveBackup.Controls.Add(Me.btnBackup)
		Me.gpEveBackup.Controls.Add(Me.lblBackupFreq)
		Me.gpEveBackup.Controls.Add(Me.lblNextBackupLbl)
		Me.gpEveBackup.Controls.Add(Me.nudDays)
		Me.gpEveBackup.Controls.Add(Me.lblLastBackup)
		Me.gpEveBackup.Controls.Add(Me.lblBackupDays)
		Me.gpEveBackup.Controls.Add(Me.lblNextBackup)
		Me.gpEveBackup.Controls.Add(Me.lblBackupStart)
		Me.gpEveBackup.Controls.Add(Me.lblLastBackupLbl)
		Me.gpEveBackup.Controls.Add(Me.dtpStart)
		Me.gpEveBackup.Controls.Add(Me.lblStartFormat)
		Me.gpEveBackup.DisabledBackColor = System.Drawing.Color.Empty
		Me.gpEveBackup.Location = New System.Drawing.Point(6, 7)
		Me.gpEveBackup.Name = "gpEveBackup"
		Me.gpEveBackup.Size = New System.Drawing.Size(1236, 196)
		Me.gpEveBackup.BackColor = System.Drawing.Color.Transparent
		'
		'
		'
		Me.gpEveBackup.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
		Me.gpEveBackup.Style.BorderBottomWidth = 1
		Me.gpEveBackup.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
		Me.gpEveBackup.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
		Me.gpEveBackup.Style.BorderLeftWidth = 1
		Me.gpEveBackup.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
		Me.gpEveBackup.Style.BorderRightWidth = 1
		Me.gpEveBackup.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
		Me.gpEveBackup.Style.BorderTopWidth = 1
		Me.gpEveBackup.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
		Me.gpEveBackup.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
		Me.gpEveBackup.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
		'
		'
		'
		Me.gpEveBackup.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
		'
		'
		'
		Me.gpEveBackup.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
		Me.gpEveBackup.TabIndex = 2
		Me.gpEveBackup.Text = "Eve Settings Backup"
		'
		'btnResetBackup
		'
		Me.btnResetBackup.Location = New System.Drawing.Point(110, 134)
		Me.btnResetBackup.Name = "btnResetBackup"
		Me.btnResetBackup.Size = New System.Drawing.Size(137, 30)
		Me.btnResetBackup.TabIndex = 13
		Me.btnResetBackup.TabStop = false
		Me.btnResetBackup.Text = "Reset Last Backup"
		Me.btnResetBackup.UseVisualStyleBackColor = true
		'
		'chkAuto
		'
		Me.chkAuto.AutoSize = true
		Me.chkAuto.BackColor = System.Drawing.Color.Transparent
		Me.chkAuto.Location = New System.Drawing.Point(23, 3)
		Me.chkAuto.Name = "chkAuto"
		Me.chkAuto.Size = New System.Drawing.Size(171, 17)
		Me.chkAuto.TabIndex = 3
		Me.chkAuto.Text = "Activate Auto Settings Backup"
		Me.chkAuto.UseVisualStyleBackColor = false
		'
		'btnBackup
		'
		Me.btnBackup.Location = New System.Drawing.Point(10, 134)
		Me.btnBackup.Name = "btnBackup"
		Me.btnBackup.Size = New System.Drawing.Size(92, 30)
		Me.btnBackup.TabIndex = 0
		Me.btnBackup.Text = "Backup Now!"
		Me.btnBackup.UseVisualStyleBackColor = true
		'
		'lblBackupFreq
		'
		Me.lblBackupFreq.AutoSize = true
		Me.lblBackupFreq.BackColor = System.Drawing.Color.Transparent
		Me.lblBackupFreq.Enabled = false
		Me.lblBackupFreq.Location = New System.Drawing.Point(74, 28)
		Me.lblBackupFreq.Name = "lblBackupFreq"
		Me.lblBackupFreq.Size = New System.Drawing.Size(99, 13)
		Me.lblBackupFreq.TabIndex = 1
		Me.lblBackupFreq.Text = "Backup Frequency:"
		'
		'lblNextBackupLbl
		'
		Me.lblNextBackupLbl.AutoSize = true
		Me.lblNextBackupLbl.BackColor = System.Drawing.Color.Transparent
		Me.lblNextBackupLbl.Enabled = false
		Me.lblNextBackupLbl.Location = New System.Drawing.Point(74, 108)
		Me.lblNextBackupLbl.Name = "lblNextBackupLbl"
		Me.lblNextBackupLbl.Size = New System.Drawing.Size(71, 13)
		Me.lblNextBackupLbl.TabIndex = 11
		Me.lblNextBackupLbl.Text = "Next Backup:"
		'
		'nudDays
		'
		Me.nudDays.Enabled = false
		Me.nudDays.Location = New System.Drawing.Point(180, 26)
		Me.nudDays.Maximum = New Decimal(New Integer() {28, 0, 0, 0})
		Me.nudDays.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
		Me.nudDays.Name = "nudDays"
		Me.nudDays.Size = New System.Drawing.Size(36, 21)
		Me.nudDays.TabIndex = 4
		Me.nudDays.Tag = "1"
		Me.nudDays.Value = New Decimal(New Integer() {1, 0, 0, 0})
		'
		'lblLastBackup
		'
		Me.lblLastBackup.AutoSize = true
		Me.lblLastBackup.BackColor = System.Drawing.Color.Transparent
		Me.lblLastBackup.Enabled = false
		Me.lblLastBackup.Location = New System.Drawing.Point(177, 83)
		Me.lblLastBackup.Name = "lblLastBackup"
		Me.lblLastBackup.Size = New System.Drawing.Size(66, 13)
		Me.lblLastBackup.TabIndex = 10
		Me.lblLastBackup.Text = "<unknown>"
		'
		'lblBackupDays
		'
		Me.lblBackupDays.AutoSize = true
		Me.lblBackupDays.BackColor = System.Drawing.Color.Transparent
		Me.lblBackupDays.Enabled = false
		Me.lblBackupDays.Location = New System.Drawing.Point(222, 28)
		Me.lblBackupDays.Name = "lblBackupDays"
		Me.lblBackupDays.Size = New System.Drawing.Size(39, 13)
		Me.lblBackupDays.TabIndex = 3
		Me.lblBackupDays.Text = "(Days)"
		'
		'lblNextBackup
		'
		Me.lblNextBackup.AutoSize = true
		Me.lblNextBackup.BackColor = System.Drawing.Color.Transparent
		Me.lblNextBackup.Enabled = false
		Me.lblNextBackup.Location = New System.Drawing.Point(177, 108)
		Me.lblNextBackup.Name = "lblNextBackup"
		Me.lblNextBackup.Size = New System.Drawing.Size(66, 13)
		Me.lblNextBackup.TabIndex = 9
		Me.lblNextBackup.Text = "<unknown>"
		'
		'lblBackupStart
		'
		Me.lblBackupStart.AutoSize = true
		Me.lblBackupStart.BackColor = System.Drawing.Color.Transparent
		Me.lblBackupStart.Enabled = false
		Me.lblBackupStart.Location = New System.Drawing.Point(74, 58)
		Me.lblBackupStart.Name = "lblBackupStart"
		Me.lblBackupStart.Size = New System.Drawing.Size(87, 13)
		Me.lblBackupStart.TabIndex = 4
		Me.lblBackupStart.Text = "Start Date/Time:"
		'
		'lblLastBackupLbl
		'
		Me.lblLastBackupLbl.AutoSize = true
		Me.lblLastBackupLbl.BackColor = System.Drawing.Color.Transparent
		Me.lblLastBackupLbl.Enabled = false
		Me.lblLastBackupLbl.Location = New System.Drawing.Point(74, 83)
		Me.lblLastBackupLbl.Name = "lblLastBackupLbl"
		Me.lblLastBackupLbl.Size = New System.Drawing.Size(68, 13)
		Me.lblLastBackupLbl.TabIndex = 8
		Me.lblLastBackupLbl.Text = "Last Backup:"
		'
		'dtpStart
		'
		Me.dtpStart.CustomFormat = "dd/MM/yyyy HH:mm"
		Me.dtpStart.Enabled = false
		Me.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom
		Me.dtpStart.Location = New System.Drawing.Point(180, 52)
		Me.dtpStart.Name = "dtpStart"
		Me.dtpStart.ShowUpDown = true
		Me.dtpStart.Size = New System.Drawing.Size(129, 21)
		Me.dtpStart.TabIndex = 5
		Me.dtpStart.Tag = "1"
		Me.dtpStart.Value = New Date(2006, 3, 10, 0, 0, 0, 0)
		'
		'lblStartFormat
		'
		Me.lblStartFormat.AutoSize = true
		Me.lblStartFormat.BackColor = System.Drawing.Color.Transparent
		Me.lblStartFormat.Enabled = false
		Me.lblStartFormat.Location = New System.Drawing.Point(315, 56)
		Me.lblStartFormat.Name = "lblStartFormat"
		Me.lblStartFormat.Size = New System.Drawing.Size(164, 13)
		Me.lblStartFormat.TabIndex = 7
		Me.lblStartFormat.Text = "(in ""dd/mm/yyyy hh:mm"" format)"
		'
		'gpEveRestore
		'
		Me.gpEveRestore.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.gpEveRestore.CanvasColor = System.Drawing.SystemColors.Control
		Me.gpEveRestore.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
		Me.gpEveRestore.Controls.Add(Me.btnScan)
		Me.gpEveRestore.Controls.Add(Me.btnRestore)
		Me.gpEveRestore.Controls.Add(Me.lvwBackups)
		Me.gpEveRestore.DisabledBackColor = System.Drawing.Color.Empty
		Me.gpEveRestore.Location = New System.Drawing.Point(6, 209)
		Me.gpEveRestore.Name = "gpEveRestore"
		Me.gpEveRestore.Size = New System.Drawing.Size(1236, 615)
		Me.gpEveRestore.BackColor = System.Drawing.Color.Transparent
		'
		'
		'
		'Me.gpEveRestore.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
		'Me.gpEveRestore.Style.BackColorGradientAngle = 90
		'Me.gpEveRestore.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
		Me.gpEveRestore.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
		Me.gpEveRestore.Style.BorderBottomWidth = 1
		Me.gpEveRestore.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
		Me.gpEveRestore.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
		Me.gpEveRestore.Style.BorderLeftWidth = 1
		Me.gpEveRestore.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
		Me.gpEveRestore.Style.BorderRightWidth = 1
		Me.gpEveRestore.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
		Me.gpEveRestore.Style.BorderTopWidth = 1
		Me.gpEveRestore.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
		Me.gpEveRestore.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
		Me.gpEveRestore.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
		'
		'
		'
		Me.gpEveRestore.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
		'
		'
		'
		Me.gpEveRestore.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
		Me.gpEveRestore.TabIndex = 3
		Me.gpEveRestore.Text = "Eve Settings Restore"
		'
		'btnRestore
		'
		Me.btnRestore.Location = New System.Drawing.Point(10, 3)
		Me.btnRestore.Name = "btnRestore"
		Me.btnRestore.Size = New System.Drawing.Size(92, 30)
		Me.btnRestore.TabIndex = 2
		Me.btnRestore.Text = "Restore Now!"
		Me.btnRestore.UseVisualStyleBackColor = true
		'
		'FrmBackup
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.Color.White
		Me.ClientSize = New System.Drawing.Size(1252, 836)
		Me.Controls.Add(Me.gpEveRestore)
		Me.Controls.Add(Me.gpEveBackup)
		Me.DoubleBuffered = true
		Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
		Me.MinimumSize = New System.Drawing.Size(500, 450)
		Me.Name = "FrmBackup"
		Me.Text = "Eve Settings Backup"
		Me.gpEveBackup.ResumeLayout(false)
		Me.gpEveBackup.PerformLayout
		CType(Me.nudDays,System.ComponentModel.ISupportInitialize).EndInit
		Me.gpEveRestore.ResumeLayout(false)
		Me.ResumeLayout(false)

End Sub
        Friend WithEvents lvwBackups As System.Windows.Forms.ListView
        Friend WithEvents btnScan As System.Windows.Forms.Button
        Friend WithEvents BackupDate As System.Windows.Forms.ColumnHeader
        Friend WithEvents BackedUpTo As System.Windows.Forms.ColumnHeader
        Friend WithEvents btnResetBackup As System.Windows.Forms.Button
        Friend WithEvents chkAuto As System.Windows.Forms.CheckBox
        Friend WithEvents btnBackup As System.Windows.Forms.Button
        Friend WithEvents lblBackupFreq As System.Windows.Forms.Label
        Friend WithEvents lblNextBackupLbl As System.Windows.Forms.Label
        Friend WithEvents nudDays As System.Windows.Forms.NumericUpDown
        Friend WithEvents lblLastBackup As System.Windows.Forms.Label
        Friend WithEvents lblBackupDays As System.Windows.Forms.Label
        Friend WithEvents lblNextBackup As System.Windows.Forms.Label
        Friend WithEvents lblBackupStart As System.Windows.Forms.Label
        Friend WithEvents lblLastBackupLbl As System.Windows.Forms.Label
        Friend WithEvents dtpStart As System.Windows.Forms.DateTimePicker
        Friend WithEvents lblStartFormat As System.Windows.Forms.Label
		Private WithEvents gpEveBackup As DevComponents.DotNetBar.Controls.GroupPanel
		Private WithEvents gpEveRestore As DevComponents.DotNetBar.Controls.GroupPanel
		Friend WithEvents BackedUpFrom As ColumnHeader
		Friend WithEvents btnRestore As Button
	End Class
End NameSpace