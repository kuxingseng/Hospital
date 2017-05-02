namespace Blaze.UI.Emoticons
{
    using UnityEngine;
    using UnityEngine.EventSystems;

    /// <summary>
    /// 表情图标组件。
    /// </summary>
    public class Emoticon : UIBehaviour
    {
        /// <summary>
        /// 获取缓存的<see cref="RectTransform"/>组件。
        /// </summary>
        public RectTransform RectTransform
        {
            get
            {
                if (mRectTransform == null)
                    mRectTransform = gameObject.AddComponent<RectTransform>();
                return mRectTransform;
            }
        }

        /// <summary>
        /// 获取或设置图片的尺寸。
        /// </summary>
        public int Size { get; set; }

        private RectTransform mRectTransform;
    }
}