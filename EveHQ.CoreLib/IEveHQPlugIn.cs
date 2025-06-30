using System.Windows.Forms;

namespace EveHQ.CoreLib;

public interface IEveHQPlugIn
{
    EveHQPlugIn GetEveHQPlugInInfo();
    bool EveHQStartUp();
    Form RunEveHQPlugIn();
    object GetPlugInData(object data, int dataType);
    bool SaveAll();
}