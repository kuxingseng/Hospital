namespace Blaze.Framework.Serialization
{
    using System.Linq;
    using UnityEngine;

    /// <summary>
    /// 常用数据类型的转换器。
    /// </summary>
    public static class DataConverter
    {
        /// <summary>
        /// 将文本转换为指定的<see cref="Color32"/>。
        /// </summary>
        /// <param name="text">文本</param>
        /// <returns>转换后的值</returns>
        public static Color32 GetColor32(string text)
        {
            if (string.IsNullOrEmpty(text))
                return new Color32();
            var array = text.Split(',').Select(c => byte.Parse(c)).ToArray();
            return new Color32(array[0], array[1], array[2], array[3]);
        }

        /// <summary>
        /// 将文本转换为指定的<see cref="Vector3"/>。
        /// </summary>
        /// <param name="text">文本</param>
        /// <returns>转换后的值</returns>
        public static Rect GetRect(string text)
        {
            if (string.IsNullOrEmpty(text))
                return new Rect();
            var array = text.Split(',').Select(c => float.Parse(c)).ToArray();
            return new Rect(array[0], array[1], array[2], array[3]);
        }

        /// <summary>
        /// 获取<see cref="Vector3"/>的文本表示。
        /// </summary>
        /// <param name="val">值</param>
        /// <returns>文本</returns>
        public static string GetString(Vector3 val)
        {
            return string.Format("{0},{1},{2}", val.x, val.y, val.z);
        }

        /// <summary>
        /// 获取<see cref="Vector2"/>的文本表示。
        /// </summary>
        /// <param name="val">值</param>
        /// <returns>文本</returns>
        public static string GetString(Vector2 val)
        {
            return string.Format("{0},{1}", val.x, val.y);
        }

        /// <summary>
        /// 获取<see cref="Color32"/>的文本表示。
        /// </summary>
        /// <param name="val">值</param>
        /// <returns>文本</returns>
        public static string GetString(Color32 val)
        {
            return string.Format("{0},{1},{2},{3}", val.r, val.g, val.b, val.a);
        }

        /// <summary>
        /// 获取<see cref="Rect"/>的文本表示。
        /// </summary>
        /// <param name="val">值</param>
        /// <returns>文本</returns>
        public static string GetString(Rect val)
        {
            return string.Format("{0},{1},{2},{3}", val.xMin, val.yMin, val.width, val.height);
        }

        /// <summary>
        /// 将文本转换为指定的<see cref="Vector2"/>。
        /// </summary>
        /// <param name="text">文本</param>
        /// <returns>转换后的值</returns>
        public static Vector2 GetVector2(string text)
        {
            if (string.IsNullOrEmpty(text))
                return Vector3.zero;
            var array = text.Split(',').Select(c => float.Parse(c)).ToArray();
            return new Vector2(array[0], array[1]);
        }

        /// <summary>
        /// 将文本转换为指定的<see cref="Vector3"/>。
        /// </summary>
        /// <param name="text">文本</param>
        /// <returns>转换后的值</returns>
        public static Vector3 GetVector3(string text)
        {
            if (string.IsNullOrEmpty(text))
                return Vector3.zero;
            var array = text.Split(',').Select(c => float.Parse(c)).ToArray();
            return new Vector3(array[0], array[1], array[2]);
        }
    }
}