namespace Muhe.Mjhx.Editor.AssetProcessors
{
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// 预处理动态图片资源设置。
    /// </summary>
    public class ArtifactProcessor : AssetPostprocessor
    {
        protected void OnPreprocessTexture()
        {
            if (!mRegex.IsMatch(assetPath))
                return;

            var match = mRegex.Match(assetPath);
            var category = match.Result("${category}");
            var id = match.Result("${id}");
            var file = match.Result("${file}");
            if (category != "Artifact" && category != "Emoticon" && category != "Icon" && category != "Map")
                return;

            var importer = (TextureImporter)assetImporter;
            importer.textureType = TextureImporterType.Sprite;
            importer.spriteImportMode = SpriteImportMode.Single;

            if (id != "Chess" && id != "SequenceFrame")
                importer.spritePackingTag = category + id;
            else
                importer.spritePackingTag = string.Empty;

            importer.maxTextureSize = 1024;
            importer.textureFormat = TextureImporterFormat.Automatic16bit;
            if (category == "Map")
            {
                importer.spritePackingTag = string.Empty;
                if (file.Contains("World") || file.Contains("WStage"))
                {
                    importer.textureFormat = TextureImporterFormat.AutomaticTruecolor;
                }   
            }
            if (mExcludeNames.Contains(file))
                importer.textureFormat = TextureImporterFormat.AutomaticTruecolor;
            

            importer.spritePixelsPerUnit = 100;
            importer.mipmapEnabled = false;
            importer.filterMode = FilterMode.Bilinear;

#if UNITY_5_5_OR_NEWER
            importer.crunchedCompression = true;
            importer.allowAlphaSplitting = false;
#endif
            
            EditorApplication.delayCall += () =>
            {
                var pathName= Path.GetDirectoryName(assetPath);
                var prefabName = Path.GetFileNameWithoutExtension(file);
                //var prefabPath = string.Format("Assets/MjhxResource/{0}/{1}/{2}.prefab", category, id, prefabName);
                var prefabPath = string.Format("{0}/{1}.prefab", pathName, prefabName);
                var obj = new GameObject(prefabName);
                var spriteRenderer = obj.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = (Sprite)AssetDatabase.LoadAssetAtPath(assetPath, typeof(Sprite));
                PrefabUtility.CreatePrefab(prefabPath, obj);
                Object.DestroyImmediate(obj);
            };
        }

        private static readonly Regex mRegex = new Regex("Assets/MjhxResource/(?<category>.+?)/(?<id>.+?)/(?<file>.+)");

        private static readonly string[] mExcludeNames = new[] { "banner_0.png", "banner_1.png", "banner_2.png", "banner_22.png", 
            "banner_222.png", "banner_kfjj.png", "banner_qmfl.png" };
    }
}