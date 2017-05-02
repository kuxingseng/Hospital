namespace Blaze.Framework.Serialization.Resolvers
{
    using System;
    using System.Xml;
    using UnityEngine;

    /// <summary>
    /// 用于解析<see cref="Canvas"/>的解析器。
    /// </summary>
    public class CanvasResolver : ComponentResolver
    {
        /// <summary>
        /// 获取作为Xml元素名的唯一字符串。
        /// </summary>
        public override string ElementName
        {
            get { return "Canvas"; }
        }

        /// <summary>
        /// 获取解析器所针对的类型。
        /// </summary>
        public override Type TargetType
        {
            get { return typeof (Canvas); }
        }

        /// <summary>
        /// 从指定的XML中读取并填充<see cref="Component"/>属性。
        /// </summary>
        /// <param name="component">目标组件</param>
        /// <param name="reader">读取器</param>
        /// <param name="context"></param>
        public override void Deserialize(Component component, XmlReader reader, DeserializationContext context)
        {
            var canvas = (Canvas) component;
            canvas.renderMode = (RenderMode) reader.GetIntAttribute(AttributeRenderMode);
            switch (canvas.renderMode)
            {
                case RenderMode.ScreenSpaceOverlay:
                    canvas.pixelPerfect = reader.GetBoolAttribute(AttributePixelPerfect);
                    canvas.sortingOrder = reader.GetIntAttribute(AttributeSortOrder);
                    break;
                case RenderMode.ScreenSpaceCamera:
                    canvas.pixelPerfect = reader.GetBoolAttribute(AttributePixelPerfect);
                    if (!string.IsNullOrEmpty(reader.GetAttribute(AttributeRenderCamera)))
                        Debug.LogWarning("Resolving render camera not implement yet.");
                    canvas.planeDistance = reader.GetFloatAttribute(AttributePlaneDistance);
                    canvas.sortingLayerID = reader.GetIntAttribute(AttributeSortLayer);
                    canvas.sortingOrder = reader.GetIntAttribute(AttributeOrderInLayer);
                    break;
                case RenderMode.WorldSpace:
                    if (!string.IsNullOrEmpty(reader.GetAttribute(AttributeEventCamera)))
                        Debug.LogWarning("Resolving event camera not implement yet.");
                    canvas.sortingLayerID = reader.GetIntAttribute(AttributeSortLayer);
                    canvas.sortingOrder = reader.GetIntAttribute(AttributeOrderInLayer);
                    break;
                default:
                    Debug.LogWarning("Resolving " + canvas.renderMode + " not implement yet.");
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
            var canvas = (Canvas) component;
            writer.WriteIntAttribute(AttributeRenderMode, (int) canvas.renderMode);
            switch (canvas.renderMode)
            {
                case RenderMode.ScreenSpaceOverlay:
                    writer.WriteBoolAttribute(AttributePixelPerfect, canvas.pixelPerfect);
                    writer.WriteIntAttribute(AttributeSortOrder, canvas.sortingOrder);
                    break;
                case RenderMode.ScreenSpaceCamera:
                    writer.WriteBoolAttribute(AttributePixelPerfect, canvas.pixelPerfect);
                    if (canvas.worldCamera != null)
                        Debug.LogWarning("Resolving render camera not implement yet.");
                    writer.WriteFloatAttribute(AttributePlaneDistance, canvas.planeDistance);
                    writer.WriteIntAttribute(AttributeSortLayer, canvas.sortingLayerID);
                    writer.WriteIntAttribute(AttributeOrderInLayer, canvas.sortingOrder);
                    break;
                case RenderMode.WorldSpace:
                    if (canvas.worldCamera != null)
                        Debug.LogWarning("Resolving event camera not implement yet.");
                    writer.WriteIntAttribute(AttributeSortLayer, canvas.sortingLayerID);
                    writer.WriteIntAttribute(AttributeOrderInLayer, canvas.sortingOrder);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public const string AttributeRenderMode = "RenderMode";
        public const string AttributePixelPerfect = "PixelPerfect";
        public const string AttributeSortOrder = "SortOrder";
        public const string AttributePlaneDistance = "PlaneDistance";
        public const string AttributeSortLayer = "SortingLayer";
        public const string AttributeOrderInLayer = "OrderInLayer";
        public const string AttributeRenderCamera = "RenderCamera";
        public const string AttributeEventCamera = "EventCamera";
    }
}