namespace Blaze.Framework.OperationQueues
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 串行操作。
    /// </summary>
    public class SerialOperation : IOperation
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
            executeNextOperation();
        }

        #endregion

        /// <summary>
        /// 构造器。
        /// </summary>
        /// <param name="operations">操作集合</param>
        public SerialOperation(IEnumerable<IOperation> operations)
        {
            mOperations = new Queue<IOperation>(operations);
        }

        private void executeNextOperation()
        {
            if (mOperations.Count == 0)
            {
                if (Succeed != null)
                    Succeed(this, EventArgs.Empty);
                return;
            }
            var operation = mOperations.Dequeue();
            operation.OnSucceed(executeNextOperation);
            operation.OnFailed(fail);
            operation.Execute();
        }

        private void fail()
        {
            mOperations.Clear();
            if (Failed != null)
                Failed(this, EventArgs.Empty);
        }

        private readonly Queue<IOperation> mOperations;
    }
}