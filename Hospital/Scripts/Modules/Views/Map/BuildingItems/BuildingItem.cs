namespace Assets.Hospital.Scripts.Modules.Views.Map.BuildingItems
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Configs.Data.Buildings;
    using Muhe.Mjhx.Common;
    using UnityEngine;
    using UnityEngine.UI;
    using Random = UnityEngine.Random;

    /// <summary>
    /// author chenshuai
    /// $Id:$
    /// Create time:2017/4/21 15:28:25 
    /// Desc:所有建造物的基类
    /// </summary>
    ///
    public class BuildingItem:MonoBehaviour
    {
        /// <summary>
        /// 所绑定的格子
        /// </summary>
        protected List<TileItem> BindTileItems=new List<TileItem>();

        /// <summary>
        /// 建材详情
        /// </summary>
        public BuildingsEntryInfo BuildingsInfo { get; set; }

        /// <summary>
        /// 是否处在预建造状态
        /// </summary>
        public bool IsPreBuilding;  

        protected static readonly Color PreBuildColor = new Color(255f / 255f, 255f / 255f, 255f / 255f, 180 / 255f);
        protected static readonly Color NormalColor = new Color(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f);

        protected Image Image;

        protected void Awake()
        {
            Image = GetComponentInChildren<Image>();
            Image.gameObject.SetActive(false);
        }

        /// <summary>
        /// 绑定格子
        /// </summary>
        /// <param name="item"></param>
        public void BindTileItem(TileItem item)
        {
            BindTileItems.Add(item);
            OnBindTileItem();
        }

        /// <summary>
        /// 绑定格子
        /// </summary>
        /// <param name="items"></param>
        public void BindTileItem(List<TileItem> items)
        {
            BindTileItems = items;
            OnBindTileItem();
        }

        /// <summary>
        /// 解绑格子
        /// </summary>
        /// <param name="item"></param>
        public void UnBindTileItem(TileItem item)
        {
            if (BindTileItems.Contains(item))
                BindTileItems.Remove(item);
        }

        protected virtual void OnBindTileItem() {}

        protected virtual void OnUnBindTileItem() { }

        /// <summary>
        /// 预建造
        /// </summary>
        public void PreBuild()
        {
            Image.sprite = Resources.Load<SpriteRenderer>(BuildingsInfo.ImagePath).sprite;
            Image.color = PreBuildColor;
            Image.gameObject.SetActive(true);
            IsPreBuilding = true;

            OnPreBuild();
        }

        protected virtual void OnPreBuild(){}

        /// <summary>
        /// 建造
        /// </summary>
        /// <returns></returns>
        public IEnumerator Build()
        {
            yield return StartCoroutine(OnBuild());
            IsPreBuilding = false;
        }

        protected virtual IEnumerator OnBuild()
        {
            var randomTime = Random.Range(0.5f, 1.5f);
            yield return new WaitForSeconds(randomTime);
            Image.color = NormalColor;
        }

        /// <summary>
        /// 拆除
        /// </summary>
        /// <returns></returns>
        public IEnumerator Remove(bool immediately=false)
        {
            yield return StartCoroutine(OnRemove(immediately));
        }

        protected virtual IEnumerator OnRemove(bool immediately)
        {
            if (immediately == false)
            {
                var randomTime = Random.Range(0.5f, 1.5f);
                yield return new WaitForSeconds(randomTime);
            }
            DestroyObject(gameObject);
        }
    }
}
