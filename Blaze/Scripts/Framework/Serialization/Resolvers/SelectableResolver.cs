namespace Blaze.Framework.Serialization.Resolvers
{
    using System.Xml;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// 用于解析<see cref="Selectable"/>的解析器。
    /// </summary>
    public abstract class SelectableResolver : ComponentResolver
    {
        /// <summary>
        /// 从指定的XML中读取并填充<see cref="Component"/>属性。
        /// </summary>
        /// <param name="component">目标组件</param>
        /// <param name="reader">读取器</param>
        /// <param name="context">反序列化上下文</param>
        public override void Deserialize(Component component, XmlReader reader, DeserializationContext context)
        {
            var selectable = (Selectable) component;
            selectable.interactable = reader.GetBoolAttribute(AttributeInteractable);
            selectable.transition = (Selectable.Transition) reader.GetIntAttribute(AttributeTransition);

            switch (selectable.transition)
            {
                case Selectable.Transition.None:
                    break;
                case Selectable.Transition.ColorTint:
                    var graphicInstanceId = reader.GetIntAttribute(AttributeTargetGraphic);
                    context.AddReference<Graphic>(graphicInstanceId, graphic => { selectable.targetGraphic = graphic; });
                    var subReader = reader.ReadSubtree();
                    subReader.Read();
                    if (subReader.Read())
                        selectable.colors = subReader.GetColorBlockElement(ElementColors);
                    break;
                case Selectable.Transition.SpriteSwap:
                    var graphicInstanceId2 = reader.GetIntAttribute(AttributeTargetGraphic);
                    context.AddReference<Graphic>(graphicInstanceId2, graphic => { selectable.targetGraphic = graphic; });
                    var subReader2 = reader.ReadSubtree();
                    subReader2.Read();
                    if (subReader2.Read())
                    {
                        reader.GetSpriteStateElement(ElementSpriteState, spriteState => { selectable.spriteState = spriteState; }, context);
                        reader.Skip();
                    }
                    break;
                case Selectable.Transition.Animation:
                    var subReader3 = reader.ReadSubtree();
                    subReader3.Read();
                    if (subReader3.Read())
                    {
                        selectable.animationTriggers = reader.GetAnimationTriggers(ElementAnimationTriggers);
                        reader.Skip();
                    }
                    break;
                default:
                    Debug.LogWarning("Unsupported transition mode " + selectable.transition);
                    break;
            }
            reader.GetNavigationElement(ElementNavigation, navigation => { selectable.navigation = navigation; }, context);
        }

        /// <summary>
        /// 将指定的<see cref="Component"/>序列化为XML。
        /// </summary>
        /// <param name="component">原组件</param>
        /// <param name="writer">XML</param>
        /// <param name="context">序列化上下文</param>
        public override void Serialize(Component component, XmlWriter writer, SerializationContext context)
        {
            var selectable = (Selectable) component;
            writer.WriteBoolAttribute(AttributeInteractable, selectable.interactable);
            writer.WriteIntAttribute(AttributeTransition, (int) selectable.transition);
            switch (selectable.transition)
            {
                case Selectable.Transition.None:
                    break;
                case Selectable.Transition.ColorTint:
                    if (selectable.targetGraphic != null)
                        writer.WriteIntAttribute(AttributeTargetGraphic, selectable.targetGraphic.GetInstanceID());
                    writer.WriteColorBlockElement(ElementColors, selectable.colors);
                    break;
                case Selectable.Transition.SpriteSwap:
                    if (selectable.targetGraphic != null)
                        writer.WriteIntAttribute(AttributeTargetGraphic, selectable.targetGraphic.GetInstanceID());
                    writer.WriteSpriteStateElement(ElementSpriteState, selectable.spriteState, context);
                    break;
                case Selectable.Transition.Animation:
                    writer.WriteAnimationTriggers(ElementAnimationTriggers, selectable.animationTriggers);
                    break;
                default:
                    Debug.LogWarning("Unsupported transition mode " + selectable.transition);
                    break;
            }
            writer.WriteNavigationElement(ElementNavigation, selectable.navigation);
        }

        public const string AttributeInteractable = "Interactable";
        public const string AttributeTargetGraphic = "TargetGraphic";
        public const string AttributeTransition = "Transition";
        public const string ElementNavigation = "Navigation";
        public const string ElementColors = "Colors";
        public const string ElementSpriteState = "SpriteState";
        public const string ElementAnimationTriggers = "AnimationTriggers";
    }
}