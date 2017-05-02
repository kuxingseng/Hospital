namespace Blaze.Framework.Serialization.Resolvers
{
    using System;
    using UnityEngine.UI;

    /// <summary>
    /// 用于解析<see cref="PositionAsUV1"/>的解析器。
    /// </summary>
    public class PositionAsUV1Resolver : EmptyResolver
    {
        /// <summary>
        /// 获取作为Xml元素名的唯一字符串。
        /// </summary>
        public override string ElementName
        {
            get { return "PositionAsUV1"; }
        }

        /// <summary>
        /// 获取解析器所针对的类型。
        /// </summary>
        public override Type TargetType
        {
            get { return typeof (PositionAsUV1); }
        }
    }
}