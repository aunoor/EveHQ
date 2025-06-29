using System;
using System.Collections.Generic;

namespace EveHQ.CoreLib;

[Serializable]
public class EveSkill : ICloneable
{
    public int ID;
    public string Name = "";
    public string Description = "";
    public string GroupID = "";
    public bool Published;
    public int Rank;
    public int SP;
    public int Level;
    public int[] LevelUp = [5];
    public string Pa = "";
    public string Sa = "";
    public Dictionary<int, int> PreReqSkills = [];
    public double BasePrice;

    public object Clone()
    {
        return this.MemberwiseClone();
    }
}

[Serializable]
public class SkillGroup
{
    public int ID;
    public string Name = "";
}