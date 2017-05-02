namespace Blaze.Framework.Logging
{
    using System;

    /// <summary>
    /// 组合日志记录器。
    /// </summary>
    public class CompositeLogger : ILogger
    {
        #region ILogger Members

        /// <summary>
        /// 记录调试信息到日志。
        /// </summary>
        /// <param name="content">信息内容</param>
        public void Debug(object content)
        {
            run(logger => logger.Debug(content));
        }

        /// <summary>
        /// 记录调试信息到日志。
        /// </summary>
        /// <param name="format">格式化文本</param>
        /// <param name="args">格式化参数</param>
        public void DebugFormat(string format, params object[] args)
        {
            run(logger => logger.DebugFormat(format, args));
        }

        /// <summary>
        /// 记录错误信息到日志。
        /// </summary>
        /// <param name="content">信息内容</param>
        public void Error(object content)
        {
            run(logger => logger.Error(content));
        }

        /// <summary>
        /// 记录错误信息到日志。
        /// </summary>
        /// <param name="format">格式化文本</param>
        /// <param name="args">格式化参数</param>
        public void ErrorFormat(string format, params object[] args)
        {
            run(logger => logger.ErrorFormat(format, args));
        }

        /// <summary>
        /// 记录信息到日志。
        /// </summary>
        /// <param name="content">信息内容</param>
        public void Info(object content)
        {
            run(logger => logger.Info(content));
        }

        /// <summary>
        /// 记录信息到日志。
        /// </summary>
        /// <param name="format">格式化文本</param>
        /// <param name="args">格式化参数</param>
        public void InfoFormat(string format, params object[] args)
        {
            run(logger => logger.InfoFormat(format, args));
        }

        /// <summary>
        /// 记录警告信息到日志。
        /// </summary>
        /// <param name="content">信息内容</param>
        public void Warning(object content)
        {
            run(logger => logger.Warning(content));
        }

        /// <summary>
        /// 记录警告信息到日志。
        /// </summary>
        /// <param name="format">格式化文本</param>
        /// <param name="args">格式化参数</param>
        public void WarningFormat(string format, params object[] args)
        {
            run(logger => logger.WarningFormat(format, args));
        }

        #endregion

        /// <summary>
        /// 构造器。
        /// </summary>
        /// <param name="loggers">日志记录器数组</param>
        public CompositeLogger(ILogger[] loggers)
        {
            mLoggers = loggers;
        }

        private void run(Action<ILogger> action)
        {
            for (var i = 0; i < mLoggers.Length; i++)
                action(mLoggers[i]);
        }

        private readonly ILogger[] mLoggers;
    }
}