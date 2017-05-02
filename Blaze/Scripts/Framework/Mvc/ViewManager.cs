namespace Blaze.Framework.Mvc
{
    using System.Collections.Generic;
    using UI;

    /// <summary>
    /// 视图管理器。
    /// </summary>
    public class ViewManager : ICaretaker
    {
        public ViewManager(UILayerGroup root)
        {
            mRoot = root;
        }

        /// <summary>
        /// 向管理器中增加一个视图。
        /// </summary>
        /// <param name="view">视图</param>
        public void AddToTop(IView view)
        {

        }

        /// <summary>
        /// 清除所有视图。
        /// </summary>
        public void Clear()
        {
        }

        /// <summary>
        /// 移除顶部视图。
        /// </summary>
        public void RemoveTop()
        {
        }

        /// <summary>
        /// 向管理器中增加一个视图，并替换掉原视图。
        /// </summary>
        /// <param name="view"></param>
        public void ReplaceTop(IView view)
        {
        }

        private readonly UILayerGroup mRoot;
        private List<IView> mViews = new List<IView>();
    }
}