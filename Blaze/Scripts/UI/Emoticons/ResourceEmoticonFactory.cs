namespace Blaze.UI.Emoticons
{
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// ��ResourceĿ¼���ر���ͼƬ�Ĺ���ʵ�֡�
    /// </summary>
    public class ResourceEmoticonFactory : IEmoticonFactory
    {
        #region IEmoticonFactory Members

        /// <summary>
        /// ����ָ����ʶ���ı���ͼƬ��
        /// </summary>
        /// <param name="id">�����ʶ��</param>
        /// <returns>����ͼƬ</returns>
        public Emoticon Create(string id)
        {
            var path = mRelativePath == null ? id : (mRelativePath + "/" + id);
            var sprite = Resources.Load<Sprite>(path);
            var imageObject = new GameObject(id);
            var image = imageObject.AddComponent<Image>();
            image.sprite = sprite;
            var ret = imageObject.AddComponent<Emoticon>();
            ret.Size = (int) sprite.rect.size.x;
            return ret;
        }

        #endregion

        /// <summary>
        /// ����һ��<see cref="ResourceEmoticonFactory"/>��
        /// </summary>
        /// <param name="relativePath">����ͼƬ���ڵ����·��</param>
        public ResourceEmoticonFactory(string relativePath = null)
        {
            mRelativePath = relativePath;
        }

        private readonly string mRelativePath;
    }
}