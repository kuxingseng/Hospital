namespace Blaze.Framework.UI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// ���ڹ���<see cref="UILayer"/>�ķ��������ࡣ
    /// </summary>
    public abstract class UILayerGroup
    {
        /// <summary>
        /// һ����������������<see cref="UILayer"/>������
        /// </summary>
        public const int Capacity = 100;

        /// <summary>
        /// ��ȡ�����÷������ʾ���ȼ������е�<see cref="UILayer"/>����ʾ���ȼ����������ֵ��
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
        /// ��ȡ���������е�<see cref="UILayer"/>����Ҫ���ⲿ�޸�������ϡ�
        /// </summary>
        public List<UILayer> Layers
        {
            get { return mLayers; }
        }

        /// <summary>
        /// �����������һ���µ�<see cref="UILayer"/>����������ʾ���ȼ�Ϊ�÷�����ߡ�
        /// </summary>
        /// <param name="callback">�����ɹ���Ļص�</param>
        /// <param name="factory">����ͼ��Ĺ���</param>
        /// <param name="args">��������</param>
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
        /// �������е�ͼ�㡣
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
        /// ��ȡ��һ��ͼ�����ʾ���ȼ���
        /// </summary>
        /// <returns></returns>
        protected virtual int GetNextLayerOrder()
        {
            if (mLayers.Count == 0)
                return 1;
            return 1 + mLayers.Last().Order;
        }

        /// <summary>
        /// ��<see cref="UILayer"/>������Ϻ���ô˷�����
        /// </summary>
        /// <param name="layer"></param>
        protected virtual void OnAddUILayer(UILayer layer)
        {
        }

        /// <summary>
        /// ���������ʾ���ȼ��ı�ʱ���ô˷�����
        /// </summary>
        /// <param name="oldValue">ԭ��ʾ���ȼ�</param>
        /// <param name="newValue">����ʾ���ȼ�</param>
        protected virtual void OnOrderChange(int oldValue, int newValue)
        {
            var delta = (newValue - oldValue) * Capacity;
            foreach (var layer in mLayers)
                layer.Order += delta;
        }

        /// <summary>
        /// ��ָ����<see cref="UILayer"/>�ƶ������������ʾ���ȼ���
        /// </summary>
        /// <param name="layer">ͼ��</param>
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
        /// �Ƴ�������ָ����<see cref="UILayer"/>��
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