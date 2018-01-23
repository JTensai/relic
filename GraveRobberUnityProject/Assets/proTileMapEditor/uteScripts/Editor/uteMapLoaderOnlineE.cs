using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(uteMapLoader))]
public class uteMapLoaderOnlineE : Editor {

	public int selectedItemIndex;
	private int lastSelectedIndex;

	private void Awake()
	{
		lastSelectedIndex = -1;
	}

	public override void OnInspectorGUI ()
    {
    	GUILayout.Label("Loading in RUNTIME:");
    	base.OnInspectorGUI();

		string[] maps = uteMapDatabase.GetSingleton().EnumerateMaps().ToArray();

 		uteMapLoader myTarget = (uteMapLoader) target;

 		if(myTarget.currentMapIndex!=lastSelectedIndex)
 		{
 			lastSelectedIndex = myTarget.currentMapIndex;
			myTarget.SetMap(maps[myTarget.currentMapIndex]);
    	}

        EditorGUILayout.BeginHorizontal();
		myTarget.currentMapIndex = EditorGUILayout.Popup("Map to Load: ",myTarget.currentMapIndex, maps, EditorStyles.popup);
        EditorGUILayout.EndHorizontal();

        GUILayout.Label("-------------");

        GUILayout.Label("Loading in EDITOR:");
        if(GUILayout.Button("Load Map In Editor Scene Now"))
        {
        	myTarget.LoadMap();
        }
    }
}