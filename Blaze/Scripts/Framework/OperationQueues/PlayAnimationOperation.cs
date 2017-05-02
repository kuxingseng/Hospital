namespace Blaze.Framework.OperationQueues
{
    using System;
    using System.Collections;
    using UnityEngine;

    /// <summary>
    /// 播放音乐操作。
    /// </summary>
    public class PlayAnimationOperation : IOperation
    {
        /// <summary>
        /// 获取或设置需要播放的动画名称。
        /// </summary>
        public string AnimationName { get; set; }

        /// <summary>
        /// 获取或设置动画的播放模式。
        /// </summary>
        public PlayMode PlayMode { get; set; }

        #region IOperation Members

        /// <summary>
        /// 执行与释放或重置非托管资源相关的应用程序定义的任务。
        /// </summary>
        public void Dispose()
        {
            mAnimation = null;
        }

        /// <summary>
        /// 当操作失败时触发此事件。
        /// </summary>
        public event EventHandler Failed;

        /// <summary>
        /// 当操作成功完成时触发此事件。
        /// </summary>
        public event EventHandler Succeed;

        /// <summary>
        /// 取消操作。
        /// </summary>
        public void Cancel()
        {
            mAnimation = null;
        }

        /// <summary>
        /// 立即执行操作。
        /// </summary>
        public void Execute()
        {
            if (string.IsNullOrEmpty(AnimationName))
                mAnimation.Play(PlayMode);
            else
                mAnimation.Play(AnimationName, PlayMode);
            mGame.StartCoroutine(run());
        }

        #endregion

        /// <summary>
        /// 播放动画操作。
        /// </summary>
        public PlayAnimationOperation(IGameEngine game, Animation animation)
        {
            if (game == null)
                throw new ArgumentNullException("game");
            if (animation == null)
                throw new ArgumentNullException("animation");
            mGame = game;
            mAnimation = animation;
        }

        private IEnumerator run()
        {
            while (true)
            {
                if (AnimationName == null && !mAnimation.isPlaying)
                    break;
                if (mAnimation.IsPlaying(AnimationName))
                    break;
                yield return null;
            }

            if (Succeed != null)
                Succeed(this, EventArgs.Empty);
        }

        private readonly IGameEngine mGame;
        private Animation mAnimation;
    }
}