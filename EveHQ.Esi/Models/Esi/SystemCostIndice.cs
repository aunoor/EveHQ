namespace EveHQ.Esi.Models.Esi
{
    using Newtonsoft.Json;
    using System;

    public class SystemCostIndice
    {
        private string _activity;

        public int ActivityID { get; set; }

        public string Activity
        {
            get
            {
                return this._activity;
            }

            set
            {
                this._activity = value;

                switch (value)
                {
                    case "manufacturing":
                        this.ActivityID = (int)FactoryActivity.Manufacturing;
                        break;
                    case "invention":
                        this.ActivityID = (int)FactoryActivity.Invention;
                        break;
                    case "copying":
                        this.ActivityID = (int)FactoryActivity.Copying;
                        break;
                    case "researching_time_efficiency":
                        this.ActivityID = (int)FactoryActivity.ResearchingTimeEfficiency;
                        break;
                    case "researching_material_efficiency":
                        this.ActivityID = (int)FactoryActivity.ResearchingMaterialEfficiency;
                        break;
                    default:
                        this.ActivityID = 0;
                        break;
                }
            }
        }

        [JsonProperty("cost_index")]
        public double CostIndex { get; set; }
    }
}
