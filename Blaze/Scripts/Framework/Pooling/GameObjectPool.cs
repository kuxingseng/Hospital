namespace Blaze.Framework.Pooling
{
    using System;
    using UnityEngine;

    /// <summary>
    /// <see cref="GameObject"/>对象池。
    /// </summary>
    public class GameObjectPool : ObjectPool<GameObject>
    {
        /// <summary>
        /// 构造一个<see cref="ObjectPool&lt;T&gt;"/>的实例，并指定最大容量。
        /// </summary>
        /// <param name="prefab">用于创建游戏对象的预设</param>
        /// <param name="capacity">最大容量</param>
        public GameObjectPool(GameObject prefab, int capacity)
            : base(capacity)
        {
            if (prefab == null)
                throw new ArgumentNullException("prefab");

            mPrefab = prefab;
        }

        /// <summary>
        /// 清理所有缓存的对象。
        /// </summary>
        public void Clear()
        {
            while (AvailiableObjects.Count > 0)
                UnityEngine.Object.Destroy(AvailiableObjects.Dequeue());
            while (InUseObjects.Count > 0)
                UnityEngine.Object.Destroy(InUseObjects.Dequeue());
        }

        /// <summary>
        /// 获取所有正在使用中的对象。
        /// </summary>
        /// <returns></returns>
        public GameObject[] GetInUseObjects()
        {
            return InUseObjects.ToArray();
        }

        /// <summary>
        /// 回收所有正在使用的对象。
        /// </summary>
        public void RecycleAll()
        {
            while (InUseObjects.Count > 0)
            {
                var obj = InUseObjects.Dequeue();
                Put(obj);
            }
        }

        /// <summary>
        /// 当需要创建一个新的对象时调用此方法。
        /// </summary>
        /// <returns></returns>
        protected override GameObject Create()
        {
            var obj = (GameObject) UnityEngine.Object.Instantiate(mPrefab);
            UnityEngine.Object.DontDestroyOnLoad(obj);
            return obj;
        }

        /// <summary>
        /// 当需要从对象池中获取对象时调用此方法。
        /// </summary>
        /// <param name="obj"></param>
        protected override void OnGet(GameObject obj)
        {
            base.OnGet(obj);
            obj.SetActive(true);
        }

        /// <summary>
        /// 当需要回收指定对象时调用此方法。
        /// </summary>
        /// <param name="obj">需要回收的对象</param>
        protected override void OnPut(GameObject obj)
        {
            base.OnPut(obj);
            obj.SetActive(false);
        }

        private readonly GameObject mPrefab;
    }
}