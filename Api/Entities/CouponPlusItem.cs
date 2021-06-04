using System.Text.Json.Serialization;

namespace KoenZomers.Lidl.Api.Entities
{
    /// <summary>
    /// A milestone one can achieve within the coupon plus program
    /// </summary>
    public class CouponPlusItem
    {
        /// <summary>
        /// Amount of spending at Lidl needed to achieve this milestone
        /// </summary>
        [JsonPropertyName("amount")]
        public double? Amount { get; set; }

        /// <summary>
        /// Unique identifier of this milestone
        /// </summary>
        [JsonPropertyName("couponId")]
        public string CouponId { get; set; }

        /// <summary>
        /// Boolean indicating if the reward for reaching this milestone has been redeemed
        /// </summary>
        [JsonPropertyName("isRedeemed")]
        public bool? IsRedeemed { get; set; }

        /// <summary>
        /// Boolean indicating if this milestone has been completed
        /// </summary>
        [JsonPropertyName("isCompleted")]
        public bool? IsCompleted { get; set; }
    }
}