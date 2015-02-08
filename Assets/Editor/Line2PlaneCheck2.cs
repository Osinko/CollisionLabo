using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(Line2))]
public class Line2PlaneCheck2 : Editor
{
		void OnSceneGUI ()
		{
				Line2 line = target as Line2;

				Handles.color = Color.green;
				Handles.DrawPolyLine (line.poly);
				Handles.DrawLine (line.poly [0], line.poly [2]);

				Handles.color = Color.cyan;
				Vector3 p = line.poly [1];				//基準とする平面上の点
				Vector3 pp = line.v2 - p;				//平面内検出用
				Vector3 n = Vector3.Cross (line.poly [0] - line.poly [1], line.poly [2] - line.poly [1]);	//ポリゴンの法線
				Vector3 np = Vector3.Dot (p, n.normalized) * n.normalized;	//法線に投影されたp
				Handles.DrawDottedLine (Vector3.zero, p, 6);
				Handles.DrawDottedLine (Vector3.zero, n, 6);
				Handles.DrawDottedLine (line.v2, p, 6);
				Handles.color = Color.blue;
				Handles.DrawAAPolyLine (12, Vector3.zero, np);

				Handles.color = Color.yellow;
				Vector3 v = line.v1 - line.v2;
				Handles.DrawLine (line.v1, line.v2);

				//内分計算用
				Vector3 d1 = Vector3.Dot (v, n.normalized) * n.normalized;	//線分の法線への投影ベクトル（線分全体）
				Vector3 d2 = Vector3.Dot (pp, n.normalized) * n.normalized;	//線分と平面までの法線への投影ベクトル（平面上の点から線分片方まで）
				Handles.color = Color.blue;
				Handles.DrawDottedLine (line.v2, d1 + line.v2, 3);
				Handles.color = Color.red;
				Handles.DrawDottedLine (line.v2, -d2 + line.v2, 3);

				float a = d2.magnitude / d1.magnitude;		//内分比（投影を利用して内分比を割り出す）
//				Vector3 crossPoint = v * a;
//				Handles.DrawLine (line.v2, crossPoint + line.v2);	//平面と線分の交点

				Vector3 v3 = (1 - a) * (line.v2 - p) + a * (line.v1 - p);
				Handles.DrawLine (p, p + v3);	//平面と線分の交点

				Vector3 poly1 = Vector3.Cross (line.poly [0] - line.poly [1], v3 + p - line.poly [0]);
				Vector3 poly2 = Vector3.Cross (line.poly [1] - line.poly [2], v3 + p - line.poly [1]);
				Vector3 poly3 = Vector3.Cross (line.poly [2] - line.poly [0], v3 + p - line.poly [2]);

				bool p1Check = poly1.x * n.x >= 0 && poly1.y * n.y >= 0 && poly1.z * n.z >= 0;
				bool p2Check = poly2.x * n.x >= 0 && poly2.y * n.y >= 0 && poly2.z * n.z >= 0;
				bool p3Check = poly3.x * n.x >= 0 && poly3.y * n.y >= 0 && poly3.z * n.z >= 0;
				
				line.insideChk = p1Check && p2Check && p3Check;
		}
}
