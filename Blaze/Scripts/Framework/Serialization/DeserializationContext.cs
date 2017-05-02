namespace Blaze.Framework.Serialization
{
    using System;
    using System.Collections.Generic;
    using Loading;
    using OperationQueues;

    /// <summary>
    /// �����л������ġ�
    /// </summary>
    public class DeserializationContext : IDisposable
    {
        /// <summary>
        /// ��ȡ��������Դ��������
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
        /// ����һ���ɹ���Ļص���
        /// </summary>
        /// <param name="callback"></param>
        public void AddCallback(Action callback)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");
            mCallbacks.Add(callback);
        }

        /// <summary>
        /// ��������������
        /// </summary>
        /// <param name="operation">����</param>
        /// <param name="level">�����ȼ�</param>
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
        /// ��������������
        /// </summary>
        /// <param name="assetId">��Դ��ʶ��</param>
        /// <param name="callback">������ɺ�Ļص�</param>
        /// <param name="level">�����ȼ�</param>
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
        /// ���ӽ������ò�����
        /// </summary>
        /// <typeparam name="T">��Դ����</typeparam>
        /// <param name="instanceId">��Դʵ����</param>
        /// <param name="callback">������ɺ�Ļص�</param>
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
        /// ��ɷ����л���������Դ��
        /// </summary>
        public void Finish()
        {
            foreach (var action in mCallbacks)
                action();
            Dispose();
        }

        /// <summary>
        /// ��ȡָ�������ȼ��ļ��ز�����
        /// </summary>
        /// <param name="level">�����ȼ�</param>
        /// <returns>����</returns>
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
        /// �����л���������ע��ʵ�������ã��Ա��ں��������н������ù�ϵ��
        /// </summary>
        /// <param name="referenceId">���ñ�ʶ��</param>
        /// <param name="obj">��Դ����</param>
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