namespace Blaze.Framework.OperationQueues
{
    /// <summary>
    /// 操作队列接口。
    /// </summary>
    public interface IOperationQueue
    {
        /// <summary>
        /// 获取操作队列中的数量。
        /// </summary>
        int Count { get; }

        /// <summary>
        /// 将指定操作加入操作队列。
        /// </summary>
        /// <param name="operation">操作</param>
        void Enqueue(IOperation operation);

        /// <summary>
        /// 清除队列中所有的操作。
        /// </summary>
        void Clear();
    }

    /// <summary>
    /// 操作队列接口。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IOperationQueue<in T> where T : IOperation
    {
        /// <summary>
        /// 将指定操作加入操作队列。
        /// </summary>
        /// <param name="operation">操作</param>
        void Enqueue(T operation);
    }
}