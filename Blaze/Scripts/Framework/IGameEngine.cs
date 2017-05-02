namespace Blaze.Framework
{
    using System.Collections;
    using UnityEngine;

    /// <summary>
    /// ��Ϸ����ӿڡ�
    /// </summary>
    public interface IGameEngine
    {
        /// <summary>
        /// ע��ÿ֡���µĶ���
        /// </summary>
        /// <param name="updatable">���Ҹ��µĶ���</param>
        void RegisterUpdatable(IUpdatable updatable);

        /// <summary>
        /// ����һ��Эͬ����
        /// </summary>
        /// <param name="coroutine">Эͬ����</param>
        Coroutine StartCoroutine(IEnumerator coroutine);

        /// <summary>
        /// ȡ��ע��ÿ֡���µĶ���
        /// </summary>
        /// <param name="updatable">���Ҹ��µĶ���</param>
        void UnregisterUpdatable(IUpdatable updatable);
    }
}