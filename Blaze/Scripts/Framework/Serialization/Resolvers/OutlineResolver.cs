namespace Blaze.Framework.Serialization.Resolvers
{
    using System;
    using System.Xml;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// ���ڽ���<see cref="Shadow"/>�Ľ�������
    /// </summary>
    public class OutlineResolver : ComponentResolver
    {
        /// <summary>
        /// ��ȡ��ΪXmlԪ������Ψһ�ַ�����
        /// </summary>
        public override string ElementName
        {
            get { return "Outline"; }
        }

        /// <summary>
        /// ��ȡ����������Ե����͡�
        /// </summary>
        public override Type TargetType
        {
            get { return typeof(Outline); }
        }

        /// <summary>
        /// ��ָ����XML�ж�ȡ�����<see cref="Component"/>���ԡ�
        /// </summary>
        /// <param name="component">Ŀ�����</param>
        /// <param name="reader">��ȡ��</param>
        /// <param name="context"></param>
        public override void Deserialize(Component component, XmlReader reader, DeserializationContext context)
        {
            var outline = (Outline)component;
            outline.effectColor = reader.GetColor32Attribute(AttributeEffectColor);
            outline.effectDistance = reader.GetVector2Attribute(AttributeEffectDistance);
            outline.useGraphicAlpha = reader.GetBoolAttribute(AttributeUseGraphicAlpha);
        }

        /// <summary>
        /// ��ָ����<see cref="Component"/>���л�ΪXML��
        /// </summary>
        /// <param name="component">ԭ���</param>
        /// <param name="writer">XML</param>
        /// <param name="context">���л�������</param>
        public override void Serialize(Component component, XmlWriter writer, SerializationContext context)
        {
            var outline = (Shadow)component;
            writer.WriteColor32Attribute(AttributeEffectColor, outline.effectColor);
            writer.WriteVector2Attribute(AttributeEffectDistance, outline.effectDistance);
            writer.WriteBoolAttribute(AttributeUseGraphicAlpha, outline.useGraphicAlpha);
        }

        public const string AttributeEffectColor = "EffectColor";
        public const string AttributeEffectDistance = "EffectDistance";
        public const string AttributeUseGraphicAlpha = "UseGraphicAlhpa";
    }
}