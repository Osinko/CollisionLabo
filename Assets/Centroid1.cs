using UnityEngine;
using System.Collections;

public class Centroid1 : MonoBehaviour
{
		public Vector3[] poly;
		public Vector3 g, g2;
		Vector3 v, v2;

		void Start ()
		{
				poly = new Vector3[] {new Vector3 (1, 0.5f, 0),new Vector3 (0, 0.5f, 1),new Vector3 (-1, 0.5f, 0)};
		}

		void Update ()
		{
				//重心を求める（通常こちらを利用する）
				g = (poly [0] + poly [1] + poly [2]) / 3.0f;

				//対角線2:1の性質を利用して外分比を利用して求める
				v = (poly [1] - poly [0]) * 0.5f;
				v2 = (poly [2] - poly [0] - v) / 3f;
				g2 = poly [0] + v + v2;
		}
}