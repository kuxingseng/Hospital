namespace Assets.Hospital.Scripts.Modules.Views.Map
{
    using BuildingItems;
    using Configs.Data.Buildings;
    using Controllers.Map;
    using Models.Game;
    using UnityEngine;

    /// <summary>
    /// author chenshuai
    /// $Id:$
    /// Create time:2017/4/21 17:22:14 
    /// Desc:
    /// </summary>
    ///
    public static class BuildingItemFactory
    {
        private const string mBuildingItemPath = "Prefabs/UI/Game/BuildingItem";
        private static GameObject mBuildItemGameObject;

        public static GameObject Create(BuildingsEntryInfo buildingsEntryInfo,MapController controller, Vector3 position)
        {
            Transform parent = controller.View.GroundworkLayer;
            if (mBuildItemGameObject==null)
                mBuildItemGameObject = Resources.Load<GameObject>(mBuildingItemPath);
            var go = Object.Instantiate(mBuildItemGameObject);
            switch (buildingsEntryInfo.MenuType)
            {
                    case MenuTypeEnum.Groundwork:
                        parent = controller.View.GroundworkLayer;
                    break;
                    case MenuTypeEnum.BuildingMaterials:
                        parent = controller.View.BuildingMaterialsLayer;
                    break;
                    case MenuTypeEnum.Room:
                    parent = controller.View.RoomLayer;
                    break;
            }
            go.transform.SetParent(parent);
            go.transform.localScale = Vector3.one;
            go.transform.localPosition = position;
            
            return go;
        }
    }
}
