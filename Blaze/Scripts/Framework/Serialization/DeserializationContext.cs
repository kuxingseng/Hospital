namespace Blaze.Framework.Serialization
{
    using System;
    using System.Collections.Generic;
    using Loading;
    using OperationQueues;

    /// <summary>
    /// 反序列化上下文。
    /// </summary>
    public class DeserializationContext : IDisposable
    {
        /// <summary>
        /// 获取或设置资源解析器。
        /// </summary>
        public IAssetResolver AssetResolver { get; set; }

        public void Dispose()
        {
            mDecorations.Clear();
            mRequires.Clear();
            mReferences.Clear();
            mRegisteredReferences.Clear();
            mCallbacks.Clear();
        }

        /// <summary>
        /// 增加一个成功后的回调。
        /// </summary>
        /// <param name="callback"></param>
        public void AddCallback(Action callback)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");
            mCallbacks.Add(callback);
        }

        /// <summary>
        /// 增加依赖操作。
        /// </summary>
        /// <param name="operation">操作</param>
        /// <param name="level">依赖等级</param>
        public void AddDependency(ILoadOperation operation, DependencyLevel level)
        {
            if (level == DependencyLevel.Required)
                mRequires.Add(operation);
            else if (level == DependencyLevel.Decorate)
                mDecorations.Add(operation);
            else if (level == DependencyLevel.Reference)
                mReferences.Add(operation);
        }

        /// <summary>
        /// 增加依赖操作。
        /// </summary>
        /// <param name="assetId">资源标识符</param>
        /// <param name="callback">加载完成后的回调</param>
        /// <param name="level">依赖等级</param>
        public void AddDependency<T>(string assetId, Action<T> callback, DependencyLevel level = DependencyLevel.Required)
            where T : UnityEngine.Object
        {
            var operation = AssetResolver.Deserialize<T>(assetId);
            operation.OnSucceed(() =>
            {
                var result = (T) operation.Result;
                callback(result);
            });
            AddDependency(operation, level);
        }

        /// <summary>
        /// 增加解析引用操作。
        /// </summary>
        /// <typeparam name="T">资源类型</typeparam>
        /// <param name="instanceId">资源实例号</param>
        /// <param name="callback">加载完成后的回调</param>
        public void AddReference<T>(int instanceId, Action<T> callback)
            where T : UnityEngine.Object
        {
            var operation = new EmptyLoadOperation();
            operation.OnSucceed(() =>
            {
                UnityEngine.Object result;
                mRegisteredReferences.TryGetValue(instanceId, out result);
                callback((T) result);
            });
            AddDependency(operation, DependencyLevel.Reference);
        }

        /// <summary>
        /// 完成反序列化并清理资源。
        /// </summary>
        public void Finish()
        {
            foreach (var action in mCallbacks)
                action();
            Dispose();
        }

        /// <summary>
        /// 获取指定依赖等级的加载操作。
        /// </summary>
        /// <param name="level">依赖等级</param>
        /// <returns>操作</returns>
        public IEnumerable<ILoadOperation> GetDependencyOperations(DependencyLevel level)
        {
            if (level == DependencyLevel.Required)
                return mRequires;
            if (level == DependencyLevel.Decorate)
                return mDecorations;
            if (level == DependencyLevel.Reference)
                return mReferences;
            throw new ArgumentException("level");
        }

        /// <summary>
        /// 向反序列化上下文中注册实例的引用，以便在后续操作中解析引用关系。
        /// </summary>
        /// <param name="referenceId">引用标识符</param>
        /// <param name="obj">资源对象</param>
        public void RegisterReference(int referenceId, UnityEngine.Object obj)
        {
            if (!mRegisteredReferences.ContainsKey(referenceId))
                mRegisteredReferences.Add(referenceId, obj);
        }

        private readonly List<Action> mCallbacks = new List<Action>();
        private readonly List<ILoadOperation> mDecorations = new List<ILoadOperation>();
        private readonly List<ILoadOperation> mReferences = new List<ILoadOperation>();
        private readonly Dictionary<int, UnityEngine.Object> mRegisteredReferences = new Dictionary<int, UnityEngine.Object>();
        private readonly List<ILoadOperation> mRequires = new List<ILoadOperation>();
    }
}