namespace Blaze.Framework.Serialization.Resolvers
{
    using System;
    using System.Xml;
    using UnityEngine;

    /// <summary>
    /// ���ڽ���<see cref="Transform"/>�Ľ�������
    /// </summary>
    public class TransformResolver : ComponentResolver
    {
        /// <summary>
        /// ��ȡ��ΪXmlԪ������Ψһ�ַ�����
        /// </summary>
        public override string ElementName
        {
            get { return "Transform"; }
        }

        /// <summary>
        /// ��ȡ����������Ե����͡�
        /// </summary>
        public override Type TargetType
        {
            get { return typeof (Transform); }
        }

        /// <summary>
        /// ��ָ����XML�ж�ȡ�����<see cref="Component"/>���ԡ�
        /// </summary>
        /// <param name="component">Ŀ�����</param>
        /// <param name="reader">��ȡ��</param>
        /// <param name="context"></param>
        public override void Deserialize(Component component, XmlReader reader, DeserializationContext context)
        {
            var transform = (Transform) component;
            transform.localPosition = reader.GetVector3Attribute(AttributePosition);
            transform.localRotation = Quaternion.Euler(reader.GetVector3Attribute(AttributeRotation));
            transform.localScale = reader.GetVector3Attribute(AttributeScale);
        }

        /// <summary>
        /// ��ָ����<see cref="Component"/>���л�ΪXML��
        /// </summary>
        /// <param name="component">ԭ���</param>
        /// <param name="writer">XML</param>
        /// <param name="context">���л�������</param>
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