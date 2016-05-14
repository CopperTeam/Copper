using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Copper.Core.Domain.Localization;

namespace Copper.Services.Localization
{
    public class LocalizationService: ILocalizationService
    {
        #region Fields

        #endregion

        #region Ctor

        #endregion

        #region Utilities

        #endregion

        #region Methods
        public void DeleteLocaleStringResource(LocaleStringResource localeStringResource)
        {
            throw new NotImplementedException();
        }

        public LocaleStringResource GetLocaleStringResourceById(int localeStringResourceId)
        {
            throw new NotImplementedException();
        }

        public LocaleStringResource GetLocaleStringResourceByName(string resourceName, int languageId)
        {
            throw new NotImplementedException();
        }

        public IList<LocaleStringResource> GetAllResources(int languageId)
        {
            throw new NotImplementedException();
        }

        public void InsertLocaleStringResource(LocaleStringResource localeStringResource)
        {
            throw new NotImplementedException();
        }

        public void UpdateLocaleStringResource(LocaleStringResource localeStringResource)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, KeyValuePair<int, string>> GetAllResourceValues(int languageId)
        {
            throw new NotImplementedException();
        }

        public string GetResource(string resourceKey, int languageId, string defaultValue = "")
        {
            throw new NotImplementedException();
        }

        public string GetResource(string resourceKey)
        {
            throw new NotImplementedException();
        }

        public string ExportResourcesToXml(Language language)
        {
            throw new NotImplementedException();
        }

        public void ImportResourcesFromXml(Language language, string xml)
        {
            throw new NotImplementedException();
        }

        public void ImportPluginResourcesFromXml(Language language, string xml)
        {
            throw new NotImplementedException();
        }

        public void DeletePluginResourcesFromXml(string xml)
        {
            throw new NotImplementedException();
        }
        #endregion


    }
}
