using App.Exam.Emite.Data.Interfaces.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace App.Exam.Emite.Data.Helpers
{
    public class MemoryCacheHelper : IMemoryCacheHelper
    {
        public static string NULL_VALUE = "NULL_VALUE";

        public async Task<T> GetObjectFromCache<T>(string cacheItemName, double cacheTimeInMinutes,
            Func<Task<T>> objectSetterCallback)
        {
            var cache = MemoryCache.Default;

            if (cache.Contains(cacheItemName))
            {
                var value = cache[cacheItemName];
                if (value.ToString() == NULL_VALUE)
                    return default(T);

                return (T)value;
            }

            if (objectSetterCallback == null)
            {
                throw new Exception($"There is no cache object setter for {cacheItemName}");
            }

            var cachedObject = await objectSetterCallback();

            var policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(cacheTimeInMinutes);

            if (cachedObject != null)
            {
                cache.Set(cacheItemName, cachedObject, policy);
            }
            else
            {
                cache.Set(cacheItemName, NULL_VALUE, policy);
            }

            return cachedObject;
        }
    }
}
