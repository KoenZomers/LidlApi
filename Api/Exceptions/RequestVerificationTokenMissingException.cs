using System;

namespace KoenZomers.Lidl.Api.Exceptions
{
    /// <summary>
    /// Exception thrown when no request verification token has been found in the login page
    /// </summary>
    public class RequestVerificationTokenMissingException : Exception
    {
        /// <summary>
        /// The contents of the login page where the request verification token could not be located
        /// </summary>
        public string LoginPageContents { get; private set; }

        /// <summary>
        /// Throws a new exception
        /// </summary>
        /// <param name="loginPageContents">The contents of the login page where the request verification token could not be located</param>
        public RequestVerificationTokenMissingException(string loginPageContents) : base("Request verification token could not be found in the login page")
        {
            LoginPageContents = loginPageContents;
        }
    }
}
