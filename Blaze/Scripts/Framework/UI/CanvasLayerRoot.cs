namespace Blaze.Framework.UI
{
    using UnityEngine;

    /// <summary>
    /// 基于<see cref="Canvas"/>的<see cref="UILayerRoot"/>实现。
    /// </summary>
    public class CanvasLayerRoot : UILayerRoot
    {
        /// <summary>
        /// 获取根游戏对象上的<see cref="Canvas"/>组件。
        /// </summary>
        public Canvas Canvas { get; private set; }

        /// <summary>
        /// 构造一个<see cref="CanvasLayerRoot"/>。
        /// </summary>
        /// <param name="canvas">游戏根对象上的<see cref="Canvas"/>组件</param>
        public CanvasLayerRoot(Canvas canvas)
        {
            Canvas = canvas;
            mCachedRoot = canvas.transform;
        }

        /// <summary>
        /// 在界面根对象上生成<see cref="UILayerGroup"/>。
        /// </summary>
        /// <param name="order">显示优先级</param>
        /// <param name="name">名称</param>
        /// <returns>生成的分组</returns>
        public override UILayerGroup CreateGroup(int order, string name)
        {
            var group = new RectTransformLayerGroup(mCachedRoot, name) {Order = order};
            return group;
        }

        private readonly Transform mCachedRoot;
    }
}