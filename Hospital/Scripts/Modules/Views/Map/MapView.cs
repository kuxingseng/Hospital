using System.Collections.Generic;

namespace Assets.Hospital.Scripts.Modules.Views.Map
{
    using System.Collections;
    using System.Diagnostics;
    using AI;
    using Blaze.Framework;
    using Configs.Data.Buildings;
    using Controllers.Map;
    using Managers;
    using Muhe.Mjhx.Common;
    using Muhe.Mjhx.Modules;
    using Muhe.Mjhx.UI.Common;
    using UnityEngine;
    using UnityEngine.UI;
    using Debug = UnityEngine.Debug;

    /// <summary>
    /// author chenshuai
    /// $Id:$
    /// Create time:2017/3/27 16:21:26 
    /// Desc:
    /// </summary>
    ///
    public class MapView:ModuleView
    {
        private MapController mController;


        #region UI
        public Transform LandformLayer;   //地貌层
        public Transform GroundworkLayer; //建筑地基层
        public Transform BuildingMaterialsLayer; //建筑材料层 墙体等
        public Transform RoomLayer; //房间层
        public Transform PreSelectLayer;  //预选中层
        public Transform RoleLayer;   //角色层
        public Transform MouseEventLayer; //鼠标事件层

        [SerializeField]
        private MapCamera mMapCamera;

        [SerializeField]
        private Image mDragRect;

        [SerializeField]
        private GameObject mTileItem;
        [SerializeField]
        private Role mRole;
        #endregion

        private static readonly float mMapWidth = 6400f;
        private static readonly float mMapHeight = 6400f;
        private static readonly float mTileSize = 64f;

        private Vector2 mPointDownPosition;

        private bool mIsStopMove;   //是否停止移动

        private bool mIsMoveSelect; //是否在移动标识选中状态
        private int mPreMoveIndexX;
        private int mPreMoveIndexY;
        
        private readonly List<TileItem> mPreSelectTileItems = new List<TileItem>();    //选定的格子列表

        private BuildingsEntryInfo mCurrentBuildingsInfo;   //当前选中建材信息

        /// <summary>
        /// 获取视图上的控制器。
        /// </summary>
        public MapController Controller
        {
            get
            {
                if (mController == null)
                    mController = GameObject.GetOrAddComponent<MapController>();
                return mController;
            }
        }

        protected override void Awake()
        {
            mIsMoveSelect = false;
            LandformLayer.gameObject.SetActive(true);
            MouseEventLayer.gameObject.SetActive(false);

            //todo:据实际建筑类型控制选择点、线、片
            
            UGUIEventTrigger.Get(MouseEventLayer.gameObject).onPointerDown = (go, eventData) =>
            {
                //MyLogger.Debug("onPointerDown p:" + screenPoint2LocalPoint(eventData.position));
                //开始区域或者目标选择
                mPointDownPosition = screenPoint2LocalPoint(eventData.position);
                //mDragRect.rectTransform.localPosition = mPointDownPosition;
                createPreSelectTiles(getPointDownTileItem());
            };

            UGUIEventTrigger.Get(MouseEventLayer.gameObject).onDrag = (go, eventData) =>
            {
                mIsMoveSelect = false;
                //划定区域 隐藏
//                var currentMousePosition = screenPoint2LocalPoint(eventData.position);
//                var scaleX = 1;
//                var scaleY = 1;
//                mDragRect.rectTransform.sizeDelta = new Vector2(Mathf.Abs(currentMousePosition.x - mPointDownPosition.x), Mathf.Abs(currentMousePosition.y - mPointDownPosition.y));
//                if (currentMousePosition.x - mPointDownPosition.x < 0)
//                    scaleX = -1;
//                if (currentMousePosition.y - mPointDownPosition.y > 0)
//                    scaleY = -1;
//                mDragRect.rectTransform.localScale = new Vector3(scaleX, scaleY, 1);
//                if (!mDragRect.gameObject.activeSelf)
//                    mDragRect.gameObject.SetActive(true);

                //标示选中区域
                var tileItems = getRectTileItems(screenPoint2LocalPoint(eventData.position));
                createPreSelectTiles(tileItems);
            };

            UGUIEventTrigger.Get(MouseEventLayer.gameObject).onPointerUp = (go, eventData) =>
            {
                mIsMoveSelect = true;
                StartCoroutine(checkMouseSelect());
                //Logger.Debug("onPointerUp p:" + screenPoint2LocalPoint(eventData.position));
                //mDragRect.gameObject.SetActive(false);

                //创建建造命令并执行
                if (mPreSelectTileItems.Count > 0)
                {
                    var buildCommand = BuildCommandFactory.Create(Controller, mPreSelectTileItems, mCurrentBuildingsInfo);
                    if (buildCommand != null)
                    {
                        if (buildCommand.Prepare())
                            buildCommand.Execute();
                    }

                    PreSelectLayer.DestroyChildren();
                }
                else
                    MyLogger.Debug("no target");
            };
        }

        //判断当前鼠标经过的格子
        private IEnumerator checkMouseSelect()
        {
            while (mIsMoveSelect)
            {
                var tileItems = getMoveSelectTile(screenPoint2LocalPoint(Input.mousePosition));
                createPreSelectTiles(tileItems);
                yield return new WaitForSeconds(0.1f);
            }
            mPreMoveIndexX = -1;
            mPreMoveIndexY = -1;
            PreSelectLayer.DestroyChildren();
        }


        /// <summary>
        /// 地图初始化
        /// </summary>
        /// <returns></returns>
        public IEnumerator Init()
        {
            //初始化地图
            //yield return new WaitForEndOfFrame();
            var tileRowNum = mMapHeight / mTileSize;
            var tileColumnNum = mMapWidth / mTileSize;
            var time = new Stopwatch();
            time.Start();
            for (var i = 0; i < tileColumnNum; i++)
            {
                for (var j = 0; j < tileRowNum; j++)
                {
                    //var tile = Utils.CreateGameObject(mTileItem, mEditorLayer, new Vector3(mTileSize * i, -mTileSize * j, 0)).GetComponent<TileItem>();
                    var go = Instantiate(mTileItem);
                    go.transform.SetParent(LandformLayer);
                    go.transform.localScale = Vector3.one;
                    go.SetLayer(LandformLayer.gameObject.layer, true);
                    go.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(mTileSize * i, -mTileSize * j, 0);
                    var tileItem = go.AddComponent<TileItem>();
                    StartCoroutine(tileItem.SetData(i, j));

                    TilesManager.TileItemDicts.Add(new Vector2(i, j), tileItem);
                }
            }
            Debug.Log("time:" + (time.ElapsedMilliseconds));

            //初始化工人
            TileItem item = TilesManager.GetTileItem(75, 50);
            //mTileItemDicts.TryGetValue(new Vector2(75, 50), out item);
            
            WorkerManager.CreateWorker(RoleLayer, item);
            item = TilesManager.GetTileItem(76, 51);
            //mTileItemDicts.TryGetValue(new Vector2(76, 51), out item);
            WorkerManager.CreateWorker(RoleLayer, item);

            yield break;
        }

        /// <summary>
        /// 设置编辑模式
        /// </summary>
        /// <param name="isEditor"></param>
        /// <param name="buildingsEntryInfo"></param>
        public void Editor(bool isEditor,BuildingsEntryInfo buildingsEntryInfo)
        {
            mCurrentBuildingsInfo = buildingsEntryInfo;
            if (isEditor)
            {
                if (MouseEventLayer.gameObject.activeSelf==false)
                    MouseEventLayer.gameObject.SetActive(true);
                mIsMoveSelect = true;
                StartCoroutine(checkMouseSelect());
            }
            else
            {
                if (MouseEventLayer.gameObject.activeSelf)
                    MouseEventLayer.gameObject.SetActive(false);
                mIsMoveSelect = false;
            }
        }

        /// <summary>
        /// 设置摄像机视窗大小
        /// </summary>
        /// <param name="rectTransform"></param>
        public void SetSmartViewportTarget(RectTransform rectTransform)
        {
            mMapCamera.gameObject.AddComponent<SmartViewport>().Target = rectTransform;
            mMapCamera.SetMapRectTransform(transform.GetComponent<RectTransform>());
        }

        /// <summary>
        /// 移动地图
        /// </summary>
        /// <param name="direct"></param>
        public IEnumerator Move(MapMoveDirect direct)
        {
            mIsStopMove = false;
            while (!mIsStopMove)
            {
                mMapCamera.Move(direct);
                yield return null;
                //yield return null;
            }
            StopCoroutine("Move");
        }

        /// <summary>
        /// 停止移动
        /// </summary>
        public void StopMove()
        {
            mIsStopMove = true;
        }

        //生成并显示预选中格子
        private void createPreSelectTiles(List<TileItem> items)
        {
            if (items == null || items.Count <= 0)
                return;
            for (int i = 0; i < items.Count; i++)
            {
                var go = Instantiate(mTileItem);
                go.transform.SetParent(PreSelectLayer);
                go.transform.localScale = Vector3.one;
                go.SetLayer(PreSelectLayer.gameObject.layer, true);
                go.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(mTileSize * items[i].IndexX, -mTileSize * items[i].IndexY, 0);
                var tileItem = go.AddComponent<PreSelectTileItem>();
                tileItem.Show();
            }
        }

        //获取鼠标移动过程中经过的格子
        private List<TileItem> getMoveSelectTile(Vector2 position)
        {
            var tileIndexX = (int)((position.x + mMapWidth / 2) / mTileSize);
            var tileIndexY = (int)((mMapHeight / 2 - position.y) / mTileSize);

            if (mPreMoveIndexX == tileIndexX && mPreMoveIndexY == tileIndexY)
                return null;

            mPreMoveIndexX = tileIndexX;
            mPreMoveIndexY = tileIndexY;
            PreSelectLayer.DestroyChildren();

            var items=new List<TileItem>();
            TileItem tileItem = TilesManager.GetTileItem(tileIndexX, tileIndexY); ;
            if (tileItem!=null)
                items.Add(tileItem);
            return items;
        }

        private List<TileItem> getPointDownTileItem()
        {
            mPreSelectTileItems.Clear();
            var tileIndexX = (int)((mPointDownPosition.x + mMapWidth / 2) / mTileSize);
            var tileIndexY = (int)((mMapHeight / 2 - mPointDownPosition.y) / mTileSize);
            TileItem tileItem = TilesManager.GetTileItem(tileIndexX, tileIndexY); ;
            if (tileItem != null)
                mPreSelectTileItems.Add(tileItem);
            return mPreSelectTileItems;
        }

        //根据当前区域矩形形状获取所覆盖的格子
        private List<TileItem> getRectTileItems(Vector2 finalPoint)
        {
            //清除之前选中
            PreSelectLayer.DestroyChildren();
            mPreSelectTileItems.Clear();

            TileItem tileItem;

            var tileIndexX = (int)((mPointDownPosition.x + mMapWidth / 2) / mTileSize);
            var tileIndexY = (int)((mMapHeight / 2 - mPointDownPosition.y) / mTileSize);
            var tileFinalIndexX = (int)((finalPoint.x + mMapWidth / 2) / mTileSize);
            var tileFinalIndexY = (int)((mMapHeight / 2 - finalPoint.y) / mTileSize);
            //Logger.Debug(tileIndexX + " " + tileIndexY);

            if (tileIndexX == tileFinalIndexX && tileIndexY == tileFinalIndexY)
            {
                tileItem = TilesManager.GetTileItem(tileIndexX, tileIndexY); ;
                if (tileItem != null)
                    mPreSelectTileItems.Add(tileItem);
            }
            else
            {
                if (tileIndexX <= tileFinalIndexX)
                {
                    if (tileIndexY <= tileFinalIndexY)
                    {
                        //四象限方向
                        for (var i = tileIndexX; i <= tileFinalIndexX; i++)
                        {
                            for (var j = tileIndexY; j <= tileFinalIndexY; j++)
                            {
                                tileItem = TilesManager.GetTileItem(i, j); ;
                                if (tileItem != null)
                                {
                                    if (!mPreSelectTileItems.Contains(tileItem))
                                        mPreSelectTileItems.Add(tileItem);
                                }

                            }
                        }
                    }
                    else
                    {
                        //一象限方向
                        for (var i = tileIndexX; i <= tileFinalIndexX; i++)
                        {
                            for (var j = tileIndexY; j >= tileFinalIndexY; j--)
                            {
                                tileItem = TilesManager.GetTileItem(i, j); ;
                                if (tileItem != null)
                                {
                                    if (!mPreSelectTileItems.Contains(tileItem))
                                        mPreSelectTileItems.Add(tileItem);
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (tileIndexY <= tileFinalIndexY)
                    {
                        //三象限方向
                        for (var i = tileIndexX; i >= tileFinalIndexX; i--)
                        {
                            for (var j = tileIndexY; j <= tileFinalIndexY; j++)
                            {
                                tileItem = TilesManager.GetTileItem(i, j); ;
                                if (tileItem != null)
                                {
                                    if (!mPreSelectTileItems.Contains(tileItem))
                                        mPreSelectTileItems.Add(tileItem);
                                }
                            }
                        }
                    }
                    else
                    {
                        //二象限方向
                        for (var i = tileIndexX; i >= tileFinalIndexX; i--)
                        {
                            for (var j = tileIndexY; j >= tileFinalIndexY; j--)
                            {
                                tileItem = TilesManager.GetTileItem(i, j); ;
                                if (tileItem != null)
                                {
                                    if (!mPreSelectTileItems.Contains(tileItem))
                                        mPreSelectTileItems.Add(tileItem);
                                }
                            }
                        }
                    }
                }
            }

            if (mPreSelectTileItems.Count > 0)
                return mPreSelectTileItems;
            return null;
        }

        //屏幕坐标转换成本地坐标
        private Vector2 screenPoint2LocalPoint(Vector2 screenPoint)
        {
            Vector2 localPoint;
            //RectTransformUtility.ScreenPointToLocalPointInRectangle(mMouseEventLayer.GetComponent<RectTransform>(), screenPoint, Camera.main, out localPoint);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(MouseEventLayer.GetComponent<RectTransform>(), screenPoint, mMapCamera.Camera, out localPoint);
            return localPoint;
        }
    }
}
