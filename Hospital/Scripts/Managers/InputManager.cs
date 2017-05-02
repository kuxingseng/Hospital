namespace Muhe.Mjhx.Managers
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// 用户操作输入管理器。
    /// </summary>
    public static class InputManager
    {
        /// <summary>
        /// 初始化。
        /// </summary>
        /// <param name="graphicRaycaster"></param>
        public static void Initialize(GraphicRaycaster graphicRaycaster)
        {
            if (graphicRaycaster == null)
                throw new ArgumentNullException("graphicRaycaster");

            mGraphicRaycaster = graphicRaycaster;
        }

        /// <summary>
        /// 锁定用户输入。
        /// </summary>
        /// <returns>锁定标识符，用于后续解锁输入用</returns>
        public static int Lock()
        {
            mGraphicRaycaster.enabled = false;
            var lockId = mLockCounter++;
            mLockIds.Add(lockId);
            return lockId;
        }

        /// <summary>
        /// 解锁用户输入。
        /// </summary>
        /// <param name="id">锁定标识符</param>
        public static void Unlock(int id)
        {
            if (!mLockIds.Remove(id))
            {
                Debug.Log("Unlock input with a nonexistent id -> " + id);
                return;
            }

            if (mLockIds.Count == 0)
                mGraphicRaycaster.enabled = true;
        }

        private static int mLockCounter;
        private static readonly List<int> mLockIds = new List<int>();
        private static GraphicRaycaster mGraphicRaycaster;
    }
}