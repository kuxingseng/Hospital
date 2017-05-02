namespace Blaze.Framework
{
    using System;

    /// <summary>
    /// 时间戳扩展。
    /// </summary>
    public static class TimestampExtension
    {
        /// <summary>
        /// 获取指定的<see cref="DateTime"/>所对应的时间戳。
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns>时间戳</returns>
        public static int ToTimestamp(this DateTime time)
        {
            return (int) (time - mBeginTime).TotalSeconds;
        }

        private static readonly DateTime mBeginTime = new DateTime(1970, 1, 1);
    }
}