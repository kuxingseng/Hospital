namespace Blaze.Editor
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// 在工程中筛选所有引用了指定组件名称的预设，并列在窗口中。
    /// </summary>
    public class ReferenceFilterWindow : EditorWindow
    {
        [MenuItem("Blaze/Find script reference...")]
        public static void Open()
        {
            GetWindow<ReferenceFilterWindow>(false, "Ref Filter", true);
        }

        protected void OnGUI()
        {
            GUILayout.BeginHorizontal();
            mSearchContent = GUILayout.TextField(mSearchContent);
            if (GUILayout.Button("Search"))
                search();
            GUILayout.EndHorizontal();

            var tip = string.Format("{0} matched.", mMatchedObjects.Count);
            mFoldoutResult = EditorGUILayout.Foldout(mFoldoutResult, tip);
            if (!mFoldoutResult)
                return;
            mScroll = GUILayout.BeginScrollView(mScroll);
            for (var i = 0; i < mMatchedObjects.Count; i++)
            {
                var match = mMatchedObjects[i];
                EditorGUILayout.ObjectField(match.name, match, typeof (GameObject), false);
            }
            GUILayout.EndScrollView();
        }

        private void search()
        {
            mMatchedObjects.Clear();
            var dir = new DirectoryInfo(mAssetsName);
            var files = dir.GetFiles("*.prefab", SearchOption.AllDirectories).ToArray();

            for (var i = 0; i < files.Length; i++)
            {
                var file = files[i];
                var info = string.Format("{0}\n{1}", mSearchContent, file.Name);
                EditorUtility.DisplayProgressBar("Searching...", info, (float) i / files.Length);

                var path = string.Format("{0}{1}", mAssetsName, file.FullName.Substring(dir.FullName.Length));
                var obj = (GameObject) AssetDatabase.LoadAssetAtPath(path, typeof (GameObject));
                if (obj == null)
                    continue;
                var components = obj.GetComponentsInChildren<MonoBehaviour>(true);
                foreach (var component in components)
                {
                    if (component == null)
                        continue;
                    if (!component.GetType().FullName.Contains(mSearchContent))
                        continue;
                    mMatchedObjects.Add(obj);
                    break;
                }
            }
            EditorUtility.ClearProgressBar();
        }

        private const string mAssetsName = "Assets";
        private readonly List<GameObject> mMatchedObjects = new List<GameObject>();
        private bool mFoldoutResult;
        private Vector2 mScroll;
        private string mSearchContent = string.Empty;
    }
}