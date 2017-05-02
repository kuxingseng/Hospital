namespace Blaze.Framework.Logging
{
    /// <summary>
    /// 不输出日志。
    /// </summary>
    public class EmptyLogger : ILogger
    {
        #region ILogger Members

        /// <summary>
        /// 记录调试信息到日志。
        /// </summary>
        /// <param name="content">信息内容</param>
        public void Debug(object content)
        {
        }

        /// <summary>
        /// 记录调试信息到日志。
        /// </summary>
        /// <param name="format">格式化文本</param>
        /// <param name="args">格式化参数</param>
        public void DebugFormat(string format, params object[] args)
        {
        }

        /// <summary>
        /// 记录错误信息到日志。
        /// </summary>
        /// <param name="content">信息内容</param>
        public void Error(object content)
        {
        }

        /// <summary>
        /// 记录错误信息到日志。
        /// </summary>
        /// <param name="format">格式化文本</param>
        /// <param name="args">格式化参数</param>
        public void ErrorFormat(string format, params object[] args)
        {
        }

        /// <summary>
        /// 记录信息到日志。
        /// </summary>
        /// <param name="content">信息内容</param>
        public void Info(object content)
        {
        }

        /// <summary>
        /// 记录信息到日志。
        /// </summary>
        /// <param name="format">格式化文本</param>
        /// <param name="args">格式化参数</param>
        public void InfoFormat(string format, params object[] args)
        {
        }

        /// <summary>
        /// 记录警告信息到日志。
        /// </summary>
        /// <param name="content">信息内容</param>
        public void Warning(object content)
        {
        }

        /// <summary>
        /// 记录警告信息到日志。
        /// </summary>
        /// <param name="format">格式化文本</param>
        /// <param name="args">格式化参数</param>
        public void WarningFormat(string format, params object[] args)
        {
        }

        #endregion
    }
}