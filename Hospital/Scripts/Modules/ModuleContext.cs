namespace Muhe.Mjhx.Modules
{
    using System;
    using Configs.Modules;
    using Models;
    using UnityEngine;

    /// <summary>
    /// 模块实例上下文。
    /// </summary>
    public class ModuleContext
    {
        /// <summary>
        /// 获取或设置模块配置。
        /// </summary>
        public Module Config { get; set; }

        /// <summary>
        /// 获取或设置模块所使用的控制器。
        /// </summary>
        public ModuleController Controller
        {
            get
            {
                if (mController == null)
                {
                    if (Root != null)
                        mController = Root.GetComponent<ModuleController>();
                }
                return mController;
            }
        }

        /// <summary>
        /// 获取或设置模块所使用的模型。
        /// </summary>
        public ModuleModel Model { get; set; }

        /// <summary>
        /// 获取或设置模块所对应的视图的根游戏对象。
        /// </summary>
        public GameObject Root
        {
            get { return mRoot; }
            set
            {
                if (mRoot != null)
                    throw new NotSupportedException();
                mRoot = value;
            }
        }

        /// <summary>
        /// 获取或设置模块所使用的视图。
        /// </summary>
        public ModuleView View
        {
            get
            {
                if (mView == null)
                {
                    if (Root != null)
                        mView = Root.GetComponent<ModuleView>();
                }
                return mView;
            }
        }

        private ModuleController mController;
        private ModuleView mView;
        private GameObject mRoot;
    }
}