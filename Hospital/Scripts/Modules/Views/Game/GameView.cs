namespace Assets.Hospital.Scripts.Modules.Views.Game
{
    using System.Collections;
    using System.Collections.Generic;
    using Blaze.Framework;
    using Configs.Data.Buildings;
    using Controllers.Game;
    using Map;
    using Models.Game;
    using Muhe.Mjhx.Modules;
    using Muhe.Mjhx.UI.Common;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

    /// <summary>
    /// author chenshuai
    /// $Id:$
    /// Create time:2017/3/20 15:24:37 
    /// Desc:
    /// </summary>
    ///
    public class GameView:ModuleView
    {
        #region UI

        [SerializeField]
        private RectTransform mBottomPanel;
        [SerializeField]
        private RectTransform mBorderUp;
        [SerializeField]
        private RectTransform mBorderDown;
        [SerializeField]
        private RectTransform mBorderRight;
        [SerializeField]
        private RectTransform mBorderLeft;
        
        [SerializeField]
        private BuildMenuContent mBuildMenuContent;
        #endregion

        /// <summary>
        /// 获取地图子视图。
        /// </summary>
        public MapView Map { get; private set; }

        private GameController mController;
        private MenuTypeEnum mCurrentMenuType=MenuTypeEnum.None;
       

        /// <summary>
        /// 获取视图上的控制器。
        /// </summary>
        public GameController Controller
        {
            get
            {
                if (mController == null)
                    mController = GameObject.GetOrAddComponent<GameController>();
                return mController;
            }
        }

        protected override void Awake()
        {
            UGUIEventTrigger.Get(mBorderLeft.gameObject).onEnter = (go,data) => StartCoroutine(Map.Move(MapMoveDirect.LEFT));
            UGUIEventTrigger.Get(mBorderDown.gameObject).onEnter = (go, data) => StartCoroutine(Map.Move(MapMoveDirect.DOWN));
            UGUIEventTrigger.Get(mBorderRight.gameObject).onEnter = (go, data) => StartCoroutine(Map.Move(MapMoveDirect.RIGHT));
            UGUIEventTrigger.Get(mBorderUp.gameObject).onEnter = (go, data) => StartCoroutine(Map.Move(MapMoveDirect.UP));
            UGUIEventTrigger.Get(mBorderUp.gameObject).onExit = go => Map.StopMove();
            UGUIEventTrigger.Get(mBorderDown.gameObject).onExit = go => Map.StopMove();
            UGUIEventTrigger.Get(mBorderRight.gameObject).onExit = go => Map.StopMove();
            UGUIEventTrigger.Get(mBorderLeft.gameObject).onExit = go => Map.StopMove();
        }

        /// <summary>
        /// 设置当前是否编辑状态
        /// </summary>
        /// <param name="isEditor"></param>
        /// <param name="buildingsEntryInfo"></param>
        public void SetEditor(bool isEditor, BuildingsEntryInfo buildingsEntryInfo =null)
        {
            //Debug.Log("map editor:"+isEditor);
            Map.Editor(isEditor, buildingsEntryInfo);
        }

        /// <summary>
        /// 打开指定菜单
        /// </summary>
        /// <param name="menuType"></param>
        /// <param name="items"></param>
        public void OpenMenu(MenuTypeEnum menuType,List<BuildingsEntryInfo> items)
        {
            if (mCurrentMenuType == menuType)
                return;
            mCurrentMenuType = menuType;

            StartCoroutine(mBuildMenuContent.SetMenu(items));
        }

        /// <summary>
        /// 关闭菜单
        /// </summary>
        public void CloseMenu()
        {
            mCurrentMenuType=MenuTypeEnum.None;
        }

        /// <summary>
        /// 当需要加载视图资源时调用此协同方法。
        /// </summary>
        /// <returns></returns>
        protected override IEnumerator OnLoadContent()
        {
            var request = SceneManager.LoadSceneAsync("Map", LoadSceneMode.Additive);
            while (!request.isDone)
                yield return null;
            Map = FindObjectOfType<MapView>();
            Map.SetSmartViewportTarget(transform.GetComponent<RectTransform>());
            //Map.SetSmartViewportTarget(mBottomPanel);
        }

        /// <summary>
        /// 当需要施放视图资源时调用此方法。
        /// </summary>
        protected override void OnUnloadContent()
        {
            if(Map!=null)
                Destroy(Map.gameObject);
        }
    }
}
