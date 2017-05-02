namespace Blaze.Framework
{
    using System.Collections;
    using UnityEngine;

    /// <summary>
    /// Ϊ<see cref="Animator"/>�ṩ��չ������
    /// </summary>
    public static class AnimatorExtension
    {
        /// <summary>
        /// �ȴ��������Ž�����
        /// </summary>
        /// <param name="animator">����������</param>
        /// <param name="layerIndex">�����㼶����</param>
        /// <param name="hash">�������ƹ�ϣֵ</param>
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