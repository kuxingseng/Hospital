namespace Blaze.Editor
{
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// <see cref="Transform"/>辅助工具类。
    /// </summary>
    public static class TransformTool
    {
        /// <summary>
        /// 显示<see cref="Transform"/>的完整路径。
        /// </summary>
        [MenuItem("CONTEXT/Transform/Echo full path")]
        public static void ShowTransformPath(MenuCommand cmd)
        {
            var transform = (Transform) cmd.context;
            Debug.Log(getTransformPath(transform));
        }

        private static string getTransformPath(Transform transform)
        {
            if (transform == null)
                return null;
            var basePath = getTransformPath(transform.parent);
            return basePath == null ? transform.name : string.Format("{0}/{1}", basePath, transform.name);
        }
    }
}