using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Text.RegularExpressions;

namespace Copper.Core.Caching
{
    /// <summary>
    /// 内存缓存管理
    /// </summary>
    public partial class MemoryCacheManager : ICacheManager
    {
        protected ObjectCache Cache
        {
            get { return MemoryCache.Default; }
        }

        /// <summary>
        /// 获取或设置与指定的键相关联的值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">健</param>
        /// <returns>指定的键相关联的值</returns>
        public virtual T Get<T>(string key)
        {
            return (T)Cache[key];
        }

        /// <summary>
        /// 将指定的键和对象添加到缓存中
        /// </summary>
        /// <param name="key">健</param>
        /// <param name="data">数据</param>
        /// <param name="cacheTime">缓存时间（分）</param>
        public virtual void Set(string key, object data, int cacheTime)
        {
            if (data == null)
                return;

            var policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(cacheTime);
            Cache.Add(new CacheItem(key, data), policy);
        }

        /// <summary>
        /// 获取一个值,该值指示值是否与指定的键相关联的是缓存
        /// </summary>
        /// <param name="key">健</param>
        /// <returns>结果</returns>
        public virtual bool IsSet(string key)
        {
            return (Cache.Contains(key));
        }

        /// <summary>
        /// 从缓存中删除值与指定键
        /// </summary>
        /// <param name="key">/key</param>
        public virtual void Remove(string key)
        {
            Cache.Remove(key);
        }

        /// <summary>
        /// 根据模式移除缓存项
        /// </summary>
        /// <param name="pattern">模式</param>
        public virtual void RemoveByPattern(string pattern)
        {
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = new List<String>();

            foreach (var item in Cache)
                if (regex.IsMatch(item.Key))
                    keysToRemove.Add(item.Key);

            foreach (string key in keysToRemove)
            {
                Remove(key);
            }
        }

        /// <summary>
        /// 清除所有缓存数据
        /// </summary>
        public virtual void Clear()
        {
            foreach (var item in Cache)
                Remove(item.Key);
        }
    }
}
