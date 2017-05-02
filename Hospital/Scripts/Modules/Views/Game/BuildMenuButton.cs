namespace Assets.Hospital.Scripts.Modules.Views.Game
{
    using System.Collections.Generic;
    using Configs.Data.Buildings;
    using Models.Game;
    using Models.Map;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// author chenshuai
    /// $Id:$
    /// Create time:2017/4/12 18:22:44 
    /// Desc:
    /// </summary>
    ///
    public class BuildMenuButton:MonoBehaviour
    {
        public Button MenuButton;
        public MenuTypeEnum MenuType;
        public GameView GameView;

        private Text mMenuButtonText;

        private List<BuildingsEntryInfo> mBuildingsList;

        private void Start()
        {
            mMenuButtonText=MenuButton.GetComponentInChildren<Text>();

            var menuName = "地基";
            switch (MenuType)
            {
                case MenuTypeEnum.Groundwork:
                    menuName = "地基";
                    break;
                case MenuTypeEnum.BuildingMaterials:
                    menuName = "建材";
                    break;
                case MenuTypeEnum.Room:
                    menuName = "房间";
                    break;
                case MenuTypeEnum.Decorate:
                    menuName = "装饰";
                    break;
            }
            mMenuButtonText.text = menuName;

            MenuButton.onClick.AddListener(openMenu);
        }

        private void openMenu()
        {
            //todo:根据菜单类型获取不同菜单的配置表并初始化
            var imagePath1 = "Sprites/Groundwork/brick";
            var imagePath2 = "Sprites/Groundwork/concrete";
            var imagePath3 = "Sprites/Common/remove";
            if (MenuType == MenuTypeEnum.Groundwork)
            {
                if (mBuildingsList == null)
                {
                    mBuildingsList = new List<BuildingsEntryInfo>();
                    var item = new BuildingsEntryInfo
                    {
                        MenuType = MenuType,
                        CommandType = BuildCommandType.BuildGroundwork,
                        TakeOverTileNumH = 1,
                        TakeOverTileNumV = 1,
                        ImagePath = imagePath1
                    };
                    mBuildingsList.Add(item);
                    var item1 = new BuildingsEntryInfo
                    {
                        MenuType = MenuType,
                        CommandType = BuildCommandType.BuildGroundwork,
                        TakeOverTileNumH = 1,
                        TakeOverTileNumV = 1,
                        ImagePath = imagePath2
                    };
                    mBuildingsList.Add(item1);
                    var item2 = new BuildingsEntryInfo
                    {
                        MenuType = MenuType,
                        CommandType = BuildCommandType.RemoveGroundwork,
                        ImagePath = imagePath3
                    };
                    mBuildingsList.Add(item2);
                }
            }
            else if (MenuType == MenuTypeEnum.BuildingMaterials)
            {
                imagePath1 = "Sprites/BuildingMaterials/wall-one";
                imagePath2 = "Sprites/BuildingMaterials/door";
                if (mBuildingsList == null)
                {
                    mBuildingsList = new List<BuildingsEntryInfo>();
                    var item = new BuildingsEntryInfo
                    {
                        MenuType = MenuType,
                        CommandType = BuildCommandType.BuildWall,
                        TakeOverTileNumH = 1,
                        TakeOverTileNumV = 1,
                        ImagePath = imagePath1
                    };
                    mBuildingsList.Add(item);
                    var item1 = new BuildingsEntryInfo
                    {
                        MenuType = MenuType,
                        CommandType = BuildCommandType.BuildDoor,
                        TakeOverTileNumH = 1,
                        TakeOverTileNumV = 1,
                        ImagePath = imagePath2
                    };
                    mBuildingsList.Add(item1);
                    var item2 = new BuildingsEntryInfo
                    {
                        MenuType = MenuType,
                        CommandType = BuildCommandType.RemoveBuildingMaterials,
                        ImagePath = imagePath3
                    };
                    mBuildingsList.Add(item2);
                }
            }
            else if (MenuType == MenuTypeEnum.Room)
            {
                imagePath1 = "Sprites/Room/jizhenshi";
                imagePath2 = "Sprites/Room/menzhenshi";
                if (mBuildingsList == null)
                {
                    mBuildingsList = new List<BuildingsEntryInfo>();
                    var item = new BuildingsEntryInfo
                    {
                        MenuType = MenuType,
                        CommandType = BuildCommandType.BuildRoom,
                        TakeOverTileNumH = 2,
                        TakeOverTileNumV = 3,
                        ImagePath = imagePath1
                    };
                    mBuildingsList.Add(item);
                    var item1 = new BuildingsEntryInfo
                    {
                        MenuType = MenuType,
                        CommandType = BuildCommandType.BuildRoom,
                        TakeOverTileNumH = 3,
                        TakeOverTileNumV = 3,
                        ImagePath = imagePath2
                    };
                    mBuildingsList.Add(item1);
                }
            }

            GameView.OpenMenu(MenuType, mBuildingsList);    //关闭其他已打开的菜单
        }
    }
}
