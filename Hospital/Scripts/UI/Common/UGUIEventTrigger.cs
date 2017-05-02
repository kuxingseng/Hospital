namespace Muhe.Mjhx.UI.Common
{
    using UnityEngine;
    using UnityEngine.EventSystems;

    /// <summary>
    /// author chenshuai
    /// $Id$
    /// Create time:2015/2/25 15:45:59 
    /// </summary>
    ///
    public class UGUIEventTrigger : EventTrigger
    {
        public delegate void VoidDelegate(GameObject go);
        public delegate void VoidDelegetWithData(GameObject go, PointerEventData eventData);
        public delegate void MoveDelegetWithData(GameObject go, AxisEventData eventData);
        public VoidDelegate onClick;
        public VoidDelegetWithData onEnter;
        public VoidDelegate onExit;
        public VoidDelegetWithData onPointerDown;
        public VoidDelegetWithData onPointerUp;
        public VoidDelegetWithData onEndDrag;
        public VoidDelegetWithData onDrag;
        public VoidDelegetWithData onDrop;
        public MoveDelegetWithData onMove;

        public static UGUIEventTrigger Get(GameObject go)
        {
            UGUIEventTrigger trigger = go.GetComponent<UGUIEventTrigger>();
            if (trigger == null)
                trigger=go.AddComponent<UGUIEventTrigger>();

            return trigger;
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
                return;

            if (!gameObject.activeSelf)
                return;

            if (onClick != null)
                onClick(gameObject);
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
                return;

            if (!gameObject.activeSelf)
                return;

            if (onEnter != null)
                onEnter(gameObject,eventData);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
                return;

            if (!gameObject.activeSelf)
                return;

            if (onExit != null)
                onExit(gameObject);
        }

        public override void OnDrag(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
                return;

            if (!gameObject.activeSelf)
                return;
            
            if (onDrag != null)
                onDrag(gameObject, eventData);
        }

        public override void OnDrop(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
                return;

            if (!gameObject.activeSelf)
                return;

            if (onDrop != null)
                onDrop(gameObject, eventData);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
                return;

            if (!gameObject.activeSelf)
                return;

            if (onPointerUp != null)
                onPointerUp(gameObject,eventData);
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
                return;

            if (!gameObject.activeSelf)
                return;

            if (onPointerDown != null)
                onPointerDown(gameObject,eventData);
        }
        
        public override void OnEndDrag(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
                return;

            if (!gameObject.activeSelf)
                return;

            if (onEndDrag != null)
                onEndDrag(gameObject, eventData);
        }

        public override void OnMove(AxisEventData eventData)
        {
            if (!gameObject.activeSelf)
                return;

            if (onMove != null)
                onMove(gameObject, eventData);
        }
    }
}
