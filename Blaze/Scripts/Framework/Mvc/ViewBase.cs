namespace Blaze.Framework.Mvc
{
    using OperationQueues;
    using UI;

    /// <summary>
    /// 视图抽象基类。
    /// </summary>
    public abstract class ViewBase : IView
    {
        /// <summary>
        /// 获取视图所在的层。
        /// </summary>
        public UILayer Layer
        {
            get { throw new System.NotImplementedException(); }
        }

        /// <summary>
        /// 获取视图所对应的管理器。
        /// </summary>
        protected ViewManager Manager { get; private set; }

        #region IOriginator Members

        /// <summary>
        /// 捕获当前备忘录状态。
        /// </summary>
        /// <returns></returns>
        public IMemento GetMemento()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 还原到指定的备忘录状态。
        /// </summary>
        /// <param name="memento">备忘录</param>
        public void SetMemento(IMemento memento)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region IView Members

        /// <summary>
        /// 获取视图加载资源的操作。
        /// </summary>
        /// <returns>操作</returns>
        public abstract IOperation LoadContent();

        /// <summary>
        /// 释放视图占用的资源。
        /// </summary>
        public abstract void UnloadContent();

        #endregion

        /// <summary>
        /// 构造一个<see cref="ViewBase"/>。
        /// </summary>
        /// <param name="manager">视图管理器</param>
        protected ViewBase(ViewManager manager)
        {
            Manager = manager;
        }
    }
}