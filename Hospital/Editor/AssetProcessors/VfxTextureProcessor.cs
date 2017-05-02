namespace Muhe.Mjhx.Editor.AssetProcessors
{
    using System.Text.RegularExpressions;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// Ԥ������Ч��ͼ���á�
    /// </summary>
    public class VfxTextureProcessor : AssetPostprocessor
    {
        protected void OnPreprocessTexture()
        {
            return; //�������⴦��
            var importer = (TextureImporter)assetImporter;
            if (!mRegex.IsMatch(importer.assetPath))
                return;
            //importer.textureType = TextureImporterType.Default;
            importer.npotScale = TextureImporterNPOTScale.ToNearest;
            importer.generateCubemap = TextureImporterGenerateCubemap.None;
            importer.isReadable = false;
            importer.spriteImportMode = SpriteImportMode.None;
            importer.mipmapEnabled = false;
            importer.generateMipsInLinearSpace = false;
            importer.wrapMode = TextureWrapMode.Clamp;
            importer.anisoLevel = 0;
            importer.textureFormat = TextureImporterFormat.AutomaticCompressed;
        }

        private static readonly Regex mRegex = new Regex("Assets/Mjhx/Resources/NeoVFX/Textures/(.+)");
    }
}