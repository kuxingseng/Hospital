using System;

namespace Assets.Hospital.Scripts.Configs.Modules
{
    using Muhe.Mjhx.Configs.Modules;
    using Scripts.Modules.Controllers.Game;

    /// <summary>
    /// author chenshuai
    /// $Id:$
    /// Create time:2017/3/20 15:27:53 
    /// Desc:
    /// </summary>
    ///
    public class GameModule:Module
    {
        /// <summary>
        /// 获取控制器类型。
        /// </summary>
        public override Type ControllerType
        {
            get { return typeof(GameController); }
        }

        /// <summary>
        /// 获取模块入口预设路径。
        /// </summary>
        public override string EntryPrefab
        {
            get { return "Prefabs/UI/Game/GameView"; }
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
