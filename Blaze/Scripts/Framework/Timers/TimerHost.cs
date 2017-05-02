namespace Blaze.Framework.Timers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    /// <summary>
    /// 计时器宿主，为<see cref="Timer"/>提供定时调用的功能。
    /// </summary>
    public class TimerHost : MonoBehaviour
    {
        /// <summary>
        /// 获取或设置依附于宿主的计时器。
        /// </summary>
        public Timer Timer { get; set; }

        /// <summary>
        /// 创建一个计时器宿主。
        /// </summary>
        /// <returns>计时器宿主</returns>
        public static TimerHost Create(Timer timer)
        {
            if (timer == null)
                throw new ArgumentNullException("timer");

            var unusedHost = mTimerHostPool.SingleOrDefault(host => !host.enabled);
            if (unusedHost != null)
            {
                unusedHost.Timer = timer;
                return unusedHost;
            }

            var hostObj = new GameObject("Timer" + mCounter);
            hostObj.transform.parent = TimerHostParent.Instance.transform;
            mCounter++;
            unusedHost = hostObj.AddComponent<TimerHost>();
            unusedHost.Timer = timer;
            return unusedHost;
        }

        protected void OnEnable()
        {
            mLastTickTime = Time.time;
        }

        protected void Update()
        {
            if (!(Time.time >= mLastTickTime + Timer.Interval))
                return;
            mLastTickTime = Time.time;
            Timer.InternalTick();
        }

        private static readonly List<TimerHost> mTimerHostPool = new List<TimerHost>();
        private static int mCounter;
        private float mLastTickTime;
    }
}