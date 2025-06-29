using System;
using System.Collections.Generic;
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
    //TODO: public static EveHqlcd As New G15Lcd
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
    //TODO: public static RemoteProxy As New RemoteProxyServer
    public static bool APIUpdateInProgress = false;
    //TODO: public static EveHQServerMessage As EveHQMessage
    public static bool RestoredSettings = false;
    public static string BcAppKey = "B23079B49E1FCBB9C224C9D9CC591DF9904C193F";
    public static bool EveHQIsUpdating = false;

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
    //TODO: private static Dictionary<string, EveHQPlugIn> _plugins = [];
    private static EveCentralMarketDataProvider? _eveCentralProvider;
    private static IMarketStatDataProvider? _eveHqProvider;
    private static CcpMarketDataProvider? _ccpMarketDataProvider;
    private static Locations? _locations;

}