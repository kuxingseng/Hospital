namespace Blaze.Framework.Serialization.Resolvers
{
    using System;
    using System.Xml;
    using UnityEngine;

    /// <summary>
    /// ���ڽ���<see cref="RectTransform"/>�Ľ�������
    /// </summary>
    public class RectTransformResolver : ComponentResolver
    {
        /// <summary>
        /// ��ȡ��ΪXmlԪ������Ψһ�ַ�����
        /// </summary>
        public override string ElementName
        {
            get { return "Rect"; }
        }

        /// <summary>
        /// ��ȡ����������Ե����͡�
        /// </summary>
        public override Type TargetType
        {
            get { return typeof (RectTransform); }
        }

        /// <summary>
        /// ��ָ����XML�ж�ȡ�����<see cref="Component"/>���ԡ�
        /// </summary>
        /// <param name="component">Ŀ�����</param>
        /// <param name="reader">��ȡ��</param>
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
        /// ��ָ����<see cref="Component"/>���л�ΪXML��
        /// </summary>
        /// <param name="component">ԭ���</param>
        /// <param name="writer">XML</param>
        /// <param name="context">���л�������</param>
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