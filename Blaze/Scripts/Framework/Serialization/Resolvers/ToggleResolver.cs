namespace Blaze.Framework.Serialization.Resolvers
{
    using System;
    using System.Xml;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// 用于解析<see cref="Toggle"/>的解析器。
    /// </summary>
    public class ToggleResolver : SelectableResolver
    {
        /// <summary>
        /// 获取作为Xml元素名的唯一字符串。
        /// </summary>
        public override string ElementName
        {
            get { return "Toggle"; }
        }

        /// <summary>
        /// 获取解析器所针对的类型。
        /// </summary>
        public override Type TargetType
        {
            get { return typeof (Toggle); }
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
            var toggle = (Toggle) component;
            toggle.isOn = reader.GetBoolAttribute(AttributeIsOn);
            toggle.toggleTransition = (Toggle.ToggleTransition) reader.GetIntAttribute(AttributeToggleTransition);

            var graphicId = reader.GetIntAttribute(AttributeGraphic);
            var toggleGroupId = reader.GetIntAttribute(AttributeToggleGroup);

            if (graphicId != 0)
                context.AddReference<Graphic>(graphicId, graphic => { toggle.graphic = graphic; });
            if (toggleGroupId != 0)
                context.AddReference<ToggleGroup>(toggleGroupId, toggleGroup => { toggle.group = toggleGroup; });
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
            var toggle = (Toggle) component;
            writer.WriteBoolAttribute(AttributeIsOn, toggle.isOn);
            writer.WriteIntAttribute(AttributeToggleTransition, (int) toggle.toggleTransition);
            if (toggle.graphic != null)
                writer.WriteIntAttribute(AttributeGraphic, toggle.graphic.GetInstanceID());
            if (toggle.group != null)
                writer.WriteIntAttribute(AttributeToggleGroup, toggle.group.GetInstanceID());
        }

        public const string AttributeIsOn = "IsOn";
        public const string AttributeToggleTransition = "ToggleTransition";
        public const string AttributeGraphic = "Graphic";
        public const string AttributeToggleGroup = "ToggleGroup";
    }
}