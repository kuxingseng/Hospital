namespace Blaze.Framework.Serialization.Resolvers
{
    using System.Xml;
    using UnityEngine;

    /// <summary>
    /// ���ڽ�������Ҫ�κ���������Ľ�������
    /// </summary>
    public abstract class EmptyResolver : ComponentResolver
    {
        /// <summary>
        /// ��ָ����XML�ж�ȡ�����<see cref="Component"/>���ԡ�
        /// </summary>
        /// <param name="component">Ŀ�����</param>
        /// <param name="reader">��ȡ��</param>
        /// <param name="context"></param>
        public override void Deserialize(Component component, XmlReader reader, DeserializationContext context)
        {
        }

        /// <summary>
        /// ��ָ����<see cref="Component"/>���л�ΪXML��
        /// </summary>
        /// <param name="component">ԭ���</param>
        /// <param name="writer">XML</param>
        /// <param name="context">���л�������</param>
        public override void Serialize(Component component, XmlWriter writer, SerializationContext context)
        {
        }
    }
}