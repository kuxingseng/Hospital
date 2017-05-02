namespace Blaze.Framework.UI
{
    /// <summary>
    /// UI界面图层抽象基类。
    /// </summary>
    public abstract class UILayer
    {
        /// <summary>
        /// 获取或设置一个值，表示该图层是否在激活状态。
        /// </summary>
        public abstract bool IsActive { get; set; }

        /// <summary>
        /// 获取或设置一个值，表示图层是否可见。
        /// </summary>
        public abstract bool IsVisible { get; set; }

        /// <summary>
        /// 获取或设置<see cref="UILayer"/>的层级。
        /// </summary>
        public abstract int Order { get; set; }

        /// <summary>
        /// 获取或设置<see cref="UILayer"/>所属的<see cref="UILayerGroup"/>。
        /// </summary>
        public UILayerGroup Group
        {
            get { return mGroup; }
            set
            {
                if (mGroup == value)
                    return;
                mGroup = value;
                OnGroupChange(mGroup, value);
            }
        }

        /// <summary>
        /// 将图层移动到其所属的<see cref="UILayerGroup"/>的最高显示优先级。
        /// </summary>
        public void BringToFront()
        {
            if (Group == null)
                return;
            Group.BringToFront(this);
        }

        /// <summary>
        /// 摧毁当前图层。
        /// </summary>
        public void Destroy()
        {
            if (Group != null)
                Group.Remove(this);
            OnDestroy();
        }

        /// <summary>
        /// 当图层被摧毁时调用此方法。
        /// </summary>
        protected virtual void OnDestroy()
        {
        }

        /// <summary>
        /// 当所属的分组变更时调用此方法。
        /// </summary>
        /// <param name="oldValue">原分组</param>
        /// <param name="newValue">新分组</param>
        protected virtual void OnGroupChange(UILayerGroup oldValue, UILayerGroup newValue)
        {
        }

        private UILayerGroup mGroup;
    }
}