using System.Text.Json.Serialization; 

namespace KoenZomers.Lidl.Api.Entities
{ 
    /// <summary>
    /// Details on the notifications configuration
    /// </summary>
    public class NotificationSettings
    {
        [JsonPropertyName("mail")]
        public bool Mail { get; set; }

        [JsonPropertyName("sms")]
        public bool Sms { get; set; }

        [JsonPropertyName("postal")]
        public bool Postal { get; set; }

        [JsonPropertyName("push")]
        public bool Push { get; set; }

        [JsonPropertyName("code")]
        public string Code { get; set; }
    }
}