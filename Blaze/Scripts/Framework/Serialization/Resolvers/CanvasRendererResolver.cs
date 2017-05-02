namespace Blaze.Framework.Serialization.Resolvers
{
    using System;
    using UnityEngine;

    /// <summary>
    /// ���ڽ���<see cref="CanvasRenderer"/>�Ľ�������
    /// </summary>
    public class CanvasRendererResolver : EmptyResolver
    {
        /// <summary>
        /// ��ȡ��ΪXmlԪ������Ψһ�ַ�����
        /// </summary>
        public override string ElementName
        {
            get { return "CanvasRenderer"; }
        }

        /// <summary>
        /// ��ȡ����������Ե����͡�
        /// </summary>
        public override Type TargetType
        {
            get { return typeof (CanvasRenderer); }
        }
    }
}