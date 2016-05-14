using System;
using Copper.Core.Caching;

namespace Copper.Core.Caching
{
    /// <summary>
    /// ������չ
    /// </summary>
    public static class CacheExtensions
    {
        /// <summary>
        /// ���������֧���̰߳�ȫ
        /// </summary>
        private static readonly object SyncObject = new object();

        /// <summary>
        /// ���ݼ���ȡ�������ݣ���������ڻ��������������
        /// </summary>
        /// <typeparam name="T">����</typeparam>
        /// <param name="cacheManager">�������</param>
        /// <param name="key">�����</param>
        /// <param name="acquire">�������������û���ڻ�����</param>
        /// <returns>��������</returns>
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