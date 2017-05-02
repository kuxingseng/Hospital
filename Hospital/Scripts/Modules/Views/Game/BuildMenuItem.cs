namespace Assets.Hospital.Scripts.Modules.Views.Game
{
    using Configs.Data.Buildings;
    using Muhe.Mjhx.UI.Common;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// author chenshuai
    /// $Id:$
    /// Create time:2017/4/12 16:06:36 
    /// Desc:
    /// </summary>
    ///
    public class BuildMenuItem:MonoBehaviour
    {
        public Image ItemImage;
        public Image SelectImage;

        /// <summary>
        /// 建造单位信息
        /// </summary>
        public BuildingsEntryInfo BuildingItemInfo;

        private BuildMenuContent mContent;
        private bool mIsSelected;

        private void Start()
        {
            mIsSelected = false;
            SelectImage.gameObject.SetActive(false);
            UGUIButtonEventTrigger.Get(gameObject).OnClick = go =>
            {
                SetStatus(true);
            };
        }

        /// <summary>
        /// 设置当前元素选中状态
        /// </summary>
        /// <param name="bySelf"></param>
        public void SetStatus(bool bySelf=false)
        {
            mIsSelected = !mIsSelected;
            SelectImage.gameObject.SetActive(mIsSelected);

            if (mIsSelected)
                mContent.SelectBuildItem(this);
            else
            {
                if(bySelf)  //如果是点击自身取消选中则当前无选中元素，取消编辑状态
                    mContent.SelectBuildItem(null);
            }
        }

        /// <summary>
        /// 初始数据
        /// </summary>
        /// <param name="content"></param>
        /// <param name="buildingsInfo"></param>
        public void Init(BuildMenuContent content, BuildingsEntryInfo buildingsInfo)
        {
            mContent = content;
            BuildingItemInfo = buildingsInfo;

            ItemImage.sprite=Resources.Load<SpriteRenderer>(buildingsInfo.ImagePath).sprite;
        }
    }
}
