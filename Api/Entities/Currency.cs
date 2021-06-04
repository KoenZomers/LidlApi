using System.Text.Json.Serialization;

namespace KoenZomers.Lidl.Api.Entities
{
    /// <summary>
    /// Currency data returned by the Lidl API
    /// </summary>
    public class Currency
    {
        /// <summary>
        /// Code representing the currency, i.e. EUR
        /// </summary>
        [JsonPropertyName("code")]
        public string Code{ get; set; }

        /// <summary>
        /// The monetary symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }
    }
}
