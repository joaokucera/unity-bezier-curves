using UnityEngine;

namespace DoisMundos.BezierCurves
{
	public enum SplineWalkerMode {
		Once,
		Loop,
		PingPong
	}

	public class SplineWalker : MonoBehaviour 
	{
		public BezierSpline spline;
		public float duration;
		public bool lookFoward;
		public SplineWalkerMode mode;

		private float progress;
		private bool goingFoward = true;
		private MeshRenderer meshRenderer;

		public void Awake() {
			meshRenderer = GetComponent<MeshRenderer> ();
		}

		public void Update() {
			if (!spline.IsThereAPath) {
				return;
			}

			if (goingFoward) {
				progress += Time.deltaTime / duration;
				if (progress > 1f) {
					if (mode == SplineWalkerMode.Once) {
						progress = 1f;
					}
					else if (mode == SplineWalkerMode.Loop) {
						progress -= 1f;
					}
					else {
						progress = 2f - progress;
						goingFoward = false;
					}
				}
			}
			else {
				progress -= Time.deltaTime / duration;
				if (progress < 0f) {
					progress = -progress;
					goingFoward = true;
				}
			}

			Vector3 position = spline.GetPoint (progress);
			transform.localPosition = position;
			if (lookFoward) {
				transform.LookAt(position + spline.GetDirection(progress));
			}
		}
		
		public void DoUpdate () {
			meshRenderer.enabled = spline.IsThereAPath;
		}
	}
}