namespace Assets.Hospital.Scripts.Modules.Views.Game
{
    using System.Collections;
    using System.Collections.Generic;
    using Blaze.Framework;
    using Configs.Data.Buildings;
    using DG.Tweening;
    using Models.Game;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// author chenshuai
    /// $Id:$
    /// Create time:2017/4/17 17:15:30 
    /// Desc:
    /// </summary>
    ///
    public class BuildMenuContent:MonoBehaviour
    {
        public ScrollRect MenuScrollRect;
        public Transform MenuContent;
        public GameObject Item;
        public GameView GameView;
        public Button CloseButton;

        private RectTransform mContent;
        private bool mIsMenuOpened;
        private BuildMenuItem mPreSelectMenuItem;

        private void Start()
        {
            mContent = MenuScrollRect.GetComponent<RectTransform>();

            mIsMenuOpened = true;
            StartCoroutine(closeMenu());
            CloseButton.onClick.AddListener(() =>
            {
                GameView.CloseMenu();
                StartCoroutine(closeMenu());
                //关闭时如果已经在编辑状态，取消状态
                if(mPreSelectMenuItem!=null)
                    GameView.SetEditor(false);
            });
        }
        
        /// <summary>
        /// 设置菜单内容
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public IEnumerator SetMenu(List<BuildingsEntryInfo> items)
        {
            yield return StartCoroutine(closeMenu());
            StartCoroutine(openMenu());

            //切换时取消编辑状态
            mPreSelectMenuItem = null;
            GameView.SetEditor(false);

            MenuContent.DestroyChildren();
            if(items==null)
                yield break;
            for (int i = 0; i < items.Count; i++)
            {
                var go = Instantiate(Item);
                go.transform.SetParent(MenuContent);
                go.transform.localScale = Vector3.one;
                var buildingItem = go.GetComponent<BuildMenuItem>();
                buildingItem.Init(this, items[i]);
                yield return new WaitForEndOfFrame();
            }
        }

        /// <summary>
        /// 选中建筑元素
        /// </summary>
        /// <param name="menuItem"></param>
        public void SelectBuildItem(BuildMenuItem menuItem)
        {
            if (menuItem == null)
            {
                //取消编辑状态
                GameView.SetEditor(false);
                mPreSelectMenuItem = null;
                return;
            }
            if (mPreSelectMenuItem != null)
            {
                if (mPreSelectMenuItem != menuItem)
                    mPreSelectMenuItem.SetStatus();
            }
            mPreSelectMenuItem = menuItem;
            //进入编辑状态
            GameView.SetEditor(true, menuItem.BuildingItemInfo);
        }

        private IEnumerator openMenu()
        {
            if (mIsMenuOpened)
                yield break;
            mIsMenuOpened = true;
            yield return mContent.DOAnchorPos(Vector2.zero, 0.3f).WaitForCompletion();
            CloseButton.gameObject.SetActive(true);
        }

        private IEnumerator closeMenu()
        {
            if (mIsMenuOpened == false)
                yield break;
            mIsMenuOpened = false;
            CloseButton.gameObject.SetActive(false);
            yield return mContent.DOAnchorPos(new Vector2(0, -mContent.sizeDelta.y), 0.2f).WaitForCompletion();
        }
    }
}
