using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KoenZomers.Lidl.Api.Entities
{
    /// <summary>
    /// Receipt data returned by the Lidl API
    /// </summary>
    public class Receipt
    {
        /// <summary>
        /// Unique identifier of the receipt
        /// </summary>
        [JsonPropertyName("id")]
        public string Id{ get; set; }

        /// <summary>
        /// Boolean indicating if the receipt has been marked as a favorite
        /// </summary>
        [JsonPropertyName("isFavorite")]
        public bool? IsFavorite { get; set; }

        /// <summary>
        /// Date and time at which the sale took place
        /// </summary>
        [JsonPropertyName("date")]
        public DateTime? PurchasedAt { get; set; }

        /// <summary>
        /// Total amount of the purchase
        /// </summary>
        [JsonPropertyName("totalAmount")]
        public decimal? TotalAmount { get; set; }

        /// <summary>
        /// Identifier of the Lidl store at which the purchase took place
        /// </summary>
        [JsonPropertyName("storeCode")]
        public string StoreCode { get; set; }

        /// <summary>
        /// The currency in which the sale took place
        /// </summary>
        [JsonPropertyName("currency")]
        public Currency Currency { get; set; }

        /// <summary>
        /// The amount of items purchased
        /// </summary>
        [JsonPropertyName("articlesCount")]
        public ushort? ArticlesCount { get; set; }

        /// <summary>
        /// The amount of coupons used in the purchased
        /// </summary>
        [JsonPropertyName("couponsUsedCount")]
        public ushort? CouponsUsedCount { get; set; }

        /// <summary>
        /// Boolean indicating if the purchase included returned items
        /// </summary>
        [JsonPropertyName("hasReturnedItems")]
        public bool? HasReturnedItems { get; set; }

        /// <summary>
        /// Total amount of the returned items in the purchase
        /// </summary>
        [JsonPropertyName("returnedAmount")]
        public decimal? ReturnedAmount { get; set; }

        /// <summary>
        /// Unique identifier of the invoice request
        /// </summary>
        [JsonPropertyName("invoiceRequestId")]
        public string InvoiceRequestId { get; set; }

        /// <summary>
        /// Unique identifier of the invoice
        /// </summary>
        [JsonPropertyName("invoiceId")]
        public string InvoiceId { get; set; }
    }
}
