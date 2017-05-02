namespace Blaze.Framework.OperationQueues
{
    using System;
    using System.Collections;
    using UnityEngine;

    /// <summary>
    /// 协同程序操作。
    /// </summary>
    public class CoroutineOperation : IOperation
    {
        #region IDisposable Members

        /// <summary>
        /// 执行与释放或重置非托管资源相关的应用程序定义的任务。
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
        /// 取消操作。
        /// </summary>
        public void Cancel()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 立即执行操作。
        /// </summary>
        public void Execute()
        {
            mGame.StartCoroutine(run());
        }

        /// <summary>
        /// 当操作失败时触发此事件。
        /// </summary>
        public event EventHandler Failed;

        /// <summary>
        /// 当操作成功完成时触发此事件。
        /// </summary>
        public event EventHandler Succeed;

        #endregion

        /// <summary>
        /// 构造器。
        /// </summary>
        /// <param name="game">游戏引擎</param>
        protected CoroutineOperation(IGameEngine game)
        {
            if (game == null)
                throw new ArgumentNullException("game");
            mGame = game;
        }

        /// <summary>
        /// 构造器。
        /// </summary>
        /// <param name="game">游戏引擎</param>
        /// <param name="coroutine">协同程序</param>
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
        /// 当需要执行操作时调用此方法，这是一个协同程序。
        /// </summary>
        protected virtual IEnumerator OnExecute()
        {
            if (mCoroutine == null)
                throw new InvalidOperationException("mCoroutine should not be null.");
            return mCoroutine;
        }

        /// <summary>
        /// 启动一个新的协同程序。
        /// </summary>
        /// <param name="coroutine">协同程序迭代器</param>
        /// <returns>协同程序</returns>
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