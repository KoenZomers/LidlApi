using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace KoenZomers.Lidl.Api.UnitTest
{
    /// <summary>
    /// Unit Tests against the session object by passing in constructor parameters
    /// </summary>
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class SessionCreatorUnitTest
    {
        /// <summary>
        /// Validate that the language is properly set on the session when passing it through the constructor
        /// </summary>
        [TestMethod]
        public void GetLanguageTestMethod()
        {
            Assert.IsTrue(new Session(language: "DE-DE").Language == "DE-DE");
        }

        /// <summary>
        /// Validate that the country is properly set on the session when passing it through the constructor
        /// </summary>
        [TestMethod]
        public void GetCountryTestMethod()
        {
            Assert.IsTrue(new Session(country: "NL").Country == "NL");
        }

        /// <summary>
        /// Validate that the clientId is properly set on the session when passing it through the constructor
        /// </summary>
        [TestMethod]
        public void GetClientIdTestMethod()
        {
            Assert.IsTrue(new Session(clientId: "test").ClientId == "test");
        }

        /// <summary>
        /// Validate that the userAgent is properly set on the session when passing it through the constructor
        /// </summary>
        [TestMethod]
        public void GetUserAgentTestMethod()
        {
            Assert.IsTrue(new Session(userAgent: "test").UserAgent == "test");
        }

        /// <summary>
        /// Validate that the redirectUri is properly set on the session when passing it through the constructor
        /// </summary>
        [TestMethod]
        public void GetRedirectUriTestMethod()
        {
            Assert.IsTrue(new Session(redirectUri: "test").RedirectUri == "test");
        }

        /// <summary>
        /// Validate that the scopes is properly set on the session when passing it through the constructor
        /// </summary>
        [TestMethod]
        public void GetScopesTestMethod()
        {
            Assert.IsTrue(new Session(scopes: new[] { "scope1", "scope2" }).Scopes.Length == 2);
        }

        /// <summary>
        /// Validate that the appPackageName is properly set on the session when passing it through the constructor
        /// </summary>
        [TestMethod]
        public void GetAppPackageNameTestMethod()
        {
            Assert.IsTrue(new Session(appPackageName: "test").AppPackageName == "test");
        }

        /// <summary>
        /// Validate that the appVersion is properly set on the session when passing it through the constructor
        /// </summary>
        [TestMethod]
        public void GetAppVersionTestMethod()
        {
            Assert.IsTrue(new Session(appVersion: "test").AppVersion == "test");
        }

        /// <summary>
        /// Validate that the operatingSystem is properly set on the session when passing it through the constructor
        /// </summary>
        [TestMethod]
        public void GetOperatingSystemTestMethod()
        {
            Assert.IsTrue(new Session(operatingSystem: "test").OperatingSystem == "test");
        }
    }
}
