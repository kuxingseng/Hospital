namespace Assets.Hospital.Scripts.Modules.Controllers.Map
{
    using System;
    using System.Collections.Generic;
    using Commands;
    using Configs.Data.Buildings;
    using Models.Map;
    using Muhe.Mjhx.Common;
    using Views.Map;

    /// <summary>
    /// author chenshuai
    /// $Id:$
    /// Create time:2017/4/20 15:01:57 
    /// Desc:创建建造命令的工厂类
    /// </summary>
    ///
    public static class BuildCommandFactory
    {
        private static readonly Dictionary<BuildCommandType, Type> mMapping = new Dictionary<BuildCommandType, Type>();

        /// <summary>
        /// 创建建造命令
        /// </summary>
        /// <param name="mapController"></param>
        /// <param name="selectedItems"></param>
        /// <param name="buildingsInfo"></param>
        /// <returns></returns>
        public static BuildCommand Create(MapController mapController, List<TileItem> selectedItems ,BuildingsEntryInfo buildingsInfo)
        {
            Type commandType;
            if (!mMapping.TryGetValue(buildingsInfo.CommandType, out commandType))
            {
                var name = "Assets.Hospital.Scripts.Modules.Controllers.Map.Commands." + buildingsInfo.CommandType.ToString()+"Command";
                commandType = Type.GetType(name);
                mMapping.Add(buildingsInfo.CommandType, commandType);
            }
            if (commandType == null)
                return null;
            var ret = (BuildCommand)Activator.CreateInstance(commandType);
            ret.BuildingsInfo = buildingsInfo;
            ret.Map = mapController;
            ret.SelectedItems = selectedItems;
            return ret;
        }
    }
}
