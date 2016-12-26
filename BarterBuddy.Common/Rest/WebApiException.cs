using System;
using System.Net;
using System.Net.Http;

namespace BarterBuddy.Common.Rest
{
    /// <summary>
    /// WebApiException class
    /// </summary>
    public class WebApiException : Exception
    {
        /// <summary>
        /// Gets the Url for which the exception occured
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// Http Status code of the exception
        /// </summary>
        public HttpStatusCode StatusCode { get; private set; }

        /// <summary>
        /// Http Status code of the exception
        /// </summary>
        public string StatusMessage { get; private set; }

        /// <summary>
        /// Gets or sets the response.
        /// </summary>
        /// <value>The response.</value>
        public HttpResponseMessage Response { get; private set; }

        /// <summary>
        /// Initializes a new instance of Web Api Exception with specified Url, StatusCode and Message
        /// </summary>
        /// <param name="url"></param>
        /// <param name="statusMessage"></param>
        /// <param name="message"></param>
        public WebApiException(string url, HttpStatusCode statusCode, string statusMessage, System.Net.Http.HttpResponseMessage response, string message = "")
            : base(message)
        {
            Url = url;
            StatusCode = statusCode;
            StatusMessage = statusMessage;
            Response = response;
        }
    }
}
