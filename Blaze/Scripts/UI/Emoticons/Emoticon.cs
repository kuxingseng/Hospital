namespace Blaze.UI.Emoticons
{
    using UnityEngine;
    using UnityEngine.EventSystems;

    /// <summary>
    /// ����ͼ�������
    /// </summary>
    public class Emoticon : UIBehaviour
    {
        /// <summary>
        /// ��ȡ�����<see cref="RectTransform"/>�����
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
        /// ��ȡ������ͼƬ�ĳߴ硣
        /// </summary>
        public int Size { get; set; }

        private RectTransform mRectTransform;
    }
}