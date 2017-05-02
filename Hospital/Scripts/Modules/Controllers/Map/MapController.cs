namespace Assets.Hospital.Scripts.Modules.Controllers.Map
{
    using Muhe.Mjhx.Common;
    using Muhe.Mjhx.Modules;
    using Views.Map;

    /// <summary>
    /// author chenshuai
    /// $Id:$
    /// Create time:2017/3/27 16:21:47 
    /// Desc:
    /// </summary>
    ///
    public class MapController:ModuleController
    {
        public MapView View;

        protected override void Awake()
        {
            View = GetComponent<MapView>();
        }

        /// <summary>
        /// 初始化地图
        /// </summary>
        public void Init()
        {
            MyLogger.Debug("map init");
            StartCoroutine(View.Init());
        }
    }
}
