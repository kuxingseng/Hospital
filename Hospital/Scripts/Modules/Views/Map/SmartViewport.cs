namespace Assets.Hospital.Scripts.Modules.Views.Map
{
    using System.Collections;
    using UnityEngine;

    /// <summary>
    /// author chenshuai
    /// $Id:$
    /// Create time:2017/3/27 18:05:34 
    /// Desc:
    /// </summary>
    ///
    [RequireComponent(typeof(Camera))]
    public class SmartViewport:MonoBehaviour
    {
        public RectTransform Target { get; set; }

        private Camera mCamera;

        private bool mCloseSet;

        protected void Awake()
        {
            mCamera = GetComponent<Camera>();
        }

        protected void Start()
        {
            StartCoroutine(Count());
        }

        protected void OnDestroy()
        {
            StopCoroutine(Count());
        }

        //计时，两秒后不再修改摄像机坐标（设置事件不固定）
        protected IEnumerator Count()
        {
            yield return new WaitForSeconds(2f);
            mCloseSet = true;
        }

        protected void LateUpdate()
        {
            if (mCloseSet)
                return;
            if (Target == null || Camera.main == null)
                return;
            var corners = new Vector3[4];
            Target.GetWorldCorners(corners);//获取ui控件世界坐标系的四个顶点坐标 依次从左下到右下

            for (var i = 0; i < corners.Length; i++)
            {
                corners[i] = Camera.main.WorldToViewportPoint(corners[i]); //将世界坐标转换为屏幕坐标
            }

            //修改相机的ViewportRect
            mCamera.rect = new Rect(corners[0].x, corners[0].y,
                corners[3].x - corners[0].x, corners[1].y - corners[0].y);
        }
    }
}
