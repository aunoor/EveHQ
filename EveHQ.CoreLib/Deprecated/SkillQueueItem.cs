using System;

namespace EveHQ.CoreLib;

[Serializable]
public class SkillQueueItem
{
    #region "Property Variables"

    private string _key = "";
    private string _name = "";
    private int _fromLevel;
    private int _toLevel;
    private int _pos;
    private int _priority;
    private string _notes = "";

    #endregion

    #region "Properties"

    /// <summary>
    /// Represents the unique key of the skill queue item
    /// The key is made up of 3 elements: Skill name (Name), FromLevel and ToLevel
    /// These are concatenated to make the key i.e.
    /// "Spaceship Command24" indicates training the "Spaceship Command" skill from level 2 to level 4
    /// </summary>
    /// <value>The unique key of the skill queue item</value>
    public string Key
    {
        get => _key;
        set => _key = value;
    }

    /// <summary>
    /// The name of the skill
    /// </summary>
    /// <value>The name of the skill for the skill queue item</value>
    public string Name
    {
        get => _name;
        set => _name = value;
    }

    /// <summary>
    /// An integer representing the end level of the training queue item
    /// </summary>
    /// <value>The level to which the skill queue item is being trained to</value>
    public int FromLevel
    {
        get => _fromLevel;
        set => _fromLevel = value;
    }

    /// <summary>
    /// An integer representing the end level of the training queue item
    /// </summary>
    /// <value>The level to which the skill queue item is being trained to</value>
    public int ToLevel
    {
        get => _toLevel;
        set => _toLevel = value;
    }

    /// <summary>
    /// An integer containing the skill queue item's position within the skill queue
    /// This should be unique but controlled from other various skill queue functions
    /// </summary>
    /// <value>The position of the skill queue item in the queue</value>
    public int Pos
    {
        get => _pos;
        set => _pos = value;
    }

    /// <summary>
    /// An integer containing the relative priority of a skill queue item
    /// *** CURRENTLY UNUSED ***
    /// </summary>
    /// <value>The priority level of the skill queue item</value>
    /// <returns>A value between 0 and 9</returns>
    public int Priority
    {
        get => _priority;
        set => _priority = value;
    }

    /// <summary>
    /// A string containing user-defined notes for the skill queue item
    /// </summary>
    /// <value>The user-defined notes of a skill queue item</value>
    public string Notes
    {
        get => _notes;
        set => _notes = value;
    }

    #endregion

    /// <summary>
    /// Routine for cloning a skill queue item
    /// </summary>
    /// <returns>A copy of the instance of EveHQ.Core.SkillQueueItem from where the function was called</returns>
    public object Clone()
    {
        return MemberwiseClone();
    }
}