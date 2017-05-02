namespace Blaze.Framework.UI
{
    using UnityEngine;

    /// <summary>
    /// ����<see cref="Canvas"/>��<see cref="UILayerRoot"/>ʵ�֡�
    /// </summary>
    public class CanvasLayerRoot : UILayerRoot
    {
        /// <summary>
        /// ��ȡ����Ϸ�����ϵ�<see cref="Canvas"/>�����
        /// </summary>
        public Canvas Canvas { get; private set; }

        /// <summary>
        /// ����һ��<see cref="CanvasLayerRoot"/>��
        /// </summary>
        /// <param name="canvas">��Ϸ�������ϵ�<see cref="Canvas"/>���</param>
        public CanvasLayerRoot(Canvas canvas)
        {
            Canvas = canvas;
            mCachedRoot = canvas.transform;
        }

        /// <summary>
        /// �ڽ��������������<see cref="UILayerGroup"/>��
        /// </summary>
        /// <param name="order">��ʾ���ȼ�</param>
        /// <param name="name">����</param>
        /// <returns>���ɵķ���</returns>
        public override UILayerGroup CreateGroup(int order, string name)
        {
            var group = new RectTransformLayerGroup(mCachedRoot, name) {Order = order};
            return group;
        }

        private readonly Transform mCachedRoot;
    }
}