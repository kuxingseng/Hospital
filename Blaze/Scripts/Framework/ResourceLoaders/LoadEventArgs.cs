namespace Blaze.Framework.ResourceLoaders
{
    using System;

    /// <summary>
    /// 加载进度事件参数。
    /// </summary>
    public class LoadProgressEventArgs : EventArgs
    {
        /// <summary>
        /// 获取或设置错误信息。
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// 获取一个值，表示是否出错，通过<see cref="Error"/>来获取详细错误信息。
        /// </summary>
        public bool IsError
        {
            get { return Error != null; }
        }

        /// <summary>
        /// 获取一个值，表示加载是否已结束。
        /// </summary>
        public bool IsFinished
        {
            get { return Progress >= 1.0f; }
        }

        /// <summary>
        /// 获取或设置加载的进度，范围为0.0-1.0。
        /// </summary>
        public float Progress
        {
            get { return mProgress; }
            set
            {
                if (value < 0)
                    mProgress = 0;
                else if (value > 1.0f)
                    mProgress = 1.0f;
                else
                    mProgress = value;
            }
        }

        private float mProgress;
    }
}