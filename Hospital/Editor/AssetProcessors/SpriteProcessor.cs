namespace Muhe.Mjhx.Editor.AssetProcessors
{
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Blaze.Framework;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// 预处理精灵设置。
    /// </summary>
    public class SpriteProcessor : AssetPostprocessor
    {
        protected void OnPreprocessTexture()
        {
            var match = mRegex.Match(assetPath);
            if (match.Success)
            {
                processStaticSprite(match);
            }
        }

        private void processStaticSprite(Match match)
        {
            var dir = match.Result("${dir}");
            var file = match.Result("${file}");
            var importer = (TextureImporter) assetImporter;
            importer.textureType = TextureImporterType.Sprite;
            importer.spriteImportMode = SpriteImportMode.Single;
            if (mNoTagCategories.Contains(dir))
            {
                importer.spritePackingTag = string.Empty;
            }
            else{
                importer.spritePackingTag = dir;
            }

            importer.spritePixelsPerUnit = 100;
            importer.mipmapEnabled = false;
            importer.filterMode = FilterMode.Bilinear;

#if UNITY_5_5_OR_NEWER
            importer.crunchedCompression = true;
            importer.allowAlphaSplitting = false;
#endif

            var prefabDir = mOutputDirectory + "/" + dir;
            if (!Directory.Exists(prefabDir))
            {
                Directory.CreateDirectory(prefabDir);
                AssetDatabase.Refresh();
            }
            EditorApplication.delayCall += () =>
            {
                var prefabPath = string.Format("Hospital/Resources/Sprites/{0}/{1}", dir, Path.GetFileNameWithoutExtension(file));
                var obj = new GameObject(Path.GetFileNameWithoutExtension(file));
                var spriteRenderer = obj.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = (Sprite)AssetDatabase.LoadAssetAtPath(string.Format("Assets/Hospital/Sprites/{0}/{1}", dir, file), typeof(Sprite));
                PrefabUtility.CreatePrefab("Assets/" + prefabPath + ".prefab", obj);
                Object.DestroyImmediate(obj);
            };
        }

        private static readonly Regex mRegex = new Regex("Assets/Hospital/Sprites/(?<dir>.+?)/(?<file>.+)");
        private static readonly string mOutputDirectory = UnityPath.GetProjectPath("Assets/Hospital/Resources/Sprites");
        private static readonly string[] mNoTagCategories = {"Background"};

    }
}