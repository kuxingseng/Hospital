namespace Blaze.Framework.OperationQueues
{
    using System;

    /// <summary>
    /// 执行某个方法的操作。
    /// </summary>
    public class ActionOperation : IOperation
    {
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
            mIsExecuting = true;
            if (mAction != null)
                mAction();
            mIsExecuting = false;

            if (Succeed != null)
                Succeed(this, EventArgs.Empty);
        }

        #endregion

        /// <summary>
        /// 构造器。
        /// </summary>
        /// <param name="action">需要执行的方法</param>
        public ActionOperation(Action action)
        {
            mAction = action;
        }

        private Action mAction;

        private bool mIsExecuting;

        /// <summary>
        /// 提供<see cref="Action"/>到<see cref="ActionOperation"/>的隐式转换。
        /// </summary>
        /// <param name="action">需要封装的方法</param>
        /// <returns>转换后的操作</returns>
        public static implicit operator ActionOperation(Action action)
        {
            return new ActionOperation(action);
        }
    }
}