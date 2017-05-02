namespace Assets.Hospital.Scripts.Modules.Views.Map
{
    using System.Collections;
    using DG.Tweening;
    using UnityEngine;

    /// <summary>
    /// author chenshuai
    /// $Id:$
    /// Create time:2017/3/27 17:09:19 
    /// Desc:
    /// </summary>
    ///
    public class MapCamera:MonoBehaviour
    {
        public float ScaleStep=20;

        public float MoveStep=5;

        private const int mMinCameraSize = 320;    //放大到最大十个格子的高度
        private int mMaxCameraSize;   //最小缩放的摄像机尺寸

        public Camera Camera;

        private RectTransform mMapRectTransform;
        private float mMaxOffsetX;
        private float mMaxOffsetY;
        private bool mIsScaleChanged;   

        /// <summary>
        /// 设置地图视图
        /// </summary>
        /// <param name="mapRectTransform"></param>
        public void SetMapRectTransform(RectTransform mapRectTransform)
        {
            mMapRectTransform = mapRectTransform;
            var mapAspect = mapRectTransform.rect.width / mapRectTransform.rect.height;
            //Logger.Debug("mCamera.aspect:" + Camera.aspect);
            //Logger.Debug("mapAspect:" + mapAspect); 
            
            if (Camera.aspect <= mapAspect)
            {
                //摄像机宽高比小于地图宽高比，摄像机最大尺寸以高度为准
                mMaxCameraSize = (int) mapRectTransform.rect.height / 2;
            }
            else
            {
                //摄像机宽高比大于地图宽高比，摄像机最大尺寸以宽度为准
                mMaxCameraSize = (int)(mapRectTransform.rect.width / Camera.aspect) / 2;
            }
        }

        /// <summary>
        /// 移动
        /// </summary>
        /// <param name="direct"></param>
        public void Move(MapMoveDirect direct)
        {
            if (mIsScaleChanged)
            {
                var viewPortHeight = (int)Camera.orthographicSize * 2;
                var viewPortWidth = (int)(viewPortHeight*Camera.aspect);
                mMaxOffsetX = (mMapRectTransform.rect.width - viewPortWidth) / 2;
                mMaxOffsetY = (mMapRectTransform.rect.height - viewPortHeight) / 2;
                mIsScaleChanged = false;
            }
            //Logger.Debug("mMaxOffsetX:" + mMaxOffsetX);
            //Logger.Debug("mMaxOffsetY:" + mMaxOffsetY);
            var currentX = transform.localPosition.x;
            var currentY = transform.localPosition.y;
            var newX = currentX;
            var newY = currentY;
            switch (direct)
            {
                case MapMoveDirect.DOWN:
                    if (currentY > -mMaxOffsetY + MoveStep)
                        newY = currentY - MoveStep;
                    break;
                case MapMoveDirect.UP:
                    if (currentY < mMaxOffsetY - MoveStep)
                        newY = currentY + MoveStep;
                    break;
                case MapMoveDirect.RIGHT:
                    if (currentX < mMaxOffsetX - MoveStep)
                        newX = currentX + MoveStep;
                    break;
                case MapMoveDirect.LEFT:
                    if (currentX > -mMaxOffsetX + MoveStep)
                        newX = currentX - MoveStep;
                    break;
            }
            transform.localPosition=new Vector3(newX,newY,-10);
        }

        private void Start()
        {
            mIsScaleChanged = true;
            Camera = transform.GetComponent<Camera>();
        }

        private void Update()
        {
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                var currentScale = Camera.orthographicSize;
                if (currentScale < mMaxCameraSize - ScaleStep)
                {
                    mIsScaleChanged = true;
                    Camera.orthographicSize = currentScale + ScaleStep;

                    makeMapInViewport();
                }   
            }
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                var currentScale = Camera.orthographicSize;
                if (currentScale > mMinCameraSize)
                {
                    mIsScaleChanged = true;
                    Camera.orthographicSize = currentScale - ScaleStep;
                }  
            }
            if(Input.GetKey(KeyCode.A))
                Move(MapMoveDirect.LEFT);
            if (Input.GetKey(KeyCode.D))
                Move(MapMoveDirect.RIGHT);
            if (Input.GetKey(KeyCode.W))
                Move(MapMoveDirect.UP);
            if (Input.GetKey(KeyCode.S))
                Move(MapMoveDirect.DOWN);
        }

        ////控制地图始终在边界内
        private void makeMapInViewport()
        {
            var viewPortHeight = (int)Camera.orthographicSize * 2;
            var viewPortWidth = (int)(viewPortHeight * Camera.aspect);
            var cameraMaxX = (mMapRectTransform.rect.width - viewPortWidth) / 2;
            var cameraMaxY = (mMapRectTransform.rect.height - viewPortHeight) / 2;

            var currentX = transform.localPosition.x;
            var currentY = transform.localPosition.y;
            if (transform.localPosition.x > cameraMaxX)
                currentX = cameraMaxX;
            if (transform.localPosition.x < -cameraMaxX)
                currentX = -cameraMaxX;
            if (transform.localPosition.y > cameraMaxY)
                currentY = cameraMaxY;
            if (transform.localPosition.y < -cameraMaxY)
                currentY = -cameraMaxY;

            transform.localPosition = new Vector3(currentX, currentY, -10);
        }
    }
}
