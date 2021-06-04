using System.Text.Json.Serialization; 
using System.Collections.Generic; 

namespace KoenZomers.Lidl.Api.Entities
{ 
    /// <summary>
    /// Details on one purchased item
    /// </summary>
    public class ItemsLine
    {
        [JsonPropertyName("currentUnitPrice")]
        public string CurrentUnitPrice { get; set; }

        [JsonPropertyName("quantity")]
        public string Quantity { get; set; }

        [JsonPropertyName("isWeight")]
        public bool IsWeight { get; set; }

        [JsonPropertyName("originalAmount")]
        public string OriginalAmount { get; set; }

        [JsonPropertyName("extendedAmount")]
        public string ExtendedAmount { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("taxGroup")]
        public string TaxGroup { get; set; }

        [JsonPropertyName("taxGroupName")]
        public string TaxGroupName { get; set; }

        [JsonPropertyName("codeInput")]
        public string CodeInput { get; set; }

        [JsonPropertyName("discounts")]
        public List<object> Discounts { get; set; }

        [JsonPropertyName("deposit")]
        public object Deposit { get; set; }

        [JsonPropertyName("giftSerialNumber")]
        public object GiftSerialNumber { get; set; }
    }
}