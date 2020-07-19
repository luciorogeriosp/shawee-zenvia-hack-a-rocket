using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Caching;

namespace Tropical.Infrastructure.Data
{
    public static class CacheProvider 
    {
        private static ObjectCache Cache { get { return MemoryCache.Default; } }
        private static int _cacheTime = 15;

        public static object Get(string key)
        {
            return Cache[key];
        }

        public static void Set(string key, object data, int cacheTime)
        {
            CacheItemPolicy policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(cacheTime);

            Cache.Add(new CacheItem(key, data), policy);
        }

        public static void Set(string key, object data)
        {
            CacheItemPolicy policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(_cacheTime);

            Cache.Add(new CacheItem(key, data), policy);
        }

        public static bool IsSet(string key)
        {
            return (Cache[key] != null);
        }

        public static void Invalidate(string key)
        {
            Cache.Remove(key);
        }
    }

}
