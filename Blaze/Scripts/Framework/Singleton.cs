namespace Blaze.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    /// <summary>
    /// 单例管理器。
    /// </summary>
    public class Singleton : MonoBehaviour
    {
        /// <summary>
        /// 获取单例对象管理器的根对象。
        /// </summary>
        protected static GameObject Root
        {
            get
            {
                if (mRoot == null)
                {
                    mRoot = new GameObject(mRootName) {name = mRootName};
                    mRoot.AddComponent<Singleton>();
                    DontDestroyOnLoad(mRoot);
                }
                return mRoot;
            }
        }

        /// <summary>
        /// 获取游戏管理器的组件。
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        /// <returns>组件</returns>
        public static T GetInstance<T>() where T : Component
        {
            var componentType = typeof (T);
            Component component;
            if (mComponentCache.TryGetValue(componentType, out component))
            {
                //Maybe the component was destroyed by Unity.
                if (component == null)
                    mComponentCache.Remove(componentType);
                else
                    return (T) component;
            }

            var attribute = getSingletonAttribute(componentType);
            string error = null;
            if (attribute == null)
                throw new ArgumentException(string.Format("Can't find SingletonAttribute in {0}.", componentType));

            if (attribute.PrefabPath != null)
            {
                //create from prefab
                var obj = (GameObject) Resources.Load(attribute.PrefabPath);
                if (obj == null)
                {
                    error = string.Format("Can't find prefab in '{0}'", attribute.PrefabPath);
                }
                else
                {
                    obj = (GameObject) Instantiate(obj);
                    obj.name = componentType.Name;
                    component = obj.GetComponent<T>();
                    if (component == null)
                        error = string.Format("Can't find {0} in prefab '{1}'", componentType, attribute.PrefabPath);
                }
            }
            else if (attribute.CreateByManual)
            {
                //Find from scene
                component = FindObjectOfType<T>();
                error = string.Format("Can't find {0} in gameobject hierarchy.", componentType);
            }
            else
            {
                //create by default
                var obj = new GameObject(componentType.Name);
                component = obj.AddComponent<T>();
            }

            if (component == null)
                throw new InvalidOperationException(error);

            if (attribute.DontDestroyOnLoad)
                component.transform.parent = getTransformByPath(Root.transform, attribute.Hierarchy);

            mComponentCache.Add(componentType, component);
            return (T) component;
        }

        /// <summary>
        /// 安全使用指定组件的引用，防止在程序关闭的时候仍访问组件。
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        /// <param name="callback">功能回调</param>
        public static void SafeUsing<T>(Action<T> callback) where T : Component
        {
            if (mIsShuttingDown)
                return;
            var component = GetInstance<T>();
            callback(component);
        }

        protected void OnApplicationQuit()
        {
            mIsShuttingDown = true;
        }

        private static SingletonAttribute getSingletonAttribute(Type type)
        {
            var ret = type.GetCustomAttributes(typeof (SingletonAttribute), false).SingleOrDefault() as SingletonAttribute;
            return ret;
        }

        private const string mRootName = "_Singleton";
        private static readonly Dictionary<Type, Component> mComponentCache = new Dictionary<Type, Component>();
        private static bool mIsShuttingDown;
        private static GameObject mRoot;

        #region Create transform by path

        private static Transform getOrCreateChild(Transform root, string childName)
        {
            var child = root.Find(childName);
            if (child != null)
                return child;
            var childObj = new GameObject(childName);
            child = childObj.transform;
            child.parent = root;
            return child;
        }

        private static Transform getTransformByPath(Transform root, string path)
        {
            while (true)
            {
                if (string.IsNullOrEmpty(path))
                    return root;
                var index = path.IndexOf('/');
                if (index < 0)
                {
                    var child = getOrCreateChild(root, path);
                    return child;
                }
                else
                {
                    var childPath = path.Substring(0, index);
                    var child = getOrCreateChild(root, childPath);
                    root = child;
                    path = path.Substring(index + 1);
                }
            }
        }

        #endregion
    }
}