namespace Blaze.UI.Emoticons
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;
    using Framework;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// 为<see cref="Text"/>组件提供表情图片支持。
    /// </summary>
    [AddComponentMenu("Blaze/UI/EmoticonText")]
    [RequireComponent(typeof (Text))]
    public class EmoticonText : BaseMeshEffect
    {
        public bool UseNativeSize;

        /// <summary>
        /// 获取或设置需要显示的表情格式化文本。
        /// </summary>
        public string SourceText
        {
            get { return mSourceText; }
            set
            {
                if (mSourceText == value)
                    return;
                mSourceText = value;
                update();
            }
        }

        protected override void Awake()
        {
            if (!Application.isPlaying)
                return;
            if (mDatabase == null)
                mDatabase = Singleton.GetInstance<EmoticonDatabase>();
            mText = GetComponent<Text>();
            mRectTransform = GetComponent<RectTransform>();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            foreach (var image in mEmoticons.Values)
                mDatabase.Put(image);
        }

        protected override void Start()
        {
            if (!Application.isPlaying)
                return;
            update();
        }

        private void addChar(string value)
        {
            if (String.IsNullOrEmpty(value))
                return;
            mTextBuffer.Append(value);
        }

        private void addEmoticon(string symbol)
        {
            if (symbol == null || symbol.Length < 4)
                return;
            symbol = symbol.Substring(1, symbol.Length - 2);
            var index = mTextBuffer.Length;
            var buffer = new StringBuilder();
            var emoticon = mDatabase.Get(symbol);
            emoticon.RectTransform.SetParent(mRectTransform, false);
            if (UseNativeSize)
            {
                buffer.Append("<size=").Append(emoticon.Size).Append(">");
                index += buffer.Length;
            }
            mEmoticons.Add(index, emoticon);

            if (UseNativeSize)
            {
                mTextBuffer.Append(buffer).Append("　").Append("</size>");       //utf8下全角空格占3个字节 半角空格占1个字节
            }
            else
            {
                addChar("　");
            }
        }

        private void clear()
        {
            mSourceText = string.Empty;
            mTextBuffer.Length = 0;
        }

        private string parseEmoticon(string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            var matchs = Regex.Matches(value, EmoticonDatabase.PatternEmoticon);
            if (matchs.Count > 0)
            {
                var endString = value;
                foreach (Match item in matchs)
                {
                    var str = item.Value;
                    var startIndex = endString.IndexOf(str, StringComparison.Ordinal);
                    var endIndex = startIndex + str.Length;
                    if (startIndex > -1)
                    {
                        var sub = endString.Substring(0, startIndex);
                        addChar(sub);
                        addEmoticon(str);
                        endString = endString.Substring(endIndex);
                    }
                }
                addChar(endString);
            }
            else
            {
                addChar(value);
            }
            mIsInitialized = true;
            return mTextBuffer.ToString();
        }

        private void update()
        {
            if (!Application.isPlaying)
                return;

            mTextBuffer.Length = 0;
            foreach (var image in mEmoticons.Values)
                mDatabase.Put(image);
            mEmoticons.Clear();
            mText.text = parseEmoticon(mSourceText);
            graphic.SetVerticesDirty();
        }

        private IEnumerator updateEmoticons(List<UIVertex> verts)
        {
            verts = new List<UIVertex>(verts);
            yield return null;
            foreach (var pair in mEmoticons)
            {
                if (pair.Key < 0 || pair.Key * 6 >= verts.Count)    //每个文字、图片有六个顶点
                    continue;

                var vertices = new[]
                {
                    verts[pair.Key * 6],
                    verts[pair.Key * 6 + 1],
                    verts[pair.Key * 6 + 2],
                    verts[pair.Key * 6 + 3]
                };
                updateImagePosition(pair.Value, vertices);
                pair.Value.RectTransform.SetParent(mRectTransform, true);
            }
        }

        private void updateImagePosition(Emoticon image, UIVertex[] vertices)
        {
            var size = UseNativeSize ? image.Size : mText.fontSize;
            image.RectTransform.sizeDelta = new Vector2(size, size);
            var x = (vertices[0].position.x + vertices[1].position.x) / 2;
            if (vertices[0].position.x == vertices[1].position.x)
                x += image.RectTransform.rect.width / 2;
            var y = (vertices[0].position.y + vertices[3].position.y) / 2;
            if ((vertices[0].position.y == vertices[3].position.y))
                y += image.RectTransform.rect.height / 2 - size / 8.0f; //修正和文字对齐，文字会整体向下偏移
            image.RectTransform.anchoredPosition = new Vector2(x, y);
        }

        [TextArea(3, 10)]
        [SerializeField]
        private string mSourceText;

        private readonly StringBuilder mTextBuffer = new StringBuilder();
        private readonly Dictionary<int, Emoticon> mEmoticons = new Dictionary<int, Emoticon>();
        private static EmoticonDatabase mDatabase;
        private Text mText;
        private RectTransform mRectTransform;
        private bool mIsInitialized;
        
        public override void ModifyMesh(VertexHelper vh)
        {
            if (!IsActive() || vh.currentVertCount == 0 || !mIsInitialized)
                return;

            var _vertexList = new List<UIVertex>();
            vh.GetUIVertexStream(_vertexList);

            if (mText == null || string.IsNullOrEmpty(mText.text))
                return;

            StartCoroutine(updateEmoticons(_vertexList));
        }
    }
}