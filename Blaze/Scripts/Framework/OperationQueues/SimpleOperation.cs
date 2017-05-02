namespace Blaze.Framework.OperationQueues
{
    using System;

    /// <summary>
    /// �򵥵Ĳ�����װ��ͨ���̳�ʵ�ֶ�Ӧ�Ĳ�����Ϊ��
    /// </summary>
    public abstract class SimpleOperation : IOperation
    {
        #region IOperation Members

        /// <summary>
        /// ִ�����ͷŻ����÷��й���Դ��ص�Ӧ�ó����������
        /// </summary>
        public void Dispose()
        {
            OnDispose();
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
            OnCancel();
        }

        /// <summary>
        /// ����ִ�в�����
        /// </summary>
        public void Execute()
        {
            OnExecute();
        }

        #endregion

        /// <summary>
        /// ��������Ҫȡ��ʱ���ô˷�����
        /// </summary>
        protected virtual void OnCancel() {}

        /// <summary>
        /// ����Ҫ�ͷ������Դʱ���ô˷�����
        /// </summary>
        protected virtual void OnDispose() {}

        /// <summary>
        /// ����Ҫִ�в���ʱ���ô˷�����
        /// </summary>
        protected virtual void OnExecute()
        {
            if (Succeed != null)
                Succeed(this, EventArgs.Empty);
        }
    }
}