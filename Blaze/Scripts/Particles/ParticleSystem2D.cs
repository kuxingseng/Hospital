namespace Blaze.Particles
{
    using UnityEngine;
    using UnityEngine.EventSystems;

    /// <summary>
    /// 2D粒子系统。
    /// </summary>
    [RequireComponent(typeof (RectTransform))]
    public class ParticleSystem2D : UIBehaviour
    {
        public bool PlayOnAwake = true;
        public float Duration = 5.0f;

        public void Play()
        {
            var emitter = GetComponent<Particle2DEmitter>();
            emitter.Emit();
        }

        protected override void Start()
        {
            if (PlayOnAwake)
                Play();
        }
    }
}