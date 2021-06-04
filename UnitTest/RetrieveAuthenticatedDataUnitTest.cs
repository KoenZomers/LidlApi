using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using System.Configuration;

namespace KoenZomers.Lidl.Api.UnitTest
{
    /// <summary>
    /// Unit Tests against the Lidl APIs which require an authenticated session
    /// </summary>
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class RetrieveAuthenticatedDataUnitTest
    {
        /// <summary>
        /// Lidl Api session to use for the unit tests in this class
        /// </summary>
        private static Session _session;

        [ClassInitialize]
        public static async Task ClassInit(TestContext testContext)
        {
            var emailAddress = ConfigurationManager.AppSettings["EmailAddress"];
            var password = ConfigurationManager.AppSettings["Password"];
            var refreshToken = ConfigurationManager.AppSettings["RefreshToken"];

            if ((string.IsNullOrWhiteSpace(emailAddress) || string.IsNullOrWhiteSpace(password)) && string.IsNullOrWhiteSpace(refreshToken))
            {
                Assert.Inconclusive("Please configure credentials or a refresh token in the App.config file first");
            }

            _session = new Session();

            if(!string.IsNullOrWhiteSpace(refreshToken))
            {
                // Authenticate using refresh token
                Assert.IsTrue(await _session.Authenticate(refreshToken));
            }
            else
            {
                // Authenticate using email address and password
                Assert.IsTrue(await _session.Authenticate(emailAddress, password));
            }
        }

        /// <summary>
        /// Validate that the stores can be retrieved
        /// </summary>
        [TestMethod]
        public async Task GetStoresTestMethod()
        {
            Assert.IsTrue((await _session.GetStores()).Length > 0);
        }

        /// <summary>
        /// Validate that receipts can be retrieved
        /// </summary>
        [TestMethod]
        public async Task GetReceiptsTestMethod()
        {
            Assert.IsTrue((await _session.GetReceipts()).TotalCount > 0);
        }

        /// <summary>
        /// Validate that notification settings can be retrieved
        /// </summary>
        [TestMethod]
        public async Task GetNotificationSettingsTestMethod()
        {
            // If no exception occurs, we assume the call and therefore was successful
            await _session.GetNotificationSettings();
        }

        /// <summary>
        /// Validate that alerts can be retrieved
        /// </summary>
        [TestMethod]
        public async Task GetAlertsTestMethod()
        {
            Assert.IsTrue((await _session.GetAlerts()).Count > 0);
        }

        /// <summary>
        /// Validate that coupons can be retrieved
        /// </summary>
        [TestMethod]
        public async Task GetCouponsTestMethod()
        {
            Assert.IsTrue((await _session.GetCoupons()).Count > 0);
        }

        /// <summary>
        /// Validate that coupon plus data can be retrieved
        /// </summary>
        [TestMethod]
        public async Task GetCouponPlusTestMethod()
        {
            // If no exception occurs, we assume the call and therefore was successful
            await _session.GetLidlPlusDeal();
        }

        /// <summary>
        /// Validate that scratch coupon data can be retrieved
        /// </summary>
        [TestMethod]
        public async Task GetScratchCouponsTestMethod()
        {
            // If no exception occurs, we assume the call and therefore was successful
            await _session.GetScratchCoupons();
        }
    }
}
