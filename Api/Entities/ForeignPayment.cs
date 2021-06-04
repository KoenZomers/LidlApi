using System.Text.Json.Serialization; 

namespace KoenZomers.Lidl.Api.Entities
{ 
    /// <summary>
    /// Details on the payment made and in which currency
    /// </summary>
    public class ForeignPayment
    {
        [JsonPropertyName("foreignAmount")]
        public object ForeignAmount { get; set; }

        [JsonPropertyName("amount")]
        public string Amount { get; set; }

        [JsonPropertyName("foreignCurrency")]
        public string ForeignCurrency { get; set; }
    }

}