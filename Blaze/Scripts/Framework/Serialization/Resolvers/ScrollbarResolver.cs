namespace Blaze.Framework.Serialization.Resolvers
{
    using System;
    using System.Xml;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// ���ڽ���<see cref="Scrollbar"/>�Ľ�������
    /// </summary>
    public class ScrollbarResolver : SelectableResolver
    {
        /// <summary>
        /// ��ȡ��ΪXmlԪ������Ψһ�ַ�����
        /// </summary>
        public override string ElementName
        {
            get { return "Scrollbar"; }
        }

        /// <summary>
        /// ��ȡ����������Ե����͡�
        /// </summary>
        public override Type TargetType
        {
            get { return typeof (Scrollbar); }
        }

        /// <summary>
        /// ��ָ����XML�ж�ȡ�����<see cref="Component"/>���ԡ�
        /// </summary>
        /// <param name="component">Ŀ�����</param>
        /// <param name="reader">��ȡ��</param>
        /// <param name="context">�����л�������</param>
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
        /// ��ָ����<see cref="Component"/>���л�ΪXML��
        /// </summary>
        /// <param name="component">ԭ���</param>
        /// <param name="writer">XML</param>
        /// <param name="context">���л�������</param>
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