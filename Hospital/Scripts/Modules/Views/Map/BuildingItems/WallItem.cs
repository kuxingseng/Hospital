namespace Assets.Hospital.Scripts.Modules.Views.Map.BuildingItems
{
    using System.Collections;
    using Managers;
    using Muhe.Mjhx.Common;
    using UnityEngine;

    /// <summary>
    /// author chenshuai
    /// $Id:$
    /// Create time:2017/4/24 17:19:02 
    /// Desc:
    /// </summary>
    ///
    public class WallItem:BuildingItem
    {
        private TileItem mUpTile;
        private TileItem mDownTile;
        private TileItem mLeftTile;
        private TileItem mRightTile;
        private bool mTop = false;
        private bool mBottom = false;
        private bool mLeft = false;
        private bool mRight = false;

        /// <summary>
        /// 是否可建门（当前墙体不能为转角或连接处）
        /// </summary>
        public bool IsCanBuildDoor;

        /// <summary>
        /// 墙体旋转角度
        /// </summary>
        public int WallRotation;

        protected override IEnumerator OnBuild()
        {
            var randomTime = Random.Range(0.5f, 1.5f);
            yield return new WaitForSeconds(randomTime);

            setAroundWallIamge();
            SetWallImage();

            Image.color = NormalColor;
        }

        //设置周边墙体形状
        private void setAroundWallIamge()
        {
            mUpTile = TilesManager.GetTileItem(BindTileItems[0].IndexX, BindTileItems[0].IndexY - 1);
            mDownTile = TilesManager.GetTileItem(BindTileItems[0].IndexX, BindTileItems[0].IndexY + 1);
            mLeftTile = TilesManager.GetTileItem(BindTileItems[0].IndexX - 1, BindTileItems[0].IndexY);
            mRightTile = TilesManager.GetTileItem(BindTileItems[0].IndexX + 1, BindTileItems[0].IndexY);

            //重新设置周边墙体形状
            if (mUpTile != null && mUpTile.HaveWall)
            {
                mTop = true;
                mUpTile.WallItem.SetWallImage(false);
            }
            if (mDownTile != null && mDownTile.HaveWall)
            {
                mBottom = true;
                mDownTile.WallItem.SetWallImage(false);
            }
            if (mLeftTile != null && mLeftTile.HaveWall)
            {
                mLeft = true;
                mLeftTile.WallItem.SetWallImage(false);
            }
            if (mRightTile != null && mRightTile.HaveWall)
            {
                mRight = true;
                mRightTile.WallItem.SetWallImage(false);
            }   
        }

        /// <summary>
        /// 根据上下左右的墙体情况设置当前墙体的形状
        /// </summary>
        /// <param name="isBySelf"></param>
        public void SetWallImage(bool isBySelf=true)
        {
            //非自我建造调用且在与建造状态不执行
            if (isBySelf == false)
            {
                if (IsPreBuilding)
                    return;

                mTop = false;
                mBottom = false;
                mLeft = false;
                mRight = false;
                mUpTile = TilesManager.GetTileItem(BindTileItems[0].IndexX, BindTileItems[0].IndexY - 1);
                mDownTile = TilesManager.GetTileItem(BindTileItems[0].IndexX, BindTileItems[0].IndexY + 1);
                mLeftTile = TilesManager.GetTileItem(BindTileItems[0].IndexX - 1, BindTileItems[0].IndexY);
                mRightTile = TilesManager.GetTileItem(BindTileItems[0].IndexX + 1, BindTileItems[0].IndexY);

                if (mUpTile != null && mUpTile.HaveWall)
                    mTop = true;
                if (mDownTile != null && mDownTile.HaveWall)
                    mBottom = true;
                if (mLeftTile != null && mLeftTile.HaveWall)
                    mLeft = true;
                if (mRightTile != null && mRightTile.HaveWall)
                    mRight = true;
            }

            var imageName = "wall-one";
            WallRotation = 0;
            if (mTop && mBottom && mRight && mLeft)
            {
                //四面都有
                imageName = "wall-four";
            }
            else if (mTop && mBottom && mRight && !mLeft)
            {
                //上、下、右三面
                imageName = "wall-three";
            }
            else if (mTop && mBottom && !mRight && mLeft)
            {
                //上、下、左三面
                WallRotation = 180;
                imageName = "wall-three";
            }
            else if (mTop && !mBottom && mRight && mLeft)
            {
                //上、左、右三面
                WallRotation = 90;
                imageName = "wall-three";
            }
            else if (!mTop && mBottom && mRight && mLeft)
            {
                //下、左、右三面
                WallRotation = 270;
                imageName = "wall-three";
            }
            else if (!mTop && mBottom && mRight && !mLeft)
            {
                //下、右两面
                WallRotation = 270;
                imageName = "wall-two";
            }
            else if (!mTop && mBottom && !mRight && mLeft)
            {
                //下、左两面
                WallRotation = 180;
                imageName = "wall-two";
            }
            else if (mTop && !mBottom && !mRight && mLeft)
            {
                //上、左两面
                WallRotation = 90;
                imageName = "wall-two";
            }
            else if (mTop && !mBottom && mRight && !mLeft)
            {
                //上、右两面
                imageName = "wall-two";
            }
            else if (mTop && mBottom && !mRight && !mLeft)
            {
                //上、下两面
                imageName = "wall-one";
            }
            else if (mTop && !mBottom && !mRight && !mLeft)
            {
                //上一面
                imageName = "wall-one";
            }
            else if (!mTop && mBottom && !mRight && !mLeft)
            {
                //下一面
                imageName = "wall-one";
            }
            else if (!mTop && !mBottom && mRight && mLeft)
            {
                //左、右两面
                WallRotation = 90;
                imageName = "wall-one";
            }
            else if (!mTop && !mBottom && !mRight && mLeft)
            {
                //左一面
                WallRotation = 90;
                imageName = "wall-one";
            }
            else if (!mTop && !mBottom && mRight && !mLeft)
            {
                //右一面
                WallRotation = 90;
                imageName = "wall-one";
            }
            else if (!mTop && !mBottom && !mRight && !mLeft)
            {
                //上下左右都没有
                imageName = "wall-one";
            }

            IsCanBuildDoor = false;
            if (imageName == "wall-one")
                IsCanBuildDoor = true;

            Image.sprite = Resources.Load<SpriteRenderer>("Sprites/BuildingMaterials/" + imageName).sprite;
            Image.rectTransform.localEulerAngles = new Vector3(0, 0, WallRotation);
        }
    }
}
