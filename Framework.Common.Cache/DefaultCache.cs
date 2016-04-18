using System;
using System.Web;

namespace Framework.Common.Cache
{
    /// <summary>
    /// 
    /// </summary>
    public class DefaultCache : ICache
    {
        private readonly System.Web.Caching.Cache _cache;

        public DefaultCache()
        {
            this._cache = HttpRuntime.Cache;
        }

        public bool Exists(string cacheKey)
        {
            return this._cache[cacheKey] != null;
        }

        public bool Set(string cacheKey, object cacheValue, bool isAbsoluteExpire, int expireMinutes = 0)
        {
            bool result = true;

            if (expireMinutes > 0)
            {
                if (isAbsoluteExpire)
                {
                    this._cache.Insert(cacheKey, cacheValue, null, DateTime.Now.AddMinutes(expireMinutes), System.Web.Caching.Cache.NoSlidingExpiration);
                }
                else
                {
                    this._cache.Insert(cacheKey, cacheValue, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(expireMinutes));
                }
            }
            else
            {
                this._cache.Insert(cacheKey, cacheValue);
            }

            return result;
        }

        public T Get<T>(string cacheKey)
        {
            return (T)this._cache[cacheKey];
        }

        public bool Remove(string cacheKey)
        {
            bool result = true;

            if (this._cache[cacheKey] != null)
            {
                this._cache.Remove(cacheKey);
            }

            return result;
        }

        public void RemoveAll()
        {
            var caches = this._cache.GetEnumerator();

            while (caches.MoveNext())
            {
                this._cache.Remove(caches.Key.ToString());
            }
        }
    }
}