namespace Blaze.Framework.Logging
{
    /// <summary>
    /// 日志记录器接口。
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// 记录调试信息到日志。
        /// </summary>
        /// <param name="content">信息内容</param>
        void Debug(object content);

        /// <summary>
        /// 记录调试信息到日志。
        /// </summary>
        /// <param name="format">格式化文本</param>
        /// <param name="args">格式化参数</param>
        void DebugFormat(string format, params object[] args);

        /// <summary>
        /// 记录错误信息到日志。
        /// </summary>
        /// <param name="content">信息内容</param>
        void Error(object content);

        /// <summary>
        /// 记录错误信息到日志。
        /// </summary>
        /// <param name="format">格式化文本</param>
        /// <param name="args">格式化参数</param>
        void ErrorFormat(string format, params object[] args);

        /// <summary>
        /// 记录信息到日志。
        /// </summary>
        /// <param name="content">信息内容</param>
        void Info(object content);

        /// <summary>
        /// 记录信息到日志。
        /// </summary>
        /// <param name="format">格式化文本</param>
        /// <param name="args">格式化参数</param>
        void InfoFormat(string format, params object[] args);

        /// <summary>
        /// 记录警告信息到日志。
        /// </summary>
        /// <param name="content">信息内容</param>
        void Warning(object content);

        /// <summary>
        /// 记录警告信息到日志。
        /// </summary>
        /// <param name="format">格式化文本</param>
        /// <param name="args">格式化参数</param>
        void WarningFormat(string format, params object[] args);
    }
}