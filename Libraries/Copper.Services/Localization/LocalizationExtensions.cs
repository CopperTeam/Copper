using System;
using Copper.Core;

namespace Copper.Services.Localization
{
    public static class LocalizationExtensions
    {

        /// <summary>
        /// Get localized value of enum
        /// </summary>
        /// <typeparam name="T">Enum</typeparam>
        /// <param name="enumValue">Enum value</param>
        /// <param name="localizationService">Localization service</param>
        /// <param name="workContext">Work context</param>
        /// <returns>Localized value</returns>
        public static string GetLocalizedEnum<T>(this T enumValue, ILocalizationService localizationService, IWorkContext workContext)
            where T : struct
        {
            if (workContext == null)
                throw new ArgumentNullException(nameof(workContext));

            return GetLocalizedEnum(enumValue, localizationService, workContext.WorkingLanguage.Id);
        }
        /// <summary>
        /// Get localized value of enum
        /// </summary>
        /// <typeparam name="T">Enum</typeparam>
        /// <param name="enumValue">Enum value</param>
        /// <param name="localizationService">Localization service</param>
        /// <param name="languageId">Language identifier</param>
        /// <returns>Localized value</returns>
        public static string GetLocalizedEnum<T>(this T enumValue, ILocalizationService localizationService, int languageId)
            where T : struct
        {
            if (localizationService == null)
                throw new ArgumentNullException("localizationService");

            if (!typeof(T).IsEnum) throw new ArgumentException("T must be an enumerated type");

            //localized value
            string resourceName = string.Format("Enums.{0}.{1}",
                typeof(T).ToString(),
                //Convert.ToInt32(enumValue)
                enumValue.ToString());
            string result = localizationService.GetResource(resourceName, languageId, "");

            //set default value if required
            if (String.IsNullOrEmpty(result))
                result = CommonHelper.ConvertEnum(enumValue.ToString());

            return result;
        }
    }
}
