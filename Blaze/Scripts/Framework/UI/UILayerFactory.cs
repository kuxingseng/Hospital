namespace Blaze.Framework.UI
{
    using System;

    /// <summary>
    /// 用于创建<see cref="UILayer"/>的工厂。
    /// </summary>
    public class UILayerFactory
    {
        /// <summary>
        /// 创建一个<see cref="UILayer"/>。
        /// </summary>
        /// <param name="order">图层的显示优先级</param>
        /// <param name="callback">创建完成后的回调</param>
        /// <param name="args">创建图层的参数</param>
        public virtual void Create(int order, Action<UILayer> callback, params object[] args)
        {
        }
    }
}