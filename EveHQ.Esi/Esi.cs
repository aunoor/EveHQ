namespace EveHQ.Esi
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using System.Threading.Tasks;
    using EveHQ.Common;
    using EveHQ.Common.Extensions;
    using EveHQ.Esi.Models.Cache;
    using EveHQ.Esi.Models.Esi;
    
    /// <summary>
    /// This class is a conversion from CREST project for ESI, waiting a better ESI integration 
    /// </summary>
    public class Esi
    {
        /// <summary>
        /// The base of all ESI URI request
        /// </summary>
        private const string BaseEsiEndpoint = "https://esi.tech.ccp.is";

        /// <summary>
        /// Store all request call including the pull content until its expiration
        /// </summary>
        private static Dictionary<Uri, CacheItem> _cache;

        /// <summary>
        /// singleton property
        /// </summary>
        private static Esi _instance;

        /// <summary>
        /// instantiate the http request only once
        /// </summary>
        private readonly IHttpRequestProvider _requestProvider;

        /// <summary>
        /// Prevents a default instance of the Esi class from being created.
        /// </summary>
        private Esi()
        {
            _cache = new Dictionary<Uri, CacheItem>();
            _requestProvider = new HttpRequestProvider(null);
        }

        /// <summary>
        /// Return an ESI instance on which make ESI requests
        /// </summary>
        /// <returns>ESI instance</returns>
        public static Esi Instance()
        {
            if (_instance == null)
            {
                _instance = new Esi();
            }

            return _instance;
        }

        #region "Industry"
        /// <summary>
        /// Fetch industry system data from ESI (including system name and indices)
        /// </summary>    
        /// <returns>A list of industry indices from all systems</returns>
        public Task<Dictionary<long, IndustrySystem>> GetIndustrySystems()
        {
            return Task<Dictionary<long, IndustrySystem>>.Factory.TryRun(
                () =>
                {
                    IEnumerable<IndustrySystem> result;
                    Uri esiEndpoint = new Uri(
                        string.Format("{0}/v1/industry/systems/", BaseEsiEndpoint));
                    Dictionary<long, IndustrySystem> systems = new Dictionary<long, IndustrySystem>();

                    if (!IsCached(esiEndpoint))
                    {
                        FetchData(esiEndpoint);
                    }

                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<IndustrySystem>>(_cache[esiEndpoint].CachedData);

                    if (result != null)
                    {
                        foreach (var system in result)
                        {
                            if (!systems.ContainsKey(system.SolarSystemId))
                            {
                                systems.Add(system.SolarSystemId, system);
                            }
                        }
                    }

                    return systems;
                });
        }
        #endregion

        #region "Market"
        /// <summary>
        /// Fetch market prices (average and adjusted) from ESI
        /// </summary>
        /// <returns>A list of all market prices</returns>
        public Task<Dictionary<long, MarketPrice>> FetchMarketPrices()
        {
            return Task<Dictionary<long, MarketPrice>>.Factory.TryRun(
                () =>
                {
                    IEnumerable<MarketPrice> result;
                    Uri esiEndpoint = new Uri(
                        string.Format("{0}/v1/markets/prices/", BaseEsiEndpoint));
                    Dictionary<long, MarketPrice> prices = new Dictionary<long, MarketPrice>();

                    if (!IsCached(esiEndpoint))
                    {
                        FetchData(esiEndpoint);
                    }

                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<MarketPrice>>(_cache[esiEndpoint].CachedData);

                    if (result != null)
                    {
                        foreach (var price in result)
                        {
                            if (!prices.ContainsKey(price.TypeId))
                            {
                                prices.Add(price.TypeId, price);
                            }
                        }
                    }

                    return prices;
                });
        }
        #endregion

        #region "Helpers"
        /// <summary>
        /// Send request to ESI, fetch data in an async process and store the result to the cache
        /// </summary>
        /// <param name="esiEndpoint">The endpoint to call</param>
        public void FetchData(Uri esiEndpoint)
        {
            // call data, parse, and so on
            Task<HttpResponseMessage> requestTask = _requestProvider.GetAsync(esiEndpoint);
            requestTask.Wait();

            if (requestTask.IsCompleted && !requestTask.IsCanceled && !requestTask.IsFaulted &&
                requestTask.Exception == null && requestTask.Result != null)
            {
                Task<Stream> contentStreamTask = requestTask.Result.Content.ReadAsStreamAsync();
                contentStreamTask.Wait();

                using (Stream stream = contentStreamTask.Result)
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        if (!_cache.ContainsKey(esiEndpoint))
                        {
                            _cache.Add(esiEndpoint, new CacheItem(1, reader.ReadToEnd()));
                        }
                    }
                }
            }
        }
        
        /// <summary>
        /// Enable to check if an endpoint result is still in cache
        /// </summary>
        /// <param name="esiEndpoint">The ESI endpoint to check</param>
        /// <returns>True if the endpoint is cached</returns>
        private bool IsCached(Uri esiEndpoint)
        {
            long currentTimestamp = (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;

            if (_cache.ContainsKey(esiEndpoint))
            {
                if (_cache[esiEndpoint].ExpiredTimestamp > currentTimestamp)
                {
                    return true;
                }

                _cache.Remove(esiEndpoint);
            }

            return false;
        }
        #endregion
    }
}
