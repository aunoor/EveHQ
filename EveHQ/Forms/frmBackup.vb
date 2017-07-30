'==============================================================================
'
' EveHQ - An Eve-Online™ character assistance application
' Copyright © 2005-2015  EveHQ Development Team
'
' This file is part of EveHQ.
'
' The source code for EveHQ is free and you may redistribute 
' it and/or modify it under the terms of the MIT License. 
'
' Refer to the NOTICES file in the root folder of EVEHQ source
' project for details of 3rd party components that are covered
' under their own, separate licenses.
'
' EveHQ is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the MIT 
' license below for details.
'
' ------------------------------------------------------------------------------
'
' The MIT License (MIT)
'
' Copyright © 2005-2015  EveHQ Development Team
'
' Permission is hereby granted, free of charge, to any person obtaining a copy
' of this software and associated documentation files (the "Software"), to deal
' in the Software without restriction, including without limitation the rights
' to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
' copies of the Software, and to permit persons to whom the Software is
' furnished to do so, subject to the following conditions:
'
' The above copyright notice and this permission notice shall be included in
' all copies or substantial portions of the Software.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
' IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
' FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
' AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
' LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
' OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
' THE SOFTWARE.
'
' ==============================================================================

Imports EveHQ.Core
Imports System.IO
Imports Microsoft.VisualBasic.FileIO

Namespace Forms

	Public Class FrmBackup

		Private Sub chkAuto_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkAuto.CheckedChanged
			If chkAuto.Checked = False Then
				lblBackupDays.Enabled = False
				lblBackupFreq.Enabled = False
				lblBackupStart.Enabled = False
				lblLastBackup.Enabled = False
				lblLastBackupLbl.Enabled = False
				lblNextBackup.Enabled = False
				lblNextBackupLbl.Enabled = False
				lblStartFormat.Enabled = False
				nudDays.Enabled = False
				dtpStart.Enabled = False
				HQ.Settings.BackupAuto = False
			Else
				lblBackupDays.Enabled = True
				lblBackupFreq.Enabled = True
				lblBackupStart.Enabled = True
				lblLastBackup.Enabled = True
				lblLastBackupLbl.Enabled = True
				lblNextBackup.Enabled = True
				lblNextBackupLbl.Enabled = True
				lblStartFormat.Enabled = True
				nudDays.Enabled = True
				dtpStart.Enabled = True
				HQ.Settings.BackupAuto = True
			End If
		End Sub

		Private Sub frmBackup_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
			nudDays.Tag = CInt(1) : dtpStart.Tag = CInt(1)
			chkAuto.Checked = HQ.Settings.BackupAuto
			nudDays.Value = HQ.Settings.BackupFreq
			dtpStart.Value = HQ.Settings.BackupStart
			nudDays.Tag = 0 : dtpStart.Tag = 0
			Call CalcNextBackup()
			If HQ.Settings.BackupLast.Year < 2000 Then
				lblLastBackup.Text = "<not backed up>"
			Else
				lblLastBackup.Text = HQ.Settings.BackupLast.ToString
			End If
			Call ScanBackups()
		End Sub

		Private Sub nudDays_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles nudDays.ValueChanged
			If nudDays.Tag IsNot Nothing Then
				If nudDays.Tag.ToString = "0" Then
					HQ.Settings.BackupFreq = CInt(nudDays.Value)
				End If
			End If
			Call CalcNextBackup()
		End Sub

		Private Sub dtpStart_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles dtpStart.ValueChanged
			If dtpStart.Tag IsNot Nothing Then
				If dtpStart.Tag.ToString = "0" Then
					HQ.Settings.BackupStart = dtpStart.Value
				End If
			End If
			Call CalcNextBackup()
		End Sub

		Private Sub btnBackup_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBackup.Click

			' Check if we have anything to back up!
			Dim noLocations As Boolean = True
			For eveLocation As Integer = 1 To 4
				If HQ.Settings.EveFolder(eveLocation) <> "" Then
					noLocations = False
				End If
			Next
			Do
				If noLocations = True Then
					Dim msg As String = ""
					msg &= "Before trying to backup your Eve-Online settings, you must set the" & ControlChars.CrLf
					msg &= "path to your Eve installation(s) in the Eve Folders section in EveHQ Settings." & ControlChars.CrLf & ControlChars.CrLf
					msg &= "Would you like to do this now?"
					If MessageBox.Show(msg, "Backup Location Required", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
						Exit Sub
					Else
						Using eveHQSettings As New FrmSettings
							eveHQSettings.Tag = "nodeEveFolders"
							eveHQSettings.ShowDialog()
						End Using
					End If
				End If
			Loop Until noLocations = False

			If BackupEveSettings() = True Then
				lblLastBackup.Text = HQ.Settings.BackupLast.ToString
			End If
			Call CalcNextBackup()
			Call ScanBackups()
		End Sub

		Private Sub btnScan_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnScan.Click
			Call ScanBackups()
		End Sub

		Public Sub ScanBackups()
			lvwBackups.BeginUpdate()
			lvwBackups.Items.Clear()

			For Each backupDir As String In My.Computer.FileSystem.GetDirectories(HQ.backupFolder)
				Dim backupFolder = new DirectoryInfo(backupDir)
				For Each backupFile As FileInfo In backupFolder.GetFiles("backup.txt", IO.SearchOption.AllDirectories)
					Using backupFileReader As New StreamReader(backupFile.FullName)
						Dim backedUpOn As String = backupFileReader.ReadLine
						Dim settingsFolderPath As String = backupFileReader.ReadLine
						Dim backupFolderPath As String = backupFileReader.ReadLine
						Dim foundBackupItem = New ListViewItem With {
							.Tag = backupDir,
							.Text = backedUpOn
						}
						Dim bDate As DateTime
						If DateTime.TryParse(foundBackupItem.Text, bDate) = True Then
							foundBackupItem.Name = bDate.ToString("dd-MM-yyyy HH-mm")
						Else
							foundBackupItem.Name = foundBackupItem.Text
						End If

						foundBackupItem.SubItems.Add(settingsFolderPath)
						foundBackupItem.SubItems.Add(backupFolderPath)
						lvwBackups.Items.Add(foundBackupItem)
					End Using
				Next
			Next

			' Do an initial sort of the first column
			lvwBackups.ListViewItemSorter = New ListViewItemComparerName(0, SortOrder.Ascending)
			lvwBackups.Tag = -1
			lvwBackups.Sort()
			lvwBackups.Columns(0).AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent)
			lvwBackups.Columns(1).AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent)
			lvwBackups.Columns(2).AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent)

			lvwBackups.EndUpdate()
		End Sub

		Private Sub btnRestore_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRestore.Click 
			If lvwBackups.SelectedItems.Count = 0 Then
				MessageBox.Show("Please select a backup to restore before proceeding.", "Backup Set Required", MessageBoxButtons.OK, MessageBoxIcon.Information)
				Exit Sub
			Else
				Call RestoreEveSettings(lvwBackups.SelectedItems(0))
				Call ScanBackups()
			End If
		End Sub

		Private Sub btnResetBackup_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnResetBackup.Click
			If MessageBox.Show("Are you sure you wish to reset the last backup time?", "Confirm Reset", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
				Exit Sub
			End If
			HQ.Settings.BackupLast = CDate("01/01/1999")
			lblLastBackup.Text = "<not backed up>"
			Call CalcNextBackup()
		End Sub

		Private Sub lvwBackups_ColumnClick(ByVal sender As Object, ByVal e As ColumnClickEventArgs) Handles lvwBackups.ColumnClick
			If CInt(lvwBackups.Tag) = e.Column Then
				lvwBackups.ListViewItemSorter = New ListViewItemComparerName(e.Column, SortOrder.Ascending)
				lvwBackups.Tag = -1
			Else
				lvwBackups.ListViewItemSorter = New ListViewItemComparerName(e.Column, SortOrder.Descending)
				lvwBackups.Tag = e.Column
			End If
			' Call the sort method to manually sort.
			lvwBackups.Sort()
		End Sub

		Public Sub CalcNextBackup()
			Dim nextBackup As Date = HQ.Settings.BackupStart
			If HQ.Settings.BackupLast > nextBackup Then
				nextBackup = HQ.Settings.BackupLast
			End If
			nextBackup = DateAdd(DateInterval.Day, HQ.Settings.BackupFreq, nextBackup)
			lblNextBackup.Text = nextBackup.ToString
		End Sub

		Public Function BackupEveSettings() As Boolean
			Dim backupTime As Date = Now
			Dim timeStamp As String = Format(backupTime, "dd-MM-yyyy HH-mm")
			Dim settingsFound As Boolean = True

			Try
				For eveLocation As Integer = 1 To 4
					If HQ.Settings.EveFolder(eveLocation) <> "" Then
						settingsFound = SaveEveSettings(eveLocation, timeStamp)
					End If
				Next
				HQ.Settings.BackupLast = backupTime
				If settingsFound Then
					HQ.Settings.BackupLastResult = 1
				Else
					HQ.Settings.BackupLastResult = -1
				End If
				Return True
			Catch e As Exception
				' Try and tidy up
				For eveLocation As Integer = 1 To 4
					Dim chkDir As String = HQ.backupFolder & "Location " & eveLocation & timeStamp
					If My.Computer.FileSystem.DirectoryExists(chkDir) = True Then
						My.Computer.FileSystem.DeleteDirectory(chkDir, CType(DeleteDirectoryOption.DeleteAllContents, UIOption), RecycleOption.DeletePermanently)
					End If
				Next
				Dim msg As String = "Error Performing Backup"
				msg &= ControlChars.CrLf & e.Message & ControlChars.CrLf
				MessageBox.Show(msg, "Backup Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
				HQ.Settings.BackupLastResult = 0
				Return False
			End Try
		End Function

		Private Function SaveEveSettings(locationIndex As Integer, timeStamp As String) As Boolean
			Dim locationFolderPath As String = Nothing
			Dim locationFolderName As String = Nothing
			If Not TryGetLocationFolder(HQ.Settings.EveFolder(locationIndex), locationFolderPath, locationFolderName) Then
				Return False
			End If

			Dim settingsFolders As IEnumerable(Of String) = GetSettingsFolders(locationFolderPath)

			If Not settingsFolders.Any() Then
				Return False
			End If

			Dim backupFolder As String = Path.Combine(HQ.backupFolder, timeStamp, locationFolderName)
			BackupAllSettingsProfiles(locationFolderPath, settingsFolders, backupFolder)
			SaveBackupInfo(timeStamp, locationFolderPath, backupFolder)

			Return True
		End Function

		Private Sub BackupAllSettingsProfiles(eveFolder As String, settingsFolders As IEnumerable(Of String), backupFolder As String)
			For Each settingsFolder As String In settingsFolders
				Dim sourceFolder As String = Path.Combine(eveFolder, settingsFolder)
				Dim destinationFolder As String = Path.Combine(backupFolder, settingsFolder)
				My.Computer.FileSystem.CopyDirectory(sourceFolder, destinationFolder, True)
			Next
		End Sub

		Private Function TryGetLocationFolder(eveLocation As String, ByRef locationFolderPath As String, ByRef locationFolderName As String) As Boolean
			Dim eveDataFolder = New DirectoryInfo( '
				Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CCP", "Eve"))
			Dim folderNamePattern As String = $"{eveLocation.Replace("\", "_").Replace(":", "").Replace(" ", "_").ToLower}_*"
			Dim folder As DirectoryInfo = eveDataFolder.GetDirectories(folderNamePattern, IO.SearchOption.TopDirectoryOnly).FirstOrDefault()

			If folder Is Nothing Then
				Return False
			End If

			locationFolderPath = folder.FullName
			locationFolderName = folder.Name

			Return True
		End Function

		Private Sub SaveBackupInfo(backedUpOn As String, sourceFolderPath As String, backupFolderPath As String)
			Using writer = New StreamWriter(Path.Combine(backupFolderPath, "backup.txt"))
				writer.WriteLine(backedUpOn)
				writer.WriteLine(sourceFolderPath)
				writer.WriteLine(backupFolderPath)
			End Using
		End Sub

		Private Function GetSettingsFolders(eveFolder As String) As String()
			Dim eveDirectoryInfo = new DirectoryInfo(eveFolder)
			Return eveDirectoryInfo.GetDirectories("settings*", IO.SearchOption.TopDirectoryOnly).Select(Function(info) info.Name).ToArray()
		End Function

		Private Function RestoreEveSettings(backupItem As ListViewItem) As Boolean
			If MessageBox.Show("Are you sure you wish to restore this backup?", "Confirm Restore", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
				Return False
			End If

			Try
				Dim sourceFolder As String = backupItem.SubItems(2).Text
				Dim destinationFolder As String = backupItem.SubItems(1).Text

				Dim settingsFolders As DirectoryInfo() = new DirectoryInfo(sourceFolder).GetDirectories()
				For Each settingsFolder As DirectoryInfo In settingsFolders
					My.Computer.FileSystem.CopyDirectory(settingsFolder.FullName, Path.Combine(destinationFolder, settingsFolder.Name), True)
				Next

				Return True
			Catch e As Exception
				Dim msg As String = $"Error Performing Restore{ControlChars.CrLf}{e.Message}{ControlChars.CrLf}"
				MessageBox.Show(msg, "Backup Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
				Return False
			End Try
		End Function
		
	End Class
End NameSpace