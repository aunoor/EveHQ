using System;
using System.Drawing;

namespace EveHQ.CoreLib;

[Serializable]
public class PlugIn
{
    public string Name;
    public bool Disabled;
    [NonSerialized]
    public string Description;
    [NonSerialized]
    public string Author;
    [NonSerialized]
    public string MainMenuText;
    [NonSerialized]
    public Image MenuImage;
    [NonSerialized]
    public bool RunAtStartup;
    [NonSerialized]
    public bool RunInIGB;
    [NonSerialized]
    public string FileName;
    [NonSerialized]
    public string ShortFileName;
    [NonSerialized]
    public string FileType;
    [NonSerialized]
    public string Version;
    [NonSerialized]
    public bool Available;
    [NonSerialized]
    public int Status;
    [NonSerialized]
    public IEveHQPlugIn Instance;
    [NonSerialized]
    public object PostStartupData;

    public enum PlugInStatus
    {
        Uninitialised = 0,
        Loading = 1,
        Failed = 2,
        Active = 3,
    }
}