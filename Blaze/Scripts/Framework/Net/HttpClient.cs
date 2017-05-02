namespace Blaze.Framework.Net
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Logging;
    using UnityEngine;

    /// <summary>
    /// Http交互客户端。
    /// </summary>
    public class HttpClient
    {
        private static HttpClient mInstance;

        public static HttpClient Instance(IGameEngine game)
        {
            if (mInstance == null)
            {
                mInstance=new HttpClient(game);
            }
            return mInstance;
        }

        /// <summary>
        /// 构造器。
        /// </summary>
        /// <param name="game">游戏引擎</param>
        public HttpClient(IGameEngine game)
        {
            if (game == null)
                throw new ArgumentNullException("game");
            mGame = game;
        }

        /// <summary>
        /// 发起指定的Http请求。
        /// </summary>
        /// <param name="request">请求</param>
        public void Request(HttpRequest request)
        {
            mGame.StartCoroutine(perform(request));
        }

        private IEnumerator perform(HttpRequest request)
        {
            WWW www;
            if (request.Bytes.Length == 0)
            {
                //GET
                var form = new WWWForm();
                foreach (var header in request.Headers)
                    form.headers.Add(header.Key, header.Value);
                if (mCookies != null)
                    form.headers.Add("Cookie", mCookies);
                foreach (var key in request.QueryString.Keys)
                    form.AddField(key, request.QueryString[key]);
                if (form.data.Length == 0)
                    www = new WWW(request.Uri.ToString());
                else
                    www = new WWW(request.Uri.ToString(), form);
            }
            else
            {
                //POST
                var headers = new Dictionary<string, string>(request.Headers);
                if (mCookies != null)
                    headers.Add("Cookie", mCookies);
                www = new WWW(request.Uri.ToString(), request.Bytes, headers);
            }
            using (www)
            {
                var beginTime = Time.time;
                while (!www.isDone)
                {
                    yield return null;
                    if (request.Timeout > 0 && Time.time - beginTime >= request.Timeout)
                    {
                        www.Dispose();
                        request.Fail("timeout");
                        yield break;
                    }
                }

                if (www.error == null)
                {
                    BlazeLog.InfoFormat("[HttpClient]text={0}", www.text);
                    request.Finish(www.bytes);
                    performCookies(www);
                }
                else
                {
                    BlazeLog.WarningFormat("[HttpClient]error={0}", www.error);
                    request.Fail(www.error);
                }
            }
        }

        private void performCookies(WWW www)
        {
            string cookiesText;
            if (!www.responseHeaders.TryGetValue("SET-COOKIE", out cookiesText))
                return;
            mCookies = cookiesText;
        }

        private readonly IGameEngine mGame;
        private string mCookies;
    }
}