using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace EveHQ.CoreLib;

[Serializable]
public class Pilot
{
    public string Name = "";
    public string ID = "";
    public string Account = "";
    public string AccountPosition = "";
    public string Race = "";
    public string Blood = "";
    public string Gender = "";
    public string Corp = "";
    public string CorpID = "";
    public double Isk = 0;
    public string CloneName = "";
    public string CloneSP = "";
    public int SkillPoints = 0;
    public bool Training = false;
    public DateTime TrainingStartTime = DateTime.Now;
    public DateTime TrainingStartTimeActual = DateTime.Now;
    public DateTime TrainingEndTime = DateTime.Now;
    public DateTime TrainingEndTimeActual = DateTime.Now;
    public string TrainingSkillID = "";
    public string TrainingSkillName = "";
    public int TrainingStartSP = 0;
    public int TrainingEndSP = 0;
    public int TrainingCurrentSP = 0;
    public long TrainingCurrentTime = 0;
    public int TrainingSkillLevel = 0;
    public bool TrainingNotifiedNow = false;
    public bool TrainingNotifiedEarly = false;
    public int CAtt = 0;
    public int IAtt = 0;
    public int MAtt = 0;
    public int PAtt = 0;
    public int WAtt = 0;
    public int CImplant = 0;
    public int IImplant = 0;
    public int MImplant = 0;
    public int PImplant = 0;
    public int WImplant = 0;
    public int CImplantA = 0;
    public int IImplantA = 0;
    public int MImplantA = 0;
    public int PImplantA = 0;
    public int WImplantA = 0;
    public int CImplantM = 0;
    public int IImplantM = 0;
    public int MImplantM = 0;
    public int PImplantM = 0;
    public int WImplantM = 0;
    public bool UseManualImplants = false;
    public double CAttT = 0;
    public double IAttT = 0;
    public double MAttT = 0;
    public double PAttT = 0;
    public double WAttT = 0;
    public Collection<PilotSkill> PilotSkills = [];
    public SortedList<long, PilotQueuedSkill> QueuedSkills = [];
    public long QueuedSkillTime;
    public ArrayList Certificates = [];
    public string PrimaryQueue = "";
    public SkillQueue ActiveQueue = new();
    public string ActiveQueueName = "";
    [NonSerialized]
    public SortedList<string, SkillQueue> TrainingQueues = [];
    public Collection<string> Blueprints = [];
    public DateTime CacheFileTime;
    public DateTime CacheExpirationTime;
    public DateTime TrainingFileTime;
    public DateTime TrainingExpirationTime;
    public bool Updated = false;
    public string LastUpdate = "";
    public bool Active = true;
    public string[] KeySkills = new string[53];
    public SortedList<long, PilotStanding> Standings = [];
    public List<CorporationRoles> CorpRoles = [];
    
    public enum KeySkill { 
        Mining = 1,
    MiningUpgrades = 2,
    Astrogeology = 3,
    MiningBarge = 4,
    MiningDrone = 5,
    Exhumers = 6,
    Refining = 7,
    RefiningEfficiency = 8,
    Metallurgy = 9,
    Research = 10,
    Science = 11,
    Industry = 12,
    ProductionEfficiency = 13,
    ArkonorProc = 14,
    BistotProc = 15,
    CrokiteProc = 16,
    DarkOchreProc = 17,
    GneissProc = 18,
    HedbergiteProc = 19,
    HemorphiteProc = 20,
    JaspetProc = 21,
    KerniteProc = 22,
    MercoxitProc = 23,
    OmberProc = 24,
    PlagioclaseProc = 25,
    PyroxeresProc = 26,
    ScorditeProc = 27,
    SpodumainProc = 28,
    VeldsparProc = 29,
    IceProc = 30,
    IceHarvesting = 31,
    DeepCoreMining = 32,
    MiningForeman = 33,
    MiningDirector = 34,
    Learning = 35,
    JumpDriveOperation = 36,
    JumpDriveCalibration = 37,
    JumpFuelConservation = 38,
    JumpFreighters = 39,
    ScrapMetalProc = 40,
    Accounting = 41,
    BrokerRelations = 42,
    Daytrading = 43,
    MarginTrading = 44,
    Marketing = 45,
    Procurement = 46,
    Retail = 47,
    Trade = 48,
    Tycoon = 49,
    Visibility = 50,
    Wholesale = 51,
    Diplomacy = 52,
    Connections = 53,
    }    
}

[Serializable]
public class PilotSkill : ICloneable
{
    public string ID;
    public string Name;
    public string GroupID;
    public int Flag;
    public int Rank;
    public int SP;
    public int Level;
    public int[] LevelUp = new int[5];

    public Object Clone()
    {
        return MemberwiseClone() as PilotSkill;
    }
}

[Serializable]
public class PilotQueuedSkill
{
    public int Position;
    public int SkillID;
    public int Level;
    public long StartSP;
    public long EndSP;
    public DateTime StartTime = new DateTime();
    public DateTime EndTime = new DateTime();    
}

public class PilotStanding
{
    public StandingType Type;
    public long ID; //Key for Standings
    public string Name;
    public double Standing;    
}

