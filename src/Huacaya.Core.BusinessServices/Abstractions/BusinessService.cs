//-----------------------------------------------------------------------
// <copyright file="BusinessService.cs" company="Teraeon Consulting, Corp">
//     Copyright (c) Teraeon Consulting, Corp. All rights reserved.
// </copyright>
// <author>Moe Yassine</author>
// <email>myassine@teraeon.com</email>
// <date>1/1/2010</date>
//-----------------------------------------------------------------------

namespace Runopia.Core.Services
{
    using System;
    using System.Web.Caching;
    using Data.Contracts;
    using Qualifiers;

    /// <summary>
    /// The BusinessObject class is the base class for which all services must be written.
    /// Included are a number of functions that are used by each business object.
    /// </summary>
    public abstract class BusinessService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessService"/> class.
        /// </summary>
        /// <param name="qualifiers">The dependent.</param>
        /// <remarks>This is in dependency injection while unit testing</remarks>
        public BusinessService(Qualifier qualifiers)
        {
            if (qualifiers == null)
            {
                throw new ArgumentNullException("dependent");
            }

            this.CacheFactory = qualifiers.CacheFactory;
            this.Logger = qualifiers.LogRepository;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessService"/> class.
        /// </summary>
        /// <param name="cacheRepository">The cache repository.</param>
        /// <param name="logger">The logger.</param>
        public BusinessService(ICacheRepository cacheRepository, ILogRepository logger)
        {
            this.CacheFactory = cacheRepository;
            this.Logger = logger;
        }

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>The logger.</value>
        protected ILogRepository Logger { get; set; }

        /// <summary>
        /// Gets or sets the cache factory.
        /// </summary>
        /// <value>The cache factory.</value>
        protected ICacheRepository CacheFactory { get; set; }

        /// <summary>
        /// Gets the cache key for a function.  This is used extensively for caching result counts for paging.
        /// </summary>
        /// <param name="name">The name of the function.</param>
        /// <param name="args">The the arguments.</param>
        /// <returns>A unique cache key for that function and arguments.</returns>
        public string GetCacheKey(string name, params object[] args)
        {
            name = "_" + name;
            foreach (object item in args)
            {
                name += "_";
                if (item != null)
                {
                    name += item.ToString();
                }
            }

            return name;
        }

        /// <summary>
        /// Sets the count to http cache.
        /// </summary>
        /// <param name="name">The name of the function.</param>
        /// <param name="count">The count.</param>
        /// <param name="args">The arguments in the function.</param>
        /// <returns>True if count worked</returns>
        protected bool SetCount(string name, int count, params object[] args)
        {
            try
            {
                this.CacheFactory.Add(this.GetCacheKey(name, args), count);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the count from HTTP cache.
        /// </summary>
        /// <param name="name">The name of the function.</param>
        /// <param name="args">The arguments in the function.</param>
        /// <returns>The count if found in the cache</returns>
        protected int GetCount(string name, params object[] args)
        {
            return (int)this.CacheFactory.Get(this.GetCacheKey(name, args));
        }

        /// <summary>
        /// Saves an object to cache.
        /// </summary>
        /// <param name="cacheKey">The cache key.</param>
        /// <param name="value">The value.</param>
        /// <param name="dependencies">The dependencies.</param>
        /// <param name="overWrite">True if allows to overwrite the current value</param>
        /// <returns>True if successfully added to cache, if overwrite is on then it should always return true</returns>
        protected bool SaveToCache(string cacheKey, object value, CacheDependency dependencies, bool overWrite = true)
        {
            this.PrepareCache(cacheKey, overWrite);
            try
            {
                this.CacheFactory.Insert(cacheKey, value, dependencies);
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Saves an object to cache.
        /// </summary>
        /// <param name="cacheKey">The cache key.</param>
        /// <param name="value">The value.</param>
        /// <param name="overWrite">True if allowed to overwrite current value</param>
        /// <returns>True if successfully added to cache, if overwrite is on it should always return true</returns>
        protected bool SaveToCache(string cacheKey, object value, bool overWrite = true)
        {
            this.PrepareCache(cacheKey, overWrite);

            try
            {
                this.CacheFactory.Add(cacheKey, value);
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Removes from cache.
        /// </summary>
        /// <param name="cacheKey">The cache key.</param>
        /// <returns>True if successfully removed from cache</returns>
        protected bool RemoveFromCache(string cacheKey)
        {
            if (this.CacheFactory.Get(cacheKey) == null)
            {
                return false;
            }

            this.CacheFactory.Remove(cacheKey);
            return true;
        }

        /// <summary>
        /// Gets an object from cache.
        /// </summary>
        /// <param name="cacheKey">The cache key.</param>
        /// <returns>The object from cache</returns>
        protected object GetFromCache(string cacheKey)
        {
            return this.CacheFactory.Get(cacheKey);
        }

        /// <summary>
        /// Prepares the cache by removing an item from cache if it is already
        /// in the cache system.
        /// </summary>
        /// <param name="cacheKey">The cache key.</param>
        /// <param name="overWrite">if set to <c>true</c> [over write].</param>
        /// <returns>True if the item was removed, false otherwise</returns>
        private bool PrepareCache(string cacheKey, bool overWrite = true)
        {
            if (!overWrite && this.CacheFactory.Get(cacheKey) != null)
            {
                return false;
            }

            if (this.CacheFactory.Get(cacheKey) != null)
            {
                this.CacheFactory.Remove(cacheKey);
            }

            return true;
        }
    }
}
