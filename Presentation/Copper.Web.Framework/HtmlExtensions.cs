using System;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Copper.Web.Framework
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString CopperLabelFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, bool displayHint = true)
        {
            var result = new StringBuilder();
            //var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            //var hintResource = string.Empty;
            //object value;
            //if (metadata.AdditionalValues.TryGetValue("NopResourceDisplayName", out value))
            //{
            //    var resourceDisplayName = value as NopResourceDisplayName;
            //    if (resourceDisplayName != null && displayHint)
            //    {
            //        var langId = EngineContext.Current.Resolve<IWorkContext>().WorkingLanguage.Id;
            //        hintResource =
            //            EngineContext.Current.Resolve<ILocalizationService>()
            //            .GetResource(resourceDisplayName.ResourceKey + ".Hint", langId);

            //        result.Append(helper.Hint(hintResource).ToHtmlString());
            //    }
            //}
            //result.Append(helper.LabelFor(expression, new { title = hintResource }));
            return MvcHtmlString.Create(result.ToString());
        }

    }
}
