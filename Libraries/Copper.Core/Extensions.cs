using System;

namespace Copper.Core
{
    public static class Extensions
    {
        #region 金额相关
        /// <summary>
        /// 转为换金额格式（精确到小数点后两位）
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal ToMoney(this decimal value)
        {
            return Convert.ToDecimal(string.Format("{0:F}", value));
        }
        #endregion
    }
}
