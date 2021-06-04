using System.Text.Json.Serialization; 
using System; 

namespace KoenZomers.Lidl.Api.Entities
{ 
    /// <summary>
    /// Details on the payment
    /// </summary>
    public class Payment
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("amount")]
        public string Amount { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("roundingDifference")]
        public object RoundingDifference { get; set; }

        [JsonPropertyName("foreignPayment")]
        public ForeignPayment ForeignPayment { get; set; }

        [JsonPropertyName("cardInfo")]
        public CardInfo CardInfo { get; set; }

        [JsonPropertyName("beginDate")]
        public DateTime BeginDate { get; set; }

        [JsonPropertyName("endDate")]
        public DateTime EndDate { get; set; }

        [JsonPropertyName("rawPaymentInformationHTML")]
        public string RawPaymentInformationHTML { get; set; }
    }
}