using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EveHQ.Common.Extensions;
using EveHQ.Market;
using EveHQ.CoreLib;
using EveHQ.Market;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EveHQ.CoreLib;

/// <summary>
/// Class for the new EveHQ settings.
/// </summary>
[Serializable]
public class EveHQSettings
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
    [JsonProperty] private string[] _eveFolderLabel = new string[5];
    [JsonProperty] private bool[] _eveFolderLua = new bool[5];
    [JsonProperty] private string[] _eveFolder = new string[5];
    private SortedList<string, bool> _igbAllowedData = [];
    private string[][]? _qColumns;
    private Dictionary<int, ItemMarketOverride> _marketStatOverrides = [];
    private List<int> _marketRegions = [];
    private Dictionary<string, EveHQPilot> _pilots = [];
    private Dictionary<string, EveHQAccount> _accounts = [];
    private Dictionary<string, EveHQPlugInConfig> _plugins = [];

    #endregion

    #region "Constructors"

    public EveHQSettings()
    {
        _qColumns = new string[20][];
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
            if (string.IsNullOrWhiteSpace(_marketDataProvider))
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
    public MarketSite MarketDataSource { get; set; } = new MarketSite();
    public Dictionary<int, ItemMarketOverride> MarketStatOverrides {get => _marketStatOverrides; set => _marketStatOverrides = value; }
    
    
    
    
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

    public bool BackupBeforeUpdate { get; set; }
    public string QatLayout { get; set; }
    public bool NotifyEveNotification { get; set; }
    public bool NotifyEveMail { get; set; }
    public bool AutoMailAPI { get; set; }
    public int EveHqBackupWarnFreq { get; set; }
    public int EveHqBackupMode { get; set; }
    public DateTime EveHqBackupStart { get; set; }
    public int EveHqBackupFreq { get; set; }
    public DateTime EveHqBackupLast { get; set; }
    public int EveHqBackupLastResult { get; set; }
    public bool IbShowAllItems { get; set; }


    public string EmailSenderAddress
    {
        get
        {
            if (string.IsNullOrWhiteSpace(_emailSenderAddress))
            {
                _emailSenderAddress = "contact.evehq@gmail.com";
            }

            return _emailSenderAddress;
        }
        set => _emailSenderAddress = value;
    }

    public ArrayList UserQueueColumns
    {
        get => _userQueueColumns;
        set => _userQueueColumns = value;
    }

    public ArrayList StandardQueueColumns
    {
        get => _standardQueueColumns;
        set => _standardQueueColumns = value;
    }

    public string DBTickerLocation { get; set; }
    public bool DBTicker { get; set; }

    public List<SortedList<string, object>> DashboardConfiguration
    {
        get => _dashboardConfiguration;
        set => _dashboardConfiguration = value;
    }

    public string CsvSeparatorChar
    {
        get
        {
            if (string.IsNullOrWhiteSpace(_csvSeparatorChar))
            {
                _csvSeparatorChar = CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator;
            }

            return _csvSeparatorChar;
        }
        set => _csvSeparatorChar = value;
    }

    public bool DisableVisualStyles { get; set; }
    public bool DisableAutoWebConnections { get; set; }
    public int TrainingBarHeight { get; set; }
    public int TrainingBarWidth { get; set; }
    public int TrainingBarDockPosition { get; set; }
    public string MdiTabPosition { get; set; }

    public ArrayList MarketRegionList
    {
        get => _marketRegionList;
        set => _marketRegionList = value;
    }

    public double IgnoreBuyOrderLimit { get; set; }
    public double IgnoreSellOrderLimit { get; set; }

    //XXX: get_ and set_ prefixes used for VB 
    public bool get_PriceCriteria(int index)
    {
        return _priceCriteria[index - 1];
    }

    public void set_PriceCriteria(int index, bool value)
    {
        _priceCriteria[index - 1] = value;
    }

    public bool MarketLogUpdateData { get; set; }
    public bool MarketLogUpdatePrice { get; set; }
    public bool MarketLogPopupConfirm { get; set; }
    public bool MarketLogToolTipConfirm { get; set; }
    public bool IgnoreBuyOrders { get; set; }
    public bool IgnoreSellOrders { get; set; }
    public string CustomDBFileName { get; set; }
    public int DBTimeout { get; set; }
    public long PilotSkillHighlightColor { get; set; }
    public long PilotSkillTextColor { get; set; }
    public long PilotGroupTextColor { get; set; }
    public long PilotGroupBackgroundColor { get; set; }
    public string ErrorReportingEmail { get; set; }
    public string ErrorReportingName { get; set; }
    public bool ErrorReportingEnabled { get; set; }
    public int TaskbarIconMode { get; set; }
    public string EcmDefaultLocation { get; set; }
    public string APIFileExtension { get; set; }
    public bool UseAppDirectoryForDB { get; set; }
    public bool OmitCurrentSkill { get; set; }
    public string UpdateUrl { get; set; }
    public bool UseCcpapiBackup { get; set; }
    public bool UseApirs { get; set; }
    public string ApirsAddress { get; set; }

    [Obsolete("CcpapiServerAddress is deprecated.")]
    public string CcpapiServerAddress { get; set; }

    public string EveFolderLabel(int index)
    {
        if (index is < 1 or > 4)
        {
            //TODO: Message => "Eve Folder Label index must be in the range 1 to 4"
            return "0";
        }

        return _eveFolderLabel[index - 1];
    }

    public void SetEveFolderLabel(int index, string label)
    {
        if (index is < 1 or > 4)
        {
            //TODO: Message => "Eve Folder Label index must be in the range 1 to 4"
            return;
        }

        _eveFolderLabel[index - 1] = label;
    }

    public bool EveFolderLua(int index)
    {
        if (index is < 1 or > 4)
        {
            //TODO: Message => "Eve Folder LUA index must be in the range 1 to 4"
            return false;
        }

        return _eveFolderLua[index - 1];
    }

    public void SetEveFolderLua(int index, bool value)
    {
        if (index is < 1 or > 4)
        {
            //TODO: Message => "Eve Folder LUA index must be in the range 1 to 4"
            return;
        }

        _eveFolderLua[index - 1] = value;
    }

    public string EveFolder(int index)
    {
        if (index is < 1 or > 4)
        {
            //TODO: Message => "Eve Folder index must be in the range 1 to 4"
            return "0";
        }

        return _eveFolder[index - 1];
    }

    public void SetEveFolder(int index, string value)
    {
        if (index is < 1 or > 4)
        {
            //TODO: Message => "Eve Folder index must be in the range 1 to 4"
            return;
        }

        _eveFolder[index - 1] = value;
    }


    public long PilotCurrentTrainSkillColor { get; set; }
    public long PilotPartTrainedSkillColor { get; set; }
    public long PilotLevel5SkillColor { get; set; }
    public long PilotStandardSkillColor { get; set; }
    public long PanelHighlightColor { get; set; }
    public long PanelTextColor { get; set; }
    public long PanelRightColor { get; set; }
    public long PanelLeftColor { get; set; }
    public long PanelBottomRightColor { get; set; }
    public long PanelTopLeftColor { get; set; }
    public long PanelOutlineColor { get; set; }
    public long PanelBackgroundColor { get; set; }
    public DateTime LastMarketPriceUpdate { get; set; }
    public DateTime LastFactionPriceUpdate { get; set; }

    public int CycleG15Time { get; set; }
    public bool CycleG15Pilots { get; set; }
    public bool ActivateG15 { get; set; }
    public bool AutoAPI { get; set; }

    public bool DeleteSkills { get; set; }
    public long PartialTrainColor { get; set; }
    public long ReadySkillColor { get; set; }
    public long IsPreReqColor { get; set; }
    public long HasPreReqColor { get; set; }
    public long BothPreReqColor { get; set; }
    public long DtClashColor { get; set; }
    public string ColorHighlightQueuePreReq { get; set; }
    public string ColorHighlightQueueTraining { get; set; }
    public string ColorHighlightPilotTraining { get; set; }
    public bool ContinueTraining { get; set; }
    public string EMailPassword { get; set; }
    public string EMailUsername { get; set; }
    public bool UseSsl { get; set; }
    public bool UseSmtpAuth { get; set; }
    public string EMailAddress { get; set; }
    public int EMailPort { get; set; }
    public string EMailServer { get; set; }
    public string NotifySoundFile { get; set; }
    public int NotifyOffset { get; set; }
    public bool NotifyEarly { get; set; }
    public bool NotifyNow { get; set; }
    public bool NotifySound { get; set; }
    public bool NotifyEMail { get; set; }
    public bool NotifyDialog { get; set; }
    public bool NotifyToolTip { get; set; }
    public int ShutdownNotifyPeriod { get; set; }
    public bool ShutdownNotify { get; set; }
    public int ServerOffset { get; set; }
    public bool EnableEveStatus { get; set; }
    public bool ProxyUseDefault { get; set; }
    public bool ProxyUseBasic { get; set; }
    public string ProxyPassword { get; set; }
    public string ProxyUsername { get; set; }
    public int ProxyPort { get; set; }
    public string ProxyServer { get; set; }
    public bool ProxyRequired { get; set; }
    public int IgbPort { get; set; }
    public bool IgbAutoStart { get; set; }
    public bool IgbFullMode { get; set; }

    public SortedList<string, bool> IgbAllowedData
    {
        get => _igbAllowedData;
        set => _igbAllowedData = value;
    }

    public bool AutoHide { get; set; }
    public bool AutoStart { get; set; }
    public bool AutoCheck { get; set; }
    public bool MinimiseExit { get; set; }
    public bool AutoMinimise { get; set; }
    public string StartupPilot { get; set; }

    public bool BackupAuto { get; set; }
    public DateTime BackupStart { get; set; }
    public int BackupFreq { get; set; }
    public DateTime BackupLast { get; set; }
    public int BackupLastResult { get; set; }
    public bool QColumnsSet { get; set; }

    public string QColumns(int col, int refer)
    {
        return _qColumns[col][refer];
    }

    public void SetQColumns(int col, int refer, string value)
    {
        _qColumns[col][refer] = value;
    }

    public Dictionary<string, EveHQAccount> Accounts
    {
        get => _accounts;
        set => _accounts = value;
    }

    public Dictionary<string, EveHQPlugInConfig> Plugins
    {
        get => _plugins;
        set => _plugins = value;
    }

    public Dictionary<string, EveHQPilot> Pilots
    {
        get => _pilots;
        set => _pilots = value;
    }

    public List<int> MarketRegions
    {
        get
        {
            if (_marketRegions.Count == 0)
            {
                _marketRegions.Add(10000002); //The Forge... safe default.
            }

            return _marketRegions;
        }
        set => _marketRegions = value;
    }

    public int MarketSystem
    {
        get
        {
            if (_marketSystem == 0)
            {
                _marketSystem = 30000142; //XXX: Jira?
            }

            return _marketSystem;
        }
        set => _marketSystem = value;
    }

    public bool MarketUseRegionMarket { get; set; }
    public MarketMetric MarketDefaultMetric { get; set; }

    public Dictionary<int, ItemMarketOverride> MarketOverrides
    {
        get => _marketStatOverrides;
        set => _marketStatOverrides = value;
    }

    public MarketTransactionKind MarketDefaultTransactionType { get; set; }
    public int EveQueueDisplayLength { get; set; }


    public int StartupForms { get; set; }
    public bool RibbonMinimised { get; set; }
    public bool ThemeSetByUser { get; set; }
    public Color ThemeCanvas { get; set; }
    public Color ThemeTint { get; set; }
    public eStyle ThemeStyle { get; set; }
    public FormWindowState MainFormWindowState { get; set; }
    public Point MainFormLocation { get; set; }
    public Size MainFormSize { get; set; }

    #endregion

    private void InitialiseSettings()
    {
        IgbPort = 26001;
        AutoHide = true;
        BackupStart = DateTime.Now;
        BackupFreq = 1;
        BackupLast = new DateTime(1999, 1, 1);
        EnableAutomaticSave = true;
        AutomaticSaveTime = 15;
        ProxyUseDefault = true;
        ShutdownNotifyPeriod = 8;
        EMailPort = 25;
        IsPreReqColor = Color.LightSteelBlue.ToArgb();
        HasPreReqColor = Color.White.ToArgb();
        BothPreReqColor = Color.White.ToArgb();
        DtClashColor = Color.Red.ToArgb();
        ReadySkillColor = Color.White.ToArgb();
        PartialTrainColor = Color.White.ToArgb();
        CycleG15Time = 15;
        PanelBackgroundColor = Color.Navy.ToArgb();
        PanelOutlineColor = Color.SteelBlue.ToArgb();
        PanelTopLeftColor = Color.LightSteelBlue.ToArgb();
        PanelBottomRightColor = Color.LightSteelBlue.ToArgb();
        PanelLeftColor = Color.RoyalBlue.ToArgb();
        PanelRightColor = Color.LightSteelBlue.ToArgb();
        PanelTextColor = Color.Black.ToArgb();
        PanelHighlightColor = Color.LightSteelBlue.ToArgb();
        PilotStandardSkillColor = Color.White.ToArgb();
        PilotLevel5SkillColor = Color.Thistle.ToArgb();
        PilotPartTrainedSkillColor = Color.Gold.ToArgb();
        PilotCurrentTrainSkillColor = Color.LimeGreen.ToArgb();
        CcpapiServerAddress = OfficialApiLocation;
        UpdateUrl = "http://evehq.co/update/";
        APIFileExtension = "aspx";
        PilotGroupBackgroundColor = Color.DimGray.ToArgb();
        PilotGroupTextColor = Color.White.ToArgb();
        PilotSkillTextColor = Color.Black.ToArgb();
        PilotSkillHighlightColor = Color.DodgerBlue.ToArgb();
        DBTimeout = 30;
        IgnoreBuyOrders = true;
        IgnoreSellOrderLimit = 1000;
        IgnoreBuyOrderLimit = 1;
        MdiTabPosition = "Top";
        TrainingBarHeight = 54;
        TrainingBarWidth = 100;
        CsvSeparatorChar = CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator;
        DBTickerLocation = "Bottom";
        EmailSenderAddress = "contact.evehq@gmail.com";
        EveHqBackupStart = DateTime.Now;
        EveHqBackupFreq = 1;
        EveHqBackupLast = new DateTime(1999, 1, 1);
        EveHqBackupWarnFreq = 1;
        ThemeStyle = eStyle.Office2007Black;
        ThemeTint = Color.Empty;
        LastMessageDate = new DateTime(1999, 1, 1);
        AccountTimeLimit = 168;
        SkillQueuePanelWidth = 440;
        MarketSystem = 30000142; //'Safe Default of Jita
        MaxUpdateThreads = 5;
        MainFormWindowState = FormWindowState.Maximized;
        StartupPilot = "";
        StartupForms = 0;
    }

    public static void ResetColumns()
    {
        var trueStr = true.ToString();
        var falseStr = false.ToString();
        HQ_.Settings.SetQColumns(0, 0, "Name");
        HQ_.Settings.SetQColumns(0, 1, trueStr);
        HQ_.Settings.SetQColumns(1, 0, "Curr");
        HQ_.Settings.SetQColumns(1, 1, trueStr);
        HQ_.Settings.SetQColumns(2, 0, "From");
        HQ_.Settings.SetQColumns(2, 1, trueStr);
        HQ_.Settings.SetQColumns(3, 0, "Tole");
        HQ_.Settings.SetQColumns(3, 1, trueStr);
        HQ_.Settings.SetQColumns(4, 0, "Perc");
        HQ_.Settings.SetQColumns(4, 1, trueStr);
        HQ_.Settings.SetQColumns(5, 0, "Trai");
        HQ_.Settings.SetQColumns(5, 1, trueStr);
        HQ_.Settings.SetQColumns(6, 0, "Comp");
        HQ_.Settings.SetQColumns(6, 1, trueStr);
        HQ_.Settings.SetQColumns(7, 0, "Date");
        HQ_.Settings.SetQColumns(7, 1, trueStr);
        HQ_.Settings.SetQColumns(8, 0, "Rank");
        HQ_.Settings.SetQColumns(8, 1, falseStr);
        HQ_.Settings.SetQColumns(9, 0, "PAtt");
        HQ_.Settings.SetQColumns(9, 1, falseStr);
        HQ_.Settings.SetQColumns(10, 0,  "SAtt");
        HQ_.Settings.SetQColumns(10, 1,  falseStr);
        HQ_.Settings.SetQColumns(11, 0,  "SPRH");
        HQ_.Settings.SetQColumns(11, 1,  falseStr);
        HQ_.Settings.SetQColumns(12, 0,  "SPRD");
        HQ_.Settings.SetQColumns(12, 1,  falseStr);
        HQ_.Settings.SetQColumns(13, 0,  "SPRW");
        HQ_.Settings.SetQColumns(13, 1,  falseStr);
        HQ_.Settings.SetQColumns(14, 0,  "SPRM");
        HQ_.Settings.SetQColumns(14, 1,  falseStr);
        HQ_.Settings.SetQColumns(15, 0,  "SPRY");
        HQ_.Settings.SetQColumns(15, 1,  falseStr);
        HQ_.Settings.SetQColumns(16, 0,  "SPAd");
        HQ_.Settings.SetQColumns(16, 1,  falseStr);
        HQ_.Settings.SetQColumns(17, 0,  "SPTo");
        HQ_.Settings.SetQColumns(17, 1,  falseStr);
        HQ_.Settings.SetQColumns(18, 0,  "Note");
        HQ_.Settings.SetQColumns(18, 1,  falseStr);
        HQ_.Settings.SetQColumns(19, 0,  "Prio");
        HQ_.Settings.SetQColumns(19, 1,  falseStr);
        HQ_.Settings.QColumnsSet = true;
    }

    public void Save()
    {
        var fileName = Path.Combine(HQ_.AppDataFolder, "EveHQSettings.json");
        HQ_.WriteLogEvent("Settings: Saving EveHQ settings to " + fileName);
        //Convert the current settings to a JSON formatted string
        var json = JsonConvert.SerializeObject(this, Formatting.Indented);
        
        //Write the JSON string to the file
        try
        {
            using var s = new StreamWriter(fileName, false);
            s.Write(json);
            s.Flush();
        }
        catch (Exception ex)
        {
            HQ_.WriteLogEvent("Settings: Error saving EveHQ settings to " +
                              Path.Combine(HQ_.AppDataFolder, "EveHQSettings.bin - " + ex.Message));
        }
        
        //Update the Proxy Server settings
        InitialiseRemoteProxyServer();
    }

    public static bool Load(bool showRawData)
    {
        if (File.Exists(Path.Combine(HQ_.AppDataFolder, "EveHQSettings.json")))
        {
            try
            {
                using var s = new StreamReader(Path.Combine(HQ_.AppDataFolder, "EveHQSettings.json"));
                var json  = s.ReadToEnd();
                HQ_.Settings = JsonConvert.DeserializeObject<EveHQSettings>(json);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.FormatException());
                var msg =
                    "There was an error trying to load the settings file and it appears that this file is corrupt." +
                    "\r\n" + "\r\n";
                msg += "The error was: " + ex.Message + "\r\n" + "\r\n";
                msg += "Stacktrace: " + ex.StackTrace + "\r\n" + "\r\n";
                msg += "EveHQ will copy this file to 'EveHQSettings.bad' and delete the original file and re-initialise the settings. This means you will need to re-enter your API information but your production and fittings data should be intact and available once the API data has been downloaded. You can attempt to reload the old settings by renaming the 'EveHQSettings.bad' file to 'EveHQSettings.bin', however if the issue continues the bad file will be useful to the EveHQ team for debugging purposes" +
                       "\r\n" + "\r\n";
                msg += "Press OK to reset the settings." + "\r\n";
                MessageBox.Show(msg, "Invalid Settings file detected", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                try
                {
                    File.Copy(Path.Combine(HQ_.AppDataFolder, "EveHQSettings.json"),
                        Path.Combine(HQ_.AppDataFolder, "EveHQSettings.bad"), true);
                }
                catch (Exception)
                {
                    MessageBox.Show(
                        "Unable to delete the EveHQSettings.json file. Please delete this manually before proceeding",
                        "Delete File Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Application.Exit();
                }
            }
        }
        else
        {
            HQ_.Settings = new EveHQSettings();
        }

        if (HQ_.Settings == null)
        {
            MessageBox.Show(
                "There was an issue loading the settings file: It was empty. Please delete the EveHQSettigns.json file manually and restore from backup.");
            return false;
        }

        if (showRawData == false)
        {
            //' Reset the update URL to a temp location
            if (HQ_.Settings.UpdateUrl != "http://evehq.co/update/")
            {
                HQ_.Settings.UpdateUrl = "http://evehq.co/update/";
            }
            
            //' Set the Custom database connection
            try
            {
                if (CustomDataFunctions_.SetEveHQDataConnectionString() == false)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                var msg = new StringBuilder();
                msg.AppendLine("Error: " + ex.Message);
                msg.AppendLine("");
                msg.AppendLine(
                    "An error occurred trying to set the custom database connection string. This could be down to a missing database library file.");
                MessageBox.Show(msg.ToString(), "Error Initialising Database", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

            InitialiseQueueColumns();
            InitialiseUserColumns();
            InitialiseRemoteProxyServer();
            if (string.IsNullOrEmpty(HQ_.Settings.QColumns(0, 0)))
            {
                ResetColumns();
            }
            
            //Set Theme stuff
            if (HQ_.Settings.ThemeSetByUser == false)
            {
                HQ_.Settings.ThemeStyle = eStyle.Office2007Black;
                HQ_.Settings.ThemeTint = Color.Empty;
            }
        }

        return true;
    }
    
    public static void InitialiseRemoteProxyServer()
    {
        HQ_.RemoteProxy.ProxyRequired = HQ_.Settings.ProxyRequired;
        HQ_.RemoteProxy.ProxyServer = HQ_.Settings.ProxyServer;
        HQ_.RemoteProxy.ProxyPort = HQ_.Settings.ProxyPort;
        HQ_.RemoteProxy.UseDefaultCredentials = HQ_.Settings.ProxyUseDefault;
        HQ_.RemoteProxy.ProxyUsername = HQ_.Settings.ProxyUsername;
        HQ_.RemoteProxy.ProxyPassword = HQ_.Settings.ProxyPassword;
        HQ_.RemoteProxy.UseBasicAuthentication = HQ_.Settings.ProxyUseBasic;        
    }

    public static void InitialiseQueueColumns()
    {
        HQ_.Settings.StandardQueueColumns.Clear();
        ListViewItem newItem;
        
        newItem = new ListViewItem();
        newItem.Name = "Current";
        newItem.Text = "Cur Lvl";
        newItem.Checked = true;
        HQ_.Settings.StandardQueueColumns.Add(newItem);
        newItem = new ListViewItem();
        newItem.Name = "From";
        newItem.Text = "From Lvl";
        newItem.Checked = true;
        HQ_.Settings.StandardQueueColumns.Add(newItem);
        newItem = new ListViewItem();
        newItem.Name = "To";
        newItem.Text = "To Lvl";
        newItem.Checked = true;
        HQ_.Settings.StandardQueueColumns.Add(newItem);
        newItem = new ListViewItem();
        newItem.Name = "Percent";
        newItem.Text = "%";
        newItem.Checked = true;
        HQ_.Settings.StandardQueueColumns.Add(newItem);
        newItem = new ListViewItem();
        newItem.Name = "TrainTime";
        newItem.Text = "Training Time";
        newItem.Checked = true;
        HQ_.Settings.StandardQueueColumns.Add(newItem);
        newItem = new ListViewItem();
        newItem.Name = "TimeToComplete";
        newItem.Text = "Time To Complete";
        newItem.Checked = true;
        HQ_.Settings.StandardQueueColumns.Add(newItem);
        newItem = new ListViewItem();
        newItem.Name = "DateEnded";
        newItem.Text = "Date Completed";
        newItem.Checked = true;
        HQ_.Settings.StandardQueueColumns.Add(newItem);
        newItem = new ListViewItem();
        newItem.Name = "Rank";
        newItem.Text = "Rank";
        newItem.Checked = false;
        HQ_.Settings.StandardQueueColumns.Add(newItem);
        newItem = new ListViewItem();
        newItem.Name = "PAtt";
        newItem.Text = "Pri Att";
        newItem.Checked = false;
        HQ_.Settings.StandardQueueColumns.Add(newItem);
        newItem = new ListViewItem();
        newItem.Name = "SAtt";
        newItem.Text = "Sec Att";
        newItem.Checked = false;
        HQ_.Settings.StandardQueueColumns.Add(newItem);
        newItem = new ListViewItem();
        newItem.Name = "SPHour";
        newItem.Text = "SP /hour";
        newItem.Checked = false;
        HQ_.Settings.StandardQueueColumns.Add(newItem);
        newItem = new ListViewItem();
        newItem.Name = "SPDay";
        newItem.Text = "SP /day";
        newItem.Checked = false;
        HQ_.Settings.StandardQueueColumns.Add(newItem);
        newItem = new ListViewItem();
        newItem.Name = "SPWeek";
        newItem.Text = "SP /week";
        newItem.Checked = false;
        HQ_.Settings.StandardQueueColumns.Add(newItem);
        newItem = new ListViewItem();
        newItem.Name = "SPMonth";
        newItem.Text = "SP /month";
        newItem.Checked = false;
        HQ_.Settings.StandardQueueColumns.Add(newItem);
        newItem = new ListViewItem();
        newItem.Name = "SPYear";
        newItem.Text = "SP /year";
        newItem.Checked = false;
        HQ_.Settings.StandardQueueColumns.Add(newItem);
        newItem = new ListViewItem();
        newItem.Name = "SPAdded";
        newItem.Text = "SP Added";
        newItem.Checked = false;
        HQ_.Settings.StandardQueueColumns.Add(newItem);
        newItem = new ListViewItem();
        newItem.Name = "SPTotal";
        newItem.Text = "SP Total";
        newItem.Checked = false;
        HQ_.Settings.StandardQueueColumns.Add(newItem);
        newItem = new ListViewItem();
        newItem.Name = "Notes";
        newItem.Text = "Notes";
        newItem.Checked = false;
        HQ_.Settings.StandardQueueColumns.Add(newItem);
        newItem = new ListViewItem();
        newItem.Name = "Priority";
        newItem.Text = "Priority";
        newItem.Checked = false;
        HQ_.Settings.StandardQueueColumns.Add(newItem);        
    }

    public static void InitialiseUserColumns()
    {
        if (HQ_.Settings.UserQueueColumns.Count == 0) {
            //Add preset items
            HQ_.Settings.UserQueueColumns.Add("Current1");
            HQ_.Settings.UserQueueColumns.Add("From1");
            HQ_.Settings.UserQueueColumns.Add("To1");
            HQ_.Settings.UserQueueColumns.Add("Percent1");
            HQ_.Settings.UserQueueColumns.Add("TrainTime1");
            HQ_.Settings.UserQueueColumns.Add("TimeToComplete1");
            HQ_.Settings.UserQueueColumns.Add("DateEnded1");
        }
        
        //Check if the standard columns have changed and we need to add columns
        if (HQ_.Settings.UserQueueColumns.Count == HQ_.Settings.StandardQueueColumns.Count) return;
        foreach (ListViewItem slotItem in HQ_.Settings.StandardQueueColumns)
        {
            if (
                HQ_.Settings.UserQueueColumns.Contains(slotItem.Name + "0") == false &&
                HQ_.Settings.UserQueueColumns.Contains(slotItem.Name + "1") == false)
            {
                HQ_.Settings.UserQueueColumns.Add(slotItem.Name + "0");
            }
        }
    }
    
}