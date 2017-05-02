using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Hospital.Scripts.Modules.Views.Map
{
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// author chenshuai
    /// $Id:$
    /// Create time:2017/4/19 14:46:51 
    /// Desc:
    /// </summary>
    ///
    public class PreSelectTileItem:MonoBehaviour
    {
        private Image mImage;
        private static readonly Color mSelectedColor = new Color(180 / 255f, 66 / 255f, 83 / 255f, 146 / 255f);

        public int IndexX;
        public int IndexY;

        private void Awake()
        {
            mImage = transform.GetComponent<Image>();
            mImage.color = mSelectedColor;
            mImage.gameObject.SetActive(false);
        }

        /// <summary>
        /// 设置位置信息
        /// </summary>
        /// <param name="indexX"></param>
        /// <param name="indexY"></param>
        /// <returns></returns>
        public void SetData(int indexX, int indexY)
        {
            //todo:根据当前格子是否可进行选中的操作使用不同的颜色
            IndexX = indexX;
            IndexY = indexY;
        }

        /// <summary>
        /// 显示
        /// </summary>
        public void Show()
        {
            mImage.gameObject.SetActive(true);
        }

        /// <summary>
        /// 隐藏
        /// </summary>
        public void Hide()
        {
            mImage.gameObject.SetActive(false);
        }
    }
}
