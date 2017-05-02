namespace Blaze.Framework.Serialization
{
    /// <summary>
    /// 序列化上下文。
    /// </summary>
    public class SerializationContext
    {
        /// <summary>
        /// 获取或设置资源解析器。
        /// </summary>
        public IAssetResolver Resolver { get; set; }
    }
}