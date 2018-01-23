using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(GetInstanceTest))]
public class GetInstanceTestEditor : Editor {

	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();
		Debug.Log(target.GetInstanceID());
	}
}
