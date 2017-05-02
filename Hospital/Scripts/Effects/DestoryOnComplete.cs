namespace Muhe.Mjhx.Effects
{
    using UnityEngine;

    /// <summary>
    /// 用于控制特效在其播放完毕后摧毁。
    /// </summary>
    public class DestoryOnComplete : MonoBehaviour
    {
        protected void Start()
        {
            var particle = GetComponent<ParticleSystem>();
            if (particle == null)
            {
                particle = GetComponentInChildren<ParticleSystem>();
            }
            if (particle == null)
                return;
            Destroy(gameObject, particle.duration);
        }
    }
}