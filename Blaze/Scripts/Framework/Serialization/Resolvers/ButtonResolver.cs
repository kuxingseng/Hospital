namespace Blaze.Framework.Serialization.Resolvers
{
    using System;
    using UnityEngine.UI;

    /// <summary>
    /// ���ڽ���<see cref="Button"/>�Ľ�������
    /// </summary>
    public class ButtonResolver : SelectableResolver
    {
        /// <summary>
        /// ��ȡ��ΪXmlԪ������Ψһ�ַ�����
        /// </summary>
        public override string ElementName
        {
            get { return "Button"; }
        }

        /// <summary>
        /// ��ȡ����������Ե����͡�
        /// </summary>
        public override Type TargetType
        {
            get { return typeof (Button); }
        }
    }
}