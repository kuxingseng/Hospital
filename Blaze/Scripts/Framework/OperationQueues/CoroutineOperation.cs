namespace Blaze.Framework.OperationQueues
{
    using System;
    using System.Collections;
    using UnityEngine;

    /// <summary>
    /// Эͬ���������
    /// </summary>
    public class CoroutineOperation : IOperation
    {
        #region IDisposable Members

        /// <summary>
        /// ִ�����ͷŻ����÷��й���Դ��ص�Ӧ�ó����������
        /// </summary>
        public void Dispose()
        {
            //TODO:Dispose
            mCoroutine = null;
            mGame = null;
        }

        #endregion

        #region IOperation Members

        /// <summary>
        /// ȡ��������
        /// </summary>
        public void Cancel()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// ����ִ�в�����
        /// </summary>
        public void Execute()
        {
            mGame.StartCoroutine(run());
        }

        /// <summary>
        /// ������ʧ��ʱ�������¼���
        /// </summary>
        public event EventHandler Failed;

        /// <summary>
        /// �������ɹ����ʱ�������¼���
        /// </summary>
        public event EventHandler Succeed;

        #endregion

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="game">��Ϸ����</param>
        protected CoroutineOperation(IGameEngine game)
        {
            if (game == null)
                throw new ArgumentNullException("game");
            mGame = game;
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="game">��Ϸ����</param>
        /// <param name="coroutine">Эͬ����</param>
        public CoroutineOperation(IGameEngine game, IEnumerator coroutine)
        {
            if (game == null)
                throw new ArgumentNullException("game");
            if (coroutine == null)
                throw new ArgumentNullException("coroutine");
            mGame = game;
            mCoroutine = coroutine;
        }

        /// <summary>
        /// ����Ҫִ�в���ʱ���ô˷���������һ��Эͬ����
        /// </summary>
        protected virtual IEnumerator OnExecute()
        {
            if (mCoroutine == null)
                throw new InvalidOperationException("mCoroutine should not be null.");
            return mCoroutine;
        }

        /// <summary>
        /// ����һ���µ�Эͬ����
        /// </summary>
        /// <param name="coroutine">Эͬ���������</param>
        /// <returns>Эͬ����</returns>
        protected Coroutine StartCoroutine(IEnumerator coroutine)
        {
            return mGame.StartCoroutine(coroutine);
        }

        private IEnumerator run()
        {
            yield return mGame.StartCoroutine(OnExecute());
            if (Succeed != null)
                Succeed(this, EventArgs.Empty);
        }

        private IEnumerator mCoroutine;
        private IGameEngine mGame;
    }
}