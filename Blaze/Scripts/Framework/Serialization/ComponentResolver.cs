namespace Blaze.Framework.Serialization
{
    using System.Xml;
    using UnityEngine;

    /// <summary>
    /// <see cref="Component"/>序列化与反序列化的解析器抽象基类。
    /// </summary>
    public abstract class ComponentResolver
    {
        /// <summary>
        /// 获取作为Xml元素名的唯一字符串。
        /// </summary>
        public abstract string ElementName { get; }

        /// <summary>
        /// 获取解析器所针对的类型。
        /// </summary>
        public abstract System.Type TargetType { get; }

        /// <summary>
        /// 从指定的XML中读取并填充<see cref="Component"/>属性。
        /// </summary>
        /// <param name="component">目标组件</param>
        /// <param name="reader">读取器</param>
        /// <param name="context">反序列化上下文</param>
        public abstract void Deserialize(Component component, XmlReader reader, DeserializationContext context);

        /// <summary>
        /// 将指定的<see cref="Component"/>序列化为XML。
        /// </summary>
        /// <param name="component">原组件</param>
        /// <param name="writer">XML</param>
        /// <param name="context">序列化上下文</param>
        public abstract void Serialize(Component component, XmlWriter writer, SerializationContext context);
    }
}