namespace Blaze.Framework.Serialization.Resolvers
{
    using System;
    using System.Reflection;
    using System.Xml;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// ���ڽ���<see cref="GraphicRaycaster"/>�Ľ�������
    /// </summary>
    public class GraphicRaycasterResolver : ComponentResolver
    {
        /// <summary>
        /// ��ȡ��ΪXmlԪ������Ψһ�ַ�����
        /// </summary>
        public override string ElementName
        {
            get { return "GraphicRaycaster"; }
        }

        /// <summary>
        /// ��ȡ����������Ե����͡�
        /// </summary>
        public override Type TargetType
        {
            get { return typeof (GraphicRaycaster); }
        }

        /// <summary>
        /// ��ָ����XML�ж�ȡ�����<see cref="Component"/>���ԡ�
        /// </summary>
        /// <param name="component">Ŀ�����</param>
        /// <param name="reader">��ȡ��</param>
        /// <param name="context"></param>
        public override void Deserialize(Component component, XmlReader reader, DeserializationContext context)
        {
            var rayCaster = (GraphicRaycaster) component;
            rayCaster.ignoreReversedGraphics = reader.GetBoolAttribute(AttributeIgnoreReversedGraphics);
            rayCaster.blockingObjects = (GraphicRaycaster.BlockingObjects)reader.GetIntAttribute(AttributeBlockingObjects);

            //���������ֶΣ�ֻ��ͨ����������
            var mask = (LayerMask) reader.GetIntAttribute(AttributeBlockingMask);
            getBlockMaskFieldInfo().SetValue(rayCaster, mask);
        }

        /// <summary>
        /// ��ָ����<see cref="Component"/>���л�ΪXML��
        /// </summary>
        /// <param name="component">ԭ���</param>
        /// <param name="writer">XML</param>
        /// <param name="context">���л�������</param>
        public override void Serialize(Component component, XmlWriter writer, SerializationContext context)
        {
            var rayCaster = (GraphicRaycaster) component;
            writer.WriteBoolAttribute(AttributeIgnoreReversedGraphics, rayCaster.ignoreReversedGraphics);
            writer.WriteIntAttribute(AttributeBlockingObjects, (int)rayCaster.blockingObjects);

            //���������ֶΣ�ֻ��ͨ�������ȡ
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