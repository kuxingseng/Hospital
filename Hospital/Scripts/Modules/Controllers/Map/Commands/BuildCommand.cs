namespace Assets.Hospital.Scripts.Modules.Controllers.Map.Commands
{
    using System.Collections.Generic;
    using Configs.Data.Buildings;
    using Views.Map;

    /// <summary>
    /// author chenshuai
    /// $Id:$
    /// Create time:2017/4/20 14:55:43 
    /// Desc:建造命令
    /// </summary>
    ///
    public abstract class BuildCommand
    {
        /// <summary>
        /// 建材详情
        /// </summary>
        public BuildingsEntryInfo BuildingsInfo { get; set; }

        /// <summary>
        /// 地图控制器
        /// </summary>
        public MapController Map { get; set; }

        /// <summary>
        /// 所选中的格子
        /// </summary>
        public List<TileItem> SelectedItems { get; set; }

        /// <summary>
        /// 剔除不可用格子后真正可用于建造的格子
        /// </summary>
        protected List<TileItem> RealSelectTileItems=new List<TileItem>();

        /// <summary>
        /// 命令准备
        /// 完成建造条件判断等
        /// </summary>
        /// <returns></returns>
        public bool Prepare()
        {
            return OnPrepare();
        }

        /// <summary>
        /// 命令执行
        /// 执行建造命令
        /// </summary>
        public void Execute()
        {
            OnExecute();
        }

        protected virtual bool OnPrepare()
        {
            return true;
        }

        protected virtual void OnExecute()
        {
            
        }

        /// <summary>
        /// 判断当前格子是否可用于建设命令
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected virtual bool CheckAvailable(TileItem item)
        {
            return true;
        }
    }
}
