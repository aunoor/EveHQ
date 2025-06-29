using System;
using System.Collections;
using System.Collections.Generic;
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
    [JsonProperty] private string[] _eveFolderLabel = new string[5];
    [JsonProperty] private bool[] _eveFolderLua = new bool[5];
    private SortedList<string, bool> _igbAllowedData = [];
    [JsonProperty] private string[] _eveFolder = new string[5];
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
    }

    #endregion
}