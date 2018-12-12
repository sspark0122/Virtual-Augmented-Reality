﻿using UnityEngine;
using UnityEditor;
using System.Collections;

namespace CornellTech.Utility
{
	[CustomEditor (typeof(ChildSwitcherUtility))]
	class ChildSwitcherUtilityEditor : Editor
	{

		static readonly float disableColorValue = .85f;

		Color disabledColor = new Color (disableColorValue, disableColorValue, disableColorValue);
		Transform[] children;

		protected void OnEnable ()
		{
			Transform myTransform = ((ChildSwitcherUtility)target).transform;
			children = new Transform[myTransform.childCount];
			for (int i = 0; i < children.Length; ++i)
			{
				children [i] = myTransform.GetChild (i);
			}
		}

		public override void OnInspectorGUI ()
		{
			GUILayout.Space (10);

			for (int i = 0; i < children.Length; ++i)
			{
				if (!children [i].gameObject.activeSelf)
				{
					GUI.backgroundColor = disabledColor;
					GUI.color = disabledColor;
				}
				if (GUILayout.Button (children [i].gameObject.name))
				{
					SetActive (i);
				}
				GUI.backgroundColor = Color.white;
				GUI.color = Color.white;
			}
			GUILayout.Space (20);

			if (GUILayout.Button ("None"))
			{
				SetNoneActive ();
			}
			GUILayout.Space (10);
			if (GUILayout.Button ("All"))
			{
				SetAllActive ();
			}
		}

		protected void SetActive (int index)
		{
			for (int i = 0; i < children.Length; ++i)
			{
				Undo.RecordObject (children [i].gameObject as Object, "Undo Set Active " + children [i].gameObject.name);
				children [i].gameObject.SetActive (index == i);
			}
		}
		protected void SetNoneActive ()
		{
			for (int i = 0; i < children.Length; ++i)
			{
				Undo.RecordObject (children [i].gameObject as Object, "Undo Set Active " + children [i].gameObject.name);
				children [i].gameObject.SetActive (false);
			}
		}
		protected void SetAllActive ()
		{
			for (int i = 0; i < children.Length; ++i)
			{
				Undo.RecordObject (children [i].gameObject as Object, "Undo Set Active " + children [i].gameObject.name);
				children [i].gameObject.SetActive (true);
			}
		}
	}
}