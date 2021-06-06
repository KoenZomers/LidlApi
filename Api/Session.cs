using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KoenZomers.Lidl.Api
{
    /// <summary>
    /// A session towards the Lidl API to retrieve information from it
    /// </summary>
    public class Session
    {
        #region Fields

        /// <summary>
        /// Cookie container which holds all cookies for requests towards the Lidl Accounts website
        /// </summary>
        private CookieContainer _cookieContainer;

        /// <summary>
        /// The JWT token to use to authenticate against the Lidl API
        /// </summary>
        private Entities.JwtLoginToken JwtToken;

        /// <summary>
        /// Gets the base URI to the Lidl authentication webservice
        /// </summary>
        private readonly Uri BaseAuthEndpointUri = new("https://accounts.lidl.com");

        /// <summary>
        /// Gets the base URI to the Lidl data webservice
        /// </summary>
        private readonly Uri BaseDataEndpointUri = new("https://appgateway.lidlplus.com/app/v23/");

        /// <summary>
        /// Gets the country localized base URI to the Lidl data webservice
        /// </summary>
        private Uri BaseDataEndpointLocalizedUri => new(BaseDataEndpointUri, $"{Country}/");

        /// <summary>
        /// The default scopes for which a token will be requested
        /// </summary>
        private readonly string[] DefaultScopes = new string[] { "openid", "profile", "offline_access", "lpprofile", "lpapis" };

        #endregion

        #region Properties

        /// <summary>
        /// The language for the content, formatted like NL-NL for Dutch. This can be set through the constructor.
        /// </summary>
        public string Language { get; private set; }

        /// <summary>
        /// The country to retrieve data for, formatted like NL for The Netherlands. This can be set through the constructor.
        /// </summary>
        public string Country { get; private set; }

        /// <summary>
        /// Client Id to provide to the Lidl Api to authenticate. This can be set through the constructor.
        /// </summary>
        public string ClientId { get; private set; }

        /// <summary>
        /// Redirect Uri to provide to the Lidl Api to navigate to after successfully authenticating. This can be set through the constructor.
        /// </summary>
        public string RedirectUri { get; private set; }

        /// <summary>
        /// User agent to send to the Lidl Api
        /// </summary>
        public string UserAgent { get; private set; }

        /// <summary>
        /// Operating system identifier to send to the Lidl Api
        /// </summary>
        public string OperatingSystem { get; private set; }

        /// <summary>
        /// App version identifier to send to the Lidl Api
        /// </summary>
        public string AppVersion { get; private set; }

        /// <summary>
        /// App package name to send to the Lidl Api
        /// </summary>
        public string AppPackageName { get; private set; }

        /// <summary>
        /// Scopes to request access to for the Lidl Api
        /// </summary>
        public string[] Scopes { get; private set; }

        /// <summary>
        /// Timeout to use on the HTTP requests in milliseconds
        /// </summary>
        public int RequestTimeOut { get; private set; }

        /// <summary>
        /// Returns a boolean indicating if the current session is authenticated
        /// </summary>
        public bool IsAuthenticated => JwtToken != null && !string.IsNullOrWhiteSpace(JwtToken.AccessToken) && (JwtToken.ExpiresAt.HasValue && JwtToken.ExpiresAt.Value > DateTime.Now);

        #endregion

        #region Constructor

        /// <summary>
        /// Instantiates a new session to communicate with the Lidl Api
        /// </summary>
        /// <param name="language">Language to retrieve the content in, i.e. NL-NL for Dutch</param>
        /// <param name="country">Country to retrieve the data for, i.e. NL for The Netherlands</param>
        /// <param name="clientId">Client Id to provide to the Lidl Api to authenticate</param>
        /// <param name="redirectUri">Uri to tell the Lidl Api to redirect to after successfully authenticating</param>
        /// <param name="userAgent">User agent to send to the Lidl Api</param>
        /// <param name="operatingSystem">The operating system identifier to send to the Lidl API</param>
        /// <param name="appPackageName">Internal app package name to send to the Lidl API</param>
        /// <param name="appVersion">App version number to send to the Lidl API</param>
        /// <param name="requestTimeOut">Timeout to use on the HTTP requests</param>
        /// <param name="scopes">The scopes to request access to</param>
        public Session(string language = "NL-NL", 
                       string country = "NL", 
                       string clientId = "LidlPlusNativeClient", 
                       string userAgent = "Mozilla/5.0 (Linux; Android 9; SM-G950F) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/77.0.3865.116 Mobile Safari/537.36 EdgA/46.04.4.5157", 
                       string operatingSystem = "Android",
                       string appVersion = "14.37.2",
                       string appPackageName = "com.lidl.eci.lidl.plus",
                       string redirectUri = "com.lidlplus.app://callback", 
                       string[] scopes = null, 
                       int requestTimeOut = 6000)
        {
            Language = language;
            Country = country;
            ClientId = clientId;
            RedirectUri = redirectUri;
            Scopes = scopes ?? DefaultScopes;
            UserAgent = userAgent;
            AppVersion = appVersion;
            AppPackageName = appPackageName;
            OperatingSystem = operatingSystem;
            RequestTimeOut = requestTimeOut;

            _cookieContainer = new();
        }

        #endregion

        #region Authentication calls

        /// <summary>
        /// Authenticate against the Lidl Api with the provided credentials
        /// </summary>
        /// <param name="emailAddress">E-mail address to authenticate with</param>
        /// <param name="password">Password to authenticate with</param>
        /// <exception cref="Exceptions.CredentialsInvalidException">Thrown if invalid credentials have been provided through <paramref name="emailAddress"/> and/or <paramref name="password"/></exception>
        /// <exception cref="Exceptions.AccountNotFoundException">Thrown if no account exists witht the provided <paramref name="emailAddress"/></exception>
        /// <returns>Boolean indicating whether the authentication was successful (True) or failed (False)</returns>
        public async Task<bool> Authenticate(string emailAddress, string password)
        {
            if(string.IsNullOrWhiteSpace(emailAddress) || string.IsNullOrWhiteSpace(password))
            {
                throw new Exceptions.CredentialsInvalidException(emailAddress, password);
            }

            // Create a new cookie container to contain the authentication cookie
            _cookieContainer = new CookieContainer();

            // Generate the PKCE code verifier and code challenge
            var codeVerifier = Utilities.Encryption.GeneratePkceCodeVerifier();
            var codeChallenge = Utilities.Encryption.CalculatePkceS256CodeChallenge(codeVerifier);

            // Request the login page
            var authUri = new Uri(BaseAuthEndpointUri, $"/connect/authorize?client_id={ClientId}&scope={System.Web.HttpUtility.UrlEncode(string.Join<string>(' ', Scopes))}&response_type=code&redirect_uri={RedirectUri}&code_challenge={codeChallenge}&code_challenge_method=S256&Country={Country}&language={Language}");

            var loginPageContents = await Utilities.Http.GetRequestResult(authUri, _cookieContainer, timeout: RequestTimeOut);

            // Use a regular expression to fetch the anti cross site scriping token from the HTML
            var xssToken = Regex.Match(loginPageContents, "<input.+?name=['\"]_+?RequestVerificationToken['\"].+?type=['\"]hidden['\"].+?value=['\"](?<xsstoken>.*?)['\"].+?/>", RegexOptions.IgnoreCase);

            // Verify that the anti XSS token was found
            if (!xssToken.Success || !xssToken.Groups["xsstoken"].Success)
            {
                throw new Exceptions.RequestVerificationTokenMissingException(loginPageContents);
            }

            // Ensure the provided e-mail address exists
            var emailExists = await DoesEmailExist(emailAddress);
            if(emailExists.Exists.HasValue && !emailExists.Exists.Value)
            {
                throw new Exceptions.AccountNotFoundException(emailAddress);
            }

            // Pass in the credentials to authenticate
            using var loginResponse1 = await Utilities.Http.AuthenticateViaUrlEncodedFormMethod(new Uri(BaseAuthEndpointUri, $"/Account/Login?ReturnUrl=%2Fconnect%2Fauthorize%2Fcallback%3Fclient_id%3D{ClientId}%26scope%3D{System.Web.HttpUtility.UrlEncode(string.Join<string>(' ', Scopes))}%26response_type%3Dcode%26redirect_uri%3D{System.Web.HttpUtility.UrlEncode(RedirectUri)}%26code_challenge%3D{codeChallenge}%26code_challenge_method%3DS256%26Country%3D{Country}%26language%3D{Language}"),
                                                                                               new(),
                                                                                               new()
                                                                                               {
                                                                                                   { "Email", System.Web.HttpUtility.UrlEncode(emailAddress) },
                                                                                                   { "PhoneNumber", "" },
                                                                                                   { "EmailOrPhone", System.Web.HttpUtility.UrlEncode(emailAddress) },
                                                                                                   { "Password", System.Web.HttpUtility.UrlEncode(password) },
                                                                                                   { "ReCaptchaToken", "" },
                                                                                                   { "DeviceId", "" },
                                                                                                   { "__RequestVerificationToken", xssToken.Groups["xsstoken"].Value }
                                                                                               },
                                                                                               _cookieContainer,
                                                                                               RequestTimeOut);
            // Follow the HTTP 302 redirect to the provided URL
            using var loginResponse2 = await Utilities.Http.RequestWebResponse(new Uri(BaseAuthEndpointUri, loginResponse1.Headers["Location"]), httpRequestMethod: "GET", cookieContainer: _cookieContainer, timeout: RequestTimeOut);

            // Use a regular expression to fetch the authentication code from the redirect location header
            var authenticationCodeRegEx = Regex.Match(loginResponse2.Headers["Location"], "code=(?<code>[A-Z0-9]*)", RegexOptions.IgnoreCase);

            // Validate that the authentication code has successfully been retrieved
            if (!authenticationCodeRegEx.Success || !authenticationCodeRegEx.Groups["code"].Success)
            {

            }

            var authenticationCode = authenticationCodeRegEx.Groups["code"].Value;

            // Using the callback code received in the HTTP 302 redirect, request an access and refresh token
            using var tokenResponse = await Utilities.Http.AuthenticateViaUrlEncodedFormMethod(new Uri(BaseAuthEndpointUri, $"/connect/token"),
                                                                                           new()
                                                                                           {
                                                                                               { "Authorization", "Basic TGlkbFBsdXNOYXRpdmVDbGllbnQ6c2VjcmV0" },
                                                                                               { "Accept", "application/json" },
                                                                                           },
                                                                                           new()
                                                                                           {
                                                                                               { "grant_type", "authorization_code" },
                                                                                               { "code", authenticationCode },
                                                                                               { "redirect_uri", System.Web.HttpUtility.UrlEncode(RedirectUri) },
                                                                                               { "code_verifier", codeVerifier }
                                                                                           },
                                                                                           _cookieContainer,
                                                                                           RequestTimeOut);
            
            // Parse the JWT token from the body of the response
            JwtToken = await JsonSerializer.DeserializeAsync<Entities.JwtLoginToken>(tokenResponse.GetResponseStream());

            return !string.IsNullOrWhiteSpace(JwtToken.AccessToken);
        }

        /// <summary>
        /// Authenticate against the Lidl Api using a refresh token
        /// </summary>
        /// <param name="refreshToken">Refresh token to use to retrieve an access token</param>
        /// <returns>Boolean indicating whether the authentication was successful (True) or failed (False)</returns>
        public async Task<bool> Authenticate(string refreshToken)
        {
            // Using the callback code received in the HTTP 302 redirect, request an access and refresh token
            using var tokenResponse = await Utilities.Http.AuthenticateViaUrlEncodedFormMethod(new Uri(BaseAuthEndpointUri, $"/connect/token"),
                                                                                           new()
                                                                                           {
                                                                                               { "Authorization", "Basic TGlkbFBsdXNOYXRpdmVDbGllbnQ6c2VjcmV0" },
                                                                                               { "Accept", "application/json" },
                                                                                           },
                                                                                           new()
                                                                                           {
                                                                                               { "refresh_token", refreshToken },
                                                                                               { "grant_type", "refresh_token" }
                                                                                           },
                                                                                           null,
                                                                                           RequestTimeOut);

            // Parse the JWT token from the body of the response
            JwtToken = await JsonSerializer.DeserializeAsync<Entities.JwtLoginToken>(tokenResponse.GetResponseStream());

            return !string.IsNullOrWhiteSpace(JwtToken.AccessToken);
        }

        #endregion

        #region Authenticated API Calls

        /// <summary>
        /// Returns the available receipts
        /// </summary>
        /// <param name="pageNumber">Page number to retrieve the receipts from</param>
        /// <returns><see cref="Entities.Receipts"/> instance containing all the requested receipts</returns>
        public async Task<Entities.Receipts> GetReceipts(short pageNumber = 1)
        {
            var receipts = await GetLidlDataRequestResultAsEntity<Entities.Receipts>($"tickets/list/{pageNumber}");
            return receipts;
        }

        /// <summary>
        /// Returns details of a specific receipt
        /// </summary>
        /// <param name="receiptId">Id of the receipt to return the details for</param>
        /// <returns><see cref="Entities.ReceiptDetail"/> instance containing the requested receipt details</returns>
        public async Task<Entities.ReceiptDetail> GetReceipt(string receiptId)
        {
            var receipts = await GetLidlDataRequestResultAsEntity<Entities.ReceiptDetail>($"tickets/{receiptId}");
            return receipts;
        }

        /// <summary>
        /// Returns notification settings
        /// </summary>
        /// <returns><see cref="Entities.NotificationSettings"/> instance containing the notification settings</returns>
        public async Task<Entities.NotificationSettings> GetNotificationSettings()
        {
            var settings = await GetLidlDataRequestResultAsEntity<Entities.NotificationSettings>("contacts/notifications");
            return settings;
        }

        /// <summary>
        /// Returns alerts
        /// </summary>
        /// <returns><see cref="IList{T}"/> with <see cref="Entities.Alert"/> entities containing all the alerts</returns>
        public async Task<IList<Entities.Alert>> GetAlerts()
        {
            var alerts = await GetLidlDataRequestResultAsEntity<IList<Entities.Alert>>("alerts");
            return alerts;
        }

        /// <summary>
        /// Returns alerts regarding a specific store
        /// </summary>
        /// <returns><see cref="IList{T}"/> with <see cref="Entities.Alert"/> entities containing all the alerts of the provided store</returns>
        public async Task<IList<Entities.Alert>> GetAlertsByStore(string storeId)
        {
            var alerts = await PostLidlDataRequestResultAsEntity<IList<Entities.Alert>>($"alerts/geofencing/{storeId}");
            return alerts;
        }

        /// <summary>
        /// Returns coupons
        /// </summary>
        /// <returns><see cref="IList{T}"/> with <see cref="Entities.Coupon"/> entities containing all the coupons</returns>
        public async Task<IList<Entities.Coupon>> GetCoupons()
        {
            var coupons = await GetLidlDataRequestResultAsEntity<IList<Entities.Coupon>>("coupons");
            return coupons;
        }

        /// <summary>
        /// Returns the current Coupon Plus deal which is typically a monthly contest where depending on how much money you spend at Lidl during that month, you can get free items
        /// </summary>
        /// <returns><see cref="Entities.CouponPlus"/> instance containing all the details on the current Lidl Plus Coupon deal</returns>
        public async Task<Entities.CouponPlus> GetLidlPlusDeal()
        {
            var couponplus = await GetLidlDataRequestResultAsEntity<Entities.CouponPlus>("couponplus");
            return couponplus;
        }

        /// <summary>
        /// Returns scratch coupons which are rewarded after each purchase at Lidl
        /// </summary>
        /// <returns><see cref="IList{T}"/> with <see cref="Entities.ScratchCoupon"/> entities containing all the available scratch coupons</returns>
        public async Task<IList<Entities.ScratchCoupon>> GetScratchCoupons()
        {
            var scratchcoupons = await GetLidlDataRequestResultAsEntity<IList<Entities.ScratchCoupon>>("scratchcoupons");
            return scratchcoupons;
        }

        /// <summary>
        /// Redeems/scratches open a specific scratch coupon
        /// </summary>
        /// <returns>Boolean indicating if the action was succesful</returns>
        public async Task<bool> RedeemScratchCoupon(string scratchCouponId)
        {
            using var sratchCouponResponse = await Utilities.Http.RequestWebResponse(new Uri(BaseDataEndpointLocalizedUri, $"scratchcoupons/{scratchCouponId}/redeem"), httpRequestMethod: "POST", timeout: RequestTimeOut);
            return sratchCouponResponse.StatusCode == HttpStatusCode.NoContent;
        }

        /// <summary>
        /// Marks a receipt as favorite
        /// </summary>
        /// <param name="receiptId">Id of the receipt to make favorite</param>
        /// <returns>Boolean indicating if the action was succesful</returns>
        public async Task<bool> MakeReceiptFavorite(string receiptId)
        {
            using var favorReceiptResponse = await Utilities.Http.RequestWebResponse(new Uri(BaseDataEndpointLocalizedUri, $"tickets/{receiptId}/favorite"), httpRequestMethod: "POST", timeout: RequestTimeOut);
            return favorReceiptResponse.StatusCode == HttpStatusCode.OK;
        }

        /// <summary>
        /// Returns all the store identifiers of the stores that have been visited
        /// </summary>
        /// <returns>String array with store identifiers of stores that have been visited</returns>
        public async Task<string[]> GetStores()
        {
            var stores = await GetLidlDataRequestResultAsEntity<string[]>($"tickets/stores");
            return stores;
        }

        #endregion

        #region Unauthenticated API Calls

        /// <summary>
        /// Returns details on the configuration of the Lidl App
        /// </summary>
        /// <param name="clientId">ClientId to provide the configuration for or NULL to use the ClientId provided in the session constructor</param>
        /// <returns></returns>
        public async Task<string> GetClientConfigurationDetails(string clientId = null)
        {
            var pageContents = await Utilities.Http.GetRequestResult(new Uri(BaseAuthEndpointUri, $"/api/rules?client_id={clientId ?? ClientId}"), timeout: RequestTimeOut);
            return pageContents;
        }

        /// <summary>
        /// Returns the localized labels for the current language. No authenticated session required.
        /// </summary>
        /// <param name="language">Language to provide the localized labels for or NULL to take the language provided in the session constructor</param>
        /// <returns>Dictionary with the internal names as the key and the localized friendly text as the value</returns>
        public async Task<Dictionary<string, string>> GetAppTranslations(string language = null)
        {
            var pageContents = await Utilities.Http.GetRequestResult(new Uri(BaseAuthEndpointUri, $"/api/translations/{(language ?? Language).ToLowerInvariant()}"), timeout: RequestTimeOut);
            return JsonSerializer.Deserialize<Dictionary<string, string>>(pageContents);
        }

        /// <summary>
        /// Returns if the provided e-mail address exists in the Lidl App registrations. No authenticated session required.
        /// </summary>
        /// <param name="emailAddress">E-mail address to validate</param>
        /// <param name="clientId">The ClientId to check the e-mail address against. Leave NULL to use the ClientId used in the session constructor.</param>
        /// <returns>Instance of type <see cref="Entities.EmailExistsCheckResult"/> containing the result of the validation check</returns>
        public async Task<Entities.EmailExistsCheckResult> DoesEmailExist(string emailAddress, string clientId = null)
        {
            var pageContents = await Utilities.Http.GetRequestResult(new Uri(BaseAuthEndpointUri, $"/api/email/exists?clientId={clientId ?? ClientId}&Email={System.Web.HttpUtility.UrlEncode(emailAddress)}"), timeout: RequestTimeOut);

            var result = JsonSerializer.Deserialize<Entities.EmailExistsCheckResult>(pageContents);
            return result;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Performs a GET request to the provided Lidl Data Endpoint to download the results as a typed entity
        /// </summary>
        /// <param name="url">Partial URL of the data endpoint to query</param>
        /// <typeparam name="T">Type of entity to try to parse the JSON response into</typeparam>
        /// <returns>Parsed response into the provided entity type</returns>
        private async Task<T> GetLidlDataRequestResultAsEntity<T>(string url)
        {
            var resultString = await GetLidlDataRequestResultAsString(url);

            // Change the comma's into dots within numbers
            var localizedResultString = Regex.Replace(resultString, @"(?<=""\d+?),(?=\d+?"")", ".", RegexOptions.CultureInvariant);

            var resultEntity = JsonSerializer.Deserialize<T>(localizedResultString, new JsonSerializerOptions { NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString } ); 
            return resultEntity;
        }

        /// <summary>
        /// Performs a POST request to the provided Lidl Data Endpoint to download the results as a typed entity
        /// </summary>
        /// <param name="url">Partial URL of the data endpoint to query</param>
        /// <typeparam name="T">Type of entity to try to parse the JSON response into</typeparam>
        /// <returns>Parsed response into the provided entity type</returns>
        private async Task<T> PostLidlDataRequestResultAsEntity<T>(string url)
        {
            var resultString = await PostLidlDataRequestResultAsString(url);

            // Change the comma's into dots within numbers
            var localizedResultString = Regex.Replace(resultString, @"(?<=""\d+?),(?=\d+?"")", ".", RegexOptions.CultureInvariant);

            var resultEntity = JsonSerializer.Deserialize<T>(localizedResultString, new JsonSerializerOptions { NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString });
            return resultEntity;
        }

        /// <summary>
        /// Performs a GET request to the provided Lidl Data Endpoint to download the results as a string
        /// </summary>
        /// <param name="url">Partial URL of the data endpoint to query</param>
        /// <returns>Returned JSON data as a string</returns>
        private async Task<string> GetLidlDataRequestResultAsString(string url)
        {
            // Ensure the session is authenticated
            await EnsureAuthenticated();

            // Retrieve the data as a string from the Lidl API
            return await Utilities.Http.GetRequestResult(new Uri(BaseDataEndpointLocalizedUri, $"{url}"), timeout: RequestTimeOut, headerFields: new() { { "App-Version", AppVersion }, { "Operating-System", OperatingSystem }, { "App", AppPackageName }, { "Accept-Language", Language }, { "Authorization", $"Bearer {JwtToken.AccessToken}" }, { "User-Agent", UserAgent } });
        }

        /// <summary>
        /// Performs a POST request to the provided Lidl Data Endpoint to download the results as a string
        /// </summary>
        /// <param name="url">Partial URL of the data endpoint to query</param>
        /// <returns>Returned JSON data as a string</returns>
        private async Task<string> PostLidlDataRequestResultAsString(string url)
        {
            // Ensure the session is authenticated
            await EnsureAuthenticated();

            // Retrieve the data as a string from the Lidl API
            return await Utilities.Http.PostRequestResult(new Uri(BaseDataEndpointLocalizedUri, $"{url}"), timeout: RequestTimeOut, headerFields: new() { { "App-Version", AppVersion }, { "Operating-System", OperatingSystem }, { "App", AppPackageName }, { "Accept-Language", Language }, { "Authorization", $"Bearer {JwtToken.AccessToken}" }, { "User-Agent", UserAgent } });
        }

        /// <summary>
        /// Ensures the current session is authenticated. If not, it will try to authenticate and if not possible to, it will throw a <see cref="Exceptions.NotAuthenticatedException"/>.
        /// </summary>
        public async Task EnsureAuthenticated()
        {
            if (!IsAuthenticated)
            {
                // Session is not authenticated, check if we have a refresh token
                if (JwtToken != null && !string.IsNullOrWhiteSpace(JwtToken.RefreshToken))
                {
                    // We have a refresh token, try to authenticate using the refresh token
                    await Authenticate(JwtToken.RefreshToken);
                }
                else
                {
                    // We do not have a refresh token, throw an exception
                    throw new Exceptions.NotAuthenticatedException();
                }
            }
        }

        #endregion
    }
}
