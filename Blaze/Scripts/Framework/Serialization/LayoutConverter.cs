namespace Blaze.Framework.Serialization
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Xml;
    using Loading;
    using UnityEngine;

    /// <summary>
    /// 提供<see cref="GameObject"/>的序列化与反序列化。
    /// </summary>
    public static class LayoutConverter
    {
        /// <summary>
        /// 获取或设置默认的资源解析器。
        /// </summary>
        public static IAssetResolver DefaultAssetResolver { get; set; }

        /// <summary>
        /// 将指定的XML配置反序列化为游戏对象。
        /// </summary>
        /// <param name="xml">XML</param>
        /// <returns>反序列化操作</returns>
        public static ILoadOperation Deserialize(string xml)
        {
            var context = new DeserializationContext {AssetResolver = DefaultAssetResolver};
            var operation = new DeserializeLayoutOperation(xml, context);
            return operation;
        }

        /// <summary>
        /// 从指定的Xml配置中读取并生成一个新的<see cref="GameObject"/>。
        /// </summary>
        /// <param name="xml">配置</param>
        /// <returns>生成的对象</returns>
        public static GameObject Load(string xml)
        {
            if (xml == null)
                return null;
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
            {
                var context = new DeserializationContext();
                var reader = XmlReader.Create(stream);

                //Declaration
                reader.Read();

                //GameObject
                reader.Read();
                var gameObject = reader.GetGameObject(context);
                return gameObject;
            }
        }

        /// <summary>
        /// 将整个程序集内所有的<see cref="ComponentResolver"/>注册到转换器。
        /// </summary>
        /// <param name="assembly">目标程序集</param>
        public static void RegisterResolver(Assembly assembly)
        {
            var resolverType = typeof (ComponentResolver);
            var query = from type in assembly.GetTypes()
                where !type.IsAbstract && resolverType.IsAssignableFrom(type)
                select (ComponentResolver) Activator.CreateInstance(type);

            foreach (var resolver in query)
                SerializationExtension.RegisterResolver(resolver);
        }

        /// <summary>
        /// 将指定的<see cref="GameObject"/>序列化为Xml文本配置。
        /// </summary>
        /// <param name="obj">需要序列化的<see cref="GameObject"/></param>
        /// <returns>生成的文本配置</returns>
        public static string Serialize(GameObject obj)
        {
            var context = new SerializationContext {Resolver = DefaultAssetResolver};
            using (var stream = new MemoryStream())
            {
                var writer = XmlWriter.Create(stream);
                writer.WriteStartDocument();
                writer.WriteGameObject(obj, context);
                writer.WriteEndDocument();
                writer.Flush();

                stream.Seek(0, SeekOrigin.Begin);

                var reader = new StreamReader(stream);
                return reader.ReadToEnd();
            }
        }
    }
}