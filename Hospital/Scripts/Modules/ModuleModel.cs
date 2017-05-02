namespace Muhe.Mjhx.Modules.Models
{
    using Blaze.Framework.Mvc;
    using UnityEngine;

    /// <summary>
    /// 模块模型的抽象基类，提供模型的常用功能。
    /// </summary>
    public abstract class ModuleModel : IModel
    {
        /// <summary>
        /// 获取一个值，表示当前是否处于开发环境中。
        /// </summary>
        protected bool IsDevelopment
        {
            get { return Debug.isDebugBuild; }
        }
        
        /// <summary>
        /// 获取模块名称。
        /// </summary>
        /// <returns></returns>
        protected virtual string GetModuleName()
        {
            var moduleName = GetType().Name.Replace("Model", string.Empty);
            return moduleName;
        }

    }
}