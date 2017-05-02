namespace Blaze.Framework.Serialization.Resolvers
{
    using System;
    using System.Xml;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// 用于解析<see cref="ToggleGroup"/>的解析器。
    /// </summary>
    public class ToggleGroupResolver : ComponentResolver
    {
        /// <summary>
        /// 获取作为Xml元素名的唯一字符串。
        /// </summary>
        public override string ElementName
        {
            get { return "ToggleGroup"; }
        }

        /// <summary>
        /// 获取解析器所针对的类型。
        /// </summary>
        public override Type TargetType
        {
            get { return typeof (ToggleGroup); }
        }

        /// <summary>
        /// 从指定的XML中读取并填充<see cref="Component"/>属性。
        /// </summary>
        /// <param name="component">目标组件</param>
        /// <param name="reader">读取器</param>
        /// <param name="context">反序列化上下文</param>
        public override void Deserialize(Component component, XmlReader reader, DeserializationContext context)
        {
            var toggleGroup = (ToggleGroup) component;
            toggleGroup.allowSwitchOff = reader.GetBoolAttribute(AttributeAllowSwitchOff);
        }

        /// <summary>
        /// 将指定的<see cref="Component"/>序列化为XML。
        /// </summary>
        /// <param name="component">原组件</param>
        /// <param name="writer">XML</param>
        /// <param name="context">序列化上下文</param>
        public override void Serialize(Component component, XmlWriter writer, SerializationContext context)
        {
            var toggleGroup = (ToggleGroup) component;
            writer.WriteBoolAttribute(AttributeAllowSwitchOff, toggleGroup.allowSwitchOff);
        }

        public const string AttributeAllowSwitchOff = "AllowSwtichOff";
    }
}