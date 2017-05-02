namespace Assets.Hospital.Scripts.Modules.Controllers.Tips
{
    using System;
    using Models.Tips;
    using Muhe.Mjhx.Modules;
    using Views.Tips;

    /// <summary>
    /// author chenshuai
    /// $Id:$
    /// Create time:2017/4/20 16:16:53 
    /// Desc:
    /// </summary>
    ///
    public class TextTipsController:ModuleController
    {
        private TextTipsView mView;
        private TextTipsModel mModel;

        protected override void Awake()
        {
            base.Awake();
            mView = GetComponent<TextTipsView>();
        }

        /// <summary>
        /// 确认并结束提示。
        /// </summary>
        public void Confirm()
        {
            Close();
            if (mModel != null && mModel.Callback != null)
                mModel.Callback();
        }

        /// <summary>
        /// 显示指定的文本提示。
        /// </summary>s
        /// <param name="model">提示模型</param>
        public void ShowTip(TextTipsModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");
            mModel = model;
            StartCoroutine(mView.SetContent(model.Text, model.IsAutoDisappear));
        }
    }
}
