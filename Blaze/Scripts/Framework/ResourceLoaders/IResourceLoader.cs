namespace Blaze.Framework.ResourceLoaders
{
    using System;
    using Object = UnityEngine.Object;

    /// <summary>
    /// 资源加载器接口。
    /// </summary>
    public interface IResourceLoader : IDisposable
    {
        /// <summary>
        /// 当加载进度变更时触发此事件。
        /// </summary>
        event EventHandler<LoadProgressEventArgs> ProgressChanged;

        /// <summary>
        /// 获取加载成功后的资源。
        /// </summary>
        Object Content { get; }

        /// <summary>
        /// 加载指定标识符的资源。
        /// </summary>
        /// <param name="id">资源标识符</param>
        void Load(string id);
    }
}