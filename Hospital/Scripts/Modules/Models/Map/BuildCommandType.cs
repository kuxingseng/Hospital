namespace Assets.Hospital.Scripts.Modules.Models.Map
{
    /// <summary>
    /// author chenshuai
    /// $Id:$
    /// Create time:2017/4/20 15:05:43 
    /// Desc:建造命令类型
    /// </summary>
    ///
    public enum BuildCommandType
    {
        None,
        BuildGroundwork,
        RemoveGroundwork,
        BuildWall,
        BuildDoor,
        RemoveBuildingMaterials,
        BuildRoom,
        RemoveRoom,
        BuildDecorate,
        RemoveDecorate
    }
}
