using Microsoft.VisualStudio.TestTools.UnitTesting;
using KoenZomers.Lidl.Api;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using System.Configuration;

namespace UnitTest
{
    /// <summary>
    /// Unit Tests for authenticating the session object
    /// </summary>
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class AuthenticationUnitTest
    {
        /// <summary>
        /// Validate that the session is not authenticated if we don't authenticate it ourselves first
        /// </summary>
        [TestMethod]
        public void ByDefaultNotAuthenticatedTestMethod()
        {
            Assert.IsFalse(new Session().IsAuthenticated);
        }

        /// <summary>
        /// Validate that an exception gets thrown if we try to authenticate using an invalid e-mail address
        /// </summary>
        [TestMethod]
        public async Task InvalidEmailAddressAuthenticationAttemptTestMethod()
        {
            var session = new Session();
            await Assert.ThrowsExceptionAsync<KoenZomers.Lidl.Api.Exceptions.AccountNotFoundException>(async () => await session.Authenticate("doesnotexist@hotmail.com", "pass@word1"));
        }

        /// <summary>
        /// Validate that trying to authenticate without providing credentials fails
        /// </summary>
        [TestMethod]
        public async Task NoCredentialsAuthenticationAttemptTestMethod()
        {
            var session = new Session();
            await Assert.ThrowsExceptionAsync<KoenZomers.Lidl.Api.Exceptions.CredentialsInvalidException>(async () => await session.Authenticate(string.Empty, string.Empty));
        }

        /// <summary>
        /// Validate that logging in succeeds with valid credentials
        /// </summary>
        [TestMethod]
        public async Task SuccessfulAuthenticationAttemptUsingCredentialsTestMethod()
        {
            var emailAddress = ConfigurationManager.AppSettings["EmailAddress"];
            var password = ConfigurationManager.AppSettings["Password"];

            if (string.IsNullOrWhiteSpace(emailAddress) || string.IsNullOrWhiteSpace(password))
            {
                Assert.Inconclusive("Please configure credentials in the App.config file first");
            }
            System.Diagnostics.Trace.WriteLine("KoenTest: " + emailAddress.Remove(0, 1));
            var session = new Session();
            var success = await session.Authenticate(emailAddress, password);

            Assert.IsTrue(success);
        }

        /// <summary>
        /// Validate that logging in succeeds with a refresh token
        /// </summary>
        [TestMethod]
        public async Task SuccessfulAuthenticationAttemptUsingRefreshTokenTestMethod()
        {
            var refreshToken = ConfigurationManager.AppSettings["RefreshToken"];

            if (string.IsNullOrWhiteSpace(refreshToken))
            {
                Assert.Inconclusive("Please configure a refresh token in the App.config file first");
            }

            var session = new Session();
            var success = await session.Authenticate(refreshToken);

            Assert.IsTrue(success);
        }
    }
}
