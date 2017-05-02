namespace Blaze.Framework
{
    using System;

    /// <summary>
    /// 枚举类型标志位相关方法扩展。
    /// </summary>
    public static class EnumFlagExtension
    {
        /// <summary>
        /// 为.Net 3.5提供类似.Net 4.0版本的HasFlag方法。
        /// </summary>
        /// <param name="variable">需要被检测的枚举类型</param>
        /// <param name="flags">需要检查的标志位</param>
        /// <returns>是否包含指定标志位</returns>
        public static bool HasFlag(this Enum variable, Enum flags)
        {
            if (variable.GetType() != flags.GetType())
                throw new ArgumentException("The checked flag is not from the same type as the checked variable.");

            var num1 = Convert.ToUInt64(flags);
            var num2 = Convert.ToUInt64(variable);

            return num2 == num1;
            return (num2 & num1) == num1;
        }
    }
}