namespace Blaze.Framework.Diagnostics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    /// <summary>
    /// 命令行处理器。
    /// </summary>
    public class CommandLineHandler
    {
        /// <summary>
        /// 构造一个命令行处理器，并指定所有需要处理的命令。
        /// </summary>
        /// <param name="commands">命令集合</param>
        public CommandLineHandler(IEnumerable<DevConsoleCommand> commands)
        {
            if (commands == null)
                throw new ArgumentNullException("commands");
            foreach (var cmd in commands)
                mCommands.Add(cmd.Name, cmd);
        }

        /// <summary>
        /// 获取指定名称的命令。
        /// </summary>
        /// <param name="name">命令名</param>
        /// <returns>命令</returns>
        public DevConsoleCommand GetCommandByName(string name)
        {
            DevConsoleCommand cmd;
            return mCommands.TryGetValue(name, out cmd) ? cmd : null;
        }

        /// <summary>
        /// 获取所有可用的控制台命令。
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DevConsoleCommand> GetCommands()
        {
            return mCommands.Values;
        }

        /// <summary>
        /// 处理输入的命令行信息。
        /// </summary>
        /// <param name="input">输入</param>
        public void Handle(string input)
        {
            if (string.IsNullOrEmpty(input))
                return;

            input = input.Trim();
            var match = mOptionRegex.Match(input);
            if (!match.Success)
                return;
            var cmdName = match.Result("${cmd}");
            DevConsoleCommand cmd;
            if (!mCommands.TryGetValue(cmdName, out cmd))
            {
                DevConsole.WriteLine("Unknown command name '{0}'.", cmdName);
                return;
            }

            var args = match.Groups["arg_name"].Captures.Cast<Capture>().Select(capture => capture.Value).ToArray();
            var optionNames = match.Groups["opt_name"].Captures.Cast<Capture>().Select(capture => capture.Value).ToArray();
            var optionValues = match.Groups["opt_value"].Captures.Cast<Capture>().Select(capture => capture.Value).ToArray();
            var optionSet = new OptionSet(args);
            for (var index = 0; index < optionNames.Length; index++)
                optionSet[optionNames[index]] = optionValues[index];

            cmd.Execute(optionSet);
        }

        private static readonly Regex mOptionRegex =
            new Regex(@"(?<cmd>[\.\w]+)\s*(?<args>(?<arg_name>([\w|\.]*)\s*)*)(?<opts>(\s*[-{1,2}|/](?<opt_name>(\w+))[ |:|=](?<opt_value>(\w+)))*)");

        private readonly Dictionary<string, DevConsoleCommand> mCommands = new Dictionary<string, DevConsoleCommand>();
    }
}