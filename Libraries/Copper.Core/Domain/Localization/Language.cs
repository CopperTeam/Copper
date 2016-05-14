namespace Copper.Core.Domain.Localization
{
    public partial class Language
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 语言类型Id
        /// </summary>
        public int LanguageTypeId { get; set; }
        /// <summary>
        /// 显示顺序
        /// </summary>
        public int DisplayOrder { get; set; }

        public LanguageType LanguageType
        {
            get
            {
                return (LanguageType)LanguageTypeId;
            }
            set
            {
                LanguageTypeId = (int)value;
            }
        }
    }
}
