using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(FindInstanceTest))]
public class FindInstanceTestEditor : Editor {

	public override void OnInspectorGUI ()
	{
		if (GUILayout.Button("Do the Test"))
		{
			SceneTour.TakeTour(new string[]{
				"Assets/Prototype/james/MultiSceneTest/SceneA.unity",
				"Oh Nohes!"},
				() => { new GameObject(); },
				false);
		}
	}
}
