using System.Text.Json.Serialization;

namespace KoenZomers.Lidl.Api.Entities
{
    /// <summary>
    /// Response from the webservice on the request to check if an e-mail address exists
    /// </summary>
    public class EmailExistsCheckResult
    {
        /// <summary>
        /// Boolean indicating if the e-mail address exists
        /// </summary>
        [JsonPropertyName("exists")]
        public bool? Exists { get; set; }

        /// <summary>
        /// Boolean indicating if the request was successful
        /// </summary>
        [JsonPropertyName("isSuccess")]
        public bool? IsSuccess { get; set; }
    }
}
