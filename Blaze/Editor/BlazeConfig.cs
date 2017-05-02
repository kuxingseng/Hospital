namespace Blaze.Editor
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// Blaze配置。
    /// </summary>
    [InitializeOnLoad]
    public static class BlazeConfig
    {
        /// <summary>
        /// 获取所有的项目模版配置。
        /// </summary>
        public static XmlNode[] ProjectTemplates { get; private set; }

        static BlazeConfig()
        {
            initialize();
        }

        /// <summary>
        /// 重新加载并刷新Blaze配置。
        /// </summary>
        [MenuItem("Blaze/Refresh config")]
        public static void Refresh()
        {
            initialize();
            Debug.Log("Refresh completed.");
        }

        private static IEnumerable<XmlNode> getProjectTemplateNodes()
        {
            var files = Directory.GetFiles(Application.dataPath + "/Blaze/Editor/Configs/ProjectTemplates", "*.xml");
            foreach (var file in files)
            {
                var xml = new XmlDocument();
                using (var reader = new StreamReader(file))
                {
                    xml.Load(reader);
                }
                yield return xml.FirstChild;
            }
        }

        private static void initialize()
        {
            ProjectTemplates = getProjectTemplateNodes().ToArray();
        }
    }
}