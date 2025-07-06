namespace EveHQ.CoreLib;

/// <summary>
/// A list of status codes as a result of processing an API Request.
/// Defaults to NotYetProcessed on initialising a new APIRequest
/// </summary>
public enum APIResults : int
{
    /// <summary>
    /// The API Request has not yet been made
    /// </summary>
    NotYetProcessed = -1,

    /// <summary>
    /// A new XML file has been returned
    /// </summary>
    /// <remarks></remarks>
    ReturnedNew = 0,

    /// <summary>
    /// A cached XML file has been returned
    /// </summary>
    /// <remarks></remarks>
    ReturnedCached = 1,

    /// <summary>
    /// The specific page requested could not be found on the API Server
    /// </summary>
    /// <remarks></remarks>
    PageNotFound = 2,

    /// <summary>
    /// A CCP Error code was returned
    /// Read the APILastError and APILastErrorText to get specific details
    /// </summary>
    /// <remarks></remarks>
    CCPError = 3,

    /// <summary>
    /// The API Server does not support the requested API Type
    /// </summary>
    /// <remarks></remarks>
    InvalidFeature = 4,

    /// <summary>
    /// The API Server could not be contacted and a null XML was returned
    /// </summary>
    /// <remarks></remarks>
    APIServerDownReturnedNull = 5,

    /// <summary>
    /// The API Server could not be contacted so a cached XML file was returned
    /// </summary>
    /// <remarks></remarks>
    APIServerDownReturnedCached = 6,

    /// <summary>
    /// The actual response from the API Server has been returned
    /// </summary>
    /// <remarks></remarks>
    ReturnedActual = 7,

    /// <summary>
    /// There was no response from the API Server within a timely period
    /// </summary>
    /// <remarks></remarks>
    TimedOut = 8,

    /// <summary>
    /// An error occured with the API Request but the cause is not known
    /// </summary>
    /// <remarks></remarks>
    UnknownError = 9,

    /// <summary>
    /// An error occured within the EveAPIRequest code
    /// </summary>
    /// <remarks></remarks>
    InternalCodeError = 10
}