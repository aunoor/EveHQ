using Newtonsoft.Json;

namespace EveHQ.Esi.Models.Esi
{
    public class MarketPrice
    {
        [JsonProperty("adjusted_price")]
        public double AdjustedPrice { get; set; }

        [JsonProperty("average_price")]
        public double AveragePrice { get; set; }

        [JsonProperty("type_id")]
        public long TypeId { get; set; }
    }
}
