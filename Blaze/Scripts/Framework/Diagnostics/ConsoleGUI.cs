namespace Blaze.Framework.Diagnostics
{
    using System;
    using UnityEngine;

    /// <summary>
    /// 屏幕控制台显示。
    /// </summary>
    public class DevConsoleGUI : MonoBehaviour
    {
        /// <summary>
        /// 当第一次选中输入框时触发此事件。
        /// </summary>
        public event Action InputTextFieldFocused;

        /// <summary>
        /// 当输入的指令被提交时触发此事件。
        /// </summary>
        public event Action<string> Submit;

        /// <summary>
        /// 获取或设置控制台命令输入框的文本。
        /// </summary>
        public string InputText
        {
            get { return mInput; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");
                mInput = value;
            }
        }

        /// <summary>
        /// 获取或设置一个值，表示控制台是否为收起状态。
        /// </summary>
        public bool IsShrink { get; set; }

        /// <summary>
        /// 获取或设置控制台内显示的文本。
        /// </summary>
        public string Text
        {
            get { return mText; }
            set
            {
                mText = value;
                if (mText == null)
                    mText = string.Empty;
            }
        }

        /// <summary>
        /// 清除所有的用户输入。
        /// </summary>
        public void ClearInput()
        {
            mInput = string.Empty;
        }

        protected void OnApplicationQuit()
        {
            enabled = false;
        }

        protected void OnGUI()
        {
            if (!DevConsole.Enabled)
                return;
            GUI.SetNextControlName(string.Empty);
            Rect dragRect;
            if (IsShrink)
                dragRect = GUI.Window(1, mWindowRect, drawShrinkWindow, "Dev");
            else
                dragRect = GUI.Window(1, mWindowRect, drawExpandWindow, "DevConsole");
            if (dragRect.xMin >= 0
                && dragRect.yMin >= 0
                && dragRect.xMax < Screen.width
                && dragRect.yMax < Screen.height)
                mWindowRect = dragRect;
            mLastFocusControlName = GUI.GetNameOfFocusedControl();
        }

        protected void Start()
        {
            mWindowRect = getInitWindowRect();
            mFPSCounter = gameObject.AddComponent<FPSCounter>();
        }

        private static Rect getInitWindowRect()
        {
            return new Rect(Screen.width / 4.0f, Screen.height / 4.0f, Screen.width / 2.0f, Screen.height / 2.0f);
        }

        private bool checkFirstForcus(string controlName)
        {
            return GUI.GetNameOfFocusedControl() == controlName
                   && mLastFocusControlName != controlName;
        }

        private void submitCommandLine()
        {
            if (Submit != null)
                Submit(mInput);
            ClearInput();
        }

        #region DrawExpandedWindow

        private void drawClient()
        {
            mScrollPosition = GUILayout.BeginScrollView(mScrollPosition, false, true);
            GUILayout.TextArea(Text, mWindowStyle);
            GUILayout.EndScrollView();

            GUILayout.BeginHorizontal();
            GUI.SetNextControlName(mInputTextFieldName);
            mInput = GUILayout.TextField(mInput);

            //检测触发选中输入框事件
            if (checkFirstForcus(mInputTextFieldName))
            {
                if (InputTextFieldFocused != null)
                    InputTextFieldFocused();
            }

            //按回车提交
            if (Event.current.isKey && Event.current.keyCode == KeyCode.Return)
            {
                submitCommandLine();
                return;
            }

            //按提交按钮
            if (GUILayout.Button("Run", GUILayout.Width(64)))
                submitCommandLine();
            GUILayout.EndHorizontal();
        }

        private void drawExpandWindow(int windowID)
        {
            drawMenu();
            drawClient();
            GUI.DragWindow();
        }

        private void drawMenu()
        {
            //菜单项
            GUILayout.BeginHorizontal();
            //收起
            if (GUILayout.Button("-", GUILayout.Width(32)))
            {
                mWindowRect = new Rect(mWindowRect.xMin, mWindowRect.yMin, 64, 64);
                IsShrink = true;
                return;
            }
            //清屏
            if (GUILayout.Button("C", GUILayout.Width(32)))
            {
                Text = string.Empty;
                return;
            }

            //FPS
            mFPSCounter.Draw();
            GUILayout.EndHorizontal();
        }

        #endregion

        #region DrawShrinkWindow

        private void drawShrinkWindow(int windowID)
        {
            if (GUILayout.Button("+"))
            {
                IsShrink = false;
                mWindowRect = getInitWindowRect();
            }
            GUI.DragWindow();
        }

        #endregion

        private FPSCounter mFPSCounter;
        private string mInput = string.Empty;
        private string mLastFocusControlName;
        private Vector2 mScrollPosition;
        private string mText = string.Empty;
        private Rect mWindowRect;

        #region Control names

        private const string mInputTextFieldName = "InputTextField";

        #endregion

        #region GUIStyles

        private static readonly GUIStyle mWindowStyle = new GUIStyle
        {
            normal = new GUIStyleState
            {
                textColor = Color.white,
            }
        };

        #endregion
    }
}