using System;
using System.Collections.Generic;

namespace EveHQ.CoreLib;

[Serializable]
public class PriceGroup
{
    public string Name;
    public List<string> TypeIDs = [];
    public List<string> RegionIDs = [];
    public PriceGroupFlags PriceFlags;
    public int PriceListID;
}

public enum PriceGroupFlags : int
{
    MinAll = 1,
    MinBuy = 2,
    MinSell = 4,
    MaxAll = 8,
    MaxBuy = 16,
    MaxSell = 32,
    AvgAll = 64,
    AvgBuy = 128,
    AvgSell = 256,
    MedAll = 512,
    MedBuy = 1024,
    MedSell = 2048,
}