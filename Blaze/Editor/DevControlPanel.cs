namespace Blaze.Editor
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// 开发面板。
    /// </summary>
    [ExecuteInEditMode]
    public class DevControlPanel : EditorWindow
    {
        private class TabPageEntry
        {
            public DevTabPageAttribute Attribute;
            public IDevTabPage Page;
        }

        /// <summary>
        /// 打开开发控制面板。
        /// </summary>
        [MenuItem("Blaze/Open Dev Control Panel")]
        public static void Open()
        {
            var window = GetWindow<DevControlPanel>();
            window.title = "Dev Panel";
            window.Show();
        }

        protected void OnEnable()
        {
            initialize();
        }

        protected void OnGUI()
        {
            if (!mIsInitialized)
                return;

            ensureStyles();

            if (mTabEntries.Length == 0)
            {
                return;
            }
            GUILayout.BeginHorizontal();
            var layoutOptions = new[]
            {
                GUILayout.Width(72),
                GUILayout.Height(32 * mTabEntries.Length),
            };
            mSelectedTabPageIndex = GUILayout.SelectionGrid(mSelectedTabPageIndex, mTabPageTitles, 1, mToolBarStyle, layoutOptions);
            var tabEntry = mTabEntries[mSelectedTabPageIndex];
            GUILayout.BeginVertical();
            tabEntry.Page.OnGUI();
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }

        private string[] mTabPageTitles;
        private TabPageEntry[] mTabEntries;
        private int mSelectedTabPageIndex;
        private bool mIsInitialized;
        private bool mIsStyleInitialized;

        #region Styles

        private GUIStyle mToolBarStyle;

        #endregion

        #region Layout options

        #endregion

        #region Initializations

        private void initialize()
        {
            initializePages();
            mIsInitialized = true;
        }

        private void initializePages()
        {
            var attributeType = typeof (DevTabPageAttribute);
            var pageType = typeof (IDevTabPage);
            var query = from type in Assembly.GetExecutingAssembly().GetTypes()
                let attribute = (DevTabPageAttribute) type.GetCustomAttributes(attributeType, false).SingleOrDefault()
                where pageType.IsAssignableFrom(type) && !type.IsAbstract
                      && attribute != null
                orderby attribute.Order
                select new TabPageEntry
                {
                    Attribute = attribute,
                    Page = (IDevTabPage) Activator.CreateInstance(type),
                };
            mTabEntries = query.ToArray();
            mTabPageTitles = mTabEntries.Select(entry => entry.Attribute.Title).ToArray();
        }

        private void ensureStyles()
        {
            if (mIsStyleInitialized)
                return;
            mToolBarStyle = new GUIStyle(GUI.skin.GetStyle("ButtonMid")) {fontSize = 12};
            mIsStyleInitialized = true;
        }

        #endregion
    }
}