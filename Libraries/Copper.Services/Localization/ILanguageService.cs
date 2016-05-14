using System.Collections.Generic;
using Copper.Core.Domain.Localization;

namespace Copper.Services.Localization
{
    public partial interface ILanguageService
    {
        /// <summary>
        /// Deletes a language
        /// </summary>
        /// <param name="language">Language</param>
        void DeleteLanguage(Language language);

        /// <summary>
        /// Gets all languages
        /// </summary>
        /// <returns>Language collection</returns>
        IList<Language> GetAllLanguages();
        /// <summary>
        /// Gets all languages not cache
        /// </summary>
        /// <returns>Language collection</returns>
        IList<Language> GetAllLanguagesNotCache();
        /// <summary>
        /// Gets a language
        /// </summary>
        /// <param name="languageId">Language identifier</param>
        /// <returns>Language</returns>
        Language GetLanguageById(int languageId);

        /// <summary>
        /// Inserts a language
        /// </summary>
        /// <param name="language">Language</param>
        void InsertLanguage(Language language);

        /// <summary>
        /// Updates a language
        /// </summary>
        /// <param name="language">Language</param>
        void UpdateLanguage(Language language);
    }
}
