namespace Blaze.Framework.Net
{
    using System;
    using System.Text;

    /// <summary>
    /// 封装一个Http请求。
    /// </summary>
    public class HttpRequest
    {
        /// <summary>
        /// 获取请求内容的字节数组。
        /// </summary>
        public byte[] Bytes { get; private set; }

        /// <summary>
        /// 获取请求标头集合。
        /// </summary>
        public HttpHeaderCollection Headers { get; private set; }

        /// <summary>
        /// 获取上次发生的错误。
        /// </summary>
        public string LastError { get; private set; }

        /// <summary>
        /// 获取请求的参数集合。
        /// </summary>
        public QueryStringCollection QueryString { get; private set; }

        /// <summary>
        /// 获取服务器回应。
        /// </summary>
        public string Response { get; private set; }

        /// <summary>
        /// 获取或设置请求超时的限制（单位：秒），小于等于0表示不会超时。
        /// </summary>
        public float Timeout { get; set; }

        /// <summary>
        /// 获取请求的目标地址。
        /// </summary>
        public Uri Uri { get; private set; }

        private HttpRequest()
        {
            Headers = new HttpHeaderCollection();
            QueryString = new QueryStringCollection();
        }

        /// <summary>
        /// 手动创建一个Http请求。
        /// </summary>
        /// <param name="endpoint">请求的地址</param>
        /// <param name="data">需要发送的数据</param>
        /// <returns>请求的封装</returns>
        public static HttpRequest Create(Uri endpoint, byte[] data)
        {
            if (endpoint == null)
                throw new ArgumentNullException("endpoint");
            var ret = new HttpRequest
            {
                Uri = endpoint,
                Bytes = data,
            };
            return ret;
        }

        /// <summary>
        /// 创建一个POST的Http请求。
        /// </summary>
        /// <param name="uriString">请求的地址</param>
        /// <param name="data">数据</param>
        /// <returns>请求的封装</returns>
        public static HttpRequest CreatePost(string uriString, string data)
        {
            var ret = new HttpRequest
            {
                Uri = new Uri(uriString),
                Bytes = Encoding.UTF8.GetBytes(data),
            };
            return ret;
        }

        /// <summary>
        /// 请求失败。
        /// </summary>
        /// <param name="error">请求错误描述</param>
        public void Fail(string error)
        {
            LastError = error;
            if (mFailHandler != null)
                mFailHandler(this);
        }

        /// <summary>
        /// 完成该请求。
        /// </summary>
        /// <param name="bytes">请求成功的字节数组</param>
        public void Finish(byte[] bytes)
        {
            Response = Encoding.UTF8.GetString(bytes,0,bytes.Length);
            if (mFinishHandler != null)
                mFinishHandler(this);
        }

        /// <summary>
        /// 设置当请求失败时执行的回调。
        /// </summary>
        /// <param name="handler">失败处理程序</param>
        public HttpRequest OnFail(Action<HttpRequest> handler)
        {
            mFailHandler = handler;
            return this;
        }

        /// <summary>
        /// 设置当请求成功时执行的回调。
        /// </summary>
        /// <param name="handler">成功处理程序</param>
        public HttpRequest OnFinish(Action<HttpRequest> handler)
        {
            if (mFinishHandler == null)
                mFinishHandler = handler;
            else
                mFinishHandler += handler;
            return this;
        }

        /// <summary>
        /// 发送请求。
        /// </summary>
        /// <param name="client">客户端</param>
        public HttpRequest Send(HttpClient client)
        {
            client.Request(this);
            return this;
        }

        private Action<HttpRequest> mFailHandler;
        private Action<HttpRequest> mFinishHandler;
    }
}