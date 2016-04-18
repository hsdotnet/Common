using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Common.Cache
{
    public class DefaultCache : ICache
    {
        public string RegionName
        {
            get { throw new NotImplementedException(); }
        }

        public bool Exists(string cacheKey)
        {
            throw new NotImplementedException();
        }

        public bool Set(string cacheKey, object cacheValue, bool isAbsoluteExpire, int expireMinutes = 0)
        {
            throw new NotImplementedException();
        }

        public T Get<T>(string cacheKey)
        {
            throw new NotImplementedException();
        }

        public bool Remove(string cacheKey)
        {
            throw new NotImplementedException();
        }

        public void RemoveAll()
        {
            throw new NotImplementedException();
        }
    }
}