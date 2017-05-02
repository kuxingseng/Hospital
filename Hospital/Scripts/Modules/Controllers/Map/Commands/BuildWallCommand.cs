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
    /// Create time:2017/4/24 16:38:55 
    /// Desc:
    /// </summary>
    ///
    public class BuildWallCommand:BuildCommand
    {
        protected override bool OnPrepare()
        {
            //墙体必须建在地基之上，且不能有其他建筑物
            for (var i = 0; i < SelectedItems.Count; i++)
            {
                if (CheckAvailable(SelectedItems[i]))
                    RealSelectTileItems.Add(SelectedItems[i]);
            }
            if (RealSelectTileItems.Count <= 0)
            {
                TipsManager.ShowTextTips("墙体必须建在地基之上，且不能有其他建筑物");
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
                var buildItem = BuildingItemFactory.Create(BuildingsInfo, Map, tileItem.Position).AddComponent<WallItem>();
                buildItem.BuildingsInfo = BuildingsInfo;
                tileItem.PreBuild(BuildCommandType.BuildWall, buildItem);
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
            return item.HaveGroundwork && !item.HaveBuildingMaterials;
        }
    }
}
