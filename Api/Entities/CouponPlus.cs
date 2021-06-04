using System.Collections.Generic;
using System.Text.Json.Serialization; 

namespace KoenZomers.Lidl.Api.Entities
{ 
    /// <summary>
    /// The temporary, typically monthly, coupon plus deal where you can earn gifts depending on how much you spend at Lidl during that month
    /// </summary>
    public class CouponPlus
    {
        /// <summary>
        /// Unique identifier of this coupon plus
        /// </summary>
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("promotionId")]
        public string PromotionId { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("sectionTitle")]
        public string SectionTitle { get; set; }

        [JsonPropertyName("detailInformationTitle")]
        public string DetailInformationTitle { get; set; }

        [JsonPropertyName("detailInformationDescription")]
        public string DetailInformationDescription { get; set; }

        /// <summary>
        /// Currently reached amount of purchases towards this coupon plus deal
        /// </summary>
        [JsonPropertyName("reachedAmount")]
        public double? ReachedAmount { get; set; }

        /// <summary>
        /// Percentage of completion of the purchase amount needed to entirely make use of this coupon plus
        /// </summary>
        [JsonPropertyName("reachedPercent")]
        public double? ReachedPercent { get; set; }

        /// <summary>
        /// Amount of days left before the coupon plus deal expires
        /// </summary>
        [JsonPropertyName("expirationDays")]
        public int? ExpirationDays { get; set; }

        /// <summary>
        /// Boolean indicating if the Lidl Plus app should actively notify the user when this deal is about to expire
        /// </summary>
        [JsonPropertyName("expirationWarning")]
        public bool? ExpirationWarning { get; set; }

        /// <summary>
        /// Details on the individual milestones one can achieve during this deal
        /// </summary>
        [JsonPropertyName("items")]
        public List<CouponPlusItem> Items { get; set; }

        [JsonPropertyName("reachedAmountGoal")]
        public double? ReachedAmountGoal { get; set; }

        [JsonPropertyName("reachedPercentGoal")]
        public double? ReachedPercentGoal { get; set; }

        [JsonPropertyName("giveawayPrizes")]
        public List<object> GiveawayPrizes { get; set; }
    }
}