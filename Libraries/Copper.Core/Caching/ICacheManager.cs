namespace Copper.Core.Caching
{
    public interface ICacheManager
    {
        /// <summary>
        /// 获取或设置与指定的键相关联的值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">健</param>
        /// <returns>指定的键相关联的值</returns>
        T Get<T>(string key);

        /// <summary>
        /// 将指定的键和对象添加到缓存中
        /// </summary>
        /// <param name="key">健</param>
        /// <param name="data">数据</param>
        /// <param name="cacheTime">缓存时间（分）</param>
        void Set(string key, object data, int cacheTime);


        /// <summary>
        /// 获取一个值,该值指示值是否与指定的键相关联的是缓存
        /// </summary>
        /// <param name="key">健</param>
        /// <returns>结果</returns>
        bool IsSet(string key);

        /// <summary>
        /// 从缓存中删除值与指定键
        /// </summary>
        /// <param name="key">/key</param>
        void Remove(string key);

        /// <summary>
        /// 根据模式移除缓存项
        /// </summary>
        /// <param name="pattern">模式</param>
        void RemoveByPattern(string pattern);

        /// <summary>
        /// 清除所有缓存数据
        /// </summary>
        void Clear();
    }
}
