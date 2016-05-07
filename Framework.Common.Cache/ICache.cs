namespace Framework.Common.Cache
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICache
    {
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
        /// <param name="expireMinutes"></param>
        /// <param name="isAbsoluteExpire">
        /// true : 绝对过期时间，当超过设定时间，立即移除。
        /// false : 滑动过期时间 当超过设定时间没再使用时，才移除缓存
        /// </param>
        void Set(string cacheKey, object cacheValue, int expireMinutes = 0, bool isAbsoluteExpire = true);

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
        void Remove(string cacheKey);

        /// <summary>
        /// 
        /// </summary>
        void RemoveAll();
    }
}