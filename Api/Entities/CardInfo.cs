using System.Text.Json.Serialization; 

namespace KoenZomers.Lidl.Api.Entities
{ 
    /// <summary>
    /// Details on the card used to pay
    /// </summary>
    public class CardInfo
    {
        [JsonPropertyName("accountNumber")]
        public string AccountNumber { get; set; }

        [JsonPropertyName("authorizationMethodCode")]
        public string AuthorizationMethodCode { get; set; }

        [JsonPropertyName("transactionNumber")]
        public string TransactionNumber { get; set; }

        [JsonPropertyName("cardTypeCode")]
        public string CardTypeCode { get; set; }

        [JsonPropertyName("transactionTypeCode")]
        public string TransactionTypeCode { get; set; }

        [JsonPropertyName("terminalId")]
        public string TerminalId { get; set; }

        [JsonPropertyName("authorizationMiscSettlementData")]
        public string AuthorizationMiscSettlementData { get; set; }
    }
}