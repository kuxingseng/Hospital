namespace Blaze.Framework.ResourceLoaders
{
    using UnityEngine;

    /// <summary>
    /// 空资源加载器，当不需要加载任何资源时使用此加载器。
    /// </summary>
    public class EmptyLoader : ResourceLoaderBase
    {
        /// <summary>
        /// 加载指定标识符的资源。
        /// </summary>
        /// <param name="id">资源标识符</param>
        public override void Load(string id)
        {
            RaiseProgressChanged(0);
            Content = new GameObject("Content");
            RaiseProgressChanged(1);
        }
    }
}