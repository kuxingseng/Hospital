namespace Blaze.Framework.Pooling
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 对象池模式实现。
    /// </summary>
    /// <remarks>http://en.wikipedia.org/wiki/Object_pool_pattern</remarks>
    public abstract class ObjectPool<T> : IObjectPool<T> where T : class
    {
        /// <summary>
        /// 获取对象池的容量。
        /// </summary>
        public int Capacity { get; private set; }

        /// <summary>
        /// 获取可用对象队列。
        /// </summary>
        protected Queue<T> AvailiableObjects { get; private set; }

        /// <summary>
        /// 获取使用中的对象队列。
        /// </summary>
        protected Queue<T> InUseObjects { get; private set; }

        #region IObjectPool Members

        /// <summary>
        /// 从对象池中获取一个对象。
        /// </summary>
        /// <returns>可用的对象</returns>
        object IObjectPool.Get()
        {
            return Get();
        }

        /// <summary>
        /// 将指定的对象回收到对象池。
        /// </summary>
        /// <param name="obj">对象</param>
        public void Put(object obj)
        {
            var put = (T) obj;
            if (AvailiableObjects.Contains(put))
                return;
            OnPut(put);
            AvailiableObjects.Enqueue(put);
        }

        #endregion

        #region IObjectPool<T> Members

        /// <summary>
        /// 从对象池中获取一个对象。
        /// </summary>
        /// <returns>可用的对象</returns>
        public T Get()
        {
            T ret;
            if (AvailiableObjects.Count != 0)
            {
                ret = AvailiableObjects.Dequeue();
            }
            else if (InUseObjects.Count == Capacity)
            {
                ret = InUseObjects.Dequeue();
                OnPut(ret);
            }
            else
            {
                ret = Create();
            }
            OnGet(ret);
            InUseObjects.Enqueue(ret);
            return ret;
        }

        /// <summary>
        /// 将指定的对象回收到对象池。
        /// </summary>
        /// <param name="obj">对象</param>
        public void Put(T obj)
        {
            if (AvailiableObjects.Contains(obj))
                return;
            OnPut(obj);
            AvailiableObjects.Enqueue(obj);
        }

        #endregion

        /// <summary>
        /// 构造一个<see cref="ObjectPool&lt;T&gt;"/>的实例，并指定最大容量。
        /// </summary>
        /// <param name="capacity">最大容量</param>
        protected ObjectPool(int capacity)
        {
            if (capacity <= 0)
                throw new ArgumentOutOfRangeException("capacity");

            Capacity = capacity;

            AvailiableObjects = new Queue<T>(capacity);
            InUseObjects = new Queue<T>(capacity);
        }

        /// <summary>
        /// 当需要创建一个新的对象时调用此方法。
        /// </summary>
        /// <returns></returns>
        protected abstract T Create();

        /// <summary>
        /// 当需要从对象池中获取对象时调用此方法。
        /// </summary>
        /// <param name="obj"></param>
        protected virtual void OnGet(T obj)
        {
        }

        /// <summary>
        /// 当需要回收指定对象时调用此方法。
        /// </summary>
        /// <param name="obj">需要回收的对象</param>
        protected virtual void OnPut(T obj)
        {
        }
    }
}