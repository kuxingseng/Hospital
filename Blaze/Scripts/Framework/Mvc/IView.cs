namespace Blaze.Framework.Mvc
{
    using OperationQueues;
    using UI;

    /// <summary>
    /// 视图接口。
    /// </summary>
    public interface IView : IOriginator
    {
        /// <summary>
        /// 获取视图所在的层。
        /// </summary>
        UILayer Layer { get; }

        /// <summary>
        /// 获取视图加载资源的操作。
        /// </summary>
        /// <returns>操作</returns>
        IOperation LoadContent();

        /// <summary>
        /// 释放视图占用的资源。
        /// </summary>
        void UnloadContent();
    }
}