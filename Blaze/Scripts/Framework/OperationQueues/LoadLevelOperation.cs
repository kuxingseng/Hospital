namespace Blaze.Framework.OperationQueues
{
    using System;
    using UnityEngine;

    /// <summary>
    /// 加载指定场景的操作。
    /// </summary>
    public class LoadLevelOperation : IOperation
    {
        #region IOperation Members

        /// <summary>
        /// 执行与释放或重置非托管资源相关的应用程序定义的任务。
        /// </summary>
        public void Dispose()
        {
            mLevelName = null;
        }

        public event EventHandler Failed;
        public event EventHandler Succeed;

        /// <summary>
        /// 取消操作。
        /// </summary>
        public void Cancel()
        {
            if (mIsLoading)
                throw new InvalidOperationException("operation can not cancel when it is loading level.");
            mLevelName = null;
        }

        /// <summary>
        /// 立即执行操作。
        /// </summary>
        public void Execute()
        {
            if (mLevelName == null)
                return;
            mIsLoading = true;
            Application.LoadLevel(mLevelName);
            mIsLoading = false;

            if (Succeed != null)
                Succeed(this, EventArgs.Empty);
        }

        #endregion

        /// <summary>
        /// 构造器。
        /// </summary>
        /// <param name="name">场景名称</param>
        public LoadLevelOperation(string name)
        {
            mLevelName = name;
        }

        private bool mIsLoading;
        private string mLevelName;
    }
}