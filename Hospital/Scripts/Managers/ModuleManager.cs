namespace Muhe.Mjhx.Managers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Blaze.Framework.Logging;
    using Configs.Modules;
    using Modules;

    /// <summary>
    /// 模块管理器。
    /// </summary>
    public static class ModuleManager
    {
        /// <summary>
        /// 获取指定模块所有的上下文。
        /// </summary>
        /// <typeparam name="T">模块类型</typeparam>
        /// <returns>模块列表</returns>
        public static IEnumerable<ModuleContext> GetContexts<T>() where T : Module, new()
        {
            var module = getConfig<T>();
            List<ModuleContext> list;
            if (mModuleContexts.TryGetValue(module, out list))
                return list;
            return new ModuleContext[0];
        }

        /// <summary>
        /// 获得所有tip层的模块
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ModuleContext> GetTipContexts() 
        {
            var list=new List<ModuleContext>();
            foreach (var contexts in mModuleContexts)
            {
                foreach (var context in contexts.Value)
                {
                    if (context.Config.Layer == ModuleLayer.Tip && context.Root.activeSelf)
                        list.Add(context);
                }
            }
            return list;
        }

        /// <summary>
        /// 加载指定类型的模块。
        /// </summary>
        /// <typeparam name="T">模块类型</typeparam>
        /// <returns>模块上下文</returns>
        public static ModuleContext Load<T>(Action<ModuleContext> callback = null) where T : Module, new()
        {
            var module = getConfig<T>();
            return load(module, callback);
        }

        /// <summary>
        /// 将指定类型的模块作为子模块加载。
        /// </summary>
        /// <typeparam name="T">模块类型</typeparam>
        /// <param name="callback">加载成功后的回调</param>
        /// <returns>模块上下文</returns>
        public static ModuleContext LoadSub<T>(Action<ModuleContext> callback = null) where T : Module, new()
        {
            var module = getConfig<T>();
            return loadSub(module, callback);
        }

        /// <summary>
        /// 卸载指定的模块，若模块的IsSingleton属性为真，则会被缓存。
        /// </summary>
        /// <param name="context">模块上下文</param>
        public static void Unload(ModuleContext context)
        {
            if (context == null)
                return;
            List<ModuleContext> list;
            if (mModuleContexts.TryGetValue(context.Config, out list))
                list.Remove(context);
            UIManager.Close(context.Root);
        }

        private static void cache(Module module, ModuleContext context)
        {
            List<ModuleContext> list;
            if (mModuleContexts.TryGetValue(module, out list))
                list.Add(context);
            else
                mModuleContexts.Add(module, new List<ModuleContext> {context});
        }

        /// <summary>
        /// 获取指定类型的模块配置。
        /// </summary>
        /// <typeparam name="T">模块类型</typeparam>
        /// <returns>模块</returns>
        private static T getConfig<T>() where T : Module, new()
        {
            return (T) getConfig(typeof (T));
        }

        /// <summary>
        /// 获取指定类型的模块。
        /// </summary>
        /// <param name="moduleType">模块类型</param>
        /// <returns>模块</returns>
        private static Module getConfig(Type moduleType)
        {
            Module module;
            if (!mModuleConfigs.TryGetValue(moduleType, out module))
            {
                module = (Module) Activator.CreateInstance(moduleType);
                mModuleConfigs.Add(moduleType, module);
            }
            return module;
        }

        /// <summary>
        /// 加载指定模块。
        /// </summary>
        /// <param name="module">模块</param>
        /// <param name="callback"></param>
        private static ModuleContext load(Module module, Action<ModuleContext> callback = null)
        {
            ModuleContext context;
            if (module.IsSingleton && tryLoadFromCache(module, out context))
            {
                //单例且缓存的模块
                if (callback != null)
                    callback(context);
                return context;
            }
            context = new ModuleContext {Config = module};
            UIManager.Popup(module.Layer, module.EntryPrefab, layer =>
            {
                var rootObj = layer.Root.gameObject;
                var controller = (ModuleController)rootObj.GetComponent(module.ControllerType);
                if(controller==null)
                    controller = (ModuleController) rootObj.AddComponent(module.ControllerType);
                context.Root = rootObj;
                controller.Context = context;

                var view = rootObj.GetComponent<ModuleView>();
                view.Context = context;

                if (callback != null)
                    callback(context);
            });

            cache(module, context);
            return context;
        }

        private static ModuleContext loadSub(Module module, Action<ModuleContext> callback = null)
        {
            ModuleContext context;
            if (module.IsSingleton && tryLoadFromCache(module, out context))
            {
                //单例且缓存的模块
                if (callback != null)
                    callback(context);
                return context;
            }

            context = new ModuleContext {Config = module};
            UIManager.Create(module.EntryPrefab, rootObj =>
            {
                var controller = (ModuleController)rootObj.GetComponent(module.ControllerType);
                if (controller == null)
                    controller = (ModuleController)rootObj.AddComponent(module.ControllerType);
                context.Root = rootObj;
                controller.Context = context;

                var view = rootObj.GetComponent<ModuleView>();
                view.Context = context;

                if (callback != null)
                    callback(context);
            });

            cache(module, context);
            return context;
        }

        private static bool tryLoadFromCache(Module module, out ModuleContext context)
        {
            context = null;
            List<ModuleContext> list;
            if (!mModuleContexts.TryGetValue(module, out list))
                return false;
            context = list.LastOrDefault();
            return context != null;
        }

        private static readonly Dictionary<Type, Module> mModuleConfigs = new Dictionary<Type, Module>();
        private static readonly Dictionary<Module, List<ModuleContext>> mModuleContexts = new Dictionary<Module, List<ModuleContext>>();
    }
}