using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BarterBuddy.Common.Rest
{
    public struct PostParameters
    {
        public Dictionary<string, string> ParamCollection { get; set; }
    }

    public class RestClient : IRestClient
    {
        //object thisObject = new object();

        protected HttpClient httpClient = null;

        protected string baseUrl;
        //const string UserInfoHeader = "UserInfo";
        //private IServiceAuthenticationHelper _serviceAuthenticationHelper;

        ///// <summary>
        ///// Get current instance of IServiceAuthenticationHelper
        ///// </summary>
        //private IServiceAuthenticationHelper ServiceAuthenticationHelper
        //{
        //    get
        //    {
        //        if (null == _serviceAuthenticationHelper)
        //        {
        //            try
        //            {
        //                _serviceAuthenticationHelper = IocHelper.Resolve<IServiceAuthenticationHelper>();
        //            }
        //            catch
        //            {
        //                return null;
        //            }
        //        }
        //        return _serviceAuthenticationHelper;
        //    }
        //}

        //[InjectionConstructor]
        public RestClient()
        {
            //Default Constructor
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RestClient" /> class.
        /// </summary>
        /// <param name="baseUrl">The base URL.</param>
        public RestClient(string baseUrl)
        {
            httpClient = new HttpClient();
            this.baseUrl = baseUrl;
        }

        private HttpClient HttpClient
        {
            get
            {
                if (httpClient == null)
                    httpClient = new HttpClient();

                return httpClient;
            }
        }

        /// <summary>
        /// Gets or sets the base URL.
        /// </summary>
        /// <value>The base URL.</value>
        public string BaseUrl
        {
            get
            {
                return baseUrl;
            }
            set
            {
                baseUrl = value;
            }
        }

        /// <summary>
        /// GetAsync method for generic return types like class,enumerable etc.21 
        ///Exception in this method need to be handled by calling method.
        ///Current implementation of ILog (AzureStorageLog) cannot be used because it uses RestClient itself. 
        ///It will create circular reference.        
        /// </summary>
        public async Task<T> GetAsync<T>(string url, params object[] args)
        {
            if (string.IsNullOrEmpty(baseUrl))
                throw new ArgumentNullException("BaseUrl");

            var restUrl = string.Format(baseUrl + (args != null ? string.Format(url, args) : url));

            //AddHeadersToHttpClient();

            HttpResponseMessage response = null;
            try
            {
                response = await HttpClient.GetAsync(restUrl);
            }
            catch (HttpRequestException ex)
            {
                WebApiException webApiException;

                if (ex.InnerException == null)
                {
                    webApiException = new WebApiException(restUrl,
                                                HttpStatusCode.InternalServerError,
                                                HttpStatusCode.InternalServerError.ToString(),
                                                response,
                                                ex.Message);
                }
                else if (ex.InnerException is WebException)
                {
                    var innerWebException = ex.InnerException as WebException;

                    webApiException = new WebApiException(restUrl,
                                            HttpStatusCode.NotFound,    //TODO: Do we always set it to NotFound here?
                                            innerWebException.Status.ToString(),
                                            response,
                                            innerWebException.Message);
                }
                else
                {
                    webApiException = new WebApiException(restUrl,
                                                HttpStatusCode.InternalServerError, //TODO: Do we always set to Server Error Here?
                                                HttpStatusCode.InternalServerError.ToString(),
                                                response,
                                                ex.Message);
                }

                throw webApiException;
            }
            catch (TaskCanceledException)
            {
                throw new WebApiException(restUrl,
                                            HttpStatusCode.GatewayTimeout,
                                            "Task Canceled.",
                                            response);
            }

            await response.EnsureSuccessStatusCodeEx();

            var json = await response.Content.ReadAsStringAsync();

            var serializedObject = JsonConvert.DeserializeObject<T>(json);

            return serializedObject;
        }

        /// <summary>
        /// GetAsync method for type void.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public async Task<string> GetAsync(string url, params object[] args)
        {
            if (string.IsNullOrEmpty(baseUrl))
                throw new ArgumentNullException("BaseUrl");

            var restUrl = string.Format(baseUrl + string.Format(url, args));
            //AddHeadersToHttpClient();
            HttpResponseMessage response = null;

            try
            {
                response = await HttpClient.GetAsync(restUrl);
            }
            catch (HttpRequestException ex)
            {
                WebApiException webApiException;

                if (ex.InnerException == null)
                {
                    webApiException = new WebApiException(restUrl,
                                                HttpStatusCode.InternalServerError,
                                                HttpStatusCode.InternalServerError.ToString(),
                                                response,
                                                ex.Message);
                }
                else if (ex.InnerException is WebException)
                {
                    var innerWebException = ex.InnerException as WebException;

                    webApiException = new WebApiException(restUrl,
                                            HttpStatusCode.NotFound,    //TODO: Do we always set it to NotFound here?
                                            innerWebException.Status.ToString(),
                                            response,
                                            innerWebException.Message);
                }
                else
                {
                    webApiException = new WebApiException(restUrl,
                                                HttpStatusCode.InternalServerError, //TODO: Do we always set to Server Error Here?
                                                HttpStatusCode.InternalServerError.ToString(),
                                                response,
                                                ex.Message);
                }

                throw webApiException;
            }
            catch (TaskCanceledException)
            {
                throw new WebApiException(restUrl,
                                            HttpStatusCode.GatewayTimeout,
                                            "Task Canceled.",
                                            response);
            }

            await response.EnsureSuccessStatusCodeEx();

            var jsonResult = await response.Content.ReadAsStringAsync();

            return jsonResult;
        }

        /// <summary>
        /// DeleteASync method.
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="args">Arguments</param>
        /// <returns></returns>
        public async Task DeleteAsync(string url, params object[] args)
        {
            if (string.IsNullOrEmpty(baseUrl))
                throw new ArgumentNullException("BaseUrl");

            //var restUrl = string.Format(_baseUrl + string.Format(url, args));
            args = args.Skip(1).ToArray();
            var restUrl = string.Format(baseUrl + (args != null ? string.Format(url, args) : url));
            //AddHeadersToHttpClient();
            HttpResponseMessage response = null;
            try
            {
                response = await HttpClient.DeleteAsync(restUrl);
            }
            catch (HttpRequestException ex)
            {
                WebApiException webApiException;

                if (ex.InnerException == null)
                {
                    webApiException = new WebApiException(restUrl,
                                                HttpStatusCode.InternalServerError,
                                                HttpStatusCode.InternalServerError.ToString(),
                                                response,
                                                ex.Message);
                }
                else if (ex.InnerException is WebException)
                {
                    var innerWebException = ex.InnerException as WebException;

                    webApiException = new WebApiException(restUrl,
                                            HttpStatusCode.NotFound,    //TODO: Do we always set it to NotFound here?
                                            innerWebException.Status.ToString(),
                                            response,
                                            innerWebException.Message);
                }
                else
                {
                    webApiException = new WebApiException(restUrl,
                                                HttpStatusCode.InternalServerError, //TODO: Do we always set to Server Error Here?
                                                HttpStatusCode.InternalServerError.ToString(),
                                                response,
                                                ex.Message);
                }

                throw webApiException;
            }
            catch (TaskCanceledException)
            {
                throw new WebApiException(restUrl,
                                            HttpStatusCode.GatewayTimeout,
                                            "Task Canceled.",
                                            response);
            }

            await response.EnsureSuccessStatusCodeEx();
        }

        /// <summary>
        /// Call DELETE method of Rest Api with Payload of type TPayload and expecting result of type TResult
        /// </summary>
        /// <typeparam name="TPayload">Type of Payload</typeparam>
        /// <typeparam name="TResult">Type of Result</typeparam>
        /// <param name="url">Url of the Rest Resource with placeholders for argument</param>
        /// <param name="payload">Payload to be posted to Rest api</param>
        /// <param name="args">Values for placeholder arguments</param>
        /// <returns>Value of type TResult</returns>
        public async Task<TResult> DeleteAsync<TPayload, TResult>(string url, TPayload payload, params object[] args)
        {
            if (string.IsNullOrEmpty(baseUrl))
                throw new ArgumentNullException("BaseUrl");

            var restUrl = string.Format(baseUrl + string.Format(url, args));
            //AddHeadersToHttpClient();

            HttpResponseMessage response = null;
            try
            {
                response = await HttpClient.DeleteAsJsonAsync<TPayload>(restUrl, payload);
            }
            catch (HttpRequestException ex)
            {
                WebApiException webApiException;

                if (ex.InnerException == null)
                {
                    webApiException = new WebApiException(restUrl,
                                                HttpStatusCode.InternalServerError,
                                                HttpStatusCode.InternalServerError.ToString(),
                                                response,
                                                ex.Message);
                }
                else if (ex.InnerException is WebException)
                {
                    var innerWebException = ex.InnerException as WebException;

                    webApiException = new WebApiException(restUrl,
                                            HttpStatusCode.NotFound,    //TODO: Do we always set it to NotFound here?
                                            innerWebException.Status.ToString(),
                                            response,
                                            innerWebException.Message);
                }
                else
                {
                    webApiException = new WebApiException(restUrl,
                                                HttpStatusCode.InternalServerError, //TODO: Do we always set to Server Error Here?
                                                HttpStatusCode.InternalServerError.ToString(),
                                                response,
                                                ex.Message);
                }

                throw webApiException;
            }
            catch (TaskCanceledException)
            {
                throw new WebApiException(restUrl,
                                            HttpStatusCode.GatewayTimeout,
                                            "Task Canceled.",
                                            response);
            }

            await response.EnsureSuccessStatusCodeEx();

            var json = await response.Content.ReadAsStringAsync();

            var serializer = JsonConvert.DeserializeObject<TResult>(json);
            return serializer;
        }

        /// <summary>
        /// Call DELETE method of Rest Api with Payload of type T
        /// </summary>
        /// <typeparam name="T">Type of Payload</typeparam>
        /// <param name="url">Url of the Rest Resource with placeholders for argument</param>
        /// <param name="payload">Payload to be posted to Rest api</param>
        /// <param name="args">Values for placeholder arguments</param>
        /// <returns></returns>
        public async Task DeleteAsync<T>(string url, T payload, params object[] args)
        {
            if (string.IsNullOrEmpty(baseUrl))
                throw new ArgumentNullException("BaseUrl");

            var restUrl = string.Format(baseUrl + string.Format(url, args));

            //AddHeadersToHttpClient();
            HttpResponseMessage response = null;

            try
            {
                response = await HttpClient.DeleteAsJsonAsync<T>(restUrl, payload);
            }
            catch (HttpRequestException ex)
            {
                WebApiException webApiException;

                if (ex.InnerException == null)
                {
                    webApiException = new WebApiException(restUrl,
                                                HttpStatusCode.InternalServerError,
                                                HttpStatusCode.InternalServerError.ToString(),
                                                response,
                                                ex.Message);
                }
                else if (ex.InnerException is WebException)
                {
                    var innerWebException = ex.InnerException as WebException;

                    webApiException = new WebApiException(restUrl,
                                            HttpStatusCode.NotFound,    //TODO: Do we always set it to NotFound here?
                                            innerWebException.Status.ToString(),
                                            response,
                                            innerWebException.Message);
                }
                else
                {
                    webApiException = new WebApiException(restUrl,
                                                HttpStatusCode.InternalServerError, //TODO: Do we always set to Server Error Here?
                                                HttpStatusCode.InternalServerError.ToString(),
                                                response,
                                                ex.Message);
                }

                throw webApiException;
            }
            catch (TaskCanceledException)
            {
                throw new WebApiException(restUrl,
                                            HttpStatusCode.GatewayTimeout,
                                            "Task Canceled.",
                                            response);
            }

            await response.EnsureSuccessStatusCodeEx();
        }

        /// <summary>
        /// Call POST method of Rest Api with Payload of type TPayload and expecting result of type TResult
        /// </summary>
        /// <typeparam name="TPayload">Type of Payload</typeparam>
        /// <typeparam name="TResult">Type of Result</typeparam>
        /// <param name="url">Url of the Rest Resource with placeholders for argument</param>
        /// <param name="payload">Payload to be posted to Rest api</param>
        /// <param name="args">Values for placeholder arguments</param>
        /// <returns>Value of type TResult</returns>
        public async Task<TResult> PostAsync<TPayload, TResult>(string url, TPayload payload, params object[] args)
        {
            if (string.IsNullOrEmpty(baseUrl))
                throw new ArgumentNullException("BaseUrl");

            var restUrl = string.Format(baseUrl + string.Format(url, args));
            //AddHeadersToHttpClient();

            HttpResponseMessage response = null;
            try
            {
                if (httpClient.Timeout != TimeSpan.FromMinutes(10))
                {
                    httpClient = new HttpClient();
                    httpClient.Timeout = TimeSpan.FromMinutes(10);
                }

                response = await HttpClient.PostAsJsonAsync<TPayload>(restUrl, payload);
            }
            catch (HttpRequestException ex)
            {
                WebApiException webApiException;

                if (ex.InnerException == null)
                {
                    webApiException = new WebApiException(restUrl,
                                                HttpStatusCode.InternalServerError,
                                                HttpStatusCode.InternalServerError.ToString(),
                                                response,
                                                ex.Message);
                }
                else if (ex.InnerException is WebException)
                {
                    var innerWebException = ex.InnerException as WebException;

                    webApiException = new WebApiException(restUrl,
                                            HttpStatusCode.NotFound,    //TODO: Do we always set it to NotFound here?
                                            innerWebException.Status.ToString(),
                                            response,
                                            innerWebException.Message);
                }
                else
                {
                    webApiException = new WebApiException(restUrl,
                                                HttpStatusCode.InternalServerError, //TODO: Do we always set to Server Error Here?
                                                HttpStatusCode.InternalServerError.ToString(),
                                                response,
                                                ex.Message);
                }

                throw webApiException;
            }
            catch (TaskCanceledException)
            {
                throw new WebApiException(restUrl,
                                            HttpStatusCode.GatewayTimeout,
                                            "Task Canceled.",
                                            response);
            }

            await response.EnsureSuccessStatusCodeEx();

            var json = await response.Content.ReadAsStringAsync();

            var serializer = JsonConvert.DeserializeObject<TResult>(json);
            return serializer;
        }

        /// <summary>
        /// Call POST method of Rest Api with Payload of type T
        /// </summary>
        /// <typeparam name="T">Type of Payload</typeparam>
        /// <param name="url">Url of the Rest Resource with placeholders for argument</param>
        /// <param name="payload">Payload to be posted to Rest api</param>
        /// <param name="args">Values for placeholder arguments</param>
        /// <returns></returns>
        public async Task PostAsync<T>(string url, T payload, params object[] args)
        {
            if (string.IsNullOrEmpty(baseUrl))
                throw new ArgumentNullException("BaseUrl");

            var restUrl = string.Format(baseUrl + (args != null ? string.Format(url, args) : url));//string.Format(_baseUrl + string.Format(url, args));

            //AddHeadersToHttpClient();
            HttpResponseMessage response = null;

            try
            {
                response = await HttpClient.PostAsJsonAsync<T>(restUrl, payload);
            }
            catch (HttpRequestException ex)
            {
                WebApiException webApiException;

                if (ex.InnerException == null)
                {
                    webApiException = new WebApiException(restUrl,
                                                HttpStatusCode.InternalServerError,
                                                HttpStatusCode.InternalServerError.ToString(),
                                                response,
                                                ex.Message);
                }
                else if (ex.InnerException is WebException)
                {
                    var innerWebException = ex.InnerException as WebException;

                    webApiException = new WebApiException(restUrl,
                                            HttpStatusCode.NotFound,    //TODO: Do we always set it to NotFound here?
                                            innerWebException.Status.ToString(),
                                            response,
                                            innerWebException.Message);
                }
                else
                {
                    webApiException = new WebApiException(restUrl,
                                                HttpStatusCode.InternalServerError, //TODO: Do we always set to Server Error Here?
                                                HttpStatusCode.InternalServerError.ToString(),
                                                response,
                                                ex.Message);
                }

                throw webApiException;
            }
            catch (TaskCanceledException)
            {
                throw new WebApiException(restUrl,
                                            HttpStatusCode.GatewayTimeout,
                                            "Task Canceled.",
                                            response);
            }

            await response.EnsureSuccessStatusCodeEx();
        }

        /// <summary>
        /// PostAsync method for sending post request.
        /// This method is deprecated in favor of Post method with Paylod argument
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">Post URL</param>
        /// <param name="postParameters">Arguments to be sent in post body</param>
        /// <param name="args">Arguments to be sent in query string</param>
        /// <returns></returns>
        [Obsolete]
        public async Task<T> PostAsync<T>(string url, PostParameters postParameters, params object[] args)
        {
            if (string.IsNullOrEmpty(baseUrl))
                throw new ArgumentNullException("BaseUrl");

            var restUrl = string.Format(baseUrl + string.Format(url, args));
            //AddHeadersToHttpClient();

            HttpResponseMessage response = null;
            try
            {
                response = await HttpClient.PostAsJsonAsync<PostParameters>(restUrl, postParameters);
            }
            catch (HttpRequestException ex)
            {
                WebApiException webApiException;

                if (ex.InnerException == null)
                {
                    webApiException = new WebApiException(restUrl,
                                                HttpStatusCode.InternalServerError,
                                                HttpStatusCode.InternalServerError.ToString(),
                                                response,
                                                ex.Message);
                }
                else if (ex.InnerException is WebException)
                {
                    var innerWebException = ex.InnerException as WebException;

                    webApiException = new WebApiException(restUrl,
                                            HttpStatusCode.NotFound,    //TODO: Do we always set it to NotFound here?
                                            innerWebException.Status.ToString(),
                                            response,
                                            innerWebException.Message);
                }
                else
                {
                    webApiException = new WebApiException(restUrl,
                                                HttpStatusCode.InternalServerError, //TODO: Do we always set to Server Error Here?
                                                HttpStatusCode.InternalServerError.ToString(),
                                                response,
                                                ex.Message);
                }

                throw webApiException;
            }
            catch (TaskCanceledException)
            {
                throw new WebApiException(restUrl,
                                            HttpStatusCode.GatewayTimeout,
                                            "Task Canceled.",
                                            response);
            }

            await response.EnsureSuccessStatusCodeEx();

            var json = await response.Content.ReadAsStringAsync();

            var serializer = JsonConvert.DeserializeObject<T>(json);
            return serializer;
        }

        /// <summary>
        /// PostASync for void type.
        /// This method is deprecated in favor of Post method with Paylod argument
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="postParameters">Post Parameters</param>
        /// <param name="args">Arguments</param>
        /// <returns></returns>
        [Obsolete]
        public async Task PostAsync(string url, PostParameters postParameters, params object[] args)
        {
            if (string.IsNullOrEmpty(baseUrl))
                throw new ArgumentNullException("BaseUrl");

            var restUrl = string.Format(baseUrl + string.Format(url, args));

            //AddHeadersToHttpClient();
            HttpResponseMessage response = null;
            try
            {
                response = await HttpClient.PostAsJsonAsync<PostParameters>(restUrl, postParameters);
            }
            catch (HttpRequestException ex)
            {
                WebApiException webApiException;

                if (ex.InnerException == null)
                {
                    webApiException = new WebApiException(restUrl,
                                                HttpStatusCode.InternalServerError,
                                                HttpStatusCode.InternalServerError.ToString(),
                                                response,
                                                ex.Message);
                }
                else if (ex.InnerException is WebException)
                {
                    var innerWebException = ex.InnerException as WebException;

                    webApiException = new WebApiException(restUrl,
                                            HttpStatusCode.NotFound,    //TODO: Do we always set it to NotFound here?
                                            innerWebException.Status.ToString(),
                                            response,
                                            innerWebException.Message);
                }
                else
                {
                    webApiException = new WebApiException(restUrl,
                                                HttpStatusCode.InternalServerError, //TODO: Do we always set to Server Error Here?
                                                HttpStatusCode.InternalServerError.ToString(),
                                                response,
                                                ex.Message);
                }

                throw webApiException;
            }
            catch (TaskCanceledException)
            {
                throw new WebApiException(restUrl,
                                            HttpStatusCode.GatewayTimeout,
                                            "Task Canceled.",
                                            response);
            }

            await response.EnsureSuccessStatusCodeEx();
        }

        /// <summary>
        /// PostAsync method for sending post request 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">Post URL</param>
        /// <param name="postParameters">Arguments to be sent in post body</param>
        /// <param name="args">Arguments to be sent in query string</param>
        /// <returns></returns>
        public async Task PutAsync<T>(string url, T payload, params object[] args)
        {
            if (string.IsNullOrEmpty(baseUrl))
                throw new ArgumentNullException("BaseUrl");

            var restUrl = string.Format(baseUrl + string.Format(url, args));
            //AddHeadersToHttpClient();

            HttpResponseMessage response = null;
            try
            {
                response = await HttpClient.PutAsJsonAsync(restUrl, payload);
            }
            catch (HttpRequestException ex)
            {
                WebApiException webApiException;

                if (ex.InnerException == null)
                {
                    webApiException = new WebApiException(restUrl,
                                                HttpStatusCode.InternalServerError,
                                                HttpStatusCode.InternalServerError.ToString(),
                                                response,
                                                ex.Message);
                }
                else if (ex.InnerException is WebException)
                {
                    var innerWebException = ex.InnerException as WebException;

                    webApiException = new WebApiException(restUrl,
                                            HttpStatusCode.NotFound,    //TODO: Do we always set it to NotFound here?
                                            innerWebException.Status.ToString(),
                                            response,
                                            innerWebException.Message);
                }
                else
                {
                    webApiException = new WebApiException(restUrl,
                                                HttpStatusCode.InternalServerError, //TODO: Do we always set to Server Error Here?
                                                HttpStatusCode.InternalServerError.ToString(),
                                                response,
                                                ex.InnerException.Message);
                }

                throw webApiException;
            }
            catch (TaskCanceledException)
            {
                throw new WebApiException(restUrl,
                                            HttpStatusCode.GatewayTimeout,
                                            "Task Canceled.",
                                            response);
            }

            await response.EnsureSuccessStatusCodeEx();
        }

        /// <summary>
        /// PostAsync method for sending post request 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">Post URL</param>
        /// <param name="postParameters">Arguments to be sent in post body</param>
        /// <param name="args">Arguments to be sent in query string</param>
        /// <returns></returns>
        public async Task<TResult> PutAsync<TPayload, TResult>(string url, TPayload payload, params object[] args)
        {
            if (string.IsNullOrEmpty(baseUrl))
                throw new ArgumentNullException("BaseUrl");

            var restUrl = string.Format(baseUrl + string.Format(url, args));
            //AddHeadersToHttpClient();

            HttpResponseMessage response = null;
            try
            {
                response = await HttpClient.PutAsJsonAsync(restUrl, payload);
            }
            catch (HttpRequestException ex)
            {
                WebApiException webApiException;

                if (ex.InnerException == null)
                {
                    webApiException = new WebApiException(restUrl,
                                                HttpStatusCode.InternalServerError,
                                                HttpStatusCode.InternalServerError.ToString(),
                                                response,
                                                ex.Message);
                }
                else if (ex.InnerException is WebException)
                {
                    var innerWebException = ex.InnerException as WebException;

                    webApiException = new WebApiException(restUrl,
                                            HttpStatusCode.NotFound,    //TODO: Do we always set it to NotFound here?
                                            innerWebException.Status.ToString(),
                                            response,
                                            innerWebException.Message);
                }
                else
                {
                    webApiException = new WebApiException(restUrl,
                                                HttpStatusCode.InternalServerError, //TODO: Do we always set to Server Error Here?
                                                HttpStatusCode.InternalServerError.ToString(),
                                                response,
                                                ex.InnerException.Message);
                }

                throw webApiException;
            }
            catch (TaskCanceledException)
            {
                throw new WebApiException(restUrl,
                                            HttpStatusCode.GatewayTimeout,
                                            "Task Canceled.",
                                            response);
            }

            await response.EnsureSuccessStatusCodeEx();

            var json = await response.Content.ReadAsStringAsync();

            var serializer = JsonConvert.DeserializeObject<TResult>(json);
            return serializer;
        }

        /// <summary>
        /// This function will add accesstoken and userinfo in header.
        /// </summary>
        /// 
        //private void AddHeadersToHttpClient()
        //{
        //    lock (thisObject)
        //    {
        //        if (null != ServiceAuthenticationHelper)
        //        {
        //            if (!HttpClient.DefaultRequestHeaders.Contains(WaadConstants.AuthorizationHeader))
        //            {
        //                string accessToken = _serviceAuthenticationHelper.GetAccessToken();

        //                if (!string.IsNullOrEmpty(accessToken))
        //                {
        //                    HttpClient.DefaultRequestHeaders.Add(WaadConstants.AuthorizationHeader, accessToken);
        //                }
        //            }

        //            if (HttpClient.DefaultRequestHeaders.Contains(UserInfoHeader))
        //            {
        //                HttpClient.DefaultRequestHeaders.Remove(UserInfoHeader);
        //            }

        //            HttpClient.DefaultRequestHeaders.Add(UserInfoHeader, _serviceAuthenticationHelper.GetUserInfo());
        //        }
        //    }
        //}

        public Task<object> GetAsync<T1>(int p)
        {
            throw new NotImplementedException();
        }
    }
}
