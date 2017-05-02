namespace Blaze.Framework.OperationQueues
{
    using System.Collections;
    using UnityEngine;

    /// <summary>
    /// 为<see cref="IOperationQueue"/>提供FluentAPI扩展。
    /// </summary>
    public static class OperationQueueFluentExtension
    {
        /// <summary>
        /// 等待队列里的所有操作结束。
        /// <para>
        /// 用例：
        /// </para>
        /// <code>
        /// yield return myQueue.WaitForCompletion();
        /// </code>
        /// </summary>
        public static YieldInstruction WaitForCompletion(this IOperationQueue queue)
        {
            return BlazeEngine.Instance.StartCoroutine(waitForCompletion(queue));
        }

        private static IEnumerator waitForCompletion(IOperationQueue queue)
        {
            while (queue.Count > 0)
                yield return null;
        }
    }
}