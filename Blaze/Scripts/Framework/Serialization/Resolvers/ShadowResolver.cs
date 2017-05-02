namespace Blaze.Framework.Serialization.Resolvers
{
    using System;
    using System.Xml;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// 用于解析<see cref="Shadow"/>的解析器。
    /// </summary>
    public class ShadowResolver : ComponentResolver
    {
        /// <summary>
        /// 获取作为Xml元素名的唯一字符串。
        /// </summary>
        public override string ElementName
        {
            get { return "Shadow"; }
        }

        /// <summary>
        /// 获取解析器所针对的类型。
        /// </summary>
        public override Type TargetType
        {
            get { return typeof (Shadow); }
        }

        /// <summary>
        /// 从指定的XML中读取并填充<see cref="Component"/>属性。
        /// </summary>
        /// <param name="component">目标组件</param>
        /// <param name="reader">读取器</param>
        /// <param name="context"></param>
        public override void Deserialize(Component component, XmlReader reader, DeserializationContext context)
        {
            var shadow = (Shadow) component;
            shadow.effectColor = reader.GetColor32Attribute(AttributeEffectColor);
            shadow.effectDistance = reader.GetVector2Attribute(AttributeEffectDistance);
            shadow.useGraphicAlpha = reader.GetBoolAttribute(AttributeUseGraphicAlpha);
        }

        /// <summary>
        /// 将指定的<see cref="Component"/>序列化为XML。
        /// </summary>
        /// <param name="component">原组件</param>
        /// <param name="writer">XML</param>
        /// <param name="context">序列化上下文</param>
        public override void Serialize(Component component, XmlWriter writer, SerializationContext context)
        {
            var shadow = (Shadow) component;
            writer.WriteColor32Attribute(AttributeEffectColor, shadow.effectColor);
            writer.WriteVector2Attribute(AttributeEffectDistance, shadow.effectDistance);
            writer.WriteBoolAttribute(AttributeUseGraphicAlpha, shadow.useGraphicAlpha);
        }

        public const string AttributeEffectColor = "EffectColor";
        public const string AttributeEffectDistance = "EffectDistance";
        public const string AttributeUseGraphicAlpha = "UseGraphicAlhpa";
    }
}