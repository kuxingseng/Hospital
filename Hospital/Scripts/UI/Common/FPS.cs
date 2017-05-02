namespace Muhe.Mjhx.UI.Common
{
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// author chenshuai
    /// $Id$
    /// Create time:2016/2/4 11:58:16 
    /// </summary>
    ///
    [RequireComponent(typeof(Text))]
    public class FPS:MonoBehaviour
    {
        private Text mText;
        private float mFps = 60;

        public float updateInterval = 0.5F;

        private float accum = 0; // FPS accumulated over the interval
        private int frames = 0; // Frames drawn over the interval
        private float timeleft; // Left time for current interval

        void Awake()
        {
            mText = GetComponent<Text>();
        }

//        void LateUpdate()
//        {
//            float interp = Time.deltaTime / (1f + Time.deltaTime);
//            float currentFPS = 1.0f / Time.deltaTime;
//            mFps = Mathf.Lerp(mFps, currentFPS, interp);
//            mText.text = Mathf.RoundToInt(mFps) + "fps";
//        }

        void Start()
        {
            timeleft = updateInterval;
        }

        void Update()
        {
            timeleft -= Time.deltaTime;
            accum += Time.deltaTime/Time.timeScale;
            ++frames;

            // Interval ended - update GUI text and start new interval
            if (timeleft <= 0.0)
            {
                // display two fractional digits (f2 format)
                float fps = frames / accum;
                string format = System.String.Format("{0:F1} FPS", fps);
                mText.text = format;

                if (fps < 10)
                    mText.color = Color.red;
                else if (fps <= 20&&fps>=10)
                    mText.color = Color.yellow;
                else
                    mText.color = Color.green;

                //  DebugConsole.Log(format,level);
                timeleft = updateInterval;
                accum = 0.0F;
                frames = 0;
            }
        }
    }
}
