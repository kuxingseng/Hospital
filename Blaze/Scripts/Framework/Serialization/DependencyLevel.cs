namespace Blaze.Framework.Serialization
{
    /// <summary>
    /// ��Դ�����ȼ���
    /// </summary>
    public enum DependencyLevel
    {
        /// <summary>
        /// ��Դ������������������ϡ�
        /// </summary>
        Required,

        /// <summary>
        /// ���ù�ϵ����������ж��󶼼�����Ϻ�Ŵ������á�
        /// </summary>
        Reference,

        /// <summary>
        /// ��ʹ����û����ɣ�Ҳ�������ϣ��������ı����ٺ����еĲ���������ֹ��
        /// </summary>
        Decorate,
    }
}