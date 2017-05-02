using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace UnityEngine.UI
{
	[AddComponentMenu("UI/Effects/Letter Spacing", 14)]
	public class LetterSpacing : BaseMeshEffect
	{
        public Image image;

		[SerializeField]
		private float m_spacing = 0f;
		
		protected LetterSpacing() { }
		
		#if UNITY_EDITOR
//		protected override void OnValidate()
//		{
//			spacing = m_spacing;
//			base.OnValidate();
//		}
		#endif
		
		public float spacing
		{
			get { return m_spacing; }
			set
			{
				if (m_spacing == value) return;
				m_spacing = value;
				if (graphic != null) graphic.SetVerticesDirty();
			}
		}
		
		public void ModifyVertices(List<UIVertex> verts)
		{
			if (! IsActive()) return;
			
			Text text = GetComponent<Text>();
			if (text == null)
			{
				Debug.LogWarning("LetterSpacing: Missing Text component");
				return;
			}
			
			string[] lines = text.text.Split('\n');
			Vector3  pos;
			float    letterOffset    = spacing * (float)text.fontSize / 100f;
			float    alignmentFactor = 0;
			int      glyphIdx        = 0;
			
			switch (text.alignment)
			{
			case TextAnchor.LowerLeft:
			case TextAnchor.MiddleLeft:
			case TextAnchor.UpperLeft:
				alignmentFactor = 0f;
				break;
				
			case TextAnchor.LowerCenter:
			case TextAnchor.MiddleCenter:
			case TextAnchor.UpperCenter:
				alignmentFactor = 0.5f;
				break;
				
			case TextAnchor.LowerRight:
			case TextAnchor.MiddleRight:
			case TextAnchor.UpperRight:
				alignmentFactor = 1f;
				break;
			}
			
			for (int lineIdx=0; lineIdx < lines.Length; lineIdx++)
			{
				string line = lines[lineIdx];
				float lineOffset = (line.Length -1) * letterOffset * alignmentFactor;
				
				for (int charIdx = 0; charIdx < line.Length; charIdx++)
				{
					int idx1 = glyphIdx * 4 + 0;
					int idx2 = glyphIdx * 4 + 1;
					int idx3 = glyphIdx * 4 + 2;
					int idx4 = glyphIdx * 4 + 3;
					
					// Check for truncated text (doesn't generate verts for all characters)
					if (idx4 > verts.Count - 1) return;
					
					UIVertex vert1 = verts[idx1];
					UIVertex vert2 = verts[idx2];
					UIVertex vert3 = verts[idx3];
					UIVertex vert4 = verts[idx4];
					
					pos = Vector3.right * (letterOffset * charIdx - lineOffset);
					
					vert1.position += pos;
					vert2.position += pos;
					vert3.position += pos;
					vert4.position += pos;
                    if (image != null && lineIdx == 0 && charIdx==0)
                    {
                        image.rectTransform.anchoredPosition = vert1.position;
                        image.rectTransform.sizeDelta = new Vector2(vert2.position.x - vert1.position.x, vert2.position.y - vert4.position.y);
                    }
					
					verts[idx1] = vert1;
					verts[idx2] = vert2;
					verts[idx3] = vert3;
					verts[idx4] = vert4;
					
					glyphIdx++;
				}
				
				// Offset for carriage return character that still generates verts
				glyphIdx++;
			}
		}

	    /// <summary>
	    /// <para>
	    /// See:[[IMeshModifier]].
	    /// </para>
	    /// </summary>
	    /// <param name="mesh"/>
	    public override void ModifyMesh(Mesh mesh)
	    {
	        throw new System.NotImplementedException();
	    }

        public override void ModifyMesh(VertexHelper vh)
        {
            throw new NotImplementedException();
        }
    }
}
