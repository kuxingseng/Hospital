namespace Blaze.Framework.Serialization.Resolvers
{
    using System;
    using System.Reflection;
    using System.Xml;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// 用于解析<see cref="GraphicRaycaster"/>的解析器。
    /// </summary>
    public class GraphicRaycasterResolver : ComponentResolver
    {
        /// <summary>
        /// 获取作为Xml元素名的唯一字符串。
        /// </summary>
        public override string ElementName
        {
            get { return "GraphicRaycaster"; }
        }

        /// <summary>
        /// 获取解析器所针对的类型。
        /// </summary>
        public override Type TargetType
        {
            get { return typeof (GraphicRaycaster); }
        }

        /// <summary>
        /// 从指定的XML中读取并填充<see cref="Component"/>属性。
        /// </summary>
        /// <param name="component">目标组件</param>
        /// <param name="reader">读取器</param>
        /// <param name="context"></param>
        public override void Deserialize(Component component, XmlReader reader, DeserializationContext context)
        {
            var rayCaster = (GraphicRaycaster) component;
            rayCaster.ignoreReversedGraphics = reader.GetBoolAttribute(AttributeIgnoreReversedGraphics);
            rayCaster.blockingObjects = (GraphicRaycaster.BlockingObjects)reader.GetIntAttribute(AttributeBlockingObjects);

            //被保护的字段，只能通过反射设置
            var mask = (LayerMask) reader.GetIntAttribute(AttributeBlockingMask);
            getBlockMaskFieldInfo().SetValue(rayCaster, mask);
        }

        /// <summary>
        /// 将指定的<see cref="Component"/>序列化为XML。
        /// </summary>
        /// <param name="component">原组件</param>
        /// <param name="writer">XML</param>
        /// <param name="context">序列化上下文</param>
        public override void Serialize(Component component, XmlWriter writer, SerializationContext context)
        {
            var rayCaster = (GraphicRaycaster) component;
            writer.WriteBoolAttribute(AttributeIgnoreReversedGraphics, rayCaster.ignoreReversedGraphics);
            writer.WriteIntAttribute(AttributeBlockingObjects, (int)rayCaster.blockingObjects);

            //被保护的字段，只能通过反射获取
            var mask = (int) (LayerMask) getBlockMaskFieldInfo().GetValue(rayCaster);
            writer.WriteIntAttribute(AttributeBlockingMask, mask);
        }

        private FieldInfo getBlockMaskFieldInfo()
        {
            return TargetType.GetField("m_BlockingMask", BindingFlags.Instance | BindingFlags.NonPublic);
        }

        public const string AttributeIgnoreReversedGraphics = "IgnoreReversedGraphics";
        public const string AttributeBlockingObjects = "BlockingObjects";
        public const string AttributeBlockingMask = "BlockingMask";
    }
}