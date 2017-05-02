namespace Assets.Hospital.Scripts.Modules.Views.Tips
{
    using System.Collections;
    using Controllers.Tips;
    using DG.Tweening;
    using Muhe.Mjhx.Modules;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    /// <summary>
    /// author chenshuai
    /// $Id:$
    /// Create time:2017/4/20 16:17:46 
    /// Desc:
    /// </summary>
    ///
    public class TextTipsView:ModuleView
    {
        private Text mText;
        private Image mClickHandler;
        private RectTransform mTips;
        private TextTipsController mController;
        private Image mBackGround;
        private Text mContent;
        private Outline mContentOutLine;

        #region IPointerClickHandler Members

        public void OnPointerClick(PointerEventData eventData)
        {
            mController.Confirm();
        }

        #endregion

        /// <summary>
        /// 设置提示的文本内容。
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="isAutoDisappear">是否自动消失</param>
        public IEnumerator SetContent(string text, bool isAutoDisappear)
        {
            Content = text;
            if (isAutoDisappear)
            {
                mClickHandler.gameObject.SetActive(false);

                mBackGround.color = new Color(255, 255, 255, 0);
                mContent.color = new Color(255, 255, 255, 0);
                mContentOutLine.effectColor = new Color(0, 0, 0, 0);

                Sequence mySequence = DOTween.Sequence();

                var appearSequence = DOTween.Sequence();
                appearSequence.Join(mBackGround.DOFade(1f, 0.3f));
                appearSequence.Join(mContentOutLine.DOFade(1, 0.3f));
                appearSequence.Join(mContent.DOFade(1, 0.3f));
                appearSequence.Join(mTips.DOLocalMoveY(-100, 0f));
                appearSequence.Join(mTips.DOLocalMoveY(0, 0.3f));

                var disappearSequence = DOTween.Sequence();
                disappearSequence.Join(mBackGround.DOFade(0f, 0.5f));
                disappearSequence.Join(mContentOutLine.DOFade(0, 0.3f));
                disappearSequence.Join(mContent.DOFade(0, 0.3f));
                disappearSequence.Join(mTips.DOLocalMoveY(100, 0.5f));

                mySequence.Append(appearSequence);
                mySequence.AppendInterval(1f);
                mySequence.Append(disappearSequence);
                yield return mySequence.WaitForCompletion();

                mController.Confirm();
            }
        }

        /// <summary>
        /// 获取或设置文本内容。
        /// </summary>
        public string Content
        {
            get { return mText.text; }
            set
            {
                if (value == null)
                    mText.text = string.Empty;
                else
                    mText.text = value;
            }
        }


        protected override void Start()
        {
            mController = GetComponent<TextTipsController>();
        }

        protected override void Awake()
        {
            base.Awake();
            mText = GetChild<Text>("Panel-tips/Text");
            mClickHandler = GetChild<Image>("Image-clickHandler");
            mTips = GetChild<RectTransform>("Panel-tips");
            mBackGround = mTips.GetComponentInChildren<Image>();
            mContent = mTips.GetComponentInChildren<Text>();
            mContentOutLine = mTips.GetComponentInChildren<Outline>();
        }
    }
}
