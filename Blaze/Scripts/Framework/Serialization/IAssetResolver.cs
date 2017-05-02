namespace Blaze.Framework.Serialization
{
    using Loading;
    using UnityEngine;

    /// <summary>
    /// 提供资源引用关系的序列化与反序列化。
    /// </summary>
    public interface IAssetResolver
    {
        /// <summary>
        /// 解析引用标识符并获取资源。
        /// </summary>
        /// <typeparam name="T">资源类型</typeparam>
        /// <param name="id">资源标识符</param>
        /// <returns>资源加载操作</returns>
        ILoadOperation Deserialize<T>(string id);

        /// <summary>
        /// 将指定的资源序列化并创建引用的标识符。
        /// </summary>
        /// <param name="asset">资源</param>
        /// <returns>引用标识符</returns>
        string Serialize(Object asset);
    }
}