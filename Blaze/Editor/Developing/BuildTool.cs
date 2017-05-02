namespace Blaze.Editor
{
    using System.Diagnostics;
    using System.IO;
    using UnityEditor;
    using UnityEngine;
    using Debug = UnityEngine.Debug;

    /// <summary>
    /// 发布工具。
    /// </summary>
    public class BuildTool
    {
        /// <summary>
        /// 导出Blaze完整版本的package，包含范例。
        /// </summary>
        [MenuItem("Blaze/Export/Export full package (with samples)")]
        public static void ExportFullPackage()
        {
            initialize();
            mIsIndie = false;
            var assetPaths = new[]
            {
                "Assets/Blaze",
                "Assets/BlazeSample"
            };

            AssetDatabase.ExportPackage(assetPaths, "Build/Blaze v1.0.0 (full).unitypackage", ExportPackageOptions.Recurse);
            Debug.Log("Export full package finished.");
            showOutputDirectory();
        }

        /// <summary>
        /// 导出Blaze独立版本的package，不包含范例。
        /// </summary>
        [MenuItem("Blaze/Export/Export indie package (without samples)")]
        public static void ExportIndiePackage()
        {
            initialize();
            mIsIndie = true;

            AssetDatabase.ExportPackage("Assets/Blaze", "Build/Blaze v1.0.0 (indie).unitypackage", ExportPackageOptions.Recurse);
            Debug.Log("Export indie package finished.");
            showOutputDirectory();
        }

        private static string getOutputFilePath()
        {
            var relativePath = string.Format("../Build/Blaze v1.0.0 ({0}).unitypackage", mIsIndie ? "indie" : "full");
            var ret = Path.GetFullPath(Path.Combine(Application.dataPath, relativePath));
            return ret;
        }

        private static void initialize()
        {
            var outputDirectory = Path.Combine(Application.dataPath, "../Build");
            if (!Directory.Exists(outputDirectory))
                Directory.CreateDirectory(outputDirectory);
        }

        private static void showOutputDirectory()
        {
            var args = string.Format(@"/e,/select,""{0}""", getOutputFilePath());
            Process.Start("explorer.exe", args);
        }

        private static bool mIsIndie;
    }
}