namespace Blaze.Framework.UI
{
    /// <summary>
    /// 用于统一管理<see cref="UILayer"/>的根对象。
    /// </summary>
    public abstract class UILayerRoot
    {
        /// <summary>
        /// 在界面根对象上生成<see cref="UILayerGroup"/>。
        /// </summary>
        /// <param name="order">显示优先级</param>
        /// <param name="name">名称</param>
        /// <returns>生成的分组</returns>
        public abstract UILayerGroup CreateGroup(int order, string name);
    }
}