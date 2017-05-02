namespace Blaze.Editor
{
    using System;

    /// <summary>
    /// 开发面板标签页特性。
    /// </summary>
    public class DevTabPageAttribute : Attribute
    {
        /// <summary>
        /// 标签页显示顺序。
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 标签页名称。
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 构造一个<see cref="DevTabPageAttribute"/>。
        /// </summary>
        /// <param name="title"></param>
        public DevTabPageAttribute(string title)
        {
            Title = title;
        }
    }
}