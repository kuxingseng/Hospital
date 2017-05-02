namespace Blaze.Framework.OperationQueues
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 并行操作。
    /// </summary>
    public class ParallelOperation : IOperation
    {
        #region IOperation Members

        /// <summary>
        /// 执行与释放或重置非托管资源相关的应用程序定义的任务。
        /// </summary>
        public void Dispose()
        {
            mOperations.Clear();
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// 立即执行操作。
        /// </summary>
        public void Execute()
        {
            foreach (var operation in mOperations)
            {
                operation.Succeed += onOperationSucceed;
                operation.Failed += onOperationFailed;
                operation.Execute();
            }
        }

        #endregion

        /// <summary>
        /// 构造器。
        /// </summary>
        /// <param name="operations">操作集合</param>
        public ParallelOperation(IEnumerable<IOperation> operations)
        {
            mOperations = new List<IOperation>(operations);
        }

        private void onOperationFailed(object sender, EventArgs e)
        {
            var operation = (IOperation) sender;
            operation.Failed -= onOperationFailed;
            if (Failed != null)
                Failed(this, EventArgs.Empty);
        }

        private void onOperationSucceed(object sender, EventArgs e)
        {
            var operation = (IOperation) sender;
            operation.Succeed -= onOperationSucceed;
            mOperations.Remove(operation);
            if (mOperations.Count != 0)
                return;
            if (Succeed != null)
                Succeed(this, EventArgs.Empty);
        }

        private readonly List<IOperation> mOperations;
    }
}