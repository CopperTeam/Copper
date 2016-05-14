using System;
using Copper.Core.Caching;

namespace Copper.Core.Caching
{
    /// <summary>
    /// 缓存扩展
    /// </summary>
    public static class CacheExtensions
    {
        /// <summary>
        /// 缓存变量，支持线程安全
        /// </summary>
        private static readonly object SyncObject = new object();

        /// <summary>
        /// 根据键获取缓存数据，如果不存在缓存中则加载数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="cacheManager">缓存管理</param>
        /// <param name="key">缓存键</param>
        /// <param name="acquire">函数加载项如果没有在缓存中</param>
        /// <returns>缓存数据</returns>
        public static T Get<T>(this ICacheManager cacheManager, string key, Func<T> acquire)
        {
            return Get(cacheManager, key, 60, acquire);
        }

        /// <summary>
        /// Get a cached item. If it's not in the cache yet, then load and cache it
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="key">Cache key</param>
        /// <param name="cacheTime">Cache time in minutes (0 - do not cache)</param>
        /// <param name="acquire">Function to load item if it's not in the cache yet</param>
        /// <returns>Cached item</returns>
        public static T Get<T>(this ICacheManager cacheManager, string key, int cacheTime, Func<T> acquire) 
        {
            lock (SyncObject)
            {
                if (cacheManager.IsSet(key))
                {
                    return cacheManager.Get<T>(key);
                }

                var result = acquire();
                if (cacheTime > 0)
                    cacheManager.Set(key, result, cacheTime);
                return result;
            }
        }
    }
}
