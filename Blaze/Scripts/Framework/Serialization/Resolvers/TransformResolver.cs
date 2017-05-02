namespace Blaze.Framework.Serialization.Resolvers
{
    using System;
    using System.Xml;
    using UnityEngine;

    /// <summary>
    /// 用于解析<see cref="Transform"/>的解析器。
    /// </summary>
    public class TransformResolver : ComponentResolver
    {
        /// <summary>
        /// 获取作为Xml元素名的唯一字符串。
        /// </summary>
        public override string ElementName
        {
            get { return "Transform"; }
        }

        /// <summary>
        /// 获取解析器所针对的类型。
        /// </summary>
        public override Type TargetType
        {
            get { return typeof (Transform); }
        }

        /// <summary>
        /// 从指定的XML中读取并填充<see cref="Component"/>属性。
        /// </summary>
        /// <param name="component">目标组件</param>
        /// <param name="reader">读取器</param>
        /// <param name="context"></param>
        public override void Deserialize(Component component, XmlReader reader, DeserializationContext context)
        {
            var transform = (Transform) component;
            transform.localPosition = reader.GetVector3Attribute(AttributePosition);
            transform.localRotation = Quaternion.Euler(reader.GetVector3Attribute(AttributeRotation));
            transform.localScale = reader.GetVector3Attribute(AttributeScale);
        }

        /// <summary>
        /// 将指定的<see cref="Component"/>序列化为XML。
        /// </summary>
        /// <param name="component">原组件</param>
        /// <param name="writer">XML</param>
        /// <param name="context">序列化上下文</param>
        public override void Serialize(Component component, XmlWriter writer, SerializationContext context)
        {
            var transform = (Transform) component;
            writer.WriteVector3Attribute(AttributePosition, transform.localPosition);
            writer.WriteVector3Attribute(AttributeRotation, transform.localRotation.eulerAngles);
            writer.WriteVector3Attribute(AttributeScale, transform.localScale);
        }

        public const string AttributePosition = "Position";
        public const string AttributeRotation = "Rotation";
        public const string AttributeScale = "Scale";
    }
}