namespace Blaze.Framework.Net.Sockets
{
    using System;
    using System.Net.Sockets;
    using System.Text;
    using Logging;
    using UnityEngine;

    /// <summary>
    /// 为 TCP 网络服务提供客户端连接。
    /// </summary>
    public class TcpClient : IDisposable
    {
        /// <summary>
        /// 获取已经从网络接收且可供读取的数据量。
        /// </summary>
        public int Available
        {
            get
            {
                if (mClientSocket != null)
                    return mClientSocket.Available;
                else
                    return 0;
            }
        }

        /// <summary>
        /// 获取基础 <see cref="T:System.Net.Sockets.Socket"/>。
        /// </summary>
        public Socket Client
        {
            get
            {
                if (mIsCleanedUp)
                    throw new ObjectDisposedException(GetType().FullName);
                return mClientSocket;
            }
        }

        /// <summary>
        /// 获取一个值，该值指示 <see cref="TcpClient"/> 的基础 <see cref="T:System.Net.Sockets.Socket"/> 是否已连接到远程主机。
        /// </summary>
        public bool Connected
        {
            get { return mClientSocket != null && mClientSocket.Connected; }
        }

        #region IDisposable Members

        /// <summary>
        /// 执行与释放或重置非托管资源相关的应用程序定义的任务。
        /// </summary>
        public void Dispose()
        {
            if (mClientSocket != null)
            {
                mClientSocket.Disconnect(false);
                mClientSocket = null;
            }
        }

        #endregion

        /// <summary>
        /// 初始化<see cref="TcpClient"/>的新实例。
        /// </summary>
        public TcpClient()
        {
            mClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        /// <summary>
        /// 析构方法。
        /// </summary>
        ~TcpClient()
        {
            Dispose(false);
        }

        /// <summary>
        /// 释放此<see cref="TcpClient"/>实例，并请求关闭基础 TCP 连接。
        /// </summary>
        public void Close()
        {
            mNetworkStream = null;
            if (mClientSocket == null)
                return;
            mClientSocket.Close();
            mClientSocket = null;
        }

        /// <summary>
        /// 连接到指定的服务器终结点。
        /// </summary>
        /// <param name="hostname">服务地址</param>
        /// <param name="port">服务端口</param>
        /// <returns>是否连接成功</returns>
        public bool Connect(string hostname, int port)
        {
            BlazeLog.InfoFormat("[TcpClient]Connecting to {0}:{1}", hostname, port);
            if (mIsCleanedUp)
                throw new ObjectDisposedException(GetType().FullName);
            if (hostname == null)
                throw new ArgumentNullException("hostname");
            if (!validateTcpPort(port))
                throw new ArgumentOutOfRangeException("port");

            try
            {
                if(mClientSocket==null)
                    mClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                mClientSocket.Connect(hostname, port);
                mNetworkStream = new NetworkStream(mClientSocket);
            }
            catch (Exception e)
            {
                BlazeLog.ErrorFormat("[TcpClient]Connection failed -> " + e);
                Debug.LogError("[TcpClient]Connection failed -> " + e);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 返回用于发送和接收数据的 <see cref="T:System.Net.Sockets.NetworkStream"/>。
        /// </summary>
        public NetworkStream GetStream()
        {
            if (mIsCleanedUp)
                throw new ObjectDisposedException(GetType().FullName);
            if (!mClientSocket.Connected)
                throw new InvalidOperationException("Socket not connected.");
            if (mNetworkStream == null)
                mNetworkStream = new NetworkStream(mClientSocket);
            return mNetworkStream;
        }

        /// <summary>
        /// 读取推送信息
        /// </summary>
        /// <returns></returns>
        public string Receive()
        {
            if (mClientSocket != null && mClientSocket.Connected)
            {
                var byteCount = mClientSocket.Receive(mReceiveBuff, 0, mClientSocket.Available, SocketFlags.None);
                //Debug.Log("receive byteCount:" + byteCount);
                if (byteCount > 0)
                    return Encoding.UTF8.GetString(mReceiveBuff, 0, byteCount);    //必须指定string的长度为实际获得的字节长度
            }
            return "";
        }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message"></param>
        public void Send(string message)
        {
            if (mClientSocket != null && mClientSocket.Connected)
            {
                try
                {
                    message = message.Trim();
                    byte[] msg = Encoding.UTF8.GetBytes(message);
                    int byteCount = mClientSocket.Send(msg, 0, msg.Length, SocketFlags.None);
                    //Debug.Log("send byteCount:" + byteCount);
                }
                catch (Exception e)
                {
                    Close();
                    Debug.LogError("Tcp server error:"+e);
                }
            }
        }

        /// <summary>
        /// 释放由 <see cref="TcpClient"/> 占用的非托管资源，还可以另外再释放托管资源。
        /// 
        /// </summary>
        /// <param name="disposing">设置为 true 可释放托管资源和非托管资源；设置为 false 只能释放托管资源。</param>
        protected virtual void Dispose(bool disposing)
        {
            if (mIsCleanedUp)
                return;
            if (disposing)
            {
                if (mNetworkStream != null)
                    mNetworkStream.Dispose();

                GC.SuppressFinalize(this);
                mIsCleanedUp = true;
            }
        }

        private static bool validateTcpPort(int port)
        {
            if (port < 0)
                return false;
            return port <= ushort.MaxValue;
        }

        private readonly byte[] mReceiveBuff = new byte[1024];
        private readonly byte[] mSendBuff = new byte[1024];
        private Socket mClientSocket;
        private bool mIsCleanedUp;
        private NetworkStream mNetworkStream;
    }
}