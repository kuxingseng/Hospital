namespace Blaze.Framework
{
    using UnityEngine;

    /// <summary>
    /// 为<see cref="RectTransform"/>提供常用的扩展功能。
    /// </summary>
    public static class RectTransformExtension
    {
        /// <summary>
        /// 设置图片的中心点，但不改变图片的坐标。
        /// </summary>
        public static void SetPivot(this RectTransform rectTransform, Vector2 pivot)
        {
            var delta = pivot - rectTransform.pivot;
            var deltaPosition = new Vector2(rectTransform.sizeDelta.x * delta.x, rectTransform.sizeDelta.y * delta.y);
            rectTransform.anchoredPosition += deltaPosition;
            rectTransform.pivot = pivot;
        }

        /// <summary>
        /// 设置图片的锚点为拉伸。
        /// </summary>
        public static void SetStretch(this RectTransform rectTransform)
        {
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            var parent = (RectTransform) rectTransform.parent;
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, parent.sizeDelta.x);
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, parent.sizeDelta.y);
        }
    }
}