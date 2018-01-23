using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(uteMap))]
public class uteMapEditor : Editor {

	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();

		if (GUILayout.Button(new GUIContent("Edit")))
		{

		}
	}

	public void OnSceneGUI()
	{
		Debug.DrawLine(new Vector3(0.0f, 5.0f, 0.0f), new Vector3(5.0f, 5.0f, 0.0f));
	}
}
