using System;
using System.Collections.Generic;

namespace EveHQ.CoreLib;

/// <summary>
/// Simple class to store the results of downloading the EveHQ Server message
/// </summary>
public class EveHQMessage
{
    public DateTime MessageDate;
    public string MessageTitle;
    public bool AllowIgnore = false;
    public string Message = string.Empty;
    public SortedList<string, string> DisabledPlugins = new SortedList<string, string>();
}