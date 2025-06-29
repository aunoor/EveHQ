using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EveHQ.Common.Extensions;
using EveHQ.Market;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EveHQ.CoreLib;

/// <summary>
/// Class for the new EveHQ settings.
/// </summary>
[Serializable]
public class EveHQSettings_
{
    #region "Private Fields"

    private int _marketSystem;
    private string _marketDataProvider = "";
    private int _maxUpdateThreads;
    private SortedList<string, Corporation> _corporations = [];
    private SortedList<string, PriceGroup> _priceGroups = [];
    private int _skillQueuePanelWidth;
    private int _automaticSaveTime;
    private SortedList<string, string> _sqlQueries = [];
    private string _emailSenderAddress = "";
    private ArrayList _userQueueColumns = [];
    private ArrayList _standardQueueColumns = [];
    private List<SortedList<string, object>> _dashboardConfiguration = [];
    private string _csvSeparatorChar = "";
    private ArrayList _marketRegionList = [];
    private bool[] _priceCriteria = new bool[12];
    private string _ccpApiServerAddress = "";
    [JsonProperty] private string[] _eveFolderLabel = new string[4];
    [JsonProperty] private bool[] _eveFolderLua = new bool[4];
    private SortedList<string, bool> _igbAllowedData = [];
    [JsonProperty] private string[] _eveFolder = new string[4];
    private string[][]? _qColumns;
    private Dictionary<int, ItemMarketOverride> _marketStatOverrides = [];
    private List<int> _marketRegions = [];
    private Dictionary<string, EveHQPilot> _pilots = [];
    private Dictionary<string, EveHQAccount> _accounts = [];
    private Dictionary<string, EveHQPlugInConfig> _plugins = [];

    #endregion

    #region "Constructors"

    public EveHQSettings_()
    {
        _qColumns = new string[21][];
        for (var i = 0; i < _qColumns.Length; i++)
        {
            _qColumns[i] = new string[2];
        }

        InitialiseSettings();
    }

    #endregion

    #region "Private Constants"

    [Obsolete("OfficialApiLocation is deprecated.")]
    private const string OfficialApiLocation = "https://api.eveonline.com";

    #endregion


    #region "Public Properties"

    public string MarketDataProvider
    {
        get
        {
            if (_marketDataProvider.IsNullOrWhiteSpace())
            {
                _marketDataProvider = nameof(MarketProvider.EveCentral);
            }

            return _marketDataProvider;
        }
        set => _marketDataProvider = value;
    }

    public int MaxUpdateThreads
    {
        get
        {
            if (_maxUpdateThreads == 0)
            {
                _maxUpdateThreads = 5;
            }

            return _maxUpdateThreads;
        }
        set { _maxUpdateThreads = value; }
    }

    [Obsolete("MarketDataSource is deprecated.")]
    public MarketSite MarketDataSource { get; set; }

    public SortedList<string, Corporation> Corporations
    {
        get => _corporations;
        set => _corporations = value;
    }

    public SortedList<string, PriceGroup> PriceGroups
    {
        get => _priceGroups;
        set => _priceGroups = value;
    }

    public int SkillQueuePanelWidth
    {
        get
        {
            if (_skillQueuePanelWidth == 0)
            {
                _skillQueuePanelWidth = 440;
            }

            return _skillQueuePanelWidth;
        }
        set => _skillQueuePanelWidth = value;
    }

    public int AccountTimeLimit { get; set; }
    public bool NotifyAccountTime { get; set; }
    public bool NotifyInsuffClone { get; set; }
    public bool StartWithPrimaryQueue { get; set; }
    public bool IgnoreLastMessage { get; set; }
    public DateTime LastMessageDate { get; set; }
    public bool DisableTrainingBar { get; set; }
    public bool EnableAutomaticSave { get; set; }

    public int AutomaticSaveTime
    {
        get
        {
            if (_automaticSaveTime < 0)
            {
                _automaticSaveTime = 60;
            }

            return _automaticSaveTime;
        }
        set => _automaticSaveTime = value;
    }

    public SortedList<string, string> SQLQueries
    {
        get => _sqlQueries;
        set => _sqlQueries = value;
    }
    
    public bool BackupBeforeUpdate {get; set;}
    public string QatLayout {get; set;}
    public bool NotifyEveNotification {get; set;}
    public bool NotifyEveMail {get; set;}
    public bool AutoMailAPI {get; set;}
    public int EveHqBackupWarnFreq {get; set;}
    public int EveHqBackupMode {get; set;}
    public DateTime EveHqBackupStart {get; set;}
    public int EveHqBackupFreq {get; set;}
    public DateTime EveHqBackupLast {get; set;}
    public int EveHqBackupLastResult {get; set;}
    public bool IbShowAllItems {get; set;}


    public string EmailSenderAddress
    {
        get
        {
            if (_emailSenderAddress.IsNullOrWhiteSpace())
            {
                _emailSenderAddress = "contact.evehq@gmail.com";
            }
            return _emailSenderAddress;
        }
        set => _emailSenderAddress = value;
    }

    public ArrayList UserQueueColumns {get => _userQueueColumns; set => _userQueueColumns = value; }
    public ArrayList StandardQueueColumns {get => _standardQueueColumns; set => _standardQueueColumns = value; }
    
    public string DBTickerLocation  {get; set;}
    public bool DBTicker  {get; set;}
    
    public List<SortedList<string, object>> DashboardConfiguration { get => _dashboardConfiguration; set => _dashboardConfiguration = value; }

    public string CsvSeparatorChar
    {
        get
        {
            if (_csvSeparatorChar.IsNullOrWhiteSpace())
            {
                _csvSeparatorChar = CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator;
            }
            return _csvSeparatorChar;
        }
        set => _csvSeparatorChar = value;
    }
    
    public bool DisableVisualStyles {get; set;}
    public bool DisableAutoWebConnections {get; set;}
    public int TrainingBarHeight {get; set;}
    public int TrainingBarWidth {get; set;}
    public int TrainingBarDockPosition {get; set;}
    public string MdiTabPosition {get; set;}    
    
    public ArrayList MarketRegionList { get => _marketRegionList; set => _marketRegionList = value; }
    public double IgnoreBuyOrderLimit {get; set;}
    public double IgnoreSellOrderLimit {get; set;}

    //XXX: get_ and set_ prefixes used for VB 
    public bool get_PriceCriteria(int index)
    {
        return _priceCriteria[index-1];
    }

    public void set_PriceCriteria(int index, bool value)
    {
        _priceCriteria[index-1] = value;
    }
    
    public bool MarketLogUpdateData {get; set;}
    public bool MarketLogUpdatePrice {get; set;}
    public bool MarketLogPopupConfirm {get; set;}
    public bool MarketLogToolTipConfirm {get; set;}
    public bool IgnoreBuyOrders {get; set;}
    public bool IgnoreSellOrders {get; set;}
    public string CustomDBFileName {get; set;}
    public int DBTimeout {get; set;}
    public long PilotSkillHighlightColor {get; set;}
    public long PilotSkillTextColor {get; set;}
    public long PilotGroupTextColor {get; set;}
    public long PilotGroupBackgroundColor {get; set;}
    public string ErrorReportingEmail {get; set;}
    public string ErrorReportingName {get; set;}
    public bool ErrorReportingEnabled {get; set;}
    public int TaskbarIconMode {get; set;}
    public string EcmDefaultLocation {get; set;}
    public string APIFileExtension {get; set;}
    public bool UseAppDirectoryForDB {get; set;}
    public bool OmitCurrentSkill {get; set;}
    public string UpdateUrl {get; set;}
    public bool UseCcpapiBackup {get; set;}
    public bool UseApirs {get; set;}
    public string ApirsAddress {get; set;}    
    
    [Obsolete("CcpapiServerAddress is deprecated.")]   
    public string CcpapiServerAddress  {get; set;}

    public string get_EveFolderLabel(int index)
    {
        if (index is < 1 or > 4)
        {
            //TODO: Message => "Eve Folder Label index must be in the range 1 to 4"
            return "0";
        }
        return _eveFolderLabel[index-1];
    }

    public void set_EveFolderLabel(int index, string label)
    {
        if (index is < 1 or > 4)
        {
            //TODO: Message => "Eve Folder Label index must be in the range 1 to 4"
            return;
        }
        _eveFolderLabel[index-1] = label;
    }
    public bool get_EveFolderLua(int index)
    {
        if (index is < 1 or > 4)
        {
            //TODO: Message => "Eve Folder LUA index must be in the range 1 to 4"
            return false;
        }
        return _eveFolderLua[index-1];
    }

    public void set_EveFolderLua(int index, bool value)
    {
        if (index is < 1 or > 4)
        {
            //TODO: Message => "Eve Folder LUA index must be in the range 1 to 4"
            return;
        }
        _eveFolderLua[index-1] = value;
    }    
    public string get_EveFolder(int index)
    {
        if (index is < 1 or > 4)
        {
            //TODO: Message => "Eve Folder index must be in the range 1 to 4"
            return "0";
        }
        return _eveFolder[index-1];
    }

    public void set_EveFolder(int index, string value)
    {
        if (index is < 1 or > 4)
        {
            //TODO: Message => "Eve Folder index must be in the range 1 to 4"
            return;
        }
        _eveFolder[index-1] = value;
    }    
    
    
    public long PilotCurrentTrainSkillColor {get; set;}
    public long PilotPartTrainedSkillColor {get; set;}
    public long PilotLevel5SkillColor {get; set;}
    public long PilotStandardSkillColor {get; set;}
    public long PanelHighlightColor {get; set;}
    public long PanelTextColor {get; set;}
    public long PanelRightColor {get; set;}
    public long PanelLeftColor {get; set;}
    public long PanelBottomRightColor {get; set;}
    public long PanelTopLeftColor {get; set;}
    public long PanelOutlineColor {get; set;}
    public long PanelBackgroundColor {get; set;}
    public DateTime LastMarketPriceUpdate {get; set;}
    public DateTime LastFactionPriceUpdate {get; set;}    
    
    public int CycleG15Time {get; set;}
    public bool CycleG15Pilots {get; set;}
    public bool ActivateG15 {get; set;}
    public bool AutoAPI {get; set;}
    
    public bool DeleteSkills {get; set;}
    public long PartialTrainColor {get; set;}
    public long ReadySkillColor {get; set;}
    public long IsPreReqColor {get; set;}
    public long HasPreReqColor {get; set;}
    public long BothPreReqColor {get; set;}
    public long DtClashColor {get; set;}
    public string ColorHighlightQueuePreReq {get; set;}
    public string ColorHighlightQueueTraining {get; set;}
    public string ColorHighlightPilotTraining {get; set;}
    public bool ContinueTraining {get; set;}
    public string EMailPassword {get; set;}
    public string EMailUsername {get; set;}
    public bool UseSsl {get; set;}
    public bool UseSmtpAuth {get; set;}
    public string EMailAddress {get; set;}
    public int EMailPort {get; set;}
    public string EMailServer {get; set;}
    public string NotifySoundFile {get; set;}
    public int NotifyOffset {get; set;}
    public bool NotifyEarly {get; set;}
    public bool NotifyNow {get; set;}
    public bool NotifySound {get; set;}
    public bool NotifyEMail {get; set;}
    public bool NotifyDialog {get; set;}
    public bool NotifyToolTip {get; set;}
    public int ShutdownNotifyPeriod {get; set;}
    public bool ShutdownNotify {get; set;}
    public int ServerOffset {get; set;}
    public bool EnableEveStatus {get; set;}
    public bool ProxyUseDefault {get; set;}
    public bool ProxyUseBasic {get; set;}
    public string ProxyPassword {get; set;}
    public string ProxyUsername {get; set;}
    public int ProxyPort {get; set;}
    public string ProxyServer {get; set;}
    public bool ProxyRequired {get; set;}
    public int IgbPort {get; set;}
    public bool IgbAutoStart {get; set;}
    public bool IgbFullMode {get; set;}
    public SortedList<string, bool> IgbAllowedData {get => _igbAllowedData; set => _igbAllowedData = value;}
    public bool AutoHide {get; set;}
    public bool AutoStart {get; set;}
    public bool AutoCheck {get; set;}
    public bool MinimiseExit {get; set;}
    public bool AutoMinimise {get; set;}
    public string StartupPilot {get; set;}

    public bool BackupAuto {get; set;}
    public DateTime BackupStart {get; set;}
    public int BackupFreq {get; set;}
    public DateTime BackupLast {get; set;}
    public int BackupLastResult {get; set;}
    public bool QColumnsSet {get; set;}

    public string get_QColumns(int col, int refer)
    {
        return _qColumns[col-1][refer-1];
    }

    public void set_QColumns(int col, int refer, string value)
    {
        _qColumns[col-1][refer-1] = value;
    }
    
    public Dictionary<string, EveHQAccount> Accounts {get => _accounts; set => _accounts = value;}
    
    
    public Dictionary<string, EveHQPlugInConfig> Plugins {get => _plugins; set => _plugins = value;}
    
    
    
    public int StartupForms {get; set;}    
    public bool RibbonMinimised { get; set; }
    public bool ThemeSetByUser { get; set; }
    public Color ThemeCanvas { get; set; }
    public Color ThemeTint { get; set; }
    public eStyle ThemeStyle { get; set; }
    public FormWindowState MainFormWindowState {get; set;}
    public Point MainFormLocation {get; set;}
    public Size MainFormSize {get; set;}
    
    #endregion

    private void InitialiseSettings()
    {
    }
}