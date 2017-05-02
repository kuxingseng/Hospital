namespace Blaze.Framework.Mvc
{
    using System.Collections;
    using Logging;
    using OperationQueues;
    using UI;
    using UnityEngine;

    /// <summary>
    /// 使用<see cref="MonoBehaviour"/>实现的视图。
    /// </summary>
    public abstract class MonoView : MonoBehaviour, IView
    {
        /// <summary>
        /// 获取缓存的<see cref="GameObject"/>。
        /// </summary>
        public GameObject GameObject
        {
            get
            {
                if (mGameObject == null)
                    mGameObject = gameObject;
                return mGameObject;
            }
        }

        /// <summary>
        /// 获取或设置视图所在的层。
        /// </summary>
        public UILayer Layer { get; set; }

        /// <summary>
        /// 获取缓存的<see cref="RectTransform"/>组件。
        /// </summary>
        public RectTransform RectTransform
        {
            get
            {
                if (mRectTransform == null)
                    mRectTransform = GetComponent<RectTransform>();
                return mRectTransform;
            }
        }

        /// <summary>
        /// 获取缓存的<see cref="Transform"/>组件。
        /// </summary>
        public Transform Transform
        {
            get
            {
                if (mTransform == null)
                    mTransform = GetComponent<Transform>();
                return mTransform;
            }
        }

        #region IOriginator Members

        /// <summary>
        /// 捕获当前备忘录状态。
        /// </summary>
        /// <returns></returns>
        public virtual IMemento GetMemento()
        {
            return null;
        }

        /// <summary>
        /// 还原到指定的备忘录状态。
        /// </summary>
        /// <param name="memento">备忘录</param>
        public virtual void SetMemento(IMemento memento)
        {
        }

        #endregion

        #region IView Members

        /// <summary>
        /// 获取视图加载资源的操作。
        /// </summary>
        /// <returns>操作</returns>
        public IOperation LoadContent()
        {
            return new CoroutineOperation(BlazeEngine.Instance, OnLoadContent());
        }

        /// <summary>
        /// 释放视图占用的资源。
        /// </summary>
        public void UnloadContent()
        {
            OnUnloadContent();
        }

        #endregion

        protected virtual void Awake()
        {
        }

        /// <summary>
        /// 获取指定路径下子物体上的组件。
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        /// <param name="path">路径</param>
        protected T GetChild<T>(string path) where T : Component
        {
            var child = Transform.Find(path);
            if (child == null)
            {
                BlazeLog.ErrorFormat("Can't find child '{0}' in {1}", path, GameObject.name);
                return null;
            }
            var component = child.GetComponent<T>();
            if (component == null)
            {
                BlazeLog.ErrorFormat("Can't find component '{0}' in {1}", typeof (T).Name, GameObject.name);
                return null;
            }
            return component;
        }

        /// <summary>
        /// 当需要加载视图资源时调用此协同方法。
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerator OnLoadContent()
        {
            yield break;
        }

        /// <summary>
        /// 当需要施放视图资源时调用此方法。
        /// </summary>
        protected virtual void OnUnloadContent()
        {
        }

        protected virtual void Start()
        {
        }

        private Transform mTransform;
        private RectTransform mRectTransform;
        private GameObject mGameObject;
    }
}