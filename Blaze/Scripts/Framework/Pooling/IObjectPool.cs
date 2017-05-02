namespace Blaze.Framework.Pooling
{
    /// <summary>
    /// 对象池模式接口。
    /// </summary>
    /// <remarks>http://en.wikipedia.org/wiki/Object_pool_pattern</remarks>
    public interface IObjectPool
    {
        /// <summary>
        /// 从对象池中获取一个对象。
        /// </summary>
        /// <returns>可用的对象</returns>
        object Get();

        /// <summary>
        /// 将指定的对象回收到对象池。
        /// </summary>
        /// <param name="obj">对象</param>
        void Put(object obj);
    }

    /// <summary>
    /// 对象池模式泛型接口。
    /// </summary>
    /// <remarks>http://en.wikipedia.org/wiki/Object_pool_pattern</remarks>
    /// <typeparam name="T">对象类型</typeparam>
    public interface IObjectPool<T> : IObjectPool where T : class
    {
        /// <summary>
        /// 从对象池中获取一个对象。
        /// </summary>
        /// <returns>可用的对象</returns>
        new T Get();

        /// <summary>
        /// 将指定的对象回收到对象池。
        /// </summary>
        /// <param name="obj">对象</param>
        void Put(T obj);
    }
}