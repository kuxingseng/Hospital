
#if UNITY_4_6

namespace Blaze.Framework.ResourceLoaders
{
    using System;
    using System.Collections;
    using Logging;
    using UnityEngine;

    /// <summary>
    /// 本地资源异步加载器，资源保存在Asset/Resources目录下。
    /// </summary>
    public class BuildInResourceAsyncLoader : ResourceLoaderBase
    {
        /// <summary>
        /// 构造一个<see cref="BuildInResourceAsyncLoader"/>，并指定使用协程的宿主。
        /// </summary>
        /// <param name="engine"></param>
        public BuildInResourceAsyncLoader(IGameEngine engine)
        {
            if (engine == null)
                throw new ArgumentNullException("engine");
            mEngine = engine;
        }

        /// <summary>
        /// 加载指定标识符的资源。
        /// </summary>
        /// <param name="id">资源标识符</param>
        public override void Load(string id)
        {
            mEngine.StartCoroutine(loadAsync(id));
        }

        private IEnumerator loadAsync(string id)
        {
            RaiseProgressChanged(0);
            var request = Resources.LoadAsync(id);
            while (!request.isDone)
            {
                yield return null;
                RaiseProgressChanged(request.progress);
            }
            if (Content == null)
                BlazeLog.WarningFormat("Load content failed.id={0}", id);
            RaiseProgressChanged(1);
        }

        private readonly IGameEngine mEngine;
    }
}

#endif