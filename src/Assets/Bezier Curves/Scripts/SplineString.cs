using UnityEngine;

namespace DoisMundos.BezierCurves
{
	public class SplineString : MonoBehaviour 
	{
		public BezierSpline spline;

		private LineRenderer lineRenderer;

		public void Awake() {
			lineRenderer = GetComponent<LineRenderer>();
			DoUpdate ();
		}

		public void DoUpdate() {
			if (spline.IsEmpty) {
				return;
			}

			lineRenderer.SetVertexCount (spline.ControlPointCount);
			for (int i = 0; i < spline.ControlPointCount; i++) {
				lineRenderer.SetPosition(i, spline.GetControlPoint(i));
			}
		}
	}
}