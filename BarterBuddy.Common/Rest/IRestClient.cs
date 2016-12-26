using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarterBuddy.Common.Rest
{
    public interface IRestClient
    {

        /// <summary>
        /// GetAsync method for generic return types like class,enumerable etc.21 
        ///Exception in this method need to be handled by calling method.
        ///Current implementation of ILog (AzureStorageLog) cannot be used because it uses RestClient itself. 
        ///It will create circular reference.        
        /// </summary>
        Task<T> GetAsync<T>(string url, params object[] args);

        /// <summary>
        /// GetAsync method for type void.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        Task<string> GetAsync(string url, params object[] args);

        /// <summary>
        /// DeleteASync method.
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="args">Arguments</param>
        /// <returns></returns>
        Task DeleteAsync(string url, params object[] args);

        /// <summary>
        /// Call DELETE method of Rest Api with Payload of type TPayload and expecting result of type TResult
        /// </summary>
        /// <typeparam name="TPayload">Type of Payload</typeparam>
        /// <typeparam name="TResult">Type of Result</typeparam>
        /// <param name="url">Url of the Rest Resource with placeholders for argument</param>
        /// <param name="payload">Payload to be posted to Rest api</param>
        /// <param name="args">Values for placeholder arguments</param>
        /// <returns>Value of type TResult</returns>
        Task<TResult> DeleteAsync<TPayload, TResult>(string url, TPayload payload, params object[] args);

        /// <summary>
        /// Call DELETE method of Rest Api with Payload of type T
        /// </summary>
        /// <typeparam name="T">Type of Payload</typeparam>
        /// <param name="url">Url of the Rest Resource with placeholders for argument</param>
        /// <param name="payload">Payload to be posted to Rest api</param>
        /// <param name="args">Values for placeholder arguments</param>
        /// <returns></returns>
        Task DeleteAsync<T>(string url, T payload, params object[] args);

        /// <summary>
        /// Call POST method of Rest Api with Payload of type TPayload and expecting result of type TResult
        /// </summary>
        /// <typeparam name="TPayload">Type of Payload</typeparam>
        /// <typeparam name="TResult">Type of Result</typeparam>
        /// <param name="url">Url of the Rest Resource with placeholders for argument</param>
        /// <param name="payload">Payload to be posted to Rest api</param>
        /// <param name="args">Values for placeholder arguments</param>
        /// <returns>Value of type TResult</returns>
        Task<TResult> PostAsync<TPayload, TResult>(string url, TPayload payload, params object[] args);

        /// <summary>
        /// Call POST method of Rest Api with Payload of type T
        /// </summary>
        /// <typeparam name="T">Type of Payload</typeparam>
        /// <param name="url">Url of the Rest Resource with placeholders for argument</param>
        /// <param name="payload">Payload to be posted to Rest api</param>
        /// <param name="args">Values for placeholder arguments</param>
        /// <returns></returns>
        Task PostAsync<T>(string url, T payload, params object[] args);

        /// <summary>
        /// PostAsync method for sending post request.
        /// This method is deprecated in favor of Post method with Paylod argument
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">Post URL</param>
        /// <param name="postParameters">Arguments to be sent in post body</param>
        /// <param name="args">Arguments to be sent in query string</param>
        /// <returns></returns>
        Task<T> PostAsync<T>(string url, PostParameters postParameters, params object[] args);

        /// <summary>
        /// PostASync for void type.
        /// This method is deprecated in favor of Post method with Paylod argument
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="postParameters">Post Parameters</param>
        /// <param name="args">Arguments</param>
        /// <returns></returns>
        Task PostAsync(string url, PostParameters postParameters, params object[] args);

        /// <summary>
        /// PostAsync method for sending post request 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">Post URL</param>
        /// <param name="postParameters">Arguments to be sent in post body</param>
        /// <param name="args">Arguments to be sent in query string</param>
        /// <returns></returns>
        Task PutAsync<T>(string url, T payload, params object[] args);

        /// <summary>
        /// PostAsync method for sending post request 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">Post URL</param>
        /// <param name="postParameters">Arguments to be sent in post body</param>
        /// <param name="args">Arguments to be sent in query string</param>
        /// <returns></returns>
        Task<TResult> PutAsync<TPayload, TResult>(string url, TPayload payload, params object[] args); string BaseUrl { get; set; }

    }
}
