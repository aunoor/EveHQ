// Проект: EveHQ.NewEveAPI
// Имя файла: StructureNameClient.cs
// GUID файла: 7B211018-BF5F-4D4C-9F13-B6636F134EE0
// Автор: Mike Eshva (mike@eshva.ru)
// Дата создания: 11.08.2017

#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using EveHQ.Caching;
using EveHQ.Common;
using EveHQ.Common.Extensions;
using EveHQ.EveData;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

#endregion


namespace EveHQ.NewEveApi
{
	public sealed class StructureNameClient
	{
		public StructureNameClient(ICacheProvider cacheProvider, IHttpRequestProvider httpRequestProvider)
		{
			_cacheProvider = cacheProvider;
			_httpRequestProvider = httpRequestProvider;
		}

		public Task<EveServiceResponse<EveStructure>> GetStructureNameAsync(long locationId)
		{
			Uri requestUri;
			Uri.TryCreate(StructureNameApiLocation + locationId, UriKind.Absolute, out requestUri);
			var cacheKey = CacheKeyFormat.FormatInvariant(locationId);

			var resultData = GetCacheEntry<EveStructure>(cacheKey);

			return resultData != null && !resultData.IsDirty && resultData.Data.IsSuccess
				? ReturnCachedResponse(resultData)
				: _httpRequestProvider.GetAsync(requestUri).ContinueWith(webTask => ProcessServiceResponse(webTask, cacheKey, 600));
		}

		private EveServiceResponse<EveStructure> ProcessServiceResponse(
			Task<HttpResponseMessage> webTask,
			string cacheKey,
			int defaultCacheSeconds)
		{
			var cacheTime = DateTimeOffset.Now.AddSeconds(defaultCacheSeconds);
			var jsonFromResponse = GetJsonFromResponse(webTask.Result);
			var jObject = JObject.Parse(jsonFromResponse);
			if (jObject.Count == 0)
			{
				return new EveServiceResponse<EveStructure>
						{
							ServiceException = new Exception("Not found."),
							IsSuccessfulHttpStatus = false,
							HttpStatusCode = HttpStatusCode.NotFound,
							CacheUntil = cacheTime
						};
			}

			var result = GetStructureDataFromJson(jsonFromResponse, webTask.Result.StatusCode, cacheTime);
			SetCacheEntry(cacheKey, result);
			return result;
		}

		private EveServiceResponse<EveStructure> GetStructureDataFromJson(
			string response,
			HttpStatusCode responseStatusCode,
			DateTimeOffset cacheTime)
		{
			var structureData = JsonConvert.DeserializeObject<Dictionary<long, JToken>>(response);
			var structureProperties = structureData.Values.First();
			try
			{
				var structure = new EveStructure
								{
									StructureId = structureData.Keys.First(),
									StructureName = GetValueSafely(structureProperties, "name", "Unknow Structure"),
									StructureTypeId = GetValueSafely(structureProperties, "typeId", 35832),
									StructureTypeName = GetValueSafely(structureProperties, "typeName", "Unknown Structure Type"),
									IsPublic = GetValueSafely(structureProperties, "public", true),
									RegionId = GetValueSafely(structureProperties, "regionId", 10000003),
									RegionName = GetValueSafely(structureProperties, "regionName", "Unknown Region"),
									SystemId = GetValueSafely(structureProperties, "systemId", 30000250),
									SystemName = GetValueSafely(structureProperties, "systemName", "Unknown Solar System")
								};

				return new EveServiceResponse<EveStructure>
							{
								ResultData = structure,
								ServiceException = null,
								IsSuccessfulHttpStatus = true,
								HttpStatusCode = responseStatusCode,
								CacheUntil = cacheTime
							};
			}
			catch (Exception exception)
			{
				throw new Exception(
					$"Can not parse a response from the Structure Name API. The response code was {responseStatusCode} and it's body:" +
					$"{Environment.NewLine}{response}",
					exception);
			}
		}

		private TValue GetValueSafely<TValue>(JToken properties, string valueName, TValue defaultValue = default(TValue))
		{
			var property = properties[valueName];
			return property.Type != JTokenType.Null ? property.Value<TValue>() : defaultValue;
		}

		private void SetCacheEntry(string key, EveServiceResponse<EveStructure> data)
		{
			_cacheProvider.Add(key, data, data.CacheUntil);
		}

		private static string GetJsonFromResponse(HttpResponseMessage message)
		{
			var readTask = message.Content.ReadAsStringAsync();
			readTask.Wait();
			var content = readTask.Result;
			return content;
		}

		private CacheItem<EveServiceResponse<T>> GetCacheEntry<T>(string key) where T : class => _cacheProvider.Get<EveServiceResponse<T>>(key);

		private static Task<EveServiceResponse<T>> ReturnCachedResponse<T>(CacheItem<EveServiceResponse<T>> cachedResult) where T : class
		{
			cachedResult.Data.CachedResponse = true;
			return Task.Factory.StartNew(() => cachedResult.Data);
		}

		private readonly ICacheProvider _cacheProvider;
		private readonly IHttpRequestProvider _httpRequestProvider;
		private const string CacheKeyFormat = "EveStructureKey_{0}";
		private const string StructureNameApiLocation = "https://stop.hammerti.me.uk/api/citadel/";
	}
}