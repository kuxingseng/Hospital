namespace Blaze.Framework.Pooling
{
    using UnityEngine;

    /// <summary>
    /// 用于<see cref="Component"/>的对象池，注意组件必须绑定在不同的<see cref="GameObject"/>上。
    /// </summary>
    /// <typeparam name="T">组件类型</typeparam>
    public class ComponentPool<T> : IObjectPool<T> where T : Component
    {
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
            mPool.Put((GameObject) obj);
        }

        #endregion

        #region IObjectPool<T> Members

        /// <summary>
        /// 从对象池中获取一个对象。
        /// </summary>
        /// <returns>可用的对象</returns>
        public T Get()
        {
            return mPool.Get().GetComponent<T>();
        }

        /// <summary>
        /// 将指定的对象回收到对象池。
        /// </summary>
        /// <param name="obj">对象</param>
        public void Put(T obj)
        {
            mPool.Put(obj.gameObject);
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pool"></param>
        public ComponentPool(GameObjectPool pool)
        {
            mPool = pool;
        }

        private readonly GameObjectPool mPool;
    }
}