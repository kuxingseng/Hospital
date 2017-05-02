namespace Blaze.Framework.Diagnostics.BuildInCommands
{
    /// <summary>
    /// ����Զ�̿���̨��
    /// </summary>
    public class Connect : DevConsoleCommand
    {
        /// <summary>
        /// ��ȡ��������ơ�
        /// </summary>
        public override string Name
        {
            get { return ".connect"; }
        }

        /// <summary>
        /// ��ִ������ʱ���ô˷�����
        /// </summary>
        /// <param name="options">�����������</param>
        /// <returns>�Ƿ�ִ�гɹ�</returns>
        protected override bool OnExecute(OptionSet options)
        {
            var hostArray = options[0].Split(':');
            if (hostArray.Length == 0)
            {
                DevConsole.WriteLine("Hostname required.");
                return false;
            }

            var port = 0;
            var hostname = hostArray[0];

            if (hostArray.Length > 1)
            {
                if (int.TryParse(hostArray[1], out port))
                {
                    DevConsole.WriteLine("Invalid port.");
                    return false;
                }
            }

            bool connected;
            if (port <= 0)
                connected = DevConsole.ConnectRemoteConsole(hostname);
            else
                connected = DevConsole.ConnectRemoteConsole(hostname, port);
            DevConsole.WriteLine("Connect {0} {1}.", options[0], connected ? "succeed" : "failed");
            return true;
        }
    }
}