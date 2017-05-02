namespace Blaze.Framework.Loading
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using OperationQueues;

    /// <summary>
    /// 加载阶段。
    /// </summary>
    public abstract class LoadingPhase : CoroutineOperation, ILoadOperation
    {
        /// <summary>
        /// 构造器。
        /// </summary>
        /// <param name="game">游戏引擎</param>
        protected LoadingPhase(IGameEngine game)
            : base(game)
        {
            mQueue = new FrameOperationQueue(game);
        }

        /// <summary>
        /// 获取所有的加载子步骤。
        /// </summary>
        /// <returns>加载子步骤集合</returns>
        protected abstract IEnumerable<LoadingStep> CreateSteps();

        /// <summary>
        /// 当需要执行操作时调用此方法，这是一个协同程序。
        /// </summary>
        protected override IEnumerator OnExecute()
        {
            var steps = CreateSteps().ToArray();
            foreach (var step in steps)
                mQueue.Enqueue(step);
            while (mQueue.Count > 0)
            {
                yield return null;
                var currentProgress = 0.0f;
                for (var i = 0; i < steps.Length; i++)
                    currentProgress += steps[i].Progress;
                Progress = currentProgress/steps.Length;
            }
        }

        private readonly FrameOperationQueue mQueue;

        #region ILoadOperation Members

        /// <summary>
        /// 获取错误信息。
        /// </summary>
        public string Error { get; protected set; }

        /// <summary>
        /// 获取一个值，表示加载阶段是否已经完成。
        /// </summary>
        public bool IsCompleted
        {
            get { return Progress >= 1; }
        }

        /// <summary>
        /// 获取加载阶段的总进度。
        /// </summary>
        public float Progress { get; private set; }

        /// <summary>
        /// 获取加载的结果。
        /// </summary>
        public object Result { get; protected set; }

        #endregion
    }
}