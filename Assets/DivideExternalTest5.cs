using UnityEngine;
using System.Collections;

public class DivideExternalTest5 : MonoBehaviour
{
		public Vector3 norm, p0;	//平面（norm:法線 p0:平面上の点）
		public Vector3 v1, v2;		//線分ベクトル
		float m, n;					//内分比
		Vector3 p0cp;				//今回求めたい値
		Vector3 nn, v1p0, v2p0;
		Vector3 v2v1;
		Vector3 mSpeed, nSpeed;
	
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
				nn = norm.normalized;
				planeVectorY = Vector3.Dot (p0, nn) * nn;
				planeVectorX = p0 - planeVectorY;
				planeObj.position = planeVectorY;
				planeObj.rotation = Quaternion.LookRotation (planeVectorX, planeVectorY);
		
				//線分ベクトル
				v1p0 = v1 - p0;
				v2p0 = v2 - p0;
				v2v1 = v2 - v1;
		
				//投影したベクトルの速度を求める
				mSpeed = Vector3.Dot (v1p0, nn) * nn;	//全体の速度
				nSpeed = Vector3.Dot (v2p0, nn) * nn;	//部分の速度

				m = mSpeed.magnitude;
				n = nSpeed.magnitude;
		
				p0cp = (n * v1p0 + m * v2p0) / (m + n);			//内分比を利用してベクトルを求める
		
				Debug.DrawLine (Vector3.zero, planeVectorY, Color.gray);
				Debug.DrawLine (planeVectorY, planeVectorY + planeVectorX, Color.gray);
				Debug.DrawLine (v1, v2, Color.yellow);
				Debug.DrawLine (p0, p0 + v1p0, Color.green);
				Debug.DrawLine (p0, p0 + v2p0, Color.magenta);
		
				Debug.DrawLine (shiftDisp + Vector3.zero, shiftDisp + mSpeed, Color.green);
				Debug.DrawLine (shiftDisp * 2 + Vector3.zero, shiftDisp * 2 + nSpeed, Color.magenta);
				Debug.DrawLine (p0, p0 + p0cp, Color.red);	//今回、欲しかった値
		}
}
