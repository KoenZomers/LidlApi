using System.Text.Json.Serialization; 

namespace KoenZomers.Lidl.Api.Entities
{ 
    /// <summary>
    /// Details on the paid taxes
    /// </summary>
    public class Tax
    {
        /// <summary>
        /// Identifier of the tax percentage group
        /// </summary>
        [JsonPropertyName("taxGroup")]
        public string TaxGroup { get; set; }

        /// <summary>
        /// Name of the tax percentage group
        /// </summary>
        [JsonPropertyName("taxGroupName")]
        public string TaxGroupName { get; set; }

        /// <summary>
        /// Percentage of tax held for this group
        /// </summary>
        [JsonPropertyName("percentage")]
        public decimal? Percentage { get; set; }

        /// <summary>
        /// Amount of tax withheld
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal? Amount { get; set; }

        /// <summary>
        /// The amount over which the tax has been calculated
        /// </summary>
        [JsonPropertyName("taxableAmount")]
        public decimal? TaxableAmount { get; set; }

        /// <summary>
        /// The paid amount minus the taxes
        /// </summary>
        [JsonPropertyName("netAmount")]
        public decimal? NetAmount { get; set; }
    }
}