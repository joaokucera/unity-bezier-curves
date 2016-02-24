﻿using UnityEngine;

namespace DoisMundos.BezierCurves
{
	public static class BezierUtility 
	{
		/// <summary>
		/// Quadratic Beziér curve.
		/// </summary>
		/// <remarks>
		/// B(t) = (1 - t)² * P0 + 2 * (1 - t) * t * P1 + t² * P2.
		/// </remarks>
		public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t) {
			t = Mathf.Clamp01 (t);
			float oneMinusT = 1f - t;

			return 
					oneMinusT * oneMinusT * p0 + 
					2f * oneMinusT * t * p1 + 
					t * t * p2;
		}

		/// <summary>
		/// Quadratic Beziér curve.
		/// </summary>
		/// <remarks>
		/// B(t) = (1 - t)³ * P0 + 3 * (1 - t)² * t * P1 + 3 * (1 - t) * t² * P2 + t³ * P3
		/// </remarks>
		public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t) {
			t = Mathf.Clamp01 (t);
			float oneMinusT = 1f - t;
			
			return 
					oneMinusT * oneMinusT * oneMinusT * p0 + 
					3f * oneMinusT * oneMinusT * t * p1 + 
					3f * oneMinusT * t * t * p2 + 
					t * t * t * p3;
		}

		/// <summary>
		/// Polynomial. The first derivative.
		/// </summary>
		/// <remarks>
		/// B'(t) = 2 * (1 - t) * (P1 - P0) + 2 * t * (P2 - P1).
		/// </remarks>
		public static Vector3 GetFirstDirective(Vector3 p0, Vector3 p1, Vector3 p2, float t) {
			return 
					2f * (1f - t) * (p1 - p0) +
					2f * t * (p2 - p1); 
		}

		/// <summary>
		/// Polynomial. The first derivative.
		/// </summary>
		/// <remarks>
		/// B'(t) = 3 * (1 - t)² * (P1 - P0) + 6 * (1 - t) * t * (P2 - P1) + 3 * t² * (P3 - P2)
		/// </remarks>
		public static Vector3 GetFirstDirective(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t) {
			t = Mathf.Clamp01 (t);
			float oneMinusT = 1f - t;

			return 
					3f * oneMinusT * oneMinusT * (p1 - p0) + 
					6f * oneMinusT * t * (p2 - p1) + 
					3f * t * t * (p3 - p2);
		}
	}
}