namespace Blaze.UI.Emoticons
{
    using Framework;
    using UnityEngine;

    [Singleton(Hierarchy = "Blaze")]
    public class EmoticonDatabase : MonoBehaviour
    {
        public static readonly int DefaultEmoticonImageSize = 64;
        public static string PatternEmoticon = "\\{[A-Z]{1}\\d{1,3}\\}";
        public static string PatternHtml = "\\<.*?\\>";

        /// <summary>
        /// 获取或设置表情工厂。
        /// </summary>
        public IEmoticonFactory Factory
        {
            get
            {
                if (mFactory == null)
                    mFactory = new ResourceEmoticonFactory();
                return mFactory;
            }
            set { mFactory = value; }
        }

        /// <summary>
        /// 是否使用原生表情大小（令表情缩放到与字体相同）。
        /// </summary>
        public bool UseNativeSize { get; set; }

        /// <summary>
        /// 获取指定标识符的表情图片。
        /// </summary>
        /// <param name="symbol">表情标识符</param>
        /// <returns>表情图片</returns>
        public Emoticon Get(string symbol)
        {
            return Factory.Create(symbol);
        }

        /// <summary>
        /// 回收表情图片。
        /// </summary>
        /// <param name="image"></param>
        public void Put(Emoticon image)
        {
            Destroy(image.gameObject);
        }

        private IEmoticonFactory mFactory;
    }
}