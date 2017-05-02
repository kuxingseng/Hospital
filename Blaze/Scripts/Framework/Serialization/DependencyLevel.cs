namespace Blaze.Framework.Serialization
{
    /// <summary>
    /// 资源依赖等级。
    /// </summary>
    public enum DependencyLevel
    {
        /// <summary>
        /// 资源必须加载完后才算解析完毕。
        /// </summary>
        Required,

        /// <summary>
        /// 引用关系，必须等所有对象都加载完毕后才处理引用。
        /// </summary>
        Reference,

        /// <summary>
        /// 即使操作没有完成，也算解析完毕，当上下文被销毁后，所有的操作将被终止。
        /// </summary>
        Decorate,
    }
}