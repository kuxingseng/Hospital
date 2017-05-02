namespace Blaze.Framework.Diagnostics
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Net.Sockets;
    using UnityEngine;
    using Object = UnityEngine.Object;

    /// <summary>
    /// 表示屏幕控制台。
    /// </summary>
    public static class DevConsole
    {
        /// <summary>
        /// 获取当前使用的<see cref="CommandLineHandler"/>。
        /// </summary>
        public static CommandLineHandler CommandHandler
        {
            get
            {
                if (mCommandHandler == null)
                    initializeCommandLineHandler();
                return mCommandHandler;
            }
        }

        /// <summary>
        /// 获取或设置一个值，表示是否启用开发控制台。
        /// </summary>
        public static bool Enabled { get; set; }

        /// <summary>
        /// 获取控制台视图。
        /// </summary>
        public static DevConsoleGUI GUI
        {
            get
            {
                if (!Enabled)
                    throw new InvalidOperationException();
                if (mGUI != null)
                    return mGUI;
                var obj = new GameObject("DevConsole") {hideFlags = HideFlags.HideAndDontSave};
                Object.DontDestroyOnLoad(obj);
                mGUI = obj.AddComponent<DevConsoleGUI>();
                mGUI.Submit += onConsoleSubmit;
                return mGUI;
            }
        }

        /// <summary>
        /// 连接到远程控制台，在目标控制台上输出日志。
        /// </summary>
        /// <param name="hostname">控制台服务器地址</param>
        /// <param name="port">端口号</param>
        /// <returns>是否连接成功</returns>
        public static bool ConnectRemoteConsole(string hostname, int port = 8921)
        {
            if (mClient != null && mClient.Connected)
                throw new InvalidOperationException();
            mClient = new TcpClient();
            return mClient.Connect(hostname, port);
        }

        /// <summary>
        /// 将指定对象的文本形式显示到开发控制台。
        /// </summary>
        /// <param name="value">对象</param>
        public static void WriteLine(object value)
        {
            WriteLine(value == null ? string.Empty : value.ToString());
        }

        /// <summary>
        /// 将指定文本与一个回车换行符写到开发控制台。
        /// </summary>
        /// <param name="text"></param>
        public static void WriteLine(string text)
        {
            if (!Enabled)
                return;
            if (!prepare())
                return;
            GUI.Text += text + Environment.NewLine;
            sendToRemoteConsole(text + Environment.NewLine);
        }

        /// <summary>
        /// 使用指定的格式信息，将指定的对象（后跟当前行终止符）的文本表示写入开发控制台。
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void WriteLine(string format, params object[] args)
        {
            var text = string.Format(format, args);
            WriteLine(text);
        }

        private static void initializeCommandLineHandler()
        {
            if (mCommandHandler != null)
                return;
            var assemblies = new Assembly[] {Assembly.GetExecutingAssembly()};
            var cmdType = typeof (DevConsoleCommand);
            var query = from type in assemblies.SelectMany(assembly => assembly.GetTypes())
                where cmdType.IsAssignableFrom(type) && !type.IsAbstract
                select (DevConsoleCommand) Activator.CreateInstance(type);
            var cmds = query.ToArray();
            mCommandHandler = new CommandLineHandler(cmds);
            WriteLine("{0} commands found.", cmds.Count());
        }

        private static void onConsoleSubmit(string input)
        {
            mCommandHandler.Handle(input);
        }

        private static bool prepare()
        {
            if (GUI == null)
                return false;
            initializeCommandLineHandler();
            return true;
        }

        private static void sendToRemoteConsole(string text)
        {
            if (mClient == null || !mClient.Connected)
                return;

            using (var stream = mClient.GetStream())
            {
                var writer = new BinaryWriter(stream);
                var msg = new ConsoleMessage
                {
                    Command = "Log",
                    Content = text,
                };
                var bytes = msg.Serialize();
                writer.Write(bytes);
            }
        }

        private static TcpClient mClient;
        private static CommandLineHandler mCommandHandler;
        private static DevConsoleGUI mGUI;
    }
}