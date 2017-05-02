namespace Blaze.Framework.OperationQueues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// 提供每帧执行一个操作的操作队列。
    /// </summary>
    public class FrameOperationQueue : IOperationQueue, IUpdatable
    {
        /// <summary>
        /// 获取操作队列中的数量。
        /// </summary>
        public int Count
        {
            get { return mQueue.Count; }
        }

        private readonly IGameEngine mGame;

        /// <summary>
        /// 获取当前正在执行的操作。
        /// </summary>
        public IOperation CurrentOperation { get; private set; }

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
            mQueue.Add(operation);
        }

        /// <summary>
        /// 将指定操作插入到操作队列最前面
        /// </summary>
        /// <param name="operation"></param>
        public void Insert(IOperation operation)
        {
            if (operation == null)
                throw new ArgumentNullException("operation");
            if (mQueue.Contains(operation))
                throw new InvalidOperationException("duplicated operation in queue.");
            if (!mQueue.Any())
                mGame.RegisterUpdatable(this);
            mQueue.Insert(0,operation);
        }

        #endregion

        #region IUpdatable Members

        /// <summary>
        /// 更新逻辑。
        /// </summary>
        public virtual void Update()
        {
            //取消注册
            if (!mQueue.Any())
                mGame.UnregisterUpdatable(this);

            if (CurrentOperation != null || !mQueue.Any())
                return;
            CurrentOperation = mQueue[0];
            mQueue.Remove(CurrentOperation);
            CurrentOperation.Succeed += onOperationCompleted;
            CurrentOperation.Failed += onOperationCompleted;
            CurrentOperation.Execute();
        }

        #endregion

        /// <summary>
        /// 构造器。
        /// </summary>
        /// <param name="game">游戏</param>
        public FrameOperationQueue(IGameEngine game)
        {
            mGame = game;
        }

        private void onOperationCompleted(object sender, EventArgs e)
        {
            CurrentOperation.Dispose();
            CurrentOperation = null;
        }

        private readonly List<IOperation> mQueue = new List<IOperation>();
    }
}