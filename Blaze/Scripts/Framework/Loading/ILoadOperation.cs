namespace Blaze.Framework.Loading
{
    using OperationQueues;

    /// <summary>
    /// 加载操作接口。
    /// </summary>
    public interface ILoadOperation : IOperation
    {
        /// <summary>
        /// 获取错误信息。
        /// </summary>
        string Error { get; }

        /// <summary>
        /// 获取一个值，表示加载是否完毕。
        /// </summary>
        bool IsCompleted { get; }

        /// <summary>
        /// 获取加载进度。
        /// </summary>
        float Progress { get; }

        /// <summary>
        /// 获取加载的结果。
        /// </summary>
        object Result { get; }
    }
}