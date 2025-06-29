using System;

namespace EveHQ.CoreLib;

[Serializable]
public class EveHQPilotSkill : ICloneable
{
    public int ID { get; set; }
    public string Name { get; set; }
    public int GroupID { get; set; }
    public int Rank { get; set; }
    public int SP {get; set;}
    public int Level {get; set;}

    public object Clone()
    {
        return MemberwiseClone();
    }
}