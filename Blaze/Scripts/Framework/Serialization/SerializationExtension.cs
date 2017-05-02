namespace Blaze.Framework.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.Xml;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// 序列化常用方法扩展。
    /// </summary>
    public static class SerializationExtension
    {
        public const string AttributeColorMultiplier = "ColorMultiplier";
        public const string AttributeDisabledColor = "DisabledColor";
        public const string AttributeDisabledSprite = "DisabledSprite";
        public const string AttributeDisabledTrigger = "DisableTrigger";
        public const string AttributeFadeDuration = "FadeDuration";
        public const string AttributeHighlightedColor = "HighlightedColor";
        public const string AttributeHighlightedSprite = "HighlightedSprite";
        public const string AttributeHighlightedTrigger = "HighlightedTrigger";
        public const string AttributeNavigationDown = "Down";
        public const string AttributeNavigationLeft = "Left";
        public const string AttributeNavigationMode = "Mode";
        public const string AttributeNavigationRight = "Right";
        public const string AttributeNavigationUp = "Up";
        public const string AttributeNormalColor = "NormalColor";
        public const string AttributeNormalTrigger = "NormalTrigger";
        public const string AttributePressedColor = "PressedColor";
        public const string AttributePressedSprite = "PressedSprite";
        public const string AttributePressedTrigger = "PressedTrigger";

        /// <summary>
        /// 从<see cref="XmlReader"/>中读取指定名称的<see cref="AnimationTriggers"/>元素。
        /// </summary>
        /// <param name="reader">读取器</param>
        /// <param name="name">属性名称</param>
        /// <returns>属性的值</returns>
        public static AnimationTriggers GetAnimationTriggers(this XmlReader reader, string name)
        {
            if (!reader.IsStartElement(name))
                return new AnimationTriggers();

            var ret = new AnimationTriggers
            {
                normalTrigger = reader.GetAttribute(AttributeNormalTrigger),
                highlightedTrigger = reader.GetAttribute(AttributeHighlightedTrigger),
                pressedTrigger = reader.GetAttribute(AttributePressedTrigger),
                disabledTrigger = reader.GetAttribute(AttributeDisabledTrigger),
            };
            return ret;
        }

        /// <summary>
        /// 从<see cref="XmlReader"/>中读取指定名称的<see cref="bool"/>属性。
        /// </summary>
        /// <param name="reader">读取器</param>
        /// <param name="name">属性名称</param>
        /// <param name="defaultValue">当属性不存在时返回默认值</param>
        /// <returns>属性的值</returns>
        public static bool GetBoolAttribute(this XmlReader reader, string name, bool defaultValue = false)
        {
            var attribute = reader.GetAttribute(name);
            if (attribute == null)
                return defaultValue;
            return bool.Parse(attribute);
        }

        /// <summary>
        /// 从<see cref="XmlReader"/>中读取指定名称的<see cref="Color32"/>属性。
        /// </summary>
        /// <param name="reader">读取器</param>
        /// <param name="name">属性名称</param>
        /// <returns>属性的值</returns>
        public static Color32 GetColor32Attribute(this XmlReader reader, string name)
        {
            var attribute = reader.GetAttribute(name);
            return DataConverter.GetColor32(attribute);
        }

        /// <summary>
        /// 从<see cref="XmlReader"/>中读取指定名称的<see cref="ColorBlock"/>属性。
        /// </summary>
        /// <param name="reader">读取器</param>
        /// <param name="name">属性名称</param>
        /// <returns>属性的值</returns>
        public static ColorBlock GetColorBlockElement(this XmlReader reader, string name)
        {
            if (!reader.IsStartElement(name))
                return ColorBlock.defaultColorBlock;

            var ret = new ColorBlock
            {
                normalColor = reader.GetColor32Attribute(AttributeNormalColor),
                highlightedColor = reader.GetColor32Attribute(AttributeHighlightedColor),
                pressedColor = reader.GetColor32Attribute(AttributePressedColor),
                disabledColor = reader.GetColor32Attribute(AttributeDisabledColor),
                colorMultiplier = reader.GetFloatAttribute(AttributeColorMultiplier),
                fadeDuration = reader.GetFloatAttribute(AttributeFadeDuration)
            };
            return ret;
        }

        /// <summary>
        /// 从<see cref="XmlReader"/>中读取指定名称的<see cref="float"/>属性。
        /// </summary>
        /// <param name="reader">读取器</param>
        /// <param name="name">属性名称</param>
        /// <returns>属性的值</returns>
        public static float GetFloatAttribute(this XmlReader reader, string name)
        {
            var attribute = reader.GetAttribute(name);
            return float.Parse(attribute);
        }

        /// <summary>
        /// 从<see cref="XmlReader"/>中读取游戏对象。
        /// </summary>
        /// <param name="reader">读取器</param>
        /// <param name="context">反序列化上下文</param>
        /// <param name="forceInactive">是否强制将游戏对象设为非激活状态，通常反序列化根游戏对象时需要设置此值，以防其上面的组件的Awake方法自动执行</param>
        /// <returns>游戏对象</returns>
        public static GameObject GetGameObject(this XmlReader reader, DeserializationContext context, bool forceInactive = false)
        {
            var gameObject = new GameObject
            {
                name = reader.GetAttribute("Name"),
                layer = reader.GetIntAttribute("Layer"),
            };
            if (forceInactive)
                gameObject.SetActive(false);
            var instanceId = reader.GetIntAttribute("InstanceId");
            context.RegisterReference(instanceId, gameObject);

            var subReader = reader.ReadSubtree();
            subReader.Read();
            subReader.Read();

            //Components
            if (subReader.IsStartElement("Components"))
            {
                var innerReader = reader.ReadSubtree();
                innerReader.Read();
                if (innerReader.Read())
                {
                    while (innerReader.NodeType != XmlNodeType.EndElement)
                        readComponent(innerReader, gameObject, context);
                }
                subReader.Skip();
            }

            //Children
            if (subReader.IsStartElement("Children"))
            {
                var innerReader = reader.ReadSubtree();
                innerReader.Read();
                if (innerReader.Read())
                {
                    while (innerReader.NodeType != XmlNodeType.EndElement)
                    {
                        var childObject = reader.GetGameObject(context);
                        childObject.transform.SetParent(gameObject.transform, false);
                    }
                }
                subReader.Skip();
            }
            reader.Skip();
            return gameObject;
        }

        /// <summary>
        /// 从<see cref="XmlReader"/>中读取指定名称的<see cref="int"/>属性。
        /// </summary>
        /// <param name="reader">读取器</param>
        /// <param name="name">属性名称</param>
        /// <param name="defaultValue">当属性不存在时返回默认值</param>
        /// <returns>属性的值</returns>
        public static int GetIntAttribute(this XmlReader reader, string name, int defaultValue = 0)
        {
            var attribute = reader.GetAttribute(name);
            if (attribute == null)
                return defaultValue;
            return int.Parse(attribute);
        }

        /// <summary>
        /// 从<see cref="XmlReader"/>中读取指定名称的<see cref="Material"/>属性。
        /// </summary>
        /// <param name="reader">读取器</param>
        /// <param name="name">属性名称</param>
        /// <returns>属性的值</returns>
        public static Material GetMaterialElement(this XmlReader reader, string name)
        {
            var attribute = reader.GetAttribute(name);
            if (attribute == null)
                return null;
            Debug.LogWarning("Material resolving not implement yet.");
            return null;
        }

        /// <summary>
        /// 从<see cref="XmlReader"/>中读取指定名称的<see cref="Navigation"/>属性。
        /// </summary>
        /// <param name="reader">读取器</param>
        /// <param name="name">属性名称</param>
        /// <param name="callback">成功后的回调</param>
        /// <param name="context">反序列化上下文</param>
        /// <returns>属性的值</returns>
        public static void GetNavigationElement(this XmlReader reader, string name, Action<Navigation> callback, DeserializationContext context)
        {
            if (!reader.IsStartElement(name))
            {
                callback(Navigation.defaultNavigation);
                return;
            }

            var references = new Selectable[4];

            var mode = (Navigation.Mode) reader.GetIntAttribute(AttributeNavigationMode);
            var up = reader.GetIntAttribute(AttributeNavigationUp);
            var down = reader.GetIntAttribute(AttributeNavigationDown);
            var left = reader.GetIntAttribute(AttributeNavigationLeft);
            var right = reader.GetIntAttribute(AttributeNavigationRight);

            switch (mode)
            {
                case Navigation.Mode.None:
                case Navigation.Mode.Horizontal:
                case Navigation.Mode.Vertical:
                case Navigation.Mode.Automatic:
                    break;
                case Navigation.Mode.Explicit:
                    if (up != 0)
                        context.AddReference<Selectable>(up, selectable => { references[0] = selectable; });
                    if (down != 0)
                        context.AddReference<Selectable>(down, selectable => { references[1] = selectable; });
                    if (left != 0)
                        context.AddReference<Selectable>(left, selectable => { references[2] = selectable; });
                    if (right != 0)
                        context.AddReference<Selectable>(right, selectable => { references[3] = selectable; });
                    context.AddCallback(() =>
                    {
                        var navigation = new Navigation
                        {
                            mode = mode,
                            selectOnUp = references[0],
                            selectOnDown = references[1],
                            selectOnLeft = references[2],
                            selectOnRight = references[3],
                        };
                        callback(navigation);
                    });
                    break;
                default:
                    Debug.LogWarning("Unsupported navigation mode -> " + mode);
                    break;
            }
            reader.Skip();
        }

        /// <summary>
        /// 从<see cref="XmlReader"/>中读取指定名称的<see cref="Vector3"/>属性。
        /// </summary>
        /// <param name="reader">读取器</param>
        /// <param name="name">属性名称</param>
        /// <returns>属性的值</returns>
        public static Rect GetRectAttribute(this XmlReader reader, string name)
        {
            var attribute = reader.GetAttribute(name);
            return DataConverter.GetRect(attribute);
        }

        /// <summary>
        /// 从<see cref="XmlReader"/>中读取指定名称的<see cref="SpriteState"/>属性。
        /// </summary>
        /// <param name="reader">读取器</param>
        /// <param name="name">属性名称</param>
        /// <param name="callback">获取成功后的回调</param>
        /// <param name="context">反序列化上下文</param>
        /// <returns>属性的值</returns>
        public static void GetSpriteStateElement(this XmlReader reader, string name, Action<SpriteState> callback, DeserializationContext context)
        {
            var ret = new SpriteState();
            if (!reader.IsStartElement(name))
            {
                callback(ret);
                return;
            }

            var highlightId = reader.GetAttribute(AttributeHighlightedSprite);
            var pressedId = reader.GetAttribute(AttributePressedSprite);
            var disableId = reader.GetAttribute(AttributeDisabledSprite);

            var references = new Sprite[3];

            if (highlightId != null)
                context.AddDependency<Sprite>(highlightId, sprite => { references[0] = sprite; });
            if (pressedId != null)
                context.AddDependency<Sprite>(pressedId, sprite => { references[1] = sprite; });
            if (disableId != null)
                context.AddDependency<Sprite>(disableId, sprite => { references[2] = sprite; });

            context.AddCallback(() =>
            {
                var spriteState = new SpriteState
                {
                    highlightedSprite = references[0],
                    pressedSprite = references[1],
                    disabledSprite = references[2],
                };
                callback(spriteState);
            });
        }

        /// <summary>
        /// 从<see cref="XmlReader"/>中读取指定名称的<see cref="string"/>元素。
        /// </summary>
        /// <param name="reader">读取器</param>
        /// <param name="name">属性名称</param>
        /// <returns>属性的值</returns>
        public static string GetStringElement(this XmlReader reader, string name)
        {
            var element = reader.ReadElementString(name);
            return element;
        }

        /// <summary>
        /// 从<see cref="XmlReader"/>中读取指定名称的<see cref="Vector2"/>属性。
        /// </summary>
        /// <param name="reader">读取器</param>
        /// <param name="name">属性名称</param>
        /// <returns>属性的值</returns>
        public static Vector2 GetVector2Attribute(this XmlReader reader, string name)
        {
            var attribute = reader.GetAttribute(name);
            return DataConverter.GetVector2(attribute);
        }

        /// <summary>
        /// 从<see cref="XmlReader"/>中读取指定名称的<see cref="Vector3"/>属性。
        /// </summary>
        /// <param name="reader">读取器</param>
        /// <param name="name">属性名称</param>
        /// <returns>属性的值</returns>
        public static Vector3 GetVector3Attribute(this XmlReader reader, string name)
        {
            var attribute = reader.GetAttribute(name);
            return DataConverter.GetVector3(attribute);
        }

        /// <summary>
        /// 将指定的<see cref="ComponentResolver"/>注册到转换器。
        /// 若该解析器所对应的类型已与其他解析器绑定，则会覆盖原有的绑定。
        /// </summary>
        /// <param name="resolver">解析器</param>
        public static void RegisterResolver(ComponentResolver resolver)
        {
            var targetType = resolver.TargetType;
            if (mResolverTypeBindings.ContainsKey(targetType))
            {
                mResolverTypeBindings[targetType] = resolver;
                mResolverNameBindings[resolver.ElementName] = resolver;
            }
            else
            {
                mResolverTypeBindings.Add(targetType, resolver);
                mResolverNameBindings.Add(resolver.ElementName, resolver);
            }
        }

        /// <summary>
        /// 向<see cref="XmlWriter"/>中写入指定的<see cref="AnimationTriggers"/>元素。
        /// </summary>
        /// <param name="writer">写入器</param>
        /// <param name="name">属性名称</param>
        /// <param name="value">属性的值</param>
        public static void WriteAnimationTriggers(this XmlWriter writer, string name, AnimationTriggers value)
        {
            writer.WriteStartElement(name);
            writer.WriteAttributeString(AttributeNormalTrigger, value.normalTrigger);
            writer.WriteAttributeString(AttributePressedTrigger, value.pressedTrigger);
            writer.WriteAttributeString(AttributeHighlightedTrigger, value.highlightedTrigger);
            writer.WriteAttributeString(AttributeDisabledTrigger, value.disabledTrigger);
            writer.WriteEndElement();
        }

        /// <summary>
        /// 向<see cref="XmlWriter"/>中写入指定的<see cref="bool"/>属性。
        /// </summary>
        /// <param name="writer">写入器</param>
        /// <param name="name">属性名称</param>
        /// <param name="value">属性的值</param>
        public static void WriteBoolAttribute(this XmlWriter writer, string name, bool value)
        {
            var text = value.ToString();
            writer.WriteAttributeString(name, text);
        }

        /// <summary>
        /// 向<see cref="XmlWriter"/>中写入指定的<see cref="Color32"/>属性。
        /// </summary>
        /// <param name="writer">写入器</param>
        /// <param name="name">属性名称</param>
        /// <param name="value">属性的值</param>
        public static void WriteColor32Attribute(this XmlWriter writer, string name, Color32 value)
        {
            var text = DataConverter.GetString(value);
            writer.WriteAttributeString(name, text);
        }

        /// <summary>
        /// 向<see cref="XmlWriter"/>中写入指定的<see cref="ColorBlock"/>属性。
        /// </summary>
        /// <param name="writer">写入器</param>
        /// <param name="name">属性名称</param>
        /// <param name="value">属性的值</param>
        public static void WriteColorBlockElement(this XmlWriter writer, string name, ColorBlock value)
        {
            if (areEqual(value, ColorBlock.defaultColorBlock))
                return;

            writer.WriteStartElement(name);
            writer.WriteColor32Attribute(AttributeNormalColor, value.normalColor);
            writer.WriteColor32Attribute(AttributeHighlightedColor, value.highlightedColor);
            writer.WriteColor32Attribute(AttributePressedColor, value.pressedColor);
            writer.WriteColor32Attribute(AttributeDisabledColor, value.disabledColor);
            writer.WriteFloatAttribute(AttributeColorMultiplier, value.colorMultiplier);
            writer.WriteFloatAttribute(AttributeFadeDuration, value.fadeDuration);
            writer.WriteEndElement();
        }

        /// <summary>
        /// 向<see cref="XmlWriter"/>中写入指定的<see cref="Component"/>元素。
        /// </summary>
        /// <param name="writer">写入器</param>
        /// <param name="component">需要写入的组件</param>
        /// <param name="context">序列化上下文</param>
        public static void WriteComponent(this XmlWriter writer, Component component, SerializationContext context)
        {
            var type = component.GetType();
            ComponentResolver resolver;
            if (!mResolverTypeBindings.TryGetValue(type, out resolver))
            {
                Debug.LogWarning("Unresolved component -> " + type);
                return;
            }

            writer.WriteStartElement(resolver.ElementName);
            writer.WriteIntAttribute("InstanceId", component.GetInstanceID());
            resolver.Serialize(component, writer, context);
            writer.WriteEndElement();
        }

        /// <summary>
        /// 向<see cref="XmlWriter"/>中写入指定的<see cref="float"/>属性。
        /// </summary>
        /// <param name="writer">写入器</param>
        /// <param name="name">属性名称</param>
        /// <param name="value">属性的值</param>
        public static void WriteFloatAttribute(this XmlWriter writer, string name, float value)
        {
            var text = value.ToString("f");
            writer.WriteAttributeString(name, text);
        }

        /// <summary>
        /// 向<see cref="XmlWriter"/>中写入指定的<see cref="GameObject"/>元素。
        /// </summary>
        /// <param name="writer">写入器</param>
        /// <param name="gameObject">需要写入的对象</param>
        /// <param name="context">序列化上下文</param>
        public static void WriteGameObject(this XmlWriter writer, GameObject gameObject, SerializationContext context)
        {
            if (gameObject == null)
                throw new ArgumentNullException("gameObject");

            //GameObject
            writer.WriteStartElement("GameObject");
            writer.WriteAttributeString("Name", gameObject.name);
            writer.WriteIntAttribute("Layer", gameObject.layer);
            writer.WriteIntAttribute("InstanceId", gameObject.GetInstanceID());

            //Components
            writer.WriteStartElement("Components");
            foreach (var component in gameObject.GetComponents<Component>())
                WriteComponent(writer, component, context);
            writer.WriteEndElement();

            //Children
            if (gameObject.transform.childCount > 0)
            {
                writer.WriteStartElement("Children");
                for (var i = 0; i < gameObject.transform.childCount; i++)
                {
                    var transform = gameObject.transform.GetChild(i);
                    WriteGameObject(writer, transform.gameObject, context);
                }
                writer.WriteEndElement();
            }

            writer.WriteEndElement();
        }

        /// <summary>
        /// 向<see cref="XmlWriter"/>中写入指定的<see cref="int"/>属性。
        /// </summary>
        /// <param name="writer">写入器</param>
        /// <param name="name">属性名称</param>
        /// <param name="value">属性的值</param>
        public static void WriteIntAttribute(this XmlWriter writer, string name, int value)
        {
            var text = value.ToString();
            writer.WriteAttributeString(name, text);
        }

        /// <summary>
        /// 向<see cref="XmlWriter"/>中写入指定的<see cref="int"/>属性。
        /// </summary>
        /// <param name="writer">写入器</param>
        /// <param name="name">属性名称</param>
        /// <param name="value">属性的值</param>
        /// <param name="defaultValue">当属性与默认值相同时，不写入属性</param>
        public static void WriteIntAttribute(this XmlWriter writer, string name, int value, int defaultValue)
        {
            if (value == defaultValue)
                return;
            var text = value.ToString();
            writer.WriteAttributeString(name, text);
        }

        /// <summary>
        /// 向<see cref="XmlWriter"/>中写入指定的<see cref="Material"/>元素。
        /// </summary>
        /// <param name="writer">写入器</param>
        /// <param name="name">属性名称</param>
        /// <param name="value">属性的值</param>
        public static void WriteMaterialElement(this XmlWriter writer, string name, Material value)
        {
            if (value == null)
                return;
#if UNITY_4_6_1
            if (value == Canvas.GetDefaultCanvasTextMaterial())
                return;

            writer.WriteStartElement("Shader");
            writer.WriteAttributeString("Name", value.shader.name);
            writer.WriteEndElement();
#endif
        }

        /// <summary>
        /// 向<see cref="XmlWriter"/>中写入指定的<see cref="Navigation"/>属性。
        /// </summary>
        /// <param name="writer">写入器</param>
        /// <param name="name">属性名称</param>
        /// <param name="value">属性的值</param>
        public static void WriteNavigationElement(this XmlWriter writer, string name, Navigation value)
        {
            if (areEqual(value, Navigation.defaultNavigation))
                return;
            writer.WriteStartElement(name);
            writer.WriteIntAttribute(AttributeNavigationMode, (int) value.mode);
            switch (value.mode)
            {
                case Navigation.Mode.None:
                case Navigation.Mode.Horizontal:
                case Navigation.Mode.Vertical:
                case Navigation.Mode.Automatic:
                    break;
                case Navigation.Mode.Explicit:
                    if (value.selectOnUp != null)
                        writer.WriteIntAttribute(AttributeNavigationUp, value.selectOnUp.GetInstanceID());
                    if (value.selectOnDown != null)
                        writer.WriteIntAttribute(AttributeNavigationDown, value.selectOnDown.GetInstanceID());
                    if (value.selectOnLeft != null)
                        writer.WriteIntAttribute(AttributeNavigationLeft, value.selectOnLeft.GetInstanceID());
                    if (value.selectOnRight != null)
                        writer.WriteIntAttribute(AttributeNavigationRight, value.selectOnRight.GetInstanceID());
                    break;
                default:
                    Debug.LogWarning("Unsupoorted navigation mode -> " + value.mode);
                    break;
            }
            writer.WriteEndElement();
        }

        /// <summary>
        /// 向<see cref="XmlWriter"/>中写入指定的<see cref="Rect"/>属性。
        /// </summary>
        /// <param name="writer">写入器</param>
        /// <param name="name">属性名称</param>
        /// <param name="value">属性的值</param>
        public static void WriteRectAttribute(this XmlWriter writer, string name, Rect value)
        {
            var text = DataConverter.GetString(value);
            writer.WriteAttributeString(name, text);
        }

        /// <summary>
        /// 向<see cref="XmlWriter"/>中写入指定的<see cref="SpriteState"/>属性。
        /// </summary>
        /// <param name="writer">写入器</param>
        /// <param name="name">属性名称</param>
        /// <param name="value">属性的值</param>
        /// <param name="context">序列化上下文</param>
        public static void WriteSpriteStateElement(this XmlWriter writer, string name, SpriteState value, SerializationContext context)
        {
            if (value.highlightedSprite == null
                && value.pressedSprite == null
                && value.disabledSprite)
                return;

            writer.WriteStartElement(name);
            if (value.highlightedSprite != null)
                writer.WriteAttributeString(AttributeHighlightedSprite, context.Resolver.Serialize(value.highlightedSprite));
            if (value.pressedSprite != null)
                writer.WriteAttributeString(AttributeHighlightedSprite, context.Resolver.Serialize(value.pressedSprite));
            if (value.disabledSprite != null)
                writer.WriteAttributeString(AttributeDisabledSprite, context.Resolver.Serialize(value.disabledSprite));
            writer.WriteEndElement();
        }

        /// <summary>
        /// 向<see cref="XmlWriter"/>中写入指定的<see cref="string"/>属性。
        /// </summary>
        /// <param name="writer">写入器</param>
        /// <param name="name">元素名称</param>
        /// <param name="value">元素的值</param>
        public static void WriteStringElement(this XmlWriter writer, string name, string value)
        {
            if (value == null)
                value = string.Empty;
            writer.WriteElementString(name, value);
        }

        /// <summary>
        /// 向<see cref="XmlWriter"/>中写入指定的<see cref="Vector2"/>属性。
        /// </summary>
        /// <param name="writer">写入器</param>
        /// <param name="name">属性名称</param>
        /// <param name="value">属性的值</param>
        public static void WriteVector2Attribute(this XmlWriter writer, string name, Vector2 value)
        {
            var text = DataConverter.GetString(value);
            writer.WriteAttributeString(name, text);
        }

        /// <summary>
        /// 向<see cref="XmlWriter"/>中写入指定的<see cref="Vector3"/>属性。
        /// </summary>
        /// <param name="writer">写入器</param>
        /// <param name="name">属性名称</param>
        /// <param name="value">属性的值</param>
        public static void WriteVector3Attribute(this XmlWriter writer, string name, Vector3 value)
        {
            var text = DataConverter.GetString(value);
            writer.WriteAttributeString(name, text);
        }

        private static bool areEqual(ColorBlock x, ColorBlock y)
        {
            return x.normalColor == y.normalColor
                   && x.highlightedColor == y.highlightedColor
                   && x.pressedColor == y.pressedColor
                   && x.disabledColor == y.disabledColor
                   && x.colorMultiplier == y.colorMultiplier
                   && x.fadeDuration == y.fadeDuration;
        }

        private static bool areEqual(Navigation x, Navigation y)
        {
            return x.selectOnUp == y.selectOnUp
                   && x.selectOnDown == y.selectOnDown
                   && x.selectOnLeft == y.selectOnLeft
                   && x.selectOnRight == y.selectOnRight;
        }

        private static void readComponent(XmlReader reader, GameObject gameObject, DeserializationContext context)
        {
            var element = reader.Name;
            ComponentResolver resolver;
            if (!mResolverNameBindings.TryGetValue(element, out resolver))
            {
                Debug.LogWarning("Unresolved component type name -> " + element);
                return;
            }
            Component component;
            if (resolver.TargetType == typeof (Transform))
                component = gameObject.transform;
            else
                component = gameObject.AddComponent(resolver.TargetType);

            var instanceId = reader.GetIntAttribute("InstanceId");
            context.RegisterReference(instanceId, component);

            resolver.Deserialize(component, reader, context);
            //Debug.Log(component.GetType() + " loaded.");
            reader.Skip();
        }

        private static readonly Dictionary<Type, ComponentResolver> mResolverTypeBindings = new Dictionary<Type, ComponentResolver>();
        private static readonly Dictionary<string, ComponentResolver> mResolverNameBindings = new Dictionary<string, ComponentResolver>();
    }
}