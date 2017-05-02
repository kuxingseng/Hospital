namespace Blaze.Editor.Workflow.Localizations
{
    using System.Collections.Generic;

    /// <summary>
    /// 多语言配置模板。
    /// </summary>
    public class LocalizationEntry
    {
        /// <summary>
        /// 获取或设置多语言配置标识符。
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 获取指定语言的配置。
        /// </summary>
        /// <param name="language">语言标识符</param>
        /// <returns>配置</returns>
        public string this[string language]
        {
            get
            {
                string ret;
                if (mLanguages.TryGetValue(language, out ret))
                    return ret;
                return null;
            }
        }

        /// <summary>
        /// 获取或设置本条信息来源于哪个文件。
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 添加一项语言配置。
        /// </summary>
        /// <param name="language">语言标识符</param>
        /// <param name="content">本地化内容</param>
        public void Add(string language, string content)
        {
            if (mLanguages.ContainsKey(language))
                mLanguages[language] = content;
            else
                mLanguages.Add(language, content);
        }

        private readonly Dictionary<string, string> mLanguages = new Dictionary<string, string>();
    }
}