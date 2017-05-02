namespace Blaze.Framework.Serialization.Resolvers
{
    using System;
    using System.Xml;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// ���ڽ���<see cref="Toggle"/>�Ľ�������
    /// </summary>
    public class ToggleResolver : SelectableResolver
    {
        /// <summary>
        /// ��ȡ��ΪXmlԪ������Ψһ�ַ�����
        /// </summary>
        public override string ElementName
        {
            get { return "Toggle"; }
        }

        /// <summary>
        /// ��ȡ����������Ե����͡�
        /// </summary>
        public override Type TargetType
        {
            get { return typeof (Toggle); }
        }

        /// <summary>
        /// ��ָ����XML�ж�ȡ�����<see cref="Component"/>���ԡ�
        /// </summary>
        /// <param name="component">Ŀ�����</param>
        /// <param name="reader">��ȡ��</param>
        /// <param name="context">�����л�������</param>
        public override void Deserialize(Component component, XmlReader reader, DeserializationContext context)
        {
            base.Deserialize(component, reader, context);
            var toggle = (Toggle) component;
            toggle.isOn = reader.GetBoolAttribute(AttributeIsOn);
            toggle.toggleTransition = (Toggle.ToggleTransition) reader.GetIntAttribute(AttributeToggleTransition);

            var graphicId = reader.GetIntAttribute(AttributeGraphic);
            var toggleGroupId = reader.GetIntAttribute(AttributeToggleGroup);

            if (graphicId != 0)
                context.AddReference<Graphic>(graphicId, graphic => { toggle.graphic = graphic; });
            if (toggleGroupId != 0)
                context.AddReference<ToggleGroup>(toggleGroupId, toggleGroup => { toggle.group = toggleGroup; });
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
            var toggle = (Toggle) component;
            writer.WriteBoolAttribute(AttributeIsOn, toggle.isOn);
            writer.WriteIntAttribute(AttributeToggleTransition, (int) toggle.toggleTransition);
            if (toggle.graphic != null)
                writer.WriteIntAttribute(AttributeGraphic, toggle.graphic.GetInstanceID());
            if (toggle.group != null)
                writer.WriteIntAttribute(AttributeToggleGroup, toggle.group.GetInstanceID());
        }

        public const string AttributeIsOn = "IsOn";
        public const string AttributeToggleTransition = "ToggleTransition";
        public const string AttributeGraphic = "Graphic";
        public const string AttributeToggleGroup = "ToggleGroup";
    }
}