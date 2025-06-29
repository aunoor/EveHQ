namespace EveHQ.CoreLib;

public enum APIKeyVersions : int
{
    Unknown = 0,
    Version1 = 1,
    Version2 = 2,    
}

/// <summary>
/// Class for storing the Eve API Account details for use in the EveAPI classes
/// </summary>
public class EveApiKey
{
    /// <summary>
    /// Holds the userID element of the API account data
    /// </summary>
    /// <value>The userID of the API Account</value>
    public string UserID { get; set; } = "";

    /// <summary>
    /// Holds the APIKey element of the API account data
    /// </summary>
    /// <value>The APIKey of the API Account</value>
    public string APIKey { get; set; } = "";
    
    /// <summary>
    /// Holds the version of the API account information
    /// </summary>
    /// <value>The version of the API accounts</value>
    public APIKeyVersions APIVersion {get; set;} = APIKeyVersions.Unknown;

    /// <summary>
    /// Creates a new EveAPIAccount
    /// </summary>
    public EveApiKey() {}
    
    /// <summary>
    /// Creates a new EveAPIAccount using the userID and APIKey specified
    /// </summary>
    /// <param name="userID">The userID of the API account</param>
    /// <param name="apiKey">The APIKey of the API account</param>
    /// <param name="apiVersion">The initial version of the API key</param>
    public EveApiKey(string userID, string apiKey, APIKeyVersions apiVersion)
    {
        UserID = userID;
        APIKey = apiKey;
        APIVersion = apiVersion;
    }
}