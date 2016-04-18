namespace Framework.Common.Cache
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICache
    {
        /// <summary>
        /// 缓存区域名称
        /// </summary>
        string RegionName { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        bool Exists(string cacheKey);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="cacheValue"></param>
        /// <param name="isAbsoluteExpire"></param>
        /// <param name="expireMinutes"></param>
        /// <returns></returns>
        bool Set(string cacheKey, object cacheValue, bool isAbsoluteExpire, int expireMinutes = 0);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        T Get<T>(string cacheKey);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        bool Remove(string cacheKey);

        /// <summary>
        /// 
        /// </summary>
        void RemoveAll();
    }
}