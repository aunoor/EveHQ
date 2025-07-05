using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using EveHQ.Common;
using EveHQ.Common.Logging;
using EveHQ.Market;
using EveHQ.Market.MarketServices;
using EveHQ.NewEveAPI;

#if NET48
using System.Windows.Forms;
#endif

namespace EveHQ.CoreLib;

 
public class HQ_
{
    #if NET48
    public static Form? MainForm = null;
    #endif
    public static SortedList<string, EveHQPilot> tempPilots1 = [];
    public static SortedList<string, Corporation> TempCorps = [];
    //TODO: public static MyTqServer As EveServer = New EveServer
    public static Dictionary<string, EveSkill> SkillListName = []; //' SkillName, EveSkill
    public static SortedList<int, EveSkill> SkillListID = []; //' SkillID, EveSkill
    public static Dictionary<string, SkillGroup> SkillGroups = [];
    public static bool IsUsingLocalFolders = false;
    public static bool IsSplashFormDisabled = false;
    public static string AppFolder = "";
    public static string ApiCacheFolder = "";
    public static string StaticDataFolder = "";
    public static string ImageCacheFolder = "";
    public static string ReportFolder = "";
    public static string BackupFolder = "";
    public static string EveHQBackupFolder = "";
    public static string ItemDBConnectionString = "";
    public static string EveHQDataConnectionString = "";
    public static string DataError = "";
    public static bool IGBActive = false;
    public static SortedList<string, int> APIResults = [];
    public static SortedList<string, string> APIErrors = [];
    public static bool LastAutoAPIResult = true;
    public static DateTime NextAutoAPITime = System.DateTime.Now.AddMinutes(60);
    public static DateTime AutoRetryAPITime = System.DateTime.Now.AddMinutes(5); //' Minimum retry time if an error occurs
    public static G15Lcd EveHqlcd = new();
    public static bool IsG15LcdActive = false;
    public static string LcdPilot = "";
    public static int LcdCharMode = 0;
    public static SortedList<int, double> CustomPriceList = []; //' TypeID, Price
    public static bool APIUpdateAvailable = false;
    public static bool AppUpdateAvailable = false;
    public static DateTime NextAutoMailAPITime = System.DateTime.Now;
    public static SortedList<string, string> Widgets = [];
    //TODO: public static Event ShutDownEveHQ();
    public static bool UpdateShutDownRequest = false;
    public static RemoteProxyServer RemoteProxy = new RemoteProxyServer();
    public static bool APIUpdateInProgress = false;
    //TODO: public static EveHQServerMessage As EveHQMessage
    public static bool RestoredSettings = false;
    public static string BcAppKey = "B23079B49E1FCBB9C224C9D9CC591DF9904C193F";
    public static bool EveHQIsUpdating = false;

    public static EveHQSettings? Settings = new();
    public static Stopwatch EveHQLogTimer  = new Stopwatch();
    
    
    
    
    private static string _appDataFolder = "";
    private static IMarketStatDataProvider? _marketStatDataProvider;
    private static IMarketOrderDataProvider? _marketOrderDataProvider;
    private static DateTime MarketCacheProcessorMinTime = DateTime.Now.AddHours(-1);
    private static IEnumerable<IMarketDataReceiver>? marketDataReceivers;

    private static List<int> _tickerItemList = [];
    private static Stream? _loggingStream;
    private static EveHQTraceLogger? _eveHqTracer;
    private static WebProxyDetails? _proxyDetails;
    private static NewEveApi.EveAPI? _apiProvider;
    private static string _updateLocation = "";
    private static Dictionary<string, EveHQPlugIn> _plugins = [];
    private static EveCentralMarketDataProvider? _eveCentralProvider;
    private static IMarketStatDataProvider? _eveHqProvider;
    private static CcpMarketDataProvider? _ccpMarketDataProvider;
    private static Locations? _locations;

    
    //////////////////////////////////
    
    
    public static Dictionary<string, EveHQPlugIn> Plugins {get => _plugins; set => _plugins = value; }

    public static bool StartShutdownEveHQ
    {
        set
        {
            if (value)
            {
                //TODO ShutDownEveHQ();
            }
        }
    }
    
    public static string AppDataFolder {get => _appDataFolder; set => _appDataFolder = value; }


    public static WebProxyDetails ProxyDetails
    {
        get
        {
            return new WebProxyDetails();
            //TODO: Create proxy details
            // if (!Settings.ProxyRequired)
            // {
            //     return new WebProxyDetails();
            // }
            //
            // if (_proxyDetails == null)
            // {
            //     _proxyDetails = new WebProxyDetails();
            //     _proxyDetails.ProxyPassword = Settings.ProxyPassword;
            //     var scheme = String.Empty;
            //     if (!Settings.ProxyServer.StartsWith("http://"))
            //     {
            //         scheme = "http://";
            //     }
            //     _proxyDetails.ProxyServerAddress = new Uri(scheme + Settings.ProxyServer)
            //     _proxyDetails.ProxyUserName = Settings.ProxyUsername;
            //     _proxyDetails.UseBasicAuth = Settings.ProxyUseBasic;
            //     _proxyDetails.UseDefaultCredential = Settings.ProxyUseDefault;
            // }
            //
            // return _proxyDetails;
        }
    } 
    
    public static NewEveApi.EveAPI ApiProvider
    {
        get
        {
            if (_apiProvider == null)
            {
                _apiProvider = new NewEveApi.EveAPI(ApiCacheFolder, new HttpRequestProvider(ProxyDetails));
            }

            return _apiProvider;
        }
    }
    
    public static void WriteLogEvent(string message){}
    
}