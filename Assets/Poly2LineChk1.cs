using UnityEngine;
using System.Collections;

public class Poly2LineChk1 : MonoBehaviour
{
	public	Vector3[] poly;
	public	Vector3 v, p1;
	public	bool hitCheck;

	bool insideChk, normalChk, planeChk, penetrationLineChk;
		
	Vector3 vectorLine, p0, pp, n, nn, np, g;

	float lineV, lineP1;
	Vector3	d1, d2, crossPoint;

	float a;

	Vector3 poly1, poly2, poly3;
	bool p1Check, p2Check, p3Check;
	Vector3 poly4, poly5, poly6;
	bool p4Check, p5Check, p6Check;

	void Update ()
	{
		Debug.DrawLine (poly [0], poly [1], Color.green);
		Debug.DrawLine (poly [1], poly [2], Color.green);
		Debug.DrawLine (poly [2], poly [0], Color.green);
		Debug.DrawLine (v, p1, Color.yellow);

		vectorLine = v - p1;	//線分のベクトル
		p0 = poly [1];			//基準とする平面上の点
		pp = p1 - p0;			//平面内検出用

		n = Vector3.Cross (poly [0] - poly [1], poly [2] - poly [1]);	//ポリゴンの法線
		g = (poly [0] + poly [1] + poly [2]) / 3;						//重心計算
		Debug.DrawLine (g, n + g, Color.grey);							//法線を重心位置に表示
		
		nn = n.normalized;
		np = Vector3.Dot (nn, p0) * nn;
		Debug.DrawLine (Vector3.zero, np, Color.cyan);					//p0基準の平面の高さ位置を表示

		//外分比計算用
		d1 = Vector3.Dot (vectorLine, nn) * nn;	//線分の法線への投影ベクトル（線分全体）
		d2 = Vector3.Dot (pp, nn) * nn;			//線分と平面までの法線への投影ベクトル（平面上の点から線分片方まで）
		a = d2.magnitude / d1.magnitude;							//外分比（投影を利用して内分比を割り出す）
		crossPoint = (1 - a) * (p1 - p0) + a * (v - p0);
		Debug.DrawLine (p0, p0 + crossPoint, Color.red);			//平面と線分の交点表示

		normalChk = 0 != Vector3.Dot (np, vectorLine);        //無限平面に対する線分の衝突判定
		planeChk = 0 == Vector3.Dot (np, pp);   			 //無限平面上に線分が存在する時の衝突判定

		lineV = Vector3.Dot (n, v - np);
		lineP1 = Vector3.Dot (n, p1 - np);
		penetrationLineChk = 0 >= (lineV * lineP1);    //線分が平面を貫通しているかチェック

		//インサイドチェック
		if (planeChk) {
			//平面内に線分がある時は線分の始点と終点を基準にインサイドチェック
			poly1 = Vector3.Cross (poly [0] - poly [1], v - poly [0]);
			poly2 = Vector3.Cross (poly [1] - poly [2], v - poly [1]);
			poly3 = Vector3.Cross (poly [2] - poly [0], v - poly [2]);
			poly4 = Vector3.Cross (poly [0] - poly [1], p1 - poly [0]);
			poly5 = Vector3.Cross (poly [1] - poly [2], p1 - poly [1]);
			poly6 = Vector3.Cross (poly [2] - poly [0], p1 - poly [2]);
			p1Check = poly1.x * n.x >= 0 && poly1.y * n.y >= 0 && poly1.z * n.z >= 0;
			p2Check = poly2.x * n.x >= 0 && poly2.y * n.y >= 0 && poly2.z * n.z >= 0;
			p3Check = poly3.x * n.x >= 0 && poly3.y * n.y >= 0 && poly3.z * n.z >= 0;
			p4Check = poly4.x * n.x >= 0 && poly4.y * n.y >= 0 && poly4.z * n.z >= 0;
			p5Check = poly5.x * n.x >= 0 && poly5.y * n.y >= 0 && poly5.z * n.z >= 0;
			p6Check = poly6.x * n.x >= 0 && poly6.y * n.y >= 0 && poly6.z * n.z >= 0;
			insideChk = p1Check && p2Check && p3Check && p4Check && p5Check && p6Check;
		} else {
			//平面内以外の時は交点を基準にインサイドチェック
			poly1 = Vector3.Cross (poly [0] - poly [1], crossPoint + p0 - poly [0]);
			poly2 = Vector3.Cross (poly [1] - poly [2], crossPoint + p0 - poly [1]);
			poly3 = Vector3.Cross (poly [2] - poly [0], crossPoint + p0 - poly [2]);
			p1Check = poly1.x * n.x >= 0 && poly1.y * n.y >= 0 && poly1.z * n.z >= 0;
			p2Check = poly2.x * n.x >= 0 && poly2.y * n.y >= 0 && poly2.z * n.z >= 0;
			p3Check = poly3.x * n.x >= 0 && poly3.y * n.y >= 0 && poly3.z * n.z >= 0;
			insideChk = p1Check && p2Check && p3Check;
		}
		//フルチェック
		hitCheck = normalChk | planeChk && insideChk && penetrationLineChk;
	}
}
