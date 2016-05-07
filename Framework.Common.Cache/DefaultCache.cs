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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="cacheValue"></param>
        /// <param name="expireMinutes"></param>
        /// <param name="isAbsoluteExpire">
        /// true : 绝对过期时间，当超过设定时间，立即移除。
        /// false : 滑动过期时间 当超过设定时间没再使用时，才移除缓存
        /// </param>
        public void Set(string cacheKey, object cacheValue, int expireMinutes = 0, bool isAbsoluteExpire = true)
        {
            if (expireMinutes > 0)
            {
                if (isAbsoluteExpire)
                    this._cache.Insert(cacheKey, cacheValue, null, DateTime.Now.AddMinutes(expireMinutes), System.Web.Caching.Cache.NoSlidingExpiration);
                else
                    this._cache.Insert(cacheKey, cacheValue, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(expireMinutes));
            }
            else
                this._cache.Insert(cacheKey, cacheValue);
        }

        public T Get<T>(string cacheKey)
        {
            return (T)this._cache[cacheKey];
        }

        public void Remove(string cacheKey)
        {
            if (this._cache[cacheKey] != null)
            {
                this._cache.Remove(cacheKey);
            }
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