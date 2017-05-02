namespace Blaze.Editor.Workflow
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml;
    using UnityEditor;
    using UnityEngine;

    public class StartkitWizard : EditorWindow
    {
        /// <summary>
        /// 打开向导窗口。
        /// </summary>
        [MenuItem("Blaze/Workflow/Startkit wizard")]
        public static void Open()
        {
            var window = GetWindow<StartkitWizard>();
            window.title = "Startkit wizard";
            window.Show();
        }

        protected void Awake()
        {
            var path = Application.dataPath + "/Blaze/Editor/Configs/Blaze.config";
            try
            {
                using (var reader = new StreamReader(path))
                    mConfig.Load(reader);
                mIsInitialized = true;
            }
            catch
            {
                mIsInitialized = false;
                Debug.LogWarning("Can't load blaze config file at " + path);
            }
        }

        protected void OnGUI()
        {
            if (!mIsInitialized)
                return;

            GUILayout.BeginHorizontal();
            GUILayout.Label("Project name:");
            mProjectName = GUILayout.TextField(mProjectName, 255);
            GUILayout.EndHorizontal();

            var projectTemplateNodes = BlazeConfig.ProjectTemplates.ToArray();
            if (projectTemplateNodes.Length > 0)
            {
                if (mCurrentProjectTemplateIndex < 0)
                    mCurrentProjectTemplateIndex = 0;
                mCurrentProjectTemplateIndex = EditorGUILayout.Popup(mCurrentProjectTemplateIndex, projectTemplateNodes.Select(node => node["Name"].InnerText).ToArray());

                if (GUILayout.Button("Generate!"))
                {
                    generate(mProjectName, projectTemplateNodes[mCurrentProjectTemplateIndex]);
                    AssetDatabase.Refresh();
                    Close();
                }
            }
        }

        private static void ensureDirectoryExist(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        private void generate(string projectName, XmlNode projectTemplate)
        {
            foreach (XmlNode node in projectTemplate.SelectNodes("Directories/Directory"))
            {
                var relativePath = node.Attributes["Path"].Value.Replace("{ProjectName}", projectName);
                var absolutePath = string.Format("{0}/{1}", Application.dataPath, relativePath);
                ensureDirectoryExist(absolutePath);
            }
        }

        private readonly XmlDocument mConfig = new XmlDocument();
        private int mCurrentProjectTemplateIndex = -1;
        private bool mIsInitialized;
        private string mProjectName = "My project";
    }
}