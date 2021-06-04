using System;
using System.Configuration;
using System.Threading.Tasks;

namespace KoenZomers.Lidl.Api.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Retrieve the configuration from the App.config file
            var emailAddress = ConfigurationManager.AppSettings["EmailAddress"];
            var password = ConfigurationManager.AppSettings["Password"];
            var refreshToken = ConfigurationManager.AppSettings["RefreshToken"];

            // Set up a new session
            var session = new Session();

            // Check if we have a refresh token to authenticate with
            if (!string.IsNullOrWhiteSpace(refreshToken))
            {
                await session.Authenticate(refreshToken);
            }
            else
            {
                // Check if we have an email address and password to authenticate with
                if (!string.IsNullOrWhiteSpace(emailAddress) && !string.IsNullOrWhiteSpace(password))
                {
                    await session.Authenticate(emailAddress, password);
                }
                else
                {
                    // No credentials nor refresh token available, throw exception
                    throw new Exceptions.CredentialsInvalidException(emailAddress, password);
                }
            }
            //var test = await session.GetAppTranslations();
            //var test = await session.DoesEmailExist("johndoe@hotmail.com");
            //var test = await session.GetReceipts();
            //var test = await session.GetReceipt("12345");
            //var test = await session.GetStores();
            //var test = await session.GetAlerts();
            Console.WriteLine("Done!");
        }
    }
}
