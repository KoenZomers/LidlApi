using System;
using System.Text.Json.Serialization; 

namespace KoenZomers.Lidl.Api.Entities
{ 
    /// <summary>
    /// Details on one alert
    /// </summary>
    public class Alert
    {
        /// <summary>
        /// Unique identifier of the alert
        /// </summary>
        [JsonPropertyName("alertId")]
        public Guid? AlertId { get; set; }

        /// <summary>
        /// Section of the Lidl Plus app where the alert applies (i.e. Purchase)
        /// </summary>
        [JsonPropertyName("section")]
        public string Section { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("element")]
        public string Element { get; set; }

        /// <summary>
        /// Title of the alert
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; }

        /// <summary>
        /// Text of the alert
        /// </summary>
        [JsonPropertyName("text")]
        public string Text { get; set; }

        /// <summary>
        /// Date and time the alert has been raised
        /// </summary>
        [JsonPropertyName("date")]
        public DateTime? Date { get; set; }

        /// <summary>
        /// Boolean indicating if the alert has been read
        /// </summary>
        [JsonPropertyName("read")]
        public bool? IsRead { get; set; }
    }
}