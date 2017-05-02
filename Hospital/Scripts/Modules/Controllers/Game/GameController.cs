namespace Assets.Hospital.Scripts.Modules.Controllers.Game
{
    using Map;
    using Muhe.Mjhx.Common;
    using Muhe.Mjhx.Configs.Modules;
    using Muhe.Mjhx.Managers;
    using Muhe.Mjhx.Modules;
    using Views.Game;

    /// <summary>
    /// author chenshuai
    /// $Id:$
    /// Create time:2017/3/20 15:25:21 
    /// Desc:
    /// </summary>
    ///
    public class GameController:ModuleController
    {
        private GameView mView;

        /// <summary>
        /// 获取地图控制器
        /// </summary>
        public MapController Map { get; private set; }

        protected override void Awake()
        {
            mView = GetComponent<GameView>();
            //AstarPath.active.Scan();

            ModuleManager.Load<MainModule>(context => { });
        }

        public void Init()
        {
            MyLogger.Debug("game view init");
            //StartCoroutine(mView.Init());

            Map = mView.Map.Controller;
            Map.Init();
        }
    }
}
