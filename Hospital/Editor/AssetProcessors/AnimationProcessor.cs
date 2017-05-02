namespace Muhe.Mjhx.Editor.AssetProcessors
{
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Blaze.Framework;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// 预处理动画-生成对应的prefab
    /// </summary>
    public class AnimationProcessor : AssetPostprocessor
    {
        protected void OnPreprocessAnimation()
        {
            //（无需单独加载动画资源，暂不生成prefab）
//            var match = mDynamicRegex.Match(assetPath);
//            if (match.Success)
//            {
//                processAnimation(match);
//            }
        }

        private void processAnimation(Match match)
        {
            var dir = match.Result("${dir}");
            var file = match.Result("${file}");

            var prefabDir = UnityPath.GetProjectPath(string.Format("MjhxResource/Character/{0}", dir));
            if (!Directory.Exists(prefabDir))
            {
                Directory.CreateDirectory(prefabDir);
                AssetDatabase.Refresh();
            }
            EditorApplication.delayCall += () =>
            {
                var obj = (GameObject) AssetDatabase.LoadAssetAtPath(assetPath, typeof (GameObject));
                var prefabPath = string.Format("Assets/MjhxResource/Character/{0}/{1}.prefab", dir,Path.GetFileNameWithoutExtension(file));
                PrefabUtility.CreatePrefab(prefabPath, obj);
                //Object.DestroyImmediate(obj,true);
            };
        }
        
        private static readonly Regex mDynamicRegex = new Regex("Assets/MjhxResource/Character/(?<dir>.+?)/(?<file>.+)");
    }
}