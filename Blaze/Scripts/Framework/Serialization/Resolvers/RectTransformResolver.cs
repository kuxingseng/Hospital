namespace Blaze.Framework.Serialization.Resolvers
{
    using System;
    using System.Xml;
    using UnityEngine;

    /// <summary>
    /// 用于解析<see cref="RectTransform"/>的解析器。
    /// </summary>
    public class RectTransformResolver : ComponentResolver
    {
        /// <summary>
        /// 获取作为Xml元素名的唯一字符串。
        /// </summary>
        public override string ElementName
        {
            get { return "Rect"; }
        }

        /// <summary>
        /// 获取解析器所针对的类型。
        /// </summary>
        public override Type TargetType
        {
            get { return typeof (RectTransform); }
        }

        /// <summary>
        /// 从指定的XML中读取并填充<see cref="Component"/>属性。
        /// </summary>
        /// <param name="component">目标组件</param>
        /// <param name="reader">读取器</param>
        /// <param name="context"></param>
        public override void Deserialize(Component component, XmlReader reader, DeserializationContext context)
        {
            var rect = (RectTransform) component;
            rect.localPosition = reader.GetVector3Attribute(AttributePosition);
            rect.localRotation = Quaternion.Euler(reader.GetVector3Attribute(AttributeRotation));
            rect.localScale = reader.GetVector3Attribute(AttributeScale);
            rect.anchoredPosition = reader.GetVector2Attribute(AttributeAnchorPosition);
            rect.pivot = reader.GetVector2Attribute(AttributePivot);
            rect.anchorMin = reader.GetVector2Attribute(AttributeAnchorMin);
            rect.anchorMax = reader.GetVector2Attribute(AttributeAnchorMax);
            rect.sizeDelta = reader.GetVector2Attribute(AttributeSize);
        }

        /// <summary>
        /// 将指定的<see cref="Component"/>序列化为XML。
        /// </summary>
        /// <param name="component">原组件</param>
        /// <param name="writer">XML</param>
        /// <param name="context">序列化上下文</param>
        public override void Serialize(Component component, XmlWriter writer, SerializationContext context)
        {
            var rect = (RectTransform) component;
            writer.WriteVector3Attribute(AttributePosition, rect.localPosition);
            writer.WriteVector3Attribute(AttributeRotation, rect.localRotation.eulerAngles);
            writer.WriteVector3Attribute(AttributeScale, rect.localScale);
            writer.WriteVector2Attribute(AttributeAnchorPosition, rect.anchoredPosition);
            writer.WriteVector2Attribute(AttributePivot, rect.pivot);
            writer.WriteVector2Attribute(AttributeAnchorMin, rect.anchorMin);
            writer.WriteVector2Attribute(AttributeAnchorMax, rect.anchorMax);
            writer.WriteVector2Attribute(AttributeSize, rect.sizeDelta);
        }

        public const string AttributePosition = "Position";
        public const string AttributeRotation = "Rotation";
        public const string AttributeSize = "Size";
        public const string AttributeScale = "Scale";
        public const string AttributePivot = "Pivot";
        public const string AttributeAnchorPosition = "Anchor";
        public const string AttributeAnchorMin = "AnchorMin";
        public const string AttributeAnchorMax = "AnchorMax";
    }
}