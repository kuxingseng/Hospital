namespace Assets.Hospital.Scripts.Configs.Data.Buildings
{
    using Scripts.Modules.Models.Game;
    using Scripts.Modules.Models.Map;

    /// <summary>
    /// author chenshuai
    /// $Id:$
    /// Create time:2017/4/17 15:20:02 
    /// Desc:
    /// </summary>
    ///
    public class BuildingsEntryInfo
    {
        /// <summary>
        /// 所属建筑类型-划分所属大类
        /// </summary>
        public MenuTypeEnum MenuType;
        
        /// <summary>
        /// 建造命令类型
        /// </summary>
        public BuildCommandType CommandType;

        /// <summary>
        /// 占据格子个数(水平方向)
        /// </summary>
        public int TakeOverTileNumH;

        /// <summary>
        /// 占据格子个数(垂直方向)
        /// </summary>
        public int TakeOverTileNumV;

        /// <summary>
        /// 对应图片资源路径
        /// </summary>
        public string ImagePath;
    }
}
