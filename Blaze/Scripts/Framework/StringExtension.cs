namespace Blaze.Framework
{
    using System;

    /// <summary>
    /// 为<see cref="string"/>提供常用的扩展方法。
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// 剔除字符串末尾的指定字符串。
        /// </summary>
        /// <param name="str">需要进行剔除操作的文本</param>
        /// <param name="trimText">被剔除的文本</param>
        /// <param name="comparisonType">文本比较方式</param>
        /// <returns>剔除后的文本</returns>
        public static string TrimEnd(this string str, string trimText, StringComparison comparisonType = StringComparison.Ordinal)
        {
            while (true)
            {
                var index = str.LastIndexOf(trimText, comparisonType);
                if (index < 0)
                    return str;
                str = str.Substring(0, str.Length - trimText.Length);
            }
        }
    }
}