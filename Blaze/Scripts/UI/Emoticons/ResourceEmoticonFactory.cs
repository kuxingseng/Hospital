namespace Blaze.UI.Emoticons
{
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// 从Resource目录加载表情图片的工厂实现。
    /// </summary>
    public class ResourceEmoticonFactory : IEmoticonFactory
    {
        #region IEmoticonFactory Members

        /// <summary>
        /// 创建指定标识符的表情图片。
        /// </summary>
        /// <param name="id">表情标识符</param>
        /// <returns>表情图片</returns>
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
        /// 构造一个<see cref="ResourceEmoticonFactory"/>。
        /// </summary>
        /// <param name="relativePath">表情图片所在的相对路径</param>
        public ResourceEmoticonFactory(string relativePath = null)
        {
            mRelativePath = relativePath;
        }

        private readonly string mRelativePath;
    }
}