namespace Blaze.Framework.UI
{
    using UnityEngine;

    /// <summary>
    /// ��<see cref="RectTransformLayer"/>Ϊ�����������������ͼ�㡣
    /// </summary>
    public class RectTransformLayer : UILayer
    {
        /// <summary>
        /// ��ȡ������һ��ֵ����ʾ��ͼ���Ƿ��ڼ���״̬��
        /// </summary>
        public override bool IsActive
        {
            get { return Root.gameObject.activeInHierarchy; }
            set { Root.gameObject.SetActive(value); }
        }

        /// <summary>
        /// ��ȡ������һ��ֵ����ʾͼ���Ƿ�ɼ���
        /// </summary>
        public override bool IsVisible
        {
            get { return Root.gameObject.activeSelf; }
            set { Root.gameObject.SetActive(value); }
        }

        /// <summary>
        /// ��ȡ������<see cref="UILayer"/>�Ĳ㼶��
        /// </summary>
        public override int Order
        {
            get { return Root.GetSiblingIndex(); }
            set { Root.SetSiblingIndex(value); }
        }

        /// <summary>
        /// ��ȡ�㼶��Ӧ��<see cref="RectTransform"/>�����
        /// </summary>
        public RectTransform Root { get; private set; }

        /// <summary>
        /// ����һ��<see cref="RectTransformLayer"/>��
        /// </summary>
        /// <param name="transform"></param>
        public RectTransformLayer(RectTransform transform)
        {
            Root = transform;
        }

        /// <summary>
        /// ��ͼ�㱻�ݻ�ʱ���ô˷�����
        /// </summary>
        protected override void OnDestroy()
        {
            if (Root != null)
            {
                Object.Destroy(Root.gameObject);
                Root = null;
            }   

            //ÿ����ģ��ر�ʱ���ֶ��ͷ���Դ�����´�ʱ������
            //Resources.UnloadUnusedAssets();
        }

        /// <summary>
        /// ����ͼ�����ڵĲ㼶������
        /// </summary>
        /// <param name="transform">�㼶����</param>
        internal void SetParent(RectTransform transform)
        {
            Root.SetParent(transform, false);
        }
    }
}