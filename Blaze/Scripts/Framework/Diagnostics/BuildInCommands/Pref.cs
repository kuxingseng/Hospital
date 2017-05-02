namespace Blaze.Framework.Diagnostics.BuildInCommands
{
    using UnityEngine;

    /// <summary>
    /// 显示<see cref="PlayerPrefs"/>
    /// </summary>
    public class Pref : DevConsoleCommand
    {
        /// <summary>
        /// 获取命令的名称。
        /// </summary>
        public override string Name
        {
            get { return ".pref"; }
        }

        /// <summary>
        /// 当执行命令时调用此方法。
        /// </summary>
        /// <param name="options">命令参数集合</param>
        /// <returns>是否执行成功</returns>
        protected override bool OnExecute(OptionSet options)
        {
            var verb = options[0];
            if (string.IsNullOrEmpty(verb))
                return false;

            switch (verb)
            {
                case "delAll":
                    return delAll();
                case "del":
                    return del(options[1]);
                case "set":
                    return setValue(options[1], options[2]);
                case "get":
                    return getValue(options[1]);
            }

            return true;
        }

        private static bool del(string key)
        {
            PlayerPrefs.DeleteKey(key);
            DevConsole.WriteLine("PlayerPrefs[{0}] deleted.", key);
            return true;
        }

        private static bool delAll()
        {
            PlayerPrefs.DeleteAll();
            DevConsole.WriteLine("PlayerPrefs all keys deleted.");
            return true;
        }

        private static bool getValue(string key)
        {
            DevConsole.WriteLine("PlayerPrefs[{0}]={1}", key, PlayerPrefs.GetString(key));
            return true;
        }

        private static bool setValue(string key, string val)
        {
            PlayerPrefs.SetString(key, val);
            DevConsole.WriteLine("Set PlayerPrefs[{0}]={1}", key, PlayerPrefs.GetString(key));
            return true;
        }
    }
}