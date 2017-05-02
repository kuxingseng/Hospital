namespace Blaze.Framework.Serialization.Resolvers
{
    using System;
    using UnityEngine.UI;

    /// <summary>
    /// 用于解析<see cref="Button"/>的解析器。
    /// </summary>
    public class ButtonResolver : SelectableResolver
    {
        /// <summary>
        /// 获取作为Xml元素名的唯一字符串。
        /// </summary>
        public override string ElementName
        {
            get { return "Button"; }
        }

        /// <summary>
        /// 获取解析器所针对的类型。
        /// </summary>
        public override Type TargetType
        {
            get { return typeof (Button); }
        }
    }
}