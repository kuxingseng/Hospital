namespace Blaze.Particles
{
    using UnityEngine;

    /// <summary>
    /// 2D粒子。
    /// </summary>
    public class Particle2D : MonoBehaviour
    {
        public Color Color;
        public float Life;
        public Vector3 Velocity;

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
        /// 获取缓存的<see cref="RectTransform"/>组件。
        /// </summary>
        public RectTransform RectTransform
        {
            get
            {
                if (mRectTransform == null)
                    mRectTransform = gameObject.GetComponent<RectTransform>();
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
                    mTransform = gameObject.GetComponent<Transform>();
                return mTransform;
            }
        }

        protected void Start()
        {
            mEmitter = GetComponentInParent<Particle2DEmitter>();
        }

        protected void Update()
        {
            Life -= Time.deltaTime;
            if (Life <= 0)
            {
                Life = 0;
                mEmitter.Recycle(this);
                return;
            }

            RectTransform.Translate(Velocity * Time.deltaTime);
        }

        private Particle2DEmitter mEmitter;
        private Transform mTransform;
        private RectTransform mRectTransform;
        private GameObject mGameObject;
    }
}