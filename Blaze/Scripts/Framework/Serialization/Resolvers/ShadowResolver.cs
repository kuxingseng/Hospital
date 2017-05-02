namespace Blaze.Framework.Serialization.Resolvers
{
    using System;
    using System.Xml;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// ���ڽ���<see cref="Shadow"/>�Ľ�������
    /// </summary>
    public class ShadowResolver : ComponentResolver
    {
        /// <summary>
        /// ��ȡ��ΪXmlԪ������Ψһ�ַ�����
        /// </summary>
        public override string ElementName
        {
            get { return "Shadow"; }
        }

        /// <summary>
        /// ��ȡ����������Ե����͡�
        /// </summary>
        public override Type TargetType
        {
            get { return typeof (Shadow); }
        }

        /// <summary>
        /// ��ָ����XML�ж�ȡ�����<see cref="Component"/>���ԡ�
        /// </summary>
        /// <param name="component">Ŀ�����</param>
        /// <param name="reader">��ȡ��</param>
        /// <param name="context"></param>
        public override void Deserialize(Component component, XmlReader reader, DeserializationContext context)
        {
            var shadow = (Shadow) component;
            shadow.effectColor = reader.GetColor32Attribute(AttributeEffectColor);
            shadow.effectDistance = reader.GetVector2Attribute(AttributeEffectDistance);
            shadow.useGraphicAlpha = reader.GetBoolAttribute(AttributeUseGraphicAlpha);
        }

        /// <summary>
        /// ��ָ����<see cref="Component"/>���л�ΪXML��
        /// </summary>
        /// <param name="component">ԭ���</param>
        /// <param name="writer">XML</param>
        /// <param name="context">���л�������</param>
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