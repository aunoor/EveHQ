using System;
using System.Collections.ObjectModel;

namespace EveHQ.CoreLib;

[Serializable]
public class SkillQueue : ICloneable
{
    #region "Properties"

    /// <summary>
    /// Specifies the name of the skill queue
    /// </summary>
    /// <value>The name of the skill queue</value>
    public string Name { get; set; } = "";

    /// <summary>
    /// A boolean value indicating whether the skill queue should contain the current training skill as the first item
    /// </summary>
    /// <value>Whether the skill queue should contain the current training skill as the first item</value>
    public bool IncCurrentTraining { get; set; } = false;

    /// <summary>
    /// Contains a collection of EveHQ.Core.SkillQueueItem objects
    /// </summary>
    /// <value>A collection of EveHQ.Core.SkillQueueItem objects</value>
    public Collection<SkillQueueItem> Queue { get; set; } = [];

    /// <summary>
    /// A boolean value indicating whether the current skill queue in the main (or primary) queue for a pilot.
    /// Only one skill queue in the pilot's collection should be Primary
    /// </summary>
    /// <value>Boolean value indicating whether this in the primary skill queue for a pilot</value>
    public bool Primary { get; set; } =  false;

    /// <summary>
    /// Specifies the length of time (in seconds) of the skill queue after being processed
    /// </summary>
    /// <value>The length of time (in seconds) of the skill queue after being processed</value>
    public long QueueTime { get; set; } = 0;

    /// <summary>
    /// Specifies the number of unique entries in the skill queue
    /// </summary>
    /// <value>Specifies the number of unique entries in the skill queue</value>
    public int QueueSkills { get; set; } = 0;

    #endregion
    
    public object Clone()
    {
        var newQueue = MemberwiseClone() as SkillQueue;
        var newQ = new Collection<SkillQueueItem>();
        foreach (var item in Queue)
        {
            var nItem = new SkillQueueItem
            {
                Name = item.Name,
                FromLevel = item.FromLevel,
                ToLevel = item.ToLevel,
                Pos = item.Pos,
                Notes = item.Notes,
                Priority = item.Priority
            };
            nItem.Key = nItem.Name + nItem.FromLevel + nItem.ToLevel;
            newQ.Add(nItem);
        }
        newQueue.Queue = newQ;
        return newQueue;
    }
}