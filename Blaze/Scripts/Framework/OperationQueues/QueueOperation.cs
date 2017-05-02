namespace Blaze.Framework.OperationQueues
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// 队列操作。
    /// </summary>
    public class QueueOperation : IOperation
    {
        #region IOperation Members

        /// <summary>
        /// 执行与释放或重置非托管资源相关的应用程序定义的任务。
        /// </summary>
        public void Dispose()
        {
            //TODO:Dispose
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
                operation.OnSucceed(onOperationSucceed);
                operation.OnFailed(onOperationFailed);
                mQueue.Enqueue(operation);
            }
        }

        #endregion

        /// <summary>
        /// 构造器。
        /// </summary>
        /// <param name="queue">操作队列</param>
        /// <param name="operations">操作集合</param>
        public QueueOperation(IOperationQueue queue, IEnumerable<IOperation> operations)
        {
            if (queue == null)
                throw new ArgumentNullException("queue");
            if (operations == null)
                throw new ArgumentNullException("operations");
            mQueue = queue;
            mOperations = operations;
        }

        private void onOperationFailed()
        {
            if (Failed != null)
                Failed(this, EventArgs.Empty);
            mQueue.Clear();
        }

        private void onOperationSucceed()
        {
            if (mQueue.Count > 0)
                return;
            if (Succeed != null)
                Succeed(this, EventArgs.Empty);
        }

        private readonly IEnumerable<IOperation> mOperations;
        private readonly IOperationQueue mQueue;
    }
}