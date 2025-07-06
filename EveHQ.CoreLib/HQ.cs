using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using DevComponents.DotNetBar;
using EveHQ.Common;
using EveHQ.Common.Logging;
using EveHQ.Market;
using EveHQ.Market.MarketServices;
using EveHQ.NewEveAPI;

#if NET48
using System.Windows.Forms;
#endif

namespace EveHQ.CoreLib;


public class HQ
{
    #if NET48
    public static Form? MainForm = null;
    #endif
    public static SortedList<string, EveHQPilot> tempPilots1 = [];
    public static SortedList<string, Corporation> TempCorps = [];

    public static EveServer MyTqServer = new EveServer();
    public static EveHQMessage EveHQServerMessage;
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
    
    public delegate void ShutDownEveHQHandler();
    public static event ShutDownEveHQHandler ShutDownEveHQ;
    
    public static bool UpdateShutDownRequest = false;
    public static RemoteProxyServer RemoteProxy = new RemoteProxyServer();
    public static bool APIUpdateInProgress = false;
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
                ShutDownEveHQ?.Invoke();
                //TODO ShutDownEveHQ();
            }
        }
    }
    
    public static string AppDataFolder {get => _appDataFolder; set => _appDataFolder = value; }

    public static IMarketStatDataProvider MarketStatDataProvider
    {
        get
        {
            if (_marketStatDataProvider == null)
            {
                if (Settings.MarketDataProvider == EveCentralMarketDataProvider.Name)
                {
                    _marketStatDataProvider = GetEveCentralMarketInstance();
                }
            }

            return _marketStatDataProvider;
        }
        set => _marketStatDataProvider = value;
    }

    public static List<int> TickerItemList
    {
        get
        {
            if (_tickerItemList.Count == 0)
            {
                //Add place holder mineral types only
                _tickerItemList.Add(34);
                _tickerItemList.Add(35);
                _tickerItemList.Add(36);
                _tickerItemList.Add(37);
                _tickerItemList.Add(38);
                _tickerItemList.Add(39);
                _tickerItemList.Add(40);
                _tickerItemList.Add(11399);
            }
            return _tickerItemList;
        }
        set => _tickerItemList = value;
    }


    public static IMarketOrderDataProvider MarketOrderDataProvider
    {
        get
        {
            if (Settings.MarketDataProvider == EveCentralMarketDataProvider.Name)
            {
                _marketOrderDataProvider = GetEveCentralMarketInstance();
            } else if (Settings.MarketDataProvider == CcpMarketDataProvider.Name)
            {
                _marketOrderDataProvider = GetCcpMarketStatDataProvider();
            }
            else
            {
                _marketOrderDataProvider = new StabMarketOrderDataProvider();
            }

            return _marketOrderDataProvider;
        }
    }

    public static Stream LoggingStream
    {
        get => _loggingStream;
        set => _loggingStream = value;
    }

    public static EveHQTraceLogger EveHQTracer
    {
        get => _eveHqTracer;
        set => _eveHqTracer = value;
    }
    
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

    public static String UpdateLocation
    {
        get => _updateLocation;
        set => _updateLocation = value;
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

    public static Locations Locations
    {
        get
        {
            if (_locations == null)
            {
                _locations = new Locations(ApiProvider.StructureName, WriteLogEvent);
            }
            return _locations;
        }
    }

    public static SortedList<string, EveHQPilot> TempPilots
    {
        get => tempPilots1;
        set => tempPilots1 = value;
    }

    public static void ReduceMemory()
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();
    }
    
    public static void WriteLogEvent(string eventText)
    {
        TimeSpan ts = EveHQLogTimer.Elapsed;

        string elapsedTime = string.Format("{0:00}:{1:00}:{2:00}.{3:000}", ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds);
        eventText = "[" + elapsedTime + "]" + " " + eventText;
        try
        {
            Trace.WriteLine(eventText, "Information");
        }
        catch
        {
            // ignored
        }
    }

    public static TabItem GetMdiTab(string tabName)
    {
        TabStrip mainTab = MainForm.Controls["tabEveHQMDI"] as TabStrip;
        if (mainTab != null)
        {
            foreach (TabItem tp in mainTab.Tabs)
            {
                if (tp.Text == tabName) {
                    return tp;
                }
            }
        }

        return null;
    }


    public static EveCentralMarketDataProvider GetEveCentralMarketInstance()
    {
        if (_eveCentralProvider == null)
        {
            if (Settings.ProxyRequired)
            {
                _eveCentralProvider = new EveCentralMarketDataProvider(
                    Path.Combine(AppDataFolder, "MarketCache\\EveCentral"), new HttpRequestProvider(ProxyDetails));
            }
            else
            {
                _eveCentralProvider = new EveCentralMarketDataProvider(
                    Path.Combine(AppDataFolder, "MarketCache\\EveCentral"), new HttpRequestProvider(null));
            }
        }

        return _eveCentralProvider;
    }

    public static IMarketStatDataProvider GetFuzzworkMarketStatDataProvider()
    {
        if (_eveHqProvider == null)
        {
            if (Settings.ProxyRequired)
            {
                _eveHqProvider = new FuzzworkMarketStatDataProvider(
                    Path.Combine(AppDataFolder, "MarketCache\\Fuzzwork"), new HttpRequestProvider(ProxyDetails),
                    new SupportedMarket());
            } else {
                _eveHqProvider = new FuzzworkMarketStatDataProvider(
                    Path.Combine(AppDataFolder, "MarketCache\\Fuzzwork"), new HttpRequestProvider(null),
                    new SupportedMarket());
            }
        }

        return _eveHqProvider;
    }

    public static CcpMarketDataProvider GetCcpMarketStatDataProvider()
    {
        if (_eveHqProvider == null)
        {
            if (Settings.ProxyRequired)
            {
                _ccpMarketDataProvider = new CcpMarketDataProvider(
                    Path.Combine(AppDataFolder, "MarketCache\\Ccp"), new HttpRequestProvider(ProxyDetails),
                    new SupportedMarket(), Locations);
            }
            else
            {
                _ccpMarketDataProvider = new CcpMarketDataProvider(
                    Path.Combine(AppDataFolder, "MarketCache\\Ccp"), new HttpRequestProvider(null),
                    new SupportedMarket(), Locations);
            }
        }

        return _ccpMarketDataProvider;
    }
}

public class ListViewItemComparerText : IComparer
{
    private int _col;
    private SortOrder _order;

    public ListViewItemComparerText()
    {
        _col = 0;
        _order = SortOrder.Ascending;
    }
    
    public ListViewItemComparerText(int column, SortOrder order)
    {
        _col = column;
        _order = order;
    }

    public int Compare(object x, object y)
    {
        int returnVal = -1;

        var a = ((ListViewItem)x).SubItems[_col].Text;
        var b = ((ListViewItem)y).SubItems[_col].Text;
        
        decimal firstNumber = 0;
        decimal secondNumber = 0;
        
        if (decimal.TryParse(a, out firstNumber) &&
            decimal.TryParse(b, out secondNumber))
        {
            returnVal = Decimal.Compare(firstNumber, secondNumber);
        }
        else
        {
            returnVal = String.Compare(a, b);
        }
        
        //Determine whether the sort order is descending.
        if (_order == SortOrder.Descending)
        {
            //Invert the value returned by String.Compare.
            returnVal *= -1;
        }
        
        return returnVal;
    }
}

public class ListViewItemComparerName : IComparer
{
    private int _col;
    private SortOrder _order;

    public ListViewItemComparerName()
    {
        _col = 0;
        _order = SortOrder.Ascending;
    }
    
    public ListViewItemComparerName(int column, SortOrder order)
    {
        _col = column;
        _order = order;
    }

    public int Compare(object x, object y)
    {
        int returnVal = -1;

        var a = ((ListViewItem)x).SubItems[_col].Name;
        var b = ((ListViewItem)y).SubItems[_col].Name;
        
        decimal firstNumber = 0;
        decimal secondNumber = 0;
        
        if (decimal.TryParse(a, out firstNumber) &&
            decimal.TryParse(b, out secondNumber))
        {
            returnVal = Decimal.Compare(firstNumber, secondNumber);
        }
        else
        {
            returnVal = String.Compare(a, b);
        }
        
        //Determine whether the sort order is descending.
        if (_order == SortOrder.Descending)
        {
            //Invert the value returned by String.Compare.
            returnVal *= -1;
        }
        
        return returnVal;
    }
}