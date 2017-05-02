namespace Blaze.Framework.Serialization.Resolvers
{
    using System;
    using System.Xml;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// ���ڽ���<see cref="RawImage"/>�Ľ�������
    /// </summary>
    public class RawImageResolver : ComponentResolver
    {
        /// <summary>
        /// ��ȡ��ΪXmlԪ������Ψһ�ַ�����
        /// </summary>
        public override string ElementName
        {
            get { return "RawImage"; }
        }

        /// <summary>
        /// ��ȡ����������Ե����͡�
        /// </summary>
        public override Type TargetType
        {
            get { return typeof (RawImage); }
        }

        /// <summary>
        /// ��ָ����XML�ж�ȡ�����<see cref="Component"/>���ԡ�
        /// </summary>
        /// <param name="component">Ŀ�����</param>
        /// <param name="reader">��ȡ��</param>
        /// <param name="context"></param>
        public override void Deserialize(Component component, XmlReader reader, DeserializationContext context)
        {
            var image = (RawImage) component;
            var imageId = reader.GetAttribute(AttributeTexture);
            context.AddDependency<Texture2D>(imageId, texture => { image.texture = texture; });
            image.color = reader.GetColor32Attribute(AttributeColor);
            image.material = reader.GetMaterialElement(ElementMaterial);
            image.uvRect = reader.GetRectAttribute(AttributeRect);
        }

        /// <summary>
        /// ��ָ����<see cref="Component"/>���л�ΪXML��
        /// </summary>
        /// <param name="component">ԭ���</param>
        /// <param name="writer">XML</param>
        /// <param name="context">���л�������</param>
        public override void Serialize(Component component, XmlWriter writer, SerializationContext context)
        {
            var image = (RawImage) component;
            if (image.texture != null)
            {
                var path = context.Resolver.Serialize(image.texture);
                writer.WriteAttributeString(AttributeTexture, path);
            }
            writer.WriteColor32Attribute(AttributeColor, image.color);
#if UNITY_4_6_1
            if (image.material != Canvas.GetDefaultCanvasMaterial())
                writer.WriteMaterialElement(ElementMaterial, image.material);
#else
    //TODO:detect material
#endif
            writer.WriteRectAttribute(AttributeRect, image.uvRect);
        }

        public const string AttributeRect = "Rect";
        public const string AttributeColor = "Color";
        public const string ElementMaterial = "Material";
        public const string AttributeTexture = "Texture";
    }
}