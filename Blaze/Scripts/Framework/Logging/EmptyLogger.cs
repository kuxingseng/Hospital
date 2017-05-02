namespace Blaze.Framework.Logging
{
    /// <summary>
    /// �������־��
    /// </summary>
    public class EmptyLogger : ILogger
    {
        #region ILogger Members

        /// <summary>
        /// ��¼������Ϣ����־��
        /// </summary>
        /// <param name="content">��Ϣ����</param>
        public void Debug(object content)
        {
        }

        /// <summary>
        /// ��¼������Ϣ����־��
        /// </summary>
        /// <param name="format">��ʽ���ı�</param>
        /// <param name="args">��ʽ������</param>
        public void DebugFormat(string format, params object[] args)
        {
        }

        /// <summary>
        /// ��¼������Ϣ����־��
        /// </summary>
        /// <param name="content">��Ϣ����</param>
        public void Error(object content)
        {
        }

        /// <summary>
        /// ��¼������Ϣ����־��
        /// </summary>
        /// <param name="format">��ʽ���ı�</param>
        /// <param name="args">��ʽ������</param>
        public void ErrorFormat(string format, params object[] args)
        {
        }

        /// <summary>
        /// ��¼��Ϣ����־��
        /// </summary>
        /// <param name="content">��Ϣ����</param>
        public void Info(object content)
        {
        }

        /// <summary>
        /// ��¼��Ϣ����־��
        /// </summary>
        /// <param name="format">��ʽ���ı�</param>
        /// <param name="args">��ʽ������</param>
        public void InfoFormat(string format, params object[] args)
        {
        }

        /// <summary>
        /// ��¼������Ϣ����־��
        /// </summary>
        /// <param name="content">��Ϣ����</param>
        public void Warning(object content)
        {
        }

        /// <summary>
        /// ��¼������Ϣ����־��
        /// </summary>
        /// <param name="format">��ʽ���ı�</param>
        /// <param name="args">��ʽ������</param>
        public void WarningFormat(string format, params object[] args)
        {
        }

        #endregion
    }
}