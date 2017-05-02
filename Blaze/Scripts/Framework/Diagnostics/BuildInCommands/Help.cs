namespace Blaze.Framework.Diagnostics.BuildInCommands
{
    /// <summary>
    /// 显示指定控制台命令的说明。
    /// </summary>
    public class Help : DevConsoleCommand
    {
        /// <summary>
        /// 获取命令的名称。
        /// </summary>
        public override string Name
        {
            get { return ".help"; }
        }

        /// <summary>
        /// 获取命令的使用说明。
        /// </summary>
        /// <returns></returns>
        public override string GetUsage()
        {
            return "Show usage of given command.";
        }

        /// <summary>
        /// 当执行命令时调用此方法。
        /// </summary>
        /// <param name="options">命令参数集合</param>
        /// <returns>是否执行成功</returns>
        protected override bool OnExecute(OptionSet options)
        {
            var cmdName = options[0];
            if (string.IsNullOrEmpty(cmdName))
                return showAllCommands();

            var cmd = DevConsole.CommandHandler.GetCommandByName(cmdName);
            if (cmd == null)
            {
                DevConsole.WriteLine("Can't find command '{0}'.", cmdName);
                return false;
            }
            DevConsole.WriteLine(cmdName + " usage:");
            DevConsole.WriteLine(cmd.GetUsage());
            return true;
        }

        private static bool showAllCommands()
        {
            DevConsole.WriteLine("===== commands =====");
            foreach (var cmd in DevConsole.CommandHandler.GetCommands())
                DevConsole.WriteLine(cmd.Name);
            DevConsole.WriteLine("==================");
            return true;
        }
    }
}