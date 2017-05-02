namespace Assets.Hospital.Scripts.Managers
{
    using System;
    using Configs.Modules;
    using Modules.Controllers.Tips;
    using Modules.Models.Tips;
    using Muhe.Mjhx.Managers;

    /// <summary>
    /// author chenshuai
    /// $Id:$
    /// Create time:2017/4/20 17:33:38 
    /// Desc:
    /// </summary>
    ///
    public static class TipsManager
    {
        /// <summary>
        /// 显示文本提示
        /// </summary>
        /// <param name="text"></param>
        /// <param name="callback"></param>
        /// <param name="isAutoDisappear"></param>
        public static void ShowTextTips(string text, Action callback = null, bool isAutoDisappear = true)
        {
            var lockId = InputManager.Lock();
            ModuleManager.Load<TextTipsModule>(context =>
            {
                InputManager.Unlock(lockId);
                var tipController = (TextTipsController)context.Controller;
                var model = new TextTipsModel()
                {
                    Text = text,
                    Callback = callback,
                    IsAutoDisappear = isAutoDisappear
                };
                tipController.ShowTip(model);
            });
        }
    }
}
