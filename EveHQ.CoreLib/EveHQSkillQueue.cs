using System;
using System.Collections.Generic;

namespace EveHQ.CoreLib;

[Serializable]
// ReSharper disable once InconsistentNaming
public class EveHQSkillQueue : ICloneable
{
    #region "Property variables"

    private string _name = "";
    private bool _incCurrentTraining;
    private Dictionary<string, EveHQSkillQueueItem> _queue = [];
    private bool _primary;
    private long _queueTime;
    private int _queueSkills;
    private bool _showCompletedSkills;

    #endregion
    
    #region "Properties"
    
    /// <summary>
    /// Specifies the name of the skill queue
    /// </summary>
    /// <value>The name of the skill queue</value>
    public string Name
    {
        get => _name;
        set => _name = value;
    }

    /// <summary>
    /// A boolean value indicating whether the skill queue should contain the current training skill as the first item
    /// </summary>
    /// <value>Whether the skill queue should contain the current training skill as the first item</value>
    public bool IncCurrentTraining
    {
        get => _incCurrentTraining;
        set => _incCurrentTraining = value;
    }

    /// <summary>
    /// Contains a collection of EveHQ.Core.EveHQSkillQueueItem objects
    /// </summary>
    /// <value>A collection of EveHQ.Core.EveHQSkillQueueItem objects</value>
    public Dictionary<string, EveHQSkillQueueItem> Queue
    {
        get => _queue;
        set => _queue = value;
    }

    /// <summary>
    /// A boolean value indicating whether the current skill queue in the main (or primary) queue for a pilot.
    /// Only one skill queue in the pilot's collection should be Primary
    /// </summary>
    /// <value>Boolean value indicating whether this in the primary skill queue for a pilot</value>
    public bool Primary
    {
        get => _primary;
        set => _primary = value;
    }


    /// <summary>
    /// Specifies the length of time (in seconds) of the skill queue after being processed
    /// </summary>
    /// <value>The length of time (in seconds) of the skill queue after being processed</value>
    public long QueueTime
    {
        get => _queueTime;
        set => _queueTime = value;
    }

    /// <summary>
    /// Specifies the number of unique entries in the skill queue
    /// </summary>
    /// <value>Specifies the number of unique entries in the skill queue</value>
    public int QueueSkills
    {
        get => _queueSkills;
        set => _queueSkills = value;
    }
    
    /// <summary>
    /// A boolean value indicating whether the current skill queue should show completed skills
    /// </summary>
    /// <value>Boolean value indicating whether the skill queue shows completed skills</value>    
    public bool ShowCompletedSkills 
    {
        get => _showCompletedSkills;
        set => _showCompletedSkills = value;        
    }
    
    #endregion
    
    /// <summary>
    /// Routine for cloning an entire skill queue
    /// </summary>
    /// <returns>A copy of the instance of EveHQ.Core.SkillQueue from where the function was called</returns>    
    public object Clone()
    {
        var newQueue = MemberwiseClone() as EveHQSkillQueue;
        var newQ = new Dictionary<string, EveHQSkillQueueItem>();
        foreach (var item in _queue.Values)
        {
            var nItem = new EveHQSkillQueueItem
            {
                Name = item.Name,
                FromLevel = item.FromLevel,
                ToLevel = item.ToLevel,
                Pos = item.Pos,
                Notes = item.Notes,
                Priority = item.Priority
            };
            if (!newQ.ContainsKey(nItem.Key))
            {
                newQ.Add(nItem.Key, nItem);
            }
        }
        newQueue.Queue = newQ;
        return newQueue;
    }    
}