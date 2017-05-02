namespace Assets.Hospital.Scripts.Modules.Controllers.Map.Commands
{
    using System.Collections.Generic;
    using Managers;
    using Models.Game;
    using Models.Map;
    using Views.Map;
    using Views.Map.BuildingItems;

    /// <summary>
    /// author chenshuai
    /// $Id:$
    /// Create time:2017/4/28 10:50:01 
    /// Desc:
    /// </summary>
    ///
    public class BuildRoomCommand:BuildCommand
    {
        protected override bool OnPrepare()
        {
            //房间必须建在地基之上且不能和墙、门等重叠
            for (var i = 0; i < SelectedItems.Count; i++)
            {
                if (CheckAvailable(SelectedItems[i]))
                    RealSelectTileItems.Add(SelectedItems[i]);
            }
            if (RealSelectTileItems.Count <= 0)
            {
                TipsManager.ShowTextTips("房间必须建在地基之上且不能和墙、门等重叠");
                return false;
            }
            return true;
        }

        protected override void OnExecute()
        {
            var tasks = new List<Task>();
            TileItem tileItem = RealSelectTileItems[0];
            var buildItem = BuildingItemFactory.Create(BuildingsInfo, Map, tileItem.Position).AddComponent<RoomItem>();
            buildItem.BuildingsInfo = BuildingsInfo;
            tileItem.PreBuild(BuildCommandType.BuildRoom, buildItem,RealSelectTileItems);
            tasks.Add(new Task { Target = tileItem, Type = BuildingsInfo.CommandType });
            WorkerManager.AddTask(tasks);
        }

        /// <summary>
        /// 判断当前格子是否可用于建设命令
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected override bool CheckAvailable(TileItem item)
        {
            return item.HaveGroundwork && !item.HaveBuildingMaterials;
        }
    }
}
