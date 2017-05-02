namespace Blaze.Framework.OperationQueues
{
    using System;

    /// <summary>
    /// 将操作提供给操作队列执行。
    /// </summary>
    public interface IOperation : IDisposable
    {
        /// <summary>
        /// 当操作失败时触发此事件。
        /// </summary>
        event EventHandler Failed;

        /// <summary>
        /// 当操作成功完成时触发此事件。
        /// </summary>
        event EventHandler Succeed;

        /// <summary>
        /// 取消操作。
        /// </summary>
        void Cancel();

        /// <summary>
        /// 立即执行操作。
        /// </summary>
        void Execute();
    }
}