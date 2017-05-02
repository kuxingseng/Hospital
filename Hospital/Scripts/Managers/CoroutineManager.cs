namespace Muhe.Mjhx.Managers
{
    using System.Collections;
    using Blaze.Framework;
    using UnityEngine;

    /// <summary>
    /// 协同程序管理器。
    /// </summary>
    public static class CoroutineManager
    {
        /// <summary>
        /// 启动一个新的协同程序，不受<see cref="GameObject"/>本身是否为active影响。
        /// </summary>
        /// <param name="routine">协同程序迭代器</param>
        /// <returns>协同程序</returns>
        public static Coroutine Start(IEnumerator routine)
        {
            return BlazeEngine.Instance.StartCoroutine(routine);
        }

        /// <summary>
        /// 启动一个新的协同程序，不受<see cref="GameObject"/>本身是否为active影响。
        /// </summary>
        /// <param name="routineName">协同程序迭代器</param>
        /// <returns>协同程序</returns>
        public static Coroutine Start(string routineName)
        {
            return BlazeEngine.Instance.StartCoroutine(routineName);
        }

        /// <summary>
        /// 停止一个协程
        /// </summary>
        /// <param name="routine"></param>
        public static void Stop(IEnumerator routine)
        {
            BlazeEngine.Instance.StopCoroutine(routine);
        }

        /// <summary>
        /// 停止一个协程
        /// </summary>
        /// <param name="routineName"></param>
        public static void Stop(string routineName)
        {
            BlazeEngine.Instance.StopCoroutine(routineName);
        }
    }
}