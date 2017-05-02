namespace Blaze.Framework.Logging
{
    using System.IO;
    using System.Text;

    /// <summary>
    /// 输出日志到文件。
    /// </summary>
    public class FileLogger : ILogger
    {
        #region ILogger Members

        /// <summary>
        /// 记录调试信息到日志。
        /// </summary>
        /// <param name="content">信息内容</param>
        public void Debug(object content)
        {
            write("DEBUG", content);
        }

        /// <summary>
        /// 记录调试信息到日志。
        /// </summary>
        /// <param name="format">格式化文本</param>
        /// <param name="args">格式化参数</param>
        public void DebugFormat(string format, params object[] args)
        {
            var content = string.Format(format, args);
            Debug(content);
        }

        /// <summary>
        /// 记录错误信息到日志。
        /// </summary>
        /// <param name="content">信息内容</param>
        public void Error(object content)
        {
            write("ERROR", content);
        }

        /// <summary>
        /// 记录错误信息到日志。
        /// </summary>
        /// <param name="format">格式化文本</param>
        /// <param name="args">格式化参数</param>
        public void ErrorFormat(string format, params object[] args)
        {
            var content = string.Format(format, args);
            Error(content);
        }

        /// <summary>
        /// 记录信息到日志。
        /// </summary>
        /// <param name="content">信息内容</param>
        public void Info(object content)
        {
            write("INFO", content);
        }

        /// <summary>
        /// 记录信息到日志。
        /// </summary>
        /// <param name="format">格式化文本</param>
        /// <param name="args">格式化参数</param>
        public void InfoFormat(string format, params object[] args)
        {
            var content = string.Format(format, args);
            Info(content);
        }

        /// <summary>
        /// 记录警告信息到日志。
        /// </summary>
        /// <param name="content">信息内容</param>
        public void Warning(object content)
        {
            write("WARNING", content);
        }

        /// <summary>
        /// 记录警告信息到日志。
        /// </summary>
        /// <param name="format">格式化文本</param>
        /// <param name="args">格式化参数</param>
        public void WarningFormat(string format, params object[] args)
        {
            var content = string.Format(format, args);
            Warning(content);
        }

        #endregion

        /// <summary>
        /// 构造一个<see cref="FileLogger"/>并指定文件路径。
        /// </summary>
        /// <param name="path">日志文件路径</param>
        public FileLogger(string path)
        {
            mWriter = new StreamWriter(path, true, Encoding.UTF8);
        }

        private void write(string level, object content)
        {
            mWriter.Write("[{0}]", level);
            mWriter.WriteLine(content);
        }

        private readonly StreamWriter mWriter;
    }
}