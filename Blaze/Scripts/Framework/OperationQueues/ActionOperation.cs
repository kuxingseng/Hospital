namespace Blaze.Framework.OperationQueues
{
    using System;

    /// <summary>
    /// ִ��ĳ�������Ĳ�����
    /// </summary>
    public class ActionOperation : IOperation
    {
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
            mIsExecuting = true;
            if (mAction != null)
                mAction();
            mIsExecuting = false;

            if (Succeed != null)
                Succeed(this, EventArgs.Empty);
        }

        #endregion

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="action">��Ҫִ�еķ���</param>
        public ActionOperation(Action action)
        {
            mAction = action;
        }

        private Action mAction;

        private bool mIsExecuting;

        /// <summary>
        /// �ṩ<see cref="Action"/>��<see cref="ActionOperation"/>����ʽת����
        /// </summary>
        /// <param name="action">��Ҫ��װ�ķ���</param>
        /// <returns>ת����Ĳ���</returns>
        public static implicit operator ActionOperation(Action action)
        {
            return new ActionOperation(action);
        }
    }
}