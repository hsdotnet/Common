using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Common.Cache
{
    public class RedisCache : ICache
    {
        public bool Exists(string cacheKey)
        {
            throw new NotImplementedException();
        }

        public T Get<T>(string cacheKey)
        {
            throw new NotImplementedException();
        }

        public void Remove(string cacheKey)
        {
            throw new NotImplementedException();
        }

        public void RemoveAll()
        {
            throw new NotImplementedException();
        }

        public void Set(string cacheKey, object cacheValue, int expireMinutes = 0, bool isAbsoluteExpire = true)
        {
            throw new NotImplementedException();
        }
    }
}