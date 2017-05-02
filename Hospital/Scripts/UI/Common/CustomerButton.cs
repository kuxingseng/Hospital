namespace Muhe.Mjhx.UI.Common
{
    using DG.Tweening;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    /// <summary>
    /// author chenshuai
    /// $Id$
    /// Create time:2015/12/9 17:56:09 
    /// </summary>
    ///
    [ExecuteInEditMode]
    [RequireComponent(typeof(Button))]
    public class CustomerButton:MonoBehaviour,IPointerDownHandler,IPointerUpHandler,IPointerClickHandler
    {
        private Button mButton;

        protected void Start()
        {
            mButton = GetComponent<Button>();
            mButton.transition = Selectable.Transition.None;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            //mButton.GetComponent<Image>().DOFade(0.5f, 0.5f);
            mButton.transform.DOScale(Vector3.one*0.8f, 0.3f);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            //mButton.GetComponent<Image>().DOFade(1f, 0.5f);
            mButton.transform.DOScale(Vector3.one, 0.3f);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
        }
    }
}
