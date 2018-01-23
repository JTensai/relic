using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(NestedPrefabPacker))]
public class NestedPrefabPackerEditor : Editor {

	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI();

		if (GUILayout.Button("Pack Prefab"))
		{
			((NestedPrefabPacker)target).Pack();
		}
	}
}
