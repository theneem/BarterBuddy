using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BarterBuddy.Common.Rest
{
    /// <summary>
    /// Contains extension method for HttpResponseMessage to ensure successful content retrieval
    /// </summary>
    public static class HttpClientEx
    {
        /// <summary>
        /// Ensures that the response message has successful status code
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        public static async Task EnsureSuccessStatusCodeEx(this HttpResponseMessage sender)
        {
            var message = string.Empty;

            message = await sender.Content.ReadAsStringAsync();

            if (sender.StatusCode != HttpStatusCode.OK)
            {
                throw new WebApiException(sender.RequestMessage.RequestUri.ToString(), sender.StatusCode, "", sender, message);
            }
        }

        /// <summary>
        /// Sends a DELETE request as an asynchrounous operation, with a specified value serialized as JSON
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="httpClient"></param>
        /// <param name="url"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> DeleteAsJsonAsync<T>(this HttpClient httpClient, string url, T value)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, url);

            var bodyAsJson = JsonConvert.SerializeObject(value);
            request.Content = new StringContent(bodyAsJson);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await httpClient.SendAsync(request);

            return response;
        }

        /// <summary>
        /// Sends a DELETE request as an asynchrounous operation, with a specified value serialized as JSON
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="httpClient"></param>
        /// <param name="url"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> DeleteAsJsonAsync<T>(this HttpClient httpClient, Uri url, T value)
        {
            return await httpClient.DeleteAsJsonAsync<T>(url.ToString(), value);
        }

        /// <summary>
        /// Sends a DELETE request as an asynchrounous operation, with a specified value serialized as JSON
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="httpClient"></param>
        /// <param name="url"></param>
        /// <param name="value"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> DeleteAsJsonAsync<T>(this HttpClient httpClient, string url, T value, CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, url);

            var bodyAsJson = JsonConvert.SerializeObject(value);
            request.Content = new StringContent(bodyAsJson);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await httpClient.SendAsync(request, cancellationToken);

            return response;
        }

        /// <summary>
        /// Sends a DELETE request as an asynchrounous operation, with a specified value serialized as JSON
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="httpClient"></param>
        /// <param name="url"></param>
        /// <param name="value"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> DeleteAsJsonAsync<T>(this HttpClient httpClient, Uri url, T value, CancellationToken cancellationToken)
        {
            return await httpClient.DeleteAsJsonAsync<T>(url.ToString(), value, cancellationToken);
        }

    }
}
