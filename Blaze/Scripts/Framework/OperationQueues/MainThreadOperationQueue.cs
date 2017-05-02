namespace Blaze.Framework.OperationQueues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    /// <summary>
    /// 主线程操作队列，确保每个操作都在主线程中执行。
    /// 如果一个操作完成后，仍然在主线程中，则立即执行下一个操作。
    /// </summary>
    public class MainThreadOperationQueue : IOperationQueue, IUpdatable
    {
        private readonly IGameEngine mGame;

        /// <summary>
        /// 获取操作队列中的数量。
        /// </summary>
        public int Count
        {
            get { return mQueue.Count; }
        }

        /// <summary>
        /// 获取当前正在执行的操作。
        /// </summary>
        public IOperation CurrentOperation { get; private set; }

        /// <summary>
        /// 获取一个值，表示当前是否在主线程上下文中。
        /// </summary>
        protected bool IsInContext
        {
            get { return Thread.CurrentThread == mMainThread; }
        }

        #region IOperationQueue Members

        /// <summary>
        /// 清除队列中所有的操作。
        /// </summary>
        public void Clear()
        {
            mQueue.Clear();
        }

        /// <summary>
        /// 将指定操作加入操作队列。
        /// </summary>
        /// <param name="operation">操作</param>
        public void Enqueue(IOperation operation)
        {
            if (operation == null)
                throw new ArgumentNullException("operation");
            if (mQueue.Contains(operation))
                throw new InvalidOperationException("duplicated operation in queue.");
            if (!mQueue.Any())
                mGame.RegisterUpdatable(this);
            mQueue.Enqueue(operation);
        }

        #endregion

        #region IUpdatable Members

        /// <summary>
        /// 更新逻辑。
        /// </summary>
        public void Update()
        {
            //取消注册
            if (!mQueue.Any())
                mGame.UnregisterUpdatable(this);

            if (CurrentOperation != null || !mQueue.Any())
                return;
            dequeueAndExecute();
        }

        #endregion

        /// <summary>
        /// 构造器。
        /// </summary>
        /// <param name="gameEngine">游戏</param>
        public MainThreadOperationQueue(IGameEngine gameEngine)
        {
            mMainThread = Thread.CurrentThread;
            mGame = gameEngine;
        }

        private void dequeueAndExecute()
        {
            CurrentOperation = mQueue.Dequeue();
            CurrentOperation.Succeed += onOperationCompleted;
            CurrentOperation.Failed += onOperationCompleted;
            CurrentOperation.Execute();
        }

        private void onOperationCompleted(object sender, EventArgs e)
        {
            CurrentOperation.Succeed -= onOperationCompleted;
            CurrentOperation.Failed -= onOperationCompleted;
            CurrentOperation.Dispose();
            CurrentOperation = null;

            if (mMainThread != Thread.CurrentThread || !mQueue.Any())
                return;
            dequeueAndExecute();
        }

        private readonly Queue<IOperation> mQueue = new Queue<IOperation>();
        private readonly Thread mMainThread;
    }
}