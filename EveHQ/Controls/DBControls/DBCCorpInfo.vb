Imports System.Globalization
Imports EveHQ.Core
Imports EveHQ.CoreLib

Namespace Controls.DBControls
    Public Class DBCCorpInfo
        Public Sub New()

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.

            ' Initialise configuration form name
            ControlConfigForm = "EveHQ.Controls.DBConfigs.DBCCorpInfoConfig"

            ' Load the combo box with the pilot info
            cboCorp.BeginUpdate()
            cboCorp.Items.Clear()
            For Each corp As Corporation In HQ.Settings.Corporations.Values
                cboCorp.Items.Add(corp.Name)
            Next
            cboCorp.EndUpdate()

        End Sub
#Region "Public Overriding Propeties"

        Public Overrides ReadOnly Property ControlName() As String
            Get
                Return "Corporation Information"
            End Get
        End Property

#End Region

#Region "Custom Control Variables"
        Dim _defaultCorpName As String = ""
#End Region

#Region "Custom Control Properties"
        Public Property DefaultCorpName() As String
            Get
                Return _defaultCorpName
            End Get
            Set(ByVal value As String)
                _defaultCorpName = value
                If HQ.Settings.Corporations.ContainsKey(DefaultCorpName) Then
                    _corp = HQ.Settings.Corporations(DefaultCorpName)
                End If
                If cboCorp.Items.Contains(DefaultCorpName) = True Then cboCorp.SelectedItem = DefaultCorpName
                If ReadConfig = False Then
                    SetConfig("DefaultCorpName", value)
                    SetConfig("ControlConfigInfo", "Default Corporation: " & DefaultCorpName)
                End If
            End Set
        End Property

#End Region

#Region "Class Variables"
        Dim _corp As Corporation
#End Region

#Region "Private Methods"
        Private Sub cboCorp_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboCorp.SelectedIndexChanged
            If HQ.Settings.Corporations.ContainsKey(cboCorp.SelectedItem.ToString) Then
                _corp = HQ.Settings.Corporations(cboCorp.SelectedItem.ToString)
                Call UpdateCorpInfo()
            End If
        End Sub

        Private Sub UpdateCorpInfo()
            If HQ.Settings.Corporations.ContainsKey(cboCorp.SelectedItem.ToString) Then
                ' Update the info
                If _corp.ApiData IsNot Nothing Then
                    lblTicker.Text = "Ticker: " & _corp.ApiData.Ticker
                    lblCeo.Text = "CEO: " & _corp.ApiData.CeoName
                    lblStation.Text = "Station: " & _corp.ApiData.StationName
                    lblAlliance.Text = "Alliance: " & _corp.ApiData.AllianceName
                    lblTaxRate.Text = "Tax Rate: " & _corp.ApiData.TaxRate & "%"
                    lblMemberCount.Text = "Member Count: " & _corp.ApiData.MemberCount
                    Dim walletTotal As Double = 0
                    If _corp.WalletBalances IsNot Nothing Then
                        For Each walletBalance As NewEveAPI.Entities.AccountBalance In _corp.WalletBalances
                            walletTotal += walletBalance.Balance
                        Next
                    End If
                    lblIsk.Text = "Total Wallet: " & walletTotal.ToString("N2", CultureInfo.InvariantCulture) & " Isk"
                    pbCorp.Image = ImageHandler.GetCorpImage(_corp.ID)
                Else
                    ' Clear the info
                    lblTicker.Text = ""
                    lblCeo.Text = ""
                    lblStation.Text = ""
                    lblAlliance.Text = ""
                    lblTaxRate.Text = ""
                    lblMemberCount.Text = ""
                    lblIsk.Text = ""
                    pbCorp.Image = My.Resources.noitem
                End If
            End If
        End Sub

        Private Sub lblCorp_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblCorp.LinkClicked
            If HQ.Settings.Corporations.ContainsKey(cboCorp.SelectedItem.ToString) Then
                ' Update the info
                If _corp.ApiData IsNot Nothing And _corp.ApiData.Url.Length <> 0 Then
                    Process.Start(_corp.ApiData.Url)
                End If
            End If
        End Sub

#End Region

    End Class
End Namespace