using System.Text.Json.Serialization; 

namespace KoenZomers.Lidl.Api.Entities
{ 
    /// <summary>
    /// German fiscal information
    /// </summary>
    public class FiscalDataDe
    {
        [JsonPropertyName("fiscalSequenceNumber")]
        public object FiscalSequenceNumber { get; set; }

        [JsonPropertyName("fiscalSignatureStart")]
        public object FiscalSignatureStart { get; set; }

        [JsonPropertyName("fiscalTimeStamp")]
        public object FiscalTimeStamp { get; set; }

        [JsonPropertyName("fiscalSignatureCounter")]
        public object FiscalSignatureCounter { get; set; }

        [JsonPropertyName("fiscalReceiptLabel")]
        public object FiscalReceiptLabel { get; set; }

        [JsonPropertyName("posSerialnumber")]
        public object PosSerialnumber { get; set; }
    }
}