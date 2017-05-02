namespace Assets.Hospital.Scripts.Modules.Controllers.Map.Commands
{
    using System.Collections.Generic;
    using Managers;
    using Models.Game;
    using Models.Map;
    using Muhe.Mjhx.Common;
    using Views.Map;
    using Views.Map.BuildingItems;

    /// <summary>
    /// author chenshuai
    /// $Id:$
    /// Create time:2017/4/20 15:27:54 
    /// Desc:
    /// </summary>
    ///
    public class BuildGroundworkCommand:BuildCommand
    {
        protected override bool OnPrepare()
        {
            //剔除不可铺设的格子
            for (var i = 0; i < SelectedItems.Count; i++)
            {
                if (CheckAvailable(SelectedItems[i]))
                    RealSelectTileItems.Add(SelectedItems[i]);
            }
            if (RealSelectTileItems.Count <= 0)
            {
                TipsManager.ShowTextTips("没有可用的地块");
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
                var buildItem = BuildingItemFactory.Create(BuildingsInfo, Map, tileItem.Position).AddComponent<GroundworkItem>();  //创建地基对象
                buildItem.BuildingsInfo = BuildingsInfo;
                tileItem.PreBuild(BuildCommandType.BuildGroundwork,buildItem);
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
            return item.HaveGroundwork==false;
        }
    }
}
