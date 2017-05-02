namespace Blaze.Particles
{
    using Framework;
    using Framework.Pooling;
    using UnityEngine;

    /// <summary>
    /// 2D粒子发射器。
    /// </summary>
    public class Particle2DEmitter : MonoBehaviour
    {
        public int MaxParticle = 100;
        public GameObject ParticlePrefab;

        [ContextMenu("Emit")]
        public void Emit()
        {
            for (var i = 0; i < mAvailableCount; i++)
            {
                var obj = mPool.Get();
                var particle = obj.GetOrAddComponent<Particle2D>();
                particle.Life = 10;
                particle.Velocity = new Vector3(0, Random.value, 0);
                particle.RectTransform.SetParent(transform, false);
            }
            mAvailableCount = 0;
        }

        public void Recycle(Particle2D particle)
        {
            mPool.Put(particle.GameObject);
            mAvailableCount++;
        }

        public void Start()
        {
            mPool = new GameObjectPool(ParticlePrefab, MaxParticle);
            mAvailableCount = MaxParticle;
        }

        private GameObjectPool mPool;
        private int mAvailableCount;
    }
}