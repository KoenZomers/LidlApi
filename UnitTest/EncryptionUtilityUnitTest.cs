using Microsoft.VisualStudio.TestTools.UnitTesting;
using KoenZomers.Lidl.Api.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace KoenZomers.Lidl.Api.UnitTest
{
    /// <summary>
    /// Unit Tests against the encryption utility
    /// </summary>
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class EncryptionUtilityUnitTest
    {
        /// <summary>
        /// Validate that a generated PKCE Code Verifier meets the default length
        /// </summary>
        [TestMethod]
        public void GeneratePkceCodeVerifierTestMethod1()
        {
            Assert.IsTrue(Encryption.GeneratePkceCodeVerifier().Length == 43);
        }

        /// <summary>
        /// Validate that a generated PKCE Code Verifier meets the requested length
        /// </summary>
        [TestMethod]
        public void GeneratePkceCodeVerifierTestMethod2()
        {
            Assert.IsTrue(Encryption.GeneratePkceCodeVerifier(60).Length == 60);
        }

        /// <summary>
        /// Validate that a generated PKCE Code Verifier meets the requested length
        /// </summary>
        [TestMethod]
        public void GeneratePkceCodeVerifierTestMethod3()
        {
            Assert.IsTrue(Encryption.GeneratePkceCodeVerifier(128).Length == 128);
        }

        /// <summary>
        /// Validate the SHA256 PKCE code challenge for the provided code verifier
        /// </summary>
        [TestMethod]
        public void CalculatePkceS256CodeChallengeTestMethod1()
        {
            Assert.IsTrue(Encryption.CalculatePkceS256CodeChallenge("bpFA8lrdXby6KP3JKjKFMNFQxo8nSQ88f2DXDH99tWg") == "KXNmQNyYx6oHBZwULA0YvmzXojUwcKsIKx9TtXpYRJM");
        }

        /// <summary>
        /// Validate the SHA256 PKCE code challenge for the provided code verifier
        /// </summary>
        [TestMethod]
        public void CalculatePkceS256CodeChallengeTestMethod2()
        {
            Assert.IsTrue(Encryption.CalculatePkceS256CodeChallenge("dBjftJeZ4CVP-mB92K27uhbUJU1p1r_wW1gFWFOEjXk") == "E9Melhoa2OwvFrEMTJguCHaoeK1t8URWbuGJSstw-cM");
        }
    }
}
