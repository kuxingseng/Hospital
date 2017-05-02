namespace Blaze.Framework.Serialization.Resolvers
{
    using System;
    using System.Xml;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// ���ڽ���<see cref="Text"/>�Ľ�������
    /// </summary>
    public class TextResolver : ComponentResolver
    {
        public const string AttributeAlignment = "Alignment";
        public const string AttributeBestFit = "BestFit";
        public const string AttributeColor = "Color";
        public const string AttributeFont = "Font";
        public const string AttributeFontSize = "FontSize";
        public const string AttributeFontStyle = "FontStyle";
        public const string AttributeHorizontalOverflow = "HorizontalOverflow";
        public const string AttributeLineSpacing = "LineSpacing";
        public const string AttributeRichText = "RichText";
        public const string AttributeVerticalOverflow = "VerticalOverflow";
        public const string ElementCharacter = "Character";
        public const string ElementMaterial = "Material";
        public const string ElementParagraph = "Paragraph";
        public const string ElementText = "Text";

        /// <summary>
        /// ��ȡ��ΪXmlԪ������Ψһ�ַ�����
        /// </summary>
        public override string ElementName
        {
            get { return "Text"; }
        }

        /// <summary>
        /// ��ȡ����������Ե����͡�
        /// </summary>
        public override Type TargetType
        {
            get { return typeof (Text); }
        }

        /// <summary>
        /// ��ָ����XML�ж�ȡ�����<see cref="Component"/>���ԡ�
        /// </summary>
        /// <param name="component">Ŀ�����</param>
        /// <param name="reader">��ȡ��</param>
        /// <param name="context"></param>
        public override void Deserialize(Component component, XmlReader reader, DeserializationContext context)
        {
            var text = (Text) component;
            text.color = reader.GetColor32Attribute(AttributeColor);
            text.material = reader.GetMaterialElement(ElementMaterial);

            if (reader.ReadToDescendant(ElementText))
                text.text = reader.ReadElementString();

            //Character
            reader.ReadToDescendant(ElementCharacter);
            var font = reader.GetAttribute(AttributeFont);
            if (font != null)
                text.font = Resources.GetBuiltinResource<Font>(font + ".ttf");

            text.fontStyle = (FontStyle) reader.GetIntAttribute(AttributeFontStyle);
            text.fontSize = reader.GetIntAttribute(AttributeFontSize);
            text.lineSpacing = reader.GetFloatAttribute(AttributeLineSpacing);
            text.supportRichText = reader.GetBoolAttribute(AttributeRichText);
            reader.Skip();

            //Paragraph
            reader.ReadToDescendant(ElementParagraph);
            text.alignment = (TextAnchor) reader.GetIntAttribute(AttributeAlignment);
#if UNITY_4_6_1
            text.horizontalOverflow = (HorizontalWrapMode) reader.GetIntAttribute(AttributeHorizontalOverflow);
            text.verticalOverflow = (VerticalWrapMode) reader.GetIntAttribute(AttributeVerticalOverflow);
#endif
            text.resizeTextForBestFit = reader.GetBoolAttribute(AttributeBestFit);
            reader.Skip();
        }

        /// <summary>
        /// ��ָ����<see cref="Component"/>���л�ΪXML��
        /// </summary>
        /// <param name="component">ԭ���</param>
        /// <param name="writer">XML</param>
        /// <param name="context">���л�������</param>
        public override void Serialize(Component component, XmlWriter writer, SerializationContext context)
        {
            var text = (Text) component;
            writer.WriteColor32Attribute(AttributeColor, text.color);
            writer.WriteMaterialElement(ElementMaterial, text.material);
            writer.WriteStringElement(ElementText, text.text);

            //Character
            writer.WriteStartElement(ElementCharacter);
            if (text.font != null)
                writer.WriteAttributeString(AttributeFont, text.font.name);
            writer.WriteIntAttribute(AttributeFontStyle, (int) text.fontStyle);
            writer.WriteIntAttribute(AttributeFontSize, text.fontSize);
            writer.WriteFloatAttribute(AttributeLineSpacing, text.lineSpacing);
            writer.WriteBoolAttribute(AttributeRichText, text.supportRichText);
            writer.WriteEndElement();

            //Paragraph
            writer.WriteStartElement(ElementParagraph);
            writer.WriteIntAttribute(AttributeAlignment, (int) text.alignment);
#if UNITY_4_6_1
            writer.WriteIntAttribute(AttributeHorizontalOverflow, (int) text.horizontalOverflow, 0);
            writer.WriteIntAttribute(AttributeVerticalOverflow, (int) text.verticalOverflow, 0);
#endif
            writer.WriteBoolAttribute(AttributeBestFit, text.resizeTextForBestFit);
            writer.WriteEndElement();
        }
    }
}