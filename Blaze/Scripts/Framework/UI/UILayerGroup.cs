namespace Blaze.Framework.UI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// 用于管理<see cref="UILayer"/>的分组抽象基类。
    /// </summary>
    public abstract class UILayerGroup
    {
        /// <summary>
        /// 一个分组中最大包含的<see cref="UILayer"/>数量。
        /// </summary>
        public const int Capacity = 100;

        /// <summary>
        /// 获取或设置分组的显示优先级，所有的<see cref="UILayer"/>的显示优先级都基于这个值。
        /// </summary>
        public int Order
        {
            get { return mOrder; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("value");
                if (mOrder == value)
                    return;
                OnOrderChange(mOrder, value);
                mOrder = value;
            }
        }

        /// <summary>
        /// 获取分组内所有的<see cref="UILayer"/>，不要在外部修改这个集合。
        /// </summary>
        public List<UILayer> Layers
        {
            get { return mLayers; }
        }

        /// <summary>
        /// 向分组中增加一个新的<see cref="UILayer"/>，并令其显示优先级为该分组最高。
        /// </summary>
        /// <param name="callback">创建成功后的回调</param>
        /// <param name="factory">创建图层的工厂</param>
        /// <param name="args">创建参数</param>
        public void Add(Action<UILayer> callback, UILayerFactory factory, params object[] args)
        {
            var order = GetNextLayerOrder();
            factory.Create(order, layer =>
            {
                layer.Group = this;
                mLayers.Add(layer);
                OnAddUILayer(layer);
                if (callback != null)
                    callback(layer);
            }, args);
        }

        /// <summary>
        /// 清理所有的图层。
        /// </summary>
        public void Clear()
        {
            while (mLayers.Count > 0)
            {
                var layer = mLayers.Last();
                mLayers.RemoveAt(mLayers.Count - 1);
                layer.Destroy();
            }
        }

        /// <summary>
        /// 获取下一个图层的显示优先级。
        /// </summary>
        /// <returns></returns>
        protected virtual int GetNextLayerOrder()
        {
            if (mLayers.Count == 0)
                return 1;
            return 1 + mLayers.Last().Order;
        }

        /// <summary>
        /// 当<see cref="UILayer"/>创建完毕后调用此方法。
        /// </summary>
        /// <param name="layer"></param>
        protected virtual void OnAddUILayer(UILayer layer)
        {
        }

        /// <summary>
        /// 当分组的显示优先级改变时调用此方法。
        /// </summary>
        /// <param name="oldValue">原显示优先级</param>
        /// <param name="newValue">新显示优先级</param>
        protected virtual void OnOrderChange(int oldValue, int newValue)
        {
            var delta = (newValue - oldValue) * Capacity;
            foreach (var layer in mLayers)
                layer.Order += delta;
        }

        /// <summary>
        /// 将指定的<see cref="UILayer"/>移动到分组最高显示优先级。
        /// </summary>
        /// <param name="layer">图层</param>
        internal void BringToFront(UILayer layer)
        {
            if (layer.Group != this)
                throw new ArgumentException("layer");

            if (mLayers.Count <= 1)
                return;

            mLayers.Remove(layer);
            layer.Order = mLayers.Last().Order + 1;
            mLayers.Add(layer);
        }

        /// <summary>
        /// 移除分组中指定的<see cref="UILayer"/>。
        /// </summary>
        /// <param name="layer"></param>
        internal void Remove(UILayer layer)
        {
            mLayers.Remove(layer);
        }

        private readonly List<UILayer> mLayers = new List<UILayer>();
        private int mOrder;
    }
}