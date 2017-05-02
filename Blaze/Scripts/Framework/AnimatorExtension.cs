namespace Blaze.Framework
{
    using System.Collections;
    using UnityEngine;

    /// <summary>
    /// 为<see cref="Animator"/>提供扩展方法。
    /// </summary>
    public static class AnimatorExtension
    {
        /// <summary>
        /// 等待动画播放结束。
        /// </summary>
        /// <param name="animator">动画播放器</param>
        /// <param name="layerIndex">动画层级索引</param>
        /// <param name="hash">动画名称哈希值</param>
        public static YieldInstruction WaitForCompletion(this Animator animator, int layerIndex, int hash)
        {
            return BlazeEngine.Instance.StartCoroutine(waitForCompletion(animator, layerIndex, hash));
        }

        private static IEnumerator waitForCompletion(Animator animator, int layerIndex, int hash)
        {
            yield return null;
            while (true)
            {
                if (animator == null)
                    yield break;
                if (animator.IsInTransition(layerIndex))
                {
                    var currentInfo = animator.GetCurrentAnimatorStateInfo(layerIndex);
                    var nextInfo = animator.GetNextAnimatorStateInfo(layerIndex);
                    if (currentInfo.nameHash != hash && nextInfo.nameHash != hash)
                        yield break;
                }
                else
                {
                    var currentInfo = animator.GetCurrentAnimatorStateInfo(layerIndex);
                    if (currentInfo.nameHash != hash)
                        yield break;
                    if (currentInfo.normalizedTime > 1)
                        yield break;
                }
                yield return null;
            }
        }
    }
}