namespace Copper.Core.Domain.Localization
{
    public partial class LocaleStringResource
    {
        public int Id { get; set; }
        /// <summary>
        /// 获取/设置 语言Id
        /// </summary>
        public int LanguageId { get; set; }

        /// <summary>
        /// 获取设置资源名称
        /// </summary>
        public string ResourceName { get; set; }

        /// <summary>
        /// 获取设置资源值
        /// </summary>
        public string ResourceValue { get; set; }

        /// <summary>
        /// 获取设置语言
        /// </summary>
        public virtual Language Language { get; set; }
    }
}
