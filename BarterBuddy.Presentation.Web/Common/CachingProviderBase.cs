using System;
using System.IO;
using System.Runtime.Caching;

namespace BarterBuddy.Presentation.Web.Common
{
    /// <summary>
    /// Cache provider base class.
    /// </summary>
    /// <typeparam name="T">Generic type</typeparam>
    /// <seealso cref="System.IDisposable" />

    public abstract class CachingProviderBase<T> : IDisposable
    {
        /// <summary>
        /// The cache
        /// </summary>
        private readonly MemoryCache cache = new MemoryCache("CachingProvider");

        /// <summary>
        /// The log path
        /// </summary>
        private readonly string logPath = Environment.GetEnvironmentVariable("TEMP");

        /// <summary>
        /// Initializes a new instance of the <see cref="CachingProviderBase{T}"/> class.
        /// </summary>
        protected CachingProviderBase()
        {
            DeleteLog();
        }

        /// <summary>
        /// Gets the padlock.
        /// </summary>
        /// <value>
        /// The padlock.
        /// </value>
        private static object Padlock { get; } = new object();

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Adds the item.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        protected void AddItem(string key, T value)
        {
            lock (Padlock)
            {
                if (cache[key] != null)
                {
                    cache.Remove(key);
                }

                cache.Add(key, value, DateTimeOffset.MaxValue);
            }
        }

        /// <summary>
        /// Removes the item.
        /// </summary>
        /// <param name="key">The key.</param>
        protected void RemoveItem(string key)
        {
            lock (Padlock)
            {
                cache.Remove(key);
            }
        }

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="remove">if set to <c>true</c> [remove].</param>
        /// <returns>Returns an object of an item</returns>
        protected T GetItem(string key, bool remove)
        {
            lock (Padlock)
            {
                var cacheValue = cache[key];
                if (cacheValue == null)
                {
                    return default(T);
                }

                if (remove)
                {
                    cache.Remove(key);
                }

                return (T)cacheValue;
            }
        }

        #region Error Logs

        /////// <summary>
        /////// Writes to log.
        /////// </summary>
        /////// <param name="text">The text.</param>
        ////private void WriteToLog(string text)
        ////{
        ////    if (logPath == null) return;
        ////    using (var tw = File.AppendText($"{logPath}\\CachingProvider_Errors.txt"))
        ////    {
        ////        tw.WriteLine(text);
        ////    }
        ////}

        #endregion

        /// <summary>
        /// Gets all cache items.
        /// </summary>
        /// <returns>Cache items</returns>
        protected MemoryCache GetAllCacheItems()
        {
            return cache;
        }

        /// <summary>
        /// Deletes the log.
        /// </summary>
        private void DeleteLog()
        {
            File.Delete($"{logPath}\\CachingProvider_Errors.txt");
        }
    }
}
