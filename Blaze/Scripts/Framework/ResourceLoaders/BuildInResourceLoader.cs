namespace Blaze.Framework.ResourceLoaders
{
    using Logging;
    using UnityEngine;

    /// <summary>
    /// 本地资源同步加载器，资源保存在Asset/Resources目录下。
    /// </summary>
    public class BuildInResourceLoader : ResourceLoaderBase
    {
        /// <summary>
        /// 加载指定标识符的资源。
        /// </summary>
        /// <param name="id">资源标识符</param>
        public override void Load(string id)
        {
            RaiseProgressChanged(0);
            Content = Resources.Load(id);
            if (Content == null)
                BlazeLog.WarningFormat("Load content failed.id={0}", id);
            RaiseProgressChanged(1);
        }
    }
}