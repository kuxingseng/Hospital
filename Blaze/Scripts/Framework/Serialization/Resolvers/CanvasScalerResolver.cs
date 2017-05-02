namespace Blaze.Framework.Serialization.Resolvers
{
    using System;
    using System.Xml;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// 用于解析<see cref="CanvasScaler"/>的解析器。
    /// </summary>
    public class CanvasScalerResolver : ComponentResolver
    {
        /// <summary>
        /// 获取作为Xml元素名的唯一字符串。
        /// </summary>
        public override string ElementName
        {
            get { return "CanvasScaler"; }
        }

        /// <summary>
        /// 获取解析器所针对的类型。
        /// </summary>
        public override Type TargetType
        {
            get { return typeof (CanvasScaler); }
        }

        /// <summary>
        /// 从指定的XML中读取并填充<see cref="Component"/>属性。
        /// </summary>
        /// <param name="component">目标组件</param>
        /// <param name="reader">读取器</param>
        /// <param name="context"></param>
        public override void Deserialize(Component component, XmlReader reader, DeserializationContext context)
        {
            var scaler = (CanvasScaler) component;
            scaler.uiScaleMode = (CanvasScaler.ScaleMode) reader.GetIntAttribute(AttributeUIScaleMode);
            switch (scaler.uiScaleMode)
            {
                case CanvasScaler.ScaleMode.ConstantPixelSize:
                    scaler.scaleFactor = reader.GetFloatAttribute(AttibuteScaleFactor);
                    scaler.referencePixelsPerUnit = reader.GetFloatAttribute(AttributeReferencePixelsPerUnit);
                    break;
                case CanvasScaler.ScaleMode.ScaleWithScreenSize:
                    scaler.referenceResolution = reader.GetVector2Attribute(AttributeReferenceResolution);
                    scaler.matchWidthOrHeight = reader.GetFloatAttribute(AttributeMatchWidthOrHeight);
                    scaler.referencePixelsPerUnit = reader.GetFloatAttribute(AttributeReferencePixelsPerUnit);
                    break;
                case CanvasScaler.ScaleMode.ConstantPhysicalSize:
                    scaler.physicalUnit = (CanvasScaler.Unit) reader.GetIntAttribute(AttributeReferenceResolution);
                    scaler.fallbackScreenDPI = reader.GetFloatAttribute(AttributeFallbackScreenDPI);
                    scaler.defaultSpriteDPI = reader.GetFloatAttribute(AttributeDefaultSpriteDPI);
                    break;
                default:
                    Debug.LogWarning("Unsupport scale mode " + scaler.uiScaleMode);
                    break;
            }
        }

        /// <summary>
        /// 将指定的<see cref="Component"/>序列化为XML。
        /// </summary>
        /// <param name="component">原组件</param>
        /// <param name="writer">XML</param>
        /// <param name="context">序列化上下文</param>
        public override void Serialize(Component component, XmlWriter writer, SerializationContext context)
        {
            var scaler = (CanvasScaler) component;
            writer.WriteIntAttribute(AttributeUIScaleMode, (int) scaler.uiScaleMode);
            switch (scaler.uiScaleMode)
            {
                case CanvasScaler.ScaleMode.ConstantPixelSize:
                    writer.WriteFloatAttribute(AttibuteScaleFactor, scaler.scaleFactor);
                    writer.WriteFloatAttribute(AttributeReferencePixelsPerUnit, scaler.referencePixelsPerUnit);
                    break;
                case CanvasScaler.ScaleMode.ScaleWithScreenSize:
                    writer.WriteVector2Attribute(AttributeReferenceResolution, scaler.referenceResolution);
                    writer.WriteFloatAttribute(AttributeMatchWidthOrHeight, scaler.matchWidthOrHeight);
                    writer.WriteFloatAttribute(AttributeReferencePixelsPerUnit, scaler.referencePixelsPerUnit);
                    break;
                case CanvasScaler.ScaleMode.ConstantPhysicalSize:
                    writer.WriteIntAttribute(AttributeReferenceResolution, (int) scaler.physicalUnit);
                    writer.WriteFloatAttribute(AttributeFallbackScreenDPI, scaler.fallbackScreenDPI);
                    writer.WriteFloatAttribute(AttributeDefaultSpriteDPI, scaler.defaultSpriteDPI);
                    break;
                default:
                    Debug.LogWarning("Unsupport scale mode " + scaler.uiScaleMode);
                    break;
            }
        }

        public const string AttributeUIScaleMode = "UIScaleMode";
        public const string AttibuteScaleFactor = "ScaleFactor";
        public const string AttributeReferencePixelsPerUnit = "ReferencePixelsPerUnit";
        public const string AttributeReferenceResolution = "ReferenceResolution";
        public const string AttributeMatchWidthOrHeight = "MatchWidthOrHeight";
        public const string AttributePhysicalUnit = "PhysicalUnit";
        public const string AttributeFallbackScreenDPI = "FallbackScreenDPI";
        public const string AttributeDefaultSpriteDPI = "DefaultSpriteDPI";
    }
}