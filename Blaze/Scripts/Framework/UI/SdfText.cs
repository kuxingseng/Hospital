namespace Blaze.Framework.UI
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Signed distance field text.
    /// </summary>
    [AddComponentMenu("Blaze/UI/SdfText", 11)]
    public class SdfText : Text
    {
        /// <summary>
        /// <para>
        /// Callback function when a UI element needs to generate vertices.
        /// </para>
        /// </summary>
        /// <param name="m">Mesh to populate with UI data.</param>
        protected override void OnPopulateMesh(Mesh toFill)
        {
            base.OnPopulateMesh(toFill);
        }
//protected override void OnFillVBO(List<UIVertex> vbo)
//        {
//            base.OnFillVBO(vbo);
//            Debug.Log("fill vbo");
//            foreach (var v in vbo)
//            {
//                var t = string.Format("col:{0} pos:{1} uv0:{2} uv1:{3}", v.color, v.position, v.uv0, v.uv1);
//                Debug.Log(t);
//            }
//        }
//        
    }
}