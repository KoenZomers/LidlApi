using System;
using System.Text.Json.Serialization;

namespace KoenZomers.Lidl.Api.Entities
{
    /// <summary>
    /// Response from the authentication process containing the JWT tokens to access the API
    /// </summary>
    public class JwtLoginToken
    {
        /// <summary>
        /// The access token
        /// </summary>
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        /// <summary>
        /// The refresh token
        /// </summary>
        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }

        /// <summary>
        /// The ID token
        /// </summary>
        [JsonPropertyName("id_token")]
        public string IdToken { get; set; }

        /// <summary>
        /// Seconds after which this token will expire after having been given out
        /// </summary>
        [JsonPropertyName("expires_in")]
        public int? ExpiresIn { get; set; }

        /// <summary>
        /// Date and time at which this token was given out and starts to be valid
        /// </summary>
        public readonly DateTime NotBefore = DateTime.Now;

        /// <summary>
        /// Date and time at which this token will no longer be valid
        /// </summary>
        public DateTime? ExpiresAt => ExpiresIn.HasValue ? NotBefore.AddSeconds(ExpiresIn.Value) : null;

        /// <summary>
        /// All the scopes as a string which are valid for this token
        /// </summary>
        [JsonPropertyName("scope")]
        public string Scope { get; set; }

        /// <summary>
        /// Array with all the scopes which are valid for this token
        /// </summary>
        public string[] Scopes => Scope.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        /// <summary>
        /// The type of JWT token
        /// </summary>
        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }
    }
}
