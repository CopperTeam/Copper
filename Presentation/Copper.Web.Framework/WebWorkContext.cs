using System;
using System.Linq;
using System.Web;
using Copper.Core;
using Copper.Core.Domain.Customers;
using Copper.Core.Domain.Localization;
using Copper.Core.Infrastructure;
using Copper.Services.Localization;

namespace Copper.Web.Framework
{
    public partial class WebWorkContext : IWorkContext
    {
        #region Const

        private const string WorkingLanguangeCookieName = "Copper.WorkingLanguange";
        private const string WorkingThemeCookieName = "Copper.WorkingTheme";
        private const string WorkingThemeNavbarInverseCookieName = "Copper.WorkingThemeNavbarInverse";

        #endregion

        #region Fields

        private readonly HttpContextBase _httpContext;
        private readonly ILanguageService _languageService;

        #endregion

        #region Ctor
        public WebWorkContext(HttpContextBase httpContext, ILanguageService languageService)
        {
            _httpContext = httpContext;
            _languageService = languageService;
        }

        #endregion

        #region Utilities
        protected virtual void SetCookie(string cookieName, string cookieValue)
        {
            if (_httpContext != null && _httpContext.Response != null)
            {
                var cookie = new HttpCookie(cookieName) { HttpOnly = true, Value = cookieValue };
                const int cookieExpires = 24 * 365; //TODO make configurable
                cookie.Expires = DateTime.Now.AddHours(cookieExpires);

                _httpContext.Response.Cookies.Add(cookie);
            }
        }
        /// <summary>
        /// 获取当前工作语言cookie
        /// </summary>
        /// <returns></returns>
        protected virtual HttpCookie GetWorkingLanguangeCookie()
        {
            if (_httpContext == null || _httpContext.Request == null)
                return null;

            return _httpContext.Request.Cookies[WorkingLanguangeCookieName];
        }
       
        #endregion

        #region Properties

        public Customer CurrentCustomer { get; set; }

        /// <summary>
        /// 获取/设置当前显示语言
        /// </summary>
        public Language WorkingLanguage
        {
            get
            {
                var languageCookie = GetWorkingLanguangeCookie();
                var languageId = languageCookie != null ? Convert.ToInt32(languageCookie.Value) : 0;
                var language = _languageService.GetAllLanguages().FirstOrDefault(c => c.Id == languageId) ??
                               _languageService.GetAllLanguages().FirstOrDefault();
                return language;
            }
            set
            {
                var languageId = value?.Id ?? 1;
                this.SetCookie(WorkingLanguangeCookieName, languageId.ToString());
            }
        }
       
        #endregion
    }
}
