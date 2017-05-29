namespace EveHQ.Esi.Models.Esi
{
    /// <summary>
    /// List all factory activity
    /// </summary>
    public enum FactoryActivity : int
    {
        /// <summary>
        /// Design a copying activity
        /// </summary>
        Copying = 5,

        /// <summary>
        /// Design an invention activity
        /// </summary>
        Invention = 8,

        /// <summary>
        /// Design a manufacturing activity
        /// </summary>
        Manufacturing = 1,

        /// <summary>
        /// Design a TE research
        /// </summary>
        ResearchingTimeEfficiency = 3,

        /// <summary>
        /// Design an ME research
        /// </summary>
        ResearchingMaterialEfficiency = 4
    }
}
