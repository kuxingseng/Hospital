namespace Blaze.Framework.Serialization.Resolvers
{
    using System;
    using System.Xml;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// 用于解析<see cref="RawImage"/>的解析器。
    /// </summary>
    public class RawImageResolver : ComponentResolver
    {
        /// <summary>
        /// 获取作为Xml元素名的唯一字符串。
        /// </summary>
        public override string ElementName
        {
            get { return "RawImage"; }
        }

        /// <summary>
        /// 获取解析器所针对的类型。
        /// </summary>
        public override Type TargetType
        {
            get { return typeof (RawImage); }
        }

        /// <summary>
        /// 从指定的XML中读取并填充<see cref="Component"/>属性。
        /// </summary>
        /// <param name="component">目标组件</param>
        /// <param name="reader">读取器</param>
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
        /// 将指定的<see cref="Component"/>序列化为XML。
        /// </summary>
        /// <param name="component">原组件</param>
        /// <param name="writer">XML</param>
        /// <param name="context">序列化上下文</param>
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