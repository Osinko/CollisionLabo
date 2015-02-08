using UnityEngine;
using System.Collections;

public class DevideExternalTest1 : MonoBehaviour
{
		public enum Mode
		{
				divide,
				External,
		};
		
		public float m, n;
		public Vector3 A, B;

		public Mode mode;

		Transform goA, goB, goP;
		Vector3 P;

		void Start ()
		{
				goA = GameObject.CreatePrimitive (PrimitiveType.Cube).transform;
				goB = GameObject.CreatePrimitive (PrimitiveType.Sphere).transform;
				goP = GameObject.CreatePrimitive (PrimitiveType.Cylinder).transform;

				goA.gameObject.renderer.material.color = Color.white;
				goB.gameObject.renderer.material.color = Color.white;
				goP.gameObject.renderer.material.color = Color.yellow;
		}
	
		void Update ()
		{
				switch (mode) {
				case Mode.divide:
						P = (n * A + m * B) / (m + n);					//内分比
						break;
				case Mode.External:
						if (m != n) {
								P = (-n * A + m * B) / (m - n);			//外分比
						}
						break;
				}

				goA.position = A;
				goB.position = B;
				goP.position = P;

				if (m > n) {
						Debug.DrawLine (A, P, Color.blue);
						Debug.DrawLine (B, P, Color.green);
				} else {
						Debug.DrawLine (B, P, Color.green);
						Debug.DrawLine (A, P, Color.blue);
				}
		}
}
