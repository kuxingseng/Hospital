namespace Blaze.Framework
{
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Blaze核心驱动对象。
    /// </summary>
    [Singleton(Hierarchy = "Blaze")]
    public class BlazeEngine : MonoBehaviour, IGameEngine
    {
        /// <summary>
        /// 获取<see cref="BlazeEngine"/>的唯一实例。
        /// </summary>
        public static BlazeEngine Instance
        {
            get
            {
                if (!mIsInitialized)
                {
                    mInstance = Singleton.GetInstance<BlazeEngine>();
                    Debug.LogWarning("Blaze not initialized.");
                }
                return mInstance;
            }
        }

        /// <summary>
        /// 获取一个值，表示游戏是否正在退出。
        /// </summary>
        public static bool IsShuttingDown { get; private set; }

        #region IGameEngine Members

        /// <summary>
        /// 注册每帧更新的对象。
        /// </summary>
        /// <param name="updatable">自我更新的对象</param>
        public void RegisterUpdatable(IUpdatable updatable)
        {
            if(!mUpdatables.Contains(updatable))
                mUpdatables.Add(updatable);
        }

        /// <summary>
        /// 取消注册每帧更新的对象。
        /// </summary>
        /// <param name="updatable">自我更新的对象</param>
        public void UnregisterUpdatable(IUpdatable updatable)
        {
            mUpdatables.Remove(updatable);
        }

        #endregion

        /// <summary>
        /// 初始化。
        /// </summary>
        public static void Initialize()
        {
            if (mIsInitialized)
                return;
            mIsInitialized = true;
            mInstance = Singleton.GetInstance<BlazeEngine>();
        }

        protected void OnApplicationQuit()
        {
            IsShuttingDown = true;
        }

        protected void Update()
        {
            if (mUpdatables.Count == 0)
                return;
            for (var i = 0; i < mUpdatables.Count; i++)
            {
                var updatable = mUpdatables[i];
                updatable.Update();
            }
        }

        private static bool mIsInitialized;
        private static BlazeEngine mInstance;
        private readonly List<IUpdatable> mUpdatables = new List<IUpdatable>();
    }
}