namespace Blaze.Framework.UI
{
    /// <summary>
    /// ����ͳһ����<see cref="UILayer"/>�ĸ�����
    /// </summary>
    public abstract class UILayerRoot
    {
        /// <summary>
        /// �ڽ��������������<see cref="UILayerGroup"/>��
        /// </summary>
        /// <param name="order">��ʾ���ȼ�</param>
        /// <param name="name">����</param>
        /// <returns>���ɵķ���</returns>
        public abstract UILayerGroup CreateGroup(int order, string name);
    }
}