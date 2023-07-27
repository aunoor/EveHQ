using System;
using ESI.NET;
using ESI.NET.Enumerations;
using Microsoft.Extensions.Options;

namespace EveHQ.Esi
{
    public class EveEsiProvider
    {
        private static EveEsiProvider _instance = null;
        private static string APP_USER_AGENT = "EveHQ.Reloaded";
        public EsiClient EsiClient { get; }

        public static EveEsiProvider Instance()
        {
            if (_instance == null)
            {
                _instance = new EveEsiProvider();
            }
            return _instance;
        }

        public static void SetUserAgent(string userAgent)
        {
            APP_USER_AGENT = userAgent;
        }

        /// <summary>
        /// Function add random amount minutes to response Expires value to prevent simultaneously expired caches
        /// </summary>
        /// <param name="expires"></param>
        /// <returns></returns>
        public static DateTime RandomAddToExpire(DateTime? expires)
        {
            Random rnd = new Random();
            var extraMinutes = rnd.Next(10, 20);

            var cachedUntil = expires?.AddMinutes(extraMinutes);
            if (cachedUntil == null)
            {
                var dn = DateTime.UtcNow;
                cachedUntil = new DateTime(dn.Year, dn.Month, dn.Day, 11, 15 + extraMinutes, 0, DateTimeKind.Utc).AddDays(1);
            }

            return cachedUntil.Value;
        }

        private EveEsiProvider()
        {
            IOptions<EsiConfig> config = Options.Create(new EsiConfig()
            {
                EsiUrl = "https://esi.evetech.net/",
                DataSource = DataSource.Tranquility,
                ClientId = "",
                SecretKey = "",
                CallbackUrl = "",
                UserAgent = APP_USER_AGENT,
            });

            EsiClient = new EsiClient(config);
        }
    }
}
