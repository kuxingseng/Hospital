namespace Blaze.Framework.Utilities
{
    using UnityEngine;

    public class DontDestroyOnLoad : MonoBehaviour
    {
        protected void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}