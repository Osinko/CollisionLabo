using UnityEngine;
using System.Collections;

public class DivideExternalTest4 : MonoBehaviour
{
		public Vector3 n, p0;	//平面（n:法線 p0:平面上の点）
		public Vector3 v1, v2;	//線分ベクトル
		float a;				//内分比
		Vector3 p0cp;			//今回求めたい値
		Vector3 nn, v1p0, v2p0;
		Vector3 v2v1;
		Vector3 totalSpeed, partsSpeed;

		//表示用
		Vector3  planeVectorY, planeVectorX;
		Vector3 shiftDisp = new Vector3 (-0.5f, 0, 0);
		Transform planeObj;

		void Start ()
		{
				planeObj = GameObject.CreatePrimitive (PrimitiveType.Plane).transform;
		}

		void Update ()
		{
				//無限平面の生成
				nn = n.normalized;
				planeVectorY = Vector3.Dot (p0, nn) * nn;
				planeVectorX = p0 - planeVectorY;
				planeObj.position = planeVectorY;
				planeObj.rotation = Quaternion.LookRotation (planeVectorX, planeVectorY);

				//線分ベクトル
				v1p0 = v1 - p0;
				v2p0 = v2 - p0;
				v2v1 = v2 - v1;

				//投影したベクトルの速度を求める
				totalSpeed = Vector3.Dot (v2v1, nn) * nn;	//全体の速度
				partsSpeed = Vector3.Dot (v2p0, nn) * nn;	//部分の速度	

				a = partsSpeed.magnitude / totalSpeed.magnitude;	//全体を１とした片側の内分比率を求める
				p0cp = v2p0 * (1 - a) + v1p0 * a;					//全体を１とした内分比の変形式を利用してベクトルを求める

				Debug.DrawLine (Vector3.zero, planeVectorY, Color.green);
				Debug.DrawLine (planeVectorY, planeVectorY + planeVectorX, Color.green);
				Debug.DrawLine (v1, v2, Color.yellow);
				Debug.DrawLine (p0, p0 + v1p0, Color.cyan);
				Debug.DrawLine (p0, p0 + v2p0, Color.cyan);

				Debug.DrawLine (shiftDisp + Vector3.zero, shiftDisp + totalSpeed, Color.yellow);
				Debug.DrawLine (shiftDisp * 2 + Vector3.zero, shiftDisp * 2 + partsSpeed, Color.cyan);
				Debug.DrawLine (p0, p0 + p0cp, Color.red);	//今回、欲しかった値
		}
}
