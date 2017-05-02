namespace Blaze.Framework.Diagnostics
{
    /// <summary>
    /// 命令行参数配置。
    /// </summary>
    public class Option
    {
        /// <summary>
        /// 获取配置的名称。
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 获取或设置配置的值。
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 构造一个命令行参数，并指定其名称和值。
        /// </summary>
        /// <param name="name">参数名</param>
        /// <param name="value">参数值</param>
        public Option(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}