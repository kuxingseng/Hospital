namespace Assets.Hospital.Scripts.Modules.Views.Map.BuildingItems
{
    using System.Collections.Generic;
    using Blaze.Framework;
    using Managers;
    using Muhe.Mjhx.Common;
    using UnityEngine;

    /// <summary>
    /// author chenshuai
    /// $Id:$
    /// Create time:2017/4/28 10:59:54 
    /// Desc:
    /// </summary>
    ///
    public class RoomItem:BuildingItem
    {
        protected override void OnBindTileItem()
        {
            //房间初始绑定格子，反向为格子绑定房间并设置房间大小及位置
            for (int i = 0; i < BindTileItems.Count; i++)
            {
                if (BindTileItems[i].BelongRoom == false)
                    BindTileItems[i].RoomItem = this;
            }
            setRoomPositionAndSize();
            checkCondition();
        }

        protected override void OnUnBindTileItem()
        {
            //房间内建墙等会使格子不再是房间的一部分，重新设置房间大小及位置
            //todo:如果被隔断自动裂变成新的同类型房间

            setRoomPositionAndSize();
            checkCondition();
        }

        //设置房间的位置及大小（以当前所有格子中左上角一个为准）
        private void setRoomPositionAndSize()
        {
            var miniX = int.MaxValue;
            var miniY = int.MaxValue;
            var maxX = -1;
            var maxY = -1;
            for (int i = 0; i < BindTileItems.Count; i++)
            {
                if (BindTileItems[i].IndexX < miniX)
                    miniX = BindTileItems[i].IndexX;
                if (BindTileItems[i].IndexY < miniY)
                    miniY = BindTileItems[i].IndexY;
                if (BindTileItems[i].IndexX > maxX)
                    maxX = BindTileItems[i].IndexX;
                if (BindTileItems[i].IndexY > maxY)
                    maxY = BindTileItems[i].IndexY;
            }
            MyLogger.Debug("miniX:" + miniX+" miniY:"+miniY);
            MyLogger.Debug("maxX:" + maxX + " maxY:" + maxY);

            transform.localPosition = TilesManager.GetTileItem(miniX, miniY).Position;
            Image.rectTransform.SetPivot(new Vector2(0,1));
            Image.rectTransform.sizeDelta = new Vector2(64 * (maxX - miniX + 1), 64 * (maxY - miniY + 1));

            //todo:根据房间大小及方向设置房间名称大小及方向
        }

        //todo:判断房间条件是否满足
        private void checkCondition()
        {
            //基础条件 四周是封闭的（墙+门）
            TileItem leftUpper;
            TileItem rightUpper;
            TileItem leftBottom;
            TileItem rightBottom;

            var upperItems = new List<TileItem>();
            var bottomItems = new List<TileItem>();
            var rightItems = new List<TileItem>();
            var leftItems = new List<TileItem>();
            for (int i = 0; i < BindTileItems.Count; i++)
            {
                //四周的格子
                var arroundItems = TilesManager.GetArroundTilesWithNull(BindTileItems[i].IndexX, BindTileItems[i].IndexY);
                //上
                if(arroundItems[0]==null)
                    upperItems.Add(arroundItems[0]);
                else if(arroundItems[0].BelongRoom==false)
                    upperItems.Add(arroundItems[0]);
                //下
                if (arroundItems[1] == null)
                    bottomItems.Add(arroundItems[1]);
                else if (arroundItems[1].BelongRoom == false)
                    bottomItems.Add(arroundItems[1]);
                //左
                if (arroundItems[2] == null)
                    leftItems.Add(arroundItems[2]);
                else if (arroundItems[2].BelongRoom == false)
                    leftItems.Add(arroundItems[2]);
                //右
                if (arroundItems[3] == null)
                    rightItems.Add(arroundItems[3]);
                else if (arroundItems[3].BelongRoom == false)
                    rightItems.Add(arroundItems[3]);
            }
            //去重，防止房间内有墙壁，获取上下左右四个角的格子

        }
    }
}
