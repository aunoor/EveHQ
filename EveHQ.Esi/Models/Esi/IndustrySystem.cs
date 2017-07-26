namespace EveHQ.Esi.Models.Esi
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    /// <summary>
    /// Contain cost indices for a solar system
    /// </summary>
    public class IndustrySystem
    {
        /// <summary>
        /// Gets the solar system ID - use SDE in order to get associated information
        /// </summary>
        [JsonProperty("solar_system_id")]
        public long SolarSystemId { get; set; }

        /// <summary>
        /// Gets the list of all cost indices related to this system
        /// </summary>
        [JsonProperty("cost_indices")]
        public IEnumerable<SystemCostIndice> CostIndices { get; set; }
        
        /// <summary>
        /// Return the specific cost indice related to the factory activity
        /// </summary>
        /// <param name="activity">The activity type for which we want the cost indice</param>
        /// <returns>A cost indice</returns>
        public SystemCostIndice GetSystemCostIndice(FactoryActivity activity)
        {
            int activityID = (int)activity;
            foreach (var costIndice in CostIndices)
            {
                if (costIndice.ActivityID == activityID)
                {
                    return costIndice;
                }
            }

            return null;
        }
    }
}
