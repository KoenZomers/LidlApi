using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace KoenZomers.Lidl.Api.Entities
{
    /// <summary>
    /// Receipts data returned by the Lidl API
    /// </summary>
    public class Receipts
    {
        /// <summary>
        /// Number of the current receipts page
        /// </summary>
        [JsonPropertyName("page")]
        public ushort? PageNumber { get; set; }

        /// <summary>
        /// Number of available receipts pages
        /// </summary>
        [JsonPropertyName("size")]
        public ushort? TotalPages { get; set; }

        /// <summary>
        /// Number of available receipts
        /// </summary>
        [JsonPropertyName("totalCount")]
        public ushort? TotalCount { get; set; }

        /// <summary>
        /// The receipts
        /// </summary>
        [JsonPropertyName("records")]
        public List<Receipt> Receipt { get; set; } = new();
    }
}
