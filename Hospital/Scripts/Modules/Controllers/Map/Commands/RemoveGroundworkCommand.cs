﻿namespace Assets.Hospital.Scripts.Modules.Controllers.Map.Commands
{
    using System.Collections.Generic;
    using Managers;
    using Models.Game;
    using Models.Map;
    using Muhe.Mjhx.Common;
    using Views.Map;

    /// <summary>
    /// author chenshuai
    /// $Id:$
    /// Create time:2017/4/20 15:29:23 
    /// Desc:
    /// </summary>
    ///
    public class RemoveGroundworkCommand:BuildCommand
    {
        protected override bool OnPrepare()
        {
            //选择可拆除的格子 todo:有附加建造物不可拆除
            for (var i = 0; i < SelectedItems.Count; i++)
            {
                if (CheckAvailable(SelectedItems[i]))
                    RealSelectTileItems.Add(SelectedItems[i]);
            }
            if (RealSelectTileItems.Count <= 0)
            {
                TipsManager.ShowTextTips("没有可供拆除的地基");
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
            return item.HaveGroundwork;
        }
    }
}
