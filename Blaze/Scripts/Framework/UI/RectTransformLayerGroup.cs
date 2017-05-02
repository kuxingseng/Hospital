namespace Blaze.Framework.UI
{
    using UnityEngine;

    /// <summary>
    /// 用于管理<see cref="RectTransformLayerGroup"/>的分组。
    /// </summary>
    public class RectTransformLayerGroup : UILayerGroup
    {
        /// <summary>
        /// 构造一个<see cref="RectTransformLayerGroup"/>。
        /// </summary>
        /// <param name="rootTransform"></param>
        /// <param name="name">分组名称</param>
        public RectTransformLayerGroup(Transform rootTransform, string name)
        {
            GroupTransform = new GameObject(name).AddComponent<RectTransform>();
            GroupTransform.SetParent(rootTransform, false);
            GroupTransform.anchorMin = Vector2.zero;
            GroupTransform.anchorMax = Vector2.one;
            GroupTransform.offsetMin = Vector2.zero;
            GroupTransform.offsetMax = Vector2.zero;

//            if (name == "LayerGroup - Tips" || name == "LayerGroup - Guide")
//                GroupTransform.localPosition=new Vector3(0,0,-1500);
        }

        /// <summary>
        /// 获取下一个图层的显示优先级。
        /// </summary>
        /// <returns></returns>
        protected override int GetNextLayerOrder()
        {
            return Order + base.GetNextLayerOrder();
        }

        /// <summary>
        /// 当<see cref="UILayer"/>创建完毕后调用此方法。
        /// </summary>
        /// <param name="layer"></param>
        protected override void OnAddUILayer(UILayer layer)
        {
            var canvasLayer = (RectTransformLayer) layer;
            canvasLayer.SetParent(GroupTransform);
        }

        public readonly RectTransform GroupTransform;
    }
}