namespace Muhe.Mjhx.Configs.Modules
{
    using System;
    using Mjhx.Modules.Controllers.Main;

    /// <summary>
    /// 主界面模块配置。
    /// </summary>
    public class MainModule : Module
    {
        /// <summary>
        /// 获取控制器类型。
        /// </summary>
        public override Type ControllerType
        {
            get { return typeof (MainController); }
        }

        /// <summary>
        /// 获取模块入口预设路径。
        /// </summary>
        public override string EntryPrefab
        {
            get { return "Prefabs/UI/Main/MainView"; }
        }

        /// <summary>
        /// 获取模块分层配置。
        /// </summary>
        public override ModuleLayer Layer
        {
            get { return ModuleLayer.Scene; }
        }
    }
}