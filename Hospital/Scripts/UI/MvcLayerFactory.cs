namespace Muhe.Mjhx.UI
{
    using System;
    using Blaze.Framework;
    using Blaze.Framework.Mvc;
    using Blaze.Framework.OperationQueues;
    using Blaze.Framework.UI;
    using UnityEngine;
    using Object = UnityEngine.Object;

    /// <summary>
    /// 用于创建Mvc结构图层的工厂。
    /// </summary>
    public class MvcLayerFactory : UILayerFactory
    {
        /// <summary>
        /// 创建一个<see cref="UILayer"/>。
        /// </summary>
        /// <param name="order">图层的显示优先级</param>
        /// <param name="callback">创建完成后的回调</param>
        /// <param name="args">创建图层的参数</param>
        public override void Create(int order, Action<UILayer> callback, params object[] args)
        {
            var path = getArgument<string>(args, 0);
            var controllerType = getArgument<Type>(args, 1);

            var prefab = Resources.Load<GameObject>(path);
            if (prefab == null)
            {
                Debug.Log("Can't find prefab in path :"+ path);
                return;
            }

            var viewObj = (GameObject) Object.Instantiate(prefab);/////////////////////////////////////
            viewObj.transform.localPosition = Vector3.zero;
            var view = viewObj.GetComponent<MonoView>();
            if (view == null)
            {
                Debug.Log("Can't find view comonent in prefab path="+path);
                return;
            }

            var layer = new RectTransformLayer(view.RectTransform)
            {
                Order = order,
                IsVisible = false,
            };
            view.Layer = layer;

            var loadContentOperation = view.LoadContent();
            loadContentOperation.OnSucceed(() =>
            {
                layer.IsVisible = true;
                if (controllerType != null)
                    view.GameObject.AddComponent(controllerType);
                if (callback != null)
                    callback(layer);
            });
            mLoadingQueue.Enqueue(loadContentOperation);
        }

        private static T getArgument<T>(object[] args, int index)
        {
            if (index < args.Length)
                return (T) args[index];
            return default(T);
        }

        private readonly MainThreadOperationQueue mLoadingQueue = new MainThreadOperationQueue(BlazeEngine.Instance);

    }
}