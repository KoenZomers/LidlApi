using System;
using System.Security.Cryptography;

namespace KoenZomers.Lidl.Api.Utilities
{
    /// <summary>
    /// Utilities for encrypion
    /// </summary>
    public static class Encryption
    {
        /// <summary>
        /// Generates a PKCE Code Verifier
        /// </summary>
        /// <param name="length">Length of the code verifier challenge (should be at least 43 and at most 128)</param>
        /// <seealso cref="https://www.authlete.com/developers/pkce/"/>
        /// <returns>PKCE Code Verifier</returns>
        public static string GeneratePkceCodeVerifier(short length = 43)
        {
            using var randomNumberGenerator = RandomNumberGenerator.Create();
            var codeverifierBytes = new byte[length];
            randomNumberGenerator.GetBytes(codeverifierBytes);

            var codeVerifier = Convert.ToBase64String(codeverifierBytes).Split('=')[0].Replace('+', '-').Replace('/', '_').Remove(length);
            return codeVerifier;
        }

        /// <summary>
        /// Calculates the SHA256 PKCE code challenge for the provided code verifier
        /// </summary>
        /// <param name="codeverifier">Code verifier to encode</param>
        /// <seealso cref="https://www.authlete.com/developers/pkce/"/>
        /// <returns>SHA256 code challenge</returns>
        public static string CalculatePkceS256CodeChallenge(string codeverifier)
        {
            using var sha256 = SHA256.Create();

            var challengeBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(codeverifier));
            var challenge = Convert.ToBase64String(challengeBytes).Split('=')[0].Replace('+', '-').Replace('/', '_');

            return challenge;
        }
    }
}
