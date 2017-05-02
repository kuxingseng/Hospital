namespace Muhe.Mjhx.Extensions
{
    using DG.Tweening;
    using UnityEngine;

    /// <summary>
    /// 为<see cref="DOTween"/>提供扩展。
    /// </summary>
    public static class DOTweenExtension
    {
        /// <summary>
        /// 使用nameId来提高DOFloat方法的性能。
        /// </summary>
        /// <param name="target">需要缓动的目标材质</param>
        /// <param name="endValue">缓动目标值</param>
        /// <param name="nameId">材质属性名称编号</param>
        /// <param name="duration">缓动时长</param>
        public static Tweener DOFloat(this Material target, float endValue, int nameId, float duration)
        {
            return DOTween.To(() => target.GetFloat(nameId), x => target.SetFloat(nameId, x), endValue, duration).SetTarget(target);
        }
    }
}