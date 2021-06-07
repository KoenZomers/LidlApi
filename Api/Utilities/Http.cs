using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace KoenZomers.Lidl.Api.Utilities
{
    /// <summary>
    /// Internal utility class for Http communication with the UniFi Controller
    /// </summary>
    internal static class Http
    {
        /// <summary>
        /// Disables SSL Validation in case of self signed SSL certificates being used
        /// </summary>
        public static void DisableSslValidation()
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
        }

        /// <summary>
        /// Enables connecting to a remote server hosting UniFi using a TLS 1.1 or TLS 1.2 certificate
        /// </summary>
        public static void EnableTls11and12()
        {
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
        }

        /// <summary>
        /// Performs a GET request to the provided url to download the page contents
        /// </summary>
        /// <param name="url">Url of the page to retrieve</param>
        /// <param name="cookieContainer">Cookies which have been recorded for this session</param>
        /// <param name="headerFields">Additional headers to add for the request</param>
        /// <param name="timeout">Timeout in milliseconds on how long the request may take. Default = 60000 = 60 seconds.</param>
        /// <returns>Contents of the page</returns>
        public async static Task<string> GetRequestResult(Uri url, CookieContainer cookieContainer = null, Dictionary<string, string> headerFields = null, int timeout = 60000)
        {
            return await RequestResult(url, WebRequestMethods.Http.Get, cookieContainer, headerFields, timeout);
        }

        /// <summary>
        /// Performs a POST request to the provided url to download the page contents
        /// </summary>
        /// <param name="url">Url of the page to retrieve</param>
        /// <param name="cookieContainer">Cookies which have been recorded for this session</param>
        /// <param name="headerFields">Additional headers to add for the request</param>
        /// <param name="timeout">Timeout in milliseconds on how long the request may take. Default = 60000 = 60 seconds.</param>
        /// <returns>Contents of the page</returns>
        public async static Task<string> PostRequestResult(Uri url, CookieContainer cookieContainer = null, Dictionary<string, string> headerFields = null, int timeout = 60000)
        {
            return await RequestResult(url, WebRequestMethods.Http.Post, cookieContainer, headerFields, timeout);
        }

        /// <summary>
        /// Performs a request to the provided url to download the page contents
        /// </summary>
        /// <param name="url">Url of the page to retrieve</param>
        /// <param name="httpMethod">HTTP Method to use for the request, i.e. GET or POST</param>
        /// <param name="cookieContainer">Cookies which have been recorded for this session</param>
        /// <param name="headerFields">Additional headers to add for the request</param>
        /// <param name="timeout">Timeout in milliseconds on how long the request may take. Default = 60000 = 60 seconds.</param>
        /// <returns>Contents of the page</returns>
        public async static Task<string> RequestResult(Uri url, string httpMethod, CookieContainer cookieContainer = null, Dictionary<string, string> headerFields = null, int timeout = 60000)
        {
            // Construct the request
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.CookieContainer = cookieContainer;
            request.Timeout = timeout;
            request.KeepAlive = false;
            request.Method = httpMethod;

            if (headerFields != null)
            {
                // Construct header data
                foreach (var headerField in headerFields)
                {
                    request.Headers[headerField.Key] = headerField.Value;
                }
            }

            // Send the request to the webserver
            using var response = await request.GetResponseAsync();
            
            // Get the stream containing content returned by the server.
            using var dataStream = response.GetResponseStream();
            
            if (dataStream == null) return null;

            using var reader = new StreamReader(dataStream);
            
            // Read the content returned
            var responseFromServer = await reader.ReadToEndAsync();
            return responseFromServer;
        }

        /// <summary>
        /// Performs a GET request to the provided url
        /// </summary>
        /// <param name="url">Url of the page to retrieve</param>
        /// <param name="httpRequestMethod">The HTTP request method to use, i.e. GET/POST/PUT</param>
        /// <param name="cookieContainer">Cookies which have been recorded for this session</param>
        /// <param name="timeout">Timeout in milliseconds on how long the request may take. Default = 60000 = 60 seconds.</param>
        /// <returns>WebRequest response</returns>
        public async static Task<HttpWebResponse> RequestWebResponse(Uri url, CookieContainer cookieContainer = null, string httpRequestMethod = "GET", int timeout = 60000)
        {
            // Construct the request
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.CookieContainer = cookieContainer;
            request.Timeout = timeout;
            request.KeepAlive = false;
            request.AllowAutoRedirect = false;
            request.Method = httpRequestMethod;

            var response = await request.GetResponseAsync() as HttpWebResponse;
            return response;
        }

        /// <summary>
        /// Sends a POST request towards UniFi
        /// </summary>
        /// <param name="url">Url to POST the postData to</param>
        /// <param name="postData">Data to send to the UniFi service, typically a JSON payload</param>
        /// <param name="cookieContainer">Cookies which have been recorded for this session</param>
        /// <param name="timeout">Timeout in milliseconds on how long the request may take. Default = 60000 = 60 seconds.</param>
        /// <returns>The website contents returned by the webserver after posting the data</returns>
        public async static Task<string> PostRequest(Uri url, string postData, CookieContainer cookieContainer, int timeout = 60000)
        {
            // Construct the POST request
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.Accept = "application/json, text/plain, */*";
            request.ContentType = "application/json;charset=UTF-8";
            request.CookieContainer = cookieContainer;
            request.Timeout = timeout;
            request.KeepAlive = false;

            // Check if the have a Cross Site Request Forgery cookie and if so, add it as the X-Csrf-Token header which is required by UniFi when making a POST
            var csrfCookie = cookieContainer.GetAllCookies().FirstOrDefault(c => c.Name == "csrf_token");
            if(csrfCookie != null)
            {
                request.Headers.Add("X-Csrf-Token", csrfCookie.Value);
            }

            // Convert the POST data to a byte array
            var postDataByteArray = Encoding.UTF8.GetBytes(postData);

            // Set the ContentLength property of the WebRequest.
            request.ContentLength = postDataByteArray.Length;

            // Get the request stream
            using (var postDataStream = await request.GetRequestStreamAsync())
            {
                // Write the POST data to the request stream
                await postDataStream.WriteAsync(postDataByteArray, 0, postDataByteArray.Length);

                // Close the Stream object
                postDataStream.Close();
            }

            // Receive the response from the webserver
            using var response = await request.GetResponseAsync() as HttpWebResponse;
            
            // Make sure the webserver has sent a response
            if (response == null) return null;

            using var requestDataStream = response.GetResponseStream();
            
            // Make sure the datastream with the response is available
            if (requestDataStream == null) return null;

            using var reader = new StreamReader(requestDataStream);
            return await reader.ReadToEndAsync();
        }

        /// <summary>
        /// Sends a POST request using the url encoded form method to authenticate
        /// </summary>
        /// <param name="url">Url to POST the login information to</param>
        /// <param name="formFields">Dictonary with key/value pairs containing the forms data to POST to the webserver</param>
        /// <param name="cookieContainer">Cookies which have been recorded for this session</param>
        /// <param name="timeout">Timeout in milliseconds on how long the request may take. Default = 60000 = 60 seconds.</param>
        /// <returns>The website contents returned by the webserver after posting the data</returns>
        public async static Task<HttpWebResponse> AuthenticateViaUrlEncodedFormMethod(Uri url, Dictionary<string, string> headerFields, Dictionary<string, string> formFields, CookieContainer cookieContainer, int timeout = 60000)
        {
            // Construct the POST request which performs the login
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.Accept = "*/*";
            request.ServicePoint.Expect100Continue = false;
            request.CookieContainer = cookieContainer;
            request.Timeout = timeout;
            request.AllowAutoRedirect = false;

            // Construct header data
            foreach (var headerField in headerFields)
            {
                request.Headers[headerField.Key] = headerField.Value;
            }

            // Construct POST data
            var postData = new StringBuilder();
            foreach (var formField in formFields)
            {
                if (postData.Length > 0) postData.Append('&');
                postData.Append(formField.Key);
                postData.Append('=');
                postData.Append(formField.Value);
            }

            // Convert the POST data to a byte array
            var postDataByteArray = Encoding.UTF8.GetBytes(postData.ToString());

            // Set the ContentType property of the WebRequest
            request.ContentType = "application/x-www-form-urlencoded";

            // Set the ContentLength property of the WebRequest.
            request.ContentLength = postDataByteArray.Length;

            // Get the request stream
            var dataStream = await request.GetRequestStreamAsync();

            // Write the POST data to the request stream
            dataStream.Write(postDataByteArray, 0, postDataByteArray.Length);

            // Close the Stream object
            dataStream.Close();

            // Receive the response from the webserver
            var response = await request.GetResponseAsync() as HttpWebResponse;
            return response;
        }

        /// <summary>
        /// Sends a POST request with JSON variables to authenticate against UniFi
        /// </summary>
        /// <param name="url">Url to POST the login information to</param>
        /// <param name="username">Username to authenticate with</param>
        /// <param name="password">Password to authenticate with</param>
        /// <param name="cookieContainer">Cookies which have been recorded for this session</param>
        /// <param name="timeout">Timeout in milliseconds on how long the request may take. Default = 60000 = 60 seconds.</param>
        /// <returns>The website contents returned by the webserver after posting the data</returns>
        public async static Task<string> AuthenticateViaJsonPostMethod(Uri url, string username, string password, CookieContainer cookieContainer, int timeout = 60000)
        {
            // Construct the POST request which performs the login
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.Accept = "application/json, text/plain, */*";
            request.ContentType = "application/json;charset=UTF-8";
            request.ServicePoint.Expect100Continue = false;
            request.CookieContainer = cookieContainer;
            request.Timeout = timeout;
            request.KeepAlive = false;

            // Construct POST data
            var postData = string.Concat(@"{""username"":""", username, @""",""password"":""", password, @""",""remember"":false,""strict"":true}");

            // Convert the POST data to a byte array
            var postDataByteArray = Encoding.UTF8.GetBytes(postData);

            // Set the ContentLength property of the WebRequest.
            request.ContentLength = postDataByteArray.Length;

            // Get the request stream
            using (var postDataStream = await request.GetRequestStreamAsync())
            {
                // Write the POST data to the request stream
                await postDataStream.WriteAsync(postDataByteArray, 0, postDataByteArray.Length);

                // Close the Stream object
                postDataStream.Close();
            }

            try
            {
                // Request must be kept alive because in case of an error (i.e. wrong credentials) the response otherwise can't be read anymore
                request.KeepAlive = true;

                // Receive the response from the webserver
                using var response = await request.GetResponseAsync() as HttpWebResponse;

                // Make sure the webserver has sent a response
                if (response == null) return null;

                using var requestDataStream = response.GetResponseStream();

                // Make sure the datastream with the response is available
                if (requestDataStream == null) return null;

                using var reader = new StreamReader(requestDataStream);
                return await reader.ReadToEndAsync();
            }
            catch (WebException e)
            {
                // A protocolerror typically indicates that the credentials were wrong so we can handle that. Other types could be anything so we rethrow it to the caller to deal with.
                if (e.Status != WebExceptionStatus.ProtocolError)
                {
                    throw;
                }

                // Parse the response from the server
                using var response = (HttpWebResponse)e.Response;
                using var stream = response.GetResponseStream();
                using var reader = new StreamReader(stream, Encoding.GetEncoding("utf-8"));
                return reader.ReadToEnd();
            }
        }

        /// <summary>
        /// Sends a POST request to log out from the UniFi Controller
        /// </summary>
        /// <param name="url">Url to POST the logout request to</param>
        /// <param name="cookieContainer">Cookies which have been recorded for this session</param>
        /// <param name="timeout">Timeout in milliseconds on how long the request may take. Default = 60000 = 60 seconds.</param>
        /// <returns>The website contents returned by the webserver after posting the data</returns>
        public async static Task<string> LogoutViaJsonPostMethod(Uri url, CookieContainer cookieContainer, int timeout = 60000)
        {
            // Construct the POST request which performs the login
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.Accept = "application/json, text/plain, */*";
            request.ServicePoint.Expect100Continue = false;
            request.CookieContainer = cookieContainer;
            request.Timeout = timeout;
            request.KeepAlive = false;

            // Construct POST data
            var postData = "{}";

            // Convert the POST data to a byte array
            var postDataByteArray = Encoding.UTF8.GetBytes(postData);

            // Set the ContentType property of the WebRequest
            request.ContentType = "application/json;charset=UTF-8";

            // Set the ContentLength property of the WebRequest.
            request.ContentLength = postDataByteArray.Length;

            // Get the request stream
            using (var postDataStream = await request.GetRequestStreamAsync())
            {
                // Write the POST data to the request stream
                await postDataStream.WriteAsync(postDataByteArray, 0, postDataByteArray.Length);

                // Close the Stream object
                postDataStream.Close();
            }

            // Receive the response from the webserver
            using var response = await request.GetResponseAsync() as HttpWebResponse;

            // Make sure the webserver has sent a response
            if (response == null) return null;

            using var requestDataStream = response.GetResponseStream();

            // Make sure the datastream with the response is available
            if (requestDataStream == null) return null;

            using var reader = new StreamReader(requestDataStream);
            return await reader.ReadToEndAsync();
        }
    }
}
