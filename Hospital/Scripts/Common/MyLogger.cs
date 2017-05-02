namespace Muhe.Mjhx.Common
{
    using System.Diagnostics;

    /// <summary>
    /// author chenshuai
    /// $Id$
    /// Create time:2014/9/25 11:29:45 
    /// </summary>
    ///
    public static class MyLogger
    {
        /// <summary>
        /// 获取或设置一个值，表示是否启用日志输出。
        /// </summary>
        public static bool IsEnabled { get; set; }

        static MyLogger()
        {
            IsEnabled = true;
        }

        /// <summary>
        /// 记录调试日志。
        /// </summary>
        /// <param name="text"></param>
        [Conditional("DEBUG")]
        public static void Debug(string text)
        {
            if (!IsEnabled)
                return;
            UnityEngine.Debug.Log(text);
        }

        /// <summary>
        /// 记录格式化过的调试日志。
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        [Conditional("DEBUG")]
        public static void Debug(string format, params object[] args)
        {
            var text = string.Format(format, args);
            Debug(text);
        }

        /// <summary>
        /// 记录错误日志。
        /// </summary>
        /// <param name="text">错误信息</param>
        public static void Error(string text)
        {
            if (!IsEnabled)
                return;
            UnityEngine.Debug.LogError(text);
        }

        /// <summary>
        /// 记录信息日志。
        /// </summary>
        /// <param name="text">提示信息</param>
        public static void Info(string text)
        {
            if (!IsEnabled)
                return;
            UnityEngine.Debug.Log(text);
        }

        /// <summary>
        /// 记录警告日志。
        /// </summary>
        /// <param name="text">警告信息</param>
        public static void Warn(string text)
        {
            if (!IsEnabled)
                return;
            UnityEngine.Debug.LogWarning(text);
        }
    }
}
