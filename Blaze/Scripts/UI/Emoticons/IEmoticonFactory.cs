namespace Blaze.UI.Emoticons
{
    /// <summary>
    /// ����ͼƬ�����ӿڡ�
    /// </summary>
    public interface IEmoticonFactory
    {
        /// <summary>
        /// ����ָ����ʶ���ı���ͼƬ��
        /// </summary>
        /// <param name="id">�����ʶ��</param>
        /// <returns>����ͼƬ</returns>
        Emoticon Create(string id);
    }
}