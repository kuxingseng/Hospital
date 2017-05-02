namespace Blaze.Framework.OperationQueues
{
    using System;

    /// <summary>
    /// �첽�Ĳ�����
    /// </summary>
    public class AsyncActionOperation : IOperation
    {
        /// <summary>
        /// ����ί�У��������ɹ�����Ҫ�ص�<see cref="done"/>���׳������ɹ��¼���
        /// </summary>
        /// <param name="done">�����ɹ���Ļص�</param>
        public delegate void OperationDelegate(Action done);

        #region IOperation Members

        /// <summary>
        /// ִ�����ͷŻ����÷��й���Դ��ص�Ӧ�ó����������
        /// </summary>
        public void Dispose()
        {
            mAction = null;
        }

        /// <summary>
        /// ������ʧ��ʱ�������¼���
        /// </summary>
        public event EventHandler Failed;

        /// <summary>
        /// �������ɹ����ʱ�������¼���
        /// </summary>
        public event EventHandler Succeed;

        /// <summary>
        /// ȡ��������
        /// </summary>
        public void Cancel()
        {
            if (mIsExecuting)
                throw new NotSupportedException("The action can not be cancel when it is executing.");
            mAction = null;
        }

        /// <summary>
        /// ����ִ�в�����
        /// </summary>
        public void Execute()
        {
            if (mAction == null)
                return;
            mIsExecuting = true;
            mAction(done);
        }

        #endregion

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="action">��Ҫִ�еķ���</param>
        public AsyncActionOperation(OperationDelegate action)
        {
            mAction = action;
        }

        private void done()
        {
            mIsExecuting = false;
            if (Succeed != null)
                Succeed(this, EventArgs.Empty);
        }

        private OperationDelegate mAction;
        private bool mIsExecuting;
    }
}