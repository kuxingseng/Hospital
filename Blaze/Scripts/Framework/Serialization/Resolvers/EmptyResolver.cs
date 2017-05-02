namespace Blaze.Framework.Serialization.Resolvers
{
    using System.Xml;
    using UnityEngine;

    /// <summary>
    /// 用于解析不需要任何属性组件的解析器。
    /// </summary>
    public abstract class EmptyResolver : ComponentResolver
    {
        /// <summary>
        /// 从指定的XML中读取并填充<see cref="Component"/>属性。
        /// </summary>
        /// <param name="component">目标组件</param>
        /// <param name="reader">读取器</param>
        /// <param name="context"></param>
        public override void Deserialize(Component component, XmlReader reader, DeserializationContext context)
        {
        }

        /// <summary>
        /// 将指定的<see cref="Component"/>序列化为XML。
        /// </summary>
        /// <param name="component">原组件</param>
        /// <param name="writer">XML</param>
        /// <param name="context">序列化上下文</param>
        public override void Serialize(Component component, XmlWriter writer, SerializationContext context)
        {
        }
    }
}