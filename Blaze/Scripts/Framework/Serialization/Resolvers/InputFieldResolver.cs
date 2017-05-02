namespace Blaze.Framework.Serialization.Resolvers
{
    using System;
    using System.Xml;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// ���ڽ���<see cref="InputField"/>�Ľ�������
    /// </summary>
    public class InputFieldResolver : SelectableResolver
    {
        public const string AttributeCaretBlinkRate = "CaretBlinkRate";
        public const string AttributeCharacterLimit = "CharacterLimit";
        public const string AttributeCharacterValidation = "CharacterValidation";
        public const string AttributeContentType = "ContentType";
        public const string AttributeHideMobileInput = "HideMobileInput";
        public const string AttributeInputType = "InputType";
        public const string AttributeKeyboardType = "KeyboardType";
        public const string AttributeLineType = "LineType";
        public const string AttributePlaceholder = "Placeholder";
        public const string AttributeSelectionColor = "SelectionColor";
        public const string AttributeTextComponent = "TextComponent";
        public const string ElementText = "Text";

        /// <summary>
        /// ��ȡ��ΪXmlԪ������Ψһ�ַ�����
        /// </summary>
        public override string ElementName
        {
            get { return "InputField"; }
        }

        /// <summary>
        /// ��ȡ����������Ե����͡�
        /// </summary>
        public override Type TargetType
        {
            get { return typeof (InputField); }
        }

        /// <summary>
        /// ��ָ����XML�ж�ȡ�����<see cref="Component"/>���ԡ�
        /// </summary>
        /// <param name="component">Ŀ�����</param>
        /// <param name="reader">��ȡ��</param>
        /// <param name="context">�����л�������</param>
        public override void Deserialize(Component component, XmlReader reader, DeserializationContext context)
        {
            base.Deserialize(component, reader.ReadSubtree(), context);

            var inputField = (InputField) component;
            var textComponentId = reader.GetIntAttribute(AttributeTextComponent);
            if (textComponentId != 0)
                context.AddReference<Text>(textComponentId, text => { inputField.textComponent = text; });

            inputField.characterLimit = reader.GetIntAttribute(AttributeCharacterLimit);
            inputField.contentType = (InputField.ContentType) reader.GetIntAttribute(AttributeContentType);
            inputField.lineType = (InputField.LineType) reader.GetIntAttribute(AttributeLineType);
            inputField.inputType = (InputField.InputType) reader.GetIntAttribute(AttributeInputType);
            inputField.keyboardType = (TouchScreenKeyboardType) reader.GetIntAttribute(AttributeKeyboardType);
            inputField.characterValidation = (InputField.CharacterValidation) reader.GetIntAttribute(AttributeCharacterValidation);

            var placeholderId = reader.GetIntAttribute(AttributePlaceholder);
            if (placeholderId != 0)
                context.AddReference<Text>(textComponentId, placeholder => { inputField.placeholder = placeholder; });
            inputField.caretBlinkRate = reader.GetFloatAttribute(AttributeCaretBlinkRate);
            inputField.selectionColor = reader.GetColor32Attribute(AttributeSelectionColor);
            inputField.shouldHideMobileInput = reader.GetBoolAttribute(AttributeHideMobileInput);

            if (reader.ReadToDescendant(ElementText))
                inputField.text = reader.ReadElementString();
        }

        /// <summary>
        /// ��ָ����<see cref="Component"/>���л�ΪXML��
        /// </summary>
        /// <param name="component">ԭ���</param>
        /// <param name="writer">XML</param>
        /// <param name="context">���л�������</param>
        public override void Serialize(Component component, XmlWriter writer, SerializationContext context)
        {
            base.Serialize(component, writer, context);

            var inputField = (InputField) component;
            if (inputField.textComponent != null)
                writer.WriteIntAttribute(AttributeTextComponent, inputField.textComponent.GetInstanceID());
            writer.WriteIntAttribute(AttributeCharacterLimit, inputField.characterLimit);
            writer.WriteIntAttribute(AttributeContentType, (int) inputField.contentType);
            writer.WriteIntAttribute(AttributeLineType, (int) inputField.lineType);
            writer.WriteIntAttribute(AttributeInputType, (int) inputField.inputType);
            writer.WriteIntAttribute(AttributeKeyboardType, (int) inputField.keyboardType);
            writer.WriteIntAttribute(AttributeCharacterValidation, (int) inputField.characterValidation);

            if (inputField.placeholder != null)
                writer.WriteIntAttribute(AttributePlaceholder, inputField.placeholder.GetInstanceID());
            writer.WriteFloatAttribute(AttributeCaretBlinkRate, inputField.caretBlinkRate);
            writer.WriteColor32Attribute(AttributeSelectionColor, inputField.selectionColor);
            writer.WriteBoolAttribute(AttributeHideMobileInput, inputField.shouldHideMobileInput);

            if (!string.IsNullOrEmpty(inputField.text))
                writer.WriteElementString(ElementText, inputField.text);
        }
    }
}