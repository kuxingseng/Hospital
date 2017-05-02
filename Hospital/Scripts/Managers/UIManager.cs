namespace Muhe.Mjhx.Managers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Blaze.Framework.UI;
    using Configs.Modules;
    using Modules;
    using UI;
    using UnityEngine;

    /// <summary>
    /// 界面管理器。
    /// </summary>
    public static class UIManager
    {
        public static UILayerGroup sceneGroup;
        public static UILayerGroup tipGroup;
        public static UILayerGroup guideGroup;

        private static Camera mUIEffectCamera;

        /// <summary>
        /// 关闭指定的游戏对象所对应的图层。
        /// </summary>
        /// <param name="obj">游戏对象</param>
        public static void Close(GameObject obj)
        {
            if (obj == null)
                return;
            var view = obj.GetComponent<ModuleView>();
            var previousLayer = getPreviousLayer(view.Layer);
            //处理场景遮挡通知
            if (previousLayer != null)
            {
                var previousLayerView = previousLayer.Root.GetComponent<ModuleView>();
                previousLayerView.OnOverlay(false);
            }
            //处理提示遮挡通知
            if (view.Layer.Group == mLayerMap[ModuleLayer.Tip] && checkIsNewOverlayer(view.Layer))
                notifyTopLayerOverlay(ModuleLayer.Scene, false);
            view.Layer.Destroy();
        }

        /// <summary>
        /// 创建指定的界面。
        /// </summary>
        /// <param name="path">模块所对应的资源路径</param>
        /// <param name="callback">创建成功后的回调</param>
        public static void Create(string path, Action<GameObject> callback)
        {
            mFactory.Create(0, layer =>
            {
                var rectTransformLayer = (RectTransformLayer) layer;
                if (callback != null)
                    callback(rectTransformLayer.Root.gameObject);
            }, path);
        }

        /// <summary>
        /// 初始化界面管理器。
        /// </summary>
        /// <param name="canvas">界面所使用的根<see cref="Canvas"/>对象</param>
        /// <param name="uiEffectCamera">界面特效摄像机</param>
        public static void Initialize(Canvas canvas,Camera uiEffectCamera)
        {
            mUIEffectCamera = uiEffectCamera;
            var root = new CanvasLayerRoot(canvas);
            sceneGroup = root.CreateGroup(0, "LayerGroup - Scenes");
            tipGroup = root.CreateGroup(1, "LayerGroup - Tips");
            guideGroup = root.CreateGroup(2, "LayerGroup - Guide");

            mLayerMap.Add(ModuleLayer.Scene, sceneGroup);
            mLayerMap.Add(ModuleLayer.Tip, tipGroup);
            mLayerMap.Add(ModuleLayer.Guide, guideGroup);
        }

        /// <summary>
        /// 设置ui特效是否可见
        /// </summary>
        /// <param name="isVisible"></param>
        public static void SetUIEffectVisible(bool isVisible)
        {
            if (isVisible)
                mUIEffectCamera.depth = 4;
            else
                mUIEffectCamera.depth = 2;
        }

        /// <summary>
        /// 在指定模块层弹出一个新的界面。
        /// </summary>
        /// <param name="moduleLayer">模块分层</param>
        /// <param name="path">模块所对应的资源路径</param>
        /// <param name="callback">创建成功后的回调</param>
        public static void Popup(ModuleLayer moduleLayer, string path, Action<RectTransformLayer> callback)
        {
            UILayerGroup layerGroup;
            if (!mLayerMap.TryGetValue(moduleLayer, out layerGroup))
            {
                Debug.Log("Undefined module layer -> "+ moduleLayer);
                return;
            }
            layerGroup.Add(layer =>
            {
                //处理场景遮挡通知
                if (layerGroup.Layers.Count > 1)
                {
                    var lowerLayer = (RectTransformLayer) layerGroup.Layers[layerGroup.Layers.Count - 2];
                    var view = lowerLayer.Root.GetComponent<ModuleView>();
                    view.OnOverlay(true);
                }

                //处理提示遮挡通知
                if (moduleLayer == ModuleLayer.Tip && checkIsNewOverlayer(layer))
                    notifyTopLayerOverlay(ModuleLayer.Scene, true);

                //图层创建成功回调
                var rectTransformLayer = (RectTransformLayer) layer;
                if (callback != null)
                    callback(rectTransformLayer);
            }, mFactory, path);
        }

        /// <summary>
        /// 设置指定的层是否可见，用于集中管理图层遮挡。
        /// </summary>
        /// <param name="layer">图层</param>
        /// <param name="isVisible">是否可见</param>
        public static void SetVisible(UILayer layer, bool isVisible)
        {
            //loading及界面切换效果 不发送遮挡事件
//            if (isVisible)
//            {
//                //显示
//                //处理提示遮挡通知
//                if (layer.Group == mLayerMap[ModuleLayer.Tip] && checkIsNewOverlayer(layer))
//                    notifyTopLayerOverlay(ModuleLayer.Scene, true);
//            }
//            else
//            {
//                //隐藏
//                //处理提示遮挡通知
//                if (layer.Group == mLayerMap[ModuleLayer.Tip])
//                    notifyTopLayerOverlay(ModuleLayer.Scene, false);
//            }
            layer.IsVisible = isVisible;
        }

        //判断是否当前层级的第一个可见界面（当tip层有一个界面打开时再有新界面打开不会发送覆盖事件-针对弹窗上弹出tip问题）
        private static bool checkIsNewOverlayer(UILayer currentLayer)
        {
            for (var i = 0; i < currentLayer.Group.Layers.Count - 1; i++)
            {
                if (currentLayer.Group.Layers[i].IsVisible)
                    return false;
            }
            return true;
        }

        private static RectTransformLayer getPreviousLayer(UILayer layer)
        {
            if (layer.Group == null)
                return null;
            var index = layer.Group.Layers.IndexOf(layer) - 1;
            if (index < 0)
                return null;
            return (RectTransformLayer) layer.Group.Layers[index];
        }

        private static void notifyTopLayerOverlay(ModuleLayer layer, bool isOverlay)
        {
            var topLayer = (RectTransformLayer) mLayerMap[layer].Layers.LastOrDefault();
            if (topLayer != null)
            {
                var sceneView = topLayer.Root.GetComponent<ModuleView>();
                sceneView.OnOverlay(isOverlay);
            }
        }

        private static readonly UILayerFactory mFactory = new MvcLayerFactory();
        private static readonly Dictionary<ModuleLayer, UILayerGroup> mLayerMap = new Dictionary<ModuleLayer, UILayerGroup>();

    }
}