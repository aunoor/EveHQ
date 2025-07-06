using System;

namespace EveHQ.CoreLib;

public class EveServer
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
        try
        {
            var serverInfo = HQ.ApiProvider.Server.ServerStatus();
            if (serverInfo.IsSuccess)
            {
                var serverIsUp = serverInfo.ResultData.IsServerOpen;
                var serverPlayers = serverInfo.ResultData.OnlinePlayers;
                if (serverIsUp)
                {
                    Status = (int)ServerStatus.Up;
                    Players = serverPlayers;
                }
                else
                {
                    Status = (int)ServerStatus.Down;
                    Players = 0;
                }
            }
            else
            {
                Version = "";
                Players = 0;
                Codename = "";
                Status = (int)ServerStatus.Unknown;
                StatusText = "Server status Unknown";
            }

            LastChecked = DateTime.Now;
        }
        catch (Exception ex)
        {
            Version = "";
            Players = 0;
            Codename = "";
            Status = (int)ServerStatus.Unknown;
            StatusText = "Server status Unknown";            
        }
    }
}