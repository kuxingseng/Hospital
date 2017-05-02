namespace Blaze.Framework.Serialization.Resolvers
{
    using System;
    using System.Xml;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// ���ڽ���<see cref="Slider"/>�Ľ�������
    /// </summary>
    public class SliderResolver : SelectableResolver
    {
        /// <summary>
        /// ��ȡ��ΪXmlԪ������Ψһ�ַ�����
        /// </summary>
        public override string ElementName
        {
            get { return "Slider"; }
        }

        /// <summary>
        /// ��ȡ����������Ե����͡�
        /// </summary>
        public override Type TargetType
        {
            get { return typeof (Slider); }
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
            var slider = (Slider) component;
            var fillRectId = reader.GetIntAttribute(AttributeFillRect);
            var handleRectId = reader.GetIntAttribute(AttributeHandleRect);
            if (fillRectId != 0)
                context.AddReference<RectTransform>(fillRectId, rect => { slider.fillRect = rect; });
            if (handleRectId != 0)
                context.AddReference<RectTransform>(handleRectId, rect => { slider.handleRect = rect; });
            slider.direction = (Slider.Direction) reader.GetIntAttribute(AttributeDirection);
            slider.minValue = reader.GetFloatAttribute(AttributeMinValue);
            slider.maxValue = reader.GetFloatAttribute(AttributeMaxValue);
            slider.wholeNumbers = reader.GetBoolAttribute(AttributeWholeNumbers);
            slider.value = reader.GetFloatAttribute(AttributeValue);
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
            var slider = (Slider) component;
            if (slider.fillRect != null)
                writer.WriteIntAttribute(AttributeFillRect, slider.fillRect.GetInstanceID());
            if (slider.handleRect != null)
                writer.WriteIntAttribute(AttributeHandleRect, slider.handleRect.GetInstanceID());
            writer.WriteIntAttribute(AttributeDirection, (int) slider.direction);
            writer.WriteFloatAttribute(AttributeMinValue, slider.minValue);
            writer.WriteFloatAttribute(AttributeMaxValue, slider.maxValue);
            writer.WriteBoolAttribute(AttributeWholeNumbers, slider.wholeNumbers);
            writer.WriteFloatAttribute(AttributeValue, slider.value);
        }

        public const string AttributeFillRect = "FillRect";
        public const string AttributeHandleRect = "HandleRect";
        public const string AttributeDirection = "Direction";
        public const string AttributeMinValue = "MinValue";
        public const string AttributeMaxValue = "MaxValue";
        public const string AttributeWholeNumbers = "WholeNumbers";
        public const string AttributeValue = "Value";
    }
}