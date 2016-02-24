﻿using UnityEngine;

namespace DoisMundos.BezierCurves
{
	public class BezierCurve : MonoBehaviour 
	{
		public Vector3[] points;

		public void Reset() {
			points = new Vector3[] {
				new Vector3 (1f, 0f, 0f),
				new Vector3 (2f, 0f, 0f),
				new Vector3 (3f, 0f, 0f),
				new Vector3 (4f, 0f, 0f)
			};
		}

		public Vector3 GetPoint(float t) {
			return transform.TransformPoint(BezierUtility.GetPoint(points[0], points[1], points[2], points[3], t));
		}

		public Vector3 GetDirection (float t) {
			return GetVelocity(t).normalized;
		}

		private Vector3 GetVelocity(float t) {
			return transform.TransformPoint (BezierUtility.GetFirstDirective (points [0], points [1], points [2], points[3], t)) - 
				   transform.position;
		}
	}
}