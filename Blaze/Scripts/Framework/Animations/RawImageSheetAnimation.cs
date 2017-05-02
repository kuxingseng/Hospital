namespace Blaze.Framework.Animations
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// 用于<see cref="RawImage"/>的逐帧动画。
    /// </summary>
    [RequireComponent(typeof (RawImage))]
    public class RawImageSheetAnimation : MonoBehaviour
    {
        public int ColumnCount;
        public int Fps = 30;
        public int FrameCount;
        public int RowCount;

        protected void Awake()
        {
            mImage = GetComponent<RawImage>();
        }

        protected void Update()
        {
            mTimer += Time.smoothDeltaTime;
            mIndex = (int)(Fps * mTimer) % FrameCount;
            showIndex(mIndex);
        }

        private void showIndex(int index)
        {
            if (ColumnCount == 0 || RowCount == 0)
                return;
            var cellWidth = 1.0f / ColumnCount;
            var cellHeight = 1.0f / RowCount;

            var column = index % ColumnCount;
            var row = RowCount-1-index / ColumnCount;   //从上到下 从左到右播放序列动画
            var x = column * cellWidth;
            var y = row * cellHeight;
            mImage.uvRect = new Rect(x, y, cellWidth, cellHeight);
        }

        private int mIndex;
        private float mTimer;
        private RawImage mImage;
    }
}