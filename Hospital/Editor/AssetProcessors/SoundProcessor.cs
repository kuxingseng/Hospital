namespace Muhe.Mjhx.Editor.AssetProcessors
{
    using System.Text.RegularExpressions;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// 预处理声音设置。
    /// </summary>
    public class SoundProcessor : AssetPostprocessor
    {
        protected void OnPostprocessAudio(AudioClip clip)
        {
            var match = mRegex.Match(assetPath);
            if (!match.Success)
                return;
            var importer = (AudioImporter) assetImporter;
            var settings=new AudioImporterSampleSettings();
            settings.loadType = AudioClipLoadType.CompressedInMemory;
            settings.compressionFormat = AudioCompressionFormat.Vorbis;
            importer.SetOverrideSampleSettings("Android", settings);

            var defaultSetting=new AudioImporterSampleSettings();
            defaultSetting.loadType = AudioClipLoadType.CompressedInMemory;
            defaultSetting.compressionFormat = AudioCompressionFormat.Vorbis;
            importer.defaultSampleSettings = defaultSetting;
        }

        private static readonly Regex mRegex = new Regex("Assets/Mjhx/Resources/Sounds/Sfx(.+)");
    }
}