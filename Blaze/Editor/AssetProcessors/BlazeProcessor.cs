namespace Blaze.Editor.AssetProcessors
{
    using Framework.Logging;
    using UnityEditor;

    public class BlazeProcessor : AssetPostprocessor
    {
        protected void OnPreprocessTexture()
        {
            BlazeLog.Info("process " + assetPath);
        }
    }
}