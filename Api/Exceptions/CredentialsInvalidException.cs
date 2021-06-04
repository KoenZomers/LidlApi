using System;

namespace KoenZomers.Lidl.Api.Exceptions
{
    /// <summary>
    /// Exception thrown when trying to authenticate using invalid credentials
    /// </summary>
    public class CredentialsInvalidException : Exception
    {
        /// <summary>
        /// The contents of the login page where the request verification token could not be located
        /// </summary>
        public string EmailAddress { get; private set; }

        /// <summary>
        /// The contents of the login page where the request verification token could not be located
        /// </summary>
        public string Password { get; private set; }

        /// <summary>
        /// Throws a new exception
        /// </summary>
        /// <param name="emailAddress">E-mail address provided to use for authentication</param>
        /// <param name="password">Password provided to use for authentication</param>
        public CredentialsInvalidException(string emailAddress, string password) : base($"Credentials passed to use for authentication are invalid. E-mail address: '{emailAddress}'. Password: '{password}'.")
        {
            EmailAddress = emailAddress;
            Password = password;
        }
    }
}
