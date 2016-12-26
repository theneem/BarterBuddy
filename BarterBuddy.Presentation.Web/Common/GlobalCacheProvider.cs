using System.Runtime.Caching;

namespace BarterBuddy.Presentation.Web.Common
{
    /// <summary>
    /// Global cache provider
    /// </summary>
    /// <typeparam name="T">Generic type</typeparam>
    /// <seealso cref="GeLoc.Framework.Caching.CachingProviderBase{T}" />
    /// <seealso>
    ///   <cref>GeLoc.BusinessManager.Entities.CachingProviderBase</cref>
    /// </seealso>
    public sealed class GlobalCacheProvider<T> : CachingProviderBase<T>
    {
        #region Singleton 

        /// <summary>
        /// Prevents a default instance of the <see cref="GlobalCacheProvider{T}"/> class from being created.
        /// </summary>
        private GlobalCacheProvider()
        {
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static GlobalCacheProvider<T> Instance => Nested.Instance;

        #endregion

        #region ICachingProvider

        /// <summary>
        /// Adds the item.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public new void AddItem(string key, T value)
        {
            base.AddItem(key, value);
        }

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>Object of an item</returns>
        public T GetItem(string key)
        {
            return base.GetItem(key, false); ////Remove default is true because it's Global Cache!
        }

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="remove">if set to <c>true</c> [remove].</param>
        /// <returns>object of an item</returns>
        public new T GetItem(string key, bool remove)
        {
            return base.GetItem(key, remove);
        }

        /// <summary>
        /// Sets the user specific detail.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="keyValue">The key value.</param>
        public void SetUserSpecificDetail(string key, T keyValue)
        {
            base.AddItem($"{System.Web.HttpContext.Current.Session.SessionID}-{key}", keyValue);
        }

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="remove">if set to <c>true</c> [remove].</param>
        /// <returns>object of an item</returns>
        public T GetUserSpecificDetail(string key, bool remove = false)
        {
            if (System.Web.HttpContext.Current != null)
                return base.GetItem($"{System.Web.HttpContext.Current.Session.SessionID}-{key}", remove);
            return default(T);
        }

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <param name="key">The key.</param>
        public void RemoveUserSpecificDetail(string key)
        {
            if (System.Web.HttpContext.Current != null)
                base.RemoveItem($"{System.Web.HttpContext.Current.Session.SessionID}-{key}");
        }

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="sessionId">The session identifier.</param>
        public void RemoveUserSpecificDetail(string key, string sessionId)
        {
            base.RemoveItem($"{sessionId}-{key}");
        }

        /// <summary>
        /// Removes the item.
        /// </summary>
        /// <param name="key">The key.</param>
        public new void RemoveItem(string key)
        {
            base.RemoveItem(key);
        }

        /// <summary>
        /// Gets all cache.
        /// </summary>
        /// <returns>Get all the cache items</returns>
        public new MemoryCache GetAllCacheItems()
        {
            return base.GetAllCacheItems();
        }
        #endregion

        /// <summary>
        /// Nested class
        /// </summary>
        // ReSharper disable once ClassNeverInstantiated.Local
        private class Nested
        {
            /// <summary>
            /// The instance
            /// </summary>
            // ReSharper disable once MemberHidesStaticFromOuterClass
            // ReSharper disable once StaticMemberInGenericType

            internal static readonly GlobalCacheProvider<T> Instance = new GlobalCacheProvider<T>();

            //// Explicit static constructor to tell C# compiler
            //// not to mark type as beforefieldinit

            /////// <summary>
            /////// Initializes the <see cref="Nested"/> class.
            /////// </summary>
            ////static Nested()
            ////{
            ////    ////Constructor code should be added here.
            ////}
        }
    }
}