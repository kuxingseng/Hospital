namespace Blaze.Framework.Logging
{
    using System;

    /// <summary>
    /// 为Blaze库提供日志记录。
    /// </summary>
    public static class BlazeLog
    {
        /// <summary>
        /// 获取或设置日志输出。
        /// </summary>
        public static ILogger Output
        {
            get { return mLogger; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");
                mLogger = value;
            }
        }

        /// <summary>
        /// 记录错误信息到日志。
        /// </summary>
        /// <param name="content">信息内容</param>
        public static void Error(object content)
        {
            Output.Error(content);
        }

        /// <summary>
        /// 记录错误信息到日志。
        /// </summary>
        /// <param name="format">格式化文本</param>
        /// <param name="args">格式化参数</param>
        public static void ErrorFormat(string format, params object[] args)
        {
            Output.ErrorFormat(format, args);
        }

        /// <summary>
        /// 记录信息到日志。
        /// </summary>
        /// <param name="content">信息内容</param>
        public static void Info(object content)
        {
            Output.Info(content);
        }

        /// <summary>
        /// 记录信息到日志。
        /// </summary>
        /// <param name="format">格式化文本</param>
        /// <param name="args">格式化参数</param>
        public static void InfoFormat(string format, params object[] args)
        {
            Output.InfoFormat(format, args);
        }

        /// <summary>
        /// 记录警告信息到日志。
        /// </summary>
        /// <param name="content">信息内容</param>
        public static void Warning(object content)
        {
            Output.Warning(content);
        }

        /// <summary>
        /// 记录警告信息到日志。
        /// </summary>
        /// <param name="format">格式化文本</param>
        /// <param name="args">格式化参数</param>
        public static void WarningFormat(string format, params object[] args)
        {
            Output.WarningFormat(format, args);
        }

#if BLAZE_DEBUG
        private static ILogger mLogger = new UnityConsoleLogger();
#else
        private static ILogger mLogger = new EmptyLogger();
#endif
    }
}