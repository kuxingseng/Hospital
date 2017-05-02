namespace Blaze.Framework.Timers
{
    using UnityEngine;

    /// <summary>
    /// 为<see cref="TimerHost"/>提供统一的父对象，方便在编辑器中显示和管理。
    /// </summary>
    [Singleton(Hierarchy = "Blaze/Timers")]
    public class TimerHostParent : MonoBehaviour
    {
        /// <summary>
        /// 获取<see cref="TimerHostParent"/>的唯一实例。
        /// </summary>
        public static TimerHostParent Instance
        {
            get { return Singleton.GetInstance<TimerHostParent>(); }
        }
    }
}