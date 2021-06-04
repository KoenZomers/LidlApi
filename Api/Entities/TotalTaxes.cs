using System.Text.Json.Serialization;

namespace KoenZomers.Lidl.Api.Entities
{ 
    /// <summary>
    /// Sum of all paid taxes
    /// </summary>
    public class TotalTaxes
    {
        /// <summary>
        /// Total amount of paid taxes
        /// </summary>
        [JsonPropertyName("totalAmount")]
        public decimal? TotalAmount { get; set; }

        /// <summary>
        /// Total amount paid over which taxes have been charged
        /// </summary>
        [JsonPropertyName("totalTaxableAmount")]
        public decimal? TotalTaxableAmount { get; set; }

        /// <summary>
        /// Total amount paid minus the paid taxes
        /// </summary>
        [JsonPropertyName("totalNetAmount")]
        public decimal? TotalNetAmount { get; set; }
    }
}