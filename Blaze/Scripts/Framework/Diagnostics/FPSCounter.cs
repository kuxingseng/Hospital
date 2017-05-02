namespace Blaze.Framework.Diagnostics
{
    using UnityEngine;

    /// <summary>
    /// ”√”⁄º∆À„FPS°£
    /// </summary>
    public class FPSCounter : MonoBehaviour
    {
        public float FPS;

        public void Draw()
        {
            GUILayout.Label("FPS:" + FPS.ToString("f1"));
        }

        protected void Update()
        {
            FPS = 1 / Time.deltaTime;
        }
    }
}