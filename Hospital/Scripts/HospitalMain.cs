namespace Muhe.Mjhx
{
    using Assets.Hospital.Scripts.Configs.Modules;
    using Assets.Hospital.Scripts.Managers;
    using Assets.Hospital.Scripts.Modules.Controllers.Game;
    using Blaze.Framework;
    using DG.Tweening;
    using Managers;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// 游戏主入口，用于初始化整个游戏，并加载引导模块。
    /// </summary>
    public class HospitalMain : MonoBehaviour
    {
        public Canvas UIRootCanvas;
        public Camera UIEffectCamera;

        protected void Awake()
        {
            BlazeEngine.Initialize();
            DOTween.Init();
            UIManager.Initialize(UIRootCanvas, UIEffectCamera);
            InputManager.Initialize(UIRootCanvas.GetComponent<GraphicRaycaster>());
            WorkerManager.Init();
            Debug.logger.logEnabled = Debug.isDebugBuild;
        }

        protected void Start()
        {
            ModuleManager.Load<GameModule>(context => ((GameController)context.Controller).Init());
        }

        protected void OnDestory()
        {
            
        }

        protected void OnApplicationPause(bool isPause)
        {
            if(isPause)
                Application.targetFrameRate = 1;
            else
                Application.targetFrameRate = 30;
        }
    }
}