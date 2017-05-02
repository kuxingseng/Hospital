namespace Muhe.Mjhx.UI.Common
{
    using System;
    using UnityEngine;
    using UnityEngine.EventSystems;

    /// <summary>
    /// author chenshuai
    /// button放在scorllrect中使用时，事件不能继承drag等接口，独立出来处理
    /// 处理点击、长按、重复按操作(长按和重复按不能叠加在同一个对象上)
    /// $Id$
    /// Create time:2015/4/30 11:46:58 
    /// </summary>
    ///
    public class UGUIButtonEventTrigger : MonoBehaviour,IPointerClickHandler,IPointerDownHandler,IPointerUpHandler,IPointerExitHandler
    {
        public delegate void VoidDelegate(GameObject go);
        public VoidDelegate OnClick;
        public VoidDelegate OnExit;
        public VoidDelegate OnLongTimePress;
        public VoidDelegate OnLongTimePressUp;
        public VoidDelegate OnRepeatPress;
        public VoidDelegate OnRepeatPressUp;

        public float LongTimePressDelayTime = 0.6f;
        public float RepeatPressInterval = 0.4f;
        private float mBeginTime;
        private bool mBeginLongTimePressListen=false;
        private bool mLongTimePressListening = false;
        private bool mBeginRepeatPressListen=false;
        private bool mRepeatPressListening = false;
        private bool mIsCancelClickListening = false;   //当监听click事件的同时监听了长按或者repeat,在其他事件已相应的时候不再响应click事件

        public static UGUIButtonEventTrigger Get(GameObject go)
        {
            UGUIButtonEventTrigger listener = go.GetComponent<UGUIButtonEventTrigger>();
            if (listener == null)
                listener = go.AddComponent<UGUIButtonEventTrigger>();

            return listener;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
                return;

            if (!gameObject.activeSelf)
                return;

            if (OnClick != null && !mIsCancelClickListening)
                OnClick(gameObject);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
                return;

            if (!gameObject.activeSelf)
                return;

            if (OnLongTimePress != null)
            {
                mBeginTime = Time.time;
                mBeginLongTimePressListen = true;
                mLongTimePressListening = false;
            }

            if (OnRepeatPress != null)
            {
                mBeginTime = Time.time;
                mBeginRepeatPressListen = true;
            }
            mIsCancelClickListening = false;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            //Debug.Log("OnPointerUp");
            if (eventData.button != PointerEventData.InputButton.Left)
                return;

            if (!gameObject.activeSelf)
                return;

            if (OnLongTimePressUp != null && mLongTimePressListening)
            {
                OnLongTimePressUp(gameObject);
                mIsCancelClickListening = true;
            }


            if (OnRepeatPressUp != null && mRepeatPressListening)
            {
                OnRepeatPressUp(gameObject);
                mIsCancelClickListening = true;
            }   

            mBeginLongTimePressListen = false;
            mBeginRepeatPressListen = false;
            mLongTimePressListening = false;
            mRepeatPressListening = false;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
                return;

            if (!gameObject.activeSelf)
                return;

            if (OnExit != null)
                OnExit(gameObject);
//            Debug.Log("OnPointerExit");
//            if (eventData.button != PointerEventData.InputButton.Left)
//                return;
//
//            if (!gameObject.activeSelf)
//                return;
//
//            if (OnLongTimePressUp != null && mLongTimePressListening)
//            {
//                OnLongTimePressUp(gameObject);
//                mIsCancelClickListening = true;
//            }
//
//
//            if (OnRepeatPressUp != null && mRepeatPressListening)
//            {
//                OnRepeatPressUp(gameObject);
//                mIsCancelClickListening = true;
//            }   
//
//            mBeginLongTimePressListen = false;
//            mBeginRepeatPressListen = false;
//            mLongTimePressListening = false;
//            mRepeatPressListening = false;
        }

        private void Update()
        {
            if (mBeginLongTimePressListen)
            {
                //Debug.Log("long time press:" + Time.deltaTime + " " + mBeginTime + " " + mLongTimePressDelayTime);
                if (Time.time - mBeginTime >= LongTimePressDelayTime)
                {
                    if (OnLongTimePress != null)
                        OnLongTimePress(gameObject);

                    mBeginLongTimePressListen = false;
                    mLongTimePressListening = true;
                }
            }

            if (mBeginRepeatPressListen)
            {
                if (Time.time - mBeginTime >= RepeatPressInterval)
                {
                    mBeginTime = Time.time;
                    if (OnRepeatPress != null)
                        OnRepeatPress(gameObject);

                    mRepeatPressListening = true;
                }
            }
        }
    }
}
