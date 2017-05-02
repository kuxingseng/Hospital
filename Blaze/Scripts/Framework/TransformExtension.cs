namespace Blaze.Framework
{
    using UnityEngine;

    /// <summary>
    /// 为<see cref="Transform"/>提供常用的扩展功能。
    /// </summary>
    public static class TransformExtension
    {
        /// <summary>
        /// 摧毁指定对象下所有的子对象。
        /// </summary>
        public static void DestroyChildren(this Transform transform)
        {
            for (var i = 0; i < transform.childCount; i++)
                Object.Destroy(transform.GetChild(i).gameObject);
        }

        /// <summary>
        /// 从子对象上找到指定的组件。
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        /// <param name="transform">父组件</param>
        /// <param name="path">路径</param>
        /// <returns>组件</returns>
        public static T FindChild<T>(this Transform transform, string path) where T : Component
        {
            var child = transform.Find(path);
            if (child == null)
                return null;
            return child.GetComponent<T>();
        }

        /// <summary>
        /// 重置<see cref="Transform"/>的本地坐标到(0,0,0)，本地旋转到(0,0,0)，本地缩放到(1,1,1)。
        /// </summary>
        public static void ResetLocal(this Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }
    }
}