using System;
using System.Collections.Generic;
using System.Text.Json.Serialization; 

namespace KoenZomers.Lidl.Api.Entities
{ 
    /// <summary>
    /// Details on one coupon
    /// </summary>
    public class Coupon
    {
        /// <summary>
        /// Unique identifier of the coupon
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// URL to the image showing the product offered in this coupon
        /// </summary>
        [JsonPropertyName("image")]
        public string Image { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("offerTitle")]
        public string OfferTitle { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("offerDescription")]
        public object OfferDescription { get; set; }

        [JsonPropertyName("offerDescriptionShort")]
        public string OfferDescriptionShort { get; set; }

        [JsonPropertyName("description")]
        public object Description { get; set; }

        [JsonPropertyName("familyOfInterest")]
        public object FamilyOfInterest { get; set; }

        [JsonPropertyName("characteristics")]
        public List<object> Characteristics { get; set; }

        [JsonPropertyName("startDisplayDate")]
        public DateTime? StartDisplayDate { get; set; }

        [JsonPropertyName("startValidityDate")]
        public DateTime? StartValidityDate { get; set; }

        [JsonPropertyName("endValidityDate")]
        public DateTime? EndValidityDate { get; set; }

        [JsonPropertyName("brand")]
        public object Brand { get; set; }

        [JsonPropertyName("footerTitle")]
        public object FooterTitle { get; set; }

        [JsonPropertyName("footerDescription")]
        public object FooterDescription { get; set; }

        [JsonPropertyName("url")]
        public object Url { get; set; }

        [JsonPropertyName("blocked")]
        public bool? Blocked { get; set; }

        [JsonPropertyName("featured")]
        public bool? Featured { get; set; }

        [JsonPropertyName("isActivated")]
        public bool? IsActivated { get; set; }

        [JsonPropertyName("blockedTitle")]
        public object BlockedTitle { get; set; }

        [JsonPropertyName("blockedDescription")]
        public object BlockedDescription { get; set; }

        [JsonPropertyName("blockedText")]
        public object BlockedText { get; set; }

        [JsonPropertyName("apologizeText")]
        public string ApologizeText { get; set; }

        [JsonPropertyName("apologizeStatus")]
        public bool? ApologizeStatus { get; set; }

        [JsonPropertyName("apologizeTitle")]
        public string ApologizeTitle { get; set; }

        [JsonPropertyName("block1Description")]
        public object Block1Description { get; set; }

        [JsonPropertyName("block1Title")]
        public object Block1Title { get; set; }

        [JsonPropertyName("block2Description")]
        public object Block2Description { get; set; }

        [JsonPropertyName("block2Title")]
        public object Block2Title { get; set; }

        [JsonPropertyName("ticketInfo")]
        public object TicketInfo { get; set; }

        [JsonPropertyName("promotionId")]
        public string PromotionId { get; set; }

        [JsonPropertyName("products")]
        public List<object> Products { get; set; }

        [JsonPropertyName("productsDiscounted")]
        public List<object> ProductsDiscounted { get; set; }

        [JsonPropertyName("stores")]
        public List<object> Stores { get; set; }

        [JsonPropertyName("daysToExpire")]
        public string DaysToExpire { get; set; }

        [JsonPropertyName("segmentId")]
        public object SegmentId { get; set; }

        [JsonPropertyName("tagTitle")]
        public object TagTitle { get; set; }

        [JsonPropertyName("tagSpecial")]
        public string TagSpecial { get; set; }

        [JsonPropertyName("firstColor")]
        public string FirstColor { get; set; }

        [JsonPropertyName("secondaryColor")]
        public string SecondaryColor { get; set; }

        [JsonPropertyName("firstFontColor")]
        public string FirstFontColor { get; set; }

        [JsonPropertyName("secondaryFontColor")]
        public string SecondaryFontColor { get; set; }

        [JsonPropertyName("category")]
        public object Category { get; set; }

        [JsonPropertyName("isSpecial")]
        public bool? IsSpecial { get; set; }

        [JsonPropertyName("hasAlcoholicArticles")]
        public bool? HasAlcoholicArticles { get; set; }

        [JsonPropertyName("hasAsterisk")]
        public bool? HasAsterisk { get; set; }

        [JsonPropertyName("isHappyHour")]
        public bool? IsHappyHour { get; set; }
    }
}