namespace Blaze.Framework.UI
{
    using UnityEngine;

    /// <summary>
    /// ���ڹ���<see cref="RectTransformLayerGroup"/>�ķ��顣
    /// </summary>
    public class RectTransformLayerGroup : UILayerGroup
    {
        /// <summary>
        /// ����һ��<see cref="RectTransformLayerGroup"/>��
        /// </summary>
        /// <param name="rootTransform"></param>
        /// <param name="name">��������</param>
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
        /// ��ȡ��һ��ͼ�����ʾ���ȼ���
        /// </summary>
        /// <returns></returns>
        protected override int GetNextLayerOrder()
        {
            return Order + base.GetNextLayerOrder();
        }

        /// <summary>
        /// ��<see cref="UILayer"/>������Ϻ���ô˷�����
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