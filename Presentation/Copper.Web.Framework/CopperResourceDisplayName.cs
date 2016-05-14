using Copper.Core;
using Copper.Core.Infrastructure;
using Copper.Services.Localization;

namespace Copper.Web.Framework
{
    public class CopperResourceDisplayName : System.ComponentModel.DisplayNameAttribute
    {
        private string _resourceValue = string.Empty;

        public CopperResourceDisplayName(string resourceKey)
            : base(resourceKey)
        {
            ResourceKey = resourceKey;
        }

        public string ResourceKey { get; set; }

        public override string DisplayName
        {
            get
            {
                var langId = EngineContext.Current.Resolve<IWorkContext>().WorkingLanguage.Id;
                _resourceValue = EngineContext.Current
                    .Resolve<ILocalizationService>()
                    .GetResource(ResourceKey, langId, ResourceKey);
                return _resourceValue;
            }
        }

        public string Name
        {
            get { return "CopperResourceDisplayName"; }
        }
    }
}
