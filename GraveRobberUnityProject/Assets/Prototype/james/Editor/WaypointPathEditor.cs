using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(WaypointPath))]
public class WaypointPathEditor : Editor {

	public void OnSceneGUI()
	{
		WaypointPath path = (WaypointPath)target;

		for (int i = 0; i < path.WaypointCount; ++i)
		{
			path.SetWaypoint(i, Handles.PositionHandle(path.GetWaypoint(i), path.transform.rotation));
		}
	}
}
