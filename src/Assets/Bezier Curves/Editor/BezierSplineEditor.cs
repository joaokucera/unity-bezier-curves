﻿using UnityEngine;
using UnityEditor;

namespace DoisMundos.BezierCurves
{
	[CustomEditor(typeof(BezierSpline))]
	public class BezierSplineEditor : Editor 
	{
		private static Color[] modeColors = {
			Color.white,
			Color.yellow,
			Color.cyan
		};

		private const int lineSteps = 10;
		private const int stepsPerCurve = 10;
		private const float directionScale = .5f;
		private const float handleSize = .04f;
		private const float pickSize = .06f;

		private BezierSpline spline;
		private Transform handleTransform;
		private Quaternion handleRotation;
		private int selectedIndex = -1;
		private SplineString splineString;

		public void OnEnable() {
			splineString = FindObjectOfType<SplineString> ();
		}

		public override void OnInspectorGUI () {
//			DrawDefaultInspector ();
			spline = target as BezierSpline;

//			EditorGUI.BeginChangeCheck ();
//			bool loop = EditorGUILayout.Toggle ("Loop", spline.Loop);
//			if (EditorGUI.EndChangeCheck ()) {
//				Undo.RecordObject(spline, "Toggle Loop");
//				EditorUtility.SetDirty(spline);
//				spline.Loop = loop;
//			}

			if (selectedIndex >= 0 && selectedIndex < spline.ControlPointCount && !spline.IsEmpty) {
				DrawSelectedPointInspector();
			}
//			if (GUILayout.Button ("Add Curve")) {
//				Undo.RecordObject(spline, "Add Curve");
//				spline.AddCurve();
//				EditorUtility.SetDirty(spline);
//			}
		}

		private void OnSceneGUI() {
			spline = target as BezierSpline;
			handleTransform = spline.transform;
			handleRotation = Tools.pivotRotation == PivotRotation.Local ? handleTransform.rotation : Quaternion.identity;

			if (!spline.IsThereAPath) {
				return;
			}

			Vector3 p0 = ShowPoint (0);
			for (int i = 1; i < spline.ControlPointCount; i += 3) {
				Vector3 p1 = ShowPoint (i);
				Vector3 p2 = ShowPoint (i + 1);
				Vector3 p3 = ShowPoint (i + 2);

				Handles.color = Color.gray;
				Handles.DrawLine (p0, p1);
				Handles.DrawLine (p2, p3);

				Handles.DrawBezier (p0, p3, p1, p2, Color.white, null, 2f);
				p0 = p3;
			}
		
			ShowDirections ();
		}

		private void ShowDirections() {
			Handles.color = Color.green;
			Vector3 point = spline.GetPoint (0f);
			Handles.DrawLine (point, point + spline.GetDirection (0f) * directionScale);

			int steps = stepsPerCurve * spline.CurveCount;
			for (int i = 1; i <= steps; i++) {
				point = spline.GetPoint(i / (float)steps);
				Handles.DrawLine(point, point + spline.GetDirection(i / (float)steps) * directionScale);
			}
		}
		
		private Vector3 ShowPoint(int index) {
			Vector3 point = handleTransform.TransformPoint (spline.GetControlPoint(index));
			float size = HandleUtility.GetHandleSize (point);
//			if (index == 0) {
//				size *= 2f;
//			}
			Handles.color = modeColors[(int)spline.GetControlPointMode(index)];

			if (Handles.Button (point, handleRotation, size * handleSize, size * pickSize, Handles.DotCap)) {
				selectedIndex = index;
				Repaint();
			}
			if (selectedIndex == index) {
				EditorGUI.BeginChangeCheck ();
				point = Handles.DoPositionHandle (point, handleRotation);
				if (EditorGUI.EndChangeCheck ()) {
					Undo.RecordObject(spline, "Move Point");
					EditorUtility.SetDirty(spline);

					//TODO: Do what you need before change any point.
					spline.DoBeforeSetControlPoint();
					//Set the control point to a new position.
					spline.SetControlPoint(index, handleTransform.InverseTransformPoint(point));
					//TODO: Do what you need after change any point.
					spline.DoAfterSetControlPoint();

					//Updating spline string.
					if (splineString != null ) { splineString.DoUpdate(); }
				}
			}
			
			return point;
		}
		
		private void DrawSelectedPointInspector() {
			GUILayout.Label ("Selected Point");

			EditorGUI.BeginChangeCheck ();
			Vector3 point = EditorGUILayout.Vector3Field ("Position", spline.GetControlPoint (selectedIndex));
			if (EditorGUI.EndChangeCheck ()) {
				Undo.RecordObject(spline, "Move Point");
				EditorUtility.SetDirty(spline);
				spline.SetControlPoint(selectedIndex, point);
			}

			EditorGUI.BeginChangeCheck ();
			BezierControlPointMode mode = (BezierControlPointMode)EditorGUILayout.EnumPopup ("Mode", spline.GetControlPointMode (selectedIndex));
			if (EditorGUI.EndChangeCheck ()) {
				Undo.RecordObject(spline, "Change Point Mode");
				EditorUtility.SetDirty(spline);
				spline.SetControlPointMode(selectedIndex, mode);
			}
		}
	}
}