namespace Blaze.Framework.OperationQueues
{
    using System;
    using UnityEngine;

    /// <summary>
    /// ����ָ�������Ĳ�����
    /// </summary>
    public class LoadLevelOperation : IOperation
    {
        #region IOperation Members

        /// <summary>
        /// ִ�����ͷŻ����÷��й���Դ��ص�Ӧ�ó����������
        /// </summary>
        public void Dispose()
        {
            mLevelName = null;
        }

        public event EventHandler Failed;
        public event EventHandler Succeed;

        /// <summary>
        /// ȡ��������
        /// </summary>
        public void Cancel()
        {
            if (mIsLoading)
                throw new InvalidOperationException("operation can not cancel when it is loading level.");
            mLevelName = null;
        }

        /// <summary>
        /// ����ִ�в�����
        /// </summary>
        public void Execute()
        {
            if (mLevelName == null)
                return;
            mIsLoading = true;
            Application.LoadLevel(mLevelName);
            mIsLoading = false;

            if (Succeed != null)
                Succeed(this, EventArgs.Empty);
        }

        #endregion

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="name">��������</param>
        public LoadLevelOperation(string name)
        {
            mLevelName = name;
        }

        private bool mIsLoading;
        private string mLevelName;
    }
}