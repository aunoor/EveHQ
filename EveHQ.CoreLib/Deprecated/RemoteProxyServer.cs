using System;
using System.Net;

namespace EveHQ.CoreLib;

/// <summary>
/// Class for storing details of a proxy server to use for various connections to the web
/// </summary>
public class RemoteProxyServer
{
    /// <summary>
    /// Determines whether a Proxy Server should be used or not
    /// </summary>
    /// <returns>A boolean value indicating whether a Proxy Server is required to be used</returns>
    public bool ProxyRequired {get; set;}
    /// <summary>
    /// Holds the host name or IP address of the proxy server
    /// </summary>
    /// <returns>A string containing the host name or IP address of the proxy server</returns>
    public string ProxyServer {get; set;}
    
    /// <summary>
    /// Holds the port number to use for the proxy server
    /// </summary>
    /// <returns>An integer value representing the port number to use for the proxy server</returns>
    public int ProxyPort {get; set;}
    
    /// <summary>
    /// Determines whether default Windows logon information should be used for the proxy server
    /// </summary>
    /// <returns>A value indicating whether default Windows credentials are passed to the proxy server</returns>
    public bool UseDefaultCredentials {get; set;}
    
    /// <summary>
    /// Determines if the Proxy is using "Basic" authentication as opposed to "NTLM"
    /// </summary>
    public bool UseBasicAuthentication {get; set;}
    
    /// <summary>
    /// Holds the username of the credentials used to access the proxy server
    /// </summary>
    /// <returns>A string containing the username to access the proxy server</returns>
    public string ProxyUsername {get; set;}
    
    /// <summary>
    /// Holds the password of the credentials used to access the proxy server
    /// </summary>
    /// <returns>A string containing the password to access the proxy server</returns>
    public string ProxyPassword {get; set;}

    [Obsolete("SetupWebProxy is deprecated.")]
    public WebProxy SetupWebProxy()
    {
        return WebProxy.GetDefaultProxy();
    } 
}