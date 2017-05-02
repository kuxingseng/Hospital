namespace Muhe.Mjhx.Editor.AssetProcessors
{
    using System.Linq;
    using System.Text.RegularExpressions;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// 预处理模型的贴图设置。
    /// </summary>
    public class ModelTextureProcessor : AssetPostprocessor
    {
        protected void OnPreprocessTexture()
        {
            var importer = (TextureImporter) assetImporter;
            var match = mRegex.Match(importer.assetPath);
            if (!match.Success)
                return;

            var dir = match.Result("${dir}");
            if (!dir.StartsWith("Character"))
                return;

            var isRepeat = false;
            var file = match.Result("${file}");
            if (file.Contains("#"))
            {
                var args = file.Split('#');
                if (args.Contains("repeat"))
                    isRepeat = true;
            }

#if UNITY_5_5_OR_NEWER
            importer.textureType = TextureImporterType.Default;
#endif
            importer.npotScale = TextureImporterNPOTScale.ToNearest;
            importer.generateCubemap = TextureImporterGenerateCubemap.None;
            importer.isReadable = false;
            importer.spriteImportMode = SpriteImportMode.None;
            importer.mipmapEnabled = false;
            importer.generateMipsInLinearSpace = false;
            importer.wrapMode = isRepeat ? TextureWrapMode.Repeat : TextureWrapMode.Clamp;
            importer.anisoLevel = 0;
        }

        private static readonly Regex mRegex = new Regex("Assets/MjhxResource/(?<dir>.+?)/(?<file>.+)\\.(?<ext>.+)");
    }
}