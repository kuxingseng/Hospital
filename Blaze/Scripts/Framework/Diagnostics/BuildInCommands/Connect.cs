namespace Blaze.Framework.Diagnostics.BuildInCommands
{
    /// <summary>
    /// 连接远程控制台。
    /// </summary>
    public class Connect : DevConsoleCommand
    {
        /// <summary>
        /// 获取命令的名称。
        /// </summary>
        public override string Name
        {
            get { return ".connect"; }
        }

        /// <summary>
        /// 当执行命令时调用此方法。
        /// </summary>
        /// <param name="options">命令参数集合</param>
        /// <returns>是否执行成功</returns>
        protected override bool OnExecute(OptionSet options)
        {
            var hostArray = options[0].Split(':');
            if (hostArray.Length == 0)
            {
                DevConsole.WriteLine("Hostname required.");
                return false;
            }

            var port = 0;
            var hostname = hostArray[0];

            if (hostArray.Length > 1)
            {
                if (int.TryParse(hostArray[1], out port))
                {
                    DevConsole.WriteLine("Invalid port.");
                    return false;
                }
            }

            bool connected;
            if (port <= 0)
                connected = DevConsole.ConnectRemoteConsole(hostname);
            else
                connected = DevConsole.ConnectRemoteConsole(hostname, port);
            DevConsole.WriteLine("Connect {0} {1}.", options[0], connected ? "succeed" : "failed");
            return true;
        }
    }
}