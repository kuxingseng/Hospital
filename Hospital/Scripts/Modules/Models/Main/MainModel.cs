namespace Muhe.Mjhx.Modules.Models.Main
{
    using Controllers.Main;

    /// <summary>
    /// author chenshuai
    /// $Id:$
    /// Create time:2016/11/29 10:44:52 
    /// Desc:主界面滚动消息始终接收消息，使用单例类
    /// 在控制器中定期取消息队列中的消息进行显示
    /// </summary>
    ///
    public class MainModel:ModuleModel
    {
        /// <summary>
        /// 主界面控制器
        /// </summary>
        public MainController Controller;

        
    }
}
