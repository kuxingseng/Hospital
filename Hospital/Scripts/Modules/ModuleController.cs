namespace Muhe.Mjhx.Modules
{
    using System;
    using Blaze.Framework.Mvc;
    using Configs.Modules;
    using Controllers.Main;
    using DG.Tweening;
    using Managers;
    using UI.Common;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// 模块控制器的抽象基类，提供控制器的常用功能。
    /// </summary>
    public abstract class ModuleController : MonoController
    {
        /// <summary>
        /// 获取或设置控制器所对应的模块上下文。
        /// </summary>
        internal ModuleContext Context { get; set; }


        /// <summary>
        /// 关闭当前模块。
        /// </summary>
        public void Close()
        {
            OnClose();
            ModuleManager.Unload(Context);
//            if (mTipCount > 0)
//            {
//                mTipCount--;
//                if (mTipCount == 0)
//                {
//                    (UIManager.tipGroup as RectTransformLayerGroup).GroupTransform.localPosition = Vector3.zero;
//                    (UIManager.guideGroup as RectTransformLayerGroup).GroupTransform.localPosition = Vector3.zero;
//                }
//            }
            //CoroutineManager.Start(TransitionalManager.HideBlur());
        }

        #region Guiding

        /// <summary>
        /// 检查是否需要进入指定的新手引导阶段。
        /// </summary>
        /// <param name="phaseId">引导阶段编号</param>
        //        protected void CheckGuidePoint(int phaseId)
        //        {
        //            var context = ModuleManager.Load<GuideModule>();
        //            var controller = (GuideController) context.Controller;
        //            CoroutineManager.Start(controller.Execute(phaseId));
        //        }

        #endregion

        /// <summary>
        /// 获取模块名称。
        /// </summary>
        /// <returns></returns>
        protected virtual string GetModuleName()
        {
            var moduleName = GetType().Name.Replace("Controller", string.Empty);
            return moduleName;
        }

        /// <summary>
        /// 获取控制器所对应的视图。
        /// </summary>
        /// <returns></returns>
        protected ModuleView GetView()
        {
            return Context.View;
        }

        /// <summary>
        /// 当模块被关闭时调用此方法。
        /// </summary>
        protected virtual void OnClose()
        {
        }

        /// <summary>
        /// 弹出指定的模块。
        /// </summary>
        /// <typeparam name="T">模块类型</typeparam>
        protected void Popup<T>(Action<ModuleContext> callback = null) where T : Module, new()
        {
            mTipCount++;
            var lockId = InputManager.Lock();
            ModuleManager.Load<T>(context =>
            {
                InputManager.Unlock(lockId);
                if (callback != null)
                    callback(context);
            });
        }

        /// <summary>
        /// 弹出指定的 对话框模块
        ///  此处会自动添加对话框半透明背景遮罩，并处理点击遮罩关闭
        ///  所有使用当前类型弹框的内容放在Content节点下
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="callback"></param>
        /// <param name="isShowEffect">是否使用出现效果</param>
        /// <param name="isCanClickClose">是否可以点击空白处关闭</param>
        /// <param name="isShowTips">是否显示点击关闭提示，默认不提示</param>
        protected void PopupPanel<T>(Action<ModuleContext> callback = null, bool isShowEffect = true, bool isCanClickClose = true, bool isShowTips = false) where T : Module, new()
        {
//            if ((UIManager.tipGroup as RectTransformLayerGroup).GroupTransform.localPosition != new Vector3(0, 0, -1500))
//            {
//                (UIManager.tipGroup as RectTransformLayerGroup).GroupTransform.localPosition = new Vector3(0, 0, -1500);
//                (UIManager.guideGroup as RectTransformLayerGroup).GroupTransform.localPosition = new Vector3(0, 0, -1500);
//            }

            Popup<T>(context =>
            {
                var clickHandler = new GameObject("ClickHandler");
                clickHandler.transform.SetParent(context.View.transform);
                clickHandler.transform.SetAsFirstSibling();
                clickHandler.transform.localScale = Vector3.one;
                clickHandler.transform.localPosition = Vector3.zero;

                var iamge = clickHandler.AddComponent<Image>();
                iamge.color = new Color(0, 0, 0, 0.7f);

                var clickRect = clickHandler.GetComponent<RectTransform>();
                clickRect.sizeDelta = Vector2.zero;
                clickRect.anchorMin = Vector2.zero;
                clickRect.anchorMax = Vector2.one;

                if (isShowEffect)
                {
                    var rectTransform = context.View.transform.Find("Content").GetComponent<RectTransform>();
                    rectTransform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    rectTransform.DOScale(Vector3.one, 0.15f).SetEase(Ease.InOutBack);
                }

                if (callback != null)
                    callback(context);

                if (isCanClickClose)
                {
                    if (isShowTips)
                    {
                        var tips = new GameObject("tips");
                        tips.transform.SetParent(clickHandler.transform);
                        tips.transform.localScale = Vector3.one;
                        tips.transform.localPosition = Vector3.zero;
                        var textTips = tips.AddComponent<Text>();
                        textTips.text = "点击屏幕空白处关闭";
                        textTips.rectTransform.sizeDelta = new Vector2(1000, 200);
                        textTips.rectTransform.anchoredPosition = new Vector2(0, -830);
                        textTips.alignment = TextAnchor.MiddleCenter;
                        textTips.fontSize = 40;
                        var outLine = textTips.gameObject.AddComponent<Outline>();
                        outLine.effectColor = new Color(0, 0, 0, 255);
                        outLine.effectDistance = new Vector2(2, 2);
                    }

                    UGUIButtonEventTrigger.Get(clickHandler).OnClick = go =>
                    {
                        context.Controller.Close();
                    };
                }
            });
        }

        private Blaze.Framework.Logging.ILogger mLogger;
        private static int mTipCount = 0;

        #region 作为子模块使用

        /// <summary>
        /// 获取或设置父容器控制器。
        /// </summary>
        public MainController Container { get; set; }

        /// <summary>
        /// 当前模块关闭是是否直接返回主界面，或者返回上级界面
        /// </summary>
        public bool IsBackToHome { get; set; }

        /// <summary>
        /// 当前模块是否可相应关闭（模块正在处理某种不可打断的操作）
        /// </summary>
        public virtual bool IsCanBack
        {
            get { return true; }
            set { }
        }

        /// <summary>
        /// 隐藏界面
        /// </summary>
        public virtual void Hide() { }

        /// <summary>
        /// 重写回退到上级界面方法
        /// </summary>
        /// <returns>是否执行默认回退方法</returns>
        public virtual bool Back()
        {
            return true;
        }

        #endregion
    }
}