using System;
using System.Collections;

namespace EveHQ.CoreLib;

[Serializable]
public class EveAccount
{
    private string cUserID;
    private string cAPIkey;
    private string cFriendlyName;
    private ArrayList cCharacters = new ArrayList();
    private APIKeyTypes cAPIKeyType;
    private APIAccountStatuses cAPIAccountStatus;
    private int cFailedAttempts;
    private DateTime cLastAccountStatusCheck;
    private DateTime cCreateDate;
    private DateTime cPaidUntil;
    private long cLogonCount;
    private long cLogonMinutes;
    private APIKeySystems cAPIKeySystem;
    private long cAccessMask;
    private DateTime cAPIKeyExpiryDate;
    private string cCorpAPIAccountKey;


    public string CorpAPIAccountKey
    {
        get => cCorpAPIAccountKey;
        set => cCorpAPIAccountKey = value;
    }

    public DateTime APIKeyExpiryDate
    {
        get => cAPIKeyExpiryDate;
        set => cAPIKeyExpiryDate = value;
    }

    public long AccessMask
    {
        get => cAccessMask;
        set => cAccessMask = value;
    }

    public APIKeySystems APIKeySystem
    {
        get
        {
            if (cAPIKeySystem == APIKeySystems.Unknown)
            {
                cAPIKeySystem = APIKeySystems.Version2;
            }
            return cAPIKeySystem;
        }
        set => cAPIKeySystem = value;
    }

    public long LogonMinutes
    {
        get => cLogonMinutes;
        set => cLogonMinutes = value;
    }
    
    public long LogonCount 
    {
        get => cLogonCount;
        set => cLogonCount = value;
    }
    
    public DateTime PaidUntil
    {
        get => cPaidUntil;
        set => cPaidUntil = value;
    }
    
    public DateTime CreateDate 
    {
        get => cCreateDate;
        set => cCreateDate = value;
    }
    
    public DateTime LastAccountStatusCheck
    {
        get => cLastAccountStatusCheck;
        set => cLastAccountStatusCheck = value;
    }
    
    public int FailedAttempts
    {
        get => cFailedAttempts;
        set => cFailedAttempts = value;
    }

    public string userID
    {
        get => cUserID;
        set => cUserID = value;
    }

    public string APIKey
    {
        get => cAPIkey;
        set => cAPIkey = value;
    }
    
    public string FriendlyName
    {
        get => cFriendlyName;
        set => cFriendlyName = value;
    }
    
    public ArrayList Characters
    {
        get => cCharacters;
        set => cCharacters = value;
    }
    
    public APIKeyTypes APIKeyType
    {
        get => cAPIKeyType;
        set => cAPIKeyType = value;
    }

    public APIAccountStatuses APIAccountStatus
    {
        get => cAPIAccountStatus;
        set => cAPIAccountStatus = value;
    }
}