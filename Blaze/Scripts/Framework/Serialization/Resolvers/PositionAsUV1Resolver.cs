namespace Blaze.Framework.Serialization.Resolvers
{
    using System;
    using UnityEngine.UI;

    /// <summary>
    /// ���ڽ���<see cref="PositionAsUV1"/>�Ľ�������
    /// </summary>
    public class PositionAsUV1Resolver : EmptyResolver
    {
        /// <summary>
        /// ��ȡ��ΪXmlԪ������Ψһ�ַ�����
        /// </summary>
        public override string ElementName
        {
            get { return "PositionAsUV1"; }
        }

        /// <summary>
        /// ��ȡ����������Ե����͡�
        /// </summary>
        public override Type TargetType
        {
            get { return typeof (PositionAsUV1); }
        }
    }
}