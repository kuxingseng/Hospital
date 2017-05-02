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
    /// Create time:2017/4/27 18:00:18 
    /// Desc:
    /// </summary>
    ///
    public class BuildDoorCommand:BuildCommand
    {
        protected override bool OnPrepare()
        {
            //门必须建在不带转角的墙壁上
            //todo:建门只能单选
            for (var i = 0; i < SelectedItems.Count; i++)
            {
                if (CheckAvailable(SelectedItems[i]))
                {
                    RealSelectTileItems.Add(SelectedItems[i]);
                    break;
                }   
            }
            if (RealSelectTileItems.Count <= 0)
            {
                TipsManager.ShowTextTips("门必须建在不带转角的墙壁上");
                return false;
            }
            return true;
        }

        protected override void OnExecute()
        {
            var tasks = new List<Task>();
            for (var i = 0; i < RealSelectTileItems.Count; i++)
            {
                TileItem tileItem = RealSelectTileItems[i];
                var buildItem = BuildingItemFactory.Create(BuildingsInfo, Map, tileItem.Position).AddComponent<DoorItem>();
                buildItem.BuildingsInfo = BuildingsInfo;
                tileItem.PreBuild(BuildCommandType.BuildDoor, buildItem);
                tasks.Add(new Task { Target = tileItem, Type = BuildingsInfo.CommandType });
            }
            WorkerManager.AddTask(tasks);
        }

        /// <summary>
        /// 判断当前格子是否可用于建设命令
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected override bool CheckAvailable(TileItem item)
        {
            return item.HaveWall && item.WallItem.IsCanBuildDoor;
        }
    }
}
