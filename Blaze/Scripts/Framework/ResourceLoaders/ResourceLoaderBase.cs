namespace Blaze.Framework.ResourceLoaders
{
    using System;
    using Object = UnityEngine.Object;

    /// <summary>
    /// 资源加载器抽象基类。
    /// </summary>
    public abstract class ResourceLoaderBase : IResourceLoader
    {
        #region IResourceLoader Members

        /// <summary>
        /// 执行与释放或重置非托管资源相关的应用程序定义的任务。
        /// </summary>
        public void Dispose()
        {
            mContentReference.Target = null;
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 获取加载成功后的资源。
        /// </summary>
        public Object Content
        {
            get { return (Object) mContentReference.Target; }
            protected set { mContentReference.Target = value; }
        }

        /// <summary>
        /// 当加载进度变更时触发此事件。
        /// </summary>
        public event EventHandler<LoadProgressEventArgs> ProgressChanged;

        /// <summary>
        /// 加载指定标识符的资源。
        /// </summary>
        /// <param name="id">资源标识符</param>
        public abstract void Load(string id);

        #endregion

        ~ResourceLoaderBase()
        {
            Dispose(false);
        }

        /// <summary>
        /// 释放资源。
        /// </summary>
        /// <param name="isDisposing">若为真则表示由<see cref="Dispose"/>方法调用，否则为析构方法调用</param>
        protected virtual void Dispose(bool isDisposing) {}

        /// <summary>
        /// 广播加载进度变更事件。
        /// </summary>
        /// <param name="progress">进度</param>
        /// <param name="error">错误信息</param>
        protected void RaiseProgressChanged(float progress, string error = null)
        {
            if (ProgressChanged == null)
                return;
            ProgressChanged(this, new LoadProgressEventArgs
            {
                Error = error,
                Progress = progress
            });
        }

        private readonly WeakReference mContentReference = new WeakReference(null);
    }
}