namespace Muhe.Mjhx.Modules.Controllers.Main
{
    using Views.Main;

    /// <summary>
    /// 主页控制器。
    /// </summary>
    public class MainController : ModuleController
    {
        private MainView mView;

        
        protected override void Start()
        {
            mView = GetComponent<MainView>();
        }
    }
}