namespace Blaze.Framework.Serialization.Resolvers
{
    using System;
    using System.Xml;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// ���ڽ���<see cref="CanvasScaler"/>�Ľ�������
    /// </summary>
    public class CanvasScalerResolver : ComponentResolver
    {
        /// <summary>
        /// ��ȡ��ΪXmlԪ������Ψһ�ַ�����
        /// </summary>
        public override string ElementName
        {
            get { return "CanvasScaler"; }
        }

        /// <summary>
        /// ��ȡ����������Ե����͡�
        /// </summary>
        public override Type TargetType
        {
            get { return typeof (CanvasScaler); }
        }

        /// <summary>
        /// ��ָ����XML�ж�ȡ�����<see cref="Component"/>���ԡ�
        /// </summary>
        /// <param name="component">Ŀ�����</param>
        /// <param name="reader">��ȡ��</param>
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
        /// ��ָ����<see cref="Component"/>���л�ΪXML��
        /// </summary>
        /// <param name="component">ԭ���</param>
        /// <param name="writer">XML</param>
        /// <param name="context">���л�������</param>
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