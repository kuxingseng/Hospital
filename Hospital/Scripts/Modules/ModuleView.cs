namespace Muhe.Mjhx.Modules
{
    using Blaze.Framework.Mvc;

    /// <summary>
    /// 模块视图的抽象基类，提供视图的常用功能。
    /// </summary>
    public abstract class ModuleView : MonoView
    {
        /// <summary>
        /// 获取或设置控制器所对应的模块上下文。
        /// </summary>
        public ModuleContext Context { get; set; }

        /// <summary>
        /// 确保获取非空的字符串。
        /// </summary>
        /// <param name="text">字符串</param>
        /// <returns></returns>
        protected static string GetSafeString(string text)
        {
            if (text == null)
                return string.Empty;
            return text;
        }

        /// <summary>
        /// 获取模块所对应的控制器。
        /// </summary>
        /// <returns>控制器</returns>
        protected ModuleController GetController()
        {
            return GameObject.GetComponent<ModuleController>();
        }

        /// <summary>
        /// 获取模块名称。
        /// </summary>
        /// <returns></returns>
        protected virtual string GetModuleName()
        {
            var moduleName = GetType().Name.Replace("View", string.Empty);
            return moduleName;
        }

        protected virtual void OnDestroy()
        {
            UnloadContent();
        }
        

        /// <summary>
        /// 当视图被遮挡的状态变更时调用此方法。
        /// </summary>
        /// <param name="isOverlayed">是否被遮挡</param>
        internal virtual void OnOverlay(bool isOverlayed)
        {
        }
    }
}