namespace Blaze.Framework.UI
{
    using System;

    /// <summary>
    /// ���ڴ���<see cref="UILayer"/>�Ĺ�����
    /// </summary>
    public class UILayerFactory
    {
        /// <summary>
        /// ����һ��<see cref="UILayer"/>��
        /// </summary>
        /// <param name="order">ͼ�����ʾ���ȼ�</param>
        /// <param name="callback">������ɺ�Ļص�</param>
        /// <param name="args">����ͼ��Ĳ���</param>
        public virtual void Create(int order, Action<UILayer> callback, params object[] args)
        {
        }
    }
}