namespace Blaze.Framework.OperationQueues
{
    using System;
    using System.Collections;
    using UnityEngine;

    /// <summary>
    /// 为<see cref="IOperation"/>提供FluentAPI扩展。
    /// </summary>
    public static class OperationFluentExtension
    {
        /// <summary>
        /// 延迟N帧后执行操作。
        /// </summary>
        /// <param name="operation">操作</param>
        /// <param name="frameCount">帧数</param>
        /// <returns></returns>
        public static IOperation DelayExecute(this IOperation operation, int frameCount = 1)
        {
            BlazeEngine.Instance.StartCoroutine(delayExecute(operation, frameCount));
            return operation;
        }

        /// <summary>
        /// 执行并等待操作成功完成。
        /// <para>
        /// 用例：
        /// </para>
        /// <code>
        /// yield return myOperation.ExecuteAndWaitForSuccess();
        /// </code>
        /// </summary>
        public static YieldInstruction ExecuteAndWaitForSuccess(this IOperation operation)
        {
            return BlazeEngine.Instance.StartCoroutine(executeAndWaitForSuccess(operation));
        }

        /// <summary>
        /// 当操作执行失败后调用指定方法。
        /// </summary>
        /// <param name="operation">操作</param>
        /// <param name="callback">需要调用的方法</param>
        /// <returns>原操作</returns>
        public static IOperation OnFailed(this IOperation operation, Action callback)
        {
            EventHandler handler = null;
            handler = (s, e) =>
            {
                operation.Failed -= handler;
                if (callback != null)
                    callback();
            };
            operation.Failed += handler;
            return operation;
        }

        /// <summary>
        /// 当操作执行成功后调用指定方法。
        /// </summary>
        /// <param name="operation">操作</param>
        /// <param name="callback">需要调用的方法</param>
        /// <returns>原操作</returns>
        public static IOperation OnSucceed(this IOperation operation, Action callback)
        {
            EventHandler handler = null;
            handler = (s, e) =>
            {
                operation.Succeed -= handler;
                if (callback != null)
                    callback();
            };
            operation.Succeed += handler;
            return operation;
        }

        /// <summary>
        /// 等待操作成功完成（通常交由队列执行该操作）。
        /// <para>
        /// 用例：
        /// </para>
        /// <code>
        /// yield return myOperation.WaitForSuccess();
        /// </code>
        /// </summary>
        public static YieldInstruction WaitForSuccess(this IOperation operation)
        {
            return BlazeEngine.Instance.StartCoroutine(waitForSuccess(operation));
        }

        private static IEnumerator delayExecute(IOperation operation, int frameCount)
        {
            for (var i = 0; i < frameCount; i++)
                yield return null;
            operation.Execute();
        }

        private static IEnumerator executeAndWaitForSuccess(IOperation operation)
        {
            var isSucceed = false;
            operation.OnSucceed(() => { isSucceed = true; });
            operation.Execute();
            while (!isSucceed)
                yield return null;
        }

        private static IEnumerator waitForSuccess(IOperation operation)
        {
            var isSucceed = false;
            EventHandler handler = null;
            handler = (s, e) =>
            {
                isSucceed = true;
                operation.Succeed -= handler;
            };
            operation.Succeed += handler;
            while (!isSucceed)
                yield return null;
        }
    }
}