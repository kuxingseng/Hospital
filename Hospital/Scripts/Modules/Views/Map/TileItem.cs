namespace Assets.Hospital.Scripts.Modules.Views.Map
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using BuildingItems;
    using Configs.Data.Buildings;
    using Managers;
    using Models.Map;
    using Muhe.Mjhx.Common;
    using UnityEngine;
    using UnityEngine.UI;
    using Random = UnityEngine.Random;

    /// <summary>
    /// author chenshuai
    /// $Id:$
    /// Create time:2017/3/20 18:36:28 
    /// Desc:
    /// </summary>
    ///
    public class TileItem:MonoBehaviour
    {
        private Transform mTransform;
        private Image mImage;

        public int IndexX;
        public int IndexY;

        private BuildingItem mPreBuildingItem;   //当前正在建造的建造物
        private GroundworkItem mGroundworkItem;  //地基
        public WallItem WallItem;  //墙体
        public DoorItem DoorItem;   //门
        public RoomItem RoomItem;   //房间

        /// <summary>
        /// 是否有地基
        /// </summary>
        public bool HaveGroundwork{get { return mGroundworkItem != null; }}

        /// <summary>
        /// 是否有墙体
        /// </summary>
        public bool HaveWall { get { return WallItem != null; } }

        /// <summary>
        /// 是否有墙体
        /// </summary>
        public bool HaveDoor { get { return DoorItem != null; } }

        /// <summary>
        /// 是否有建材
        /// </summary>
        public bool HaveBuildingMaterials { get { return WallItem != null || DoorItem != null; } }

        /// <summary>
        /// 是否属于房间的一部分
        /// </summary>
        public bool BelongRoom { get { return RoomItem != null; } }

        /// <summary>
        /// 位置信息
        /// </summary>
        public Vector3 Position { get { return mTransform.localPosition; } }

        private void Awake()
        {
            mTransform = transform;
            mImage = mTransform.GetComponent<Image>();
        }

        /// <summary>
        /// 设置位置信息并初始化地貌
        /// </summary>
        /// <param name="indexX"></param>
        /// <param name="indexY"></param>
        /// <returns></returns>
        public IEnumerator SetData(int indexX, int indexY)
        {
            IndexX = indexX;
            IndexY = indexY;

            string tileName;
            if (Random.Range(0, 10) < 5)
                tileName = "tile3";
            else
                tileName = "tile4";
            mImage.sprite = Resources.Load<SpriteRenderer>("Sprites/Landform/" + tileName).sprite;
            yield break;
        }

        /// <summary>
        /// 预建造
        /// </summary> 
        /// <param name="commandType"></param>
        /// <param name="item"></param>
        /// <param name="contents">建造物所包含的格子（房间等占多个格子）</param>
        public void PreBuild(BuildCommandType commandType, BuildingItem item, List<TileItem> contents=null)
        {
            //生成新的建造物并放置在指定层
            switch (commandType)
            {
                case BuildCommandType.BuildGroundwork:
                    mGroundworkItem = (GroundworkItem)item;
                    mGroundworkItem.BindTileItem(this);
                    break;
                case BuildCommandType.BuildWall:
                    WallItem = (WallItem)item;
                    WallItem.BindTileItem(this);
                    if (BelongRoom) //在已有房间内建墙，墙壁不属于房间的一部分
                    {
                        RoomItem.UnBindTileItem(this);
                        RoomItem = null;
                    }
                    break;
                case BuildCommandType.BuildDoor:
                    DoorItem = (DoorItem)item;
                    DoorItem.BindTileItem(this);
                    break;
                case BuildCommandType.BuildRoom:
                    RoomItem = (RoomItem) item;
                    RoomItem.BindTileItem(contents);
                    break;
                case BuildCommandType.BuildDecorate:
                    break;
            }
            
            mPreBuildingItem = item;
            mPreBuildingItem.PreBuild();
            
        }

        /// <summary>
        /// 工人到位后开始真正建造
        /// </summary>
        /// <param name="finish"></param>
        /// <returns></returns>
        public IEnumerator Build(Action finish)
        {
            if (mPreBuildingItem != null)
            {
                yield return StartCoroutine(mPreBuildingItem.Build());
                finish();
            }
        }

        /// <summary>
        /// 工人到位后开始拆除
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="finish"></param>
        /// <returns></returns>
        public IEnumerator Remove(BuildCommandType commandType, Action finish)
        {
            switch (commandType)
            {
                    case BuildCommandType.RemoveGroundwork:
                        if (HaveGroundwork)
                        {
                            yield return StartCoroutine(mGroundworkItem.Remove());
                            mGroundworkItem = null;
                        }
                    break;
                    case BuildCommandType.RemoveBuildingMaterials:
                        if (HaveWall)
                        {
                            yield return StartCoroutine(WallItem.Remove());
                            WallItem = null;
                            foreach (var item in TilesManager.GetArroundTiles(IndexX,IndexY))
                            {
                                if(item.HaveWall)
                                    item.WallItem.SetWallImage(false);
                            }
                        }else if (HaveDoor)
                        {
                            yield return StartCoroutine(DoorItem.Remove());
                            DoorItem = null;
                            foreach (var item in TilesManager.GetArroundTiles(IndexX, IndexY))
                            {
                                if (item.HaveWall)
                                    item.WallItem.SetWallImage(false);
                            }
                        }
                    break;
                    case BuildCommandType.RemoveRoom:
                    break;
                    case BuildCommandType.RemoveDecorate:
                    break;
            }
            finish();
        }
    }
}
