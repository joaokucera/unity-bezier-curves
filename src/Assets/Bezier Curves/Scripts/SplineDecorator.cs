﻿using UnityEngine;

namespace DoisMundos.BezierCurves
{
	public class SplineDecorator : MonoBehaviour 
	{
		public BezierSpline spline;
		public int frequency;
		public bool lookFoward;
		public Transform[] items;

		private void Awake() {
			DoUpdate ();
		}

		public void DoUpdate() {
			if (frequency <= 0 || items == null || items.Length == 0 || !spline.IsThereAPath) {
				return;
			}
			
			float stepSize = frequency * items.Length;
			if (spline.Loop || stepSize == 1f) {
				stepSize = 1f / stepSize;
			}
			else {
				stepSize = 1f / (stepSize - 1);
			}
			
			for (int p = 0, f = 0; f < frequency; f++) {
				for (int i = 0; i < items.Length; i++, p++) {
					Transform item = Instantiate(items[i]) as Transform;
					Vector3 position = spline.GetPoint(p * stepSize);
					item.transform.localPosition = position;
					if (lookFoward) {
						item.transform.LookAt(position + spline.GetDirection(p * stepSize));
					}
					item.transform.SetParent(transform);
				}
			}
		}
	}
}