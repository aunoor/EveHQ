using System.Drawing;

namespace EveHQ.CoreLib;

public enum EveHQPlugInStatus
{
    Uninitialised = 0,
    Loading = 1,
    Failed = 2,
    Active = 3,
}

public class EveHQPlugIn
{
    public string Name;
    public string Description;
    public string Author;
    public string MainMenuText;
    public Image MenuImage;
    public bool RunAtStartup;
    public bool RunInIGB;
    public string FileName;
    public string ShortFileName;
    public string FileType;
    public string Version;
    public bool Disabled;
    public bool Available;
    public EveHQPlugInStatus Status;
    public IEveHQPlugIn Instance;
    public object PostStartupData;    
}