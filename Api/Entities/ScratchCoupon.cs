using System;
using System.Text.Json.Serialization; 

namespace KoenZomers.Lidl.Api.Entities
{ 
    /// <summary>
    /// Details on one scratchcoupon
    /// </summary>
    public class ScratchCoupon
    {
        /// <summary>
        /// Unique identifier of the scratch coupon
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// Unique identifier of promoting this item
        /// </summary>
        [JsonPropertyName("promotionId")]
        public string PromotionId { get; set; }

        /// <summary>
        /// Type of scratch coupon, typically Scratch
        /// </summary>
        [JsonPropertyName("promotionType")]
        public string PromotionType { get; set; }

        /// <summary>
        /// Date and time at which it was issued
        /// </summary>
        [JsonPropertyName("creationDate")]
        public DateTime? CreationDate { get; set; }

        /// <summary>
        /// Date and time at which it will no longer be available
        /// </summary>
        [JsonPropertyName("expirationDate")]
        public DateTime? ExpirationDate { get; set; }

        /// <summary>
        /// URL to Azure blob storage where the image for this coupon resides
        /// </summary>
        [JsonPropertyName("logo")]
        public string Logo { get; set; }

        /// <summary>
        /// Number of days remaining that this scratch coupon can be used
        /// </summary>
        [JsonPropertyName("remainingDays")]
        public int? RemainingDays { get; set; }
    }
}