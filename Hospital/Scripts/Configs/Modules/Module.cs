namespace Muhe.Mjhx.Configs.Modules
{
    using System;

    /// <summary>
    /// 游戏模块配置。
    /// </summary>
    public abstract class Module
    {
        /// <summary>
        /// 获取控制器类型。
        /// </summary>
        public abstract Type ControllerType { get; }

        /// <summary>
        /// 获取模块入口预设路径。
        /// </summary>
        public abstract string EntryPrefab { get; }

        /// <summary>
        /// 获取游戏模块的唯一标识符。
        /// </summary>
        public virtual string Id
        {
            get
            {
                if (mId == null)
                    mId = GetType().Name.Replace("Module", string.Empty);
                return mId;
            }
        }

        /// <summary>
        /// 获取一个值，表示游戏模块是否为单例。
        /// </summary>
        public virtual bool IsSingleton
        {
            get { return false; }
        }

        /// <summary>
        /// 获取模块分层配置。
        /// </summary>
        public abstract ModuleLayer Layer { get; }

        private string mId;
    }
}