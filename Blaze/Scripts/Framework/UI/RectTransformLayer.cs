namespace Blaze.Framework.UI
{
    using UnityEngine;

    /// <summary>
    /// 以<see cref="RectTransformLayer"/>为基本组件来管理界面的图层。
    /// </summary>
    public class RectTransformLayer : UILayer
    {
        /// <summary>
        /// 获取或设置一个值，表示该图层是否在激活状态。
        /// </summary>
        public override bool IsActive
        {
            get { return Root.gameObject.activeInHierarchy; }
            set { Root.gameObject.SetActive(value); }
        }

        /// <summary>
        /// 获取或设置一个值，表示图层是否可见。
        /// </summary>
        public override bool IsVisible
        {
            get { return Root.gameObject.activeSelf; }
            set { Root.gameObject.SetActive(value); }
        }

        /// <summary>
        /// 获取或设置<see cref="UILayer"/>的层级。
        /// </summary>
        public override int Order
        {
            get { return Root.GetSiblingIndex(); }
            set { Root.SetSiblingIndex(value); }
        }

        /// <summary>
        /// 获取层级对应的<see cref="RectTransform"/>组件。
        /// </summary>
        public RectTransform Root { get; private set; }

        /// <summary>
        /// 构造一个<see cref="RectTransformLayer"/>。
        /// </summary>
        /// <param name="transform"></param>
        public RectTransformLayer(RectTransform transform)
        {
            Root = transform;
        }

        /// <summary>
        /// 当图层被摧毁时调用此方法。
        /// </summary>
        protected override void OnDestroy()
        {
            if (Root != null)
            {
                Object.Destroy(Root.gameObject);
                Root = null;
            }   

            //每次有模块关闭时，手动释放资源（重新打开时较慢）
            //Resources.UnloadUnusedAssets();
        }

        /// <summary>
        /// 设置图层所在的层级父对象。
        /// </summary>
        /// <param name="transform">层级对象</param>
        internal void SetParent(RectTransform transform)
        {
            Root.SetParent(transform, false);
        }
    }
}