namespace Blaze.Framework
{
    using UnityEngine;

    /// <summary>
    /// 为<see cref="GameObject"/>提供常用的扩展功能。
    /// </summary>
    public static class GameObjectExtension
    {
        /// <summary>
        /// 获取或增加一个指定的组件。
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        /// <returns>组件</returns>
        public static T GetOrAddComponent<T>(this GameObject obj) where T : Component
        {
            var component = obj.GetComponent<T>();
            if (component == null)
                component = obj.AddComponent<T>();
            return component;
        }
    }
}