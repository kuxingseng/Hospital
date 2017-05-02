namespace Muhe.Mjhx.Modules.Views.Main
{
    using Blaze.Framework;
    using Controllers.Main;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// 主界面视图。
    /// </summary>
    public class MainView : ModuleView
    {
        #region UI
        
        #endregion

        private MainController mController;
        
        /// <summary>
        /// 获取视图上的控制器。
        /// </summary>
        public MainController Controller
        {
            get
            {
                if (mController == null)
                    mController = GameObject.GetOrAddComponent<MainController>();
                return mController;
            }
        }

        /// <summary>
        /// 当视图被遮挡的状态变更时调用此方法。
        /// </summary>
        /// <param name="isOverlayed">是否被遮挡</param>
        internal override void OnOverlay(bool isOverlayed)
        {
        }

        protected override void Start()
        {
            
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
        
    }
}