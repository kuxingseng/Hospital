namespace Blaze.Framework.OperationQueues
{
    using System;

    /// <summary>
    /// 异步的操作。
    /// </summary>
    public class AsyncActionOperation : IOperation
    {
        /// <summary>
        /// 操作委托，当操作成功后需要回调<see cref="done"/>来抛出操作成功事件。
        /// </summary>
        /// <param name="done">操作成功后的回调</param>
        public delegate void OperationDelegate(Action done);

        #region IOperation Members

        /// <summary>
        /// 执行与释放或重置非托管资源相关的应用程序定义的任务。
        /// </summary>
        public void Dispose()
        {
            mAction = null;
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
            if (mIsExecuting)
                throw new NotSupportedException("The action can not be cancel when it is executing.");
            mAction = null;
        }

        /// <summary>
        /// 立即执行操作。
        /// </summary>
        public void Execute()
        {
            if (mAction == null)
                return;
            mIsExecuting = true;
            mAction(done);
        }

        #endregion

        /// <summary>
        /// 构造器。
        /// </summary>
        /// <param name="action">需要执行的方法</param>
        public AsyncActionOperation(OperationDelegate action)
        {
            mAction = action;
        }

        private void done()
        {
            mIsExecuting = false;
            if (Succeed != null)
                Succeed(this, EventArgs.Empty);
        }

        private OperationDelegate mAction;
        private bool mIsExecuting;
    }
}