using System;
using EveHQ.Market;

namespace EveHQ.CoreLib;

[Serializable]
public class ItemMarketOverride
{
    public int ItemId { get; set; }
    public MarketTransactionKind TransactionType  { get; set; }
    public MarketMetric MarketStat {get; set;}
}