namespace Blaze.Framework.Serialization.Resolvers
{
    using System;
    using System.Xml;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// 用于解析<see cref="Scrollbar"/>的解析器。
    /// </summary>
    public class ScrollbarResolver : SelectableResolver
    {
        /// <summary>
        /// 获取作为Xml元素名的唯一字符串。
        /// </summary>
        public override string ElementName
        {
            get { return "Scrollbar"; }
        }

        /// <summary>
        /// 获取解析器所针对的类型。
        /// </summary>
        public override Type TargetType
        {
            get { return typeof (Scrollbar); }
        }

        /// <summary>
        /// 从指定的XML中读取并填充<see cref="Component"/>属性。
        /// </summary>
        /// <param name="component">目标组件</param>
        /// <param name="reader">读取器</param>
        /// <param name="context">反序列化上下文</param>
        public override void Deserialize(Component component, XmlReader reader, DeserializationContext context)
        {
            base.Deserialize(component, reader, context);
            var scrollbar = (Scrollbar) component;
            var handleRectId = reader.GetIntAttribute(AttributeHandleRect);
            if (handleRectId != 0)
                context.AddReference<RectTransform>(handleRectId, rect => { scrollbar.handleRect = rect; });
            scrollbar.direction = (Scrollbar.Direction) reader.GetIntAttribute(AttributeDirection);
            scrollbar.value = reader.GetFloatAttribute(AttributeValue);
            scrollbar.size = reader.GetFloatAttribute(AttrbuteSize);
            scrollbar.numberOfSteps = reader.GetIntAttribute(AttributeNumberOfSteps);
        }

        /// <summary>
        /// 将指定的<see cref="Component"/>序列化为XML。
        /// </summary>
        /// <param name="component">原组件</param>
        /// <param name="writer">XML</param>
        /// <param name="context">序列化上下文</param>
        public override void Serialize(Component component, XmlWriter writer, SerializationContext context)
        {
            base.Serialize(component, writer, context);
            var scrollbar = (Scrollbar) component;
            if (scrollbar.handleRect != null)
                writer.WriteIntAttribute(AttributeHandleRect, scrollbar.handleRect.GetInstanceID());
            writer.WriteIntAttribute(AttributeDirection, (int) scrollbar.direction);
            writer.WriteFloatAttribute(AttributeValue, scrollbar.value);
            writer.WriteFloatAttribute(AttrbuteSize, scrollbar.size);
            writer.WriteIntAttribute(AttributeNumberOfSteps, scrollbar.numberOfSteps);
        }

        public const string AttributeHandleRect = "HandleRect";
        public const string AttributeDirection = "Direction";
        public const string AttributeValue = "Value";
        public const string AttrbuteSize = "Size";
        public const string AttributeNumberOfSteps = "NumberOfSteps";
    }
}