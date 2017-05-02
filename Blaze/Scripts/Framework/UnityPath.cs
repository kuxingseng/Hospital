namespace Blaze.Framework
{
    using System;
    using System.IO;
    using UnityEngine;

    /// <summary>
    /// 提供Unity项目常用的路径。
    /// </summary>
    public static class UnityPath
    {
        /// <summary>
        /// 获取StreamingAssets的路径。
        /// 如：
        /// Windows Standalone：file:///E:/Projects/Blaze/Assets/StreamingAssets/
        /// </summary>
        public static string StreamingAssets
        {
            get
            {
                if (mStreamingAssets == null)
                {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
                    mStreamingAssets = "file:///" + Application.streamingAssetsPath;
#elif UNITY_ANDROID
                    mStreamingAssets = "jar:file://" + Application.dataPath + "!/assets";
#elif UNITY_IPHONE
                    mStreamingAssets = "file:///" + Application.streamingAssetsPath;
#else
                    mStreamingAssets  = string.Empty;
#endif
                }
                return mStreamingAssets;
            }
        }

        /// <summary>
        /// 获得streamingAssetsPath路径（可通过AssetBundle.LoadFromFile加载的路径）
        /// </summary>
        public static string StreamingAssetsPath
        {
            get
            {
                if (mStreamingAssetsPath == null)
                {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
                    mStreamingAssetsPath = Application.streamingAssetsPath;
#elif UNITY_ANDROID
                    mStreamingAssetsPath = Application.dataPath + "!assets";
#elif UNITY_IPHONE
                    mStreamingAssetsPath = Application.streamingAssetsPath;
#else
                    mStreamingAssetsPath  = string.Empty;
#endif
                }
                return mStreamingAssetsPath;
            }
        }

        /// <summary>
        /// Creates a relative path from one file or folder to another.
        /// </summary>
        /// <param name="fromPath">Contains the directory that defines the start of the relative path.</param>
        /// <param name="toPath">Contains the path that defines the endpoint of the relative path.</param>
        /// <returns>The relative path from the start directory to the end path.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string MakeRelativePath(String fromPath, String toPath)
        {
            if (string.IsNullOrEmpty(fromPath))
                throw new ArgumentNullException("fromPath");
            if (string.IsNullOrEmpty(toPath))
                throw new ArgumentNullException("toPath");

            var fromUri = new Uri(fromPath);
            var toUri = new Uri(toPath);

            var relativeUri = fromUri.MakeRelativeUri(toUri);
            var relativePath = Uri.UnescapeDataString(relativeUri.ToString());

            return relativePath.Replace('/', Path.DirectorySeparatorChar);
        }

        private static string mStreamingAssetsPath;
        private static string mStreamingAssets;
#if UNITY_EDITOR

        /// <summary>
        /// 获取项目根路径。如：
        /// <remarks>E:/Projects/Blaze</remarks>
        /// </summary>
        public static string ProjectRoot
        {
            get
            {
                if (mProjectRoot == null)
                    mProjectRoot = Application.dataPath.Substring(0, Application.dataPath.Length - 7);
                return mProjectRoot;
            }
        }

        /// <summary>
        /// 获取资源根路径。如：
        /// <remarks>E:/Projects/Blaze/Assets</remarks>
        /// </summary>
        public static string AssetsRoot
        {
            get { return Application.dataPath; }
        }

        /// <summary>
        /// 获取相对于工程目录的绝对路径。
        /// </summary>
        /// <param name="relativePath">相对路径</param>
        /// <returns>绝对路径</returns>
        public static string GetProjectPath(string relativePath)
        {
            return Path.GetFullPath(Path.Combine(ProjectRoot, relativePath));
        }

        /// <summary>
        /// 获取相对于Assets目录的绝对路径。
        /// </summary>
        /// <returns>绝对路径</returns>
        public static string GetAssetsPath(string relativePath)
        {
            return Path.GetFullPath(Path.Combine(Application.dataPath, relativePath));
        }

        /// <summary>
        /// 获取相对于<see cref="AssetsRoot"/>的路径。
        /// </summary>
        /// <param name="absolutePath">绝对路径</param>
        /// <returns></returns>
        public static string GetAssetsRelativePath(string absolutePath)
        {
            absolutePath = Path.GetFullPath(absolutePath).Replace('\\', '/');
            return absolutePath.Substring(AssetsRoot.Length + 1, absolutePath.Length - AssetsRoot.Length - 1).TrimStart(mSeparators);
        }

        private static string mProjectRoot;
        private static readonly char[] mSeparators = {'\\', '/'};

        /// <summary>
        /// 获取相对于<see cref="ProjectRoot"/>的路径。
        /// </summary>
        /// <param name="absolutePath">绝对路径</param>
        /// <returns></returns>
        public static string GetProjectRelativePath(string absolutePath)
        {
            absolutePath = Path.GetFullPath(absolutePath).Replace('\\', '/');
            return absolutePath.Substring(ProjectRoot.Length + 1, absolutePath.Length - ProjectRoot.Length - 1).TrimStart(mSeparators);
        }
#endif
    }
}