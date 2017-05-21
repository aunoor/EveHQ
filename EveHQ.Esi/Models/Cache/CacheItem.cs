namespace EveHQ.Esi.Models.Cache
{
    using System;

    /// <summary>
    /// Represent a cached item stored by the caching system
    /// It contains the time when data has been registered and its associated content
    /// </summary>
    public class CacheItem
    {
        /// <summary>
        /// The hours amount before the element expire
        /// </summary>
        private int _cacheTime;
        
        /// <summary>
        /// Initializes a new instance of the CacheItem class
        /// </summary>
        /// <param name="hours">Hours amount before the item expires</param>
        /// <param name="data">Data to be cache</param>
        internal CacheItem(int hours, string data)
        {
            this.CacheTime = hours;
            this.CachedData = data;
        }

        /// <summary>
        /// Gets the hours amount during the item should be cached
        /// Set the expiration timestamp by adding the associated value to the current timestamp
        /// </summary>
        internal int CacheTime
        {
            get
            {
                return _cacheTime;
            }

            private set
            {
                this.ExpiredTimestamp = ((long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds) + (value * 60 * 100);
                this._cacheTime = value;
            }
        }

        /// <summary>
        /// Gets the timestamp when the cached item expires
        /// </summary>
        internal long ExpiredTimestamp { get; private set; }

        /// <summary>
        /// Gets the cached content
        /// </summary>
        internal string CachedData { get; private set; }
    }
}
