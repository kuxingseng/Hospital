namespace Blaze.Framework
{
    using System;

    /// <summary>
    ///     单例模式属性。
    /// </summary>
    public class SingletonAttribute : Attribute
    {
        /// <summary>
        ///     获取或设置一个值，表示当获取单例对象时，是否直接从游戏层次面板中获取。
        /// </summary>
        public bool CreateByManual { get; set; }

        /// <summary>
        ///     获取一个值，表示是否在切换场景时摧毁单例对象。
        /// </summary>
        public bool DontDestroyOnLoad
        {
            get { return !CreateByManual; }
        }

        /// <summary>
        ///     获取单例对象所使用的路径。
        /// </summary>
        public string Hierarchy { get; set; }

        /// <summary>
        ///     获取单例对象所使用的预设路径和名称。
        /// </summary>
        public string PrefabPath { get; set; }
    }
}