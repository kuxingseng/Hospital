namespace Muhe.Mjhx.Managers
{
    using System.Collections;
    using Blaze.Framework;
    using UnityEngine;

    /// <summary>
    /// Эͬ�����������
    /// </summary>
    public static class CoroutineManager
    {
        /// <summary>
        /// ����һ���µ�Эͬ���򣬲���<see cref="GameObject"/>�����Ƿ�ΪactiveӰ�졣
        /// </summary>
        /// <param name="routine">Эͬ���������</param>
        /// <returns>Эͬ����</returns>
        public static Coroutine Start(IEnumerator routine)
        {
            return BlazeEngine.Instance.StartCoroutine(routine);
        }

        /// <summary>
        /// ����һ���µ�Эͬ���򣬲���<see cref="GameObject"/>�����Ƿ�ΪactiveӰ�졣
        /// </summary>
        /// <param name="routineName">Эͬ���������</param>
        /// <returns>Эͬ����</returns>
        public static Coroutine Start(string routineName)
        {
            return BlazeEngine.Instance.StartCoroutine(routineName);
        }

        /// <summary>
        /// ֹͣһ��Э��
        /// </summary>
        /// <param name="routine"></param>
        public static void Stop(IEnumerator routine)
        {
            BlazeEngine.Instance.StopCoroutine(routine);
        }

        /// <summary>
        /// ֹͣһ��Э��
        /// </summary>
        /// <param name="routineName"></param>
        public static void Stop(string routineName)
        {
            BlazeEngine.Instance.StopCoroutine(routineName);
        }
    }
}