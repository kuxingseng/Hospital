namespace Assets.Hospital.Scripts.Configs.Modules
{
    using System;
    using Muhe.Mjhx.Configs.Modules;
    using Scripts.Modules.Controllers.Tips;

    /// <summary>
    /// author chenshuai
    /// $Id:$
    /// Create time:2017/4/20 16:26:36 
    /// Desc:
    /// </summary>
    ///
    public class TextTipsModule:Module
    {
        /// <summary>
        /// 获取控制器类型。
        /// </summary>
        public override Type ControllerType
        {
            get { return typeof(TextTipsController); }
        }

        /// <summary>
        /// 获取模块入口预设路径。
        /// </summary>
        public override string EntryPrefab
        {
            get { return "Prefabs/UI/Tips/TextTipsView"; }
        }

        /// <summary>
        /// 获取模块分层配置。
        /// </summary>
        public override ModuleLayer Layer
        {
            get { return ModuleLayer.Tip; }
        }
    }
}
