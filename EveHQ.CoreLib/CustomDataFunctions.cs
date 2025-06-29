namespace EveHQ.CoreLib;

/// <summary>
/// Class for handling the custom database - based on SQLite format
/// </summary>
public class CustomDataFunctions_
{
//region "Core Database Access Routines"
    

/// <summary>
/// Function to create the custom DB connection string
/// </summary>
/// <returns>A boolean value indicating if the routine was successful</returns>
public static bool SetEveHQDataConnectionString()
{
    //HQ.EveHQDataConnectionString = "Data Source=\"" + HQ.Settings.CustomDBFileName + "\";Version=3;";    
    return true;
}

}