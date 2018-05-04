﻿using System;
using UnityEngine;
using UnityEditor;


namespace ShibaInu
{
	[CustomEditor (typeof(CameraTool))]
	public class CameraToolEditor : BaseEditor
	{

		protected SerializedProperty m_alignSceneView;
		protected SerializedProperty m_copyFormat;

		protected GUIContent m_c_copyToClipboard = new GUIContent ("Copy To Clipboard", "复制摄像机的位置和旋转信息到系统剪切板");
		protected GUIContent m_c_pasteFromTheClipboard = new GUIContent ("Paste From The Clipboard", "将摄像机的位置和旋转设置成系统剪切板中的值");

		protected CameraTool m_cameraTool;



		protected virtual void OnEnable ()
		{
			m_cameraTool = (CameraTool)target;

			m_alignSceneView = serializedObject.FindProperty ("m_alignSceneView");
			m_copyFormat = serializedObject.FindProperty ("m_copyFormat");
		}



		public override void OnInspectorGUI ()
		{
			base.OnInspectorGUI ();


			EditorGUILayout.PropertyField (m_alignSceneView);
			EditorGUILayout.PropertyField (m_copyFormat);


			if (GUILayout.Button (m_c_copyToClipboard)) {
				Transform transform = m_cameraTool.transform;
				Vector3 pos = transform.localPosition;
				Vector3 rot = transform.localEulerAngles;

				string format = "{0:N3}";
				string str = m_cameraTool.copyFormat;
				str = str.Replace ("p.x", String.Format (format, pos.x));
				str = str.Replace ("p.y", String.Format (format, pos.y));
				str = str.Replace ("p.z", String.Format (format, pos.z));
				str = str.Replace ("r.x", String.Format (format, rot.x));
				str = str.Replace ("r.y", String.Format (format, rot.y));
				str = str.Replace ("r.z", String.Format (format, rot.z));
				GUIUtility.systemCopyBuffer = str;
			}


			if (GUILayout.Button (m_c_pasteFromTheClipboard)) {
				m_cameraTool.alignSceneView = false;

				string str = GUIUtility.systemCopyBuffer;
				str = str.Replace (" ", "");
				str = str.Replace ("Vector3", "");
				str = str.Replace (".New", "");
				str = str.Replace ("new", "");
				str = str.Replace ("pos", "");
				str = str.Replace ("rot", "");
				str = str.Replace ("=", "");
				str = str.Replace ("(", "");
				str = str.Replace (")", "");
				str = str.Replace ("{", "");
				str = str.Replace ("}", "");
				str = str.Replace ("[", "");
				str = str.Replace ("]", "");
				str = str.Replace (";", "");

				string[] arr = str.Split (',');
				Vector3 pos = new Vector3 (float.Parse (arr [0]), float.Parse (arr [1]), float.Parse (arr [2]));
				Vector3 rot = new Vector3 (float.Parse (arr [3]), float.Parse (arr [4]), float.Parse (arr [5]));

				Transform transform = m_cameraTool.transform;
				transform.localPosition = pos;
				transform.localEulerAngles = rot;

				SceneView sv = SceneView.lastActiveSceneView;
				if (sv != null)
					sv.AlignViewToObject (transform);
			}


			serializedObject.ApplyModifiedProperties ();
		}


		//
	}
}
