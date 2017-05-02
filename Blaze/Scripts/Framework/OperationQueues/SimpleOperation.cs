namespace Blaze.Framework.OperationQueues
{
    using System;

    /// <summary>
    /// 简单的操作封装，通过继承实现对应的操作行为。
    /// </summary>
    public abstract class SimpleOperation : IOperation
    {
        #region IOperation Members

        /// <summary>
        /// 执行与释放或重置非托管资源相关的应用程序定义的任务。
        /// </summary>
        public void Dispose()
        {
            OnDispose();
        }

        /// <summary>
        /// 当操作失败时触发此事件。
        /// </summary>
        public event EventHandler Failed;

        /// <summary>
        /// 当操作成功完成时触发此事件。
        /// </summary>
        public event EventHandler Succeed;

        /// <summary>
        /// 取消操作。
        /// </summary>
        public void Cancel()
        {
            OnCancel();
        }

        /// <summary>
        /// 立即执行操作。
        /// </summary>
        public void Execute()
        {
            OnExecute();
        }

        #endregion

        /// <summary>
        /// 当操作需要取消时调用此方法。
        /// </summary>
        protected virtual void OnCancel() {}

        /// <summary>
        /// 当需要释放相关资源时调用此方法。
        /// </summary>
        protected virtual void OnDispose() {}

        /// <summary>
        /// 当需要执行操作时调用此方法。
        /// </summary>
        protected virtual void OnExecute()
        {
            if (Succeed != null)
                Succeed(this, EventArgs.Empty);
        }
    }
}