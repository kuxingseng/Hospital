namespace Blaze.Framework.Diagnostics
{
    /// <summary>
    /// 开发控制台命令抽象基类。
    /// </summary>
    public abstract class DevConsoleCommand
    {
        /// <summary>
        /// 获取命令的名称。
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// 执行命令。
        /// </summary>
        /// <param name="options">命令参数集合</param>
        public void Execute(OptionSet options)
        {
            OnExecute(options);
        }

        /// <summary>
        /// 获取命令的使用说明。
        /// </summary>
        /// <returns></returns>
        public virtual string GetUsage()
        {
            return string.Empty;
        }

        /// <summary>
        /// 当执行命令时调用此方法。
        /// </summary>
        /// <param name="options">命令参数集合</param>
        /// <returns>是否执行成功</returns>
        protected virtual bool OnExecute(OptionSet options)
        {
            return true;
        }
    }
}