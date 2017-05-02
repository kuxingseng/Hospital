namespace Blaze.Framework.Serialization.Resolvers
{
    using System;
    using System.Xml;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// 用于解析<see cref="Image"/>的解析器。
    /// </summary>
    public class ImageResolver : ComponentResolver
    {
        /// <summary>
        /// 获取作为Xml元素名的唯一字符串。
        /// </summary>
        public override string ElementName
        {
            get { return "Image"; }
        }

        /// <summary>
        /// 获取解析器所针对的类型。
        /// </summary>
        public override Type TargetType
        {
            get { return typeof (Image); }
        }

        /// <summary>
        /// 从指定的XML中读取并填充<see cref="Component"/>属性。
        /// </summary>
        /// <param name="component">目标组件</param>
        /// <param name="reader">读取器</param>
        /// <param name="context"></param>
        public override void Deserialize(Component component, XmlReader reader, DeserializationContext context)
        {
            var image = (Image) component;
            var spriteId = reader.GetAttribute(AttributeSourceImage);
            context.AddDependency<Sprite>(spriteId, sprite => { image.sprite = sprite; });
            image.color = reader.GetColor32Attribute(AttributeColor);
            image.material = reader.GetMaterialElement(ElementMaterial);
            image.type = (Image.Type) reader.GetIntAttribute(AttributeImageType);
            switch (image.type)
            {
                case Image.Type.Simple:
                    reader.GetBoolAttribute(AttributePreserveAspect, image.preserveAspect);
                    break;
                case Image.Type.Sliced:
                    reader.GetBoolAttribute(AttributeFillCenter, image.fillCenter);
                    break;
                case Image.Type.Tiled:
                    reader.GetBoolAttribute(AttributeFillCenter, image.fillCenter);
                    break;
                case Image.Type.Filled:
                    image.fillMethod = (Image.FillMethod) reader.GetIntAttribute(AttributeFillCenter);
                    image.fillOrigin = reader.GetIntAttribute(AttributeFillOrigin);
                    image.fillAmount = reader.GetFloatAttribute(AttributeFillAmount);
                    image.fillClockwise = reader.GetBoolAttribute(AttributeClockwise);
                    image.preserveAspect = reader.GetBoolAttribute(AttributePreserveAspect);
                    break;
                default:
                    Debug.LogWarning("Unsupported image type -> " + image.type);
                    break;
            }

            image.preserveAspect = reader.GetBoolAttribute(AttributePreserveAspect);
        }

        /// <summary>
        /// 将指定的<see cref="Component"/>序列化为XML。
        /// </summary>
        /// <param name="component">原组件</param>
        /// <param name="writer">XML</param>
        /// <param name="context">序列化上下文</param>
        public override void Serialize(Component component, XmlWriter writer, SerializationContext context)
        {
            var image = (Image) component;
            if (image.sprite != null)
            {
                var path = context.Resolver.Serialize(image.sprite);
                writer.WriteAttributeString(AttributeSourceImage, path);
            }
            writer.WriteColor32Attribute(AttributeColor, image.color);
#if UNITY_4_6_1
            if (image.material != Canvas.GetDefaultCanvasMaterial())
                writer.WriteMaterialElement(ElementMaterial, image.material);
#else
    //TODO:detect material
#endif
            writer.WriteIntAttribute(AttributeImageType, (int) image.type);
            switch (image.type)
            {
                case Image.Type.Simple:
                    writer.WriteBoolAttribute(AttributePreserveAspect, image.preserveAspect);
                    break;
                case Image.Type.Sliced:
                    writer.WriteBoolAttribute(AttributeFillCenter, image.fillCenter);
                    break;
                case Image.Type.Tiled:
                    writer.WriteBoolAttribute(AttributeFillCenter, image.fillCenter);
                    break;
                case Image.Type.Filled:
                    writer.WriteIntAttribute(AttributeFillCenter, (int) image.fillMethod);
                    writer.WriteIntAttribute(AttributeFillOrigin, image.fillOrigin);
                    writer.WriteFloatAttribute(AttributeFillAmount, image.fillAmount);
                    writer.WriteBoolAttribute(AttributeClockwise, image.fillClockwise);
                    writer.WriteBoolAttribute(AttributePreserveAspect, image.preserveAspect);
                    break;
                default:
                    Debug.LogWarning("Unsupported image type -> " + image.type);
                    break;
            }
        }

        public const string AttributeColor = "Color";
        public const string ElementMaterial = "Material";
        public const string AttributePreserveAspect = "PreserveAspect";
        public const string AttributeFillCenter = "FillCenter";
        public const string AttributeSourceImage = "SourceImage";
        public const string AttributeImageType = "ImageType";
        public const string AttributeFillAmount = "FillAmount";
        public const string AttributeFillMethod = "FillMethod";
        public const string AttributeFillOrigin = "FillOrigin";
        public const string AttributeClockwise = "Clockwise";
    }
}