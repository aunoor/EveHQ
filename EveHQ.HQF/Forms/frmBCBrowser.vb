﻿'==============================================================================
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

Imports System.ComponentModel
Imports System.Drawing
Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Windows.Forms
Imports System.Xml
Imports DevComponents.AdvTree
Imports EveHQ.Core

Namespace Forms

    Public Class FrmBcBrowser

        Dim _currentShip As Ship
        Dim _currentFit As New ArrayList
        Dim _currentFitting As Fitting
        ReadOnly _bcLoadoutCache As String = Path.Combine(HQ.AppDataFolder, "BCLoadoutCache")
        Dim _sourceURL As String = ""

        Public Sub New()

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Create the fittings cache if it doesn't exist!
            Try
                If My.Computer.FileSystem.DirectoryExists(_bcLoadoutCache) = False Then
                    My.Computer.FileSystem.CreateDirectory(_bcLoadoutCache)
                End If
            Catch ex As Exception
                MessageBox.Show("Unable to create the Loadout cache folder. Caching will be disabled which may affect loadout downloads.", "Error Creating Folder", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Try

            ' Add the current list of pilots to the combobox
            cboPilots.BeginUpdate()
            cboPilots.Items.Clear()
            For Each cPilot As EveHQPilot In HQ.Settings.Pilots.Values
                If cPilot.Active = True Then
                    cboPilots.Items.Add(cPilot.Name)
                End If
            Next
            cboPilots.EndUpdate()
            ' Look at the settings for default pilot
            If cboPilots.Items.Count > 0 Then
                If cboPilots.Items.Contains(PluginSettings.HQFSettings.DefaultPilot) = True Then
                    cboPilots.SelectedItem = PluginSettings.HQFSettings.DefaultPilot
                Else
                    cboPilots.SelectedIndex = 0
                End If
            End If

            ' Add the profiles
            cboProfiles.BeginUpdate()
            cboProfiles.Items.Clear()
            For Each newProfile As HQFDamageProfile In HQFDamageProfiles.ProfileList.Values
                cboProfiles.Items.Add(newProfile.Name)
            Next
            cboProfiles.EndUpdate()
            ' Select the default profile
            cboProfiles.SelectedItem = "<Omni-Damage>"

        End Sub

        Public Property ShipType() As Ship
            Get
                Return _currentShip
            End Get
            Set(ByVal value As Ship)
                _currentShip = value
                pbShip.ImageLocation = Core.ImageHandler.GetImageLocation(CInt(_currentShip.ID))
            End Set
        End Property

#Region "Ship Fitting routines"

        Private Sub UpdateSlotColumns()
            ' Clear the columns
            lvwSlots.Columns.Clear()
            ' Add the module name column
            lvwSlots.Columns.Add("colName", "Module Name", 175, HorizontalAlignment.Left, "")
            lvwSlots.Columns.Add("Charge", "Charge Name", 175, HorizontalAlignment.Left, "")
        End Sub

        Private Sub UpdateSlotLayout()
            If _currentShip IsNot Nothing Then
                lvwSlots.BeginUpdate()
                lvwSlots.Items.Clear()
                ' Produce high slots
                For slot As Integer = 1 To _currentShip.HiSlots
                    Dim newSlot As New ListViewItem
                    newSlot.Name = "8_" & slot
                    newSlot.BackColor = Color.FromArgb(CInt(PluginSettings.HQFSettings.HiSlotColour))
                    newSlot.ForeColor = Color.Black
                    newSlot.Group = lvwSlots.Groups.Item("lvwgHighSlots")
                    Call AddUserColumns(_currentShip.HiSlot(slot), newSlot)
                    lvwSlots.Items.Add(newSlot)
                Next
                For slot As Integer = 1 To _currentShip.MidSlots
                    Dim newSlot As New ListViewItem
                    newSlot.Name = "4_" & slot
                    newSlot.BackColor = Color.FromArgb(CInt(PluginSettings.HQFSettings.MidSlotColour))
                    newSlot.ForeColor = Color.Black
                    newSlot.Group = lvwSlots.Groups.Item("lvwgMidSlots")
                    Call AddUserColumns(_currentShip.MidSlot(slot), newSlot)
                    lvwSlots.Items.Add(newSlot)
                Next
                For slot As Integer = 1 To _currentShip.LowSlots
                    Dim newSlot As New ListViewItem
                    newSlot.Name = "2_" & slot
                    newSlot.BackColor = Color.FromArgb(CInt(PluginSettings.HQFSettings.LowSlotColour))
                    newSlot.ForeColor = Color.Black
                    newSlot.Group = lvwSlots.Groups.Item("lvwgLowSlots")
                    Call AddUserColumns(_currentShip.LowSlot(slot), newSlot)
                    lvwSlots.Items.Add(newSlot)
                Next
                For slot As Integer = 1 To _currentShip.RigSlots
                    Dim newSlot As New ListViewItem
                    newSlot.Name = "1_" & slot
                    newSlot.BackColor = Color.FromArgb(CInt(PluginSettings.HQFSettings.RigSlotColour))
                    newSlot.ForeColor = Color.Black
                    newSlot.Group = lvwSlots.Groups.Item("lvwgRigSlots")
                    Call AddUserColumns(_currentShip.RigSlot(slot), newSlot)
                    lvwSlots.Items.Add(newSlot)
                Next
                For slot As Integer = 1 To _currentShip.SubSlots
                    Dim newSlot As New ListViewItem
                    newSlot.Name = "16_" & slot
                    newSlot.BackColor = Color.FromArgb(CInt(PluginSettings.HQFSettings.SubSlotColour))
                    newSlot.ForeColor = Color.Black
                    newSlot.Group = lvwSlots.Groups.Item("lvwgSubSlots")
                    Call AddUserColumns(_currentShip.SubSlot(slot), newSlot)
                    lvwSlots.Items.Add(newSlot)
                Next
                lvwSlots.EndUpdate()
            End If
        End Sub

        Private Sub AddUserColumns(ByVal shipMod As ShipModule, ByVal slotName As ListViewItem)
            ' Add subitems based on the user selected columns
            If shipMod IsNot Nothing Then
                ' Add in the module name
                slotName.Text = shipMod.Name
                If shipMod.LoadedCharge IsNot Nothing Then
                    slotName.SubItems.Add(shipMod.LoadedCharge.Name)
                Else
                    slotName.SubItems.Add("")
                End If
            Else
                slotName.Text = "<Empty>"
                slotName.SubItems.Add("")
            End If
        End Sub

        Private Sub ClearShipSlots()
            If _currentShip IsNot Nothing Then
                For slot As Integer = 1 To _currentShip.HiSlots
                    _currentShip.HiSlot(slot) = Nothing
                Next
                For slot As Integer = 1 To _currentShip.MidSlots
                    _currentShip.MidSlot(slot) = Nothing
                Next
                For slot As Integer = 1 To _currentShip.LowSlots
                    _currentShip.LowSlot(slot) = Nothing
                Next
                For slot As Integer = 1 To _currentShip.RigSlots
                    _currentShip.RigSlot(slot) = Nothing
                Next
                For slot As Integer = 1 To _currentShip.SubSlots
                    _currentShip.SubSlot(slot) = Nothing
                Next
                _currentShip.SlotCollection.Clear()
                _currentShip.FleetSlotCollection.Clear()
                _currentShip.DroneBayItems.Clear()
                _currentShip.DroneBayUsed = 0
                _currentShip.FighterBayItems.Clear()
                _currentShip.FighterBayUsed = 0
                _currentShip.CargoBayItems.Clear()
                _currentShip.CargoBayUsed = 0
            End If
        End Sub
        Private Sub GenerateFittingData()
            ' Let's try and generate a fitting and get some damage info
            If _currentShip IsNot Nothing Then
                If _currentFitting IsNot Nothing Then
                    If cboPilots.SelectedItem IsNot Nothing Then
                        gpStatistics.Enabled = True
                        Dim loadoutPilot As FittingPilot = FittingPilots.HQFPilots(cboPilots.SelectedItem.ToString)
                        Dim loadoutProfile As HQFDamageProfile = HQFDamageProfiles.ProfileList(cboProfiles.SelectedItem.ToString)

                        _currentFitting.PilotName = loadoutPilot.PilotName
                        _currentFitting.BaseShip.DamageProfile = loadoutProfile
                        _currentFitting.ApplyFitting()
                        Dim loadoutShip As Ship = _currentFitting.FittedShip

                        lblEHP.Text = loadoutShip.EffectiveHP.ToString("N0")
                        lblTank.Text = loadoutShip.Attributes(10062).ToString("N2") & " DPS"
                        lblVolley.Text = loadoutShip.Attributes(10028).ToString("N2")
                        lblDPS.Text = loadoutShip.Attributes(10029).ToString("N2")
                        lblShieldResists.Text = loadoutShip.ShieldEMResist.ToString("N0") & "/" & loadoutShip.ShieldExResist.ToString("N0") & "/" & loadoutShip.ShieldKiResist.ToString("N0") & "/" & loadoutShip.ShieldThResist.ToString("N0")
                        lblArmorResists.Text = loadoutShip.ArmorEMResist.ToString("N0") & "/" & loadoutShip.ArmorExResist.ToString("N0") & "/" & loadoutShip.ArmorKiResist.ToString("N0") & "/" & loadoutShip.ArmorThResist.ToString("N0")
                        Dim csr As CapSimResults = Capacitor.CalculateCapStatistics(loadoutShip, False)
                        If csr.CapIsDrained = False Then
                            lblCapacitor.Text = "Stable at " & (csr.MinimumCap / loadoutShip.CapCapacity * 100).ToString("N0") & "%"
                        Else
                            lblCapacitor.Text = "Lasts " & SkillFunctions.TimeToString(csr.TimeToDrain, False)
                        End If
                        lblVelocity.Text = loadoutShip.MaxVelocity.ToString("N2") & " m/s"
                        lblMaxRange.Text = loadoutShip.MaxTargetRange.ToString("N0") & "m"
                        Dim cpu As Double = loadoutShip.CPUUsed / loadoutShip.CPU * 100
                        lblCPU.Text = cpu.ToString("N2") & "%"
                        If cpu > 100 Then
                            lblCPU.ForeColor = Color.Red
                        Else
                            lblCPU.ForeColor = Color.Black
                        End If
                        Dim pg As Double = loadoutShip.PGUsed / loadoutShip.PG * 100
                        lblPG.Text = pg.ToString("N2") & "%"
                        If pg > 100 Then
                            lblPG.ForeColor = Color.Red
                        Else
                            lblPG.ForeColor = Color.Black
                        End If
                        Dim maxOpt As Double = 0
                        For slot As Integer = 1 To loadoutShip.HiSlots
                            Dim shipMod As ShipModule = loadoutShip.HiSlot(slot)
                            If shipMod IsNot Nothing Then
                                If shipMod.Attributes.ContainsKey(54) Then
                                    maxOpt = Math.Max(maxOpt, CDbl(shipMod.Attributes(54)))
                                End If
                            End If
                        Next
                        lblOptimalRange.Text = maxOpt.ToString("N0") & "m"
                    Else
                        gpStatistics.Enabled = False
                    End If
                End If
            End If

        End Sub

        Private Sub cboPilots_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboPilots.SelectedIndexChanged
            Call GenerateFittingData()
        End Sub

        Private Sub cboProfiles_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboProfiles.SelectedIndexChanged
            Call GenerateFittingData()
        End Sub

        Private Sub btnImport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnImport.Click
            If _currentFitting IsNot Nothing Then
                Dim shipName As String = lblShipType.Text
                Dim fittingName As String = lblLoadoutName.Text
                ' If the fitting exists, add a number onto the end
                If Fittings.FittingList.ContainsKey(shipName & ", " & fittingName) = True Then
                    Dim response As Integer = MessageBox.Show("Fitting name already exists. Are you sure you wish to import the fitting?", "Confirm Import for " & shipName, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    If response = DialogResult.Yes Then
                        Dim newFittingName As String
                        Dim revision As Integer = 1
                        Do
                            revision += 1
                            newFittingName = fittingName & " " & revision.ToString
                        Loop Until Fittings.FittingList.ContainsKey(shipName & ", " & newFittingName) = False
                        fittingName = newFittingName
                        MessageBox.Show("New fitting name is '" & fittingName & "'.", "New Fitting Imported", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        Exit Sub
                    End If
                End If
                ' Lets create the fitting
                Dim newFit As Fitting = Fittings.ConvertOldFitToNewFit(shipName & ", " & fittingName, _currentFit)
                Fittings.FittingList.Add(newFit.KeyName, newFit)
                HQFEvents.StartUpdateFittingList = True
            Else
                MessageBox.Show("Please ensure a fitting is loaded before importing.", "Import Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End Sub

#End Region

#Region "UI Routines"

        Private Sub mnuViewLoadout_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuViewLoadout.Click
            Dim cLoadout As Node = adtLoadouts.SelectedNodes(0)
        End Sub
        Private Sub lblLoadoutTopic_LinkClicked(ByVal sender As Object, ByVal e As LinkLabelLinkClickedEventArgs) Handles lblLoadoutTopic.LinkClicked
            Try
                Process.Start(_sourceURL)
            Catch ex As Exception
                MessageBox.Show("Unable to start default web browser. Please check your browser PluginSettings.", "Error Starting Web Browser", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
        End Sub

        Private Sub lblLoadoutTopic_MouseEnter(ByVal sender As Object, ByVal e As EventArgs) Handles lblLoadoutTopic.MouseEnter
            lblTopicAddress.Text = _sourceURL
        End Sub

        Private Sub lblLoadoutTopic_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles lblLoadoutTopic.MouseLeave
            lblTopicAddress.Text = ""
        End Sub

        Private Sub mnuCopyURL_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuCopyURL.Click
            Try
                Clipboard.SetText(_sourceURL)
            Catch ex As Exception
                MessageBox.Show("There was an error copying data to the clipboard: " & ex.Message & ControlChars.CrLf & ControlChars.CrLf & "Please try again.", "Copy to Clipboard Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Try
        End Sub

        Private Sub adtLoadouts_ColumnHeaderMouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles adtLoadouts.ColumnHeaderMouseDown
            Dim ch As DevComponents.AdvTree.ColumnHeader = CType(sender, DevComponents.AdvTree.ColumnHeader)
            AdvTreeSorter.Sort(ch, True, False)
        End Sub

        Private Sub adtLoadouts_NodeDoubleClick(ByVal sender As Object, ByVal e As TreeNodeMouseEventArgs) Handles adtLoadouts.NodeDoubleClick
            If adtLoadouts.SelectedNodes.Count > 0 Then
                Dim cLoadout As Node = adtLoadouts.SelectedNodes(0)
            End If
        End Sub

#End Region

#Region "Context Menu Routines"

        Private Sub ctxSlots_Opening(ByVal sender As Object, ByVal e As CancelEventArgs) Handles ctxSlots.Opening
            If lvwSlots.SelectedItems.Count <> 1 Then
                e.Cancel = True
            End If
        End Sub

        Private Sub mnuShowInfo_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuShowInfo.Click
            Dim selectedSlot As ListViewItem = lvwSlots.SelectedItems(0)
            Dim slotInfo() As String = selectedSlot.Name.Split("_".ToCharArray)
            Dim sModule As New ShipModule
            Select Case CInt(slotInfo(0))
                Case SlotTypes.Rig
                    sModule = _currentFitting.FittedShip.RigSlot(CInt(slotInfo(1)))
                Case SlotTypes.Low
                    sModule = _currentFitting.FittedShip.LowSlot(CInt(slotInfo(1)))
                Case SlotTypes.Mid
                    sModule = _currentFitting.FittedShip.MidSlot(CInt(slotInfo(1)))
                Case SlotTypes.High
                    sModule = _currentFitting.FittedShip.HiSlot(CInt(slotInfo(1)))
                Case SlotTypes.Subsystem
                    sModule = _currentFitting.FittedShip.SubSlot(CInt(slotInfo(1)))
            End Select
            Dim showInfo As New FrmShowInfo
            Dim hPilot As EveHQPilot
            If cboPilots.SelectedItem IsNot Nothing Then
                hPilot = HQ.Settings.Pilots(cboPilots.SelectedItem.ToString)
            Else
                If HQ.Settings.StartupPilot <> "" Then
                    hPilot = HQ.Settings.Pilots(HQ.Settings.StartupPilot)
                Else
                    hPilot = HQ.Settings.Pilots.Values(0)
                End If
            End If
            showInfo.ShowItemDetails(sModule, hPilot)
        End Sub

#End Region

    End Class
End Namespace