using UnityEngine;
using System.Collections.Generic;

namespace DoisMundos.BezierCurves
{
	public class BezierHandler : MonoBehaviour 
	{
		public SplineWalker walker;
		public SplineDecorator decorator;
		public SplineString splineString;

		public BezierSpline spline;
		public LayerMask mask;

		private void Update() {
			if (Input.GetMouseButtonDown (0)) {
				RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast(ray, out hit, float.MaxValue, mask)) {
					spline.AddCurve(hit.point);

					walker.DoUpdate();
					decorator.DoUpdate();
					splineString.DoUpdate();
				}
			}
		}
	}
}