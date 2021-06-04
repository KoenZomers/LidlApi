using System;

namespace KoenZomers.Lidl.Api.Exceptions
{
    /// <summary>
    /// Exception thrown when trying to log in using an e-mail address that is not known in the Lidl App
    /// </summary>
    public class AccountNotFoundException : Exception
    {
        /// <summary>
        /// The e-mail address with which logging in was attempted
        /// </summary>
        public string EmailAddress { get; private set; }

        /// <summary>
        /// Throws a new exception
        /// </summary>
        /// <param name="emailAddress">The e-mail address with which logging in was attempted</param>
        public AccountNotFoundException(string emailAddress) : base($"E-mail address {emailAddress} is not a known user")
        {
            EmailAddress = emailAddress;
        }
    }
}
