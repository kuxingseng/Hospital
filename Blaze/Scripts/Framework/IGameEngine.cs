namespace Blaze.Framework
{
    using System.Collections;
    using UnityEngine;

    /// <summary>
    /// 游戏引擎接口。
    /// </summary>
    public interface IGameEngine
    {
        /// <summary>
        /// 注册每帧更新的对象。
        /// </summary>
        /// <param name="updatable">自我更新的对象</param>
        void RegisterUpdatable(IUpdatable updatable);

        /// <summary>
        /// 启动一个协同程序。
        /// </summary>
        /// <param name="coroutine">协同程序</param>
        Coroutine StartCoroutine(IEnumerator coroutine);

        /// <summary>
        /// 取消注册每帧更新的对象。
        /// </summary>
        /// <param name="updatable">自我更新的对象</param>
        void UnregisterUpdatable(IUpdatable updatable);
    }
}