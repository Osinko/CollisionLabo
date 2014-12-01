using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(Line1))]
public class Line2PlaneCheck1 : Editor
{
		void OnSceneGUI ()
		{
				Line1 line = target as Line1;
				Handles.color = Color.green;
				Handles.DrawDottedLine (Vector3.zero, line.n, 6);
				Handles.DrawLine (Vector3.zero, line.p0);
				Vector3 np = line.n.normalized * Vector3.Dot (line.p0, line.n.normalized);
				Handles.DrawWireDisc (np, line.n, (line.p0 - np).magnitude);

				Handles.color = Color.yellow;
				Vector3 vp = line.v - line.p1;
				Handles.DrawLine (line.v, line.p1);

				Handles.color = Color.cyan;
				Vector3 pp = line.p1 - line.p0;
				Handles.DrawDottedLine (line.p1, line.p0, 6);
		
				line.normalCheck = 0 != Vector3.Dot (np, vp);		//無限平面に対する線分の衝突判定
				line.planeInLineCheck = 0 == Vector3.Dot (np, pp);	//無限平面上に線分が存在する時の衝突判定

				line.lineV = Vector3.Dot (np, line.v - np);
				line.lineP1 = Vector3.Dot (np, line.p1 - np);
				line.penetrationLineCheck = 0 >= (line.lineV * line.lineP1);

				line.hitCheck = line.normalCheck | line.planeInLineCheck && line.penetrationLineCheck;
		}
}
