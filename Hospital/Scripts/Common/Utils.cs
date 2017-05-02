namespace Muhe.Mjhx.Common
{
    using System;
    using System.Collections;
    using Blaze.Framework;
    using Effects;
    using UnityEngine;
    using UnityEngine.UI;
    using Object = UnityEngine.Object;

    /// <summary>
    /// author chenshuai
    /// $Id$
    /// Create time:2015/12/17 12:10:55 
    /// </summary>
    ///
    public static class Utils
    {
        /// <summary>
        /// 获取当前时间戳
        /// </summary>
        /// <returns></returns>
        public static int DateTimeToUnixTimestamp()
        {
            DateTime now = DateTime.Now;
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return Convert.ToInt32((now - startTime).TotalSeconds);
        }

        /// <summary>
        /// 通过时间戳获取当前时间
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static DateTime UnixTimestampToDateTime(double timestamp)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return startTime.AddSeconds(timestamp);
        }

        /// <summary>
        /// 获取时分秒格式的倒计时
        /// </summary>
        /// <param name="second"></param>
        /// <returns></returns>
        public static string GetFomatTimeHms(float second)
        {
            var h = (int)Math.Floor(second / 3600);
            var m = (int)Math.Floor((second - h * 3600) / 60);
            var s = (int)second % 60;
            return string.Format("{0:d2}", h) + ":" + string.Format("{0:d2}", m) + ":" + string.Format("{0:d2}", s);
        }

        /// <summary>
        /// 获取时分格式的倒计时
        /// </summary>
        /// <param name="second"></param>
        /// <returns></returns>
        public static string GetFomatTimeHm(float second)
        {
            var h = (int)Math.Floor(second / 3600);
            var m = (int)Math.Floor((second - h * 3600) / 60);
            return string.Format("{0:d2}", h) + ":" + string.Format("{0:d2}", m);
        }
        /// <summary>
        /// 获取天时分的倒计时
        /// </summary>
        /// <param name="second"></param>
        /// <returns></returns>
        public static string GetGetFomatTimeDhms(float second)
        {
            var d = (int)Math.Floor(second / 86400);
            var h = (int)Math.Floor((second - d * 86400) / 3600);
            var m = (int)Math.Floor((second - d * 86400 - h * 3600) / 60);
            return d + "天" + h + "小时" + m + "分";
        }

        /// <summary>
        /// 转换金币显示单位（超过百万以万为单位，超过亿以亿为单位，零头以万为单位）
        /// </summary>
        /// <param name="coin"></param>
        /// <returns></returns>
        public static string ConvertCoinUnit(int coin)
        {
            string final;
            if (coin < 1000000)
                final = coin.ToString();
            else if (coin >= 1000000 && coin < 100000000)
            {
                coin = (int)(coin / 10000);
                final = coin + "万";
            }
            else
            {
                var first = (int)(coin / 100000000);
                var remainCoin = coin - first * 100000000;
                var second = (int)(remainCoin / 10000);
                if (second > 0)
                    final = first + "亿" + second + "万";
                else
                    final = first + "亿";
            }
            return final;
        }

        /// <summary>
        /// 创建特效
        /// </summary>
        /// <param name="effectId"></param>
        /// <param name="parent"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public static GameObject CreateEffect(string effectId, Transform parent, Vector3 position, bool isDestoryOnComplete)
        {
            var path = string.Format("{0}", effectId);
            var effectPrefab = Resources.Load<GameObject>(path);
            if (effectPrefab == null)
            {
                return new GameObject(effectId);
            }
            var effectObj = (GameObject)Object.Instantiate(effectPrefab);
            effectObj.SetLayer(parent.gameObject.layer, true);
            effectObj.transform.SetParent(parent.transform);
            effectObj.transform.localPosition = position;
            effectObj.transform.localScale = Vector3.one;
            if (isDestoryOnComplete)
                effectObj.AddComponent<DestoryOnComplete>();
            return effectObj;
        }

        /// <summary>
        /// 创建预设
        /// </summary>
        /// <param name="prefabPath"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static GameObject CreateGameObject(string prefabPath, Transform parent)
        {
            var obj = Resources.Load(prefabPath);
            if (obj == null)
            {
                return new GameObject("new gameobject");
            }
            var go = (GameObject)Object.Instantiate(obj);
            go.transform.SetParent(parent);
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = Vector3.one;
            return go;
        }

        /// <summary>
        /// 创建预设
        /// </summary>
        /// <param name="child"></param>
        /// <param name="parent"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public static GameObject CreateGameObject(GameObject child, Transform parent, Vector3 position = default(Vector3))
        {
            var go = (GameObject)Object.Instantiate(child);
            go.transform.SetParent(parent);
            if (position == default(Vector3))
                go.transform.localPosition = Vector3.zero;
            else
                go.transform.localPosition = position;
            go.transform.localScale = Vector3.one;
            return go;
        }

        /// <summary>
        /// 显示滚动文本效果
        /// </summary>
        /// <param name="lab"></param>
        /// <param name="durationTime"></param>
        /// <param name="basic"></param>
        /// <param name="target"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        public static IEnumerator ShowRollingNum(Text lab, float durationTime, float basic, float target, string desc = "")
        {
            float beginTime = Time.time;
            float totalTime = 0.0f;

            while (totalTime < durationTime)
            {
                float intervalTime = Time.time - beginTime;
                int intervalNum = (int)Mathf.Lerp(basic, target, intervalTime / durationTime);
                lab.text = intervalNum + desc;
                yield return null;
                totalTime = Time.time - beginTime;
            }
            lab.text = target + desc;
        }

    }
}
