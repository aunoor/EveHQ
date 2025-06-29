using System;

namespace EveHQ.CoreLib;

[Serializable]
public class EveHQPilotQueuedSkill
{
    public int Position;
    public int SkillID;
    public int Level;
    public long StartSP;
    public long EndSP;
    public DateTime StartTime = new();
    public DateTime EndTime = new();
}