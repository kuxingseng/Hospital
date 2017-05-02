namespace Blaze.Editor.Diagnostics
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading;
    using Framework.Diagnostics;
    using Framework.Logging;
    using UnityEngine;

    /// <summary>
    /// 远程控制台。
    /// </summary>
    public class RemoteConsole
    {
        /// <summary>
        /// 获取一个值，表示控制台是否正在运行。
        /// </summary>
        public bool IsRunning
        {
            get { return mThread != null && mThread.IsAlive; }
        }

        /// <summary>
        /// 获取控制台所监听的端口。
        /// </summary>
        public int Port { get; private set; }

        /// <summary>
        /// 启动远程控制台。
        /// </summary>
        /// <param name="port">控制台监听的端口号</param>
        public void Start(int port)
        {
            if (IsRunning)
                return;
            Port = port;
            mThread = new Thread(run);
            mThread.Start();
        }

        /// <summary>
        /// 停止远程控制台。
        /// </summary>
        public void Stop()
        {
            if (mThread == null || mServer == null)
                return;
            mThread.Abort();
            mThread = null;
            mServer.Stop();
        }

        private void handleMsg(ConsoleMessage msg)
        {
            Debug.Log("[CMD]" + msg.Command + " " + msg.Content);
        }

        private void run()
        {
            try
            {
                mServer = new TcpListener(IPAddress.Any, Port);
                mServer.Start();
                while (true)
                {
                    var client = mServer.AcceptTcpClient();
                    mLogger.Info("Remote client connected.");

                    var stream = client.GetStream();
                    var reader = new BinaryReader(stream, Encoding.UTF8);
                    while (true)
                    {
                        if (stream.DataAvailable)
                        {
                            var msg = ConsoleMessage.Read(reader);
                            handleMsg(msg);
                        }
                        Thread.Sleep(1);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.ToString());
            }
        }

        private readonly Framework.Logging.ILogger mLogger = new UnityConsoleLogger();
        private TcpListener mServer;
        private Thread mThread;
    }
}