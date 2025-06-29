using System;

namespace EveHQ.CoreLib;

public class EveServer_
{
    public enum ServerStatus : int
    {
        Up = -1,
        Down = 0,
        Starting = 1,
        Unknown = 2,
        Shutting = 3,
        Full = 4,
        ProxyDown = 5,
    }

    public enum Servers : int
    {
        Tranquility = 0,
        Singularity = 1
    }

    public int Server = (int)Servers.Tranquility;
    public string ServerName  = "Tranquility";
    public string Version = "";
    public int Players;
    public string Codename = "";
    public int Status = (int)ServerStatus.Unknown;
    public int LastStatus = (int)ServerStatus.Unknown;
    public string StatusText = "";
    public DateTime LastChecked = DateTime.MinValue;

    public void GetServerStatus()
    {
        //TODO: GetServerStatus()
    }
}