using System;

namespace KoenZomers.Lidl.Api.Exceptions
{
    /// <summary>
    /// Exception thrown when trying to call a Lidl API that requires the session to be authenticated first
    /// </summary>
    public class NotAuthenticatedException : Exception
    {
        /// <summary>
        /// Throws a new exception
        /// </summary>
        public NotAuthenticatedException() : base($"Session must be authenticated first before calling into this API")
        {
        }
    }
}
