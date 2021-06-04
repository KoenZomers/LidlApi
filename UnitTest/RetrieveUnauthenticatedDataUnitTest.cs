using Microsoft.VisualStudio.TestTools.UnitTesting;
using KoenZomers.Lidl.Api;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace KoenZomers.Lidl.Api.UnitTest
{
    /// <summary>
    /// Unit Tests against the Lidl APIs which don't require an authenticated session
    /// </summary>
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class RetrieveUnauthenticatedDataUnitTest
    {
        /// <summary>
        /// Validate that the localized app translations can be retrieved in German by passing the language through the constructor
        /// </summary>
        [TestMethod]
        public async Task GetAppTranslationsTestMethod1()
        {
            Assert.IsTrue((await new Session(language: "DE-DE").GetAppTranslations()).Keys.Count > 0);
        }

        /// <summary>
        /// Validate that the localized app translations can be retrieved in Dutch by passing the language through the constructor
        /// </summary>
        [TestMethod]
        public async Task GetAppTranslationsTestMethod2()
        {
            Assert.IsTrue((await new Session(language: "NL-NL").GetAppTranslations()).Keys.Count > 0);
        }

        /// <summary>
        /// Validate that the localized app translations can be retrieved in English by passing the language to the method call
        /// </summary>
        [TestMethod]
        public async Task GetAppTranslationsTestMethod3()
        {
            Assert.IsTrue((await new Session().GetAppTranslations("EN-EN")).Keys.Count > 0);
        }
    }
}
