using Copper.Core.Data;
using Copper.Core.Infrastructure;
using Copper.Services.Localization;
using Copper.Web.Framework.Localization;

namespace Copper.Web.Framework.ViewEngines.Razor
{
    public abstract class WebViewPage<TModel> : System.Web.Mvc.WebViewPage<TModel>
    {
        #region Fields
        private ILocalizationService _localizationService;
        private Localizer _localizer;

        #endregion


        public override void InitHelpers()
        {
            base.InitHelpers();
            if (DataSettingsHelper.DatabaseIsInstalled())
            {
                _localizationService = EngineContext.Current.Resolve<ILocalizationService>();
            }
        }

        /// <summary>
        /// Get a localized resources
        /// </summary>
        public Localizer T
        {
            get
            {
                if (_localizer == null)
                {
                    _localizer = (format, args) =>
                    {
                        var resFormat = _localizationService.GetResource(format);

                        if (string.IsNullOrEmpty(resFormat))
                        {
                            return new LocalizedString(format);
                        }
                        return
                            new LocalizedString((args == null || args.Length == 0)
                                ? resFormat
                                : string.Format(resFormat, args));
                    };
                }
                return _localizer;
            }
        }
    }
    public abstract class WebViewPage : WebViewPage<dynamic>
    {
    }
}