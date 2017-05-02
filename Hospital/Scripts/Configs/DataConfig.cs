namespace Muhe.Mjhx.Configs
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// 静态数据配置抽象基类。
    /// </summary>
    public abstract class DataConfig
    {
        /// <summary>
        /// 获取配置项标识符。
        /// </summary>
        [JsonIgnore]
        public abstract string Key { get; }

        /// <summary>
        /// 获取或设置当前配置的版本号，若版本号为空，则表示是本地配置。
        /// </summary>
        [JsonIgnore]
        public string Version { get; set; }

        /// <summary>
        /// 初始化配置，默认加载JSON文本并填充当前对象。
        /// </summary>
        /// <param name="data">配置内容</param>
        /// <returns>是否加载成功</returns>
        public virtual bool Initialize(JContainer data)
        {
            using (var reader = data.CreateReader())
            {
                var serializer = new JsonSerializer();
                serializer.Populate(reader, this);
                return true;
            }
        }
    }
}