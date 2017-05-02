namespace Blaze.Framework.Serialization
{
    using System;
    using Loading;

    /// <summary>
    /// 表示一个不进行任何加载行为的操作。
    /// </summary>
    public class EmptyLoadOperation : ILoadOperation
    {
        public void Dispose()
        {
        }

        public event EventHandler Failed;
        public event EventHandler Succeed;

        /// <summary>
        /// 取消操作。
        /// </summary>
        public void Cancel()
        {
        }

        /// <summary>
        /// 立即执行操作。
        /// </summary>
        public void Execute()
        {
            if (Succeed != null)
                Succeed(this, EventArgs.Empty);
        }

        /// <summary>
        /// 获取错误信息。
        /// </summary>
        public string Error { get; private set; }

        /// <summary>
        /// 获取一个值，表示加载是否完毕。
        /// </summary>
        public bool IsCompleted
        {
            get { return true; }
        }

        /// <summary>
        /// 获取加载进度。
        /// </summary>
        public float Progress
        {
            get { return 1; }
        }

        /// <summary>
        /// 获取加载的结果。
        /// </summary>
        public object Result { get; private set; }
    }
}