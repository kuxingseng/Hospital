namespace Blaze.Framework.Loading
{
    using OperationQueues;

    /// <summary>
    /// 加载步骤。
    /// </summary>
    public abstract class LoadingStep : CoroutineOperation, ILoadOperation
    {
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
        /// 获取加载进度。
        /// </summary>
        public float Progress { get; protected set; }

        /// <summary>
        /// 获取加载的结果。
        /// </summary>
        public object Result { get; protected set; }

        #endregion

        /// <summary>
        /// 构造器。
        /// </summary>
        /// <param name="game">游戏引擎</param>
        protected LoadingStep(IGameEngine game)
            : base(game)
        {
        }
    }
}