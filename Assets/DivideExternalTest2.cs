using UnityEngine;
using System.Collections;

public class DivideExternalTest2 : MonoBehaviour
{
		public Vector3 A, B;
		public float m, n;
		Vector3 P, mx, my, nx, ny;

		void Update ()
		{
				//内分点
				P = (n * A + m * B) / (m + n);

				//投影
				mx = Vector3.Dot (P - A, Vector3.right) * Vector3.right;
				my = Vector3.Dot (P - A, Vector3.up) * Vector3.up;
				nx = Vector3.Dot (B - P, Vector3.right) * Vector3.right;
				ny = Vector3.Dot (B - P, Vector3.up) * Vector3.up;

				Debug.DrawLine (A, P, Color.green);
				Debug.DrawLine (P, B, Color.red);
				Debug.DrawLine (A, A + mx, Color.green);
				Debug.DrawLine (A, A + my, Color.green);
				Debug.DrawLine (A + mx, A + mx + nx, Color.red);
				Debug.DrawLine (A + my, A + my + ny, Color.red);
		}
}
