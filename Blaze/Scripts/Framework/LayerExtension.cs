namespace Blaze.Framework
{
    using UnityEngine;

    /// <summary>
    /// 为<see cref="GameObject"/>提供修改图层的扩展功能。
    /// </summary>
    public static class LayerExtension
    {
        /// <summary>
        /// 设置指定游戏对象到指定图层。
        /// </summary>
        /// <param name="target">目标游戏对象</param>
        /// <param name="layerName">图层名称</param>
        /// <param name="containChildren">是否一并修改其子对象</param>
        public static void SetLayer(this GameObject target, string layerName, bool containChildren)
        {
            target.layer = LayerMask.NameToLayer(layerName);
            if (containChildren)
            {
                foreach (Transform child in target.transform)
                    child.gameObject.SetLayer(layerName, true);
            }
        }

        /// <summary>
        /// 设置指定游戏对象到指定图层。
        /// </summary>
        /// <param name="target">目标游戏对象</param>
        /// <param name="layerId">图层编号</param>
        /// <param name="containChildren">是否一并修改其子对象</param>
        public static void SetLayer(this GameObject target, int layerId, bool containChildren)
        {
            target.layer = layerId;
            if (containChildren)
            {
                foreach (Transform child in target.transform)
                    child.gameObject.SetLayer(layerId, true);
            }
        }
    }
}