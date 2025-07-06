using System;
using System.Collections.Generic;
using EveHQ.NewEveApi;

namespace EveHQ.CoreLib;

[Serializable]
public class EveHQAccount
{
    private APIKeySystems _apiKeySystem = APIKeySystems.Unknown;

    public string CorpApiAccountKey { get; set; } = "";
    public DateTime ApiKeyExpiryDate { get; set; } = DateTime.MinValue;
    public long AccessMask { get; set; }

    public APIKeySystems ApiKeySystem
    {
        get
        {
            if (_apiKeySystem == APIKeySystems.Unknown)
            {
                _apiKeySystem = APIKeySystems.Version2;
            }

            ;
            return _apiKeySystem;
        }
        set => _apiKeySystem = value;
    }

    public long LogonMinutes { get; set; }
    public long LogonCount { get; set; }
    public DateTime PaidUntil { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime LastAccountStatusCheck { get; set; }
    public int FailedAttempts { get; set; }
    public string UserID { get; set; }
    public string APIKey { get; set; }
    public string FriendlyName { get; set; }
    public List<string> Characters { get; set; }
    public APIKeyTypes APIKeyType { get; set; }
    public APIAccountStatuses APIAccountStatus { get; set; }

    public EveApiKey ToAPIAccount()
    {
        var apiAccount = new EveApiKey
        {
            UserID = UserID,
            APIKey = APIKey,
            APIVersion = (APIKeyVersions)ApiKeySystem
        };
        return apiAccount;
    }

    public void CheckAPIKey()
    {
        AccessMask = 0;

        if (ApiKeySystem != APIKeySystems.Version2) return;
        
        var apiResponse = HQ.ApiProvider.Account.ApiKeyInfo(UserID, APIKey);
        if (!apiResponse.IsSuccess) return;
        
        AccessMask = apiResponse.ResultData.AccessMask;
        APIKeyType = apiResponse.ResultData.ApiType switch
        {
            NewEveApi.ApiKeyType.Corporation => APIKeyTypes.Corporation,
            NewEveApi.ApiKeyType.Character => APIKeyTypes.Character,
            NewEveApi.ApiKeyType.Account => APIKeyTypes.Account,
        };
    }

    public List<string> GetCharactersOnAccount()
    {
        var charList = new List<string>();
        do
        {
            var characters = HQ.ApiProvider.Account.Characters(UserID, APIKey);
            if (!characters.IsSuccess)
            {
                break;
            }

            var characterList = characters.ResultData;
            foreach (var character in characterList)
            {
                if (ApiKeySystem != APIKeySystems.Version2) continue;
                if (APIKeyType == APIKeyTypes.Corporation)
                {
                    if (charList.Contains(character.CorporationName) == false)
                    {
                        charList.Add(character.CorporationName);
                    }
                }
                else
                {
                    if (charList.Contains(character.Name) == false)
                    {
                        charList.Add(character.Name);
                    }                            
                }
            }
        } while (false);
        return charList;
    }

    public bool CanUseCharacterAPI(CharacterAccessMasks characterAPIToCheck)
    {
        return AccessMasks.HasCharacterPermissions(AccessMask, characterAPIToCheck);
    }

    public bool CanUseCorporateAPI(CorporateAccessMasks corporateAPIToCheck)
    {
        return AccessMasks.HasCorpPermissions(AccessMask, corporateAPIToCheck);
    }
    
}