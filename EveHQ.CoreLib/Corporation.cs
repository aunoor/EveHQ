using System;
using System.Collections.Generic;

namespace EveHQ.CoreLib;

[Serializable]
public class Corporation
{
    public string Name;
    public string ID;
    public NewEveApi.Entities.CorporateData ApiData;
    public IEnumerable<NewEveApi.Entities.AccountBalance> WalletBalances;
    /// List of all chars supporting this corp (can be multiple)
    public List<string> CharacterIDs;
    /// <summary>
    /// List of all char names supporting this corp (can be multiple)
    /// </summary>
    public List<string> CharacterNames;
    /// <summary>
    /// IDs of all corp accounts which support this corporation (can be multiple)
    /// </summary>
    public List<string> Accounts;
}