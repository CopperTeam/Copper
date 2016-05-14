using System;
using System.Linq;
using System.Web.Mvc;
using Copper.Core.Infrastructure;

namespace Copper.Web.Framework.Extensions
{
    public static class WebExtensions
    {
        ///// <summary>
        ///// 枚举转换为SelectList
        ///// </summary>
        ///// <typeparam name="TEnum">枚举类型</typeparam>
        ///// <param name="enumObj">枚举对象</param>
        ///// <param name="markCurrentAsSelected">当前值是否选中</param>
        ///// <param name="valuesToExclude">排除的值</param>
        ///// <returns></returns>
        //public static SelectList ToSelectList<TEnum>(this TEnum enumObj,
        //    bool markCurrentAsSelected = true, int[] valuesToExclude = null) where TEnum : struct
        //{
        //    if (!typeof(TEnum).IsEnum) throw new ArgumentException("An Enumeration type is required.", "enumObj");

        //    var localizationService = EngineContext.Current.Resolve<ILocalizationService>();
        //    var workContext = EngineContext.Current.Resolve<IWorkContext>();

        //    var values = from TEnum enumValue in Enum.GetValues(typeof(TEnum))
        //                 where valuesToExclude == null || !valuesToExclude.Contains(Convert.ToInt32(enumValue))
        //                 select new { ID = Convert.ToInt32(enumValue), Name = enumValue.GetLocalizedEnum(localizationService, workContext) };
        //    object selectedValue = null;
        //    if (markCurrentAsSelected)
        //        selectedValue = Convert.ToInt32(enumObj);
        //    return new SelectList(values, "ID", "Name", selectedValue);
        //}
        ///// <summary>
        ///// 转换为枚举多语言
        ///// </summary>
        ///// <typeparam name="TEnum">枚举类型</typeparam>
        ///// <param name="enumId">枚举值</param>
        ///// <returns></returns>
        //public static string ToEnumName<TEnum>(this int enumId) where TEnum : struct
        //{
        //    if (!typeof(TEnum).IsEnum) throw new ArgumentException("An Enumeration type is required.", "enumId");
        //    var localizationService = EngineContext.Current.Resolve<ILocalizationService>();
        //    var workContext = EngineContext.Current.Resolve<IWorkContext>();
        //    var names = from TEnum enumValue in Enum.GetValues(typeof(TEnum))
        //                where Convert.ToInt32(enumValue) == enumId
        //                select new { Name = enumValue.GetLocalizedEnum(localizationService, workContext) };
        //    var enumName = string.Empty;
        //    // ReSharper disable once PossibleMultipleEnumeration
        //    if (names.FirstOrDefault() != null)
        //    {
        //        // ReSharper disable once PossibleNullReferenceException
        //        enumName = names.FirstOrDefault().Name;
        //    }
        //    return enumName;
        //}
        ///// <summary>
        ///// 格式化为长日期格式“yyyy-MM-dd HH:mm:ss”
        ///// </summary>
        ///// <param name="source"></param>
        ///// <returns></returns>
        //public static string ToLongFormat(this DateTime source)
        //{
        //    return EngineContext.Current.Resolve<IDateTimeHelper>().FormatLong(source);
        //}
        ///// <summary>
        ///// 格式化为短日期格式“yyyy-MM-dd”
        ///// </summary>
        ///// <param name="source"></param>
        ///// <returns></returns>
        //public static string ToShortFormat(this DateTime source)
        //{
        //    return EngineContext.Current.Resolve<IDateTimeHelper>().FormatShort(source);
        //}

        ///// <summary>
        ///// 转换下一天的开始时间
        ///// </summary>
        ///// <param name="dt"></param>
        ///// <returns></returns>
        //public static DateTime ToNextDayBeginTime(this DateTime dt)
        //{
        //    var nextDay = dt.AddDays(1);
        //    return new DateTime(nextDay.Year, nextDay.Month, nextDay.Day);
        //}
    }
}
